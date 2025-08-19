using System;
using System.Text;

namespace Terraria.Utilities
{
	// Token: 0x02000093 RID: 147
	public struct Vertical64BitStrips
	{
		// Token: 0x17000252 RID: 594
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

		// Token: 0x06001495 RID: 5269 RVA: 0x004A2AE6 File Offset: 0x004A0CE6
		public Vertical64BitStrips(int len)
		{
			this.arr = new Bits64[len];
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x004A2AF4 File Offset: 0x004A0CF4
		public void Clear()
		{
			Array.Clear(this.arr, 0, this.arr.Length);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x004A2B0C File Offset: 0x004A0D0C
		public void Expand3x3()
		{
			for (int i = 0; i < this.arr.Length - 1; i++)
			{
				Bits64[] array = this.arr;
				int num2 = i;
				array[num2] |= this.arr[i + 1];
			}
			for (int num = this.arr.Length - 1; num > 0; num--)
			{
				Bits64[] array2 = this.arr;
				int num3 = num;
				array2[num3] |= this.arr[num - 1];
			}
			for (int j = 0; j < this.arr.Length; j++)
			{
				Bits64 bits = this.arr[j];
				this.arr[j] = (bits << 1 | bits | bits >> 1);
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x004A2BF8 File Offset: 0x004A0DF8
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

		// Token: 0x040010B5 RID: 4277
		private Bits64[] arr;
	}
}
