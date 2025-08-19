using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005C4 RID: 1476
	public class UndergroundBiome : AShoppingBiome
	{
		// Token: 0x060042C5 RID: 17093 RVA: 0x005FA697 File Offset: 0x005F8897
		public UndergroundBiome()
		{
			base.NameKey = "NormalUnderground";
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x005FA6AA File Offset: 0x005F88AA
		public override bool IsInBiome(Player player)
		{
			return player.ShoppingZone_BelowSurface;
		}
	}
}
