using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000655 RID: 1621
	public class DunesBiome : MicroBiome
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x006341D8 File Offset: 0x006323D8
		public int MaximumWidth
		{
			get
			{
				return this._singleDunesWidth.ScaledMaximum * 2;
			}
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x006341E8 File Offset: 0x006323E8
		public override bool Place(Point origin, StructureMap structures)
		{
			int height = (int)((double)GenBase._random.Next(60, 100) * this._heightScale);
			int height2 = (int)((double)GenBase._random.Next(60, 100) * this._heightScale);
			int random = this._singleDunesWidth.GetRandom(GenBase._random);
			int random2 = this._singleDunesWidth.GetRandom(GenBase._random);
			DunesBiome.DunesDescription description = DunesBiome.DunesDescription.CreateFromPlacement(new Point(origin.X - random / 2 + 30, origin.Y), random, height);
			DunesBiome.DunesDescription description2 = DunesBiome.DunesDescription.CreateFromPlacement(new Point(origin.X + random2 / 2 - 30, origin.Y), random2, height2);
			this.PlaceSingle(description, structures);
			this.PlaceSingle(description2, structures);
			return true;
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x0063429C File Offset: 0x0063249C
		private void PlaceSingle(DunesBiome.DunesDescription description, StructureMap structures)
		{
			int num = GenBase._random.Next(3) + 8;
			for (int i = 0; i < num - 1; i++)
			{
				int num2 = (int)(2.0 / (double)num * (double)description.Area.Width);
				int num3 = (int)((double)i / (double)num * (double)description.Area.Width + (double)description.Area.Left) + num2 * 2 / 5;
				num3 += GenBase._random.Next(-5, 6);
				double num4 = (double)i / (double)(num - 2);
				double num5 = 1.0 - Math.Abs(num4 - 0.5) * 2.0;
				DunesBiome.PlaceHill(num3 - num2 / 2, num3 + num2 / 2, (num5 * 0.3 + 0.2) * this._heightScale, description);
			}
			int num6 = GenBase._random.Next(2) + 1;
			for (int j = 0; j < num6; j++)
			{
				int num7 = description.Area.Width / 2;
				int x = description.Area.Center.X;
				x += GenBase._random.Next(-10, 11);
				DunesBiome.PlaceHill(x - num7 / 2, x + num7 / 2, 0.8 * this._heightScale, description);
			}
			structures.AddStructure(description.Area, 20);
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x0063440C File Offset: 0x0063260C
		private static void PlaceHill(int startX, int endX, double scale, DunesBiome.DunesDescription description)
		{
			Point startPoint;
			startPoint..ctor(startX, (int)description.Surface[startX]);
			Point endPoint;
			endPoint..ctor(endX, (int)description.Surface[endX]);
			Point point;
			point..ctor((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2 - (int)(35.0 * scale));
			int num = (endPoint.X - point.X) / 4;
			int minValue = (endPoint.X - point.X) / 16;
			if (description.WindDirection == DunesBiome.WindDirection.Left)
			{
				point.X -= WorldGen.genRand.Next(minValue, num + 1);
			}
			else
			{
				point.X += WorldGen.genRand.Next(minValue, num + 1);
			}
			Point point2;
			point2..ctor(0, (int)(scale * 12.0));
			Point point3;
			point3..ctor(point2.X / -2, point2.Y / -2);
			DunesBiome.PlaceCurvedLine(startPoint, point, (description.WindDirection != DunesBiome.WindDirection.Left) ? point3 : point2, description);
			DunesBiome.PlaceCurvedLine(point, endPoint, (description.WindDirection == DunesBiome.WindDirection.Left) ? point3 : point2, description);
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00634530 File Offset: 0x00632730
		private unsafe static void PlaceCurvedLine(Point startPoint, Point endPoint, Point anchorOffset, DunesBiome.DunesDescription description)
		{
			Point p;
			p..ctor((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
			p.X += anchorOffset.X;
			p.Y += anchorOffset.Y;
			Vector2D value = startPoint.ToVector2D();
			Vector2D value2 = endPoint.ToVector2D();
			Vector2D vector2D = p.ToVector2D();
			double num = 0.5 / (value2.X - value.X);
			Point point;
			point..ctor(-1, -1);
			for (double num2 = 0.0; num2 <= 1.0; num2 += num)
			{
				Vector2D vector2D2 = Vector2D.Lerp(value, vector2D, num2);
				Vector2D value3 = Vector2D.Lerp(vector2D, value2, num2);
				Point point2 = Vector2D.Lerp(vector2D2, value3, num2).ToPoint();
				if (!(point2 == point))
				{
					point = point2;
					int num3 = description.Area.Width / 2 - Math.Abs(point2.X - description.Area.Center.X);
					int num4 = (int)description.Surface[point2.X] + (int)(Math.Sqrt((double)num3) * 3.0);
					for (int i = point2.Y - 10; i < point2.Y; i++)
					{
						if (GenBase._tiles[point2.X, i].active() && *GenBase._tiles[point2.X, i].type != 53)
						{
							GenBase._tiles[point2.X, i].ClearEverything();
						}
					}
					for (int j = point2.Y; j < num4; j++)
					{
						GenBase._tiles[point2.X, j].ResetToType(53);
						Tile.SmoothSlope(point2.X, j, true, false);
					}
				}
			}
		}

		// Token: 0x04005BA1 RID: 23457
		[JsonProperty("SingleDunesWidth")]
		private WorldGenRange _singleDunesWidth = WorldGenRange.Empty;

		// Token: 0x04005BA2 RID: 23458
		[JsonProperty("HeightScale")]
		private double _heightScale = 1.0;

		// Token: 0x02000D0E RID: 3342
		private class DunesDescription
		{
			// Token: 0x1700098B RID: 2443
			// (get) Token: 0x060062EC RID: 25324 RVA: 0x006D7756 File Offset: 0x006D5956
			// (set) Token: 0x060062ED RID: 25325 RVA: 0x006D775E File Offset: 0x006D595E
			public bool IsValid { get; private set; }

			// Token: 0x1700098C RID: 2444
			// (get) Token: 0x060062EE RID: 25326 RVA: 0x006D7767 File Offset: 0x006D5967
			// (set) Token: 0x060062EF RID: 25327 RVA: 0x006D776F File Offset: 0x006D596F
			public SurfaceMap Surface { get; private set; }

			// Token: 0x1700098D RID: 2445
			// (get) Token: 0x060062F0 RID: 25328 RVA: 0x006D7778 File Offset: 0x006D5978
			// (set) Token: 0x060062F1 RID: 25329 RVA: 0x006D7780 File Offset: 0x006D5980
			public Rectangle Area { get; private set; }

			// Token: 0x1700098E RID: 2446
			// (get) Token: 0x060062F2 RID: 25330 RVA: 0x006D7789 File Offset: 0x006D5989
			// (set) Token: 0x060062F3 RID: 25331 RVA: 0x006D7791 File Offset: 0x006D5991
			public DunesBiome.WindDirection WindDirection { get; private set; }

			// Token: 0x060062F4 RID: 25332 RVA: 0x006D779A File Offset: 0x006D599A
			private DunesDescription()
			{
			}

			// Token: 0x060062F5 RID: 25333 RVA: 0x006D77A4 File Offset: 0x006D59A4
			public static DunesBiome.DunesDescription CreateFromPlacement(Point origin, int width, int height)
			{
				Rectangle area;
				area..ctor(origin.X - width / 2, origin.Y - height / 2, width, height);
				return new DunesBiome.DunesDescription
				{
					Area = area,
					IsValid = true,
					Surface = SurfaceMap.FromArea(area.Left - 20, area.Width + 40),
					WindDirection = ((WorldGen.genRand.Next(2) != 0) ? DunesBiome.WindDirection.Right : DunesBiome.WindDirection.Left)
				};
			}
		}

		// Token: 0x02000D0F RID: 3343
		private enum WindDirection
		{
			// Token: 0x04007AB5 RID: 31413
			Left,
			// Token: 0x04007AB6 RID: 31414
			Right
		}
	}
}
