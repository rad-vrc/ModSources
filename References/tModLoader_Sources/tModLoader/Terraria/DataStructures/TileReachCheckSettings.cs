using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200073A RID: 1850
	public struct TileReachCheckSettings
	{
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06004B35 RID: 19253 RVA: 0x00669934 File Offset: 0x00667B34
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

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06004B36 RID: 19254 RVA: 0x00669960 File Offset: 0x00667B60
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

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x00669994 File Offset: 0x00667B94
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

		// Token: 0x06004B38 RID: 19256 RVA: 0x006699C8 File Offset: 0x00667BC8
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

		// Token: 0x04006055 RID: 24661
		public int TileRangeMultiplier;

		// Token: 0x04006056 RID: 24662
		public int? TileReachLimit;

		// Token: 0x04006057 RID: 24663
		public int? OverrideXReach;

		// Token: 0x04006058 RID: 24664
		public int? OverrideYReach;
	}
}
