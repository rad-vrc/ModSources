using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000757 RID: 1879
	[ChatCommand("PVPDeath")]
	public class PVPDeathCommand : IChatCommand
	{
		// Token: 0x06004C05 RID: 19461 RVA: 0x0066CA0C File Offset: 0x0066AC0C
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

		// Token: 0x06004C06 RID: 19462 RVA: 0x0066CA9C File Offset: 0x0066AC9C
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x040060A6 RID: 24742
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
