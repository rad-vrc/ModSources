using System;
using System.Collections.Generic;
using System.IO;

namespace Terraria
{
	// Token: 0x02000023 RID: 35
	public struct BitsByte
	{
		// Token: 0x17000066 RID: 102
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

		// Token: 0x060000EF RID: 239 RVA: 0x00006258 File Offset: 0x00004458
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

		// Token: 0x060000F0 RID: 240 RVA: 0x000062B1 File Offset: 0x000044B1
		public void ClearAll()
		{
			this.value = 0;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000062BA File Offset: 0x000044BA
		public void SetAll()
		{
			this.value = byte.MaxValue;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000062C8 File Offset: 0x000044C8
		public void Retrieve(ref bool b0)
		{
			this.Retrieve(ref b0, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006300 File Offset: 0x00004500
		public void Retrieve(ref bool b0, ref bool b1)
		{
			this.Retrieve(ref b0, ref b1, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006334 File Offset: 0x00004534
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006364 File Offset: 0x00004564
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006390 File Offset: 0x00004590
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref BitsByte.Null, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000063BC File Offset: 0x000045BC
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref BitsByte.Null, ref BitsByte.Null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000063E4 File Offset: 0x000045E4
		public void Retrieve(ref bool b0, ref bool b1, ref bool b2, ref bool b3, ref bool b4, ref bool b5, ref bool b6)
		{
			this.Retrieve(ref b0, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref BitsByte.Null);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006408 File Offset: 0x00004608
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

		// Token: 0x060000FA RID: 250 RVA: 0x00006462 File Offset: 0x00004662
		public static implicit operator byte(BitsByte bb)
		{
			return bb.value;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000646C File Offset: 0x0000466C
		public static implicit operator BitsByte(byte b)
		{
			return new BitsByte
			{
				value = b
			};
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000648C File Offset: 0x0000468C
		public static BitsByte[] ComposeBitsBytesChain(bool optimizeLength, params bool[] flags)
		{
			int num = flags.Length;
			int num2 = 0;
			while (num > 0)
			{
				num2++;
				num -= 7;
			}
			BitsByte[] array = new BitsByte[num2];
			int num3 = 0;
			int num4 = 0;
			for (int i = 0; i < flags.Length; i++)
			{
				array[num4][num3] = flags[i];
				num3++;
				if (num3 == 7 && num4 < num2 - 1)
				{
					array[num4][num3] = true;
					num3 = 0;
					num4++;
				}
			}
			if (optimizeLength)
			{
				int num5 = array.Length - 1;
				while (array[num5] == 0 && num5 > 0)
				{
					array[num5 - 1][7] = false;
					num5--;
				}
				Array.Resize<BitsByte>(ref array, num5 + 1);
			}
			return array;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006548 File Offset: 0x00004748
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

		// Token: 0x060000FE RID: 254 RVA: 0x0000657E File Offset: 0x0000477E
		public void Deconstruct(out bool b0)
		{
			b0 = this[0];
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006589 File Offset: 0x00004789
		public void Deconstruct(out bool b0, out bool b1)
		{
			b0 = this[0];
			b1 = this[1];
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000659D File Offset: 0x0000479D
		public void Deconstruct(out bool b0, out bool b1, out bool b2)
		{
			b0 = this[0];
			b1 = this[1];
			b2 = this[2];
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000065BA File Offset: 0x000047BA
		public void Deconstruct(out bool b0, out bool b1, out bool b2, out bool b3)
		{
			b0 = this[0];
			b1 = this[1];
			b2 = this[2];
			b3 = this[3];
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000065E1 File Offset: 0x000047E1
		public void Deconstruct(out bool b0, out bool b1, out bool b2, out bool b3, out bool b4)
		{
			b0 = this[0];
			b1 = this[1];
			b2 = this[2];
			b3 = this[3];
			b4 = this[4];
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006612 File Offset: 0x00004812
		public void Deconstruct(out bool b0, out bool b1, out bool b2, out bool b3, out bool b4, out bool b5)
		{
			b0 = this[0];
			b1 = this[1];
			b2 = this[2];
			b3 = this[3];
			b4 = this[4];
			b5 = this[5];
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006650 File Offset: 0x00004850
		public void Deconstruct(out bool b0, out bool b1, out bool b2, out bool b3, out bool b4, out bool b5, out bool b6)
		{
			b0 = this[0];
			b1 = this[1];
			b2 = this[2];
			b3 = this[3];
			b4 = this[4];
			b5 = this[5];
			b6 = this[6];
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000066A0 File Offset: 0x000048A0
		public void Deconstruct(out bool b0, out bool b1, out bool b2, out bool b3, out bool b4, out bool b5, out bool b6, out bool b7)
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

		// Token: 0x06000106 RID: 262 RVA: 0x000066FC File Offset: 0x000048FC
		public unsafe override string ToString()
		{
			Span<char> characters = new Span<char>(stackalloc byte[(UIntPtr)16], 8);
			for (int i = 0; i < 8; i++)
			{
				*characters[i] = (this[i] ? '1' : '0');
			}
			return new string(characters);
		}

		// Token: 0x04000097 RID: 151
		private static bool Null;

		// Token: 0x04000098 RID: 152
		private byte value;
	}
}
