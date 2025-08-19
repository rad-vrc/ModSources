using System;
using System.IO;
using Terraria.DataStructures;

namespace Terraria.Net
{
	// Token: 0x02000121 RID: 289
	public struct NetPacket
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x004CAFBD File Offset: 0x004C91BD
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x004CAFC5 File Offset: 0x004C91C5
		public int Length { readonly get; private set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x004CAFCE File Offset: 0x004C91CE
		public BinaryWriter Writer
		{
			get
			{
				return this.Buffer.Writer;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x004CAFDB File Offset: 0x004C91DB
		public BinaryReader Reader
		{
			get
			{
				return this.Buffer.Reader;
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x004CAFE8 File Offset: 0x004C91E8
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

		// Token: 0x06001A31 RID: 6705 RVA: 0x004CB042 File Offset: 0x004C9242
		public void Recycle()
		{
			this.Buffer.Recycle();
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x004CB050 File Offset: 0x004C9250
		public void ShrinkToFit()
		{
			if (this.Length != (int)this.Writer.BaseStream.Position)
			{
				this.Length = (int)this.Writer.BaseStream.Position;
				this.Writer.Seek(0, SeekOrigin.Begin);
				this.Writer.Write((ushort)this.Length);
				this.Writer.Seek(this.Length, SeekOrigin.Begin);
			}
		}

		// Token: 0x0400141A RID: 5146
		private const int HEADER_SIZE = 5;

		// Token: 0x0400141B RID: 5147
		public readonly ushort Id;

		// Token: 0x0400141C RID: 5148
		public readonly CachedBuffer Buffer;
	}
}
