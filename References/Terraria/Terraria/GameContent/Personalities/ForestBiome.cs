using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003C9 RID: 969
	public class ForestBiome : AShoppingBiome
	{
		// Token: 0x06002A85 RID: 10885 RVA: 0x00599ACD File Offset: 0x00597CCD
		public ForestBiome()
		{
			base.NameKey = "Forest";
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00599AE0 File Offset: 0x00597CE0
		public override bool IsInBiome(Player player)
		{
			return player.ShoppingZone_Forest;
		}
	}
}
