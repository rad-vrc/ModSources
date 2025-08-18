When a task/change is completed:
- Build: Use tModLoader Build+Reload (in-game) or build solution in IDE; ensure no compile errors
- Smoke test: Launch a world, enable mod, check logs for hook registration and "TranslateTest2 loaded successfully"
- Config sanity: Enter DeepL API key, verify /deepl status shows Enabled=true; run /deepl test Hello
- Tooltip check: Hover various items holding Left Shift; expect dictionary replacements and DeepL-queued translations (may appear after a short delay)
- Name reconstruction: Hover items with names like "Legendary X of Y" and confirm Japanese order
- Prefix localization: Verify prefix display names and description keys resolve; look for "攻撃範囲" in ExtendedDescr when JA
- Regression: Verify no crashes in projectile AI/whip hooks; check logs for warnings/errors
- Optional cleanup: Clear caches by reloading mod or via TooltipTranslateGlobalItem.ClearCaches() when needed
