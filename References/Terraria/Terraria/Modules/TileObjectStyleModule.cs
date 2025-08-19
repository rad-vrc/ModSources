using System;

namespace Terraria.Modules
{
	// Token: 0x0200005D RID: 93
	public class TileObjectStyleModule
	{
		// Token: 0x06001123 RID: 4387 RVA: 0x0048C5A8 File Offset: 0x0048A7A8
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

		// Token: 0x04000EF3 RID: 3827
		public int style;

		// Token: 0x04000EF4 RID: 3828
		public bool horizontal;

		// Token: 0x04000EF5 RID: 3829
		public int styleWrapLimit;

		// Token: 0x04000EF6 RID: 3830
		public int styleMultiplier;

		// Token: 0x04000EF7 RID: 3831
		public int styleLineSkip;

		// Token: 0x04000EF8 RID: 3832
		public int? styleWrapLimitVisualOverride;

		// Token: 0x04000EF9 RID: 3833
		public int? styleLineSkipVisualoverride;
	}
}
