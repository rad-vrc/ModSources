using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003D1 RID: 977
	public class CorruptionBiome : AShoppingBiome
	{
		// Token: 0x06002A95 RID: 10901 RVA: 0x00599BA5 File Offset: 0x00597DA5
		public CorruptionBiome()
		{
			base.NameKey = "Corruption";
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x00599BB8 File Offset: 0x00597DB8
		public override bool IsInBiome(Player player)
		{
			return player.ZoneCorrupt;
		}
	}
}
