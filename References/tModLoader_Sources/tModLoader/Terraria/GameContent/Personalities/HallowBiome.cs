using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B9 RID: 1465
	public class HallowBiome : AShoppingBiome
	{
		// Token: 0x060042AF RID: 17071 RVA: 0x005FA161 File Offset: 0x005F8361
		public HallowBiome()
		{
			base.NameKey = "Hallow";
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x005FA174 File Offset: 0x005F8374
		public override bool IsInBiome(Player player)
		{
			return player.ZoneHallow;
		}
	}
}
