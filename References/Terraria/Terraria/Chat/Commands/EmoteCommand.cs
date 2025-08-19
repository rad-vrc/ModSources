using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000473 RID: 1139
	[ChatCommand("Emote")]
	public class EmoteCommand : IChatCommand
	{
		// Token: 0x06002D76 RID: 11638 RVA: 0x005BEA67 File Offset: 0x005BCC67
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			if (text != "")
			{
				text = string.Format("*{0} {1}", Main.player[(int)clientId].name, text);
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), EmoteCommand.RESPONSE_COLOR, -1);
			}
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x04005142 RID: 20802
		private static readonly Color RESPONSE_COLOR = new Color(200, 100, 0);
	}
}
