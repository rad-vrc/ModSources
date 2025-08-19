using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000088 RID: 136
	public static class WorldUtils
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x004A1110 File Offset: 0x0049F310
		public static Rectangle ClampToWorld(Rectangle tileRectangle)
		{
			int num = Math.Max(0, Math.Min(tileRectangle.Left, Main.maxTilesX));
			int num2 = Math.Max(0, Math.Min(tileRectangle.Top, Main.maxTilesY));
			int num3 = Math.Max(0, Math.Min(tileRectangle.Right, Main.maxTilesX));
			int num4 = Math.Max(0, Math.Min(tileRectangle.Bottom, Main.maxTilesY));
			return new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x004A118A File Offset: 0x0049F38A
		public static bool Gen(Point origin, GenShape shape, GenAction action)
		{
			return shape.Perform(origin, action);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x004A1194 File Offset: 0x0049F394
		public static bool Gen(Point origin, GenShapeActionPair pair)
		{
			return pair.Shape.Perform(origin, pair.Action);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x004A11A8 File Offset: 0x0049F3A8
		public static bool Find(Point origin, GenSearch search, out Point result)
		{
			result = search.Find(origin);
			return !(result == GenSearch.NOT_FOUND);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x004A11CC File Offset: 0x0049F3CC
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

		// Token: 0x0600142E RID: 5166 RVA: 0x004A121C File Offset: 0x0049F41C
		public unsafe static void ClearWall(int x, int y, bool frameNeighbors = false)
		{
			*Main.tile[x, y].wall = 0;
			if (frameNeighbors)
			{
				WorldGen.SquareWallFrame(x + 1, y, true);
				WorldGen.SquareWallFrame(x - 1, y, true);
				WorldGen.SquareWallFrame(x, y + 1, true);
				WorldGen.SquareWallFrame(x, y - 1, true);
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x004A126A File Offset: 0x0049F46A
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

		// Token: 0x06001430 RID: 5168 RVA: 0x004A12A4 File Offset: 0x0049F4A4
		public static void ClearChestLocation(int x, int y)
		{
			WorldUtils.ClearTile(x, y, true);
			WorldUtils.ClearTile(x - 1, y, true);
			WorldUtils.ClearTile(x, y - 1, true);
			WorldUtils.ClearTile(x - 1, y - 1, true);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x004A12D0 File Offset: 0x0049F4D0
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

		// Token: 0x06001432 RID: 5170 RVA: 0x004A1369 File Offset: 0x0049F569
		public static void DebugRegen()
		{
			WorldGen.clearWorld();
			WorldGen.GenerateWorld(Main.ActiveWorldFileData.Seed, null);
			Main.NewText("World Regen Complete.", byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x004A139C File Offset: 0x0049F59C
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
