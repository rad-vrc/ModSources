using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003D0 RID: 976
	public class DungeonBiome : AShoppingBiome
	{
		// Token: 0x06002A93 RID: 10899 RVA: 0x00599B8A File Offset: 0x00597D8A
		public DungeonBiome()
		{
			base.NameKey = "Dungeon";
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00599B9D File Offset: 0x00597D9D
		public override bool IsInBiome(Player player)
		{
			return player.ZoneDungeon;
		}
	}
}
