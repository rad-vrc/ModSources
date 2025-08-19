using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x0200012D RID: 301
	public class AnchorDataModule
	{
		// Token: 0x06001A79 RID: 6777 RVA: 0x004CB8E4 File Offset: 0x004C9AE4
		public AnchorDataModule(AnchorDataModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.top = default(AnchorData);
				this.bottom = default(AnchorData);
				this.left = default(AnchorData);
				this.right = default(AnchorData);
				this.wall = false;
				return;
			}
			this.top = copyFrom.top;
			this.bottom = copyFrom.bottom;
			this.left = copyFrom.left;
			this.right = copyFrom.right;
			this.wall = copyFrom.wall;
		}

		// Token: 0x04001433 RID: 5171
		public AnchorData top;

		// Token: 0x04001434 RID: 5172
		public AnchorData bottom;

		// Token: 0x04001435 RID: 5173
		public AnchorData left;

		// Token: 0x04001436 RID: 5174
		public AnchorData right;

		// Token: 0x04001437 RID: 5175
		public bool wall;
	}
}
