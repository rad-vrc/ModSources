using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003CC RID: 972
	public class JungleBiome : AShoppingBiome
	{
		// Token: 0x06002A8B RID: 10891 RVA: 0x00599B1E File Offset: 0x00597D1E
		public JungleBiome()
		{
			base.NameKey = "Jungle";
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00599B31 File Offset: 0x00597D31
		public override bool IsInBiome(Player player)
		{
			return player.ZoneJungle;
		}
	}
}
