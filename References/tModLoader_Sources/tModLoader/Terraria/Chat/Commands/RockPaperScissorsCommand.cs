using System;
using Terraria.GameContent.UI;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000758 RID: 1880
	[ChatCommand("RPS")]
	public class RockPaperScissorsCommand : IChatCommand
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x0066CABB File Offset: 0x0066ACBB
		public void ProcessIncomingMessage(string text, byte clientId)
		{
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0066CAC0 File Offset: 0x0066ACC0
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
