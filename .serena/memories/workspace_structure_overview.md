workspace root: ModSources (tModLoader mods)
Top-level dirs: DPSPanel, Logs, ModAssemblies, RadQoL, References, scripts, TranslateTest2, _backup_removed (+ dot-config dirs)

DPSPanel: UI-based DPS tracker panel
- Common/Configs: ShowPlayerIconConfigElement.cs, ThemeConfigElement.cs
- Common/DamageCalculation: BossDamageTrackerSP.cs
- Helpers: Ass.cs, PanelPositionJsonHelper.cs
- UI: MainPanel.cs, MainSystem.cs, PlayerBar.cs, PlayerHead.cs, ToggleButton.cs, WeaponBar.cs

RadQoL: Main QoL mod
- Assets: tooltip_dict.txt
- Common: Commands, Compatibility, Config, DPS, GlobalItems/NPCs/Projectiles, Helpers, Networking, PetRenamer, Players, ShopExpander, Systems, UI, Utilities
- Content: Buffs, InfoDisplays, Items, NPCs, PetRenamer(json), Prefixes, Projectiles, Tiles, tooltip_dict.txt
- Core: DeepLTranslator.cs, InfoPlayer.cs (+ ModBiomes partial), NameComposer.cs, TextLangHelper.cs, TooltipTranslator.cs, TranslationService.cs
- Localization: en-US_Mods.RadQoL.hjson, ja-JP_Mods.RadQoL.hjson
- Map: AiPhoneMapLayer.cs
- Properties: AssemblyInfo.json, launchSettings.json
- Roots: RadQoL.csproj, RadQoL.sln, GlobalUsings.cs, RadQoL.cs, icons, build scripts, StackablePetItems.cs, StackablePetsGlobalItem.cs

References: Large set of external sources
- Terraria, tModLoader sources, various mod references, wiki text, textures

TranslateTest2: Auxiliary translation-focused mod
- Common, Content, Core, Localization, Map; many binaries in bin/obj

Notes: Building likely via scripts/build-tml.ps1; localization with HJSON; heavy reliance on tModLoader and external mods in ModAssemblies.