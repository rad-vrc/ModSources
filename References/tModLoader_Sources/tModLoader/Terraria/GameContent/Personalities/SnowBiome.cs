using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005C3 RID: 1475
	public class SnowBiome : AShoppingBiome
	{
		// Token: 0x060042C3 RID: 17091 RVA: 0x005FA67C File Offset: 0x005F887C
		public SnowBiome()
		{
			base.NameKey = "Snow";
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x005FA68F File Offset: 0x005F888F
		public override bool IsInBiome(Player player)
		{
			return player.ZoneSnow;
		}
	}
}
