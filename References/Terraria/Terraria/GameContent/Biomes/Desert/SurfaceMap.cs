using System;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x020002DD RID: 733
	public class SurfaceMap
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0054B8DA File Offset: 0x00549ADA
		public int Width
		{
			get
			{
				return this._heights.Length;
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0054B8E4 File Offset: 0x00549AE4
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

		// Token: 0x170002D3 RID: 723
		public short this[int absoluteX]
		{
			get
			{
				return this._heights[absoluteX - this.X];
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0054B988 File Offset: 0x00549B88
		public static SurfaceMap FromArea(int startX, int width)
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
						if (Main.tile[i, j].type == 189 || Main.tile[i, j].type == 196 || Main.tile[i, j].type == 460)
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

		// Token: 0x040047FE RID: 18430
		public readonly double Average;

		// Token: 0x040047FF RID: 18431
		public readonly int Bottom;

		// Token: 0x04004800 RID: 18432
		public readonly int Top;

		// Token: 0x04004801 RID: 18433
		public readonly int X;

		// Token: 0x04004802 RID: 18434
		private readonly short[] _heights;
	}
}
