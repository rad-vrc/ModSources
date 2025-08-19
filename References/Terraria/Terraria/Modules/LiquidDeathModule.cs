using System;

namespace Terraria.Modules
{
	// Token: 0x02000058 RID: 88
	public class LiquidDeathModule
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x0048C3DC File Offset: 0x0048A5DC
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

		// Token: 0x04000EE5 RID: 3813
		public bool water;

		// Token: 0x04000EE6 RID: 3814
		public bool lava;
	}
}
