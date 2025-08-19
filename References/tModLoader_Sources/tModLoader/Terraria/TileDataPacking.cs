using System;

namespace Terraria
{
	// Token: 0x0200005E RID: 94
	public static class TileDataPacking
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x003FD363 File Offset: 0x003FB563
		public static int Unpack(int bits, int offset, int width)
		{
			return bits >> offset & (1 << width) - 1;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x003FD374 File Offset: 0x003FB574
		public static int Pack(int value, int bits, int offset, int width)
		{
			return (bits & ~((1 << width) - 1 << offset)) | (value & (1 << width) - 1) << offset;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x003FD396 File Offset: 0x003FB596
		public static bool GetBit(int bits, int offset)
		{
			return (bits & 1 << offset) != 0;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x003FD3A3 File Offset: 0x003FB5A3
		public static int SetBit(bool value, int bits, int offset)
		{
			if (!value)
			{
				return bits & ~(1 << offset);
			}
			return bits | 1 << offset;
		}
	}
}
