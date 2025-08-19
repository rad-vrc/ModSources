using System;
using Terraria.DataStructures;

namespace Terraria.Modules
{
	// Token: 0x02000054 RID: 84
	public class AnchorDataModule
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x0048C0E8 File Offset: 0x0048A2E8
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

		// Token: 0x04000ED4 RID: 3796
		public AnchorData top;

		// Token: 0x04000ED5 RID: 3797
		public AnchorData bottom;

		// Token: 0x04000ED6 RID: 3798
		public AnchorData left;

		// Token: 0x04000ED7 RID: 3799
		public AnchorData right;

		// Token: 0x04000ED8 RID: 3800
		public bool wall;
	}
}
