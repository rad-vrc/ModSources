using System;

namespace Terraria
{
	// Token: 0x02000038 RID: 56
	public class LiquidBuffer
	{
		// Token: 0x06000366 RID: 870 RVA: 0x00091710 File Offset: 0x0008F910
		public static void AddBuffer(int x, int y)
		{
			if (LiquidBuffer.numLiquidBuffer < 49998 && !Main.tile[x, y].checkingLiquid())
			{
				Main.tile[x, y].checkingLiquid(true);
				Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].x = x;
				Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].y = y;
				LiquidBuffer.numLiquidBuffer++;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00091784 File Offset: 0x0008F984
		public static void DelBuffer(int l)
		{
			LiquidBuffer.numLiquidBuffer--;
			Main.liquidBuffer[l].x = Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].x;
			Main.liquidBuffer[l].y = Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].y;
		}

		// Token: 0x040002F1 RID: 753
		public static int numLiquidBuffer;

		// Token: 0x040002F2 RID: 754
		public int x;

		// Token: 0x040002F3 RID: 755
		public int y;
	}
}
