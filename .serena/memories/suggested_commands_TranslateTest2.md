Build (close tModLoader first):
- Ctrl+Shift+B or Run Task: "Build + HJSON Audit"
- Run Task: "Build TranslateTest2 (dotnet build)"

HJSON quick audit:
- Run Task: "HJSON Audit (quick)"

Manual PowerShell (optional):
- powershell -NoProfile -ExecutionPolicy Bypass -File scripts/check-hjson.ps1

tModLoader build (alternative):
- Run Task: "Build TranslateTest2 (tModLoader)" (mod must be disabled in-game)
