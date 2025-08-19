using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000477 RID: 1143
	[ChatCommand("Roll")]
	public class RollCommand : IChatCommand
	{
		// Token: 0x06002D89 RID: 11657 RVA: 0x005BED70 File Offset: 0x005BCF70
		public void ProcessIncomingMessage(string text, byte clientId)
		{
			int num = Main.rand.Next(1, 101);
			ChatHelper.BroadcastChatMessage(NetworkText.FromFormattable("*{0} {1} {2}", new object[]
			{
				Main.player[(int)clientId].name,
				Lang.mp[9].ToNetworkText(),
				num
			}), RollCommand.RESPONSE_COLOR, -1);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x04005146 RID: 20806
		private static readonly Color RESPONSE_COLOR = new Color(255, 240, 20);
	}
}
