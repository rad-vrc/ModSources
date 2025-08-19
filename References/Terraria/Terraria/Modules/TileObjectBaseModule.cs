using System;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.Modules
{
	// Token: 0x02000057 RID: 87
	public class TileObjectBaseModule
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x0048C314 File Offset: 0x0048A514
		public TileObjectBaseModule(TileObjectBaseModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.width = 1;
				this.height = 1;
				this.origin = Point16.Zero;
				this.direction = TileObjectDirection.None;
				this.randomRange = 0;
				this.flattenAnchors = false;
				this.specificRandomStyles = null;
				return;
			}
			this.width = copyFrom.width;
			this.height = copyFrom.height;
			this.origin = copyFrom.origin;
			this.direction = copyFrom.direction;
			this.randomRange = copyFrom.randomRange;
			this.flattenAnchors = copyFrom.flattenAnchors;
			this.specificRandomStyles = null;
			if (copyFrom.specificRandomStyles != null)
			{
				this.specificRandomStyles = new int[copyFrom.specificRandomStyles.Length];
				copyFrom.specificRandomStyles.CopyTo(this.specificRandomStyles, 0);
			}
		}

		// Token: 0x04000EDE RID: 3806
		public int width;

		// Token: 0x04000EDF RID: 3807
		public int height;

		// Token: 0x04000EE0 RID: 3808
		public Point16 origin;

		// Token: 0x04000EE1 RID: 3809
		public TileObjectDirection direction;

		// Token: 0x04000EE2 RID: 3810
		public int randomRange;

		// Token: 0x04000EE3 RID: 3811
		public bool flattenAnchors;

		// Token: 0x04000EE4 RID: 3812
		public int[] specificRandomStyles;
	}
}
