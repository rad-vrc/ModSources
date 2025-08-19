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
	// Token: 0x0200066A RID: 1642
	public class HouseBuilder
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x0063B844 File Offset: 0x00639A44
		// (set) Token: 0x0600473D RID: 18237 RVA: 0x0063B84C File Offset: 0x00639A4C
		public double ChestChance { get; set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600473E RID: 18238 RVA: 0x0063B855 File Offset: 0x00639A55
		// (set) Token: 0x0600473F RID: 18239 RVA: 0x0063B85D File Offset: 0x00639A5D
		public ushort TileType { get; protected set; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x0063B866 File Offset: 0x00639A66
		// (set) Token: 0x06004741 RID: 18241 RVA: 0x0063B86E File Offset: 0x00639A6E
		public ushort WallType { get; protected set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x0063B877 File Offset: 0x00639A77
		// (set) Token: 0x06004743 RID: 18243 RVA: 0x0063B87F File Offset: 0x00639A7F
		public ushort BeamType { get; protected set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x0063B888 File Offset: 0x00639A88
		// (set) Token: 0x06004745 RID: 18245 RVA: 0x0063B890 File Offset: 0x00639A90
		public int PlatformStyle { get; protected set; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x0063B899 File Offset: 0x00639A99
		// (set) Token: 0x06004747 RID: 18247 RVA: 0x0063B8A1 File Offset: 0x00639AA1
		public int DoorStyle { get; protected set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x0063B8AA File Offset: 0x00639AAA
		// (set) Token: 0x06004749 RID: 18249 RVA: 0x0063B8B2 File Offset: 0x00639AB2
		public int TableStyle { get; protected set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x0063B8BB File Offset: 0x00639ABB
		// (set) Token: 0x0600474B RID: 18251 RVA: 0x0063B8C3 File Offset: 0x00639AC3
		public bool UsesTables2 { get; protected set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x0063B8CC File Offset: 0x00639ACC
		// (set) Token: 0x0600474D RID: 18253 RVA: 0x0063B8D4 File Offset: 0x00639AD4
		public int WorkbenchStyle { get; protected set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600474E RID: 18254 RVA: 0x0063B8DD File Offset: 0x00639ADD
		// (set) Token: 0x0600474F RID: 18255 RVA: 0x0063B8E5 File Offset: 0x00639AE5
		public int PianoStyle { get; protected set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x0063B8EE File Offset: 0x00639AEE
		// (set) Token: 0x06004751 RID: 18257 RVA: 0x0063B8F6 File Offset: 0x00639AF6
		public int BookcaseStyle { get; protected set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06004752 RID: 18258 RVA: 0x0063B8FF File Offset: 0x00639AFF
		// (set) Token: 0x06004753 RID: 18259 RVA: 0x0063B907 File Offset: 0x00639B07
		public int ChairStyle { get; protected set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06004754 RID: 18260 RVA: 0x0063B910 File Offset: 0x00639B10
		// (set) Token: 0x06004755 RID: 18261 RVA: 0x0063B918 File Offset: 0x00639B18
		public int ChestStyle { get; protected set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06004756 RID: 18262 RVA: 0x0063B921 File Offset: 0x00639B21
		// (set) Token: 0x06004757 RID: 18263 RVA: 0x0063B929 File Offset: 0x00639B29
		public bool UsesContainers2 { get; protected set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x0063B932 File Offset: 0x00639B32
		// (set) Token: 0x06004759 RID: 18265 RVA: 0x0063B93A File Offset: 0x00639B3A
		public ReadOnlyCollection<Rectangle> Rooms { get; private set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x0063B943 File Offset: 0x00639B43
		public Rectangle TopRoom
		{
			get
			{
				return this.Rooms.First<Rectangle>();
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600475B RID: 18267 RVA: 0x0063B950 File Offset: 0x00639B50
		public Rectangle BottomRoom
		{
			get
			{
				return this.Rooms.Last<Rectangle>();
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600475C RID: 18268 RVA: 0x0063B95D File Offset: 0x00639B5D
		private UnifiedRandom _random
		{
			get
			{
				return WorldGen.genRand;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x0063B964 File Offset: 0x00639B64
		private Tilemap _tiles
		{
			get
			{
				return Main.tile;
			}
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0063B96B File Offset: 0x00639B6B
		private HouseBuilder()
		{
			this.IsValid = false;
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x0063B994 File Offset: 0x00639B94
		protected HouseBuilder(HouseType type, IEnumerable<Rectangle> rooms)
		{
			this.Type = type;
			this.IsValid = true;
			List<Rectangle> list = rooms.ToList<Rectangle>();
			list.Sort((Rectangle lhs, Rectangle rhs) => lhs.Top.CompareTo(rhs.Top));
			this.Rooms = list.AsReadOnly();
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x0063BA04 File Offset: 0x00639C04
		protected virtual void AgeRoom(Rectangle room)
		{
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0063BA08 File Offset: 0x00639C08
		public virtual void Place(HouseBuilderContext context, StructureMap structures)
		{
			this.PlaceEmptyRooms();
			foreach (Rectangle room in this.Rooms)
			{
				structures.AddProtectedStructure(room, 8);
			}
			this.PlaceStairs();
			this.PlaceDoors();
			this.PlacePlatforms();
			this.PlaceSupportBeams();
			this.PlaceBiomeSpecificPriorityTool(context);
			this.FillRooms();
			foreach (Rectangle room2 in this.Rooms)
			{
				this.AgeRoom(room2);
			}
			this.PlaceChests();
			this.PlaceBiomeSpecificTool(context);
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x0063BACC File Offset: 0x00639CCC
		private void PlaceEmptyRooms()
		{
			foreach (Rectangle room in this.Rooms)
			{
				WorldUtils.Gen(new Point(room.X, room.Y), new Shapes.Rectangle(room.Width, room.Height), Actions.Chain(new GenAction[]
				{
					new Actions.SetTileKeepWall(this.TileType, false, true),
					new Actions.SetFrames(true)
				}));
				WorldUtils.Gen(new Point(room.X + 1, room.Y + 1), new Shapes.Rectangle(room.Width - 2, room.Height - 2), Actions.Chain(new GenAction[]
				{
					new Actions.ClearTile(true),
					new Actions.PlaceWall(this.WallType, true)
				}));
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0063BBB8 File Offset: 0x00639DB8
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
			foreach (Rectangle room in this.Rooms)
			{
				int num = room.Width / 8;
				int num2 = room.Width / (num + 1);
				int num3 = this._random.Next(2);
				for (int i = 0; i < num; i++)
				{
					int num4 = (i + 1) * num2 + room.X;
					int num11 = i + num3 % 2;
					if (num11 != 0)
					{
						if (num11 == 1)
						{
							int num5 = room.Y + 1;
							WorldGen.PlaceTile(num4, num5, 34, true, false, -1, this._random.Next(6));
							for (int j = -1; j < 2; j++)
							{
								for (int k = 0; k < 3; k++)
								{
									ref short frameX = ref this._tiles[j + num4, k + num5].frameX;
									frameX += 54;
								}
							}
						}
					}
					else
					{
						int num6 = room.Y + Math.Min(room.Height / 2, room.Height - 5);
						PaintingEntry paintingEntry = (this.Type == HouseType.Desert) ? WorldGen.RandHousePictureDesert() : WorldGen.RandHousePicture();
						WorldGen.PlaceTile(num4, num6, paintingEntry.tileType, true, false, -1, paintingEntry.style);
					}
				}
				int num7 = room.Width / 8 + 3;
				WorldGen.SetupStatueList();
				while (num7 > 0)
				{
					int num8 = this._random.Next(room.Width - 3) + 1 + room.X;
					int num9 = room.Y + room.Height - 2;
					switch (this._random.Next(4))
					{
					case 0:
						WorldGen.PlaceSmallPile(num8, num9, this._random.Next(31, 34), 1, 185);
						break;
					case 1:
						WorldGen.PlaceTile(num8, num9, 186, true, false, -1, this._random.Next(22, 26));
						break;
					case 2:
					{
						int num10 = this._random.Next(2, GenVars.statueList.Length);
						WorldGen.PlaceTile(num8, num9, (int)GenVars.statueList[num10].X, true, false, -1, (int)GenVars.statueList[num10].Y);
						if (GenVars.StatuesWithTraps.Contains(num10))
						{
							WorldGen.PlaceStatueTrap(num8, num9);
						}
						break;
					}
					case 3:
					{
						Point point = Utils.SelectRandom<Point>(this._random, choices);
						WorldGen.PlaceTile(num8, num9, point.X, true, false, -1, point.Y);
						break;
					}
					}
					num7--;
				}
			}
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x0063BF14 File Offset: 0x0063A114
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

		// Token: 0x06004765 RID: 18277 RVA: 0x0063C06C File Offset: 0x0063A26C
		private List<Tuple<Point, Point>> CreateStairsList()
		{
			List<Tuple<Point, Point>> list = new List<Tuple<Point, Point>>();
			for (int i = 1; i < this.Rooms.Count; i++)
			{
				Rectangle rectangle = this.Rooms[i];
				Rectangle rectangle2 = this.Rooms[i - 1];
				int num3 = rectangle2.X - rectangle.X;
				int num2 = rectangle.X + rectangle.Width - (rectangle2.X + rectangle2.Width);
				if (num3 > num2)
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

		// Token: 0x06004766 RID: 18278 RVA: 0x0063C17C File Offset: 0x0063A37C
		private void PlaceDoors()
		{
			foreach (Point item in this.CreateDoorList())
			{
				WorldUtils.Gen(item, new Shapes.Rectangle(1, 3), new Actions.ClearTile(true));
				WorldGen.PlaceTile(item.X, item.Y, 10, true, true, -1, this.DoorStyle);
			}
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x0063C1FC File Offset: 0x0063A3FC
		private List<Point> CreateDoorList()
		{
			List<Point> list = new List<Point>();
			foreach (Rectangle room in this.Rooms)
			{
				int exitY;
				if (HouseBuilder.FindSideExit(new Rectangle(room.X + room.Width, room.Y + 1, 1, room.Height - 2), false, out exitY))
				{
					list.Add(new Point(room.X + room.Width - 1, exitY));
				}
				if (HouseBuilder.FindSideExit(new Rectangle(room.X, room.Y + 1, 1, room.Height - 2), true, out exitY))
				{
					list.Add(new Point(room.X, exitY));
				}
			}
			return list;
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x0063C2D0 File Offset: 0x0063A4D0
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

		// Token: 0x06004769 RID: 18281 RVA: 0x0063C354 File Offset: 0x0063A554
		private List<Point> CreatePlatformsList()
		{
			List<Point> list = new List<Point>();
			Rectangle topRoom = this.TopRoom;
			Rectangle bottomRoom = this.BottomRoom;
			int exitX;
			if (HouseBuilder.FindVerticalExit(new Rectangle(topRoom.X + 2, topRoom.Y, topRoom.Width - 4, 1), true, out exitX))
			{
				list.Add(new Point(exitX, topRoom.Y));
			}
			if (HouseBuilder.FindVerticalExit(new Rectangle(bottomRoom.X + 2, bottomRoom.Y + bottomRoom.Height - 1, bottomRoom.Width - 4, 1), false, out exitX))
			{
				list.Add(new Point(exitX, bottomRoom.Y + bottomRoom.Height - 1));
			}
			return list;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x0063C3F8 File Offset: 0x0063A5F8
		private unsafe void PlaceSupportBeams()
		{
			foreach (Rectangle item in this.CreateSupportBeamList())
			{
				if (item.Height > 1 && *this._tiles[item.X, item.Y - 1].type != 19)
				{
					WorldUtils.Gen(new Point(item.X, item.Y), new Shapes.Rectangle(item.Width, item.Height), Actions.Chain(new GenAction[]
					{
						new Actions.SetTileKeepWall(this.BeamType, false, true),
						new Actions.SetFrames(true)
					}));
					Tile tile = this._tiles[item.X, item.Y + item.Height];
					tile.slope(0);
					tile.halfBrick(false);
				}
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x0063C500 File Offset: 0x0063A700
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
							Point result;
							bool flag = WorldUtils.Find(new Point(i, num4), Searches.Chain(new Searches.Down(num5), new GenCondition[]
							{
								new Conditions.IsSolid()
							}), out result);
							if (num5 < 50)
							{
								flag = true;
								result..ctor(i, num4 + num5);
							}
							if (flag)
							{
								list.Add(new Rectangle(i, num4, 1, result.Y - num4));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x0063C6E0 File Offset: 0x0063A8E0
		private static bool FindVerticalExit(Rectangle wall, bool isUp, out int exitX)
		{
			Point result;
			bool result2 = WorldUtils.Find(new Point(wall.X + wall.Width - 3, wall.Y + (isUp ? -5 : 0)), Searches.Chain(new Searches.Left(wall.Width - 3), new GenCondition[]
			{
				new Conditions.IsSolid().Not().AreaOr(3, 5)
			}), out result);
			exitX = result.X;
			return result2;
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x0063C74C File Offset: 0x0063A94C
		private static bool FindSideExit(Rectangle wall, bool isLeft, out int exitY)
		{
			Point result;
			bool result2 = WorldUtils.Find(new Point(wall.X + (isLeft ? -4 : 0), wall.Y + wall.Height - 3), Searches.Chain(new Searches.Up(wall.Height - 3), new GenCondition[]
			{
				new Conditions.IsSolid().Not().AreaOr(4, 3)
			}), out result);
			exitY = result.Y;
			return result2;
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x0063C7B8 File Offset: 0x0063A9B8
		private void PlaceChests()
		{
			if (this._random.NextDouble() > this.ChestChance)
			{
				return;
			}
			bool flag = false;
			foreach (Rectangle room in this.Rooms)
			{
				int num = room.Height - 1 + room.Y;
				bool flag2 = num > (int)Main.worldSurface;
				ushort chestTileType = (flag2 && this.UsesContainers2) ? 467 : 21;
				int style = flag2 ? this.ChestStyle : 0;
				int i = 0;
				while (i < 10 && !(flag = WorldGen.AddBuriedChest(this._random.Next(2, room.Width - 2) + room.X, num, 0, false, style, false, chestTileType)))
				{
					i++;
				}
				if (flag)
				{
					break;
				}
				int j = room.X + 2;
				while (j <= room.X + room.Width - 2 && !(flag = WorldGen.AddBuriedChest(j, num, 0, false, style, false, chestTileType)))
				{
					j++;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				foreach (Rectangle room2 in this.Rooms)
				{
					int num2 = room2.Y - 1;
					bool flag3 = num2 > (int)Main.worldSurface;
					ushort chestTileType2 = (flag3 && this.UsesContainers2) ? 467 : 21;
					int style2 = flag3 ? this.ChestStyle : 0;
					int k = 0;
					while (k < 10 && !(flag = WorldGen.AddBuriedChest(this._random.Next(2, room2.Width - 2) + room2.X, num2, 0, false, style2, false, chestTileType2)))
					{
						k++;
					}
					if (flag)
					{
						break;
					}
					int l = room2.X + 2;
					while (l <= room2.X + room2.Width - 2 && !(flag = WorldGen.AddBuriedChest(l, num2, 0, false, style2, false, chestTileType2)))
					{
						l++;
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (flag)
			{
				return;
			}
			for (int m = 0; m < 1000; m++)
			{
				int i2 = this._random.Next(this.Rooms[0].X - 30, this.Rooms[0].X + 30);
				int num3 = this._random.Next(this.Rooms[0].Y - 30, this.Rooms[0].Y + 30);
				bool flag4 = num3 > (int)Main.worldSurface;
				ushort chestTileType3 = (flag4 && this.UsesContainers2) ? 467 : 21;
				int style3 = flag4 ? this.ChestStyle : 0;
				if (flag = WorldGen.AddBuriedChest(i2, num3, 0, false, style3, false, chestTileType3))
				{
					break;
				}
			}
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x0063CAA0 File Offset: 0x0063ACA0
		private unsafe void PlaceBiomeSpecificPriorityTool(HouseBuilderContext context)
		{
			if (this.Type != HouseType.Desert || GenVars.extraBastStatueCount >= GenVars.extraBastStatueCountMax)
			{
				return;
			}
			bool flag = false;
			foreach (Rectangle room in this.Rooms)
			{
				int num = room.Height - 2 + room.Y;
				if (WorldGen.remixWorldGen && (double)num > Main.rockLayer)
				{
					return;
				}
				for (int i = 0; i < 10; i++)
				{
					int num2 = this._random.Next(2, room.Width - 2) + room.X;
					WorldGen.PlaceTile(num2, num, 506, true, true, -1, 0);
					if (flag = (this._tiles[num2, num].active() && *this._tiles[num2, num].type == 506))
					{
						break;
					}
				}
				if (flag)
				{
					break;
				}
				int j = room.X + 2;
				while (j <= room.X + room.Width - 2 && !(flag = WorldGen.PlaceTile(j, num, 506, true, true, -1, 0)))
				{
					j++;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				foreach (Rectangle room2 in this.Rooms)
				{
					int num3 = room2.Y - 1;
					for (int k = 0; k < 10; k++)
					{
						int num4 = this._random.Next(2, room2.Width - 2) + room2.X;
						WorldGen.PlaceTile(num4, num3, 506, true, true, -1, 0);
						if (flag = (this._tiles[num4, num3].active() && *this._tiles[num4, num3].type == 506))
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int l = room2.X + 2;
					while (l <= room2.X + room2.Width - 2 && !(flag = WorldGen.PlaceTile(l, num3, 506, true, true, -1, 0)))
					{
						l++;
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

		// Token: 0x06004770 RID: 18288 RVA: 0x0063CD48 File Offset: 0x0063AF48
		private unsafe void PlaceBiomeSpecificTool(HouseBuilderContext context)
		{
			if (this.Type == HouseType.Jungle && context.SharpenerCount < this._random.Next(2, 5))
			{
				bool flag = false;
				foreach (Rectangle room in this.Rooms)
				{
					int num = room.Height - 2 + room.Y;
					for (int i = 0; i < 10; i++)
					{
						int num2 = this._random.Next(2, room.Width - 2) + room.X;
						WorldGen.PlaceTile(num2, num, 377, true, true, -1, 0);
						if (flag = (this._tiles[num2, num].active() && *this._tiles[num2, num].type == 377))
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int j = room.X + 2;
					while (j <= room.X + room.Width - 2 && !(flag = WorldGen.PlaceTile(j, num, 377, true, true, -1, 0)))
					{
						j++;
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
			if (this.Type != HouseType.Desert || context.ExtractinatorCount >= this._random.Next(2, 5))
			{
				return;
			}
			bool flag2 = false;
			foreach (Rectangle room2 in this.Rooms)
			{
				int num3 = room2.Height - 2 + room2.Y;
				for (int k = 0; k < 10; k++)
				{
					int num4 = this._random.Next(2, room2.Width - 2) + room2.X;
					WorldGen.PlaceTile(num4, num3, 219, true, true, -1, 0);
					if (flag2 = (this._tiles[num4, num3].active() && *this._tiles[num4, num3].type == 219))
					{
						break;
					}
				}
				if (flag2)
				{
					break;
				}
				int l = room2.X + 2;
				while (l <= room2.X + room2.Width - 2 && !(flag2 = WorldGen.PlaceTile(l, num3, 219, true, true, -1, 0)))
				{
					l++;
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

		// Token: 0x04005BBC RID: 23484
		private const int VERTICAL_EXIT_WIDTH = 3;

		// Token: 0x04005BBD RID: 23485
		public static readonly HouseBuilder Invalid = new HouseBuilder();

		// Token: 0x04005BBE RID: 23486
		public readonly HouseType Type;

		// Token: 0x04005BBF RID: 23487
		public readonly bool IsValid;

		// Token: 0x04005BC0 RID: 23488
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
