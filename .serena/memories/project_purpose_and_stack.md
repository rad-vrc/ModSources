Purpose: A tModLoader mod "TranslateTest2" that integrates SummonerPrefix features with advanced translation. It translates tooltips/UI/chat, reconstructs item names for Japanese grammar, and adds summoner/whip prefixes and projectile logic.

Tech stack: C# (.NET via tModLoader), Terraria mod API, VS/VS Code. Translation uses DeepL HTTP API with async batching, caching, retry. Localization via HJSON files. Concurrent collections for queues/cache.

Structure:
- TranslateTest2.cs: Mod entry, hooks registration, diagnostics.
- Config/: ClientConfig for DeepL and features.
- Core/: DeepLTranslator, TooltipTranslator (dictionary), NameComposer (JP name order), TextLangHelper.
- Items/: TooltipTranslateGlobalItem (apply translations to tooltips).
- Systems/: Chat/Menu/UI/Universal translation systems.
- Prefixes/: Many ModPrefix implementations and helpers (SummonerPrefix).
- GlobalProjectiles/SPGlobalProj.cs, SPModPlayer.cs, WhipTag*.cs: Summoner/whip logic and hooks.
- Localization/: en-US/ja-JP HJSON keys and reference files.

Conventions: C# standard naming (PascalCase for types/methods, camelCase for locals), explicit null/exception guards in hooks, logging via Mod.Logger. Defensive range checks for gameplay modifiers. Strings handled with culture-invariant comparisons for protocol.

Entrypoints: tModLoader loads the mod; no explicit CLI run. Build is via tModLoader mod build pipeline.

Key runtime behaviors: Tooltip dictionary exact/substring replacement; DeepL async queue with batch worker; NameComposer inserts/omits "の" based on JP heuristics (numeric/joiner/adjective-like endings). Hooks modify projectile AI and whip settings safely.
