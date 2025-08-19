using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000755 RID: 1877
	[ChatCommand("Playing")]
	public class ListPlayersCommand : IChatCommand
	{
		// Token: 0x06004BFC RID: 19452 RVA: 0x0066C8CC File Offset: 0x0066AACC
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(string.Join(", ", from player in Main.player
			where player.active
			select player.name)), ListPlayersCommand.RESPONSE_COLOR, (int)clientId);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0066C940 File Offset: 0x0066AB40
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x040060A4 RID: 24740
		private static readonly Color RESPONSE_COLOR = new Color(255, 240, 20);
	}
}
