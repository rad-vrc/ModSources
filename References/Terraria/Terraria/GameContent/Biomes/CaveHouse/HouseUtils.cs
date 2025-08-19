using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x020002DE RID: 734
	public static class HouseUtils
	{
		// Token: 0x0600230C RID: 8972 RVA: 0x0054BA58 File Offset: 0x00549C58
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

		// Token: 0x0600230D RID: 8973 RVA: 0x0054BAF4 File Offset: 0x00549CF4
		private static List<Rectangle> CreateRooms(Point origin)
		{
			Point point;
			if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(200), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point) || point == origin)
			{
				return new List<Rectangle>();
			}
			Rectangle rectangle = HouseUtils.FindRoom(point);
			Rectangle rectangle2 = HouseUtils.FindRoom(new Point(rectangle.Center.X, rectangle.Y + 1));
			Rectangle rectangle3 = HouseUtils.FindRoom(new Point(rectangle.Center.X, rectangle.Y + rectangle.Height + 10));
			rectangle3.Y = rectangle.Y + rectangle.Height - 1;
			double roomSolidPrecentage = HouseUtils.GetRoomSolidPrecentage(rectangle2);
			double roomSolidPrecentage2 = HouseUtils.GetRoomSolidPrecentage(rectangle3);
			rectangle.Y += 3;
			rectangle2.Y += 3;
			rectangle3.Y += 3;
			List<Rectangle> list = new List<Rectangle>();
			if (WorldGen.genRand.NextDouble() > roomSolidPrecentage + 0.2)
			{
				list.Add(rectangle2);
			}
			list.Add(rectangle);
			if (WorldGen.genRand.NextDouble() > roomSolidPrecentage2 + 0.2)
			{
				list.Add(rectangle3);
			}
			return list;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0054BC20 File Offset: 0x00549E20
		private static Rectangle FindRoom(Point origin)
		{
			Point point;
			bool flag = WorldUtils.Find(origin, Searches.Chain(new Searches.Left(25), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point);
			Point point2;
			bool flag2 = WorldUtils.Find(origin, Searches.Chain(new Searches.Right(25), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point2);
			if (!flag)
			{
				point = new Point(origin.X - 25, origin.Y);
			}
			if (!flag2)
			{
				point2 = new Point(origin.X + 25, origin.Y);
			}
			Rectangle rectangle = new Rectangle(origin.X, origin.Y, 0, 0);
			if (origin.X - point.X > point2.X - origin.X)
			{
				rectangle.X = point.X;
				rectangle.Width = Utils.Clamp<int>(point2.X - point.X, 15, 30);
			}
			else
			{
				rectangle.Width = Utils.Clamp<int>(point2.X - point.X, 15, 30);
				rectangle.X = point2.X - rectangle.Width;
			}
			Point point3;
			bool flag3 = WorldUtils.Find(point, Searches.Chain(new Searches.Up(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point3);
			Point point4;
			bool flag4 = WorldUtils.Find(point2, Searches.Chain(new Searches.Up(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point4);
			if (!flag3)
			{
				point3 = new Point(origin.X, origin.Y - 10);
			}
			if (!flag4)
			{
				point4 = new Point(origin.X, origin.Y - 10);
			}
			rectangle.Height = Utils.Clamp<int>(Math.Max(origin.Y - point3.Y, origin.Y - point4.Y), 8, 12);
			rectangle.Y -= rectangle.Height;
			return rectangle;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0054BDEC File Offset: 0x00549FEC
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

		// Token: 0x06002310 RID: 8976 RVA: 0x0054BE5C File Offset: 0x0054A05C
		private static int SortBiomeResults(Tuple<HouseType, int> item1, Tuple<HouseType, int> item2)
		{
			return item2.Item2.CompareTo(item1.Item2);
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0054BE80 File Offset: 0x0054A080
		private static bool AreRoomLocationsValid(IEnumerable<Rectangle> rooms)
		{
			foreach (Rectangle rectangle in rooms)
			{
				if (rectangle.Y + rectangle.Height > Main.maxTilesY - 220)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0054BEE4 File Offset: 0x0054A0E4
		private static HouseType GetHouseType(IEnumerable<Rectangle> rooms)
		{
			Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
			foreach (Rectangle rectangle in rooms)
			{
				WorldUtils.Gen(new Point(rectangle.X - 10, rectangle.Y - 10), new Shapes.Rectangle(rectangle.Width + 20, rectangle.Height + 20), new Actions.TileScanner(new ushort[]
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
			list.Sort(new Comparison<Tuple<HouseType, int>>(HouseUtils.SortBiomeResults));
			return list[0].Item1;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0054C078 File Offset: 0x0054A278
		private static bool AreRoomsValid(IEnumerable<Rectangle> rooms, StructureMap structures, HouseType style)
		{
			foreach (Rectangle rectangle in rooms)
			{
				Point point;
				if (style != HouseType.Granite && WorldUtils.Find(new Point(rectangle.X - 2, rectangle.Y - 2), Searches.Chain(new Searches.Rectangle(rectangle.Width + 4, rectangle.Height + 4).RequireAll(false), new GenCondition[]
				{
					new Conditions.HasLava()
				}), out point))
				{
					return false;
				}
				if (WorldGen.notTheBees)
				{
					if (!structures.CanPlace(rectangle, HouseUtils.BeelistedTiles, 5))
					{
						return false;
					}
				}
				else if (!structures.CanPlace(rectangle, HouseUtils.BlacklistedTiles, 5))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04004803 RID: 18435
		private static readonly bool[] BlacklistedTiles = TileID.Sets.Factory.CreateBoolSet(true, new int[]
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

		// Token: 0x04004804 RID: 18436
		private static readonly bool[] BeelistedTiles = TileID.Sets.Factory.CreateBoolSet(true, new int[]
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
	}
}
