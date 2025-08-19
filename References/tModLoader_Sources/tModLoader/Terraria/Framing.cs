using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria
{
	// Token: 0x0200002E RID: 46
	public class Framing
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00021DFC File Offset: 0x0001FFFC
		public static void Initialize()
		{
			Framing.selfFrame8WayLookup = new Point16[256][];
			Framing.frameSize8Way = new Point16(18, 18);
			Framing.Add8WayLookup(0, 9, 3, 10, 3, 11, 3);
			Framing.Add8WayLookup(1, 6, 3, 7, 3, 8, 3);
			Framing.Add8WayLookup(2, 12, 0, 12, 1, 12, 2);
			Framing.Add8WayLookup(3, 15, 2);
			Framing.Add8WayLookup(4, 9, 0, 9, 1, 9, 2);
			Framing.Add8WayLookup(5, 13, 2);
			Framing.Add8WayLookup(6, 6, 4, 7, 4, 8, 4);
			Framing.Add8WayLookup(7, 14, 2);
			Framing.Add8WayLookup(8, 6, 0, 7, 0, 8, 0);
			Framing.Add8WayLookup(9, 5, 0, 5, 1, 5, 2);
			Framing.Add8WayLookup(10, 15, 0);
			Framing.Add8WayLookup(11, 15, 1);
			Framing.Add8WayLookup(12, 13, 0);
			Framing.Add8WayLookup(13, 13, 1);
			Framing.Add8WayLookup(14, 14, 0);
			Framing.Add8WayLookup(15, 14, 1);
			Framing.Add8WayLookup(19, 1, 4, 3, 4, 5, 4);
			Framing.Add8WayLookup(23, 16, 3);
			Framing.Add8WayLookup(27, 17, 0);
			Framing.Add8WayLookup(31, 13, 4);
			Framing.Add8WayLookup(37, 0, 4, 2, 4, 4, 4);
			Framing.Add8WayLookup(39, 17, 3);
			Framing.Add8WayLookup(45, 16, 0);
			Framing.Add8WayLookup(47, 12, 4);
			Framing.Add8WayLookup(55, 1, 2, 2, 2, 3, 2);
			Framing.Add8WayLookup(63, 6, 2, 7, 2, 8, 2);
			Framing.Add8WayLookup(74, 1, 3, 3, 3, 5, 3);
			Framing.Add8WayLookup(75, 17, 1);
			Framing.Add8WayLookup(78, 16, 2);
			Framing.Add8WayLookup(79, 13, 3);
			Framing.Add8WayLookup(91, 4, 0, 4, 1, 4, 2);
			Framing.Add8WayLookup(95, 11, 0, 11, 1, 11, 2);
			Framing.Add8WayLookup(111, 17, 4);
			Framing.Add8WayLookup(127, 14, 3);
			Framing.Add8WayLookup(140, 0, 3, 2, 3, 4, 3);
			Framing.Add8WayLookup(141, 16, 1);
			Framing.Add8WayLookup(142, 17, 2);
			Framing.Add8WayLookup(143, 12, 3);
			Framing.Add8WayLookup(159, 16, 4);
			Framing.Add8WayLookup(173, 0, 0, 0, 1, 0, 2);
			Framing.Add8WayLookup(175, 10, 0, 10, 1, 10, 2);
			Framing.Add8WayLookup(191, 15, 3);
			Framing.Add8WayLookup(206, 1, 0, 2, 0, 3, 0);
			Framing.Add8WayLookup(207, 6, 1, 7, 1, 8, 1);
			Framing.Add8WayLookup(223, 14, 4);
			Framing.Add8WayLookup(239, 15, 4);
			Framing.Add8WayLookup(255, 1, 1, 2, 1, 3, 1);
			Framing.blockStyleLookup = new Framing.BlockStyle[6];
			Framing.blockStyleLookup[0] = new Framing.BlockStyle(true, true, true, true);
			Framing.blockStyleLookup[1] = new Framing.BlockStyle(false, true, true, true);
			Framing.blockStyleLookup[2] = new Framing.BlockStyle(false, true, true, false);
			Framing.blockStyleLookup[3] = new Framing.BlockStyle(false, true, false, true);
			Framing.blockStyleLookup[4] = new Framing.BlockStyle(true, false, true, false);
			Framing.blockStyleLookup[5] = new Framing.BlockStyle(true, false, false, true);
			Framing.phlebasTileFrameNumberLookup = new int[][]
			{
				new int[]
				{
					2,
					4,
					2
				},
				new int[]
				{
					1,
					3,
					1
				},
				new int[]
				{
					2,
					2,
					4
				},
				new int[]
				{
					1,
					1,
					3
				}
			};
			Framing.lazureTileFrameNumberLookup = new int[][]
			{
				new int[]
				{
					1,
					3
				},
				new int[]
				{
					2,
					4
				}
			};
			int[][] array = new int[3][];
			int num = 0;
			int[] array2 = new int[3];
			array2[0] = 2;
			array[num] = array2;
			array[1] = new int[]
			{
				0,
				1,
				4
			};
			int num2 = 2;
			int[] array3 = new int[3];
			array3[1] = 3;
			array[num2] = array3;
			Framing.centerWallFrameLookup = array;
			Framing.wallFrameLookup = new Point16[20][];
			Framing.wallFrameSize = new Point16(36, 36);
			Framing.AddWallFrameLookup(0, 9, 3, 10, 3, 11, 3, 6, 6);
			Framing.AddWallFrameLookup(1, 6, 3, 7, 3, 8, 3, 4, 6);
			Framing.AddWallFrameLookup(2, 12, 0, 12, 1, 12, 2, 12, 5);
			Framing.AddWallFrameLookup(3, 1, 4, 3, 4, 5, 4, 3, 6);
			Framing.AddWallFrameLookup(4, 9, 0, 9, 1, 9, 2, 9, 5);
			Framing.AddWallFrameLookup(5, 0, 4, 2, 4, 4, 4, 2, 6);
			Framing.AddWallFrameLookup(6, 6, 4, 7, 4, 8, 4, 5, 6);
			Framing.AddWallFrameLookup(7, 1, 2, 2, 2, 3, 2, 3, 5);
			Framing.AddWallFrameLookup(8, 6, 0, 7, 0, 8, 0, 6, 5);
			Framing.AddWallFrameLookup(9, 5, 0, 5, 1, 5, 2, 5, 5);
			Framing.AddWallFrameLookup(10, 1, 3, 3, 3, 5, 3, 1, 6);
			Framing.AddWallFrameLookup(11, 4, 0, 4, 1, 4, 2, 4, 5);
			Framing.AddWallFrameLookup(12, 0, 3, 2, 3, 4, 3, 0, 6);
			Framing.AddWallFrameLookup(13, 0, 0, 0, 1, 0, 2, 0, 5);
			Framing.AddWallFrameLookup(14, 1, 0, 2, 0, 3, 0, 1, 5);
			Framing.AddWallFrameLookup(15, 1, 1, 2, 1, 3, 1, 2, 5);
			Framing.AddWallFrameLookup(16, 6, 1, 7, 1, 8, 1, 7, 5);
			Framing.AddWallFrameLookup(17, 6, 2, 7, 2, 8, 2, 8, 5);
			Framing.AddWallFrameLookup(18, 10, 0, 10, 1, 10, 2, 10, 5);
			Framing.AddWallFrameLookup(19, 11, 0, 11, 1, 11, 2, 11, 5);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000222F6 File Offset: 0x000204F6
		private static Framing.BlockStyle FindBlockStyle(Tile blockTile)
		{
			return Framing.blockStyleLookup[blockTile.blockType()];
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0002230C File Offset: 0x0002050C
		public static void Add8WayLookup(int lookup, short point1X, short point1Y, short point2X, short point2Y, short point3X, short point3Y)
		{
			Point16[] array = new Point16[]
			{
				new Point16((int)(point1X * Framing.frameSize8Way.X), (int)(point1Y * Framing.frameSize8Way.Y)),
				new Point16((int)(point2X * Framing.frameSize8Way.X), (int)(point2Y * Framing.frameSize8Way.Y)),
				new Point16((int)(point3X * Framing.frameSize8Way.X), (int)(point3Y * Framing.frameSize8Way.Y))
			};
			Framing.selfFrame8WayLookup[lookup] = array;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00022398 File Offset: 0x00020598
		public static void Add8WayLookup(int lookup, short x, short y)
		{
			Point16[] array = new Point16[]
			{
				new Point16((int)(x * Framing.frameSize8Way.X), (int)(y * Framing.frameSize8Way.Y)),
				new Point16((int)(x * Framing.frameSize8Way.X), (int)(y * Framing.frameSize8Way.Y)),
				new Point16((int)(x * Framing.frameSize8Way.X), (int)(y * Framing.frameSize8Way.Y))
			};
			Framing.selfFrame8WayLookup[lookup] = array;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00022420 File Offset: 0x00020620
		public static void AddWallFrameLookup(int lookup, short point1X, short point1Y, short point2X, short point2Y, short point3X, short point3Y, short point4X, short point4Y)
		{
			Point16[] array = new Point16[]
			{
				new Point16((int)(point1X * Framing.wallFrameSize.X), (int)(point1Y * Framing.wallFrameSize.Y)),
				new Point16((int)(point2X * Framing.wallFrameSize.X), (int)(point2Y * Framing.wallFrameSize.Y)),
				new Point16((int)(point3X * Framing.wallFrameSize.X), (int)(point3Y * Framing.wallFrameSize.Y)),
				new Point16((int)(point4X * Framing.wallFrameSize.X), (int)(point4Y * Framing.wallFrameSize.Y))
			};
			Framing.wallFrameLookup[lookup] = array;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000224D1 File Offset: 0x000206D1
		private static bool WillItBlend(ushort myType, ushort otherType)
		{
			return (TileID.Sets.ForcedDirtMerging[(int)myType] && otherType == 0) || (Main.tileBrick[(int)myType] && Main.tileBrick[(int)otherType]) || TileID.Sets.GemsparkFramingTypes[(int)otherType] == TileID.Sets.GemsparkFramingTypes[(int)myType];
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00022508 File Offset: 0x00020708
		public unsafe static void SelfFrame8Way(int i, int j, Tile centerTile, bool resetFrame)
		{
			if (!centerTile.active())
			{
				return;
			}
			Framing.BlockStyle blockStyle10 = Framing.FindBlockStyle(centerTile);
			int num = 0;
			Framing.BlockStyle blockStyle2 = default(Framing.BlockStyle);
			if (blockStyle10.top)
			{
				Tile tileSafely = Framing.GetTileSafely(i, j - 1);
				if (tileSafely.active() && Framing.WillItBlend(*centerTile.type, *tileSafely.type))
				{
					blockStyle2 = Framing.FindBlockStyle(tileSafely);
					if (blockStyle2.bottom)
					{
						num |= 1;
					}
					else
					{
						blockStyle2.Clear();
					}
				}
			}
			Framing.BlockStyle blockStyle3 = default(Framing.BlockStyle);
			if (blockStyle10.left)
			{
				Tile tileSafely2 = Framing.GetTileSafely(i - 1, j);
				if (tileSafely2.active() && Framing.WillItBlend(*centerTile.type, *tileSafely2.type))
				{
					blockStyle3 = Framing.FindBlockStyle(tileSafely2);
					if (blockStyle3.right)
					{
						num |= 2;
					}
					else
					{
						blockStyle3.Clear();
					}
				}
			}
			Framing.BlockStyle blockStyle4 = default(Framing.BlockStyle);
			if (blockStyle10.right)
			{
				Tile tileSafely3 = Framing.GetTileSafely(i + 1, j);
				if (tileSafely3.active() && Framing.WillItBlend(*centerTile.type, *tileSafely3.type))
				{
					blockStyle4 = Framing.FindBlockStyle(tileSafely3);
					if (blockStyle4.left)
					{
						num |= 4;
					}
					else
					{
						blockStyle4.Clear();
					}
				}
			}
			Framing.BlockStyle blockStyle5 = default(Framing.BlockStyle);
			if (blockStyle10.bottom)
			{
				Tile tileSafely4 = Framing.GetTileSafely(i, j + 1);
				if (tileSafely4.active() && Framing.WillItBlend(*centerTile.type, *tileSafely4.type))
				{
					blockStyle5 = Framing.FindBlockStyle(tileSafely4);
					if (blockStyle5.top)
					{
						num |= 8;
					}
					else
					{
						blockStyle5.Clear();
					}
				}
			}
			if (blockStyle2.left && blockStyle3.top)
			{
				Tile tileSafely5 = Framing.GetTileSafely(i - 1, j - 1);
				if (tileSafely5.active() && Framing.WillItBlend(*centerTile.type, *tileSafely5.type))
				{
					Framing.BlockStyle blockStyle6 = Framing.FindBlockStyle(tileSafely5);
					if (blockStyle6.right && blockStyle6.bottom)
					{
						num |= 16;
					}
				}
			}
			if (blockStyle2.right && blockStyle4.top)
			{
				Tile tileSafely6 = Framing.GetTileSafely(i + 1, j - 1);
				if (tileSafely6.active() && Framing.WillItBlend(*centerTile.type, *tileSafely6.type))
				{
					Framing.BlockStyle blockStyle7 = Framing.FindBlockStyle(tileSafely6);
					if (blockStyle7.left && blockStyle7.bottom)
					{
						num |= 32;
					}
				}
			}
			if (blockStyle5.left && blockStyle3.bottom)
			{
				Tile tileSafely7 = Framing.GetTileSafely(i - 1, j + 1);
				if (tileSafely7.active() && Framing.WillItBlend(*centerTile.type, *tileSafely7.type))
				{
					Framing.BlockStyle blockStyle8 = Framing.FindBlockStyle(tileSafely7);
					if (blockStyle8.right && blockStyle8.top)
					{
						num |= 64;
					}
				}
			}
			if (blockStyle5.right && blockStyle4.bottom)
			{
				Tile tileSafely8 = Framing.GetTileSafely(i + 1, j + 1);
				if (tileSafely8.active() && Framing.WillItBlend(*centerTile.type, *tileSafely8.type))
				{
					Framing.BlockStyle blockStyle9 = Framing.FindBlockStyle(tileSafely8);
					if (blockStyle9.left && blockStyle9.top)
					{
						num |= 128;
					}
				}
			}
			if (resetFrame)
			{
				centerTile.frameNumber((byte)WorldGen.genRand.Next(0, 3));
			}
			Point16 point = Framing.selfFrame8WayLookup[num][(int)centerTile.frameNumber()];
			*centerTile.frameX = point.X;
			*centerTile.frameY = point.Y;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0002284C File Offset: 0x00020A4C
		public unsafe static void WallFrame(int i, int j, bool resetFrame = false)
		{
			if (WorldGen.SkipFramingBecauseOfGen || i <= 0 || j <= 0 || i >= Main.maxTilesX - 1 || j >= Main.maxTilesY - 1 || Main.tile[i, j] == null)
			{
				return;
			}
			if ((int)(*Main.tile[i, j].wall) >= WallLoader.WallCount)
			{
				*Main.tile[i, j].wall = 0;
			}
			WorldGen.UpdateMapTile(i, j, true);
			Tile tile = Main.tile[i, j];
			if (*tile.wall == 0)
			{
				tile.wallColor(0);
				tile.ClearWallPaintAndCoating();
				return;
			}
			int num = 0;
			bool flag = Main.ShouldShowInvisibleWalls();
			if (j - 1 >= 0)
			{
				Tile tile2 = Main.tile[i, j - 1];
				if (tile2 != null && (*tile2.wall > 0 || (tile2.active() && TileID.Sets.WallsMergeWith[(int)(*tile2.type)])) && (flag || !tile2.invisibleWall()))
				{
					num = 1;
				}
			}
			if (i - 1 >= 0)
			{
				Tile tile3 = Main.tile[i - 1, j];
				if (tile3 != null && (*tile3.wall > 0 || (tile3.active() && TileID.Sets.WallsMergeWith[(int)(*tile3.type)])) && (flag || !tile3.invisibleWall()))
				{
					num |= 2;
				}
			}
			if (i + 1 <= Main.maxTilesX - 1)
			{
				Tile tile4 = Main.tile[i + 1, j];
				if (tile4 != null && (*tile4.wall > 0 || (tile4.active() && TileID.Sets.WallsMergeWith[(int)(*tile4.type)])) && (flag || !tile4.invisibleWall()))
				{
					num |= 4;
				}
			}
			if (j + 1 <= Main.maxTilesY - 1)
			{
				Tile tile5 = Main.tile[i, j + 1];
				if (tile5 != null && (*tile5.wall > 0 || (tile5.active() && TileID.Sets.WallsMergeWith[(int)(*tile5.type)])) && (flag || !tile5.invisibleWall()))
				{
					num |= 8;
				}
			}
			int num2 = 0;
			if (Main.wallLargeFrames[(int)(*tile.wall)] == 1)
			{
				num2 = Framing.phlebasTileFrameNumberLookup[j % 4][i % 3] - 1;
			}
			else if (Main.wallLargeFrames[(int)(*tile.wall)] == 2)
			{
				num2 = Framing.lazureTileFrameNumberLookup[i % 2][j % 2] - 1;
			}
			else if (resetFrame)
			{
				num2 = WorldGen.genRand.Next(0, 3);
				if (*tile.wall == 21 && WorldGen.genRand.Next(2) == 0)
				{
					num2 = 2;
				}
			}
			else
			{
				num2 = (int)tile.wallFrameNumber();
			}
			if (num == 15)
			{
				num += Framing.centerWallFrameLookup[i % 3][j % 3];
			}
			if (!WallLoader.WallFrame(i, j, (int)(*tile.wall), resetFrame, ref num, ref num2))
			{
				return;
			}
			tile.wallFrameNumber((byte)num2);
			Point16 point = Framing.wallFrameLookup[num][num2];
			tile.wallFrameX((int)point.X);
			tile.wallFrameY((int)point.Y);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00022B34 File Offset: 0x00020D34
		public static Tile GetTileSafely(Vector2 position)
		{
			position /= 16f;
			return Framing.GetTileSafely((int)position.X, (int)position.Y);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00022B56 File Offset: 0x00020D56
		public static Tile GetTileSafely(Point pt)
		{
			return Framing.GetTileSafely(pt.X, pt.Y);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00022B69 File Offset: 0x00020D69
		public static Tile GetTileSafely(Point16 pt)
		{
			return Framing.GetTileSafely((int)pt.X, (int)pt.Y);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00022B7C File Offset: 0x00020D7C
		public static Tile GetTileSafely(int i, int j)
		{
			if (!WorldGen.InWorld(i, j, 0))
			{
				return default(Tile);
			}
			Tile tile = Main.tile[i, j];
			if (tile == null)
			{
				tile = default(Tile);
				Main.tile[i, j] = tile;
			}
			return tile;
		}

		// Token: 0x040001D2 RID: 466
		private static Point16[][] selfFrame8WayLookup;

		// Token: 0x040001D3 RID: 467
		private static Point16[][] wallFrameLookup;

		// Token: 0x040001D4 RID: 468
		private static Point16 frameSize8Way;

		// Token: 0x040001D5 RID: 469
		private static Point16 wallFrameSize;

		// Token: 0x040001D6 RID: 470
		private static Framing.BlockStyle[] blockStyleLookup;

		// Token: 0x040001D7 RID: 471
		private static int[][] phlebasTileFrameNumberLookup;

		// Token: 0x040001D8 RID: 472
		private static int[][] lazureTileFrameNumberLookup;

		// Token: 0x040001D9 RID: 473
		private static int[][] centerWallFrameLookup;

		// Token: 0x02000792 RID: 1938
		private struct BlockStyle
		{
			// Token: 0x06004E8C RID: 20108 RVA: 0x006750B5 File Offset: 0x006732B5
			public BlockStyle(bool up, bool down, bool left, bool right)
			{
				this.top = up;
				this.bottom = down;
				this.left = left;
				this.right = right;
			}

			// Token: 0x06004E8D RID: 20109 RVA: 0x006750D4 File Offset: 0x006732D4
			public void Clear()
			{
				this.top = (this.bottom = (this.left = (this.right = false)));
			}

			// Token: 0x040065A7 RID: 26023
			public bool top;

			// Token: 0x040065A8 RID: 26024
			public bool bottom;

			// Token: 0x040065A9 RID: 26025
			public bool left;

			// Token: 0x040065AA RID: 26026
			public bool right;
		}
	}
}
