using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003CB RID: 971
	public class DesertBiome : AShoppingBiome
	{
		// Token: 0x06002A89 RID: 10889 RVA: 0x00599B03 File Offset: 0x00597D03
		public DesertBiome()
		{
			base.NameKey = "Desert";
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00599B16 File Offset: 0x00597D16
		public override bool IsInBiome(Player player)
		{
			return player.ZoneDesert;
		}
	}
}
