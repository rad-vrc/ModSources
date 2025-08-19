using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200046F RID: 1135
	[ChatCommand("AllDeath")]
	public class AllDeathCommand : IChatCommand
	{
		// Token: 0x06002D66 RID: 11622 RVA: 0x005BE73C File Offset: 0x005BC93C
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

		// Token: 0x06002D67 RID: 11623 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400513E RID: 20798
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
