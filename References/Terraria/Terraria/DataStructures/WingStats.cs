using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200045A RID: 1114
	public struct WingStats
	{
		// Token: 0x06002C8E RID: 11406 RVA: 0x005BB687 File Offset: 0x005B9887
		public WingStats(int flyTime = 100, float flySpeedOverride = -1f, float accelerationMultiplier = 1f, bool hasHoldDownHoverFeatures = false, float hoverFlySpeedOverride = -1f, float hoverAccelerationMultiplier = 1f)
		{
			this.FlyTime = flyTime;
			this.AccRunSpeedOverride = flySpeedOverride;
			this.AccRunAccelerationMult = accelerationMultiplier;
			this.HasDownHoverStats = hasHoldDownHoverFeatures;
			this.DownHoverSpeedOverride = hoverFlySpeedOverride;
			this.DownHoverAccelerationMult = hoverAccelerationMultiplier;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x005BB6B6 File Offset: 0x005B98B6
		public WingStats WithSpeedBoost(float multiplier)
		{
			return new WingStats(this.FlyTime, this.AccRunSpeedOverride * multiplier, this.AccRunAccelerationMult, this.HasDownHoverStats, this.DownHoverSpeedOverride * multiplier, this.DownHoverAccelerationMult);
		}

		// Token: 0x040050F0 RID: 20720
		public static readonly WingStats Default;

		// Token: 0x040050F1 RID: 20721
		public int FlyTime;

		// Token: 0x040050F2 RID: 20722
		public float AccRunSpeedOverride;

		// Token: 0x040050F3 RID: 20723
		public float AccRunAccelerationMult;

		// Token: 0x040050F4 RID: 20724
		public bool HasDownHoverStats;

		// Token: 0x040050F5 RID: 20725
		public float DownHoverSpeedOverride;

		// Token: 0x040050F6 RID: 20726
		public float DownHoverAccelerationMult;
	}
}
