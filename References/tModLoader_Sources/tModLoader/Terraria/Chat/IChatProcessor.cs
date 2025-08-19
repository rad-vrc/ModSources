using System;

namespace Terraria.Chat
{
	// Token: 0x0200074C RID: 1868
	public interface IChatProcessor
	{
		// Token: 0x06004BDB RID: 19419
		void ProcessIncomingMessage(ChatMessage message, int clientId);

		// Token: 0x06004BDC RID: 19420
		ChatMessage CreateOutgoingMessage(string text);
	}
}
