Part 4/5 summaries:

1) ExampleLastPrismBeam.cs
- Beam child for Last Prism holdout; beams scale/spread/opacity based on host charge; localAI BeamLength synced via Send/ReceiveExtraAI
- Performs LaserScan hitscan with sample points, clamps start at player if obstructed; local NPC immunity 10
- Draws outer/inner beams with rainbow laser shader; casts light, ripples water; CutTiles along beam path; Colliding via AABBvLine

2) ExampleLastPrismHoldout.cs
- Clones LastPrism; NeedsUUID and HeldProjDoesNotUsePlayerGfxOffY; MaxCharge/DamageStart constants
- AI: Update aim with AimResponsiveness, manage mana consumption cadence from 15→5; FireBeams spawns NumBeams child beams with UUID
- PreDraw custom; keeps timeLeft=2 while channeling; updates damage each frame for Mana Sickness

3) ExamplePaperAirplaneProjectile.cs
- Custom ai (or aiStyle=159 if desired); uses wind Main.WindForVisuals and sine y modifier; loops when aligned with wind
- manualDirectionChange=true; flips via PreDraw spriteEffects FlipVertically; dies in liquid; drops item on Kill (synced)

4) ExamplePiercingProjectile.cs (+ item)
- Demonstrates NPC immunity patterns; default penetrate=-1; AIType=Bullet; comments explain penetrate/local immunity strategies; item shoots it

5) ExampleSandBallProjectile.cs (+ Falling/Gun)
- Abstract base sets sets for falling blocks; FallingProjectile clones EbonsandBallFalling (hostile); GunProjectile clones EbonsandBallGun with AIType set

6) ExampleShortswordProjectile.cs
- Custom short sword: manual position update along velocity with opacity fade in/out; line-based Colliding and CutTiles; SetVisualOffsets aligns sprite; ownerHitCheck + extraUpdates

7) ExampleSpearProjectile.cs
- CloneDefaults Spear; PreAI overrides: compute progress, position along holdout range min→max and back; rotation adjustments and dust; return false to skip vanilla AI

8) ExampleSwingingEnergySwordProjectile.cs (Excalibur clone)
- Custom AI for swing over ai[1] lifetime with ai[0] direction and ai[2] scale; arc dust/imbue visuals; cone collision with IntersectsCone; CutTiles along arc; elaborate PreDraw layered glow

9) ExampleWhipProjectile.cs
- DefaultToWhip with optional charge mechanic in PreAI: increase Segments & Range; OnHit applies debuff, sets MinionAttackTarget, halves damage; custom whip segment drawing + fishing line between points

10) ExampleWhipProjectileAdvanced.cs
- Manual whip with AI: positions along arm; Charge method mirrors PreAI behavior; whipcrack sound at tip mid-swing; dust along path; custom PreDraw per segment

11) ExampleYoyoProjectile.cs
- aiStyle Yoyo (99) with sets: lifetime 3.5s, max range 300, top speed 13; MeleeNoSpeed; PostAI emits dust

12) MinionBossEye.cs
- Hostile projectile with fade-in/out using localAI flags; plays spawn sound in AI; accelerates each tick; rotates to velocity; boss cooldown slot
