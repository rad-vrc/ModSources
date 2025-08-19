using System;
using System.IO;
using System.Text;
using ReLogic.Utilities;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
	// Token: 0x02000748 RID: 1864
	public struct ChatCommandId
	{
		// Token: 0x06004BB2 RID: 19378 RVA: 0x0066BBA8 File Offset: 0x00669DA8
		private ChatCommandId(string name)
		{
			this._name = name;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0066BBB4 File Offset: 0x00669DB4
		public static ChatCommandId FromType<T>() where T : IChatCommand
		{
			ChatCommandAttribute cacheableAttribute = AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>();
			if (cacheableAttribute != null)
			{
				return new ChatCommandId(cacheableAttribute.Name);
			}
			return new ChatCommandId(null);
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0066BBDC File Offset: 0x00669DDC
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this._name ?? "");
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0066BBF3 File Offset: 0x00669DF3
		public static ChatCommandId Deserialize(BinaryReader reader)
		{
			return new ChatCommandId(reader.ReadString());
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0066BC00 File Offset: 0x00669E00
		public int GetMaxSerializedSize()
		{
			return 4 + Encoding.UTF8.GetByteCount(this._name ?? "");
		}

		// Token: 0x04006093 RID: 24723
		internal readonly string _name;
	}
}
