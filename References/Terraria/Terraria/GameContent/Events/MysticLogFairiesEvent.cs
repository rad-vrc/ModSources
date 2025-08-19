using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.GameContent.Events
{
	// Token: 0x020002A7 RID: 679
	public class MysticLogFairiesEvent
	{
		// Token: 0x06002107 RID: 8455 RVA: 0x0052023E File Offset: 0x0051E43E
		public void WorldClear()
		{
			this._canSpawnFairies = false;
			this._delayUntilNextAttempt = 0;
			this._stumpCoords.Clear();
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00520259 File Offset: 0x0051E459
		public void StartWorld()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this.ScanWholeOverworldForLogs();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0052026A File Offset: 0x0051E46A
		public void StartNight()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this._canSpawnFairies = true;
			this._delayUntilNextAttempt = 0;
			this.ScanWholeOverworldForLogs();
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0052028C File Offset: 0x0051E48C
		public void UpdateTime()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (!this._canSpawnFairies || !this.IsAGoodTime())
			{
				return;
			}
			this._delayUntilNextAttempt = Math.Max(0, this._delayUntilNextAttempt - Main.dayRate);
			if (this._delayUntilNextAttempt == 0)
			{
				this._delayUntilNextAttempt = 60;
				this.TrySpawningFairies();
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x005202E1 File Offset: 0x0051E4E1
		private bool IsAGoodTime()
		{
			if (Main.dayTime)
			{
				return false;
			}
			if (!Main.remixWorld)
			{
				if (Main.time < 6480.0000965595245)
				{
					return false;
				}
				if (Main.time > 25920.000386238098)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00520318 File Offset: 0x0051E518
		private void TrySpawningFairies()
		{
			if (Main.maxRaining > 0f || Main.bloodMoon || NPC.MoonLordCountdown > 0 || Main.snowMoon || Main.pumpkinMoon || Main.invasionType > 0)
			{
				return;
			}
			if (this._stumpCoords.Count == 0)
			{
				return;
			}
			int oneOverSpawnChance = this.GetOneOverSpawnChance();
			bool flag = false;
			for (int i = 0; i < Main.dayRate; i++)
			{
				if (Main.rand.Next(oneOverSpawnChance) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			int index = Main.rand.Next(this._stumpCoords.Count);
			Point point = this._stumpCoords[index];
			Vector2 vector = point.ToWorldCoordinates(24f, 8f);
			vector.Y -= 50f;
			if (WorldGen.PlayerLOS(point.X, point.Y))
			{
				return;
			}
			int num = Main.rand.Next(1, 4);
			if (Main.rand.Next(7) == 0)
			{
				num++;
			}
			int type = (int)Utils.SelectRandom<short>(Main.rand, new short[]
			{
				585,
				584,
				583
			});
			for (int j = 0; j < num; j++)
			{
				type = (int)Utils.SelectRandom<short>(Main.rand, new short[]
				{
					585,
					584,
					583
				});
				if (Main.tenthAnniversaryWorld && Main.rand.Next(4) != 0)
				{
					type = 583;
				}
				int num2 = NPC.NewNPC(new EntitySource_WorldEvent(), (int)vector.X, (int)vector.Y, type, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2 && num2 < 200)
				{
					NetMessage.SendData(23, -1, -1, null, num2, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			this._canSpawnFairies = false;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00520259 File Offset: 0x0051E459
		public void FallenLogDestroyed()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this.ScanWholeOverworldForLogs();
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x005204EC File Offset: 0x0051E6EC
		private void ScanWholeOverworldForLogs()
		{
			this._stumpCoords.Clear();
			NPC.fairyLog = false;
			int num = (int)Main.worldSurface - 10;
			int num2 = 100;
			int num3 = 100;
			int num4 = Main.maxTilesX - 100;
			if (Main.remixWorld)
			{
				num = Main.maxTilesY - 350;
				num2 = (int)Main.rockLayer;
			}
			int num5 = 3;
			int num6 = 2;
			List<Point> list = new List<Point>();
			for (int i = num3; i < num4; i += num5)
			{
				for (int j = num; j >= num2; j -= num6)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && tile.type == 488 && tile.liquid == 0)
					{
						list.Add(new Point(i, j));
						NPC.fairyLog = true;
					}
				}
			}
			foreach (Point stumpRandomPoint in list)
			{
				this._stumpCoords.Add(this.GetStumpTopLeft(stumpRandomPoint));
			}
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00520600 File Offset: 0x0051E800
		private Point GetStumpTopLeft(Point stumpRandomPoint)
		{
			Tile tile = Main.tile[stumpRandomPoint.X, stumpRandomPoint.Y];
			Point result = stumpRandomPoint;
			result.X -= (int)(tile.frameX / 18);
			result.Y -= (int)(tile.frameY / 18);
			return result;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00520650 File Offset: 0x0051E850
		private int GetOneOverSpawnChance()
		{
			MoonPhase moonPhase = Main.GetMoonPhase();
			int num;
			if (moonPhase == MoonPhase.Full || moonPhase == MoonPhase.Empty)
			{
				num = 3600;
			}
			else
			{
				num = 10800;
			}
			return num / 60;
		}

		// Token: 0x0400471A RID: 18202
		private bool _canSpawnFairies;

		// Token: 0x0400471B RID: 18203
		private int _delayUntilNextAttempt;

		// Token: 0x0400471C RID: 18204
		private const int DELAY_BETWEEN_ATTEMPTS = 60;

		// Token: 0x0400471D RID: 18205
		private List<Point> _stumpCoords = new List<Point>();
	}
}
