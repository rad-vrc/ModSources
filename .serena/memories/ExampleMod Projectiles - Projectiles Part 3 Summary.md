Part 3/5 summaries:

1) ExampleGravityDebuffBullet.cs
- aiStyle=1, AIType=Bullet; trail cache and afterimage PreDraw
- OnKill plays bounce sound and tile dust; OnHitNPC applies ExampleGravityDebuff (600)

2) ExampleHomingProjectile.cs
- Simple homing with target stored in ai[0] (+1 offset); ai[1] DelayTimer for initial delay
- Finds closest NPC within 400 using squared distance; validates with CanBeChasedBy + line of sight
- Rotates velocity toward target angle by 3 degrees per tick; rotation set to velocity angle; sets CultistIsResistant

3) ExampleInstancedProjectile.cs
- aiStyle=1 Bullet clone with custom PreDraw creating a Dust trail in random HSL color assigned on spawn via OnSpawn; returns false to skip default draw

4) ExampleInteractableProjectile.cs
- Interactable projectile (like Money Trough): ProjectileID.Sets.IsInteractable=true; hide=true; timeLeft=3min; behindProjectiles drawing
- PostDraw draws highlight overlay based on TryInteracting(): 0 none, 1 faded, 2 selected; uses Smart Cursor and gamepad support; cursor item icon shown
- Replaces older same-type projectile for owner; initial deceleration then hover bobbing with ai[1]; faces owner when stopped
- Right-click interaction plays sound (placeholder)

5) ExampleJavelinProjectile.cs
- Custom stick-to-NPC logic: IsStickingToTarget (ai[0]), TargetWhoAmI (ai[1]), GravityDelayTimer (ai[2]), StickTimer (localAI[0])
- Normal flight with delayed gravity and dust; rotation aligned; OnHitNPC sets stick state, zero damage, applies ExampleJavelinDebuff (900), KillOldestJavelin cap=6
- StickyAI keeps position relative to NPC center, hit effect every 30 ticks; kill after 15s or invalid NPC
- Drop item chance in OnKill only for owner and synced; DrawBehind positions behind tiles/NPCs

6) ExampleJoustingLanceProjectile.cs
- Held spear-like (custom AI) that uses owner itemAnimation to extend/hold/retract; owner.SetDummyItemTime to channel hold
- Tip position by adding velocity * tipDist; rotation computed; alpha fade-in
- Dust spawn when moving sufficiently in lance direction; ModifyHitNPC scales knockback and damage with player velocity
- Custom Colliding via AABBvLine; manual PreDraw with frame, origin, flip
