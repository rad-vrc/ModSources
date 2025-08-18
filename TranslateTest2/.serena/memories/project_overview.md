Project purpose: A tModLoader (Terraria) mod adding on-the-fly Japanese translation for tooltips and item names, with DeepL integration and a local dictionary, plus custom Summoner/Whip prefixes and localization assets.

Tech stack:
- C# (.NET, Microsoft.NET.Sdk) with tModLoader targets
- tModLoader Mod API (Mod, GlobalItem, ModCommand, hooks like On_Projectile)
- DeepL REST API via HttpClient
- HJSON for localization (en-US, ja-JP)

Key components:
- TranslateTest2.cs: Mod entry; registers hooks (Projectile.AI, GetWhipSettings), configures DeepL and tooltip translator
- Core/TooltipTranslator.cs: Loads dictionary from Assets/tooltip_dict.txt (fallbacks), translates lines with exact and substring replacements
- Core/DeepLTranslator.cs: Config-driven DeepL client, async translation with batching, caching, retries, proxy support
- Items/TooltipTranslateGlobalItem.cs: Modifies item tooltip lines; protects star/bracket segments; name reconstruction (Core/NameComposer); uses dictionary and DeepL when Left Shift is pressed
- Core/NameComposer.cs: Reorders English-like item names (prefixes + base + of + suffixes) into Japanese order; token-level DeepL caching and rules
- Core/TextLangHelper.cs: Detects Japanese vs. non-Japanese content, decides translation necessity
- Commands/DeepLCommand.cs: /deepl status|test
- Localization: en-US and ja-JP HJSON files for config UI and prefix display text
- Systems/*: placeholders (empty) for UI/Menu/Chat translation systems (future work)

Configuration (ClientConfig): DeepL API key, target language, toggles; batching/retry/timeout; proxy; name reconstruction, bracket-skip.

Build/run:
- Uses tModLoader.targets; build and run through tModLoader environment (Visual Studio or dotnet build under tModLoader). No standard unit tests.

Notable conventions:
- Logging via Mod.Logger; defensive try-catch around hooks
- Casing: StringComparer.Ordinal for dictionary keys; precise control over Japanese token composition
- Shift to activate runtime translation to avoid unnecessary DeepL calls

Potential gaps:
- Systems (UI/Chat/Menu/UniversalTextTranslateSystem) are empty
- No explicit build commands or tasks.json; relies on tModLoader tooling inside Terraria
- No unit tests; manual validation in-game

When task is completed: Rebuild the mod, launch tModLoader, enable the mod, test /deepl commands, verify tooltips with Shift held.
