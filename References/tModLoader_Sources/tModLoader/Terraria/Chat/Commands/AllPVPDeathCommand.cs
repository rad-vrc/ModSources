using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x0200074E RID: 1870
	[ChatCommand("AllPVPDeath")]
	public class AllPVPDeathCommand : IChatCommand
	{
		// Token: 0x06004BE1 RID: 19425 RVA: 0x0066C414 File Offset: 0x0066A614
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active)
				{
					NetworkText text2 = NetworkText.FromKey("LegacyMultiplayer.24", new object[]
					{
						player.name,
						player.numberOfDeathsPVP
					});
					if (player.numberOfDeathsPVP == 1)
					{
						text2 = NetworkText.FromKey("LegacyMultiplayer.26", new object[]
						{
							player.name,
							player.numberOfDeathsPVP
						});
					}
					ChatHelper.BroadcastChatMessage(text2, AllPVPDeathCommand.RESPONSE_COLOR, -1);
				}
			}
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0066C4AC File Offset: 0x0066A6AC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400609D RID: 24733
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
