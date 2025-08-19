using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000306 RID: 774
	internal class HER0zeroPlayer : ModPlayer
	{
		// Token: 0x06002E7B RID: 11899 RVA: 0x00531C30 File Offset: 0x0052FE30
		public override void ResetEffects()
		{
			this.glowEffect = false;
		}

		// Token: 0x04001C7C RID: 7292
		public bool glowEffect;
	}
}
