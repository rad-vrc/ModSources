using System;
using Terraria.Enums;

namespace Terraria.Modules
{
	// Token: 0x02000130 RID: 304
	public class LiquidPlacementModule
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x004CBAC8 File Offset: 0x004C9CC8
		public LiquidPlacementModule(LiquidPlacementModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.water = LiquidPlacement.Allowed;
				this.lava = LiquidPlacement.Allowed;
				return;
			}
			this.water = copyFrom.water;
			this.lava = copyFrom.lava;
		}

		// Token: 0x0400143E RID: 5182
		public LiquidPlacement water;

		// Token: 0x0400143F RID: 5183
		public LiquidPlacement lava;
	}
}
