using System;
using System.IO;
using Terraria.DataStructures;

namespace Terraria.Net
{
	// Token: 0x020000C2 RID: 194
	public struct NetPacket
	{
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x004A2903 File Offset: 0x004A0B03
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x004A290B File Offset: 0x004A0B0B
		public int Length { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x004A2914 File Offset: 0x004A0B14
		public BinaryWriter Writer
		{
			get
			{
				return this.Buffer.Writer;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x004A2921 File Offset: 0x004A0B21
		public BinaryReader Reader
		{
			get
			{
				return this.Buffer.Reader;
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x004A2930 File Offset: 0x004A0B30
		public NetPacket(ushort id, int size)
		{
			this = default(NetPacket);
			this.Id = id;
			this.Buffer = BufferPool.Request(size + 5);
			this.Length = size + 5;
			this.Writer.Write((ushort)(size + 5));
			this.Writer.Write(82);
			this.Writer.Write(id);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x004A298A File Offset: 0x004A0B8A
		public void Recycle()
		{
			this.Buffer.Recycle();
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x004A2998 File Offset: 0x004A0B98
		public void ShrinkToFit()
		{
			if (this.Length == (int)this.Writer.BaseStream.Position)
			{
				return;
			}
			this.Length = (int)this.Writer.BaseStream.Position;
			this.Writer.Seek(0, SeekOrigin.Begin);
			this.Writer.Write((ushort)this.Length);
			this.Writer.Seek(this.Length, SeekOrigin.Begin);
		}

		// Token: 0x040011FC RID: 4604
		private const int HEADER_SIZE = 5;

		// Token: 0x040011FD RID: 4605
		public readonly ushort Id;

		// Token: 0x040011FF RID: 4607
		public readonly CachedBuffer Buffer;
	}
}
