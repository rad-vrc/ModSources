using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005BD RID: 1469
	public class MushroomBiome : AShoppingBiome
	{
		// Token: 0x060042B4 RID: 17076 RVA: 0x005FA197 File Offset: 0x005F8397
		public MushroomBiome()
		{
			base.NameKey = "Mushroom";
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x005FA1AA File Offset: 0x005F83AA
		public override bool IsInBiome(Player player)
		{
			return player.ZoneGlowshroom;
		}
	}
}
