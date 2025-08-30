param(
    [string]$TmlExe = "D:\SteamLibrary\steamapps\common\tModLoader\tModLoader.exe",
    [string]$TmlDll = "D:\SteamLibrary\steamapps\common\tModLoader\tModLoader.dll",
    [string]$DotnetExe = "D:\SteamLibrary\steamapps\common\tModLoader\dotnet\dotnet.exe",
    [string]$ModSource = "D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\RadQoL\",
    [string]$EacDll = "D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\RadQoL\bin\Debug\net8.0\RadQoL.dll",
    [switch]$Server
)

$ErrorActionPreference = 'Stop'
Write-Host "== tModLoader -build runner =="

# Normalize paths (remove trailing slashes that may confuse tML arg parsing)
$ModSource = [System.IO.Path]::GetFullPath(($ModSource -replace "[\\/]+$",""))
$EacDll = [System.IO.Path]::GetFullPath($EacDll)

Write-Host "tML:    $TmlExe"
Write-Host "tMLdll: $TmlDll"
Write-Host "dotnet: $DotnetExe"
Write-Host "Source: $ModSource"
Write-Host "EAC:    $EacDll"

if (-not (Test-Path -LiteralPath $ModSource)) {
    Write-Error "ModSources path not found: $ModSource"
    exit 1
}

if (-not (Test-Path -LiteralPath $EacDll)) {
    Write-Warning "EAC dll not found: $EacDll (will still run -build without it if needed)"
}

$arguments = @()
if ($Server) { $arguments += '-server' }
$arguments += @('-build', $ModSource)
if (Test-Path -LiteralPath $EacDll) { $arguments += @('-eac', $EacDll) }

# Prefer tModLoader.exe; fallback to dotnet + tModLoader.dll
$useExe = Test-Path -LiteralPath $TmlExe
$useDll = (Test-Path -LiteralPath $DotnetExe) -and (Test-Path -LiteralPath $TmlDll)

if (-not $useExe -and -not $useDll) {
    if (-not (Test-Path -LiteralPath $TmlExe)) { Write-Warning "Missing tModLoader.exe: $TmlExe" }
    if (-not (Test-Path -LiteralPath $DotnetExe)) { Write-Warning "Missing dotnet.exe: $DotnetExe" }
    if (-not (Test-Path -LiteralPath $TmlDll)) { Write-Warning "Missing tModLoader.dll: $TmlDll" }
    Write-Error "No valid tModLoader launcher found. Specify -TmlExe or -DotnetExe/-TmlDll."
    exit 1
}

# Change directory to tModLoader root so adjacent assemblies (e.g., ReLogic.dll) are resolved
$tMLRoot = if ($useExe) { Split-Path -Parent $TmlExe } else { Split-Path -Parent $TmlDll }
$prev = Get-Location
try {
    Set-Location -LiteralPath $tMLRoot
    if ($useExe) {
        Write-Host "Launching (exe): $TmlExe $($arguments -join ' ')"
        & $TmlExe @arguments
    }
    else {
        Write-Host "Launching (dotnet): $DotnetExe $TmlDll $($arguments -join ' ')"
        & $DotnetExe $TmlDll @arguments
    }
    exit $LASTEXITCODE
}
finally {
    Set-Location -LiteralPath $prev
}
