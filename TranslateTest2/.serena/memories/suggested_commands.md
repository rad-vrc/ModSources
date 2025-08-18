Common developer commands (Windows PowerShell):

Build/pack inside tModLoader:
- Launch tModLoader, open Mods menu, Build + Reload (preferred)
- Or build via Visual Studio using tModLoader project integration (TranslateTest2.sln)

If dotnet CLI works with tModLoader.targets (environment dependent):
- dotnet build -c Debug
- dotnet build -c Release

In-game testing:
- Enable the mod in Mods menu
- Use command: /deepl status
- Test translation: /deepl test Hello world
- Hold Left Shift while hovering items to trigger tooltip/name translation

Config:
- Open Mod Configs -> Client -> TranslateTest2
- Set DeepL API Key; adjust TargetLang (e.g., JA, EN-US)

Logs/diagnostics:
- Check client.log for "TranslateTest2" logs
- Watch hook registration status and prefix diagnostics in PostSetupContent

Note: No unit tests or external lint configured; rely on IDE analyzers and compiler.
