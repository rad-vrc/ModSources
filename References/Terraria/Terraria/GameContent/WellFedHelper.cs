using System;

namespace Terraria.GameContent
{
	// Token: 0x020001F7 RID: 503
	public struct WellFedHelper
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x004FF688 File Offset: 0x004FD888
		public int TimeLeft
		{
			get
			{
				return this._timeLeftRank1 + this._timeLeftRank2 + this._timeLeftRank3;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x004FF69E File Offset: 0x004FD89E
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

		// Token: 0x06001CFF RID: 7423 RVA: 0x004FF6C4 File Offset: 0x004FD8C4
		public void Eat(int foodRank, int foodBuffTime)
		{
			int num = foodBuffTime;
			if (foodRank >= 3)
			{
				this.AddTimeTo(ref this._timeLeftRank3, ref num, 72000);
			}
			if (foodRank >= 2)
			{
				this.AddTimeTo(ref this._timeLeftRank2, ref num, 72000);
			}
			if (foodRank >= 1)
			{
				this.AddTimeTo(ref this._timeLeftRank1, ref num, 72000);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x004FF718 File Offset: 0x004FD918
		public void Update()
		{
			this.ReduceTimeLeft();
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x004FF720 File Offset: 0x004FD920
		public void Clear()
		{
			this._timeLeftRank1 = 0;
			this._timeLeftRank2 = 0;
			this._timeLeftRank3 = 0;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x004FF738 File Offset: 0x004FD938
		private void AddTimeTo(ref int foodTimeCounter, ref int timeLeftToAdd, int counterMaximumTime)
		{
			if (timeLeftToAdd == 0)
			{
				return;
			}
			int num = timeLeftToAdd;
			if (foodTimeCounter + num > counterMaximumTime)
			{
				num = counterMaximumTime - foodTimeCounter;
			}
			foodTimeCounter += num;
			timeLeftToAdd -= num;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x004FF768 File Offset: 0x004FD968
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

		// Token: 0x040043ED RID: 17389
		private const int MAXIMUM_TIME_LEFT_PER_COUNTER = 72000;

		// Token: 0x040043EE RID: 17390
		private int _timeLeftRank1;

		// Token: 0x040043EF RID: 17391
		private int _timeLeftRank2;

		// Token: 0x040043F0 RID: 17392
		private int _timeLeftRank3;
	}
}
