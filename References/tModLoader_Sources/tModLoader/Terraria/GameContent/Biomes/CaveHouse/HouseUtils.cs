using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x0200066D RID: 1645
	public static class HouseUtils
	{
		// Token: 0x06004773 RID: 18291 RVA: 0x0063D038 File Offset: 0x0063B238
		public static HouseBuilder CreateBuilder(Point origin, StructureMap structures)
		{
			List<Rectangle> list = HouseUtils.CreateRooms(origin);
			if (list.Count == 0 || !HouseUtils.AreRoomLocationsValid(list))
			{
				return HouseBuilder.Invalid;
			}
			HouseType houseType = HouseUtils.GetHouseType(list);
			if (!HouseUtils.AreRoomsValid(list, structures, houseType))
			{
				return HouseBuilder.Invalid;
			}
			switch (houseType)
			{
			case HouseType.Wood:
				return new WoodHouseBuilder(list);
			case HouseType.Ice:
				return new IceHouseBuilder(list);
			case HouseType.Desert:
				return new DesertHouseBuilder(list);
			case HouseType.Jungle:
				return new JungleHouseBuilder(list);
			case HouseType.Mushroom:
				return new MushroomHouseBuilder(list);
			case HouseType.Granite:
				return new GraniteHouseBuilder(list);
			case HouseType.Marble:
				return new MarbleHouseBuilder(list);
			default:
				return new WoodHouseBuilder(list);
			}
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x0063D0D4 File Offset: 0x0063B2D4
		private static List<Rectangle> CreateRooms(Point origin)
		{
			Point result;
			if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(200), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out result) || result == origin)
			{
				return new List<Rectangle>();
			}
			Rectangle item = HouseUtils.FindRoom(result);
			Rectangle rectangle = HouseUtils.FindRoom(new Point(item.Center.X, item.Y + 1));
			Rectangle rectangle2 = HouseUtils.FindRoom(new Point(item.Center.X, item.Y + item.Height + 10));
			rectangle2.Y = item.Y + item.Height - 1;
			double roomSolidPrecentage = HouseUtils.GetRoomSolidPrecentage(rectangle);
			double roomSolidPrecentage2 = HouseUtils.GetRoomSolidPrecentage(rectangle2);
			item.Y += 3;
			rectangle.Y += 3;
			rectangle2.Y += 3;
			List<Rectangle> list = new List<Rectangle>();
			if (WorldGen.genRand.NextDouble() > roomSolidPrecentage + 0.2)
			{
				list.Add(rectangle);
			}
			list.Add(item);
			if (WorldGen.genRand.NextDouble() > roomSolidPrecentage2 + 0.2)
			{
				list.Add(rectangle2);
			}
			return list;
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x0063D200 File Offset: 0x0063B400
		private static Rectangle FindRoom(Point origin)
		{
			Point result;
			bool flag = WorldUtils.Find(origin, Searches.Chain(new Searches.Left(25), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out result);
			Point result2;
			bool flag3 = WorldUtils.Find(origin, Searches.Chain(new Searches.Right(25), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out result2);
			if (!flag)
			{
				result..ctor(origin.X - 25, origin.Y);
			}
			if (!flag3)
			{
				result2..ctor(origin.X + 25, origin.Y);
			}
			Rectangle result3;
			result3..ctor(origin.X, origin.Y, 0, 0);
			if (origin.X - result.X > result2.X - origin.X)
			{
				result3.X = result.X;
				result3.Width = Utils.Clamp<int>(result2.X - result.X, 15, 30);
			}
			else
			{
				result3.Width = Utils.Clamp<int>(result2.X - result.X, 15, 30);
				result3.X = result2.X - result3.Width;
			}
			Point result4;
			bool flag2 = WorldUtils.Find(result, Searches.Chain(new Searches.Up(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out result4);
			Point result5;
			bool flag4 = WorldUtils.Find(result2, Searches.Chain(new Searches.Up(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out result5);
			if (!flag2)
			{
				result4..ctor(origin.X, origin.Y - 10);
			}
			if (!flag4)
			{
				result5..ctor(origin.X, origin.Y - 10);
			}
			result3.Height = Utils.Clamp<int>(Math.Max(origin.Y - result4.Y, origin.Y - result5.Y), 8, 12);
			result3.Y -= result3.Height;
			return result3;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x0063D3CC File Offset: 0x0063B5CC
		private static double GetRoomSolidPrecentage(Rectangle room)
		{
			double num = (double)(room.Width * room.Height);
			Ref<int> @ref = new Ref<int>(0);
			WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
			{
				new Modifiers.IsSolid(),
				new Actions.Count(@ref)
			}));
			return (double)@ref.Value / num;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x0063D43C File Offset: 0x0063B63C
		private static int SortBiomeResults(Tuple<HouseType, int> item1, Tuple<HouseType, int> item2)
		{
			return item2.Item2.CompareTo(item1.Item2);
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x0063D460 File Offset: 0x0063B660
		private static bool AreRoomLocationsValid(IEnumerable<Rectangle> rooms)
		{
			foreach (Rectangle room in rooms)
			{
				if (room.Y + room.Height > Main.maxTilesY - 220)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x0063D4C4 File Offset: 0x0063B6C4
		private static HouseType GetHouseType(IEnumerable<Rectangle> rooms)
		{
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			foreach (Rectangle room in rooms)
			{
				WorldUtils.Gen(new Point(room.X - 10, room.Y - 10), new Shapes.Rectangle(room.Width + 20, room.Height + 20), new Actions.TileScanner(new ushort[]
				{
					0,
					59,
					147,
					1,
					161,
					53,
					396,
					397,
					368,
					367,
					60,
					70
				}).Output(dictionary));
			}
			List<Tuple<HouseType, int>> list = new List<Tuple<HouseType, int>>();
			list.Add(Tuple.Create<HouseType, int>(HouseType.Wood, dictionary[0] + dictionary[1]));
			list.Add(Tuple.Create<HouseType, int>(HouseType.Jungle, dictionary[59] + dictionary[60] * 10));
			list.Add(Tuple.Create<HouseType, int>(HouseType.Mushroom, dictionary[59] + dictionary[70] * 10));
			list.Add(Tuple.Create<HouseType, int>(HouseType.Ice, dictionary[147] + dictionary[161]));
			list.Add(Tuple.Create<HouseType, int>(HouseType.Desert, dictionary[397] + dictionary[396] + dictionary[53]));
			list.Add(Tuple.Create<HouseType, int>(HouseType.Granite, dictionary[368]));
			list.Add(Tuple.Create<HouseType, int>(HouseType.Marble, dictionary[367]));
			Comparison<Tuple<HouseType, int>> comparison;
			if ((comparison = HouseUtils.<>O.<0>__SortBiomeResults) == null)
			{
				comparison = (HouseUtils.<>O.<0>__SortBiomeResults = new Comparison<Tuple<HouseType, int>>(HouseUtils.SortBiomeResults));
			}
			list.Sort(comparison);
			return list[0].Item1;
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x0063D668 File Offset: 0x0063B868
		private static bool AreRoomsValid(IEnumerable<Rectangle> rooms, StructureMap structures, HouseType style)
		{
			foreach (Rectangle room in rooms)
			{
				Point point;
				if (style != HouseType.Granite && WorldUtils.Find(new Point(room.X - 2, room.Y - 2), Searches.Chain(new Searches.Rectangle(room.Width + 4, room.Height + 4).RequireAll(false), new GenCondition[]
				{
					new Conditions.HasLava()
				}), out point))
				{
					return false;
				}
				if (WorldGen.notTheBees)
				{
					if (!structures.CanPlace(room, HouseUtils.BeelistedTiles, 5))
					{
						return false;
					}
				}
				else if (!structures.CanPlace(room, HouseUtils.BlacklistedTiles, 5))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04005BDA RID: 23514
		internal static bool[] BlacklistedTiles = TileID.Sets.Factory.CreateBoolSet(true, new int[]
		{
			225,
			41,
			43,
			44,
			226,
			203,
			112,
			25,
			151,
			21,
			467
		});

		// Token: 0x04005BDB RID: 23515
		internal static bool[] BeelistedTiles = TileID.Sets.Factory.CreateBoolSet(true, new int[]
		{
			41,
			43,
			44,
			226,
			203,
			112,
			25,
			151,
			21,
			467
		});

		// Token: 0x02000D1D RID: 3357
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007AD6 RID: 31446
			public static Comparison<Tuple<HouseType, int>> <0>__SortBiomeResults;
		}
	}
}
