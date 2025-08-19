using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000320 RID: 800
	internal class SaetharSetEffectPlayer : ModPlayer
	{
		// Token: 0x06002EC7 RID: 11975 RVA: 0x005326D2 File Offset: 0x005308D2
		public override void ResetEffects()
		{
			this.IsActive = false;
		}

		// Token: 0x04001C81 RID: 7297
		public bool IsActive;
	}
}
