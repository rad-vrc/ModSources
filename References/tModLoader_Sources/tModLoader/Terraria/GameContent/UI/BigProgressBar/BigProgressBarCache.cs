using System;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000546 RID: 1350
	public struct BigProgressBarCache
	{
		// Token: 0x06004017 RID: 16407 RVA: 0x005DE0CE File Offset: 0x005DC2CE
		public void SetLife(float current, float max)
		{
			this.LifeCurrent = current;
			this.LifeMax = max;
		}

		// Token: 0x06004018 RID: 16408 RVA: 0x005DE0DE File Offset: 0x005DC2DE
		public void SetShield(float current, float max)
		{
			this.ShieldCurrent = current;
			this.ShieldMax = max;
		}

		// Token: 0x0400583F RID: 22591
		public float LifeCurrent;

		// Token: 0x04005840 RID: 22592
		public float LifeMax;

		// Token: 0x04005841 RID: 22593
		public float ShieldCurrent;

		// Token: 0x04005842 RID: 22594
		public float ShieldMax;
	}
}
