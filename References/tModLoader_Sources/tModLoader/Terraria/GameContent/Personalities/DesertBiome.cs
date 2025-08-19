using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B6 RID: 1462
	public class DesertBiome : AShoppingBiome
	{
		// Token: 0x060042A9 RID: 17065 RVA: 0x005FA110 File Offset: 0x005F8310
		public DesertBiome()
		{
			base.NameKey = "Desert";
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x005FA123 File Offset: 0x005F8323
		public override bool IsInBiome(Player player)
		{
			return player.ZoneDesert;
		}
	}
}
