using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003CE RID: 974
	public class HallowBiome : AShoppingBiome
	{
		// Token: 0x06002A8F RID: 10895 RVA: 0x00599B54 File Offset: 0x00597D54
		public HallowBiome()
		{
			base.NameKey = "Hallow";
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00599B67 File Offset: 0x00597D67
		public override bool IsInBiome(Player player)
		{
			return player.ZoneHallow;
		}
	}
}
