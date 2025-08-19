using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000756 RID: 1878
	[ChatCommand("Party")]
	public class PartyChatCommand : IChatCommand
	{
		// Token: 0x06004C00 RID: 19456 RVA: 0x0066C964 File Offset: 0x0066AB64
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			int team = Main.player[(int)clientId].team;
			Color color = Main.teamColor[team];
			if (team == 0)
			{
				this.SendNoTeamError(clientId);
				return;
			}
			if (text == "")
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].team == team)
				{
					ChatHelper.SendChatMessageToClientAs(clientId, NetworkText.FromLiteral(text), color, i);
				}
			}
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0066C9D0 File Offset: 0x0066ABD0
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0066C9D2 File Offset: 0x0066ABD2
		private void SendNoTeamError(byte clientId)
		{
			ChatHelper.SendChatMessageToClient(Lang.mp[10].ToNetworkText(), PartyChatCommand.ERROR_COLOR, (int)clientId);
		}

		// Token: 0x040060A5 RID: 24741
		private static readonly Color ERROR_COLOR = new Color(255, 240, 20);
	}
}
