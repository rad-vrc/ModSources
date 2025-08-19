using System;
using Terraria.GameContent.UI;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000472 RID: 1138
	[ChatCommand("RPS")]
	public class RockPaperScissorsCommand : IChatCommand
	{
		// Token: 0x06002D73 RID: 11635 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessIncomingMessage(string text, byte clientId)
		{
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x005BE9F0 File Offset: 0x005BCBF0
		public void ProcessOutgoingMessage(ChatMessage message)
		{
			int num = Main.rand.NextFromList(new int[]
			{
				37,
				38,
				36
			});
			if (Main.netMode == 0)
			{
				EmoteBubble.NewBubble(num, new WorldUIAnchor(Main.LocalPlayer), 360);
				EmoteBubble.CheckForNPCsToReactToEmoteBubble(num, Main.LocalPlayer);
			}
			else
			{
				NetMessage.SendData(120, -1, -1, null, Main.myPlayer, (float)num, 0f, 0f, 0, 0, 0);
			}
			message.Consume();
		}
	}
}
