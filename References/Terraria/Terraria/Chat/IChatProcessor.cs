using System;

namespace Terraria.Chat
{
	// Token: 0x0200046A RID: 1130
	public interface IChatProcessor
	{
		// Token: 0x06002D59 RID: 11609
		void ProcessIncomingMessage(ChatMessage message, int clientId);

		// Token: 0x06002D5A RID: 11610
		ChatMessage CreateOutgoingMessage(string text);
	}
}
