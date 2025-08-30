param(
  [Parameter(Mandatory=$true)]
  [string]$InPath,
  [string]$OutPath,
  [ValidateSet('azure','deepl')]
  [string]$Provider = $env:TML_TRANSLATOR,
  [int]$MaxCharsPerBatch = 3500,
  [switch]$WhatIf,
  [switch]$PreserveMarkdownCodeBlocks
)

$ErrorActionPreference = 'Stop'

function Write-Info($msg){ Write-Host "[translate-text] $msg" -ForegroundColor Cyan }
function Write-Warn($msg){ Write-Host "[translate-text] $msg" -ForegroundColor Yellow }
function Write-Err($msg){ Write-Host "[translate-text] $msg" -ForegroundColor Red }

if (-not (Test-Path -LiteralPath $InPath)){
  Write-Err "Input not found: $InPath"
  exit 1
}

if (-not $OutPath){ $OutPath = $InPath }

if (-not $Provider){
  if ($env:AZURE_TRANSLATOR_KEY -and $env:AZURE_TRANSLATOR_ENDPOINT) { $Provider = 'azure' }
  elseif ($env:DEEPL_API_KEY) { $Provider = 'deepl' }
}

if (-not $Provider){
  Write-Err "No provider configured. Set TML_TRANSLATOR=azure|deepl and required env vars."
  exit 2
}

Write-Info ("Provider: {0}" -f $Provider)
Write-Info ("Input: {0}" -f $InPath)
Write-Info ("Output: {0}" -f $OutPath)

function Invoke-AzureTranslateBatch([string[]]$texts){
  $endpoint = $env:AZURE_TRANSLATOR_ENDPOINT
  $key = $env:AZURE_TRANSLATOR_KEY
  if (-not $endpoint -or -not $key) { throw "Azure Translator env not set" }
  $url = "$endpoint/translate?api-version=3.0&to=ja"
  $body = @()
  foreach($t in $texts){ $body += @{ Text = $t } }
  $json = $body | ConvertTo-Json -Depth 5
  $headers = @{ 'Ocp-Apim-Subscription-Key' = $key; 'Content-Type'='application/json' }
  $resp = Invoke-RestMethod -Method Post -Uri $url -Headers $headers -Body $json -TimeoutSec 60
  return ,($resp | ForEach-Object { $_.translations[0].text })
}

function Invoke-DeepLTranslateBatch([string[]]$texts){
  $key = $env:DEEPL_API_KEY
  if (-not $key) { throw "DeepL API key not set" }
  $uri = 'https://api-free.deepl.com/v2/translate'
  $pairs = @()
  foreach($t in $texts){ $pairs += "text=$([uri]::EscapeDataString($t))" }
  $pairs += 'target_lang=JA'
  $body = $pairs -join '&'
  $resp = Invoke-RestMethod -Method Post -Uri $uri -Headers @{ 'Authorization'="DeepL-Auth-Key $key" } -Body $body -ContentType 'application/x-www-form-urlencoded' -TimeoutSec 60
  return ,($resp.translations | ForEach-Object { $_.text })
}

function Split-IntoChunksByLength([string[]]$lines, [int]$maxLen){
  $chunks = New-Object System.Collections.Generic.List[string]
  $buf = New-Object System.Text.StringBuilder
  foreach($line in $lines){
    if (($buf.Length + $line.Length + 1) -gt $maxLen -and $buf.Length -gt 0){
      $chunks.Add($buf.ToString())
      $null = $buf.Clear()
    }
    [void]$buf.AppendLine($line)
  }
  if ($buf.Length -gt 0){ $chunks.Add($buf.ToString()) }
  return ,$chunks
}

function Invoke-TranslateText([string]$text){
  if ([string]::IsNullOrWhiteSpace($text)) { return $text }
  $lines = $text -split "\r?\n"

  if ($PreserveMarkdownCodeBlocks){
    # Split by fenced code blocks, translate only non-code parts
    $pattern = "(?ms)^(\s*```.*?$\n.*?^```\s*$)"
    $parts = [System.Text.RegularExpressions.Regex]::Split($text, $pattern)
    $out = New-Object System.Text.StringBuilder
    for($i=0; $i -lt $parts.Length; $i++){
      $segment = $parts[$i]
      $isCode = ($i % 2 -eq 1)
      if ($isCode){ [void]$out.Append($segment); continue }
      $chunks = Split-IntoChunksByLength ($segment -split "\r?\n") $MaxCharsPerBatch
      foreach($chunk in $chunks){
        if ([string]::IsNullOrWhiteSpace($chunk)) { [void]$out.Append($chunk); continue }
        $translated = if ($Provider -eq 'azure') { Invoke-AzureTranslateBatch @($chunk) } else { Invoke-DeepLTranslateBatch @($chunk) }
        [void]$out.Append($translated[0])
        Start-Sleep -Milliseconds 200
      }
    }
    return $out.ToString()
  }
  else {
    $chunks = Split-IntoChunksByLength $lines $MaxCharsPerBatch
    $out = New-Object System.Text.StringBuilder
    foreach($chunk in $chunks){
      if ([string]::IsNullOrWhiteSpace($chunk)) { [void]$out.Append($chunk); continue }
      $translated = if ($Provider -eq 'azure') { Invoke-AzureTranslateBatch @($chunk) } else { Invoke-DeepLTranslateBatch @($chunk) }
      [void]$out.Append($translated[0])
      Start-Sleep -Milliseconds 150
    }
    return $out.ToString()
  }
}

if ($WhatIf){
  Write-Info "WhatIf specified. No changes will be made."
  exit 0
}

$raw = Get-Content -LiteralPath $InPath -Raw -Encoding UTF8
$ja = Invoke-TranslateText $raw
Set-Content -LiteralPath $OutPath -Value $ja -Encoding UTF8 -NoNewline
Write-Info "Done."
