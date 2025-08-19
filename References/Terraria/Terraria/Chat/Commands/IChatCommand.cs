using System;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200046C RID: 1132
	public interface IChatCommand
	{
		// Token: 0x06002D5C RID: 11612
		void ProcessIncomingMessage(string text, byte clientId);

		// Token: 0x06002D5D RID: 11613
		void ProcessOutgoingMessage(ChatMessage message);
	}
}
