using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020001EA RID: 490
	public class TeleportHelpers
	{
		// Token: 0x06001CBB RID: 7355 RVA: 0x004FCC20 File Offset: 0x004FAE20
		public static bool RequestMagicConchTeleportPosition(Player player, int crawlOffsetX, int startX, out Point landingPoint)
		{
			landingPoint = default(Point);
			Point point = new Point(startX, 50);
			int num = 1;
			int num2 = -1;
			int num3 = 1;
			int num4 = 0;
			int num5 = 5000;
			Vector2 value = new Vector2((float)player.width * 0.5f, (float)player.height);
			int num6 = 40;
			bool flag = WorldGen.SolidOrSlopedTile(Main.tile[point.X, point.Y]);
			int num7 = 0;
			int num8 = 400;
			while (num4 < num5 && num7 < num8)
			{
				num4++;
				Tile tile = Main.tile[point.X, point.Y];
				Tile tile2 = Main.tile[point.X, point.Y + num3];
				bool flag2 = WorldGen.SolidOrSlopedTile(tile) || tile.liquid > 0;
				bool flag3 = WorldGen.SolidOrSlopedTile(tile2) || tile2.liquid > 0;
				if (TeleportHelpers.IsInSolidTilesExtended(new Vector2((float)(point.X * 16 + 8), (float)(point.Y * 16 + 15)) - value, player.velocity, player.width, player.height, (int)player.gravDir))
				{
					if (flag)
					{
						point.Y += num;
					}
					else
					{
						point.Y += num2;
					}
				}
				else if (flag2)
				{
					if (flag)
					{
						point.Y += num;
					}
					else
					{
						point.Y += num2;
					}
				}
				else
				{
					flag = false;
					if (!TeleportHelpers.IsInSolidTilesExtended(new Vector2((float)(point.X * 16 + 8), (float)(point.Y * 16 + 15 + 16)) - value, player.velocity, player.width, player.height, (int)player.gravDir) && !flag3 && (double)point.Y < Main.worldSurface)
					{
						point.Y += num;
					}
					else if (tile2.liquid > 0)
					{
						point.X += crawlOffsetX;
						num7++;
					}
					else if (TeleportHelpers.TileIsDangerous(point.X, point.Y))
					{
						point.X += crawlOffsetX;
						num7++;
					}
					else if (TeleportHelpers.TileIsDangerous(point.X, point.Y + num3))
					{
						point.X += crawlOffsetX;
						num7++;
					}
					else
					{
						if (point.Y >= num6)
						{
							break;
						}
						point.Y += num;
					}
				}
			}
			if (num4 == num5 || num7 >= num8)
			{
				return false;
			}
			if (!WorldGen.InWorld(point.X, point.Y, 40))
			{
				return false;
			}
			landingPoint = point;
			return true;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x004FCEC4 File Offset: 0x004FB0C4
		private static bool TileIsDangerous(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return (tile.liquid > 0 && tile.lava()) || (tile.wall == 87 && (double)y > Main.worldSurface && !NPC.downedPlantBoss) || (Main.wallDungeon[(int)tile.wall] && (double)y > Main.worldSurface && !NPC.downedBoss3);
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x004FCF30 File Offset: 0x004FB130
		private static bool IsInSolidTilesExtended(Vector2 testPosition, Vector2 playerVelocity, int width, int height, int gravDir)
		{
			if (Collision.LavaCollision(testPosition, width, height))
			{
				return true;
			}
			if (Collision.AnyHurtingTiles(testPosition, width, height))
			{
				return true;
			}
			if (Collision.SolidCollision(testPosition, width, height))
			{
				return true;
			}
			Vector2 vector = Vector2.UnitX * 16f;
			if (Collision.TileCollision(testPosition - vector, vector, width, height, false, false, gravDir) != vector)
			{
				return true;
			}
			vector = -Vector2.UnitX * 16f;
			if (Collision.TileCollision(testPosition - vector, vector, width, height, false, false, gravDir) != vector)
			{
				return true;
			}
			vector = Vector2.UnitY * 16f;
			if (Collision.TileCollision(testPosition - vector, vector, width, height, false, false, gravDir) != vector)
			{
				return true;
			}
			vector = -Vector2.UnitY * 16f;
			return Collision.TileCollision(testPosition - vector, vector, width, height, false, false, gravDir) != vector;
		}
	}
}
