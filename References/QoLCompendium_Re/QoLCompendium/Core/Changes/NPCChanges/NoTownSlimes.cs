using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000243 RID: 579
	public class NoTownSlimes : GlobalNPC
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0006D8CC File Offset: 0x0006BACC
		public override void AI(NPC npc)
		{
			if (QoLCompendium.mainConfig.NoTownSlimes && npc.active && Common.TownSlimeIDs.Contains(npc.type))
			{
				npc.timeLeft = 0;
				npc.active = false;
			}
		}
	}
}
