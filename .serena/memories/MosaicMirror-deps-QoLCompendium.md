QoLCompendium MosaicMirror dependency cheat sheet (tML 1.4.4)

Target item
- Class/Path: QoLCompendium.Content.Items.Tools.Mirrors.MosaicMirror (References/QoLCompendium/Content/Items/Tools/Mirrors/MosaicMirror.cs)
- Mode (int): 0=CursedMirror, 1=MirrorOfReturn, 2=TeleportationMirror
- Research/Consume: Item.ResearchUnlockCount = 1; OnConsumeItem increments stack → non-consumable
- Save/Load: TagCompound key "MosaicMirrorMode"
- Dynamic name: UpdateInventory → Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.{CursedMirror|MirrorOfReturn|TeleportationMirror}"))
- Config-gated tooltips: ModifyTooltips observes itemConfig.Mirrors; if !InformationAccessories → append "Mods.QoLCompendium.CommonItemTooltips.Disabled" to Tooltip1 (red)
- UseStyle per mode:
  • (0) Cursed: if player.lastDeathPostion set → Teleport to last death; else show text; kills grapples/aiStyle 7 proj; dust vfx
  • (1) Return: DoPotionOfReturnTeleportationAndSetTheComebackPoint(); kills grapples/proj; dust vfx
  • (2) Teleportation: TeleportationPotion(); kills grapples/proj; dust vfx
- Right-click cycle: CanRightClick=true; RightClick → Mode=(Mode+1)%3
- Info accessory side effect: UpdateInfoAccessory sets multiple InfoPlayer flags when itemConfig.InformationAccessories
- Warp effect: UpdateInventory sets QoLCPlayer.warpMirror = true (enables map TP-to-NPC via QoLCPlayer)
- Recipe: Requires CursedMirror, MirrorOfReturn, TeleportationMirror, WarpMirror, WormholeMirror; Tile 114 (Tinkerer's Workshop)

Related items & systems
- WormholeMirror: Hooks On_Player.HasUnityPotion/TakeUnityPotion; counts MosaicMirror as wormhole provider (no potion consumption/virtual presence)
- WarpMirror: UpdateInventory sets QoLCPlayer.warpMirror = true
- QoLCPlayer.PostUpdateMiscEffects: If Main.mapFullscreen && mouse left click on TownNPC rectangle && warpMirror → teleport to NPC (closes map)
- MapTeleporting.ModSystem: If mainConfig.MapTeleporting, enables map right-click teleport to arbitrary coordinates (independent of mirrors)
- Config gating:
  • Item load & tooltips respect QoLCompendium.itemConfig.Mirrors
  • Info accessories behavior toggled via QoLCompendium.itemConfig.InformationAccessories

Integration tips for TranslateTest2
- Presence check: ModLoader.TryGetMod("QoLCompendium", out var qlc)
- Resolve item type: qlc?.TryFind<ModItem>("MosaicMirror", out var mi) → mi?.Type
- Gate fusion recipe/feature to only run when qlc!=null (and optionally qlc.Version/feature checks)
- If wrapping Mosaic behavior, persist Mode with TagCompound key "MosaicMirrorMode" for continuity

Edge cases
- CursedMirror mode: No last death → message only (local player); ensure no NRE on zero vector
- Map input overlap: Left click NPC TP (QoLCPlayer) vs right click free TP (MapTeleporting) may conflict with other map UI mods; keep modifier keys/priority in mind
- Grapple/projectile cleanup is part of mirror behavior—replicate if fusing retains those effects