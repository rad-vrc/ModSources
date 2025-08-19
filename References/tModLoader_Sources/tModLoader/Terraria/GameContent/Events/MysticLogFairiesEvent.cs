using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Terraria.GameContent.Events
{
	// Token: 0x02000630 RID: 1584
	public class MysticLogFairiesEvent
	{
		// Token: 0x0600457C RID: 17788 RVA: 0x00613230 File Offset: 0x00611430
		public void WorldClear()
		{
			this._canSpawnFairies = false;
			this._delayUntilNextAttempt = 0.0;
			this._stumpCoords.Clear();
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x00613253 File Offset: 0x00611453
		public void StartWorld()
		{
			if (Main.netMode != 1)
			{
				this.ScanWholeOverworldForLogs();
			}
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x00613263 File Offset: 0x00611463
		public void StartNight()
		{
			if (Main.netMode != 1)
			{
				this._canSpawnFairies = true;
				this._delayUntilNextAttempt = 0.0;
				this.ScanWholeOverworldForLogs();
			}
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x0061328C File Offset: 0x0061148C
		public void UpdateTime()
		{
			if (Main.netMode != 1 && this._canSpawnFairies && this.IsAGoodTime())
			{
				this._delayUntilNextAttempt = Math.Max(0.0, this._delayUntilNextAttempt - Main.desiredWorldEventsUpdateRate);
				if (this._delayUntilNextAttempt == 0.0)
				{
					this._delayUntilNextAttempt = 60.0;
					this.TrySpawningFairies();
				}
			}
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x006132F7 File Offset: 0x006114F7
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

		// Token: 0x06004581 RID: 17793 RVA: 0x00613330 File Offset: 0x00611530
		private void TrySpawningFairies()
		{
			if (Main.maxRaining > 0f || Main.bloodMoon || NPC.MoonLordCountdown > 0 || Main.snowMoon || Main.pumpkinMoon || Main.invasionType > 0 || this._stumpCoords.Count == 0)
			{
				return;
			}
			int oneOverSpawnChance = this.GetOneOverSpawnChance();
			bool flag = false;
			for (int i = 0; i < Main.worldEventUpdates; i++)
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
			Point p = this._stumpCoords[index];
			Vector2 vector = p.ToWorldCoordinates(24f, 8f);
			vector.Y -= 50f;
			if (WorldGen.PlayerLOS(p.X, p.Y))
			{
				return;
			}
			int num = Main.rand.Next(1, 4);
			if (Main.rand.Next(7) == 0)
			{
				num++;
			}
			int num2 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
			{
				585,
				584,
				583
			});
			for (int j = 0; j < num; j++)
			{
				num2 = (int)Utils.SelectRandom<short>(Main.rand, new short[]
				{
					585,
					584,
					583
				});
				if (Main.tenthAnniversaryWorld && Main.rand.Next(4) != 0)
				{
					num2 = 583;
				}
				int num3 = NPC.NewNPC(new EntitySource_WorldEvent(null), (int)vector.X, (int)vector.Y, num2, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2 && num3 < 200)
				{
					NetMessage.SendData(23, -1, -1, null, num3, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			this._canSpawnFairies = false;
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x00613503 File Offset: 0x00611703
		public void FallenLogDestroyed()
		{
			if (Main.netMode != 1)
			{
				this.ScanWholeOverworldForLogs();
			}
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x00613514 File Offset: 0x00611714
		private unsafe void ScanWholeOverworldForLogs()
		{
			this._stumpCoords.Clear();
			NPC.fairyLog = false;
			int num = (int)Main.worldSurface - 10;
			int num2 = 100;
			int num3 = Main.maxTilesX - 100;
			if (Main.remixWorld)
			{
				num = Main.maxTilesY - 350;
				num2 = (int)Main.rockLayer;
			}
			int num4 = 3;
			int num5 = 2;
			List<Point> list = new List<Point>();
			for (int i = 100; i < num3; i += num4)
			{
				for (int num6 = num; num6 >= num2; num6 -= num5)
				{
					Tile tile = Main.tile[i, num6];
					if (tile.active() && *tile.type == 488 && *tile.liquid == 0)
					{
						list.Add(new Point(i, num6));
						NPC.fairyLog = true;
					}
				}
			}
			foreach (Point item in list)
			{
				this._stumpCoords.Add(this.GetStumpTopLeft(item));
			}
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x00613628 File Offset: 0x00611828
		private unsafe Point GetStumpTopLeft(Point stumpRandomPoint)
		{
			Tile tile = Main.tile[stumpRandomPoint.X, stumpRandomPoint.Y];
			Point result = stumpRandomPoint;
			result.X -= (int)(*tile.frameX / 18);
			result.Y -= (int)(*tile.frameY / 18);
			return result;
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x0061367C File Offset: 0x0061187C
		private int GetOneOverSpawnChance()
		{
			MoonPhase moonPhase = Main.GetMoonPhase();
			return ((moonPhase != MoonPhase.Full && moonPhase != MoonPhase.Empty) ? 10800 : 3600) / 60;
		}

		// Token: 0x04005AF6 RID: 23286
		private bool _canSpawnFairies;

		// Token: 0x04005AF7 RID: 23287
		private double _delayUntilNextAttempt;

		// Token: 0x04005AF8 RID: 23288
		private const int DELAY_BETWEEN_ATTEMPTS = 60;

		// Token: 0x04005AF9 RID: 23289
		private List<Point> _stumpCoords = new List<Point>();
	}
}
