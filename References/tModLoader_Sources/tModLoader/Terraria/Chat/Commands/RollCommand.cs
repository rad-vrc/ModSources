using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
	// Token: 0x02000759 RID: 1881
	[ChatCommand("Roll")]
	public class RollCommand : IChatCommand
	{
		// Token: 0x06004C0C RID: 19468 RVA: 0x0066CB40 File Offset: 0x0066AD40
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

		// Token: 0x06004C0D RID: 19469 RVA: 0x0066CB9E File Offset: 0x0066AD9E
		public void ProcessOutgoingMessage(ChatMessage message)
		{
		}

		// Token: 0x040060A7 RID: 24743
		private static readonly Color RESPONSE_COLOR = new Color(255, 240, 20);
	}
}
