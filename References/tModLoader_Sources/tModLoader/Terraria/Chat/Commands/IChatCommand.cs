using System;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000754 RID: 1876
	public interface IChatCommand
	{
		// Token: 0x06004BFA RID: 19450
		void ProcessIncomingMessage(string text, byte clientId);

		// Token: 0x06004BFB RID: 19451
		void ProcessOutgoingMessage(ChatMessage message);
	}
}
