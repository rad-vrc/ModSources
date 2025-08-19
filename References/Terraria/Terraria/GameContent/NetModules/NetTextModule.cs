using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.Net;
using Terraria.UI.Chat;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x02000273 RID: 627
	public class NetTextModule : NetModule
	{
		// Token: 0x06001FC1 RID: 8129 RVA: 0x00516A50 File Offset: 0x00514C50
		public static NetPacket SerializeClientMessage(ChatMessage message)
		{
			NetPacket result = NetModule.CreatePacket<NetTextModule>(message.GetMaxSerializedSize());
			message.Serialize(result.Writer);
			return result;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x00516A77 File Offset: 0x00514C77
		public static NetPacket SerializeServerMessage(NetworkText text, Color color)
		{
			return NetTextModule.SerializeServerMessage(text, color, byte.MaxValue);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x00516A88 File Offset: 0x00514C88
		public static NetPacket SerializeServerMessage(NetworkText text, Color color, byte authorId)
		{
			NetPacket result = NetModule.CreatePacket<NetTextModule>(1 + text.GetMaxSerializedSize() + 3);
			result.Writer.Write(authorId);
			text.Serialize(result.Writer);
			result.Writer.WriteRGB(color);
			return result;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00516AD0 File Offset: 0x00514CD0
		private bool DeserializeAsClient(BinaryReader reader, int senderPlayerId)
		{
			byte messageAuthor = reader.ReadByte();
			NetworkText text = NetworkText.Deserialize(reader);
			Color color = reader.ReadRGB();
			ChatHelper.DisplayMessage(text, color, messageAuthor);
			return true;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00516AFC File Offset: 0x00514CFC
		private bool DeserializeAsServer(BinaryReader reader, int senderPlayerId)
		{
			ChatMessage message = ChatMessage.Deserialize(reader);
			ChatManager.Commands.ProcessIncomingMessage(message, senderPlayerId);
			return true;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00516B1D File Offset: 0x00514D1D
		public override bool Deserialize(BinaryReader reader, int senderPlayerId)
		{
			if (Main.dedServ)
			{
				return this.DeserializeAsServer(reader, senderPlayerId);
			}
			return this.DeserializeAsClient(reader, senderPlayerId);
		}
	}
}
