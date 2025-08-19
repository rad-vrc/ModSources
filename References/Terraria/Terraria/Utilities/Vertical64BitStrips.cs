using System;
using System.Text;

namespace Terraria.Utilities
{
	// Token: 0x02000140 RID: 320
	public struct Vertical64BitStrips
	{
		// Token: 0x060018BA RID: 6330 RVA: 0x004DEE0A File Offset: 0x004DD00A
		public Vertical64BitStrips(int len)
		{
			this.arr = new Bits64[len];
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x004DEE18 File Offset: 0x004DD018
		public void Clear()
		{
			Array.Clear(this.arr, 0, this.arr.Length);
		}

		// Token: 0x1700026E RID: 622
		public Bits64 this[int x]
		{
			get
			{
				return this.arr[x];
			}
			set
			{
				this.arr[x] = value;
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x004DEE4C File Offset: 0x004DD04C
		public void Expand3x3()
		{
			for (int i = 0; i < this.arr.Length - 1; i++)
			{
				Bits64[] array = this.arr;
				int num = i;
				array[num] |= this.arr[i + 1];
			}
			for (int j = this.arr.Length - 1; j > 0; j--)
			{
				Bits64[] array2 = this.arr;
				int num2 = j;
				array2[num2] |= this.arr[j - 1];
			}
			for (int k = 0; k < this.arr.Length; k++)
			{
				Bits64 b = this.arr[k];
				this.arr[k] = (b << 1 | b | b >> 1);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x004DEF38 File Offset: 0x004DD138
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.arr.Length * 65);
			for (int i = 0; i < 64; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append('\n');
				}
				for (int j = 0; j < this.arr.Length; j++)
				{
					stringBuilder.Append(this[j][i] ? 'x' : ' ');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400150A RID: 5386
		private Bits64[] arr;
	}
}
