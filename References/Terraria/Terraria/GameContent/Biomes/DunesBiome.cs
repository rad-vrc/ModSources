using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002C9 RID: 713
	public class DunesBiome : MicroBiome
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x00545845 File Offset: 0x00543A45
		public int MaximumWidth
		{
			get
			{
				return this._singleDunesWidth.ScaledMaximum * 2;
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00545854 File Offset: 0x00543A54
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

		// Token: 0x060022A2 RID: 8866 RVA: 0x00545908 File Offset: 0x00543B08
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
				int num8 = description.Area.Center.X;
				num8 += GenBase._random.Next(-10, 11);
				DunesBiome.PlaceHill(num8 - num7 / 2, num8 + num7 / 2, 0.8 * this._heightScale, description);
			}
			structures.AddStructure(description.Area, 20);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00545A78 File Offset: 0x00543C78
		private static void PlaceHill(int startX, int endX, double scale, DunesBiome.DunesDescription description)
		{
			Point point = new Point(startX, (int)description.Surface[startX]);
			Point point2 = new Point(endX, (int)description.Surface[endX]);
			Point point3 = new Point((point.X + point2.X) / 2, (point.Y + point2.Y) / 2 - (int)(35.0 * scale));
			int num = (point2.X - point3.X) / 4;
			int minValue = (point2.X - point3.X) / 16;
			if (description.WindDirection == DunesBiome.WindDirection.Left)
			{
				point3.X -= WorldGen.genRand.Next(minValue, num + 1);
			}
			else
			{
				point3.X += WorldGen.genRand.Next(minValue, num + 1);
			}
			Point point4 = new Point(0, (int)(scale * 12.0));
			Point point5 = new Point(point4.X / -2, point4.Y / -2);
			DunesBiome.PlaceCurvedLine(point, point3, (description.WindDirection != DunesBiome.WindDirection.Left) ? point5 : point4, description);
			DunesBiome.PlaceCurvedLine(point3, point2, (description.WindDirection == DunesBiome.WindDirection.Left) ? point5 : point4, description);
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00545B9C File Offset: 0x00543D9C
		private static void PlaceCurvedLine(Point startPoint, Point endPoint, Point anchorOffset, DunesBiome.DunesDescription description)
		{
			Point p = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
			p.X += anchorOffset.X;
			p.Y += anchorOffset.Y;
			Vector2D vector2D = startPoint.ToVector2D();
			Vector2D vector2D2 = endPoint.ToVector2D();
			Vector2D vector2D3 = p.ToVector2D();
			double num = 0.5 / (vector2D2.X - vector2D.X);
			Point b = new Point(-1, -1);
			for (double num2 = 0.0; num2 <= 1.0; num2 += num)
			{
				Vector2D vector2D4 = Vector2D.Lerp(vector2D, vector2D3, num2);
				Vector2D vector2D5 = Vector2D.Lerp(vector2D3, vector2D2, num2);
				Point point = Vector2D.Lerp(vector2D4, vector2D5, num2).ToPoint();
				if (!(point == b))
				{
					b = point;
					int num3 = description.Area.Width / 2 - Math.Abs(point.X - description.Area.Center.X);
					int num4 = (int)description.Surface[point.X] + (int)(Math.Sqrt((double)num3) * 3.0);
					for (int i = point.Y - 10; i < point.Y; i++)
					{
						if (GenBase._tiles[point.X, i].active() && GenBase._tiles[point.X, i].type != 53)
						{
							GenBase._tiles[point.X, i].ClearEverything();
						}
					}
					for (int j = point.Y; j < num4; j++)
					{
						GenBase._tiles[point.X, j].ResetToType(53);
						Tile.SmoothSlope(point.X, j, true, false);
					}
				}
			}
		}

		// Token: 0x040047DF RID: 18399
		[JsonProperty("SingleDunesWidth")]
		private WorldGenRange _singleDunesWidth = WorldGenRange.Empty;

		// Token: 0x040047E0 RID: 18400
		[JsonProperty("HeightScale")]
		private double _heightScale = 1.0;

		// Token: 0x020006CA RID: 1738
		private class DunesDescription
		{
			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x06003680 RID: 13952 RVA: 0x0060C86B File Offset: 0x0060AA6B
			// (set) Token: 0x06003681 RID: 13953 RVA: 0x0060C873 File Offset: 0x0060AA73
			public bool IsValid { get; private set; }

			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06003682 RID: 13954 RVA: 0x0060C87C File Offset: 0x0060AA7C
			// (set) Token: 0x06003683 RID: 13955 RVA: 0x0060C884 File Offset: 0x0060AA84
			public SurfaceMap Surface { get; private set; }

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06003684 RID: 13956 RVA: 0x0060C88D File Offset: 0x0060AA8D
			// (set) Token: 0x06003685 RID: 13957 RVA: 0x0060C895 File Offset: 0x0060AA95
			public Rectangle Area { get; private set; }

			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x06003686 RID: 13958 RVA: 0x0060C89E File Offset: 0x0060AA9E
			// (set) Token: 0x06003687 RID: 13959 RVA: 0x0060C8A6 File Offset: 0x0060AAA6
			public DunesBiome.WindDirection WindDirection { get; private set; }

			// Token: 0x06003688 RID: 13960 RVA: 0x0000B904 File Offset: 0x00009B04
			private DunesDescription()
			{
			}

			// Token: 0x06003689 RID: 13961 RVA: 0x0060C8B0 File Offset: 0x0060AAB0
			public static DunesBiome.DunesDescription CreateFromPlacement(Point origin, int width, int height)
			{
				Rectangle rectangle = new Rectangle(origin.X - width / 2, origin.Y - height / 2, width, height);
				return new DunesBiome.DunesDescription
				{
					Area = rectangle,
					IsValid = true,
					Surface = SurfaceMap.FromArea(rectangle.Left - 20, rectangle.Width + 40),
					WindDirection = ((WorldGen.genRand.Next(2) == 0) ? DunesBiome.WindDirection.Left : DunesBiome.WindDirection.Right)
				};
			}
		}

		// Token: 0x020006CB RID: 1739
		private enum WindDirection
		{
			// Token: 0x0400622A RID: 25130
			Left,
			// Token: 0x0400622B RID: 25131
			Right
		}
	}
}
