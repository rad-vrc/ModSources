using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200046D RID: 1133
	[ChatCommand("Death")]
	public class DeathCommand : IChatCommand
	{
		// Token: 0x06002D5E RID: 11614 RVA: 0x005BE5EC File Offset: 0x005BC7EC
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			NetworkText text2 = NetworkText.FromKey("LegacyMultiplayer.23", new object[]
			{
				Main.player[(int)clientId].name,
				Main.player[(int)clientId].numberOfDeathsPVE
			});
			if (Main.player[(int)clientId].numberOfDeathsPVE == 1)
			{
				text2 = NetworkText.FromKey("LegacyMultiplayer.25", new object[]
				{
					Main.player[(int)clientId].name,
					Main.player[(int)clientId].numberOfDeathsPVE
				});
			}
			ChatHelper.BroadcastChatMessage(text2, DeathCommand.RESPONSE_COLOR, -1);
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400513C RID: 20796
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
