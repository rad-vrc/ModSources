using System;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000478 RID: 1144
	[ChatCommand("Say")]
	public class SayChatCommand : IChatCommand
	{
		// Token: 0x06002D8D RID: 11661 RVA: 0x005BEDE6 File Offset: 0x005BCFE6
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.BroadcastChatMessageAs(clientId, NetworkText.FromLiteral(text), Main.player[(int)clientId].ChatColor(), -1);
			if (Main.dedServ)
			{
				Console.WriteLine("<{0}> {1}", Main.player[(int)clientId].name, text);
			}
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}
	}
}
