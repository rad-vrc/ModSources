using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x020002E1 RID: 737
	public class HouseBuilder
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0054C674 File Offset: 0x0054A874
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x0054C67C File Offset: 0x0054A87C
		public double ChestChance { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0054C685 File Offset: 0x0054A885
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x0054C68D File Offset: 0x0054A88D
		public ushort TileType { get; protected set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0054C696 File Offset: 0x0054A896
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x0054C69E File Offset: 0x0054A89E
		public ushort WallType { get; protected set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0054C6A7 File Offset: 0x0054A8A7
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x0054C6AF File Offset: 0x0054A8AF
		public ushort BeamType { get; protected set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x0054C6B8 File Offset: 0x0054A8B8
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x0054C6C0 File Offset: 0x0054A8C0
		public int PlatformStyle { get; protected set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0054C6C9 File Offset: 0x0054A8C9
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x0054C6D1 File Offset: 0x0054A8D1
		public int DoorStyle { get; protected set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0054C6DA File Offset: 0x0054A8DA
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x0054C6E2 File Offset: 0x0054A8E2
		public int TableStyle { get; protected set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x0054C6EB File Offset: 0x0054A8EB
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x0054C6F3 File Offset: 0x0054A8F3
		public bool UsesTables2 { get; protected set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x0054C6FC File Offset: 0x0054A8FC
		// (set) Token: 0x0600232A RID: 9002 RVA: 0x0054C704 File Offset: 0x0054A904
		public int WorkbenchStyle { get; protected set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x0054C70D File Offset: 0x0054A90D
		// (set) Token: 0x0600232C RID: 9004 RVA: 0x0054C715 File Offset: 0x0054A915
		public int PianoStyle { get; protected set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x0054C71E File Offset: 0x0054A91E
		// (set) Token: 0x0600232E RID: 9006 RVA: 0x0054C726 File Offset: 0x0054A926
		public int BookcaseStyle { get; protected set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0054C72F File Offset: 0x0054A92F
		// (set) Token: 0x06002330 RID: 9008 RVA: 0x0054C737 File Offset: 0x0054A937
		public int ChairStyle { get; protected set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x0054C740 File Offset: 0x0054A940
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x0054C748 File Offset: 0x0054A948
		public int ChestStyle { get; protected set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x0054C751 File Offset: 0x0054A951
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x0054C759 File Offset: 0x0054A959
		public bool UsesContainers2 { get; protected set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0054C762 File Offset: 0x0054A962
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x0054C76A File Offset: 0x0054A96A
		public ReadOnlyCollection<Rectangle> Rooms { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x0054C773 File Offset: 0x0054A973
		public Rectangle TopRoom
		{
			get
			{
				return this.Rooms.First<Rectangle>();
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x0054C780 File Offset: 0x0054A980
		public Rectangle BottomRoom
		{
			get
			{
				return this.Rooms.Last<Rectangle>();
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0048C7E1 File Offset: 0x0048A9E1
		private UnifiedRandom _random
		{
			get
			{
				return WorldGen.genRand;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0048C7E8 File Offset: 0x0048A9E8
		private Tile[,] _tiles
		{
			get
			{
				return Main.tile;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0054C78D File Offset: 0x0054A98D
		private HouseBuilder()
		{
			this.IsValid = false;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0054C7B4 File Offset: 0x0054A9B4
		protected HouseBuilder(HouseType type, IEnumerable<Rectangle> rooms)
		{
			this.Type = type;
			this.IsValid = true;
			List<Rectangle> list = rooms.ToList<Rectangle>();
			list.Sort((Rectangle lhs, Rectangle rhs) => lhs.Top.CompareTo(rhs.Top));
			this.Rooms = list.AsReadOnly();
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected virtual void AgeRoom(Rectangle room)
		{
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0054C824 File Offset: 0x0054AA24
		public virtual void Place(HouseBuilderContext context, StructureMap structures)
		{
			this.PlaceEmptyRooms();
			foreach (Rectangle area in this.Rooms)
			{
				structures.AddProtectedStructure(area, 8);
			}
			this.PlaceStairs();
			this.PlaceDoors();
			this.PlacePlatforms();
			this.PlaceSupportBeams();
			this.PlaceBiomeSpecificPriorityTool(context);
			this.FillRooms();
			foreach (Rectangle room in this.Rooms)
			{
				this.AgeRoom(room);
			}
			this.PlaceChests();
			this.PlaceBiomeSpecificTool(context);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0054C8E8 File Offset: 0x0054AAE8
		private void PlaceEmptyRooms()
		{
			foreach (Rectangle rectangle in this.Rooms)
			{
				WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain(new GenAction[]
				{
					new Actions.SetTileKeepWall(this.TileType, false, true),
					new Actions.SetFrames(true)
				}));
				WorldUtils.Gen(new Point(rectangle.X + 1, rectangle.Y + 1), new Shapes.Rectangle(rectangle.Width - 2, rectangle.Height - 2), Actions.Chain(new GenAction[]
				{
					new Actions.ClearTile(true),
					new Actions.PlaceWall(this.WallType, true)
				}));
			}
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0054C9D4 File Offset: 0x0054ABD4
		private void FillRooms()
		{
			int x = 14;
			if (this.UsesTables2)
			{
				x = 469;
			}
			Point[] choices = new Point[]
			{
				new Point(x, this.TableStyle),
				new Point(16, 0),
				new Point(18, this.WorkbenchStyle),
				new Point(86, 0),
				new Point(87, this.PianoStyle),
				new Point(94, 0),
				new Point(101, this.BookcaseStyle)
			};
			foreach (Rectangle rectangle in this.Rooms)
			{
				int num = rectangle.Width / 8;
				int num2 = rectangle.Width / (num + 1);
				int num3 = this._random.Next(2);
				for (int i = 0; i < num; i++)
				{
					int num4 = (i + 1) * num2 + rectangle.X;
					int num5 = i + num3 % 2;
					if (num5 != 0)
					{
						if (num5 == 1)
						{
							int num6 = rectangle.Y + 1;
							WorldGen.PlaceTile(num4, num6, 34, true, false, -1, this._random.Next(6));
							for (int j = -1; j < 2; j++)
							{
								for (int k = 0; k < 3; k++)
								{
									Tile tile = this._tiles[j + num4, k + num6];
									tile.frameX += 54;
								}
							}
						}
					}
					else
					{
						int num6 = rectangle.Y + Math.Min(rectangle.Height / 2, rectangle.Height - 5);
						PaintingEntry paintingEntry = (this.Type == HouseType.Desert) ? WorldGen.RandHousePictureDesert() : WorldGen.RandHousePicture();
						WorldGen.PlaceTile(num4, num6, paintingEntry.tileType, true, false, -1, paintingEntry.style);
					}
				}
				int l = rectangle.Width / 8 + 3;
				WorldGen.SetupStatueList();
				while (l > 0)
				{
					int num7 = this._random.Next(rectangle.Width - 3) + 1 + rectangle.X;
					int num8 = rectangle.Y + rectangle.Height - 2;
					switch (this._random.Next(4))
					{
					case 0:
						WorldGen.PlaceSmallPile(num7, num8, this._random.Next(31, 34), 1, 185);
						break;
					case 1:
						WorldGen.PlaceTile(num7, num8, 186, true, false, -1, this._random.Next(22, 26));
						break;
					case 2:
					{
						int num9 = this._random.Next(2, GenVars.statueList.Length);
						WorldGen.PlaceTile(num7, num8, (int)GenVars.statueList[num9].X, true, false, -1, (int)GenVars.statueList[num9].Y);
						if (GenVars.StatuesWithTraps.Contains(num9))
						{
							WorldGen.PlaceStatueTrap(num7, num8);
						}
						break;
					}
					case 3:
					{
						Point point = Utils.SelectRandom<Point>(this._random, choices);
						WorldGen.PlaceTile(num7, num8, point.X, true, false, -1, point.Y);
						break;
					}
					}
					l--;
				}
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x0054CD2C File Offset: 0x0054AF2C
		private void PlaceStairs()
		{
			foreach (Tuple<Point, Point> tuple in this.CreateStairsList())
			{
				Point item = tuple.Item1;
				Point item2 = tuple.Item2;
				int num = (item2.X > item.X) ? 1 : -1;
				ShapeData shapeData = new ShapeData();
				for (int i = 0; i < item2.Y - item.Y; i++)
				{
					shapeData.Add(num * (i + 1), i);
				}
				WorldUtils.Gen(item, new ModShapes.All(shapeData), Actions.Chain(new GenAction[]
				{
					new Actions.PlaceTile(19, this.PlatformStyle),
					new Actions.SetSlope((num == 1) ? 1 : 2),
					new Actions.SetFrames(true)
				}));
				WorldUtils.Gen(new Point(item.X + ((num == 1) ? 1 : -4), item.Y - 1), new Shapes.Rectangle(4, 1), Actions.Chain(new GenAction[]
				{
					new Actions.Clear(),
					new Actions.PlaceWall(this.WallType, true),
					new Actions.PlaceTile(19, this.PlatformStyle),
					new Actions.SetFrames(true)
				}));
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0054CE84 File Offset: 0x0054B084
		private List<Tuple<Point, Point>> CreateStairsList()
		{
			List<Tuple<Point, Point>> list = new List<Tuple<Point, Point>>();
			for (int i = 1; i < this.Rooms.Count; i++)
			{
				Rectangle rectangle = this.Rooms[i];
				Rectangle rectangle2 = this.Rooms[i - 1];
				int num = rectangle2.X - rectangle.X;
				int num2 = rectangle.X + rectangle.Width - (rectangle2.X + rectangle2.Width);
				if (num > num2)
				{
					list.Add(new Tuple<Point, Point>(new Point(rectangle.X + rectangle.Width - 1, rectangle.Y + 1), new Point(rectangle.X + rectangle.Width - rectangle.Height + 1, rectangle.Y + rectangle.Height - 1)));
				}
				else
				{
					list.Add(new Tuple<Point, Point>(new Point(rectangle.X, rectangle.Y + 1), new Point(rectangle.X + rectangle.Height - 1, rectangle.Y + rectangle.Height - 1)));
				}
			}
			return list;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0054CF94 File Offset: 0x0054B194
		private void PlaceDoors()
		{
			foreach (Point point in this.CreateDoorList())
			{
				WorldUtils.Gen(point, new Shapes.Rectangle(1, 3), new Actions.ClearTile(true));
				WorldGen.PlaceTile(point.X, point.Y, 10, true, true, -1, this.DoorStyle);
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0054D014 File Offset: 0x0054B214
		private List<Point> CreateDoorList()
		{
			List<Point> list = new List<Point>();
			foreach (Rectangle rectangle in this.Rooms)
			{
				int y;
				if (HouseBuilder.FindSideExit(new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 1, 1, rectangle.Height - 2), false, out y))
				{
					list.Add(new Point(rectangle.X + rectangle.Width - 1, y));
				}
				if (HouseBuilder.FindSideExit(new Rectangle(rectangle.X, rectangle.Y + 1, 1, rectangle.Height - 2), true, out y))
				{
					list.Add(new Point(rectangle.X, y));
				}
			}
			return list;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0054D0E8 File Offset: 0x0054B2E8
		private void PlacePlatforms()
		{
			foreach (Point origin in this.CreatePlatformsList())
			{
				WorldUtils.Gen(origin, new Shapes.Rectangle(3, 1), Actions.Chain(new GenAction[]
				{
					new Actions.ClearMetadata(),
					new Actions.PlaceTile(19, this.PlatformStyle),
					new Actions.SetFrames(true)
				}));
			}
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0054D16C File Offset: 0x0054B36C
		private List<Point> CreatePlatformsList()
		{
			List<Point> list = new List<Point>();
			Rectangle topRoom = this.TopRoom;
			Rectangle bottomRoom = this.BottomRoom;
			int x;
			if (HouseBuilder.FindVerticalExit(new Rectangle(topRoom.X + 2, topRoom.Y, topRoom.Width - 4, 1), true, out x))
			{
				list.Add(new Point(x, topRoom.Y));
			}
			if (HouseBuilder.FindVerticalExit(new Rectangle(bottomRoom.X + 2, bottomRoom.Y + bottomRoom.Height - 1, bottomRoom.Width - 4, 1), false, out x))
			{
				list.Add(new Point(x, bottomRoom.Y + bottomRoom.Height - 1));
			}
			return list;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0054D210 File Offset: 0x0054B410
		private void PlaceSupportBeams()
		{
			foreach (Rectangle rectangle in this.CreateSupportBeamList())
			{
				if (rectangle.Height > 1 && this._tiles[rectangle.X, rectangle.Y - 1].type != 19)
				{
					WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain(new GenAction[]
					{
						new Actions.SetTileKeepWall(this.BeamType, false, true),
						new Actions.SetFrames(true)
					}));
					Tile tile = this._tiles[rectangle.X, rectangle.Y + rectangle.Height];
					tile.slope(0);
					tile.halfBrick(false);
				}
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0054D308 File Offset: 0x0054B508
		private List<Rectangle> CreateSupportBeamList()
		{
			List<Rectangle> list = new List<Rectangle>();
			int num = this.Rooms.Min((Rectangle room) => room.Left);
			int num2 = this.Rooms.Max((Rectangle room) => room.Right) - 1;
			int num3 = 6;
			while (num3 > 4 && (num2 - num) % num3 != 0)
			{
				num3--;
			}
			for (int i = num; i <= num2; i += num3)
			{
				for (int j = 0; j < this.Rooms.Count; j++)
				{
					Rectangle rectangle = this.Rooms[j];
					if (i >= rectangle.X && i < rectangle.X + rectangle.Width)
					{
						int num4 = rectangle.Y + rectangle.Height;
						int num5 = 50;
						for (int k = j + 1; k < this.Rooms.Count; k++)
						{
							if (i >= this.Rooms[k].X && i < this.Rooms[k].X + this.Rooms[k].Width)
							{
								num5 = Math.Min(num5, this.Rooms[k].Y - num4);
							}
						}
						if (num5 > 0)
						{
							Point point;
							bool flag = WorldUtils.Find(new Point(i, num4), Searches.Chain(new Searches.Down(num5), new GenCondition[]
							{
								new Conditions.IsSolid()
							}), out point);
							if (num5 < 50)
							{
								flag = true;
								point = new Point(i, num4 + num5);
							}
							if (flag)
							{
								list.Add(new Rectangle(i, num4, 1, point.Y - num4));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0054D4E8 File Offset: 0x0054B6E8
		private static bool FindVerticalExit(Rectangle wall, bool isUp, out int exitX)
		{
			Point point;
			bool result = WorldUtils.Find(new Point(wall.X + wall.Width - 3, wall.Y + (isUp ? -5 : 0)), Searches.Chain(new Searches.Left(wall.Width - 3), new GenCondition[]
			{
				new Conditions.IsSolid().Not().AreaOr(3, 5)
			}), out point);
			exitX = point.X;
			return result;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0054D554 File Offset: 0x0054B754
		private static bool FindSideExit(Rectangle wall, bool isLeft, out int exitY)
		{
			Point point;
			bool result = WorldUtils.Find(new Point(wall.X + (isLeft ? -4 : 0), wall.Y + wall.Height - 3), Searches.Chain(new Searches.Up(wall.Height - 3), new GenCondition[]
			{
				new Conditions.IsSolid().Not().AreaOr(4, 3)
			}), out point);
			exitY = point.Y;
			return result;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0054D5C0 File Offset: 0x0054B7C0
		private void PlaceChests()
		{
			if (this._random.NextDouble() > this.ChestChance)
			{
				return;
			}
			bool flag = false;
			foreach (Rectangle rectangle in this.Rooms)
			{
				int num = rectangle.Height - 1 + rectangle.Y;
				bool flag2 = num > (int)Main.worldSurface;
				ushort chestTileType = (flag2 && this.UsesContainers2) ? 467 : 21;
				int style = flag2 ? this.ChestStyle : 0;
				int num2 = 0;
				while (num2 < 10 && !(flag = WorldGen.AddBuriedChest(this._random.Next(2, rectangle.Width - 2) + rectangle.X, num, 0, false, style, false, chestTileType)))
				{
					num2++;
				}
				if (flag)
				{
					break;
				}
				int num3 = rectangle.X + 2;
				while (num3 <= rectangle.X + rectangle.Width - 2 && !(flag = WorldGen.AddBuriedChest(num3, num, 0, false, style, false, chestTileType)))
				{
					num3++;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				foreach (Rectangle rectangle2 in this.Rooms)
				{
					int num4 = rectangle2.Y - 1;
					bool flag3 = num4 > (int)Main.worldSurface;
					ushort chestTileType2 = (flag3 && this.UsesContainers2) ? 467 : 21;
					int style2 = flag3 ? this.ChestStyle : 0;
					int num5 = 0;
					while (num5 < 10 && !(flag = WorldGen.AddBuriedChest(this._random.Next(2, rectangle2.Width - 2) + rectangle2.X, num4, 0, false, style2, false, chestTileType2)))
					{
						num5++;
					}
					if (flag)
					{
						break;
					}
					int num6 = rectangle2.X + 2;
					while (num6 <= rectangle2.X + rectangle2.Width - 2 && !(flag = WorldGen.AddBuriedChest(num6, num4, 0, false, style2, false, chestTileType2)))
					{
						num6++;
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (!flag)
			{
				for (int i = 0; i < 1000; i++)
				{
					int i2 = this._random.Next(this.Rooms[0].X - 30, this.Rooms[0].X + 30);
					int num7 = this._random.Next(this.Rooms[0].Y - 30, this.Rooms[0].Y + 30);
					bool flag4 = num7 > (int)Main.worldSurface;
					ushort chestTileType3 = (flag4 && this.UsesContainers2) ? 467 : 21;
					int style3 = flag4 ? this.ChestStyle : 0;
					if (flag = WorldGen.AddBuriedChest(i2, num7, 0, false, style3, false, chestTileType3))
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0054D8A4 File Offset: 0x0054BAA4
		private void PlaceBiomeSpecificPriorityTool(HouseBuilderContext context)
		{
			if (this.Type == HouseType.Desert && GenVars.extraBastStatueCount < GenVars.extraBastStatueCountMax)
			{
				bool flag = false;
				foreach (Rectangle rectangle in this.Rooms)
				{
					int num = rectangle.Height - 2 + rectangle.Y;
					if (WorldGen.remixWorldGen && (double)num > Main.rockLayer)
					{
						return;
					}
					for (int i = 0; i < 10; i++)
					{
						int num2 = this._random.Next(2, rectangle.Width - 2) + rectangle.X;
						WorldGen.PlaceTile(num2, num, 506, true, true, -1, 0);
						if (flag = (this._tiles[num2, num].active() && this._tiles[num2, num].type == 506))
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int num3 = rectangle.X + 2;
					while (num3 <= rectangle.X + rectangle.Width - 2 && !(flag = WorldGen.PlaceTile(num3, num, 506, true, true, -1, 0)))
					{
						num3++;
					}
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					foreach (Rectangle rectangle2 in this.Rooms)
					{
						int num4 = rectangle2.Y - 1;
						for (int j = 0; j < 10; j++)
						{
							int num5 = this._random.Next(2, rectangle2.Width - 2) + rectangle2.X;
							WorldGen.PlaceTile(num5, num4, 506, true, true, -1, 0);
							if (flag = (this._tiles[num5, num4].active() && this._tiles[num5, num4].type == 506))
							{
								break;
							}
						}
						if (flag)
						{
							break;
						}
						int num6 = rectangle2.X + 2;
						while (num6 <= rectangle2.X + rectangle2.Width - 2 && !(flag = WorldGen.PlaceTile(num6, num4, 506, true, true, -1, 0)))
						{
							num6++;
						}
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					GenVars.extraBastStatueCount++;
				}
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0054DB0C File Offset: 0x0054BD0C
		private void PlaceBiomeSpecificTool(HouseBuilderContext context)
		{
			if (this.Type == HouseType.Jungle && context.SharpenerCount < this._random.Next(2, 5))
			{
				bool flag = false;
				foreach (Rectangle rectangle in this.Rooms)
				{
					int num = rectangle.Height - 2 + rectangle.Y;
					for (int i = 0; i < 10; i++)
					{
						int num2 = this._random.Next(2, rectangle.Width - 2) + rectangle.X;
						WorldGen.PlaceTile(num2, num, 377, true, true, -1, 0);
						if (flag = (this._tiles[num2, num].active() && this._tiles[num2, num].type == 377))
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int num3 = rectangle.X + 2;
					while (num3 <= rectangle.X + rectangle.Width - 2 && !(flag = WorldGen.PlaceTile(num3, num, 377, true, true, -1, 0)))
					{
						num3++;
					}
					if (flag)
					{
						break;
					}
				}
				if (flag)
				{
					context.SharpenerCount++;
				}
			}
			if (this.Type == HouseType.Desert && context.ExtractinatorCount < this._random.Next(2, 5))
			{
				bool flag2 = false;
				foreach (Rectangle rectangle2 in this.Rooms)
				{
					int num4 = rectangle2.Height - 2 + rectangle2.Y;
					for (int j = 0; j < 10; j++)
					{
						int num5 = this._random.Next(2, rectangle2.Width - 2) + rectangle2.X;
						WorldGen.PlaceTile(num5, num4, 219, true, true, -1, 0);
						if (flag2 = (this._tiles[num5, num4].active() && this._tiles[num5, num4].type == 219))
						{
							break;
						}
					}
					if (flag2)
					{
						break;
					}
					int num6 = rectangle2.X + 2;
					while (num6 <= rectangle2.X + rectangle2.Width - 2 && !(flag2 = WorldGen.PlaceTile(num6, num4, 219, true, true, -1, 0)))
					{
						num6++;
					}
					if (flag2)
					{
						break;
					}
				}
				if (flag2)
				{
					context.ExtractinatorCount++;
				}
			}
		}

		// Token: 0x04004805 RID: 18437
		private const int VERTICAL_EXIT_WIDTH = 3;

		// Token: 0x04004806 RID: 18438
		public static readonly HouseBuilder Invalid = new HouseBuilder();

		// Token: 0x04004807 RID: 18439
		public readonly HouseType Type;

		// Token: 0x04004808 RID: 18440
		public readonly bool IsValid;

		// Token: 0x04004818 RID: 18456
		protected ushort[] SkipTilesDuringWallAging = new ushort[]
		{
			245,
			246,
			240,
			241,
			242
		};
	}
}
