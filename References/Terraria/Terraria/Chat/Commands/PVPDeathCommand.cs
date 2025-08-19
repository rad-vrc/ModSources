using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200046E RID: 1134
	[ChatCommand("PVPDeath")]
	public class PVPDeathCommand : IChatCommand
	{
		// Token: 0x06002D62 RID: 11618 RVA: 0x005BE694 File Offset: 0x005BC894
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			NetworkText text2 = NetworkText.FromKey("LegacyMultiplayer.24", new object[]
			{
				Main.player[(int)clientId].name,
				Main.player[(int)clientId].numberOfDeathsPVP
			});
			if (Main.player[(int)clientId].numberOfDeathsPVP == 1)
			{
				text2 = NetworkText.FromKey("LegacyMultiplayer.26", new object[]
				{
					Main.player[(int)clientId].name,
					Main.player[(int)clientId].numberOfDeathsPVP
				});
			}
			ChatHelper.BroadcastChatMessage(text2, PVPDeathCommand.RESPONSE_COLOR, -1);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400513D RID: 20797
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
