using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000476 RID: 1142
	[ChatCommand("Party")]
	public class PartyChatCommand : IChatCommand
	{
		// Token: 0x06002D84 RID: 11652 RVA: 0x005BECD0 File Offset: 0x005BCED0
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

		// Token: 0x06002D85 RID: 11653 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x005BED3C File Offset: 0x005BCF3C
		private void SendNoTeamError(byte clientId)
		{
			ChatHelper.SendChatMessageToClient(Lang.mp[10].ToNetworkText(), PartyChatCommand.ERROR_COLOR, (int)clientId);
		}

		// Token: 0x04005145 RID: 20805
		private static readonly Color ERROR_COLOR = new Color(255, 240, 20);
	}
}
