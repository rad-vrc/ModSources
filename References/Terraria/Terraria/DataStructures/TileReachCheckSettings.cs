using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000416 RID: 1046
	public struct TileReachCheckSettings
	{
		// Token: 0x06002B5F RID: 11103 RVA: 0x0059E21C File Offset: 0x0059C41C
		public void GetRanges(Player player, out int x, out int y)
		{
			x = Player.tileRangeX * this.TileRangeMultiplier;
			y = Player.tileRangeY * this.TileRangeMultiplier;
			int? tileReachLimit = this.TileReachLimit;
			if (tileReachLimit != null)
			{
				int value = tileReachLimit.Value;
				if (x > value)
				{
					x = value;
				}
				if (y > value)
				{
					y = value;
				}
			}
			if (this.OverrideXReach != null)
			{
				x = this.OverrideXReach.Value;
			}
			if (this.OverrideYReach != null)
			{
				y = this.OverrideYReach.Value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0059E2A4 File Offset: 0x0059C4A4
		public static TileReachCheckSettings Simple
		{
			get
			{
				return new TileReachCheckSettings
				{
					TileRangeMultiplier = 1,
					TileReachLimit = new int?(20)
				};
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x0059E2D0 File Offset: 0x0059C4D0
		public static TileReachCheckSettings Pylons
		{
			get
			{
				return new TileReachCheckSettings
				{
					OverrideXReach = new int?(60),
					OverrideYReach = new int?(60)
				};
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0059E304 File Offset: 0x0059C504
		public static TileReachCheckSettings QuickStackToNearbyChests
		{
			get
			{
				return new TileReachCheckSettings
				{
					OverrideXReach = new int?(39),
					OverrideYReach = new int?(39)
				};
			}
		}

		// Token: 0x04004F74 RID: 20340
		public int TileRangeMultiplier;

		// Token: 0x04004F75 RID: 20341
		public int? TileReachLimit;

		// Token: 0x04004F76 RID: 20342
		public int? OverrideXReach;

		// Token: 0x04004F77 RID: 20343
		public int? OverrideYReach;
	}
}
