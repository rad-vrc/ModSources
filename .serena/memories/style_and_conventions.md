Code style and conventions:
- C# conventions (PascalCase for classes/methods, camelCase for locals/fields where appropriate). Nullable checks and exception safety in hooks.
- Logging via Mod.Logger with Info/Warn/Error/Debug; avoid leaking secrets (DeepL key masked as presence flag).
- Threading: Use ConcurrentQueue/ConcurrentDictionary; avoid blocking in hooks; async/await with ConfigureAwait(false) in DeepL translator.
- Localization keys: Mods.TranslateTest2.* in HJSON; prefer exact-match dictionary before substring replacements.
- Name composition: Insert "の" only when appropriate; do not duplicate if already present; skip for numeric-like and joiner tokens; directly concatenate for JP adjective-like endings (な/い/る/く/的/た).
- Defensive bounds for gameplay modifiers (e.g., WhipRangeMult clamped to [0.1, 10]).
