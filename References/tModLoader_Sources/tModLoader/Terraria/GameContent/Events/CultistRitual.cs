using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Events
{
	// Token: 0x0200062C RID: 1580
	public class CultistRitual
	{
		// Token: 0x06004521 RID: 17697 RVA: 0x0060ED30 File Offset: 0x0060CF30
		public static void UpdateTime()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			CultistRitual.delay -= Main.desiredWorldEventsUpdateRate;
			if (CultistRitual.delay < 0.0)
			{
				CultistRitual.delay = 0.0;
			}
			CultistRitual.recheck -= Main.desiredWorldEventsUpdateRate;
			if (CultistRitual.recheck < 0.0)
			{
				CultistRitual.recheck = 0.0;
			}
			if (CultistRitual.delay == 0.0 && CultistRitual.recheck == 0.0)
			{
				CultistRitual.recheck = 600.0;
				if (NPC.AnyDanger(false, false))
				{
					CultistRitual.recheck *= 6.0;
					return;
				}
				CultistRitual.TrySpawning(Main.dungeonX, Main.dungeonY);
			}
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x0060EDFD File Offset: 0x0060CFFD
		public static void CultistSlain()
		{
			CultistRitual.delay -= 3600.0;
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x0060EE13 File Offset: 0x0060D013
		public static void TabletDestroyed()
		{
			CultistRitual.delay = 43200.0;
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x0060EE24 File Offset: 0x0060D024
		public static void TrySpawning(int x, int y)
		{
			if (!WorldGen.PlayerLOS(x - 6, y) && !WorldGen.PlayerLOS(x + 6, y) && CultistRitual.CheckRitual(x, y))
			{
				NPC.NewNPC(new EntitySource_WorldEvent(null), x * 16 + 8, (y - 4) * 16 - 8, 437, 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0060EE8C File Offset: 0x0060D08C
		private static bool CheckRitual(int x, int y)
		{
			if (CultistRitual.delay != 0.0 || !Main.hardMode || !NPC.downedGolemBoss || !NPC.downedBoss3)
			{
				return false;
			}
			if (y < 7 || WorldGen.SolidTile(Main.tile[x, y - 7]))
			{
				return false;
			}
			if (NPC.AnyNPCs(437))
			{
				return false;
			}
			Vector2 center = new Vector2((float)(x * 16 + 8), (float)(y * 16 - 64 - 8 - 27));
			Point[] spawnPoints = null;
			return CultistRitual.CheckFloor(center, out spawnPoints);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0060EF10 File Offset: 0x0060D110
		public unsafe static bool CheckFloor(Vector2 Center, out Point[] spawnPoints)
		{
			Point[] array = new Point[4];
			int num = 0;
			Point point = Center.ToTileCoordinates();
			for (int i = -5; i <= 5; i += 2)
			{
				if (i != -1 && i != 1)
				{
					for (int j = -5; j < 12; j++)
					{
						int num2 = point.X + i * 2;
						int num3 = point.Y + j;
						if ((WorldGen.SolidTile(num2, num3, false) || TileID.Sets.Platforms[(int)(*Framing.GetTileSafely(num2, num3).type)]) && (!Collision.SolidTiles(num2 - 1, num2 + 1, num3 - 3, num3 - 1) || (!Collision.SolidTiles(num2, num2, num3 - 3, num3 - 1) && !Collision.SolidTiles(num2 + 1, num2 + 1, num3 - 3, num3 - 2) && !Collision.SolidTiles(num2 - 1, num2 - 1, num3 - 3, num3 - 2))))
						{
							array[num++] = new Point(num2, num3);
							break;
						}
					}
				}
			}
			if (num != 4)
			{
				spawnPoints = null;
				return false;
			}
			spawnPoints = array;
			return true;
		}

		// Token: 0x04005AD0 RID: 23248
		public const int delayStart = 86400;

		// Token: 0x04005AD1 RID: 23249
		public const int respawnDelay = 43200;

		// Token: 0x04005AD2 RID: 23250
		private const int timePerCultist = 3600;

		// Token: 0x04005AD3 RID: 23251
		private const int recheckStart = 600;

		// Token: 0x04005AD4 RID: 23252
		public static double delay;

		// Token: 0x04005AD5 RID: 23253
		public static double recheck;
	}
}
