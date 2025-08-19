using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B7 RID: 1463
	public class DungeonBiome : AShoppingBiome
	{
		// Token: 0x060042AB RID: 17067 RVA: 0x005FA12B File Offset: 0x005F832B
		public DungeonBiome()
		{
			base.NameKey = "Dungeon";
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x005FA13E File Offset: 0x005F833E
		public override bool IsInBiome(Player player)
		{
			return player.ZoneDungeon;
		}
	}
}
