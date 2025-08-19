using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B5 RID: 1461
	public class CrimsonBiome : AShoppingBiome
	{
		// Token: 0x060042A7 RID: 17063 RVA: 0x005FA0F5 File Offset: 0x005F82F5
		public CrimsonBiome()
		{
			base.NameKey = "Crimson";
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x005FA108 File Offset: 0x005F8308
		public override bool IsInBiome(Player player)
		{
			return player.ZoneCrimson;
		}
	}
}
