using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000269 RID: 617
	public class PotionOfReturnGateInteractionChecker : AHoverInteractionChecker
	{
		// Token: 0x06001F9E RID: 8094 RVA: 0x00516390 File Offset: 0x00514590
		internal override bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle)
		{
			if (Main.SmartInteractPotionOfReturn)
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x005163B4 File Offset: 0x005145B4
		internal override void DoHoverEffect(Player player, Rectangle hitbox)
		{
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = 4870;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x005163CF File Offset: 0x005145CF
		internal override bool ShouldBlockInteraction(Player player, Rectangle hitbox)
		{
			return Player.BlockInteractionWithProjectiles != 0;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x005163D9 File Offset: 0x005145D9
		internal override void PerformInteraction(Player player, Rectangle hitbox)
		{
			player.DoPotionOfReturnReturnToOriginalUsePosition();
		}
	}
}
