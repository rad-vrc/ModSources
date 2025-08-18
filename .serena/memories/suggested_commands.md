Common dev commands (Windows PowerShell):
- tModLoader build & test: Use in-game Mod Sources -> Build + Reload; or run tModLoader with -developer mode.
- Git:
  - git status
  - git add -A
  - git commit -m "message"
  - git push
- Search/replace:
  - VS Code global search (Ctrl+Shift+F), replace (Alt+R for regex)
- Packaging/upload: Use tModLoader Workshop publish UI from Mod Sources (GUI-based).

Optional scripts:
- If using dotnet tools, ensure .NET SDK matches tModLoader requirements (usually managed by tModLoader, no direct dotnet build).

Diagnostics:
- Inspect logs: %USERPROFILE%\Documents\My Games\Terraria\tModLoader\Logs
- Toggle DEBUG logging by building a Debug configuration if available, else add conditional logs.
