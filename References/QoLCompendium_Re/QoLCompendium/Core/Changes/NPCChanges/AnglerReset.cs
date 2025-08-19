using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000245 RID: 581
	public class AnglerReset : GlobalNPC
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0006D9B0 File Offset: 0x0006BBB0
		public override void OnChatButtonClicked(NPC npc, bool firstButton)
		{
			if (QoLCompendium.mainConfig.AnglerQuestInstantReset && Main.anglerQuestFinished)
			{
				if (Main.netMode == 0)
				{
					Main.AnglerQuestSwap();
					return;
				}
				if (Main.netMode == 1)
				{
					ModPacket packet = base.Mod.GetPacket(256);
					packet.Write(3);
					packet.Send(-1, -1);
				}
			}
		}
	}
}
