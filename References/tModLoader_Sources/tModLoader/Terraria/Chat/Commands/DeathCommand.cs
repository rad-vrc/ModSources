using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000750 RID: 1872
	[ChatCommand("Death")]
	public class DeathCommand : IChatCommand
	{
		// Token: 0x06004BE6 RID: 19430 RVA: 0x0066C4DC File Offset: 0x0066A6DC
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

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0066C56C File Offset: 0x0066A76C
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400609F RID: 24735
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
