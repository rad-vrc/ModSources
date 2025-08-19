using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This class serves as a place for you to place all your properties and hooks for each projectile.
	/// <br /> To use it, simply create a new class deriving from this one. Implementations will be registered automatically.
	/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile">Basic Projectile Guide</see> teaches the basics of making a modded projectile.
	/// </summary>
	// Token: 0x020001C4 RID: 452
	public abstract class ModProjectile : ModType<Projectile, ModProjectile>, ILocalizedModType, IModType
	{
		/// <summary> The projectile object that this ModProjectile controls. </summary>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x004E85DC File Offset: 0x004E67DC
		public Projectile Projectile
		{
			get
			{
				return base.Entity;
			}
		}

		/// <summary>  Shorthand for Projectile.type; </summary>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x004E85E4 File Offset: 0x004E67E4
		public int Type
		{
			get
			{
				return this.Projectile.type;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x004E85F1 File Offset: 0x004E67F1
		public virtual string LocalizationCategory
		{
			get
			{
				return "Projectiles";
			}
		}

		/// <summary> The translations for the display name of this projectile. </summary>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x004E85F8 File Offset: 0x004E67F8
		public virtual LocalizedText DisplayName
		{
			get
			{
				return this.GetLocalization("DisplayName", new Func<string>(base.PrettyPrintName));
			}
		}

		/// <summary> Determines which type of vanilla projectile this ModProjectile will copy the behavior (AI) of. Leave as 0 to not copy any behavior. Defaults to 0. </summary>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x004E8611 File Offset: 0x004E6811
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x004E8619 File Offset: 0x004E6819
		public int AIType { get; set; }

		/// <summary> Determines which <see cref="T:Terraria.ID.ImmunityCooldownID" /> to use when this projectile damages a player. Defaults to -1 (<see cref="F:Terraria.ID.ImmunityCooldownID.General" />). </summary>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x004E8622 File Offset: 0x004E6822
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x004E862A File Offset: 0x004E682A
		public int CooldownSlot { get; set; } = -1;

		/// <summary> How far to the right of its position this projectile should be drawn. Defaults to 0. </summary>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x004E8633 File Offset: 0x004E6833
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x004E863B File Offset: 0x004E683B
		public int DrawOffsetX { get; set; }

		/// <summary> The vertical origin offset from the projectile's center when it is drawn. The origin is essentially the point of rotation. This field will also determine the vertical drawing offset of the projectile. </summary>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x004E8644 File Offset: 0x004E6844
		// (set) Token: 0x06002363 RID: 9059 RVA: 0x004E864C File Offset: 0x004E684C
		public int DrawOriginOffsetY { get; set; }

		/// <summary> The horizontal origin offset from the projectile's center when it is drawn. </summary>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x004E8655 File Offset: 0x004E6855
		// (set) Token: 0x06002365 RID: 9061 RVA: 0x004E865D File Offset: 0x004E685D
		public float DrawOriginOffsetX { get; set; }

		/// <summary> If this projectile is held by the player, determines whether it is drawn in front of or behind the player's arms. Defaults to false. </summary>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x004E8666 File Offset: 0x004E6866
		// (set) Token: 0x06002367 RID: 9063 RVA: 0x004E866E File Offset: 0x004E686E
		public bool DrawHeldProjInFrontOfHeldItemAndArms { get; set; }

		/// <summary>
		/// The file name of this type's texture file in the mod loader's file space. <br />
		/// The resulting  Asset&lt;Texture2D&gt; can be retrieved using <see cref="F:Terraria.GameContent.TextureAssets.Projectile" /> indexed by <see cref="P:Terraria.ModLoader.ModProjectile.Type" /> if needed. <br />
		/// You can use a vanilla texture by returning <c>$"Terraria/Images/Projectile_{ProjectileID.ProjectileNameHere}"</c> <br />
		/// </summary>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x004E8677 File Offset: 0x004E6877
		public virtual string Texture
		{
			get
			{
				return (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
			}
		}

		/// <summary> The file name of this projectile's glow texture file in the mod loader's file space. If it does not exist it is ignored. </summary>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x004E869D File Offset: 0x004E689D
		public virtual string GlowTexture
		{
			get
			{
				return this.Texture + "_Glow";
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x004E86AF File Offset: 0x004E68AF
		protected override Projectile CreateTemplateEntity()
		{
			return new Projectile
			{
				ModProjectile = this
			};
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x004E86BD File Offset: 0x004E68BD
		protected sealed override void Register()
		{
			ModTypeLookup<ModProjectile>.Register(this);
			this.Projectile.type = ProjectileLoader.Register(this);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x004E86D6 File Offset: 0x004E68D6
		public sealed override void SetupContent()
		{
			ProjectileLoader.SetDefaults(this.Projectile, false);
			this.AutoStaticDefaults();
			this.SetStaticDefaults();
			ProjectileID.Search.Add(base.FullName, this.Type);
		}

		/// <summary>
		/// Allows you to set all your projectile's properties, such as width, damage, aiStyle, penetrate, etc.
		/// </summary>
		// Token: 0x0600236D RID: 9069 RVA: 0x004E8706 File Offset: 0x004E6906
		public virtual void SetDefaults()
		{
		}

		/// <summary>
		/// Gets called when your projectiles spawns in world.
		/// <para /> Called on the client or server spawning the projectile via Projectile.NewProjectile.
		/// </summary>
		// Token: 0x0600236E RID: 9070 RVA: 0x004E8708 File Offset: 0x004E6908
		public virtual void OnSpawn(IEntitySource source)
		{
		}

		/// <summary>
		/// Automatically sets certain static defaults. Override this if you do not want the properties to be set for you.
		/// </summary>
		// Token: 0x0600236F RID: 9071 RVA: 0x004E870C File Offset: 0x004E690C
		public virtual void AutoStaticDefaults()
		{
			TextureAssets.Projectile[this.Projectile.type] = ModContent.Request<Texture2D>(this.Texture, 2);
			Main.projFrames[this.Projectile.type] = 1;
			if (this.Projectile.hostile)
			{
				Main.projHostile[this.Projectile.type] = true;
			}
			if (this.Projectile.aiStyle == 7)
			{
				Main.projHook[this.Projectile.type] = true;
			}
		}

		/// <summary>
		/// Allows you to determine how this projectile behaves. Return false to stop the vanilla AI and the AI hook from being run. Returns true by default.
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
		/// <returns>Whether or not to stop other AI.</returns>
		// Token: 0x06002370 RID: 9072 RVA: 0x004E8787 File Offset: 0x004E6987
		public virtual bool PreAI()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how this projectile behaves. This will only be called if PreAI returns true.
		/// <para /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#custom-ai">Basic Projectile Guide</see> teaches the basics of writing a custom AI, such as timers, gravity, rotation, etc.
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
		// Token: 0x06002371 RID: 9073 RVA: 0x004E878A File Offset: 0x004E698A
		public virtual void AI()
		{
		}

		/// <summary>
		/// Allows you to determine how this projectile behaves. This will be called regardless of what PreAI returns.
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
		// Token: 0x06002372 RID: 9074 RVA: 0x004E878C File Offset: 0x004E698C
		public virtual void PostAI()
		{
		}

		/// <summary>
		/// If you are storing AI information outside of the Projectile.ai array, use this to send that AI information between clients and servers, which will be handled in <see cref="M:Terraria.ModLoader.ModProjectile.ReceiveExtraAI(System.IO.BinaryReader)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncProjectile" /> is successfully sent, for example on projectile creation, or whenever Projectile.netUpdate is set to true in the update loop for that tick.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="writer">The writer.</param>
		// Token: 0x06002373 RID: 9075 RVA: 0x004E878E File Offset: 0x004E698E
		public virtual void SendExtraAI(BinaryWriter writer)
		{
		}

		/// <summary>
		/// Use this to receive information that was sent in <see cref="M:Terraria.ModLoader.ModProjectile.SendExtraAI(System.IO.BinaryWriter)" />.
		/// <para /> Called whenever <see cref="F:Terraria.ID.MessageID.SyncProjectile" /> is successfully received.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="reader">The reader.</param>
		// Token: 0x06002374 RID: 9076 RVA: 0x004E8790 File Offset: 0x004E6990
		public virtual void ReceiveExtraAI(BinaryReader reader)
		{
		}

		/// <summary>
		/// Whether or not this projectile should update its position based on factors such as its velocity, whether it is in liquid, etc. Return false to make its velocity have no effect on its position. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002375 RID: 9077 RVA: 0x004E8792 File Offset: 0x004E6992
		public virtual bool ShouldUpdatePosition()
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine how this projectile interacts with tiles. Return false if you completely override or cancel this projectile's tile collision behavior. Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="width"> The width of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.width. </param>
		/// <param name="height"> The height of the hitbox this projectile will use for tile collision. If vanilla doesn't modify it, defaults to Projectile.height. </param>
		/// <param name="fallThrough"> Whether or not this projectile falls through platforms and similar tiles. </param>
		/// <param name="hitboxCenterFrac"> Determines by how much the tile collision hitbox's position (top left corner) will be offset from this projectile's real center. If vanilla doesn't modify it, defaults to half the hitbox size (new Vector2(0.5f, 0.5f)). </param>
		/// <returns></returns>
		// Token: 0x06002376 RID: 9078 RVA: 0x004E8795 File Offset: 0x004E6995
		public virtual bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine what happens when this projectile collides with a tile. OldVelocity is the velocity before tile collision. The velocity that takes tile collision into account can be found with Projectile.velocity. Return true to allow the vanilla tile collision code to take place (which normally kills the projectile). Returns true by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="oldVelocity">The velocity of the projectile upon collision.</param>
		// Token: 0x06002377 RID: 9079 RVA: 0x004E8798 File Offset: 0x004E6998
		public virtual bool OnTileCollide(Vector2 oldVelocity)
		{
			return true;
		}

		/// <summary>
		/// Return true or false to specify if the projectile can cut tiles like vines, pots, and Queen Bee larva. Return null for vanilla decision.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		// Token: 0x06002378 RID: 9080 RVA: 0x004E879C File Offset: 0x004E699C
		public virtual bool? CanCutTiles()
		{
			return null;
		}

		/// <summary>
		/// Code ran when the projectile cuts tiles. Only runs if CanCutTiles() returns true. Useful when programming lasers and such.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		// Token: 0x06002379 RID: 9081 RVA: 0x004E87B2 File Offset: 0x004E69B2
		public virtual void CutTiles()
		{
		}

		/// <summary>
		/// Allows you to determine whether the vanilla code for Kill and the Kill hook will be called. Return false to stop them from being called. Returns true by default. Note that this does not stop the projectile from dying.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x0600237A RID: 9082 RVA: 0x004E87B4 File Offset: 0x004E69B4
		public virtual bool PreKill(int timeLeft)
		{
			return true;
		}

		/// <summary>
		/// Allows you to control what happens when this projectile is killed (for example, creating dust or making sounds). Also useful for creating retrievable ammo.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		// Token: 0x0600237B RID: 9083 RVA: 0x004E87B7 File Offset: 0x004E69B7
		public virtual void OnKill(int timeLeft)
		{
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x004E87B9 File Offset: 0x004E69B9
		[Obsolete("Renamed to OnKill", true)]
		public virtual void Kill(int timeLeft)
		{
		}

		/// <summary>
		/// Whether or not this projectile is capable of killing tiles (such as grass) and damaging NPCs/players.
		/// Return false to prevent it from doing any sort of damage.
		/// Return true if you want the projectile to do damage regardless of the default blacklist.
		/// Return null to let the projectile follow vanilla can-damage-anything rules. This is what happens by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x0600237D RID: 9085 RVA: 0x004E87BC File Offset: 0x004E69BC
		public virtual bool? CanDamage()
		{
			return null;
		}

		/// <summary>
		/// Whether or not this minion can damage NPCs by touching them. Returns false by default. Note that this will only be used if this projectile is considered a pet (<see cref="F:Terraria.Main.projPet" />).
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x0600237E RID: 9086 RVA: 0x004E87D2 File Offset: 0x004E69D2
		public virtual bool MinionContactDamage()
		{
			return false;
		}

		/// <summary>
		/// Allows you to change the hitbox used by this projectile for damaging players and NPCs.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		/// <param name="hitbox"></param>
		// Token: 0x0600237F RID: 9087 RVA: 0x004E87D5 File Offset: 0x004E69D5
		public virtual void ModifyDamageHitbox(ref Rectangle hitbox)
		{
		}

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given NPC. Return true to allow hitting the target, return false to block this projectile from hitting the target, and return null to use the vanilla code for whether the target can be hit. Returns null by default.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="target">The target.</param>
		// Token: 0x06002380 RID: 9088 RVA: 0x004E87D8 File Offset: 0x004E69D8
		public virtual bool? CanHitNPC(NPC target)
		{
			return null;
		}

		/// <summary>
		/// Allows you to modify the damage, knockback, etc., that this projectile does to an NPC.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers">The modifiers for this strike.</param>
		// Token: 0x06002381 RID: 9089 RVA: 0x004E87EE File Offset: 0x004E69EE
		public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this projectile hits an NPC (for example, inflicting debuffs).
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="hit">The damage.</param>
		/// <param name="damageDone">The actual damage dealt to/taken by the NPC.</param>
		// Token: 0x06002382 RID: 9090 RVA: 0x004E87F0 File Offset: 0x004E69F0
		public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
		}

		/// <summary>
		/// Allows you to determine whether this projectile can hit the given opponent player. Return false to block this projectile from hitting the target. Returns true by default.
		/// <para /> Called on the client hitting the target.
		/// </summary>
		/// <param name="target">The target</param>
		// Token: 0x06002383 RID: 9091 RVA: 0x004E87F2 File Offset: 0x004E69F2
		public virtual bool CanHitPvp(Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to determine whether this hostile projectile can hit the given player. Return false to block this projectile from hitting the target. Returns true by default.
		/// <para /> Called on the server only.
		/// </summary>
		/// <param name="target">The target.</param>
		// Token: 0x06002384 RID: 9092 RVA: 0x004E87F5 File Offset: 0x004E69F5
		public virtual bool CanHitPlayer(Player target)
		{
			return true;
		}

		/// <summary>
		/// Allows you to modify the damage, etc., that this hostile projectile does to a player.
		/// <para /> Called on the client taking damage.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="modifiers"></param>
		// Token: 0x06002385 RID: 9093 RVA: 0x004E87F8 File Offset: 0x004E69F8
		public virtual void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
		{
		}

		/// <summary>
		/// Allows you to create special effects when this hostile projectile hits a player.
		/// <para /> Called on the client taking damage.
		/// </summary>
		/// <param name="target">The target.</param>
		/// <param name="info"></param>
		// Token: 0x06002386 RID: 9094 RVA: 0x004E87FA File Offset: 0x004E69FA
		public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
		{
		}

		/// <summary>
		/// Allows you to use custom collision detection between this projectile and a player or NPC that this projectile can damage. Useful for things like diagonal lasers, projectiles that leave a trail behind them, etc.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		/// <param name="projHitbox">The hitbox of the projectile.</param>
		/// <param name="targetHitbox">The hitbox of the target.</param>
		/// <returns>Whether they collide or not.</returns>
		// Token: 0x06002387 RID: 9095 RVA: 0x004E87FC File Offset: 0x004E69FC
		public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return null;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x004E8812 File Offset: 0x004E6A12
		[Obsolete("Moved to ModItem. Fishing line position and color are now set by the pole used.", true)]
		public virtual void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)
		{
		}

		/// <summary>
		/// Allows you to determine the color and transparency in which this projectile is drawn. Return null to use the default color (normally light and buff color). Returns null by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		// Token: 0x06002389 RID: 9097 RVA: 0x004E8814 File Offset: 0x004E6A14
		public virtual Color? GetAlpha(Color lightColor)
		{
			return null;
		}

		/// <summary>
		/// Allows you to draw things behind this projectile. Use the <c>Main.EntitySpriteDraw</c> method for drawing. Returns false to stop the game from drawing extras textures related to the projectile (for example, the chains for grappling hooks), useful if you're manually drawing the extras. Returns true by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		// Token: 0x0600238A RID: 9098 RVA: 0x004E882A File Offset: 0x004E6A2A
		public virtual bool PreDrawExtras()
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things behind this projectile, or to modify the way it is drawn. Use the <c>Main.EntitySpriteDraw</c> method for drawing. Return false to stop the vanilla projectile drawing code (useful if you're manually drawing the projectile). Returns true by default.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center. </param>
		// Token: 0x0600238B RID: 9099 RVA: 0x004E882D File Offset: 0x004E6A2D
		public virtual bool PreDraw(ref Color lightColor)
		{
			return true;
		}

		/// <summary>
		/// Allows you to draw things in front of this projectile. Use the <c>Main.EntitySpriteDraw</c> method for drawing. This method is called even if PreDraw returns false.
		/// <para /> Called on local and remote clients.
		/// </summary>
		/// <param name="lightColor"> The color of the light at the projectile's center, after being modified by vanilla and other mods. </param>
		// Token: 0x0600238C RID: 9100 RVA: 0x004E8830 File Offset: 0x004E6A30
		public virtual void PostDraw(Color lightColor)
		{
		}

		/// <summary>
		/// This code is called whenever the player uses a grappling hook that shoots this type of projectile. Use it to change what kind of hook is fired (for example, the Dual Hook does this), to kill old hook projectiles, etc.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x0600238D RID: 9101 RVA: 0x004E8834 File Offset: 0x004E6A34
		public virtual bool? CanUseGrapple(Player player)
		{
			return null;
		}

		/// <summary>
		/// This code is called whenever the player uses a grappling hook that shoots this type of projectile. Use it to change what kind of hook is fired (for example, the Dual Hook does this), to kill old hook projectiles, etc.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x0600238E RID: 9102 RVA: 0x004E884A File Offset: 0x004E6A4A
		public virtual void UseGrapple(Player player, ref int type)
		{
		}

		/// <summary>
		/// How far away this grappling hook can travel away from its player before it retracts.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x0600238F RID: 9103 RVA: 0x004E884C File Offset: 0x004E6A4C
		public virtual float GrappleRange()
		{
			return 300f;
		}

		/// <summary>
		/// How many of this type of grappling hook the given player can latch onto blocks before the hooks start disappearing. Change the numHooks parameter to determine this; by default it will be 3.
		/// <para /> Called on the local client only.
		/// </summary>
		// Token: 0x06002390 RID: 9104 RVA: 0x004E8853 File Offset: 0x004E6A53
		public virtual void NumGrappleHooks(Player player, ref int numHooks)
		{
		}

		/// <summary>
		/// The speed at which the grapple retreats back to the player after not hitting anything. Defaults to 11, but vanilla hooks go up to 24.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002391 RID: 9105 RVA: 0x004E8855 File Offset: 0x004E6A55
		public virtual void GrappleRetreatSpeed(Player player, ref float speed)
		{
		}

		/// <summary>
		/// The speed at which the grapple pulls the player after hitting something. Defaults to 11, but the Bat Hook uses 16.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002392 RID: 9106 RVA: 0x004E8857 File Offset: 0x004E6A57
		public virtual void GrapplePullSpeed(Player player, ref float speed)
		{
		}

		/// <summary>
		/// The location that the grappling hook pulls the player to. Defaults to the center of the hook projectile.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002393 RID: 9107 RVA: 0x004E8859 File Offset: 0x004E6A59
		public virtual void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
		{
		}

		/// <summary>
		/// Whether or not the grappling hook can latch onto the given position in tile coordinates.
		/// <para />This position may be air or an actuated tile!
		/// <para />Return true to make it latch, false to prevent it, or null to apply vanilla conditions. Returns null by default.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002394 RID: 9108 RVA: 0x004E885C File Offset: 0x004E6A5C
		public virtual bool? GrappleCanLatchOnTo(Player player, int x, int y)
		{
			return null;
		}

		/// <summary>
		/// When used in conjunction with <c>Projectile.hide = true</c> (<see cref="F:Terraria.Projectile.hide" />), allows you to specify that this projectile should be drawn behind certain elements. Add the index to one and only one of the lists. For example, the Nebula Arcanum projectile draws behind NPCs and tiles.
		/// <para /> Called on local and remote clients.
		/// </summary>
		// Token: 0x06002395 RID: 9109 RVA: 0x004E8872 File Offset: 0x004E6A72
		public virtual void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
		}

		/// <summary>
		/// Used to adjust projectile properties immediately before the projectile becomes an explosion. This is called on projectiles using the <see cref="F:Terraria.ID.ProjAIStyleID.Explosive" /> aiStyle or projectiles that are contained in the <see cref="F:Terraria.ID.ProjectileID.Sets.Explosive" /> set. By defaults tileCollide is set to false and alpha is set to 255. Use this to adjust damage, knockBack, and the projectile hitbox (Projectile.Resize).
		/// <para /> Called during Projectile.PrepareBombToBlow, which is called by default during Projectile.AI_016 and during Projectile.Kill for the aforementioned projectiles.
		/// <para /> Can be called on the local client or server, depending on who owns the projectile.
		/// </summary>
		// Token: 0x06002396 RID: 9110 RVA: 0x004E8874 File Offset: 0x004E6A74
		public virtual void PrepareBombToBlow()
		{
		}

		/// <summary>
		/// Called when <see cref="M:Terraria.Projectile.EmitEnchantmentVisualsAt(Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)" /> is called. Typically used to spawn visual effects (Dust) indicating weapon enchantments such as flasks, frost armor, or magma stone effects. This is similar to how items spawn visual effects in <see cref="M:Terraria.ModLoader.CombinedHooks.MeleeEffects(Terraria.Player,Terraria.Item,Microsoft.Xna.Framework.Rectangle)" />, but for projectiles instead. A typical weapon enchantment would likely include similar code in both to support weapon enchantment visuals for both items and projectiles.
		/// <para /> Projectiles can use <see cref="F:Terraria.Projectile.noEnchantments" /> to indicate that a projectile should not be considered for enchantment visuals, so check that field if relevant.
		/// <para /> Called on local, server, and remote clients.
		/// </summary>
		// Token: 0x06002397 RID: 9111 RVA: 0x004E8876 File Offset: 0x004E6A76
		public virtual void EmitEnchantmentVisualsAt(Vector2 boxPosition, int boxWidth, int boxHeight)
		{
		}
	}
}
