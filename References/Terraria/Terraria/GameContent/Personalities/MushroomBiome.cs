using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003CF RID: 975
	public class MushroomBiome : AShoppingBiome
	{
		// Token: 0x06002A91 RID: 10897 RVA: 0x00599B6F File Offset: 0x00597D6F
		public MushroomBiome()
		{
			base.NameKey = "Mushroom";
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00599B82 File Offset: 0x00597D82
		public override bool IsInBiome(Player player)
		{
			return player.ZoneGlowshroom;
		}
	}
}
