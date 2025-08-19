using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Chat
{
	/// <summary>
	/// Facilitates sending chat messages from the server to clients.
	/// <para /><see cref="M:Terraria.Chat.ChatHelper.BroadcastChatMessage(Terraria.Localization.NetworkText,Microsoft.Xna.Framework.Color,System.Int32)" /> and <see cref="M:Terraria.Chat.ChatHelper.SendChatMessageToClient(Terraria.Localization.NetworkText,Microsoft.Xna.Framework.Color,System.Int32)" /> are the most commonly used methods provided.
	/// </summary>
	// Token: 0x0200074A RID: 1866
	public static class ChatHelper
	{
		// Token: 0x06004BC0 RID: 19392 RVA: 0x0066BFA8 File Offset: 0x0066A1A8
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

		/// <summary>
		/// Sends a message to the specified client to be displayed in their chat. Intended to be used from the server. Useful for the server replying to client actions. 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="color"></param>
		/// <param name="playerId"></param>
		// Token: 0x06004BC1 RID: 19393 RVA: 0x0066BFE2 File Offset: 0x0066A1E2
		public static void SendChatMessageToClient(NetworkText text, Color color, int playerId)
		{
			ChatHelper.SendChatMessageToClientAs(byte.MaxValue, text, color, playerId);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0066BFF4 File Offset: 0x0066A1F4
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

		/// <summary>
		/// Sends a message to all connected clients to be displayed in chat. Intended to be used from the server.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="color"></param>
		/// <param name="excludedPlayer"></param>
		// Token: 0x06004BC3 RID: 19395 RVA: 0x0066C02D File Offset: 0x0066A22D
		public static void BroadcastChatMessage(NetworkText text, Color color, int excludedPlayer = -1)
		{
			ChatHelper.BroadcastChatMessageAs(byte.MaxValue, text, color, excludedPlayer);
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0066C03C File Offset: 0x0066A23C
		public static void BroadcastChatMessageAs(byte messageAuthor, NetworkText text, Color color, int excludedPlayer = -1)
		{
			if (Main.dedServ)
			{
				NetPacket packet = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
				NetManager instance = NetManager.Instance;
				NetPacket packet2 = packet;
				NetManager.BroadcastCondition conditionToBroadcast;
				if ((conditionToBroadcast = ChatHelper.<>O.<0>__OnlySendToPlayersWhoAreLoggedIn) == null)
				{
					conditionToBroadcast = (ChatHelper.<>O.<0>__OnlySendToPlayersWhoAreLoggedIn = new NetManager.BroadcastCondition(ChatHelper.OnlySendToPlayersWhoAreLoggedIn));
				}
				instance.Broadcast(packet2, conditionToBroadcast, excludedPlayer);
				return;
			}
			if (excludedPlayer != Main.myPlayer)
			{
				ChatHelper.DisplayMessage(text, color, messageAuthor);
			}
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0066C091 File Offset: 0x0066A291
		public static bool OnlySendToPlayersWhoAreLoggedIn(int clientIndex)
		{
			return Netplay.Clients[clientIndex].State == 10;
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0066C0A4 File Offset: 0x0066A2A4
		public static void SendChatMessageFromClient(ChatMessage message)
		{
			if (!message.IsConsumed)
			{
				NetPacket packet = NetTextModule.SerializeClientMessage(message);
				NetManager.Instance.SendToServer(packet);
			}
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0066C0CC File Offset: 0x0066A2CC
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

		// Token: 0x06004BC8 RID: 19400 RVA: 0x0066C146 File Offset: 0x0066A346
		private static void CacheMessage(string message, Color color)
		{
			ChatHelper._cachedMessages.Add(new Tuple<string, Color>(message, color));
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0066C15C File Offset: 0x0066A35C
		public static void ShowCachedMessages()
		{
			List<Tuple<string, Color>> cachedMessages = ChatHelper._cachedMessages;
			lock (cachedMessages)
			{
				foreach (Tuple<string, Color> cachedMessage in ChatHelper._cachedMessages)
				{
					Main.NewTextMultiline(cachedMessage.Item1, false, cachedMessage.Item2, -1);
				}
			}
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0066C1E0 File Offset: 0x0066A3E0
		public static void ClearDelayedMessagesCache()
		{
			ChatHelper._cachedMessages.Clear();
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x0066C1EC File Offset: 0x0066A3EC
		private static bool ShouldCacheMessage()
		{
			return Main.netMode == 1 && Main.gameMenu;
		}

		// Token: 0x04006098 RID: 24728
		private static List<Tuple<string, Color>> _cachedMessages = new List<Tuple<string, Color>>();

		// Token: 0x02000D6B RID: 3435
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B9D RID: 31645
			public static NetManager.BroadcastCondition <0>__OnlySendToPlayersWhoAreLoggedIn;
		}
	}
}
