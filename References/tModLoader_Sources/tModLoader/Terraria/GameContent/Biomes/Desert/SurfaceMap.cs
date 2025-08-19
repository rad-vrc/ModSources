using System;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x02000667 RID: 1639
	public class SurfaceMap
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x0063B1D4 File Offset: 0x006393D4
		public int Width
		{
			get
			{
				return this._heights.Length;
			}
		}

		// Token: 0x170007A5 RID: 1957
		public short this[int absoluteX]
		{
			get
			{
				return this._heights[absoluteX - this.X];
			}
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x0063B1F0 File Offset: 0x006393F0
		private SurfaceMap(short[] heights, int x)
		{
			this._heights = heights;
			this.X = x;
			int num = 0;
			int num2 = int.MaxValue;
			int num3 = 0;
			for (int i = 0; i < heights.Length; i++)
			{
				num3 += (int)heights[i];
				num = Math.Max(num, (int)heights[i]);
				num2 = Math.Min(num2, (int)heights[i]);
			}
			if ((double)num > Main.worldSurface - 10.0)
			{
				num = (int)Main.worldSurface - 10;
			}
			this.Bottom = num;
			this.Top = num2;
			this.Average = (double)num3 / (double)this._heights.Length;
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x0063B280 File Offset: 0x00639480
		public unsafe static SurfaceMap FromArea(int startX, int width)
		{
			int num = Main.maxTilesY / 2;
			short[] array = new short[width];
			for (int i = startX; i < startX + width; i++)
			{
				bool flag = false;
				int num2 = 0;
				for (int j = 50; j < 50 + num; j++)
				{
					if (Main.tile[i, j].active())
					{
						if (*Main.tile[i, j].type == 189 || *Main.tile[i, j].type == 196 || *Main.tile[i, j].type == 460)
						{
							flag = false;
						}
						else if (!flag)
						{
							num2 = j;
							flag = true;
						}
					}
					if (!flag)
					{
						num2 = num + 50;
					}
				}
				array[i - startX] = (short)num2;
			}
			return new SurfaceMap(array, startX);
		}

		// Token: 0x04005BB7 RID: 23479
		public readonly double Average;

		// Token: 0x04005BB8 RID: 23480
		public readonly int Bottom;

		// Token: 0x04005BB9 RID: 23481
		public readonly int Top;

		// Token: 0x04005BBA RID: 23482
		public readonly int X;

		// Token: 0x04005BBB RID: 23483
		private readonly short[] _heights;
	}
}
