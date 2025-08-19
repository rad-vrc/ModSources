using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003CD RID: 973
	public class UndergroundBiome : AShoppingBiome
	{
		// Token: 0x06002A8D RID: 10893 RVA: 0x00599B39 File Offset: 0x00597D39
		public UndergroundBiome()
		{
			base.NameKey = "NormalUnderground";
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x00599B4C File Offset: 0x00597D4C
		public override bool IsInBiome(Player player)
		{
			return player.ShoppingZone_BelowSurface;
		}
	}
}
