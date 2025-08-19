using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200065B RID: 1627
	public class MahoganyTreeBiome : MicroBiome
	{
		// Token: 0x060046FE RID: 18174 RVA: 0x00637678 File Offset: 0x00635878
		public override bool Place(Point origin, StructureMap structures)
		{
			Point result;
			if (!WorldUtils.Find(new Point(origin.X - 3, origin.Y), Searches.Chain(new Searches.Down(200), new GenCondition[]
			{
				new Conditions.IsSolid().AreaAnd(6, 1)
			}), out result))
			{
				return false;
			}
			Point result2;
			if (!WorldUtils.Find(new Point(result.X, result.Y - 5), Searches.Chain(new Searches.Up(120), new GenCondition[]
			{
				new Conditions.IsSolid().AreaOr(6, 1)
			}), out result2) || result.Y - 5 - result2.Y > 60)
			{
				return false;
			}
			if (result.Y - result2.Y < 30)
			{
				return false;
			}
			if (!structures.CanPlace(new Rectangle(result.X - 30, result.Y - 60, 60, 90), 0))
			{
				return false;
			}
			if (!WorldGen.drunkWorldGen || WorldGen.genRand.Next(50) > 0)
			{
				Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
				WorldUtils.Gen(new Point(result.X - 25, result.Y - 25), new Shapes.Rectangle(50, 50), new Actions.TileScanner(new ushort[]
				{
					0,
					59,
					60,
					147,
					1
				}).Output(dictionary));
				int num = dictionary[0] + dictionary[1];
				int num2 = dictionary[59] + dictionary[60];
				if (dictionary[147] > num2 || num > num2 || num2 < 50)
				{
					return false;
				}
			}
			int num3 = (result.Y - result2.Y - 9) / 5;
			int num4 = num3 * 5;
			int num5 = 0;
			double num6 = GenBase._random.NextDouble() + 1.0;
			double num7 = GenBase._random.NextDouble() + 2.0;
			if (GenBase._random.Next(2) == 0)
			{
				num7 = 0.0 - num7;
			}
			for (int i = 0; i < num3; i++)
			{
				int num8 = (int)(Math.Sin((double)(i + 1) / 12.0 * num6 * 3.1415927410125732) * num7);
				int num9 = (num8 < num5) ? (num8 - num5) : 0;
				WorldUtils.Gen(new Point(result.X + num5 + num9, result.Y - (i + 1) * 5), new Shapes.Rectangle(6 + Math.Abs(num8 - num5), 7), Actions.Chain(new GenAction[]
				{
					new Modifiers.SkipTiles(new ushort[]
					{
						21,
						467,
						226,
						237
					}),
					new Modifiers.SkipWalls(new ushort[]
					{
						87
					}),
					new Actions.RemoveWall(),
					new Actions.SetTile(383, false, true),
					new Actions.SetFrames(false)
				}));
				WorldUtils.Gen(new Point(result.X + num5 + num9 + 2, result.Y - (i + 1) * 5), new Shapes.Rectangle(2 + Math.Abs(num8 - num5), 5), Actions.Chain(new GenAction[]
				{
					new Modifiers.SkipTiles(new ushort[]
					{
						21,
						467,
						226,
						237
					}),
					new Modifiers.SkipWalls(new ushort[]
					{
						87
					}),
					new Actions.ClearTile(true),
					new Actions.PlaceWall(78, true)
				}));
				WorldUtils.Gen(new Point(result.X + num5 + 2, result.Y - i * 5), new Shapes.Rectangle(2, 2), Actions.Chain(new GenAction[]
				{
					new Modifiers.SkipTiles(new ushort[]
					{
						21,
						467,
						226,
						237
					}),
					new Modifiers.SkipWalls(new ushort[]
					{
						87
					}),
					new Actions.ClearTile(true),
					new Actions.PlaceWall(78, true)
				}));
				num5 = num8;
			}
			int num10 = 6;
			if (num7 < 0.0)
			{
				num10 = 0;
			}
			List<Point> list = new List<Point>();
			for (int j = 0; j < 2; j++)
			{
				double num11 = ((double)j + 1.0) / 3.0;
				int num12 = num10 + (int)(Math.Sin((double)num3 * num11 / 12.0 * num6 * 3.1415927410125732) * num7);
				double num13 = GenBase._random.NextDouble() * 0.7853981852531433 - 0.7853981852531433 - 0.2;
				if (num10 == 0)
				{
					num13 -= 1.5707963705062866;
				}
				WorldUtils.Gen(new Point(result.X + num12, result.Y - (int)((double)(num3 * 5) * num11)), new ShapeBranch(num13, (double)GenBase._random.Next(12, 16)).OutputEndpoints(list), Actions.Chain(new GenAction[]
				{
					new Modifiers.SkipTiles(new ushort[]
					{
						21,
						467,
						226,
						237
					}),
					new Modifiers.SkipWalls(new ushort[]
					{
						87
					}),
					new Actions.SetTile(383, false, true),
					new Actions.SetFrames(true)
				}));
				num10 = 6 - num10;
			}
			int num14 = (int)(Math.Sin((double)num3 / 12.0 * num6 * 3.1415927410125732) * num7);
			WorldUtils.Gen(new Point(result.X + 6 + num14, result.Y - num4), new ShapeBranch(-0.6853981852531433, (double)GenBase._random.Next(16, 22)).OutputEndpoints(list), Actions.Chain(new GenAction[]
			{
				new Modifiers.SkipTiles(new ushort[]
				{
					21,
					467,
					226,
					237
				}),
				new Modifiers.SkipWalls(new ushort[]
				{
					87
				}),
				new Actions.SetTile(383, false, true),
				new Actions.SetFrames(true)
			}));
			WorldUtils.Gen(new Point(result.X + num14, result.Y - num4), new ShapeBranch(-2.45619455575943, (double)GenBase._random.Next(16, 22)).OutputEndpoints(list), Actions.Chain(new GenAction[]
			{
				new Modifiers.SkipTiles(new ushort[]
				{
					21,
					467,
					226,
					237
				}),
				new Modifiers.SkipWalls(new ushort[]
				{
					87
				}),
				new Actions.SetTile(383, false, true),
				new Actions.SetFrames(true)
			}));
			foreach (Point origin2 in list)
			{
				WorldUtils.Gen(origin2, new Shapes.Circle(4), Actions.Chain(new GenAction[]
				{
					new Modifiers.Blotches(4, 2, 0.3),
					new Modifiers.SkipTiles(new ushort[]
					{
						383,
						21,
						467,
						226,
						237
					}),
					new Modifiers.SkipWalls(new ushort[]
					{
						78,
						87
					}),
					new Actions.SetTile(384, false, true),
					new Actions.SetFrames(true)
				}));
			}
			for (int k = 0; k < 4; k++)
			{
				double angle = (double)k / 3.0 * 2.0 + 0.57075;
				WorldUtils.Gen(result, new ShapeRoot(angle, (double)GenBase._random.Next(40, 60), 4.0, 1.0), Actions.Chain(new GenAction[]
				{
					new Modifiers.SkipTiles(new ushort[]
					{
						21,
						467,
						226,
						237
					}),
					new Modifiers.SkipWalls(new ushort[]
					{
						87
					}),
					new Actions.SetTile(383, true, true)
				}));
			}
			WorldGen.AddBuriedChest(result.X + 3, result.Y - 1, (GenBase._random.Next(4) != 0) ? WorldGen.GetNextJungleChestItem() : 0, false, 10, false, 0);
			structures.AddProtectedStructure(new Rectangle(result.X - 30, result.Y - 30, 60, 60), 0);
			return true;
		}
	}
}
