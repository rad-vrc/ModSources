using System;
using System.IO;
using System.Text;
using ReLogic.Utilities;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
	// Token: 0x02000466 RID: 1126
	public struct ChatCommandId
	{
		// Token: 0x06002D30 RID: 11568 RVA: 0x005BDE71 File Offset: 0x005BC071
		private ChatCommandId(string name)
		{
			this._name = name;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x005BDE7C File Offset: 0x005BC07C
		public static ChatCommandId FromType<T>() where T : IChatCommand
		{
			ChatCommandAttribute cacheableAttribute = AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>();
			if (cacheableAttribute != null)
			{
				return new ChatCommandId(cacheableAttribute.Name);
			}
			return new ChatCommandId(null);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x005BDEA4 File Offset: 0x005BC0A4
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this._name ?? "");
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x005BDEBB File Offset: 0x005BC0BB
		public static ChatCommandId Deserialize(BinaryReader reader)
		{
			return new ChatCommandId(reader.ReadString());
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x005BDEC8 File Offset: 0x005BC0C8
		public int GetMaxSerializedSize()
		{
			return 4 + Encoding.UTF8.GetByteCount(this._name ?? "");
		}

		// Token: 0x04005132 RID: 20786
		private readonly string _name;
	}
}
