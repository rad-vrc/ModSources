using System;

namespace Terraria.Modules
{
	// Token: 0x0200005A RID: 90
	public class TileObjectDrawModule
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x0048C440 File Offset: 0x0048A640
		public TileObjectDrawModule(TileObjectDrawModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.xOffset = 0;
				this.yOffset = 0;
				this.flipHorizontal = false;
				this.flipVertical = false;
				this.stepDown = 0;
				return;
			}
			this.xOffset = copyFrom.xOffset;
			this.yOffset = copyFrom.yOffset;
			this.flipHorizontal = copyFrom.flipHorizontal;
			this.flipVertical = copyFrom.flipVertical;
			this.stepDown = copyFrom.stepDown;
		}

		// Token: 0x04000EE9 RID: 3817
		public int xOffset;

		// Token: 0x04000EEA RID: 3818
		public int yOffset;

		// Token: 0x04000EEB RID: 3819
		public bool flipHorizontal;

		// Token: 0x04000EEC RID: 3820
		public bool flipVertical;

		// Token: 0x04000EED RID: 3821
		public int stepDown;
	}
}
