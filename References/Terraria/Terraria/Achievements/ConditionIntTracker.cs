using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x02000490 RID: 1168
	public class ConditionIntTracker : AchievementTracker<int>
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x005C41FB File Offset: 0x005C23FB
		public ConditionIntTracker() : base(TrackerType.Int)
		{
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x005C4204 File Offset: 0x005C2404
		public ConditionIntTracker(int maxValue) : base(TrackerType.Int)
		{
			this._maxValue = maxValue;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x005C4214 File Offset: 0x005C2414
		public override void ReportUpdate()
		{
			if (SocialAPI.Achievements != null && this._name != null)
			{
				SocialAPI.Achievements.UpdateIntStat(this._name, this._value);
			}
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected override void Load()
		{
		}
	}
}
