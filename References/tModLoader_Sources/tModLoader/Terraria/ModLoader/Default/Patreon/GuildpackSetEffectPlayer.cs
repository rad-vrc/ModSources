using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002FD RID: 765
	internal class GuildpackSetEffectPlayer : ModPlayer
	{
		// Token: 0x06002E61 RID: 11873 RVA: 0x00531727 File Offset: 0x0052F927
		public override void ResetEffects()
		{
			this.IsActive = false;
		}

		// Token: 0x04001C78 RID: 7288
		public bool IsActive;
	}
}
