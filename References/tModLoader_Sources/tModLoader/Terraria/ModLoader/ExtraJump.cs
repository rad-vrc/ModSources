using System;
using System.Collections.Generic;

namespace Terraria.ModLoader
{
	/// <summary>
	/// <see cref="T:Terraria.ModLoader.ExtraJump" /> is a singleton, defining the properties and behaviour of midair extra jumps.<br />
	/// Fields defining the state of a jump per player are stored in <see cref="T:Terraria.DataStructures.ExtraJumpState" />
	/// </summary>
	// Token: 0x0200015F RID: 351
	public abstract class ExtraJump : ModType
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x004D276F File Offset: 0x004D096F
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x004D2776 File Offset: 0x004D0976
		public static ExtraJump Flipper { get; private set; } = new FlipperJump();

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x004D277E File Offset: 0x004D097E
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x004D2785 File Offset: 0x004D0985
		public static ExtraJump BasiliskMount { get; private set; } = new BasiliskMountJump();

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x004D278D File Offset: 0x004D098D
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x004D2794 File Offset: 0x004D0994
		public static ExtraJump GoatMount { get; private set; } = new GoatMountJump();

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x004D279C File Offset: 0x004D099C
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x004D27A3 File Offset: 0x004D09A3
		public static ExtraJump SantankMount { get; private set; } = new SantankMountJump();

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x004D27AB File Offset: 0x004D09AB
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x004D27B2 File Offset: 0x004D09B2
		public static ExtraJump UnicornMount { get; private set; } = new UnicornMountJump();

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x004D27BA File Offset: 0x004D09BA
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x004D27C1 File Offset: 0x004D09C1
		public static ExtraJump SandstormInABottle { get; private set; } = new SandstormInABottleJump();

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x004D27C9 File Offset: 0x004D09C9
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x004D27D0 File Offset: 0x004D09D0
		public static ExtraJump BlizzardInABottle { get; private set; } = new BlizzardInABottleJump();

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x004D27D8 File Offset: 0x004D09D8
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x004D27DF File Offset: 0x004D09DF
		public static ExtraJump FartInAJar { get; private set; } = new FartInAJarJump();

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x004D27E7 File Offset: 0x004D09E7
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x004D27EE File Offset: 0x004D09EE
		public static ExtraJump TsunamiInABottle { get; private set; } = new TsunamiInABottleJump();

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x004D27F6 File Offset: 0x004D09F6
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x004D27FD File Offset: 0x004D09FD
		public static ExtraJump CloudInABottle { get; private set; } = new CloudInABottleJump();

		/// <summary>
		/// The internal ID of this <see cref="T:Terraria.ModLoader.ExtraJump" />.
		/// </summary>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x004D2805 File Offset: 0x004D0A05
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x004D280D File Offset: 0x004D0A0D
		public int Type { get; internal set; }

		// Token: 0x06001C2B RID: 7211 RVA: 0x004D2816 File Offset: 0x004D0A16
		protected sealed override void Register()
		{
			ModTypeLookup<ExtraJump>.Register(this);
			this.Type = ExtraJumpLoader.Add(this);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x004D282A File Offset: 0x004D0A2A
		public sealed override void SetupContent()
		{
			this.SetStaticDefaults();
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x004D2832 File Offset: 0x004D0A32
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>
		/// Returns this extra jump's default position in regard to the vanilla extra jumps.  Make use of e.g. <see cref="T:Terraria.ModLoader.ExtraJump.Before" />/<see cref="T:Terraria.ModLoader.ExtraJump.After" />, and provide an extra jump.<br /><br />
		///
		/// Recommended using one of: <c>BeforeMountJumps, MountJumpPosition, BeforeBottleJumps, AfterBottleJumps</c><br /><br />
		///
		/// <b>NOTE:</b> The position must specify a vanilla <see cref="T:Terraria.ModLoader.ExtraJump" /> otherwise an exception will be thrown.
		/// </summary>
		// Token: 0x06001C2E RID: 7214
		public abstract ExtraJump.Position GetDefaultPosition();

		/// <summary>
		/// Modded jumps are placed between vanilla jumps via <see cref="M:Terraria.ModLoader.ExtraJump.GetDefaultPosition" /> and, by default, are sorted in load order.<br />
		/// This hook allows you to sort this jump before/after other modded jumps that were placed between the same two vanilla jumps.<br />
		/// Example:
		/// <para>
		/// <c>yield return new After(ModContent.GetInstance&lt;SimpleExtraJump&gt;());</c>
		/// </para>
		/// By default, this hook returns <see langword="null" />, which indicates that this jump has no modded ordering constraints.
		/// </summary>
		// Token: 0x06001C2F RID: 7215 RVA: 0x004D283A File Offset: 0x004D0A3A
		public virtual IEnumerable<ExtraJump.Position> GetModdedConstraints()
		{
			return null;
		}

		/// <summary>
		/// Spawn effects that should appear while the player is performing this jump here.<br />
		/// Only runs while the jump is <see cref="P:Terraria.DataStructures.ExtraJumpState.Active" /> <br />
		/// For example, the Sandstorm in a Bottle's dusts are spawned here.
		/// </summary>
		/// <param name="player">The player performing the jump</param>
		// Token: 0x06001C30 RID: 7216 RVA: 0x004D283D File Offset: 0x004D0A3D
		public virtual void ShowVisuals(Player player)
		{
		}

		/// <summary>
		/// Return <see langword="false" /> to prevent <see cref="M:Terraria.ModLoader.ExtraJump.ShowVisuals(Terraria.Player)" /> from executing.<br />
		/// By default, this hook returns whether the player is moving upwards with respect to <see cref="F:Terraria.Player.gravDir" />
		/// </summary>
		/// <param name="player">The player performing the jump</param>
		// Token: 0x06001C31 RID: 7217 RVA: 0x004D2840 File Offset: 0x004D0A40
		public virtual bool CanShowVisuals(Player player)
		{
			return (player.gravDir == 1f && player.velocity.Y < 0f) || (player.gravDir == -1f && player.velocity.Y > 0f);
		}

		/// <summary>
		/// Vanilla's jumps use the following values:
		/// <para>
		/// Basilisk mount: 0.75<br />
		/// Blizzard in a Bottle: 1.5<br />
		/// Cloud in a Bottle: 0.75<br />
		/// Fart in a Jar: 2<br />
		/// Goat mount: 2<br />
		/// Sandstorm in a Bottle: 3<br />
		/// Santank mount: 2<br />
		/// Tsunami in a Bottle: 1.25<br />
		/// Unicorn mount: 2
		/// </para>
		/// </summary>
		/// <param name="player">The player performing the jump</param>
		/// <returns>A modifier to the player's jump height, which when combined effectively acts as the duration for the jump</returns>
		// Token: 0x06001C32 RID: 7218
		public abstract float GetDurationMultiplier(Player player);

		/// <summary>
		/// An extra condition for whether this extra jump can be started.  Used by vanilla for flippers (<see cref="F:Terraria.Entity.wet" />).  Returns <see langword="true" /> by default.
		/// </summary>
		/// <param name="player">The player that would perform the jump</param>
		/// <returns><see langword="true" /> to let the jump be started, <see langword="false" /> otherwise.</returns>
		// Token: 0x06001C33 RID: 7219 RVA: 0x004D288F File Offset: 0x004D0A8F
		public virtual bool CanStart(Player player)
		{
			return true;
		}

		/// <summary>
		/// This hook runs when the player uses this jump via pressing the jump key<br />
		/// Effects that should appear when the jump starts can be spawned here.<br />
		/// For example, the Cloud in a Bottle's initial puff of smoke is spawned here.<br />
		/// <br />
		/// To make the jump re-usable, set <see cref="P:Terraria.DataStructures.ExtraJumpState.Available" /> to  <see langword="true" /> <br />
		/// </summary>
		/// <param name="player">The player performing the jump</param>
		/// <param name="playSound">Whether the poof sound should play.  Set this parameter to <see langword="false" /> if you want to play a different sound.</param>
		// Token: 0x06001C34 RID: 7220 RVA: 0x004D2892 File Offset: 0x004D0A92
		public virtual void OnStarted(Player player, ref bool playSound)
		{
		}

		/// <summary>
		/// This hook runs before <see cref="P:Terraria.DataStructures.ExtraJumpState.Active" /> is set from <see langword="true" /> to <see langword="false" /><br />
		/// Jumps end when their duration expires or when <see cref="P:Terraria.DataStructures.ExtraJumpState.Enabled" /> is no longer true. <br />
		/// Jumps may end early via <see cref="M:Terraria.Player.StopExtraJumpInProgress" />, called when a grappling hook is thrown, the player grabs onto a rope, or when the player is frozen, turned to stone or webbed.
		/// </summary>
		/// <param name="player">The player that was performing the jump</param>
		// Token: 0x06001C35 RID: 7221 RVA: 0x004D2894 File Offset: 0x004D0A94
		public virtual void OnEnded(Player player)
		{
		}

		/// <summary>
		/// Modify the player's horizontal movement while performing this jump here.<br />
		/// Only runs while the jump is <see cref="P:Terraria.DataStructures.ExtraJumpState.Active" /> <br />
		/// <br />
		/// Vanilla's jumps use the following values:
		/// <para>
		/// Basilisk mount: runAcceleration *= 3; maxRunSpeed *= 1.5;<br />
		/// Blizzard in a Bottle: runAcceleration *= 3; maxRunSpeed *= 1.5;<br />
		/// Cloud in a Bottle: no change<br />
		/// Fart in a Jar: runAcceleration *= 3; maxRunSpeed *= 1.75;<br />
		/// Goat mount: runAcceleration *= 3; maxRunSpeed *= 1.5;<br />
		/// Sandstorm in a Bottle: runAcceleration *= 1.5; maxRunSpeed *= 2;<br />
		/// Santank mount: runAcceleration *= 3; maxRunSpeed *= 1.5;<br />
		/// Tsunami in a Bottle: runAcceleration *= 1.5; maxRunSpeed *= 1.25;<br />
		/// Unicorn mount: runAcceleration *= 3; maxRunSpeed *= 1.5;
		/// </para>
		/// </summary>
		/// <param name="player">The player performing the jump</param>
		// Token: 0x06001C36 RID: 7222 RVA: 0x004D2896 File Offset: 0x004D0A96
		public virtual void UpdateHorizontalSpeeds(Player player)
		{
		}

		/// <summary>
		/// This hook runs before <see cref="P:Terraria.DataStructures.ExtraJumpState.Available" /> is set to <see langword="true" /> in <see cref="M:Terraria.Player.RefreshDoubleJumps" /><br />
		/// This occurs at the start of the grounded jump and while the player is grounded, or when jumping off a grappling hook/rope.
		/// </summary>
		/// <param name="player">The player instance</param>
		// Token: 0x06001C37 RID: 7223 RVA: 0x004D2898 File Offset: 0x004D0A98
		public virtual void OnRefreshed(Player player)
		{
		}

		// Token: 0x04001517 RID: 5399
		protected static readonly ExtraJump.Position BeforeMountJumps = new ExtraJump.Before(ExtraJump.BasiliskMount);

		// Token: 0x04001518 RID: 5400
		protected static readonly ExtraJump.Position MountJumpPosition = new ExtraJump.After(ExtraJump.BasiliskMount);

		// Token: 0x04001519 RID: 5401
		protected static readonly ExtraJump.Position BeforeBottleJumps = new ExtraJump.Before(ExtraJump.SandstormInABottle);

		// Token: 0x0400151A RID: 5402
		protected static readonly ExtraJump.Position AfterBottleJumps = new ExtraJump.After(ExtraJump.CloudInABottle);

		// Token: 0x020008CA RID: 2250
		public abstract class Position
		{
			// Token: 0x170008D1 RID: 2257
			// (get) Token: 0x06005260 RID: 21088 RVA: 0x00698CD7 File Offset: 0x00696ED7
			// (set) Token: 0x06005261 RID: 21089 RVA: 0x00698CDF File Offset: 0x00696EDF
			public ExtraJump Target { get; protected set; }
		}

		// Token: 0x020008CB RID: 2251
		public sealed class Before : ExtraJump.Position
		{
			// Token: 0x06005263 RID: 21091 RVA: 0x00698CF0 File Offset: 0x00696EF0
			public Before(ExtraJump parent)
			{
				base.Target = parent;
			}
		}

		// Token: 0x020008CC RID: 2252
		public sealed class After : ExtraJump.Position
		{
			// Token: 0x06005264 RID: 21092 RVA: 0x00698CFF File Offset: 0x00696EFF
			public After(ExtraJump parent)
			{
				base.Target = parent;
			}
		}
	}
}
