using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000470 RID: 1136
	[ChatCommand("AllPVPDeath")]
	public class AllPVPDeathCommand : IChatCommand
	{
		// Token: 0x06002D6A RID: 11626 RVA: 0x005BE7EC File Offset: 0x005BC9EC
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

		// Token: 0x06002D6B RID: 11627 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x0400513F RID: 20799
		private static readonly Color RESPONSE_COLOR = new Color(255, 25, 25);
	}
}
