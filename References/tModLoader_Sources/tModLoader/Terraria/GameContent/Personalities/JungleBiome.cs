using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005BC RID: 1468
	public class JungleBiome : AShoppingBiome
	{
		// Token: 0x060042B2 RID: 17074 RVA: 0x005FA17C File Offset: 0x005F837C
		public JungleBiome()
		{
			base.NameKey = "Jungle";
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x005FA18F File Offset: 0x005F838F
		public override bool IsInBiome(Player player)
		{
			return player.ZoneJungle;
		}
	}
}
