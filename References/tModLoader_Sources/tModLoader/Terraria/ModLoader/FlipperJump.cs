using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200021B RID: 539
	public sealed class FlipperJump : VanillaExtraJump
	{
		// Token: 0x0600282C RID: 10284 RVA: 0x00509A56 File Offset: 0x00507C56
		public override float GetDurationMultiplier(Player player)
		{
			return 1f;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x00509A5D File Offset: 0x00507C5D
		public override bool CanStart(Player player)
		{
			return (!player.mount.Active || !player.mount.Cart) && player.wet;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00509A81 File Offset: 0x00507C81
		public override void OnStarted(Player player, ref bool playSound)
		{
			if (player.swimTime == 0)
			{
				player.swimTime = 30;
			}
			if (player.sliding)
			{
				player.velocity.X = (float)(3 * -(float)player.slideDir);
			}
			playSound = false;
			player.GetJumpState<FlipperJump>(this).Available = true;
		}
	}
}
