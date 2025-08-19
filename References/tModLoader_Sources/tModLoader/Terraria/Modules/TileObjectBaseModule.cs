using System;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.Modules
{
	// Token: 0x02000132 RID: 306
	public class TileObjectBaseModule
	{
		// Token: 0x06001A7E RID: 6782 RVA: 0x004CBB74 File Offset: 0x004C9D74
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

		// Token: 0x04001441 RID: 5185
		public int width;

		// Token: 0x04001442 RID: 5186
		public int height;

		// Token: 0x04001443 RID: 5187
		public Point16 origin;

		// Token: 0x04001444 RID: 5188
		public TileObjectDirection direction;

		// Token: 0x04001445 RID: 5189
		public int randomRange;

		// Token: 0x04001446 RID: 5190
		public bool flattenAnchors;

		// Token: 0x04001447 RID: 5191
		public int[] specificRandomStyles;
	}
}
