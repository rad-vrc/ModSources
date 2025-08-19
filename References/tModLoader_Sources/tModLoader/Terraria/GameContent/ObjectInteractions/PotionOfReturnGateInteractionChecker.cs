using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005CB RID: 1483
	public class PotionOfReturnGateInteractionChecker : AHoverInteractionChecker
	{
		// Token: 0x060042D7 RID: 17111 RVA: 0x005FA9C0 File Offset: 0x005F8BC0
		internal override bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle)
		{
			if (Main.SmartInteractPotionOfReturn)
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x005FA9E4 File Offset: 0x005F8BE4
		internal override void DoHoverEffect(Player player, Rectangle hitbox)
		{
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = 4870;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x005FA9FF File Offset: 0x005F8BFF
		internal override bool ShouldBlockInteraction(Player player, Rectangle hitbox)
		{
			return Player.BlockInteractionWithProjectiles != 0;
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x005FAA09 File Offset: 0x005F8C09
		internal override void PerformInteraction(Player player, Rectangle hitbox)
		{
			player.DoPotionOfReturnReturnToOriginalUsePosition();
		}
	}
}
