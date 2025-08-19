using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200072B RID: 1835
	public struct PortableStoolUsage
	{
		// Token: 0x06004AAB RID: 19115 RVA: 0x00668110 File Offset: 0x00666310
		public void Reset()
		{
			this.HasAStool = false;
			this.IsInUse = false;
			this.HeightBoost = 0;
			this.VisualYOffset = 0;
			this.MapYOffset = 0;
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00668135 File Offset: 0x00666335
		public void SetStats(int heightBoost, int visualYOffset, int mapYOffset)
		{
			this.HasAStool = true;
			this.HeightBoost = heightBoost;
			this.VisualYOffset = visualYOffset;
			this.MapYOffset = mapYOffset;
		}

		// Token: 0x04005FF1 RID: 24561
		public bool HasAStool;

		// Token: 0x04005FF2 RID: 24562
		public bool IsInUse;

		// Token: 0x04005FF3 RID: 24563
		public int HeightBoost;

		// Token: 0x04005FF4 RID: 24564
		public int VisualYOffset;

		// Token: 0x04005FF5 RID: 24565
		public int MapYOffset;
	}
}
