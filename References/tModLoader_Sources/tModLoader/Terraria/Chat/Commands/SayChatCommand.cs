using System;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200075A RID: 1882
	[ChatCommand("Say")]
	public class SayChatCommand : IChatCommand
	{
		// Token: 0x06004C10 RID: 19472 RVA: 0x0066CBC0 File Offset: 0x0066ADC0
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.BroadcastChatMessageAs(clientId, NetworkText.FromLiteral(text), Main.player[(int)clientId].ChatColor(), -1);
			if (Main.dedServ)
			{
				Console.WriteLine("<{0}> {1}", Main.player[(int)clientId].name, text);
			}
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0066CBF9 File Offset: 0x0066ADF9
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}
	}
}
