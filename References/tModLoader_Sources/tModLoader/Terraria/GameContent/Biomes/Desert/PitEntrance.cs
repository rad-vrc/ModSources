using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x02000665 RID: 1637
	public static class PitEntrance
	{
		// Token: 0x0600472E RID: 18222 RVA: 0x0063AC04 File Offset: 0x00638E04
		public static void Place(DesertDescription description)
		{
			int holeRadius = WorldGen.genRand.Next(6, 9);
			Point center = description.CombinedArea.Center;
			center.Y = (int)description.Surface[center.X];
			PitEntrance.PlaceAt(description, center, holeRadius);
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x0063AC50 File Offset: 0x00638E50
		private unsafe static void PlaceAt(DesertDescription description, Point position, int holeRadius)
		{
			for (int i = -holeRadius - 3; i < holeRadius + 3; i++)
			{
				for (int j = (int)description.Surface[i + position.X]; j <= description.Hive.Top + 10; j++)
				{
					double value = (double)(j - (int)description.Surface[i + position.X]) / (double)(description.Hive.Top - description.Desert.Top);
					value = Utils.Clamp<double>(value, 0.0, 1.0);
					int num = (int)(PitEntrance.GetHoleRadiusScaleAt(value) * (double)holeRadius);
					if (Math.Abs(i) < num)
					{
						Main.tile[i + position.X, j].ClearEverything();
					}
					else if (Math.Abs(i) < num + 3 && value > 0.35)
					{
						Main.tile[i + position.X, j].ResetToType(397);
					}
					double num2 = Math.Abs((double)i / (double)holeRadius);
					num2 *= num2;
					if (Math.Abs(i) < num + 3 && (double)(j - position.Y) > 15.0 - 3.0 * num2)
					{
						*Main.tile[i + position.X, j].wall = 187;
						WorldGen.SquareWallFrame(i + position.X, j - 1, true);
						WorldGen.SquareWallFrame(i + position.X, j, true);
					}
				}
			}
			holeRadius += 4;
			for (int k = -holeRadius; k < holeRadius; k++)
			{
				int num3 = holeRadius - Math.Abs(k);
				num3 = Math.Min(10, num3 * num3);
				for (int l = 0; l < num3; l++)
				{
					Main.tile[k + position.X, l + (int)description.Surface[k + position.X]].ClearEverything();
				}
			}
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x0063AE58 File Offset: 0x00639058
		private static double GetHoleRadiusScaleAt(double yProgress)
		{
			if (yProgress < 0.6)
			{
				return 1.0;
			}
			return (1.0 - PitEntrance.SmootherStep((yProgress - 0.6) / 0.4)) * 0.5 + 0.5;
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x0063AEB4 File Offset: 0x006390B4
		private static double SmootherStep(double delta)
		{
			delta = Utils.Clamp<double>(delta, 0.0, 1.0);
			return 1.0 - Math.Cos(delta * 3.1415927410125732) * 0.5 - 0.5;
		}
	}
}
