using System;

namespace Terraria
{
	// Token: 0x02000027 RID: 39
	public class LiquidBuffer
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public static void AddBuffer(int x, int y)
		{
			if (LiquidBuffer.numLiquidBuffer >= 49998)
			{
				return;
			}
			if (Main.tile[x, y].checkingLiquid())
			{
				return;
			}
			Main.tile[x, y].checkingLiquid(true);
			Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].x = x;
			Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].y = y;
			LiquidBuffer.numLiquidBuffer++;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public static void DelBuffer(int l)
		{
			LiquidBuffer.numLiquidBuffer--;
			Main.liquidBuffer[l].x = Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].x;
			Main.liquidBuffer[l].y = Main.liquidBuffer[LiquidBuffer.numLiquidBuffer].y;
		}

		// Token: 0x04000154 RID: 340
		public static int numLiquidBuffer;

		// Token: 0x04000155 RID: 341
		public int x;

		// Token: 0x04000156 RID: 342
		public int y;
	}
}
