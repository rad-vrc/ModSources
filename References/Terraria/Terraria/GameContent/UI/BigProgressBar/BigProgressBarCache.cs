using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003AD RID: 941
	public struct BigProgressBarCache
	{
		// Token: 0x060029E8 RID: 10728 RVA: 0x00596267 File Offset: 0x00594467
		public void SetLife(float current, float max)
		{
			this.LifeCurrent = current;
			this.LifeMax = max;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00596277 File Offset: 0x00594477
		public void SetShield(float current, float max)
		{
			this.ShieldCurrent = current;
			this.ShieldMax = max;
		}

		// Token: 0x04004CCD RID: 19661
		public float LifeCurrent;

		// Token: 0x04004CCE RID: 19662
		public float LifeMax;

		// Token: 0x04004CCF RID: 19663
		public float ShieldCurrent;

		// Token: 0x04004CD0 RID: 19664
		public float ShieldMax;
	}
}
