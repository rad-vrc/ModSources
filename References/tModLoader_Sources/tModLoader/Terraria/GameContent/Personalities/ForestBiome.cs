using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B8 RID: 1464
	public class ForestBiome : AShoppingBiome
	{
		// Token: 0x060042AD RID: 17069 RVA: 0x005FA146 File Offset: 0x005F8346
		public ForestBiome()
		{
			base.NameKey = "Forest";
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x005FA159 File Offset: 0x005F8359
		public override bool IsInBiome(Player player)
		{
			return player.ShoppingZone_Forest;
		}
	}
}
