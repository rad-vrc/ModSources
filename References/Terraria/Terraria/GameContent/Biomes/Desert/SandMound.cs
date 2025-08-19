using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x020002DC RID: 732
	public static class SandMound
	{
		// Token: 0x06002306 RID: 8966 RVA: 0x0054B624 File Offset: 0x00549824
		public static void Place(DesertDescription description)
		{
			Rectangle desert = description.Desert;
			desert.Height = Math.Min(description.Desert.Height, description.Hive.Height / 2);
			Rectangle desert2 = description.Desert;
			desert2.Y = desert.Bottom;
			desert2.Height = Math.Max(0, description.Desert.Bottom - desert.Bottom);
			SurfaceMap surface = description.Surface;
			int num = 0;
			int num2 = 0;
			for (int i = -5; i < desert.Width + 5; i++)
			{
				double num3 = Math.Abs((double)(i + 5) / (double)(desert.Width + 10)) * 2.0 - 1.0;
				num3 = Utils.Clamp<double>(num3, -1.0, 1.0);
				if (i % 3 == 0)
				{
					num += WorldGen.genRand.Next(-1, 2);
					num = Utils.Clamp<int>(num, -10, 10);
				}
				num2 += WorldGen.genRand.Next(-1, 2);
				num2 = Utils.Clamp<int>(num2, -10, 10);
				double num4 = Math.Sqrt(1.0 - num3 * num3 * num3 * num3);
				int num5 = desert.Bottom - (int)(num4 * (double)desert.Height) + num;
				if (Math.Abs(num3) < 1.0)
				{
					double num6 = Utils.UnclampedSmoothStep(0.5, 0.8, Math.Abs(num3));
					num6 = num6 * num6 * num6;
					int num7 = 10 + (int)((double)desert.Top - num6 * 20.0) + num2;
					num7 = Math.Min(num7, num5);
					for (int j = (int)(surface[i + desert.X] - 1); j < num7; j++)
					{
						int num8 = i + desert.X;
						int num9 = j;
						Main.tile[num8, num9].active(false);
						Main.tile[num8, num9].wall = 0;
					}
				}
				SandMound.PlaceSandColumn(i + desert.X, num5, desert2.Bottom - num5);
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0054B850 File Offset: 0x00549A50
		private static void PlaceSandColumn(int startX, int startY, int height)
		{
			for (int i = startY + height - 1; i >= startY; i--)
			{
				int num = i;
				Tile tile = Main.tile[startX, num];
				if (!WorldGen.remixWorldGen)
				{
					tile.liquid = 0;
				}
				Main.tile[startX, num + 1];
				Main.tile[startX, num + 2];
				tile.type = 53;
				tile.slope(0);
				tile.halfBrick(false);
				tile.active(true);
				if (i < startY)
				{
					tile.active(false);
				}
				WorldGen.SquareWallFrame(startX, num, true);
			}
		}
	}
}
