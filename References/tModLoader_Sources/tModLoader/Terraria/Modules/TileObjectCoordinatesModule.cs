using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x02000133 RID: 307
	public class TileObjectCoordinatesModule
	{
		// Token: 0x06001A7F RID: 6783 RVA: 0x004CBC3C File Offset: 0x004C9E3C
		public TileObjectCoordinatesModule(TileObjectCoordinatesModule copyFrom = null, int[] drawHeight = null)
		{
			if (copyFrom == null)
			{
				this.width = 0;
				this.padding = 0;
				this.paddingFix = Point16.Zero;
				this.styleWidth = 0;
				this.drawStyleOffset = 0;
				this.styleHeight = 0;
				this.calculated = false;
				this.heights = drawHeight;
				return;
			}
			this.width = copyFrom.width;
			this.padding = copyFrom.padding;
			this.paddingFix = copyFrom.paddingFix;
			this.drawStyleOffset = copyFrom.drawStyleOffset;
			this.styleWidth = copyFrom.styleWidth;
			this.styleHeight = copyFrom.styleHeight;
			this.calculated = copyFrom.calculated;
			if (drawHeight != null)
			{
				this.heights = drawHeight;
				return;
			}
			if (copyFrom.heights == null)
			{
				this.heights = null;
				return;
			}
			this.heights = new int[copyFrom.heights.Length];
			Array.Copy(copyFrom.heights, this.heights, this.heights.Length);
		}

		// Token: 0x04001448 RID: 5192
		public int width;

		// Token: 0x04001449 RID: 5193
		public int[] heights;

		// Token: 0x0400144A RID: 5194
		public int padding;

		// Token: 0x0400144B RID: 5195
		public Point16 paddingFix;

		// Token: 0x0400144C RID: 5196
		public int styleWidth;

		// Token: 0x0400144D RID: 5197
		public int styleHeight;

		// Token: 0x0400144E RID: 5198
		public bool calculated;

		// Token: 0x0400144F RID: 5199
		public int drawStyleOffset;
	}
}
