using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class allows you to modify and use hooks for all projectiles, both vanilla and modded.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// </summary>
	// Token: 0x0200016F RID: 367
	public abstract class GlobalProjectile : GlobalType<Projectile, GlobalProjectile>
	{
		// Token: 0x06001D6A RID: 7530 RVA: 0x004D423C File Offset: 0x004D243C
		protected override void ValidateType()
		{
			base.ValidateType();
			LoaderUtils.MustOverrideTogether<GlobalProjectile>(this, new Expression<Func<GlobalProjectile, Delegate>>[]
			{
				(GlobalProjectile g) => (Action<Projectile, BitWriter, BinaryWriter>)methodof(GlobalProjectile.SendExtraAI(Projectile, BitWriter, BinaryWriter)).CreateDelegate(typeof(Action<Projectile, BitWriter, BinaryWriter>), g),
				(GlobalProjectile g) => (Action<Projectile, BitReader, BinaryReader>)methodof(GlobalProjectile.ReceiveExtraAI(Projectile, BitReader, BinaryReader)).CreateDelegate(typeof(Action<Projectile, BitReader, BinaryReader>), g)
			});
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x004D4377 File Offset: 0x004D2577
		protected sealed override void Register()
		{
			base.Register();
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x004D437F File Offset: 0x004D257F
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		/// <summary>
		/// Gets called when any projectiles spawns in world
		/// <para /> Called on the client or server spawning the projectile via Projectile.NewProjectile.
		/// </summary>
		// Token: 0x06001D6D RID: 7533 RVA: 0x004D4387 File Offset: 0x004D2587
		public virtual void OnSpawn(Projectile projectile, IEntitySource source)
		{
		}

		/// <summary>
		/// Allows you to determine how any projectile behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
		/// <AIMethodOrder>
		/// 		<para /> The order of the AI related methods is: <code>
		/// if (PreAI()) {
		/// 	VanillaAI()
		/// 	AI()
		/// }
		/// PostAI()</code>
		/// 	</AIMethodOrder>
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile" />
		/// <returns />
		// Token: 0x06001D6E RID: 7534 RVA: 0x004D4389 File Offset: 0x004D2589
		public virtual bool PreAI(Projectile projectile)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how any projectile behaves. This will only be called if PreAI returns true.
		/// <AIMethodOrder>
		/// 		<para /> The order of the AI related methods is: <code>
		/// if (PreAI()) {
		/// 	VanillaAI()
		/// 	AI()
		/// }
		/// PostAI()</code>
		/// 	</AIMethodOrder>
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile" />
		// Token: 0x06001D6F RID: 7535 RVA: 0x004D438C File Offset: 0x004D258C
		public virtual void AI(Projectile projectile)
		{
		}

		/// <summary>
		/// Allows you to determine how any projectile behaves. This will be called regardless of what PreAI returns.
		/// <AIMethodOrder>
		/// 		<para /> The order of the AI related methods is: <code>
		/// if (PreAI()) {
		/// 	VanillaAI()
		/// 	AI()
		/// }
		/// PostAI()</code>
		/// 	</AIMethodOrder>
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile" />
		// Token: 0x06001D70 RID: 7536 RVA: 0x004D438E File Offset: 0x004D258E
		public virtual void PostAI(Projectile projectile)
		{
		}

		/// <summary>
		/// Use this judiciously to avoid straining the network.
		/// <para /> Checks and methods such as <see cref="M:Terraria.ModLoader.GlobalType`2.AppliesToEntity(`0,System.Boolean)" /> can reduce how much data must be sent for how many projectiles.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncProjectile" /> is successfully sent, for example on projectile creation, or whenever Projectile.netUpdate is set to true in the update loop for that tick.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile">The projectile.</param>
		/// <param name="bitWriter">The compressible bit writer. Booleans written via this are compressed across all mods to improve multiplayer performance.</param>
		/// <param name="binaryWriter">The writer.</param>
		// Token: 0x06001D71 RID: 7537 RVA: 0x004D4390 File Offset: 0x004D2590
		public virtual void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
		{
		}

		/// <summary>
		/// Use this to receive information that was sent in <see cref="M:Terraria.ModLoader.GlobalProjectile.SendExtraAI(Terraria.Projectile,Terraria.ModLoader.IO.BitWriter,System.IO.BinaryWriter)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncProjectile" /> is successfully received.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile">The projectile.</param>
		/// <param name="bitReader">The compressible bit reader.</param>
		/// <param name="binaryReader">The reader.</param>
		// Token: 0x06001D72 RID: 7538 RVA: 0x004D4392 File Offset: 0x004D2592
		public virtual void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
		{
		}

		/// <summary>
		/// Whether or not the given projectile should update its position based on factors such as its velocity, whether it is in liquid, etc. Return false to make its velocity have no effect on its position. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <returns></returns>
		// Token: 0x06001D73 RID: 7539 RVA: 0x004D4394 File Offset: 0x004D2594
		public virtual bool ShouldUpdatePosition(Projectile projectile)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how a projectile interacts with tiles. Return false if you completely override or cancel a projectile's tile collision behavior. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"> The projectile. </param>
		/// <param name="width"> The width of the hitbox the projectile will use for tile collision. If vanilla or a mod don't modify it, defaults to projectile.width. </param>
		/// <param name="height"> The height of the hitbox the projectile will use for tile collision. If vanilla or a mod don't modify it, defaults to projectile.height. </param>
		/// <param name="fallThrough"> Whether or not the projectile falls through platforms and similar tiles. </param>
		/// <param name="hitboxCenterFrac"> Determines by how much the tile collision hitbox's position (top left corner) will be offset from the projectile's real center. If vanilla or a mod don't modify it, defaults to half the hitbox size (new Vector2(0.5f, 0.5f)). </param>
		/// <returns></returns>
		// Token: 0x06001D74 RID: 7540 RVA: 0x004D4397 File Offset: 0x004D2597
		public virtual bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when a projectile collides with a tile. OldVelocity is the velocity before tile collision. The velocity that takes tile collision into account can be found with projectile.velocity. Return true to allow the vanilla tile collision code to take place (which normally kills the projectile). Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="oldVelocity"></param>
		/// <returns></returns>
		// Token: 0x06001D75 RID: 7541 RVA: 0x004D439A File Offset: 0x004D259A
		public virtual bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether the vanilla code for Kill and the Kill hook will be called. Return false to stop them from being called. Returns true by default. Note that this does not stop the projectile from dying.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="timeLeft"></param>
		/// <returns></returns>
		// Token: 0x06001D76 RID: 7542 RVA: 0x004D439D File Offset: 0x004D259D
		public virtual bool PreKill(Projectile projectile, int timeLeft)
		{
			return true;
		}

		/// <summary>
		/// Allows you to control what happens when a projectile is killed (for example, creating dust or making sounds).
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="timeLeft"></param>
		// Token: 0x06001D77 RID: 7543 RVA: 0x004D43A0 File Offset: 0x004D25A0
		public virtual void OnKill(Projectile projectile, int timeLeft)
		{
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x004D43A2 File Offset: 0x004D25A2
		[Obsolete("Renamed to OnKill", true)]
		public virtual void Kill(Projectile projectile, int timeLeft)
		{
		}

		/// <summary>
		/// Return true or false to specify if the projectile can cut tiles like vines, pots, and Queen Bee larva. Return null for vanilla decision.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <returns></returns>
		// Token: 0x06001D79 RID: 7545 RVA: 0x004D43A4 File Offset: 0x004D25A4
		public virtual bool? CanCutTiles(Projectile projectile)
		{
			return null;
		}

		/// <summary>
		/// Code ran when the projectile cuts tiles. Only runs if CanCutTiles() returns true. Useful when programming lasers and such.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		// Token: 0x06001D7A RID: 7546 RVA: 0x004D43BA File Offset: 0x004D25BA
		public virtual void CutTiles(Projectile projectile)
		{
		}

		/// <summary>
		/// Whether or not the given projectile is capable of killing tiles (such as grass) and damaging NPCs/players.
		/// Return false to prevent it from doing any sort of damage.
		/// Return true if you want the projectile to do damage regardless of the default blacklist.
		/// Return null to let the projectile follow vanilla can-damage-anything rules. This is what happens by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <returns></returns>
		// Token: 0x06001D7B RID: 7547 RVA: 0x004D43BC File Offset: 0x004D25BC
		public virtual bool? CanDamage(Projectile projectile)
		{
			return null;
		}

		/// <summary>
		/// Whether or not a minion can damage NPCs by touching them. Returns false by default. Note that this will only be used if the projectile is considered a pet.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <returns></returns>
		// Token: 0x06001D7C RID: 7548 RVA: 0x004D43D2 File Offset: 0x004D25D2
		public virtual bool MinionContactDamage(Projectile projectile)
		{
			return false;
		}

		/// <summary>
		/// Allows you to change the hitbox used by a projectile for damaging players and NPCs.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="hitbox"></param>
		// Token: 0x06001D7D RID: 7549 RVA: 0x004D43D5 File Offset: 0x004D25D5
		public virtual void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
		{
		}

		/// <summary>
		/// Allows you to determine whether a projectile can hit the given NPC. Return true to allow hitting the target, return false to block the projectile from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x06001D7E RID: 7550 RVA: 0x004D43D8 File Offset: 0x004D25D8
		public virtual bool? CanHitNPC(Projectile projectile, NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that a projectile does to an NPC.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D7F RID: 7551 RVA: 0x004D43EE File Offset: 0x004D25EE
		public virtual void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when a projectile hits an NPC (for example, inflicting debuffs).
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <param name="hit"></param>
		/// <param name="damageDone"></param>
		// Token: 0x06001D80 RID: 7552 RVA: 0x004D43F0 File Offset: 0x004D25F0
		public virtual void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether a projectile can hit the given opponent player. Return false to block the projectile from hitting the target. Returns true by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x06001D81 RID: 7553 RVA: 0x004D43F2 File Offset: 0x004D25F2
		public virtual bool CanHitPvp(Projectile projectile, Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether a hostile projectile can hit the given player. Return false to block the projectile from hitting the target. Returns true by default.
		/// <para /> Called on the server only.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		// Token: 0x06001D82 RID: 7554 RVA: 0x004D43F5 File Offset: 0x004D25F5
		public virtual bool CanHitPlayer(Projectile projectile, Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that a hostile projectile does to a player.
		/// <para /> Called on the client taking damage.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <param name="modifiers"></param>
		// Token: 0x06001D83 RID: 7555 RVA: 0x004D43F8 File Offset: 0x004D25F8
		public virtual void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when a hostile projectile hits a player. <br />
		/// <para /> Called on the client taking damage.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="target"></param>
		/// <param name="info"></param>
		// Token: 0x06001D84 RID: 7556 RVA: 0x004D43FA File Offset: 0x004D25FA
		public virtual void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
		{
		}

		/// <summary>
		/// Allows you to use custom collision detection between a projectile and a player or NPC that the projectile can damage. Useful for things like diagonal lasers, projectiles that leave a trail behind them, etc.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="projHitbox"></param>
		/// <param name="targetHitbox"></param>
		/// <returns></returns>
		// Token: 0x06001D85 RID: 7557 RVA: 0x004D43FC File Offset: 0x004D25FC
		public virtual bool? Colliding(Projectile projectile, Rectangle projHitbox, Rectangle targetHitbox)
		{
			return null;
		}

		/// <summary>
		/// Allows you to determine the color and transparency in which a projectile is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="lightColor"></param>
		/// <returns></returns>
		// Token: 0x06001D86 RID: 7558 RVA: 0x004D4414 File Offset: 0x004D2614
		public virtual Color? GetAlpha(Projectile projectile, Color lightColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to draw things behind a projectile. Use the <c>Main.EntitySpriteDraw</c> method for drawing. Returns false to stop the game from drawing extras textures related to the projectile (for example, the chains for grappling hooks), useful if you're manually drawing the extras. Returns true by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="projectile"> The projectile. </param>
		// Token: 0x06001D87 RID: 7559 RVA: 0x004D442A File Offset: 0x004D262A
		public virtual bool PreDrawExtras(Projectile projectile)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind a projectile, or to modify the way the projectile is drawn. Use the <c>Main.EntitySpriteDraw</c> method for drawing. Return false to stop the vanilla projectile drawing code (useful if you're manually drawing the projectile). Returns true by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="projectile"> The projectile. </param>
		/// <param name="lightColor"> The color of the light at the projectile's center. </param>
		// Token: 0x06001D88 RID: 7560 RVA: 0x004D442D File Offset: 0x004D262D
		public virtual bool PreDraw(Projectile projectile, ref Color lightColor)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of a projectile. Use the <c>Main.EntitySpriteDraw</c> method for drawing. This method is called even if PreDraw returns false.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="projectile"> The projectile. </param>
		/// <param name="lightColor"> The color of the light at the projectile's center, after being modified by vanilla and other mods. </param>
		// Token: 0x06001D89 RID: 7561 RVA: 0x004D4430 File Offset: 0x004D2630
		public virtual void PostDraw(Projectile projectile, Color lightColor)
		{
		}

		/// <summary>
		/// When used in conjunction with "projectile.hide = true", allows you to specify that this projectile should be drawn behind certain elements. Add the index to one and only one of the lists. For example, the Nebula Arcanum projectile draws behind NPCs and tiles.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="index"></param>
		/// <param name="behindNPCsAndTiles"></param>
		/// <param name="behindNPCs"></param>
		/// <param name="behindProjectiles"></param>
		/// <param name="overPlayers"></param>
		/// <param name="overWiresUI"></param>
		// Token: 0x06001D8A RID: 7562 RVA: 0x004D4432 File Offset: 0x004D2632
		public virtual void DrawBehind(Projectile projectile, int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
		}

		/// <summary>
		/// Whether or not a grappling hook that shoots this type of projectile can be used by the given player. Return null to use the default code (whether or not the player is in the middle of firing the grappling hook). Returns null by default.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06001D8B RID: 7563 RVA: 0x004D4434 File Offset: 0x004D2634
		public virtual bool? CanUseGrapple(int type, Player player)
		{
			return null;
		}

		/// <summary>
		/// This code is called whenever the player uses a grappling hook that shoots this type of projectile. Use it to change what kind of hook is fired (for example, the Dual Hook does this), to kill old hook projectiles, etc.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06001D8C RID: 7564 RVA: 0x004D444A File Offset: 0x004D264A
		public virtual void UseGrapple(Player player, ref int type)
		{
		}

		/// <summary>
		/// How many of this type of grappling hook the given player can latch onto blocks before the hooks start disappearing. Change the numHooks parameter to determine this; by default it will be 3.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06001D8D RID: 7565 RVA: 0x004D444C File Offset: 0x004D264C
		public virtual void NumGrappleHooks(Projectile projectile, Player player, ref int numHooks)
		{
		}

		/// <summary>
		/// The speed at which the grapple retreats back to the player after not hitting anything. Defaults to 11, but vanilla hooks go up to 24.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06001D8E RID: 7566 RVA: 0x004D444E File Offset: 0x004D264E
		public virtual void GrappleRetreatSpeed(Projectile projectile, Player player, ref float speed)
		{
		}

		/// <summary>
		/// The speed at which the grapple pulls the player after hitting something. Defaults to 11, but the Bat Hook uses 16.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06001D8F RID: 7567 RVA: 0x004D4450 File Offset: 0x004D2650
		public virtual void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
		{
		}

		/// <summary>
		/// The location that the grappling hook pulls the player to. Defaults to the center of the hook projectile.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06001D90 RID: 7568 RVA: 0x004D4452 File Offset: 0x004D2652
		public virtual void GrappleTargetPoint(Projectile projectile, Player player, ref float grappleX, ref float grappleY)
		{
		}

		/// <summary>
		/// Whether or not the grappling hook can latch onto the given position in tile coordinates.
		/// <para /> This position may be air or an actuated tile!
		/// <para /> Return true to make it latch, false to prevent it, or null to apply vanilla conditions. Returns null by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06001D91 RID: 7569 RVA: 0x004D4454 File Offset: 0x004D2654
		public virtual bool? GrappleCanLatchOnTo(Projectile projectile, Player player, int x, int y)
		{
			return null;
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModProjectile.PrepareBombToBlow" />
		// Token: 0x06001D92 RID: 7570 RVA: 0x004D446A File Offset: 0x004D266A
		public virtual void PrepareBombToBlow(Projectile projectile)
		{
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ModProjectile.EmitEnchantmentVisualsAt(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)" />
		// Token: 0x06001D93 RID: 7571 RVA: 0x004D446C File Offset: 0x004D266C
		public virtual void EmitEnchantmentVisualsAt(Projectile projectile, Vector2 boxPosition, int boxWidth, int boxHeight)
		{
		}
	}
}
