using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent.Golf
{
	// Token: 0x0200061D RID: 1565
	public class GolfState
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x0060BED7 File Offset: 0x0060A0D7
		public float ScoreAdjustment
		{
			get
			{
				return (float)this.golfScoreTime / (float)this.golfScoreTimeMax;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x0060BEE8 File Offset: 0x0060A0E8
		public bool ShouldScoreHole
		{
			get
			{
				return this.golfScoreTime >= this.golfScoreDelay;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x0060BEFB File Offset: 0x0060A0FB
		public bool IsTrackingBall
		{
			get
			{
				return this.GetLastHitBall() != null && this._waitingForBallToSettle;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x0060BF10 File Offset: 0x0060A110
		public bool ShouldCameraTrackBallLastKnownLocation
		{
			get
			{
				return this._lastRecordedBallTime + 2.0 >= Main.gameTimeCache.TotalGameTime.TotalSeconds && this.GetLastHitBall() == null;
			}
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x0060BF4C File Offset: 0x0060A14C
		private void UpdateScoreTime()
		{
			if (this.golfScoreTime < this.golfScoreTimeMax)
			{
				this.golfScoreTime++;
			}
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0060BF6A File Offset: 0x0060A16A
		public void ResetScoreTime()
		{
			this.golfScoreTime = 0;
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0060BF73 File Offset: 0x0060A173
		public void SetScoreTime()
		{
			this.golfScoreTime = this.golfScoreTimeMax;
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0060BF81 File Offset: 0x0060A181
		public Vector2? GetLastBallLocation()
		{
			return this._lastRecordedBallLocation;
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x0060BF89 File Offset: 0x0060A189
		public void WorldClear()
		{
			this._lastHitGolfBall = null;
			this._lastRecordedBallLocation = null;
			this._lastRecordedBallTime = 0.0;
			this._lastRecordedSwingCount = 0;
			this._waitingForBallToSettle = false;
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x0060BFBB File Offset: 0x0060A1BB
		public void CancelBallTracking()
		{
			this._waitingForBallToSettle = false;
		}

		// Token: 0x060044D6 RID: 17622 RVA: 0x0060BFC4 File Offset: 0x0060A1C4
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

		// Token: 0x060044D7 RID: 17623 RVA: 0x0060C035 File Offset: 0x0060A235
		private int GetGolfBallId(Projectile golfBall)
		{
			return golfBall.whoAmI;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x0060C040 File Offset: 0x0060A240
		public Projectile GetLastHitBall()
		{
			if (this._lastHitGolfBall == null || !this._lastHitGolfBall.active || !ProjectileID.Sets.IsAGolfBall[this._lastHitGolfBall.type] || this._lastHitGolfBall.owner != Main.myPlayer || this._lastRecordedSwingCount != (int)this._lastHitGolfBall.ai[1])
			{
				return null;
			}
			return this._lastHitGolfBall;
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x0060C0A8 File Offset: 0x0060A2A8
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
			if (Main.LocalPlayer.HeldItem.type == 3611)
			{
				flag = true;
			}
			if (!Item.IsAGolfingItem(Main.LocalPlayer.HeldItem) && !flag)
			{
				this._waitingForBallToSettle = false;
			}
		}

		// Token: 0x060044DA RID: 17626 RVA: 0x0060C11C File Offset: 0x0060A31C
		public void RecordBallInfo(Projectile golfBall)
		{
			if (this.GetLastHitBall() == golfBall && this._waitingForBallToSettle)
			{
				this._lastRecordedBallLocation = new Vector2?(golfBall.Center);
				this._lastRecordedBallTime = Main.gameTimeCache.TotalGameTime.TotalSeconds;
			}
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x0060C164 File Offset: 0x0060A364
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

		// Token: 0x060044DC RID: 17628 RVA: 0x0060C194 File Offset: 0x0060A394
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

		// Token: 0x060044DD RID: 17629 RVA: 0x0060C1C8 File Offset: 0x0060A3C8
		public void ResetGolfBall()
		{
			Projectile lastHitBall = this.GetLastHitBall();
			if (lastHitBall != null && Vector2.Distance(lastHitBall.position, this._lastSwingPosition) >= 1f)
			{
				lastHitBall.position = this._lastSwingPosition;
				lastHitBall.velocity = Vector2.Zero;
				lastHitBall.ai[1] += 1f;
				lastHitBall.netUpdate2 = true;
				this._lastRecordedSwingCount = (int)lastHitBall.ai[1];
			}
		}

		// Token: 0x04005AA4 RID: 23204
		private const int BALL_RETURN_PENALTY = 1;

		// Token: 0x04005AA5 RID: 23205
		private int golfScoreTime;

		// Token: 0x04005AA6 RID: 23206
		private int golfScoreTimeMax = 3600;

		// Token: 0x04005AA7 RID: 23207
		private int golfScoreDelay = 90;

		// Token: 0x04005AA8 RID: 23208
		private double _lastRecordedBallTime;

		// Token: 0x04005AA9 RID: 23209
		private Vector2? _lastRecordedBallLocation;

		// Token: 0x04005AAA RID: 23210
		private bool _waitingForBallToSettle;

		// Token: 0x04005AAB RID: 23211
		private Vector2 _lastSwingPosition;

		// Token: 0x04005AAC RID: 23212
		private Projectile _lastHitGolfBall;

		// Token: 0x04005AAD RID: 23213
		private int _lastRecordedSwingCount;

		// Token: 0x04005AAE RID: 23214
		private GolfBallTrackRecord[] _hitRecords = new GolfBallTrackRecord[1000];
	}
}
