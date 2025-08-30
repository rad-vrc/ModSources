Part 2/5 summaries for ExampleMod/Content/Projectiles:

1) ExampleCloneProjectile.cs
- CloneDefaults + AIType = Meowmere; penetrates +3
- OnKill spawns 4 Meowmere projectiles around with rotating velocity
- OnTileCollide plays Meowmere ricochet sound (Item57/58)

2) ExampleCustomSwingProjectile.cs
- Held melee with custom swing/spin using Projectile.ai/localAI state machine
- AttackType(Swing/Spin), AttackStage(Prepare/Execute/Unwind); timing scales with attack speed
- Syncs spriteDirection via SendExtraAI/ReceiveExtraAI
- Uses Player composite arms to pose and SetSwordPosition; line-based Colliding/CutTiles
- PreDraw draws item texture; CanDamage only after Prepare
- Plays sword sounds; resets local NPC immunity mid-spin

3) ExampleDrillProjectile.cs
- Held drill-like projectile (aiStyle=-1 custom). Hides and sets player.heldProj
- While channeling, sets holdout offset toward mouse via Projectile.velocity; netUpdate when changed
- Plays Item22 sound every 20 ticks; jiggle X; spawns Sparkle dust occasionally

4) ExampleExplosive.cs
- Bomb-like with PrepareBombToBlow and tile/wall destruction
- ProjectileID.Sets.Explosive=true; PlayerHurtDamageIgnoresDifficultyScaling
- Bouncy unless IsChild; impact sound with custom SoundStyle; delayed gravity and roll damping
- PrepareBombToBlow: tileCollide=false, alpha=255, Resize to 250, damage=250, KB=10
- OnKill: owner spawns 5 child explosives (IsChild=true); explosion VFX (smoke/torch dust, gore)
- Owner explodes tiles using ShouldWallExplode + ExplodeTiles with radius=7

5) ExampleFlailProjectile.cs
- Minimal Sunfury clone: aiStyle Flail + AIType Sunfury; usesLocalNPCImmunity/localNPCHitCooldown
- PreDrawExtras temporarily sets type=Sunfury so vanilla draws chain; PreDraw restores and renders afterimages during launch
- OnHit applies OnFire to NPC/Player; AI spawns a Grenade once when retract begins

6) ExampleGolfBallProjectile.cs
- aiStyle=149 golf ball; IsAGolfBall=true; trailing setup; netImportant; tileCollide handled by AI (false)
