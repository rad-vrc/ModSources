using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000246 RID: 582
	public class CheckTravelingNPCS : GlobalNPC
	{
		// Token: 0x06000DCB RID: 3531 RVA: 0x0006DA03 File Offset: 0x0006BC03
		public override void OnChatButtonClicked(NPC npc, bool firstButton)
		{
			if (npc.type == 368)
			{
				ModConditions.talkedToTravelingMerchant = true;
			}
			if (npc.type == 453)
			{
				ModConditions.talkedToSkeletonMerchant = true;
			}
		}
	}
}
