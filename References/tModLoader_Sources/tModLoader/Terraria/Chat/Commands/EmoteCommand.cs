using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000752 RID: 1874
	[ChatCommand("Emote")]
	public class EmoteCommand : IChatCommand
	{
		// Token: 0x06004BF0 RID: 19440 RVA: 0x0066C6E0 File Offset: 0x0066A8E0
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			if (text != "")
			{
				text = "*" + Main.player[(int)clientId].name + " " + text;
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), EmoteCommand.RESPONSE_COLOR, -1);
			}
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x0066C71E File Offset: 0x0066A91E
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x040060A2 RID: 24738
		private static readonly Color RESPONSE_COLOR = new Color(200, 100, 0);
	}
}
