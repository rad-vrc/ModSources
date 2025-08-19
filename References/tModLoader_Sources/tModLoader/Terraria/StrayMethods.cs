using System;
using Microsoft.Xna.Framework;

namespace Terraria
{
	// Token: 0x02000056 RID: 86
	public class StrayMethods
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x003FBDC8 File Offset: 0x003F9FC8
		public unsafe static bool CountSandHorizontally(int i, int j, bool[] fittingTypes, int requiredTotalSpread = 4, int spreadInEachAxis = 5)
		{
			if (!WorldGen.InWorld(i, j, 2))
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			int num3 = i - 1;
			while (num < spreadInEachAxis && num3 > 0)
			{
				Tile tile = Main.tile[num3, j];
				if (tile.active() && fittingTypes[(int)(*tile.type)] && !WorldGen.SolidTileAllowBottomSlope(num3, j - 1))
				{
					num++;
				}
				else if (!tile.active())
				{
					break;
				}
				num3--;
			}
			num3 = i + 1;
			while (num2 < spreadInEachAxis && num3 < Main.maxTilesX - 1)
			{
				Tile tile2 = Main.tile[num3, j];
				if (tile2.active() && fittingTypes[(int)(*tile2.type)] && !WorldGen.SolidTileAllowBottomSlope(num3, j - 1))
				{
					num2++;
				}
				else if (!tile2.active())
				{
					break;
				}
				num3++;
			}
			return num + num2 + 1 >= requiredTotalSpread;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x003FBE98 File Offset: 0x003FA098
		public static bool CanSpawnSandstormHostile(Vector2 position, int expandUp, int expandDown)
		{
			bool result = true;
			Point point = position.ToTileCoordinates();
			for (int i = -1; i <= 1; i++)
			{
				int topY;
				int bottomY;
				Collision.ExpandVertically(point.X + i, point.Y, out topY, out bottomY, expandUp, expandDown);
				topY++;
				bottomY--;
				if (bottomY - topY < 20)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x003FBEEC File Offset: 0x003FA0EC
		public static bool CanSpawnSandstormFriendly(Vector2 position, int expandUp, int expandDown)
		{
			bool result = true;
			Point point = position.ToTileCoordinates();
			for (int i = -1; i <= 1; i++)
			{
				int topY;
				int bottomY;
				Collision.ExpandVertically(point.X + i, point.Y, out topY, out bottomY, expandUp, expandDown);
				topY++;
				bottomY--;
				if (bottomY - topY < 10)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x003FBF40 File Offset: 0x003FA140
		public static void CheckArenaScore(Vector2 arenaCenter, out Point xLeftEnd, out Point xRightEnd, int walkerWidthInTiles = 5, int walkerHeightInTiles = 10)
		{
			bool flag = false;
			Point point = arenaCenter.ToTileCoordinates();
			xLeftEnd = (xRightEnd = point);
			int num;
			int bottomY;
			Collision.ExpandVertically(point.X, point.Y, out num, out bottomY, 0, 4);
			point.Y = bottomY;
			if (flag)
			{
				Dust.QuickDust(point, Color.Blue).scale = 5f;
			}
			Point lastIteratedFloorSpot;
			StrayMethods.SendWalker(point, walkerHeightInTiles, -1, out num, out lastIteratedFloorSpot, 120, flag);
			Point lastIteratedFloorSpot2;
			StrayMethods.SendWalker(point, walkerHeightInTiles, 1, out num, out lastIteratedFloorSpot2, 120, flag);
			lastIteratedFloorSpot.X++;
			lastIteratedFloorSpot2.X--;
			if (flag)
			{
				Dust.QuickDustLine(lastIteratedFloorSpot.ToWorldCoordinates(8f, 8f), lastIteratedFloorSpot2.ToWorldCoordinates(8f, 8f), 50f, Color.Pink);
			}
			xLeftEnd = lastIteratedFloorSpot;
			xRightEnd = lastIteratedFloorSpot2;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x003FC01C File Offset: 0x003FA21C
		public static void SendWalker(Point startFloorPosition, int height, int direction, out int distanceCoveredInTiles, out Point lastIteratedFloorSpot, int maxDistance = 100, bool showDebug = false)
		{
			distanceCoveredInTiles = 0;
			startFloorPosition.Y--;
			lastIteratedFloorSpot = startFloorPosition;
			for (int i = 0; i < maxDistance; i++)
			{
				int j = 0;
				while (j < 3 && WorldGen.SolidTile3(startFloorPosition.X, startFloorPosition.Y))
				{
					startFloorPosition.Y--;
					j++;
				}
				int topY;
				int bottomY;
				Collision.ExpandVertically(startFloorPosition.X, startFloorPosition.Y, out topY, out bottomY, height, 2);
				topY++;
				bottomY--;
				if (!WorldGen.SolidTile3(startFloorPosition.X, bottomY + 1))
				{
					int topY2;
					int bottomY2;
					Collision.ExpandVertically(startFloorPosition.X, bottomY, out topY2, out bottomY2, 0, 6);
					if (showDebug)
					{
						Dust.QuickBox(new Vector2((float)(startFloorPosition.X * 16 + 8), (float)(topY2 * 16)), new Vector2((float)(startFloorPosition.X * 16 + 8), (float)(bottomY2 * 16)), 1, Color.Blue, null);
					}
					if (!WorldGen.SolidTile3(startFloorPosition.X, bottomY2))
					{
						break;
					}
				}
				if (bottomY - topY < height - 1)
				{
					break;
				}
				if (showDebug)
				{
					Dust.QuickDust(startFloorPosition, Color.Green).scale = 1f;
					Dust.QuickBox(new Vector2((float)(startFloorPosition.X * 16 + 8), (float)(topY * 16)), new Vector2((float)(startFloorPosition.X * 16 + 8), (float)(bottomY * 16 + 16)), 1, Color.Red, null);
				}
				distanceCoveredInTiles += direction;
				startFloorPosition.X += direction;
				startFloorPosition.Y = bottomY;
				lastIteratedFloorSpot = startFloorPosition;
				if (Math.Abs(distanceCoveredInTiles) >= maxDistance)
				{
					break;
				}
			}
			distanceCoveredInTiles = Math.Abs(distanceCoveredInTiles);
		}
	}
}
