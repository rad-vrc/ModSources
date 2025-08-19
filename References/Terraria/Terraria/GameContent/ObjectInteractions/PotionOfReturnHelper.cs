using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000268 RID: 616
	public class PotionOfReturnHelper
	{
		// Token: 0x06001F9C RID: 8092 RVA: 0x00516328 File Offset: 0x00514528
		public static bool TryGetGateHitbox(Player player, out Rectangle homeHitbox)
		{
			homeHitbox = Rectangle.Empty;
			if (player.PotionOfReturnHomePosition == null)
			{
				return false;
			}
			Vector2 value = new Vector2(0f, -21f);
			Vector2 center = player.PotionOfReturnHomePosition.Value + value;
			homeHitbox = Utils.CenteredRectangle(center, new Vector2(24f, 40f));
			return true;
		}
	}
}
