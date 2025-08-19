using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x0200005E RID: 94
	public class TileObjectCoordinatesModule
	{
		// Token: 0x06001124 RID: 4388 RVA: 0x0048C650 File Offset: 0x0048A850
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

		// Token: 0x04000EFA RID: 3834
		public int width;

		// Token: 0x04000EFB RID: 3835
		public int[] heights;

		// Token: 0x04000EFC RID: 3836
		public int padding;

		// Token: 0x04000EFD RID: 3837
		public Point16 paddingFix;

		// Token: 0x04000EFE RID: 3838
		public int styleWidth;

		// Token: 0x04000EFF RID: 3839
		public int styleHeight;

		// Token: 0x04000F00 RID: 3840
		public bool calculated;

		// Token: 0x04000F01 RID: 3841
		public int drawStyleOffset;
	}
}
