using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Chat
{
	// Token: 0x02000468 RID: 1128
	public static class ChatHelper
	{
		// Token: 0x06002D3E RID: 11582 RVA: 0x005BE23C File Offset: 0x005BC43C
		public static void DisplayMessageOnClient(NetworkText text, Color color, int playerId)
		{
			if (Main.dedServ)
			{
				NetPacket packet = NetTextModule.SerializeServerMessage(text, color, byte.MaxValue);
				NetManager.Instance.SendToClient(packet, playerId);
				return;
			}
			ChatHelper.DisplayMessage(text, color, byte.MaxValue);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x005BE276 File Offset: 0x005BC476
		public static void SendChatMessageToClient(NetworkText text, Color color, int playerId)
		{
			ChatHelper.SendChatMessageToClientAs(byte.MaxValue, text, color, playerId);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x005BE288 File Offset: 0x005BC488
		public static void SendChatMessageToClientAs(byte messageAuthor, NetworkText text, Color color, int playerId)
		{
			if (Main.dedServ)
			{
				NetPacket packet = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
				NetManager.Instance.SendToClient(packet, playerId);
			}
			if (playerId == Main.myPlayer)
			{
				ChatHelper.DisplayMessage(text, color, messageAuthor);
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x005BE2C1 File Offset: 0x005BC4C1
		public static void BroadcastChatMessage(NetworkText text, Color color, int excludedPlayer = -1)
		{
			ChatHelper.BroadcastChatMessageAs(byte.MaxValue, text, color, excludedPlayer);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x005BE2D0 File Offset: 0x005BC4D0
		public static void BroadcastChatMessageAs(byte messageAuthor, NetworkText text, Color color, int excludedPlayer = -1)
		{
			if (Main.dedServ)
			{
				NetPacket packet = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
				NetManager.Instance.Broadcast(packet, new NetManager.BroadcastCondition(ChatHelper.OnlySendToPlayersWhoAreLoggedIn), excludedPlayer);
				return;
			}
			if (excludedPlayer != Main.myPlayer)
			{
				ChatHelper.DisplayMessage(text, color, messageAuthor);
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x005BE316 File Offset: 0x005BC516
		public static bool OnlySendToPlayersWhoAreLoggedIn(int clientIndex)
		{
			return Netplay.Clients[clientIndex].State == 10;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x005BE328 File Offset: 0x005BC528
		public static void SendChatMessageFromClient(ChatMessage message)
		{
			if (!message.IsConsumed)
			{
				NetPacket packet = NetTextModule.SerializeClientMessage(message);
				NetManager.Instance.SendToServer(packet);
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x005BE350 File Offset: 0x005BC550
		public static void DisplayMessage(NetworkText text, Color color, byte messageAuthor)
		{
			string text2 = text.ToString();
			if (messageAuthor < 255)
			{
				Main.player[(int)messageAuthor].chatOverhead.NewMessage(text2, Main.PlayerOverheadChatMessageDisplayTime);
				Main.player[(int)messageAuthor].chatOverhead.color = color;
				text2 = NameTagHandler.GenerateTag(Main.player[(int)messageAuthor].name) + " " + text2;
			}
			if (ChatHelper.ShouldCacheMessage())
			{
				ChatHelper.CacheMessage(text2, color);
				return;
			}
			Main.NewTextMultiline(text2, false, color, -1);
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x005BE3CA File Offset: 0x005BC5CA
		private static void CacheMessage(string message, Color color)
		{
			ChatHelper._cachedMessages.Add(new Tuple<string, Color>(message, color));
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x005BE3E0 File Offset: 0x005BC5E0
		public static void ShowCachedMessages()
		{
			List<Tuple<string, Color>> cachedMessages = ChatHelper._cachedMessages;
			lock (cachedMessages)
			{
				foreach (Tuple<string, Color> tuple in ChatHelper._cachedMessages)
				{
					Main.NewTextMultiline(tuple.Item1, false, tuple.Item2, -1);
				}
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x005BE464 File Offset: 0x005BC664
		public static void ClearDelayedMessagesCache()
		{
			ChatHelper._cachedMessages.Clear();
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x005BE470 File Offset: 0x005BC670
		private static bool ShouldCacheMessage()
		{
			return Main.netMode == 1 && Main.gameMenu;
		}

		// Token: 0x04005137 RID: 20791
		private static List<Tuple<string, Color>> _cachedMessages = new List<Tuple<string, Color>>();
	}
}
