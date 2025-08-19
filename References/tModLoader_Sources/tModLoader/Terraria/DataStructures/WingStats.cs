using System;

namespace Terraria.DataStructures
{
	/// <summary>
	/// Stores the stats and settings for a <see cref="T:Terraria.ID.ArmorIDs.Wing" /> equip.
	/// </summary>
	// Token: 0x02000741 RID: 1857
	public struct WingStats
	{
		/// <summary>
		/// Create a new <see cref="T:Terraria.DataStructures.WingStats" /> with the provided stats.
		/// </summary>
		// Token: 0x06004B61 RID: 19297 RVA: 0x0066A02D File Offset: 0x0066822D
		public WingStats(int flyTime = 100, float flySpeedOverride = -1f, float accelerationMultiplier = 1f, bool hasHoldDownHoverFeatures = false, float hoverFlySpeedOverride = -1f, float hoverAccelerationMultiplier = 1f)
		{
			this.FlyTime = flyTime;
			this.AccRunSpeedOverride = flySpeedOverride;
			this.AccRunAccelerationMult = accelerationMultiplier;
			this.HasDownHoverStats = hasHoldDownHoverFeatures;
			this.DownHoverSpeedOverride = hoverFlySpeedOverride;
			this.DownHoverAccelerationMult = hoverAccelerationMultiplier;
		}

		/// <summary>
		/// Creates a new <see cref="T:Terraria.DataStructures.WingStats" /> with a speed multiplier applied to <see cref="F:Terraria.DataStructures.WingStats.AccRunSpeedOverride" /> and <see cref="F:Terraria.DataStructures.WingStats.DownHoverSpeedOverride" />.
		/// </summary>
		/// <param name="multiplier">The multiplier.</param>
		/// <returns>The modified <see cref="T:Terraria.DataStructures.WingStats" />.</returns>
		// Token: 0x06004B62 RID: 19298 RVA: 0x0066A05C File Offset: 0x0066825C
		public WingStats WithSpeedBoost(float multiplier)
		{
			return new WingStats(this.FlyTime, this.AccRunSpeedOverride * multiplier, this.AccRunAccelerationMult, this.HasDownHoverStats, this.DownHoverSpeedOverride * multiplier, this.DownHoverAccelerationMult);
		}

		// Token: 0x04006078 RID: 24696
		public static readonly WingStats Default;

		/// <summary>
		/// The time in ticks that a player can fly for.
		/// </summary>
		// Token: 0x04006079 RID: 24697
		public int FlyTime;

		/// <summary>
		/// Overrides the value of <see cref="F:Terraria.Player.accRunSpeed" /> while the player is in the air.
		/// <br /> Only applies if this value is greater than <see cref="F:Terraria.Player.accRunSpeed" />.
		/// <br /> If <c>-1f</c>, then no effect.
		/// </summary>
		// Token: 0x0400607A RID: 24698
		public float AccRunSpeedOverride;

		/// <summary>
		/// A multiplier applied to <see cref="F:Terraria.Player.runAcceleration" /> while the player is in the air.
		/// </summary>
		// Token: 0x0400607B RID: 24699
		public float AccRunAccelerationMult;

		/// <summary>
		/// If <see langword="true" />, then players can use these wings to hover.
		/// <br /> When hovering, <see cref="F:Terraria.DataStructures.WingStats.DownHoverSpeedOverride" /> and <see cref="F:Terraria.DataStructures.WingStats.DownHoverAccelerationMult" /> apply instead of <see cref="F:Terraria.DataStructures.WingStats.AccRunSpeedOverride" /> and <see cref="F:Terraria.DataStructures.WingStats.AccRunAccelerationMult" />, respectively.
		/// </summary>
		// Token: 0x0400607C RID: 24700
		public bool HasDownHoverStats;

		/// <summary>
		/// Overrides the value of <see cref="F:Terraria.DataStructures.WingStats.AccRunSpeedOverride" /> if <see cref="F:Terraria.DataStructures.WingStats.HasDownHoverStats" /> is <see langword="true" /> and the player is hovering.
		/// </summary>
		// Token: 0x0400607D RID: 24701
		public float DownHoverSpeedOverride;

		/// <summary>
		/// Overrides the value of <see cref="F:Terraria.DataStructures.WingStats.AccRunAccelerationMult" /> if <see cref="F:Terraria.DataStructures.WingStats.HasDownHoverStats" /> is <see langword="true" /> and the player is hovering.
		/// </summary>
		// Token: 0x0400607E RID: 24702
		public float DownHoverAccelerationMult;
	}
}
