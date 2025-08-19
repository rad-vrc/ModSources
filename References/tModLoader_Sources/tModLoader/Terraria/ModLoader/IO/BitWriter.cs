using System;
using System.Collections.Generic;
using System.IO;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000280 RID: 640
	public class BitWriter
	{
		// Token: 0x06002BB8 RID: 11192 RVA: 0x005236A0 File Offset: 0x005218A0
		public void WriteBit(bool b)
		{
			if (b)
			{
				this.cur |= (byte)(1 << this.i);
			}
			int num = this.i + 1;
			this.i = num;
			if (num == 8)
			{
				this.bytes.Add(this.cur);
				this.cur = 0;
				this.i = 0;
			}
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00523700 File Offset: 0x00521900
		public void Flush(BinaryWriter w)
		{
			w.Write7BitEncodedInt(this.bytes.Count * 8 + this.i);
			if (this.i > 0)
			{
				this.bytes.Add(this.cur);
			}
			foreach (byte b in this.bytes)
			{
				w.Write(b);
			}
		}

		// Token: 0x04001BED RID: 7149
		private List<byte> bytes = new List<byte>();

		// Token: 0x04001BEE RID: 7150
		private byte cur;

		// Token: 0x04001BEF RID: 7151
		private int i;
	}
}
