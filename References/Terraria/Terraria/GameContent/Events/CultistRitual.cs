using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002AE RID: 686
	public class CultistRitual
	{
		// Token: 0x06002179 RID: 8569 RVA: 0x00525994 File Offset: 0x00523B94
		public static void UpdateTime()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			CultistRitual.delay -= Main.dayRate;
			if (CultistRitual.delay < 0)
			{
				CultistRitual.delay = 0;
			}
			CultistRitual.recheck -= Main.dayRate;
			if (CultistRitual.recheck < 0)
			{
				CultistRitual.recheck = 0;
			}
			if (CultistRitual.delay == 0 && CultistRitual.recheck == 0)
			{
				CultistRitual.recheck = 600;
				if (NPC.AnyDanger(false, false))
				{
					CultistRitual.recheck *= 6;
					return;
				}
				CultistRitual.TrySpawning(Main.dungeonX, Main.dungeonY);
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00525A23 File Offset: 0x00523C23
		public static void CultistSlain()
		{
			CultistRitual.delay -= 3600;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00525A35 File Offset: 0x00523C35
		public static void TabletDestroyed()
		{
			CultistRitual.delay = 43200;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x00525A44 File Offset: 0x00523C44
		public static void TrySpawning(int x, int y)
		{
			if (WorldGen.PlayerLOS(x - 6, y) || WorldGen.PlayerLOS(x + 6, y))
			{
				return;
			}
			if (!CultistRitual.CheckRitual(x, y))
			{
				return;
			}
			NPC.NewNPC(new EntitySource_WorldEvent(), x * 16 + 8, (y - 4) * 16 - 8, 437, 0, 0f, 0f, 0f, 0f, 255);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00525AAC File Offset: 0x00523CAC
		private static bool CheckRitual(int x, int y)
		{
			if (CultistRitual.delay != 0 || !Main.hardMode || !NPC.downedGolemBoss || !NPC.downedBoss3)
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
			Point[] array = null;
			return CultistRitual.CheckFloor(center, out array);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x00525B28 File Offset: 0x00523D28
		public static bool CheckFloor(Vector2 Center, out Point[] spawnPoints)
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
						if ((WorldGen.SolidTile(num2, num3, false) || TileID.Sets.Platforms[(int)Framing.GetTileSafely(num2, num3).type]) && (!Collision.SolidTiles(num2 - 1, num2 + 1, num3 - 3, num3 - 1) || (!Collision.SolidTiles(num2, num2, num3 - 3, num3 - 1) && !Collision.SolidTiles(num2 + 1, num2 + 1, num3 - 3, num3 - 2) && !Collision.SolidTiles(num2 - 1, num2 - 1, num3 - 3, num3 - 2))))
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

		// Token: 0x04004748 RID: 18248
		public const int delayStart = 86400;

		// Token: 0x04004749 RID: 18249
		public const int respawnDelay = 43200;

		// Token: 0x0400474A RID: 18250
		private const int timePerCultist = 3600;

		// Token: 0x0400474B RID: 18251
		private const int recheckStart = 600;

		// Token: 0x0400474C RID: 18252
		public static int delay;

		// Token: 0x0400474D RID: 18253
		public static int recheck;
	}
}
