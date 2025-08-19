using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200074D RID: 1869
	[ChatCommand("AllDeath")]
	public class AllDeathCommand : IChatCommand
	{
		// Token: 0x06004BDD RID: 19421 RVA: 0x0066C35C File Offset: 0x0066A55C
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active)
				{
					NetworkText text2 = NetworkText.FromKey("LegacyMultiplayer.23", new object[]
					{
						player.name,
						player.numberOfDeathsPVE
					});
					if (player.numberOfDeathsPVE == 1)
					{
						text2 = NetworkText.FromKey("LegacyMultiplayer.25", new object[]
						{
							player.name,
							player.numberOfDeathsPVE
						});
					}
					ChatHelper.BroadcastChatMessage(text2, AllDeathCommand.RESPONSE_COLOR, -1);
				}
			}
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0066C3F4 File Offset: 0x0066A5F4
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400609C RID: 24732
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
