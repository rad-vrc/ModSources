using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000475 RID: 1141
	[ChatCommand("Playing")]
	public class ListPlayersCommand : IChatCommand
	{
		// Token: 0x06002D80 RID: 11648 RVA: 0x005BEC44 File Offset: 0x005BCE44
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(string.Join(", ", from player in Main.player
			where player.active
			select player.name)), ListPlayersCommand.RESPONSE_COLOR, (int)clientId);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x04005144 RID: 20804
		private static readonly Color RESPONSE_COLOR = new Color(255, 240, 20);
	}
}
