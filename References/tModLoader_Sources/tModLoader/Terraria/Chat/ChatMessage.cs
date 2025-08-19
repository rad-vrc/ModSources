using System;
using System.IO;
using System.Text;
using Terraria.Chat.Commands;

namespace Terraria.Chat
{
	// Token: 0x0200074B RID: 1867
	public sealed class ChatMessage
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0066C209 File Offset: 0x0066A409
		// (set) Token: 0x06004BCE RID: 19406 RVA: 0x0066C211 File Offset: 0x0066A411
		public ChatCommandId CommandId { get; private set; }

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x0066C21A File Offset: 0x0066A41A
		// (set) Token: 0x06004BD0 RID: 19408 RVA: 0x0066C222 File Offset: 0x0066A422
		public string Text { get; set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x0066C22B File Offset: 0x0066A42B
		// (set) Token: 0x06004BD2 RID: 19410 RVA: 0x0066C233 File Offset: 0x0066A433
		public bool IsConsumed { get; private set; }

		// Token: 0x06004BD3 RID: 19411 RVA: 0x0066C23C File Offset: 0x0066A43C
		public ChatMessage(string message)
		{
			this.CommandId = ChatCommandId.FromType<SayChatCommand>();
			this.Text = message;
			this.IsConsumed = false;
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x0066C25D File Offset: 0x0066A45D
		private ChatMessage(string message, ChatCommandId commandId)
		{
			this.CommandId = commandId;
			this.Text = message;
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0066C274 File Offset: 0x0066A474
		public void Serialize(BinaryWriter writer)
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId.Serialize(writer);
			writer.Write(this.Text);
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0066C2B0 File Offset: 0x0066A4B0
		public int GetMaxSerializedSize()
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			return this.CommandId.GetMaxSerializedSize() + (4 + Encoding.UTF8.GetByteCount(this.Text));
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0066C2F4 File Offset: 0x0066A4F4
		public static ChatMessage Deserialize(BinaryReader reader)
		{
			ChatCommandId commandId = ChatCommandId.Deserialize(reader);
			return new ChatMessage(reader.ReadString(), commandId);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0066C314 File Offset: 0x0066A514
		public void SetCommand(ChatCommandId commandId)
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId = commandId;
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x0066C330 File Offset: 0x0066A530
		public void SetCommand<T>() where T : IChatCommand
		{
			if (this.IsConsumed)
			{
				throw new InvalidOperationException("Message has already been consumed.");
			}
			this.CommandId = ChatCommandId.FromType<T>();
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x0066C350 File Offset: 0x0066A550
		public void Consume()
		{
			this.IsConsumed = true;
		}
	}
}
