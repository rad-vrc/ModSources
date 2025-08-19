using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000079 RID: 121
	public static class WorldUtils
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x0048E268 File Offset: 0x0048C468
		public static Rectangle ClampToWorld(Rectangle tileRectangle)
		{
			int num = Math.Max(0, Math.Min(tileRectangle.Left, Main.maxTilesX));
			int num2 = Math.Max(0, Math.Min(tileRectangle.Top, Main.maxTilesY));
			int num3 = Math.Max(0, Math.Min(tileRectangle.Right, Main.maxTilesX));
			int num4 = Math.Max(0, Math.Min(tileRectangle.Bottom, Main.maxTilesY));
			return new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0048E2E2 File Offset: 0x0048C4E2
		public static bool Gen(Point origin, GenShape shape, GenAction action)
		{
			return shape.Perform(origin, action);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0048E2EC File Offset: 0x0048C4EC
		public static bool Gen(Point origin, GenShapeActionPair pair)
		{
			return pair.Shape.Perform(origin, pair.Action);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0048E300 File Offset: 0x0048C500
		public static bool Find(Point origin, GenSearch search, out Point result)
		{
			result = search.Find(origin);
			return !(result == GenSearch.NOT_FOUND);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0048E324 File Offset: 0x0048C524
		public static void ClearTile(int x, int y, bool frameNeighbors = false)
		{
			Main.tile[x, y].ClearTile();
			if (frameNeighbors)
			{
				WorldGen.TileFrame(x + 1, y, false, false);
				WorldGen.TileFrame(x - 1, y, false, false);
				WorldGen.TileFrame(x, y + 1, false, false);
				WorldGen.TileFrame(x, y - 1, false, false);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0048E371 File Offset: 0x0048C571
		public static void ClearWall(int x, int y, bool frameNeighbors = false)
		{
			Main.tile[x, y].wall = 0;
			if (frameNeighbors)
			{
				WorldGen.SquareWallFrame(x + 1, y, true);
				WorldGen.SquareWallFrame(x - 1, y, true);
				WorldGen.SquareWallFrame(x, y + 1, true);
				WorldGen.SquareWallFrame(x, y - 1, true);
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0048E3B0 File Offset: 0x0048C5B0
		public static void TileFrame(int x, int y, bool frameNeighbors = false)
		{
			WorldGen.TileFrame(x, y, true, false);
			if (frameNeighbors)
			{
				WorldGen.TileFrame(x + 1, y, true, false);
				WorldGen.TileFrame(x - 1, y, true, false);
				WorldGen.TileFrame(x, y + 1, true, false);
				WorldGen.TileFrame(x, y - 1, true, false);
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0048E3EA File Offset: 0x0048C5EA
		public static void ClearChestLocation(int x, int y)
		{
			WorldUtils.ClearTile(x, y, true);
			WorldUtils.ClearTile(x - 1, y, true);
			WorldUtils.ClearTile(x, y - 1, true);
			WorldUtils.ClearTile(x - 1, y - 1, true);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0048E414 File Offset: 0x0048C614
		public static void WireLine(Point start, Point end)
		{
			Point point = start;
			Point point2 = end;
			if (end.X < start.X)
			{
				Utils.Swap<int>(ref end.X, ref start.X);
			}
			if (end.Y < start.Y)
			{
				Utils.Swap<int>(ref end.Y, ref start.Y);
			}
			for (int i = start.X; i <= end.X; i++)
			{
				WorldGen.PlaceWire(i, point.Y);
			}
			for (int j = start.Y; j <= end.Y; j++)
			{
				WorldGen.PlaceWire(point2.X, j);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0048E4AD File Offset: 0x0048C6AD
		public static void DebugRegen()
		{
			WorldGen.clearWorld();
			WorldGen.GenerateWorld(Main.ActiveWorldFileData.Seed, null);
			Main.NewText("World Regen Complete.", byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0048E4E0 File Offset: 0x0048C6E0
		public static void DebugRotate()
		{
			int num = 0;
			int num2 = 0;
			int maxTilesY = Main.maxTilesY;
			for (int i = 0; i < Main.maxTilesX / Main.maxTilesY; i++)
			{
				for (int j = 0; j < maxTilesY / 2; j++)
				{
					for (int k = j; k < maxTilesY - j; k++)
					{
						Tile tile = Main.tile[k + num, j + num2];
						Main.tile[k + num, j + num2] = Main.tile[j + num, maxTilesY - k + num2];
						Main.tile[j + num, maxTilesY - k + num2] = Main.tile[maxTilesY - k + num, maxTilesY - j + num2];
						Main.tile[maxTilesY - k + num, maxTilesY - j + num2] = Main.tile[maxTilesY - j + num, k + num2];
						Main.tile[maxTilesY - j + num, k + num2] = tile;
					}
				}
				num += maxTilesY;
			}
		}
	}
}
