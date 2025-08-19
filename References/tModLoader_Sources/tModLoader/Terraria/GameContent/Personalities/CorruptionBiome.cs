using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B4 RID: 1460
	public class CorruptionBiome : AShoppingBiome
	{
		// Token: 0x060042A5 RID: 17061 RVA: 0x005FA0DA File Offset: 0x005F82DA
		public CorruptionBiome()
		{
			base.NameKey = "Corruption";
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x005FA0ED File Offset: 0x005F82ED
		public override bool IsInBiome(Player player)
		{
			return player.ZoneCorrupt;
		}
	}
}
