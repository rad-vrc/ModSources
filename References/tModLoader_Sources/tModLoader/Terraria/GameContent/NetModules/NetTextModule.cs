using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;
using Terraria.Net;
using Terraria.UI.Chat;

namespace Terraria.GameContent.NetModules
{
	// Token: 0x020005DC RID: 1500
	public class NetTextModule : NetModule
	{
		// Token: 0x06004314 RID: 17172 RVA: 0x005FC57C File Offset: 0x005FA77C
		public static NetPacket SerializeClientMessage(ChatMessage message)
		{
			NetPacket result = NetModule.CreatePacket<NetTextModule>(message.GetMaxSerializedSize());
			message.Serialize(result.Writer);
			return result;
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x005FC5A3 File Offset: 0x005FA7A3
		public static NetPacket SerializeServerMessage(NetworkText text, Color color)
		{
			return NetTextModule.SerializeServerMessage(text, color, byte.MaxValue);
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x005FC5B4 File Offset: 0x005FA7B4
		public static NetPacket SerializeServerMessage(NetworkText text, Color color, byte authorId)
		{
			NetPacket result = NetModule.CreatePacket<NetTextModule>(1 + text.GetMaxSerializedSize() + 3);
			result.Writer.Write(authorId);
			text.Serialize(result.Writer);
			result.Writer.WriteRGB(color);
			return result;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x005FC5FC File Offset: 0x005FA7FC
		private bool DeserializeAsClient(BinaryReader reader, int senderPlayerId)
		{
			byte messageAuthor = reader.ReadByte();
			NetworkText text = NetworkText.Deserialize(reader);
			Color color = reader.ReadRGB();
			ChatHelper.DisplayMessage(text, color, messageAuthor);
			return true;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x005FC628 File Offset: 0x005FA828
		private bool DeserializeAsServer(BinaryReader reader, int senderPlayerId)
		{
			ChatMessage message = ChatMessage.Deserialize(reader);
			ChatManager.Commands.ProcessIncomingMessage(message, senderPlayerId);
			return true;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x005FC649 File Offset: 0x005FA849
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
