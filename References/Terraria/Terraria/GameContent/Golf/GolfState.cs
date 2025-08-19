using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent.Golf
{
	// Token: 0x020002A4 RID: 676
	public class GolfState
	{
		// Token: 0x060020E1 RID: 8417 RVA: 0x0051FB6E File Offset: 0x0051DD6E
		private void UpdateScoreTime()
		{
			if (this.golfScoreTime < this.golfScoreTimeMax)
			{
				this.golfScoreTime++;
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0051FB8C File Offset: 0x0051DD8C
		public void ResetScoreTime()
		{
			this.golfScoreTime = 0;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0051FB95 File Offset: 0x0051DD95
		public void SetScoreTime()
		{
			this.golfScoreTime = this.golfScoreTimeMax;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0051FBA3 File Offset: 0x0051DDA3
		public float ScoreAdjustment
		{
			get
			{
				return (float)this.golfScoreTime / (float)this.golfScoreTimeMax;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0051FBB4 File Offset: 0x0051DDB4
		public bool ShouldScoreHole
		{
			get
			{
				return this.golfScoreTime >= this.golfScoreDelay;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0051FBC7 File Offset: 0x0051DDC7
		public bool IsTrackingBall
		{
			get
			{
				return this.GetLastHitBall() != null && this._waitingForBallToSettle;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0051FBDC File Offset: 0x0051DDDC
		public bool ShouldCameraTrackBallLastKnownLocation
		{
			get
			{
				return this._lastRecordedBallTime + 2.0 >= Main.gameTimeCache.TotalGameTime.TotalSeconds && this.GetLastHitBall() == null;
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0051FC18 File Offset: 0x0051DE18
		public Vector2? GetLastBallLocation()
		{
			return this._lastRecordedBallLocation;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x0051FC20 File Offset: 0x0051DE20
		public void WorldClear()
		{
			this._lastHitGolfBall = null;
			this._lastRecordedBallLocation = null;
			this._lastRecordedBallTime = 0.0;
			this._lastRecordedSwingCount = 0;
			this._waitingForBallToSettle = false;
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0051FC52 File Offset: 0x0051DE52
		public void CancelBallTracking()
		{
			this._waitingForBallToSettle = false;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x0051FC5C File Offset: 0x0051DE5C
		public void RecordSwing(Projectile golfBall)
		{
			this._lastSwingPosition = golfBall.position;
			this._lastHitGolfBall = golfBall;
			this._lastRecordedSwingCount = (int)golfBall.ai[1];
			this._waitingForBallToSettle = true;
			int golfBallId = this.GetGolfBallId(golfBall);
			if (this._hitRecords[golfBallId] == null || this._lastRecordedSwingCount == 1)
			{
				this._hitRecords[golfBallId] = new GolfBallTrackRecord();
			}
			this._hitRecords[golfBallId].RecordHit(golfBall.position);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x0051FCCD File Offset: 0x0051DECD
		private int GetGolfBallId(Projectile golfBall)
		{
			return golfBall.whoAmI;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0051FCD8 File Offset: 0x0051DED8
		public Projectile GetLastHitBall()
		{
			if (this._lastHitGolfBall == null || !this._lastHitGolfBall.active || !ProjectileID.Sets.IsAGolfBall[this._lastHitGolfBall.type] || this._lastHitGolfBall.owner != Main.myPlayer || this._lastRecordedSwingCount != (int)this._lastHitGolfBall.ai[1])
			{
				return null;
			}
			return this._lastHitGolfBall;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0051FD40 File Offset: 0x0051DF40
		public void Update()
		{
			this.UpdateScoreTime();
			Projectile lastHitBall = this.GetLastHitBall();
			if (lastHitBall == null)
			{
				this._waitingForBallToSettle = false;
				return;
			}
			if (this._waitingForBallToSettle)
			{
				this._waitingForBallToSettle = ((int)lastHitBall.localAI[1] == 1);
			}
			bool flag = false;
			int type = Main.LocalPlayer.HeldItem.type;
			if (type == 3611)
			{
				flag = true;
			}
			if (!Item.IsAGolfingItem(Main.LocalPlayer.HeldItem) && !flag)
			{
				this._waitingForBallToSettle = false;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0051FDB8 File Offset: 0x0051DFB8
		public void RecordBallInfo(Projectile golfBall)
		{
			if (this.GetLastHitBall() != golfBall || !this._waitingForBallToSettle)
			{
				return;
			}
			this._lastRecordedBallLocation = new Vector2?(golfBall.Center);
			this._lastRecordedBallTime = Main.gameTimeCache.TotalGameTime.TotalSeconds;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0051FE00 File Offset: 0x0051E000
		public void LandBall(Projectile golfBall)
		{
			int golfBallId = this.GetGolfBallId(golfBall);
			GolfBallTrackRecord golfBallTrackRecord = this._hitRecords[golfBallId];
			if (golfBallTrackRecord == null)
			{
				return;
			}
			golfBallTrackRecord.RecordHit(golfBall.position);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0051FE30 File Offset: 0x0051E030
		public int GetGolfBallScore(Projectile golfBall)
		{
			int golfBallId = this.GetGolfBallId(golfBall);
			GolfBallTrackRecord golfBallTrackRecord = this._hitRecords[golfBallId];
			if (golfBallTrackRecord == null)
			{
				return 0;
			}
			return (int)((float)golfBallTrackRecord.GetAccumulatedScore() * this.ScoreAdjustment);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0051FE64 File Offset: 0x0051E064
		public void ResetGolfBall()
		{
			Projectile lastHitBall = this.GetLastHitBall();
			if (lastHitBall == null)
			{
				return;
			}
			if (Vector2.Distance(lastHitBall.position, this._lastSwingPosition) < 1f)
			{
				return;
			}
			lastHitBall.position = this._lastSwingPosition;
			lastHitBall.velocity = Vector2.Zero;
			lastHitBall.ai[1] += 1f;
			lastHitBall.netUpdate2 = true;
			this._lastRecordedSwingCount = (int)lastHitBall.ai[1];
		}

		// Token: 0x04004708 RID: 18184
		private const int BALL_RETURN_PENALTY = 1;

		// Token: 0x04004709 RID: 18185
		private int golfScoreTime;

		// Token: 0x0400470A RID: 18186
		private int golfScoreTimeMax = 3600;

		// Token: 0x0400470B RID: 18187
		private int golfScoreDelay = 90;

		// Token: 0x0400470C RID: 18188
		private double _lastRecordedBallTime;

		// Token: 0x0400470D RID: 18189
		private Vector2? _lastRecordedBallLocation;

		// Token: 0x0400470E RID: 18190
		private bool _waitingForBallToSettle;

		// Token: 0x0400470F RID: 18191
		private Vector2 _lastSwingPosition;

		// Token: 0x04004710 RID: 18192
		private Projectile _lastHitGolfBall;

		// Token: 0x04004711 RID: 18193
		private int _lastRecordedSwingCount;

		// Token: 0x04004712 RID: 18194
		private GolfBallTrackRecord[] _hitRecords = new GolfBallTrackRecord[1000];
	}
}
