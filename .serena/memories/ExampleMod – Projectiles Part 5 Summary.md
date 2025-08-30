# ExampleMod Projectiles – Part 5 Summary (Minions + Rockets)

Covers the remaining files under Content/Projectiles: Minions/ExampleSimpleMinion.cs and Rockets/*.

## Minions
- ExampleSimpleMinion.cs
  - Buff (ExampleSimpleMinionBuff): no save, no time display; refreshes while any minion projectile exists, otherwise removes.
  - Item (ExampleSimpleMinionItem): Staff minion; Gamepad whole-screen targeting; LockOnIgnoresCollision; mana 10; Summon damage; buffType set; Shoot() applies buff, manually spawns projectile, sets originalDamage, returns false.
  - Projectile (ExampleSimpleMinion):
    - Static: 4 frames; MinionTargettingFeature; projPet; MinionSacrificable; Cultist resistant.
    - Defaults: 18x28; tileCollide=false; friendly; minion=true; Summon damage; minionSlots=1f; penetrate=-1.
    - Behavior: CanCutTiles=false; MinionContactDamage=true.
    - AI(): CheckActive() (despawn on owner dead; keep timeLeft=2 while buff), then GeneralBehavior(), SearchForTargets(), Movement(), Visuals().
      - GeneralBehavior: idle above player, offset by minionPos; teleport back if >2000px (owner only, netUpdate); anti-overlap nudges vs other same-owner projectiles.
      - SearchForTargets: prefers owner.MinionAttackTargetNPC; otherwise scan ActiveNPCs with CanBeChasedBy(), LoS or closeThroughWall; sets Projectile.friendly = foundTarget.
      - Movement: homing toward target with speed=8/inertia=20; idle return with speed/inertia depending on distance; small poke if zero.
      - Visuals: rotation lean by vel.X, 5-tick frame cycler, white light.

## Rockets
- ExampleGrenadeProjectile.cs (grenade launched by weapons)
  - Sets: PlayerHurtDamageIgnoresDifficultyScaling=true; Explosive=true.
  - Defaults: friendly; penetrate=-1; Ranged; usesLocalNPCImmunity=true; localNPCHitCooldown=-1; timeLeft=180.
  - AI: when timeLeft<=3 → PrepareBombToBlow(); else spawn smoke dust; after 15 ticks add friction/grav; rotate by vel.X.
  - Tile collide: bouncy; return false.
  - PrepareBombToBlow: tileCollide=false; alpha=255; Resize(128,128); knockBack=8.
  - OnKill: sound Item62; Resize(22,22); smoke/fire dust & smoke gore; tile-damage left as comment.

- ExampleProximityMineProjectile.cs
  - Sets: IsAMineThatDealsTripleDamageWhenStationary=true; PlayerHurtDamageIgnoresDifficultyScaling=true; Explosive=true.
  - Defaults: friendly; penetrate=-1; Ranged.
  - AI: if timeLeft<=3 → Prepare; else fade nearly invisible when still (alpha up to 200) else opaque and smoke dust; gravity 0.2; slow 0.97; clamp tiny velocities to 0; rotate by vel.X.
  - Tile collide: bouncy; return false.
  - PrepareBombToBlow: tileCollide=false; alpha=255; Resize(128,128); knockBack=8.
  - OnKill: sound Item14; Resize(22,22); smoke/fire dust & smoke gore.

- ExampleRocketProjectile.cs
  - Sets: IsARocketThatDealsDoubleDamageToPrimaryEnemy=true; PlayerHurtDamageIgnoresDifficultyScaling=true; Explosive=true; (optional RocketsSkipDamageForPlayers commented).
  - Defaults: friendly; penetrate=-1; Ranged.
  - AI: if timeLeft<=3 → Prepare; else emit fire/smoke when speed >= ~8; accelerate by 10% while under ~15; rotate to velocity.
  - OnTileCollide: stop, set timeLeft=3, return false.
  - PrepareBombToBlow: tileCollide=false; alpha=255; Resize(128,128); knockBack=8.
  - OnKill: sound Item14; Resize(22,22); smoke/fire dust & smoke gore; commented Rocket II tile-damage sample using ShouldWallExplode/ExplodeTiles.

- ExampleSnowmanRocketProjectile.cs (homing, rocket snowman variant)
  - Sets: IsARocketThatDealsDoubleDamageToPrimaryEnemy=true; CultistIsResistantTo=true; RocketsSkipDamageForPlayers=true; Explosive=true.
  - Defaults: friendly; penetrate=-1; Ranged; scale=0.9f.
  - AI: if timeLeft<=3 → Prepare; else fade-in via localAI[1] (opaque after >6 ticks); start dust after >9 ticks; homing after >15 ticks toward nearest LoS NPC within ~600px else lead forward; speed=16, velocity lerp toward desired; keep spriteDirection/rotation consistent with face upright.
  - OnTileCollide: stop, timeLeft=3, return false.
  - PrepareBombToBlow & OnKill: same pattern as rockets; dust/gore.

## Patterns observed
- Explosives: Use ProjectileID.Sets.Explosive + PrepareBombToBlow → Resize/alpha/tileCollide false; OnTileCollide either bounce (grenade/mine) or arm (rocket) via timeLeft=3.
- Visuals: consistent smoke/fire dust and smoke gore spawning; rotation matches movement.
- Homing: simple scan with Collision.CanHit and distance tracking; lerp to target velocity.
- Minion: split AI into small helpers; target selection integrates HasMinionAttackTargetNPC; anti-overlap; friendly toggled by target presence; netUpdate when teleporting.
