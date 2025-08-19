using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005BF RID: 1471
	public class OceanBiome : AShoppingBiome
	{
		// Token: 0x060042B8 RID: 17080 RVA: 0x005FA1DD File Offset: 0x005F83DD
		public OceanBiome()
		{
			base.NameKey = "Ocean";
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x005FA1F0 File Offset: 0x005F83F0
		public override bool IsInBiome(Player player)
		{
			return player.ZoneBeach;
		}
	}
}
