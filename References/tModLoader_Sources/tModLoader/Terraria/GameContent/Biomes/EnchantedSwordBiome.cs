using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000656 RID: 1622
	public class EnchantedSwordBiome : MicroBiome
	{
		// Token: 0x060046DD RID: 18141 RVA: 0x00634754 File Offset: 0x00632954
		public override bool Place(Point origin, StructureMap structures)
		{
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			WorldUtils.Gen(new Point(origin.X - 25, origin.Y - 25), new Shapes.Rectangle(50, 50), new Actions.TileScanner(new ushort[]
			{
				0,
				1
			}).Output(dictionary));
			if (dictionary[0] + dictionary[1] < 1250)
			{
				return false;
			}
			Point result;
			bool flag = WorldUtils.Find(origin, Searches.Chain(new Searches.Up(1000), new GenCondition[]
			{
				new Conditions.IsSolid().AreaOr(1, 50).Not()
			}), out result);
			Point point3;
			if (WorldUtils.Find(origin, Searches.Chain(new Searches.Up(origin.Y - result.Y), new GenCondition[]
			{
				new Conditions.IsTile(new ushort[]
				{
					53
				})
			}), out point3))
			{
				return false;
			}
			if (!flag)
			{
				return false;
			}
			result.Y += 50;
			ShapeData shapeData = new ShapeData();
			ShapeData shapeData2 = new ShapeData();
			Point point;
			point..ctor(origin.X, origin.Y + 20);
			Point point2;
			point2..ctor(origin.X, origin.Y + 30);
			bool[] array = new bool[TileID.Sets.GeneralPlacementTiles.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TileID.Sets.GeneralPlacementTiles[i];
			}
			array[21] = false;
			array[467] = false;
			double num = 0.8 + GenBase._random.NextDouble() * 0.5;
			if (!structures.CanPlace(new Rectangle(point.X - (int)(20.0 * num), point.Y - 20, (int)(40.0 * num), 40), array, 0))
			{
				return false;
			}
			if (!structures.CanPlace(new Rectangle(origin.X, result.Y + 10, 1, origin.Y - result.Y - 9), array, 2))
			{
				return false;
			}
			WorldUtils.Gen(point, new Shapes.Slime(20, num, 1.0), Actions.Chain(new GenAction[]
			{
				new Modifiers.Blotches(2, 0.4),
				new Actions.ClearTile(true).Output(shapeData)
			}));
			WorldUtils.Gen(point2, new Shapes.Mound(14, 14), Actions.Chain(new GenAction[]
			{
				new Modifiers.Blotches(2, 1, 0.8),
				new Actions.SetTile(0, false, true),
				new Actions.SetFrames(true).Output(shapeData2)
			}));
			shapeData.Subtract(shapeData2, point, point2);
			WorldUtils.Gen(point, new ModShapes.InnerOutline(shapeData, true), Actions.Chain(new GenAction[]
			{
				new Actions.SetTile(2, false, true),
				new Actions.SetFrames(true)
			}));
			WorldUtils.Gen(point, new ModShapes.All(shapeData), Actions.Chain(new GenAction[]
			{
				new Modifiers.RectangleMask(-40, 40, 0, 40),
				new Modifiers.IsEmpty(),
				new Actions.SetLiquid(0, byte.MaxValue)
			}));
			WorldUtils.Gen(point, new ModShapes.All(shapeData), Actions.Chain(new GenAction[]
			{
				new Actions.PlaceWall(68, true),
				new Modifiers.OnlyTiles(new ushort[]
				{
					2
				}),
				new Modifiers.Offset(0, 1),
				new ActionVines(3, 5, 382)
			}));
			if (GenBase._random.NextDouble() <= this._chanceOfEntrance || WorldGen.tenthAnniversaryWorldGen)
			{
				ShapeData data = new ShapeData();
				WorldUtils.Gen(new Point(origin.X, result.Y + 10), new Shapes.Rectangle(1, origin.Y - result.Y - 9), Actions.Chain(new GenAction[]
				{
					new Modifiers.Blotches(2, 0.2),
					new Modifiers.SkipTiles(new ushort[]
					{
						191,
						192
					}),
					new Actions.ClearTile(false).Output(data),
					new Modifiers.Expand(1),
					new Modifiers.OnlyTiles(new ushort[]
					{
						53
					}),
					new Actions.SetTile(397, false, true).Output(data)
				}));
				WorldUtils.Gen(new Point(origin.X, result.Y + 10), new ModShapes.All(data), new Actions.SetFrames(true));
			}
			if (GenBase._random.NextDouble() <= this._chanceOfRealSword)
			{
				WorldGen.PlaceTile(point2.X, point2.Y - 15, 187, true, false, -1, 17);
			}
			else
			{
				WorldGen.PlaceTile(point2.X, point2.Y - 15, 186, true, false, -1, 15);
			}
			WorldUtils.Gen(point2, new ModShapes.All(shapeData2), Actions.Chain(new GenAction[]
			{
				new Modifiers.Offset(0, -1),
				new Modifiers.OnlyTiles(new ushort[]
				{
					2
				}),
				new Modifiers.Offset(0, -1),
				new ActionGrass()
			}));
			structures.AddProtectedStructure(new Rectangle(point.X - (int)(20.0 * num), point.Y - 20, (int)(40.0 * num), 40), 10);
			return true;
		}

		// Token: 0x04005BA3 RID: 23459
		[JsonProperty("ChanceOfEntrance")]
		private double _chanceOfEntrance;

		// Token: 0x04005BA4 RID: 23460
		[JsonProperty("ChanceOfRealSword")]
		private double _chanceOfRealSword;
	}
}
