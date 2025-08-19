using System;
using System.IO;
using System.Text;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
	// Token: 0x02000469 RID: 1129
	public sealed class ChatMessage
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x005BE48D File Offset: 0x005BC68D
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x005BE495 File Offset: 0x005BC695
		public ChatCommandId CommandId { get; private set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x005BE49E File Offset: 0x005BC69E
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x005BE4A6 File Offset: 0x005BC6A6
		public string Text { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x005BE4AF File Offset: 0x005BC6AF
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x005BE4B7 File Offset: 0x005BC6B7
		public bool IsConsumed { get; private set; }

		// Token: 0x06002D51 RID: 11601 RVA: 0x005BE4C0 File Offset: 0x005BC6C0
		public ChatMessage(string message)
		{
			this.CommandId = ChatCommandId.FromType<SayChatCommand>();
			this.Text = message;
			this.IsConsumed = false;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x005BE4E1 File Offset: 0x005BC6E1
		private ChatMessage(string message, ChatCommandId commandId)
		{
			this.CommandId = commandId;
			this.Text = message;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x005BE4F8 File Offset: 0x005BC6F8
		public void Serialize(BinaryWriter writer)
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId.Serialize(writer);
			writer.Write(this.Text);
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x005BE534 File Offset: 0x005BC734
		public int GetMaxSerializedSize()
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			return 0 + this.CommandId.GetMaxSerializedSize() + (4 + Encoding.UTF8.GetByteCount(this.Text));
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x005BE578 File Offset: 0x005BC778
		public static ChatMessage Deserialize(BinaryReader reader)
		{
			ChatCommandId commandId = ChatCommandId.Deserialize(reader);
			return new ChatMessage(reader.ReadString(), commandId);
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x005BE598 File Offset: 0x005BC798
		public void SetCommand(ChatCommandId commandId)
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId = commandId;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x005BE5B4 File Offset: 0x005BC7B4
		public void SetCommand<T>() where T : IChatCommand
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId = ChatCommandId.FromType<T>();
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x005BE5D4 File Offset: 0x005BC7D4
		public void Consume()
		{
			this.IsConsumed = true;
		}
	}
}
