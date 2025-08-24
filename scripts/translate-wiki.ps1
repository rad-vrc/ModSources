param(
  [string]$Root = "D:/dorad/Documents/My Games/Terraria/tModLoader/ModSources/References/tModLoader.wiki_JP",
  [switch]$WhatIf,
  [int]$MaxCharsPerBatch = 3500,
  [ValidateSet('azure','deepl')]
  [string]$Provider = $env:TML_TRANSLATOR
)

# Purpose: Batch-translate Markdown files to Japanese.
# - Preserves fenced code blocks (``` ... ```)
# - Requires translation API credentials via environment variables
#   Azure:   AZURE_TRANSLATOR_KEY, AZURE_TRANSLATOR_ENDPOINT (e.g. https://api.cognitive.microsofttranslator.com)
#   DeepL:   DEEPL_API_KEY
# - Usage examples:
#   powershell -ExecutionPolicy Bypass -File scripts/translate-wiki.ps1 -Root "<path>" -Provider azure
#   powershell -ExecutionPolicy Bypass -File scripts/translate-wiki.ps1 -WhatIf

$ErrorActionPreference = 'Stop'

function Write-Info($msg){ Write-Host "[translate] $msg" -ForegroundColor Cyan }
function Write-Warn($msg){ Write-Host "[translate] $msg" -ForegroundColor Yellow }
function Write-Err($msg){ Write-Host "[translate] $msg" -ForegroundColor Red }

if (-not (Test-Path $Root)) {
  Write-Err "Root not found: $Root"
  exit 1
}

if (-not $Provider) {
  if ($env:AZURE_TRANSLATOR_KEY -and $env:AZURE_TRANSLATOR_ENDPOINT) { $Provider = 'azure' }
  elseif ($env:DEEPL_API_KEY) { $Provider = 'deepl' }
}

if (-not $Provider) {
  Write-Err "No provider configured. Set TML_TRANSLATOR=azure or deepl and corresponding env vars."
  exit 2
}

Write-Info "Provider: $Provider"

$files = Get-ChildItem -Path $Root -Recurse -File -Include *.md | Sort-Object FullName
Write-Info ("Target files: {0}" -f $files.Count)

# Helpers
# (removed unused Split-MarkdownBlocks)

function Get-TranslatableSegments([string]$text){
  # Split by fenced code blocks and translate only non-code parts
  $segments = @()
  $pattern = "(?ms)^(\s*```.*?$\n.*?^```\s*$)"  # matches fenced blocks
  $idx = 0
  [System.Text.RegularExpressions.Regex]::Split($text, $pattern) | ForEach-Object {
    $s = $_
    if ($null -eq $s) { return }
    # Even indices are non-code; odd are code blocks (due to split capturing)
    $isCode = ($idx % 2 -eq 1)
    $segments += [PSCustomObject]@{ isCode=$isCode; text=$s }
    $idx++
  }
  return ,$segments
}

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

function Invoke-TranslateSegments([PSCustomObject[]]$segments){
  # Translate only non-code segments, chunking by char length
  $out = New-Object System.Collections.Generic.List[string]
  $batch = New-Object System.Collections.Generic.List[string]
  $mapIdx = @()
  $charSum = 0
  for($i=0; $i -lt $segments.Count; $i++){
    $seg = $segments[$i]
    if ($seg.isCode) { # flush current batch and append code as-is
      if ($batch.Count -gt 0){
        $translated = if ($Provider -eq 'azure') { Invoke-AzureTranslateBatch $batch } else { Invoke-DeepLTranslateBatch $batch }
        for($k=0; $k -lt $translated.Count; $k++){ $out.Add($translated[$k]) }
        $batch.Clear(); $charSum = 0
      }
      $out.Add($seg.text)
      continue
    }
    # non-code: accumulate
    $t = $seg.text
    if ([string]::IsNullOrWhiteSpace($t)) { $out.Add($t); continue }
    if (($charSum + $t.Length) -gt $MaxCharsPerBatch -and $batch.Count -gt 0){
      $translated = if ($Provider -eq 'azure') { Invoke-AzureTranslateBatch $batch } else { Invoke-DeepLTranslateBatch $batch }
      for($k=0; $k -lt $translated.Count; $k++){ $out.Add($translated[$k]) }
      $batch.Clear(); $charSum = 0
    }
    $batch.Add($t)
    $charSum += $t.Length
    # mark placeholder to append translated later
    $out.Add($null)
  }
  if ($batch.Count -gt 0){
    $translated = if ($Provider -eq 'azure') { Invoke-AzureTranslateBatch $batch } else { Invoke-DeepLTranslateBatch $batch }
    for($k=0; $k -lt $translated.Count; $k++){ $out.Add($translated[$k]) }
    $batch.Clear(); $charSum = 0
  }

  # Merge: replace nulls with sequential translated strings
  $translatedIdx = 0
  for($i=0; $i -lt $out.Count; $i++){
    if ($null -eq $out[$i]) { $out[$i] = $translated[$translatedIdx]; $translatedIdx++ }
  }
  return ($out -join "")
}

$processed = 0
foreach($f in $files){
  $rel = $f.FullName.Substring($Root.Length).TrimStart('\\','/')
  Write-Info "Processing: $rel"
  $raw = Get-Content -LiteralPath $f.FullName -Raw -Encoding UTF8

  if ($raw -match "(?i)^<!--\s*translated:\s*ja\s*-->"){
    Write-Info "Already marked translated, skipping"
    continue
  }

  if ($WhatIf){ continue }

  try {
    $segs = Extract-TranslatableSegments $raw
    $ja = Translate-Parts $segs
    $header = "<!-- translated: ja; provider=$Provider; ts=$(Get-Date -Format o) -->`n"
    $outText = $header + $ja
    Set-Content -LiteralPath $f.FullName -Value $outText -Encoding UTF8 -NoNewline
    $processed++
  }
  catch {
    Write-Warn "Failed: $rel : $($_.Exception.Message)"
  }
}

Write-Info ("Done. Updated: {0} files" -f $processed)
