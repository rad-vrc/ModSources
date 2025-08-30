# ExampleMod Structure Summary (tModLoader 1.4.4)

Top-level
- ExampleMod.cs / .csproj / .sln / description*.txt / icons
- ExampleMod.ModCalls.cs, ExampleMod.Networking.cs (partial)
- Localization/: en-US, zh-Hans, ru-RU (+ Configs-specific hjson)
- Assets/: Effects, Music, Sounds (Items/Guns), Textures (Backgrounds/Bestiary/Menu)
- Common/: cross-cutting features
  - Commands/, Configs/ (CustomDataTypes/, CustomUI/, ModConfigShowcases/)
  - CustomVisualEquipType/, EntitySources/
  - GlobalBossBars/, GlobalBuffs/, GlobalItems/, GlobalNPCs/, GlobalProjectiles/, GlobalPylons/, GlobalTiles/, GlobalWalls/
  - ItemDropRules/ (DropConditions/), Players/, Systems/
  - UI/: Example UIs (CoinsUI, DisplaySets, FullscreenUI, InGameNotification, ResourceUI, ResourceOverlay)
- Content/: game content definitions (autoload types)
  - Biomes/, BossBars/, BossBarStyles/, Buffs/, BuilderToggles/, Clouds/
  - Currencies/, CustomModType/, DamageClasses/, Dusts/, EmoteBubbles/, Hairs/
  - Items/: Accessories, Ammo, Armor (Vanity), Consumables, Mounts, Placeable (Banners/Furniture/Unused), Tools, Weapons
  - Mounts/
  - NPCs/: Gores/, MinionBoss/ (with Gores/), TownPets/
  - Pets/: ExampleLightPet/, ExamplePet/, MinionBossPet/
  - Prefixes/, Projectiles/ (Minions/, Rockets/), Rarities/
  - TileEntities/, Tiles/ (Banners/, Furniture/ (Unused/), Plants/), Walls/
- Old/: legacy/archived examples for reference (not active)
- Properties/: launchSettings.json

Conventions
- Separation: Common (globals/systems/ui) vs Content (entities/types)
- Examples cover: assets pipeline, UI, worldgen, systems, mod calls, networking, configs
- Localization split by culture and separate Configs hjson files

Mapping Tips (to RadQoL)
- RadQoL/Common ↔ ExampleMod/Common
- RadQoL/Content ↔ ExampleMod/Content
- Use ExampleMod examples as templates for new hooks: GlobalItems/NPCs/Projectiles, Systems, UI
