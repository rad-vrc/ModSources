using System;

namespace Terraria.Modules
{
	// Token: 0x02000134 RID: 308
	public class TileObjectDrawModule
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x004CBD2C File Offset: 0x004C9F2C
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

		// Token: 0x04001450 RID: 5200
		public int xOffset;

		// Token: 0x04001451 RID: 5201
		public int yOffset;

		// Token: 0x04001452 RID: 5202
		public bool flipHorizontal;

		// Token: 0x04001453 RID: 5203
		public bool flipVertical;

		// Token: 0x04001454 RID: 5204
		public int stepDown;
	}
}
