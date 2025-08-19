using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200041B RID: 1051
	public struct PortableStoolUsage
	{
		// Token: 0x06002B7A RID: 11130 RVA: 0x0059E6C1 File Offset: 0x0059C8C1
		public void Reset()
		{
			this.HasAStool = false;
			this.IsInUse = false;
			this.HeightBoost = 0;
			this.VisualYOffset = 0;
			this.MapYOffset = 0;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0059E6E6 File Offset: 0x0059C8E6
		public void SetStats(int heightBoost, int visualYOffset, int mapYOffset)
		{
			this.HasAStool = true;
			this.HeightBoost = heightBoost;
			this.VisualYOffset = visualYOffset;
			this.MapYOffset = mapYOffset;
		}

		// Token: 0x04004FA7 RID: 20391
		public bool HasAStool;

		// Token: 0x04004FA8 RID: 20392
		public bool IsInUse;

		// Token: 0x04004FA9 RID: 20393
		public int HeightBoost;

		// Token: 0x04004FAA RID: 20394
		public int VisualYOffset;

		// Token: 0x04004FAB RID: 20395
		public int MapYOffset;
	}
}
