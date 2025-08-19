using System;

namespace Terraria.ID
{
	/// <summary>
	/// Enumerates the different immunity cooldown options for damage affecting a player. Most damage uses <see cref="F:Terraria.ID.ImmunityCooldownID.General" /> and applies immunity via <see cref="F:Terraria.Player.immune" />. Other damage immunity cooldowns are tracked in <see cref="F:Terraria.Player.hurtCooldowns" /> indexed by these values.<para />
	/// Correct usage of <see cref="T:Terraria.ID.ImmunityCooldownID" /> in <see cref="P:Terraria.ModLoader.ModProjectile.CooldownSlot" />, <see cref="M:Terraria.ModLoader.ModNPC.CanHitPlayer(Terraria.Player,System.Int32@)" />, and <see cref="M:Terraria.Player.Hurt(Terraria.DataStructures.PlayerDeathReason,System.Int32,System.Int32,System.Boolean,System.Boolean,System.Int32,System.Boolean,System.Single,System.Single,System.Single)" /> are essential for correctly applying damage to the player.
	/// </summary>
	// Token: 0x0200040A RID: 1034
	public static class ImmunityCooldownID
	{
		/// <summary>
		/// Default, no special slot, just <see cref="F:Terraria.Player.immuneTime" />
		/// </summary>
		// Token: 0x040027E3 RID: 10211
		public const int General = -1;

		/// <summary>
		/// Contacting with tiles that deals damage, such as spikes and cactus in don't starve world
		/// </summary>
		// Token: 0x040027E4 RID: 10212
		public const int TileContactDamage = 0;

		/// <summary>
		/// Bosses like Moon Lord and Empress of Light (and their minions and projectiles)<para />
		/// Prevents cheesing by taking repeated low damage from another source
		/// </summary>
		// Token: 0x040027E5 RID: 10213
		public const int Bosses = 1;

		// Token: 0x040027E6 RID: 10214
		public const int DD2OgreKnockback = 2;

		/// <summary>
		/// Trying to catch lava critters with regular bug net
		/// </summary>
		// Token: 0x040027E7 RID: 10215
		public const int WrongBugNet = 3;

		/// <summary>
		/// Damage from lava
		/// </summary>
		// Token: 0x040027E8 RID: 10216
		public const int Lava = 4;
	}
}
