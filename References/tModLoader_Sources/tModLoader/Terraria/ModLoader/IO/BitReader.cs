using System;
using System.IO;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200027F RID: 639
	public class BitReader
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x005235E3 File Offset: 0x005217E3
		// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x005235EB File Offset: 0x005217EB
		public int MaxBits { get; private set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x005235F4 File Offset: 0x005217F4
		// (set) Token: 0x06002BB5 RID: 11189 RVA: 0x005235FC File Offset: 0x005217FC
		public int BitsRead { get; private set; }

		// Token: 0x06002BB6 RID: 11190 RVA: 0x00523608 File Offset: 0x00521808
		public BitReader(BinaryReader reader)
		{
			this.MaxBits = reader.Read7BitEncodedInt();
			int byteCount = this.MaxBits / 8;
			if (this.MaxBits % 8 != 0)
			{
				byteCount++;
			}
			this.bytes = reader.ReadBytes(byteCount);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0052364C File Offset: 0x0052184C
		public bool ReadBit()
		{
			if (this.BitsRead >= this.MaxBits)
			{
				throw new IOException("Read overflow while reading compressed bits, more info below");
			}
			int num = (int)this.bytes[this.BitsRead / 8];
			int num2 = 1;
			int bitsRead = this.BitsRead;
			this.BitsRead = bitsRead + 1;
			return (num & num2 << bitsRead % 8) != 0;
		}

		// Token: 0x04001BEA RID: 7146
		private byte[] bytes;
	}
}
