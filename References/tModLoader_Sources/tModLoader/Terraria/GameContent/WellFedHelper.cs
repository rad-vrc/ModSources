using System;

namespace Terraria.GameContent
{
	// Token: 0x020004C5 RID: 1221
	public struct WellFedHelper
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x005A84FB File Offset: 0x005A66FB
		public int TimeLeft
		{
			get
			{
				return this._timeLeftRank1 + this._timeLeftRank2 + this._timeLeftRank3;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x005A8511 File Offset: 0x005A6711
		public int Rank
		{
			get
			{
				if (this._timeLeftRank3 > 0)
				{
					return 3;
				}
				if (this._timeLeftRank2 > 0)
				{
					return 2;
				}
				if (this._timeLeftRank1 > 0)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x005A8538 File Offset: 0x005A6738
		public void Eat(int foodRank, int foodBuffTime)
		{
			int timeLeftToAdd = foodBuffTime;
			if (foodRank >= 3)
			{
				this.AddTimeTo(ref this._timeLeftRank3, ref timeLeftToAdd, 72000);
			}
			if (foodRank >= 2)
			{
				this.AddTimeTo(ref this._timeLeftRank2, ref timeLeftToAdd, 72000);
			}
			if (foodRank >= 1)
			{
				this.AddTimeTo(ref this._timeLeftRank1, ref timeLeftToAdd, 72000);
			}
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x005A858C File Offset: 0x005A678C
		public void Update()
		{
			this.ReduceTimeLeft();
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x005A8594 File Offset: 0x005A6794
		public void Clear()
		{
			this._timeLeftRank1 = 0;
			this._timeLeftRank2 = 0;
			this._timeLeftRank3 = 0;
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x005A85AC File Offset: 0x005A67AC
		private void AddTimeTo(ref int foodTimeCounter, ref int timeLeftToAdd, int counterMaximumTime)
		{
			if (timeLeftToAdd != 0)
			{
				int num = timeLeftToAdd;
				if (foodTimeCounter + num > counterMaximumTime)
				{
					num = counterMaximumTime - foodTimeCounter;
				}
				foodTimeCounter += num;
				timeLeftToAdd -= num;
			}
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x005A85D8 File Offset: 0x005A67D8
		private void ReduceTimeLeft()
		{
			if (this._timeLeftRank3 > 0)
			{
				this._timeLeftRank3--;
				return;
			}
			if (this._timeLeftRank2 > 0)
			{
				this._timeLeftRank2--;
				return;
			}
			if (this._timeLeftRank1 > 0)
			{
				this._timeLeftRank1--;
			}
		}

		// Token: 0x040053FF RID: 21503
		private const int MAXIMUM_TIME_LEFT_PER_COUNTER = 72000;

		// Token: 0x04005400 RID: 21504
		private int _timeLeftRank1;

		// Token: 0x04005401 RID: 21505
		private int _timeLeftRank2;

		// Token: 0x04005402 RID: 21506
		private int _timeLeftRank3;
	}
}
