Per-file summary: ExampleMod/Common/GlobalNPCs (tML 1.4.4)

1) BuffImmuneGlobalNPC.cs
- SetStaticDefaults: use BuffID.Sets.GrantImmunityWith to inherit immunities (Ichor→BetsysCurse); adjust NPCID.Sets.SpecificDebuffImmunity for DesertGhoulCrimson (Ichor=true) and Paladin (Ichor=false). Note: prefer GrantImmunityWith; only tweak NPC.buffImmune in SetDefaults for complex logic.

2) DamageModificationGlobalNPC.cs
- InstancePerEntity; flag exampleDefenseDebuff reset in ResetEffects; ModifyIncomingHit multiplies modifiers.Defense; DrawEffects tint (G=0) when active.

3) DamageOverTimeGlobalNPC.cs
- InstancePerEntity; exampleJavelinDebuff; UpdateLifeRegen scans Main.ActiveProjectiles for ExampleJavelinProjectile aistate; stacks lifeRegen negative and damage text.

4) EmotePickerGlobalNPC.cs
- PickEmote: add ExampleBiomeEmote when ExampleSurfaceBiome active (random); add MinionBossEmote when DownedBossSystem flag (random); return base.

5) ExampleBestiaryGlobalNPC.cs
- Adds custom IBestiaryInfoElement (ImportantFlavorTextElement) with red UIText and panel; category FlavorText; uses localization key Bestiary.ImportantFlavorText; SetBestiary adds to all entries.

6) ExampleNPCHappiness.cs
- SetStaticDefaults: NPCHappiness.Get(Guide).SetNPCAffection(ExamplePerson, Love) and SetBiomeAffection<ExampleSurfaceBiome>(Love).

7) ExampleNPCLoot.cs
- ModifyNPCLoot: add ExampleItem to non-critters; Journey-only present by condition; Guide: remove name-gated GreenCap and add unconditional; BloodNautilus: adjust SanguineStaff normal drop chance by editing DropBasedOnExpertMode→CommonDrop; QueenBee: extend OneFromOptionsNotScaledWithLuckDropRule to add WaspNest; Crimera/Corruptor cross-evil drops; slimes daytime ExampleSword; ModifyGlobalLoot: ExampleSoul 20% via ExampleSoulCondition.

8) ExampleNPCNetSync.cs
- AppliesToEntity: Sharkron2 only; OnSpawn: if spawned by Cthulunado during Blood Moon, set differentBehavior; Send/ReceiveExtraAI bit; AI scales npc if flag.

9) ExampleNPCShop.cs
- ModifyShop: add items/prices to Dryad/Stylist/BestiaryGirl/Cyborg; demonstrate shopCustomPrice & shopSpecialCurrency; demonstrate identifying Merchant shop (3 styles); InsertAfter; GetEntry.Disable / TryGetEntry safe; AddCondition; custom Condition with localization key; conditional RedPotion fallback.

10) ExampleResourcePickupGlobalNPC.cs
- OnKill: mimic heart/star drops; spawn ExampleResourcePickup near closest player if resource not full and RNG checks; use ExampleResourcePlayer.

11) GuideGlobalNPC.cs
- AppliesToEntity: Guide; AI: scale/color; EmoteBubblePosition flip/front; PartyHatPosition offset.

12) ProjectileModificationGlobalNPC.cs
- InstancePerEntity; tracks timesHitByModifiedProjectiles (used with projectile-side modifications).