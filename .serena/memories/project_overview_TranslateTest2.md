Purpose: Terraria tModLoader 1.4.4 mod (TranslateTest2). Provides AI Phone item/UI, inventory drag features, prefixes, and localization.
Tech stack: C# (.NET 8), tModLoader API, HJSON for localization, PowerShell for utility scripts.
Structure: Root has TranslateTest2/ (csproj, code, assets), Localization (en-US/ja-JP HJSON), Systems/UI/Items/Prefixes etc. Added scripts/check-hjson.ps1 and VS Code tasks.
Conventions: Weak references to other mods; localization en/ja synced; Keybinds use nested object with quoted display name; InterfaceScaleType.UI for UI.
Build: Use VS Code task "Build TranslateTest2 (dotnet build)"; close tModLoader or disable mod to avoid TML003.
Run: Through Terraria/tModLoader; UI draws via LegacyGameInterfaceLayer.
Tests/Checks: Run task "HJSON Audit (quick)" or combined "Build + HJSON Audit".
Notes: Radial UI uses dot-product selection; relative rotate mode (Q/E, wheel); debug overlays F3–F6. Keybind registered as "AI Phone: Hold Radial Wheel" (default V).