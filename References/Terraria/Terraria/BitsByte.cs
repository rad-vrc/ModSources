using System;
using System.Collections.Generic;
using System.IO;

namespace Terraria
{
	// Token: 0x02000032 RID: 50
	public struct BitsByte
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0003EB40 File Offset: 0x0003CD40
		public BitsByte(bool b1 = false, bool b2 = false, bool b3 = false, bool b4 = false, bool b5 = false, bool b6 = false, bool b7 = false, bool b8 = false)
		{
			this.value = 0;
			this[0] = b1;
			this[1] = b2;
			this[2] = b3;
			this[3] = b4;
			this[4] = b5;
			this[5] = b6;
			this[6] = b7;
			this[7] = b8;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0003EB99 File Offset: 0x0003CD99
		public void ClearAll()
		{
			this.value = 0;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0003EBA2 File Offset: 0x0003CDA2
		public void SetAll()
		{
			this.value = byte.MaxValue;
		}

		// Token: 0x170000AC RID: 172
		public bool this[int key]
		{
			get
			{
				return ((int)this.value & 1 << key) != 0;
			}
			set
			{
				if (value)
				{
					this.value |= (byte)(1 << key);
					return;
				}
				this.value &= (byte)(~(byte)(1 << key));
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0003EBF4 File Offset: 0x0003CDF4
		public void Retrieve(ref bool b0)
		{
			this.Retrieve(ref b0, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		public void Retrieve(ref bool b0, ref bool b1)
		{
			this.Retrieve(ref b0, ref b1, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0003EC60 File Offset: 0x0003CE60
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0003EC90 File Offset: 0x0003CE90
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0003ECBC File Offset: 0x0003CEBC
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0003ECE8 File Offset: 0x0003CEE8
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0003ED10 File Offset: 0x0003CF10
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref BitsByte.Null);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0003ED34 File Offset: 0x0003CF34
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6, ref bool b7)
		{
			b0 = this[0];
			b1 = this[1];
			b2 = this[2];
			b3 = this[3];
			b4 = this[4];
			b5 = this[5];
			b6 = this[6];
			b7 = this[7];
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0003ED8E File Offset: 0x0003CF8E
		public static implicit operator byte(BitsByte bb)
		{
			return bb.value;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0003ED98 File Offset: 0x0003CF98
		public static implicit operator BitsByte(byte b)
		{
			return new BitsByte
			{
				value = b
			};
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0003EDB8 File Offset: 0x0003CFB8
		public static BitsByte[] ComposeBitsBytesChain(bool optimizeLength, params bool[] flags)
		{
			int i = flags.Length;
			int num = 0;
			while (i > 0)
			{
				num++;
				i -= 7;
			}
			BitsByte[] array = new BitsByte[num];
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < flags.Length; j++)
			{
				array[num3][num2] = flags[j];
				num2++;
				if (num2 == 7 && num3 < num - 1)
				{
					array[num3][num2] = true;
					num2 = 0;
					num3++;
				}
			}
			if (optimizeLength)
			{
				int num4 = array.Length - 1;
				while (array[num4] == 0 && num4 > 0)
				{
					array[num4 - 1][7] = false;
					num4--;
				}
				Array.Resize<BitsByte>(ref array, num4 + 1);
			}
			return array;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0003EE74 File Offset: 0x0003D074
		public static BitsByte[] DecomposeBitsBytesChain(BinaryReader reader)
		{
			List<BitsByte> list = new List<BitsByte>();
			BitsByte item;
			do
			{
				item = reader.ReadByte();
				list.Add(item);
			}
			while (item[7]);
			return list.ToArray();
		}

		// Token: 0x04000226 RID: 550
		private static bool Null;

		// Token: 0x04000227 RID: 551
		private byte value;
	}
}
