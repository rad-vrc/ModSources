using System;

namespace Terraria.Modules
{
	// Token: 0x0200012F RID: 303
	public class LiquidDeathModule
	{
		// Token: 0x06001A7B RID: 6779 RVA: 0x004CBA96 File Offset: 0x004C9C96
		public LiquidDeathModule(LiquidDeathModule copyFrom = null)
		{
			if (copyFrom == null)
			{
				this.water = false;
				this.lava = false;
				return;
			}
			this.water = copyFrom.water;
			this.lava = copyFrom.lava;
		}

		// Token: 0x0400143C RID: 5180
		public bool water;

		// Token: 0x0400143D RID: 5181
		public bool lava;
	}
}
