using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x0200023C RID: 572
	public class BestiaryUnlock : GlobalNPC
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0006D185 File Offset: 0x0006B385
		public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			if (QoLCompendium.mainConfig.OneKillForBestiaryEntries)
			{
				bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[npc.type], true);
			}
		}
	}
}
