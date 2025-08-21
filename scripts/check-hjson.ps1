param(
  [string]$Root = "$(Split-Path -Parent $PSScriptRoot)"
)

$ErrorActionPreference = 'Stop'

Write-Host "== HJSON quick checks ==" -ForegroundColor Cyan

# 1) Keybinds: forbid flat '"...".DisplayName: ...' entries; require nested object form
$hjsonFiles = Get-ChildItem -Path "$Root" -Recurse -Filter "*_Mods.*.hjson"
$badKeybindLines = @()
foreach ($f in $hjsonFiles) {
  $lines = Get-Content $f.FullName
  for ($i=0; $i -lt $lines.Count; $i++) {
    $line = $lines[$i]
    if ($line -match '^[\t ]*"[^\"]+"\.DisplayName\s*:') {
      # Allow only when inside a nested object block, which typically has preceding line with the quoted key followed by '{'
      # This heuristic flags flat dot-form likely used by mistake in Keybinds
      if ($lines -notcontains '{' -or $line -match '^.*Keybinds.*$') {
        $badKeybindLines += [pscustomobject]@{ File=$f.FullName; Line=$i+1; Text=$line }
      }
    }
  }
}

if ($badKeybindLines.Count -gt 0) {
  Write-Host "[FAIL] Suspicious dot-display entries found:" -ForegroundColor Red
  $badKeybindLines | ForEach-Object { Write-Host (" - {0}:{1}  {2}" -f $_.File, $_.Line, $_.Text) }
} else {
  Write-Host "[OK] No suspicious dot-display entries detected" -ForegroundColor Green
}

# 2) Placeholder parity samples (extend as needed)
$tests = @(
  @{ key='Items.AiPhone.ModeFormat'; en='AI Phone ({0})'; ja='AIフォン（{0}）' },
  @{ key='PrefixOracleDescr'; en=''; ja='' } # reserved example; no placeholders to compare
)

function Get-Placeholders([string]$s) {
  if (-not $s) { return @() }
  $matches = [regex]::Matches($s, '\{[0-9A-Za-z]+\}')
  return $matches | ForEach-Object { $_.Value } | Sort-Object -Unique
}

$parityOk = $true
foreach ($t in $tests) {
  $enPh = Get-Placeholders $t.en
  $jaPh = Get-Placeholders $t.ja
  $missing = $enPh | Where-Object { $_ -notin $jaPh }
  $extra   = $jaPh | Where-Object { $_ -notin $enPh }
  if ($missing.Count -gt 0 -or $extra.Count -gt 0) {
    $parityOk = $false
    Write-Host ("[FAIL] Placeholder mismatch for {0}: missing={1} extra={2}" -f $t.key, ($missing -join ','), ($extra -join ',')) -ForegroundColor Red
  }
}
if ($parityOk) { Write-Host "[OK] Placeholder parity samples passed" -ForegroundColor Green }

# exit code for CI-like gating
if ($badKeybindLines.Count -gt 0 -or -not $parityOk) { exit 1 } else { exit 0 }
