using System;
using Terraria.Enums;

namespace Terraria.Modules
{
	// Token: 0x02000059 RID: 89
	public class LiquidPlacementModule
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x0048C40E File Offset: 0x0048A60E
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

		// Token: 0x04000EE7 RID: 3815
		public LiquidPlacement water;

		// Token: 0x04000EE8 RID: 3816
		public LiquidPlacement lava;
	}
}
