Style and conventions:
- C#: follow tModLoader patterns; classes under TranslateTest2.* namespaces
- Defensive coding in hooks: null checks, try/catch, logging with Logger?.Info/Warn/Error/Debug
- Async network via HttpClient; cancellation tokens with timeouts
- Configuration-driven behavior (ClientConfig) with immediate ApplyConfig on OnChanged/OnLoaded
- Localization keys in HJSON under Localization/*.hjson; English and Japanese files kept in sync; comments in files acceptable
- Tooltip dictionary format: UTF-8 text, lines of "source => target", # comments allowed; prefer longer-key-first replacement
- Item tooltip translation only when Left Shift is pressed; protect star/bracket segments from translation
- NameComposer reorders English pattern "<prefixes> <base> of <suffixes>" into Japanese; avoid translating bracket/star tokens when SkipBracketContentInNames=true
- Prefer StringComparer.Ordinal for dictionary maps; case-insensitive exact-match fallback
- Batch DeepL requests; respect _textMaxLen (4000) and throttling; cache results

Design patterns:
- Singletons via static classes for Cross-cutting services (DeepLTranslator, TooltipTranslator)
- Clear separation: Core/* for helpers/services, Items/* for tML hooks, Commands/* for chat commands
- Fail-safe: when DeepL unavailable, fall back to original text
