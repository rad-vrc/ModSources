using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005CC RID: 1484
	public class PotionOfReturnHelper
	{
		// Token: 0x060042DC RID: 17116 RVA: 0x005FAA1C File Offset: 0x005F8C1C
		public static bool TryGetGateHitbox(Player player, out Rectangle homeHitbox)
		{
			homeHitbox = Rectangle.Empty;
			if (player.PotionOfReturnHomePosition == null)
			{
				return false;
			}
			Vector2 vector;
			vector..ctor(0f, -21f);
			Vector2 center = player.PotionOfReturnHomePosition.Value + vector;
			homeHitbox = Utils.CenteredRectangle(center, new Vector2(24f, 40f));
			return true;
		}
	}
}
