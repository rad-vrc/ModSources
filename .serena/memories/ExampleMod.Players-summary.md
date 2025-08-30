Per-file summary: ExampleMod/Common/Players (tML 1.4.4)

1) ExampleAccessorySlot.cs
- Three ModAccessorySlot examples: empty default slot; custom location/texture slot (centered UI, vanity display when dye present, background color overrides, hidden when empty); Wing slot with loadout support toggle via ModConfig, AcceptItem/ModifyDefaultSwapSlot for wings, enabled when helmet worn, hover text via LocalizedText, custom textures.

2) ExampleArmorSetBonusPlayer.cs
- ModPlayer for set bonus toggling: flags reset; ArmorSetBonusActivated cycles shadow style; ArmorSetBonusHeld toggles style after hold time; prints current style name.

3) ExampleBlinkingPlayer.cs
- PostUpdate modifies Player.eyeHelper frames: close eyes under water during normal blinking; override IsBlind state with custom timing between EyeClosed/HalfClosed.

4) ExampleCostumePlayer.cs
- Tracks accessory/vanity states; UpdateEquips applies Blocky buff; FrameEffects swaps equip visuals (alt when wet); ModifyDrawInfo rotates head based on velocity/biome; ModifyHurt disables sound; OnHurt plays specific sound.

5) ExampleDamageModificationPlayer.cs
- Crit damage additive; Example Dodge system: buff gating, cooldowns, visual counter; team damage absorption accessory logic with ModifyHurt/OnHurt math and proximity checks; DrawEffects tint; networking for dodge sync; PreUpdate timers; PostUpdateEquips state and visual fade.

6) ExampleExtraJumpModificationPlayer.cs
- ModifyExtraJumpDurationMultiplier doubles SandstormInABottle duration.

7) ExampleFishingPlayer.cs
- hasExampleCrateBuff flag; ModifyFishingAttempt raises crate chance; CatchFish: spawn NPC from rod/biome conditions with custom sonar UI; biome crate replacement logic; custom quest fish catch conditions; CanConsumeBait logic for ladybug + golden rod; ModifyCaughtFish stacks when using ladybug.

8) ExampleImmunityPlayer.cs
- HasExampleImmunityAcc flag; PostHurt adds 1s immune time per damage category if accessory equipped and not PvP.

9) ExampleInfoDisplayPlayer.cs
- Info accessory flag showMinionCount; ResetInfoAccessories (runs while paused); RefreshInfoAccessoriesFromTeamPlayers shares flags with teammates.

10) ExampleInventoryPlayer.cs
- AddStartingItems creates different kits for normal vs mediumcore death; ModifyStartingInventory removes vanilla IronAxe on Journey; demonstrates yield-like array literals.

11) ExampleKeybindPlayer.cs
- Keybind samples: RandomBuff on press with chat text; hold for 30 ticks message; double-tap within 15 ticks message; uses localized texts.

12) ExampleLuckPlayer.cs
- ModifyLuck adds +0.5 in hardmode; PreModifyLuck fakes Lantern Night, Garden Gnome, and cancels bad ladybug luck, returns true to allow vanilla calc.

13) ExampleRecipeMaterialPlayer.cs
- Finds nearby unopened chest under feet client-side; caches chest index and triggers Recipe.FindRecipes when changed; AddMaterialsForCrafting returns chest items and sets callback to SyncChestItem on consumption in MP.

14) ExampleResourcePlayer.cs
- Custom resource: current/max/max2, regen rate, magnet; Initialize default max; Reset/UpdateDead reset vars; PostUpdateMiscEffects regen tick; PostUpdate cap in god mode; HealExampleResource spawns CombatText and syncs via packet; includes static color and grab range constants.

15) ExampleShiftClickSlotPlayer.cs
- ShiftClickSlot: when gel in inventory, recolor and randomize rarity, play sound, block pickup; HoverSlot: set cursor override to FavoriteStar when hovering gel with shift.

16) ExampleStatIncreasePlayer.cs
- Permanent stat increases: ModifyMaxStats adds base from counts; SyncPlayer/ReceivePlayerSync; CopyClientState/SendClientChanges to sync; SaveData/LoadData persist counts.

17) ExampleWeaponEnchantmentPlayer.cs
- Weapon imbue flag; OnHit with item/proj adds ExampleDefenseDebuff to target for 3-6s; MeleeEffects/EmitEnchantmentVisualsAt spawn custom dust when imbued.
