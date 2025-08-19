using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003C8 RID: 968
	public class OceanBiome : AShoppingBiome
	{
		// Token: 0x06002A83 RID: 10883 RVA: 0x00599AB2 File Offset: 0x00597CB2
		public OceanBiome()
		{
			base.NameKey = "Ocean";
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00599AC5 File Offset: 0x00597CC5
		public override bool IsInBiome(Player player)
		{
			return player.ZoneBeach;
		}
	}
}
