using System;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x020001AB RID: 427
	internal class PlayerCommandCaller : CommandCaller
	{
		// Token: 0x06002066 RID: 8294 RVA: 0x004E2EFA File Offset: 0x004E10FA
		public PlayerCommandCaller(Player player)
		{
			this.Player = player;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x004E2F09 File Offset: 0x004E1109
		public CommandType CommandType
		{
			get
			{
				return CommandType.Server;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x004E2F0C File Offset: 0x004E110C
		public Player Player { get; }

		// Token: 0x06002069 RID: 8297 RVA: 0x004E2F14 File Offset: 0x004E1114
		public void Reply(string text, Color color = default(Color))
		{
			if (color == default(Color))
			{
				color = Color.White;
			}
			string[] array = text.Split('\n', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral(array[i]), color, this.Player.whoAmI);
			}
		}
	}
}
