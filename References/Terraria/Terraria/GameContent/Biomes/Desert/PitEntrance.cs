using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x020002DB RID: 731
	public static class PitEntrance
	{
		// Token: 0x06002302 RID: 8962 RVA: 0x0054B32C File Offset: 0x0054952C
		public static void Place(DesertDescription description)
		{
			int holeRadius = WorldGen.genRand.Next(6, 9);
			Point center = description.CombinedArea.Center;
			center.Y = (int)description.Surface[center.X];
			PitEntrance.PlaceAt(description, center, holeRadius);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0054B378 File Offset: 0x00549578
		private static void PlaceAt(DesertDescription description, Point position, int holeRadius)
		{
			for (int i = -holeRadius - 3; i < holeRadius + 3; i++)
			{
				for (int j = (int)description.Surface[i + position.X]; j <= description.Hive.Top + 10; j++)
				{
					double num = (double)(j - (int)description.Surface[i + position.X]) / (double)(description.Hive.Top - description.Desert.Top);
					num = Utils.Clamp<double>(num, 0.0, 1.0);
					int num2 = (int)(PitEntrance.GetHoleRadiusScaleAt(num) * (double)holeRadius);
					if (Math.Abs(i) < num2)
					{
						Main.tile[i + position.X, j].ClearEverything();
					}
					else if (Math.Abs(i) < num2 + 3 && num > 0.35)
					{
						Main.tile[i + position.X, j].ResetToType(397);
					}
					double num3 = Math.Abs((double)i / (double)holeRadius);
					num3 *= num3;
					if (Math.Abs(i) < num2 + 3 && (double)(j - position.Y) > 15.0 - 3.0 * num3)
					{
						Main.tile[i + position.X, j].wall = 187;
						WorldGen.SquareWallFrame(i + position.X, j - 1, true);
						WorldGen.SquareWallFrame(i + position.X, j, true);
					}
				}
			}
			holeRadius += 4;
			for (int k = -holeRadius; k < holeRadius; k++)
			{
				int num4 = holeRadius - Math.Abs(k);
				num4 = Math.Min(10, num4 * num4);
				for (int l = 0; l < num4; l++)
				{
					Main.tile[k + position.X, l + (int)description.Surface[k + position.X]].ClearEverything();
				}
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0054B570 File Offset: 0x00549770
		private static double GetHoleRadiusScaleAt(double yProgress)
		{
			if (yProgress < 0.6)
			{
				return 1.0;
			}
			return (1.0 - PitEntrance.SmootherStep((yProgress - 0.6) / 0.4)) * 0.5 + 0.5;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0054B5CC File Offset: 0x005497CC
		private static double SmootherStep(double delta)
		{
			delta = Utils.Clamp<double>(delta, 0.0, 1.0);
			return 1.0 - Math.Cos(delta * 3.1415927410125732) * 0.5 - 0.5;
		}
	}
}
