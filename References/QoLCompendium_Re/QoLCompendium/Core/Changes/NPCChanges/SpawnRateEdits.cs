using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x0200023A RID: 570
	public class SpawnRateEdits : GlobalNPC
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0006CFE4 File Offset: 0x0006B1E4
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (SpawnRateEdits.AnyBossAlive() && player.Distance(Main.npc[SpawnRateEdits.boss].Center) < 6000f && QoLCompendium.mainConfig.NoSpawnsDuringBosses)
			{
				maxSpawns = 0;
			}
			if (player.GetModPlayer<QoLCPlayer>().noSpawns)
			{
				maxSpawns = 0;
			}
			if (player.GetModPlayer<QoLCPlayer>().increasedSpawns)
			{
				spawnRate = (int)((double)spawnRate * 0.1);
				maxSpawns = (int)((float)maxSpawns * 10f);
			}
			if (player.GetModPlayer<QoLCPlayer>().decreasedSpawns)
			{
				spawnRate = (int)((double)spawnRate * 2.5);
				maxSpawns = (int)((float)maxSpawns * 0.3f);
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0006D086 File Offset: 0x0006B286
		public override bool PreAI(NPC npc)
		{
			if (npc.boss)
			{
				SpawnRateEdits.boss = npc.whoAmI;
			}
			return true;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0006D09C File Offset: 0x0006B29C
		public static bool AnyBossAlive()
		{
			if (SpawnRateEdits.boss == -1)
			{
				return false;
			}
			NPC npc = Main.npc[SpawnRateEdits.boss];
			if (npc.active && npc.type != 395 && (npc.boss || npc.type == 13))
			{
				return true;
			}
			SpawnRateEdits.boss = -1;
			return false;
		}

		// Token: 0x040005A7 RID: 1447
		public static int boss = -1;
	}
}
