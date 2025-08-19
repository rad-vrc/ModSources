using System;

namespace Terraria.Modules
{
	// Token: 0x02000135 RID: 309
	public class TileObjectStyleModule
	{
		// Token: 0x06001A81 RID: 6785 RVA: 0x004CBDA4 File Offset: 0x004C9FA4
		public TileObjectStyleModule(TileObjectStyleModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.style = 0;
				this.horizontal = false;
				this.styleWrapLimit = 0;
				this.styleWrapLimitVisualOverride = null;
				this.styleLineSkipVisualoverride = null;
				this.styleMultiplier = 1;
				this.styleLineSkip = 1;
				return;
			}
			this.style = copyFrom.style;
			this.horizontal = copyFrom.horizontal;
			this.styleWrapLimit = copyFrom.styleWrapLimit;
			this.styleMultiplier = copyFrom.styleMultiplier;
			this.styleLineSkip = copyFrom.styleLineSkip;
			this.styleWrapLimitVisualOverride = copyFrom.styleWrapLimitVisualOverride;
			this.styleLineSkipVisualoverride = copyFrom.styleLineSkipVisualoverride;
		}

		// Token: 0x04001455 RID: 5205
		public int style;

		// Token: 0x04001456 RID: 5206
		public bool horizontal;

		// Token: 0x04001457 RID: 5207
		public int styleWrapLimit;

		// Token: 0x04001458 RID: 5208
		public int styleMultiplier;

		// Token: 0x04001459 RID: 5209
		public int styleLineSkip;

		// Token: 0x0400145A RID: 5210
		public int? styleWrapLimitVisualOverride;

		// Token: 0x0400145B RID: 5211
		public int? styleLineSkipVisualoverride;
	}
}
