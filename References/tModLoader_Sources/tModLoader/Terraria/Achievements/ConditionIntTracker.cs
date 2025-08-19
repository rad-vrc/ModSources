using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x02000777 RID: 1911
	public class ConditionIntTracker : AchievementTracker<int>
	{
		// Token: 0x06004D4C RID: 19788 RVA: 0x00672F59 File Offset: 0x00671159
		public ConditionIntTracker() : base(TrackerType.Int)
		{
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x00672F62 File Offset: 0x00671162
		public ConditionIntTracker(int maxValue) : base(TrackerType.Int)
		{
			this._maxValue = maxValue;
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00672F72 File Offset: 0x00671172
		public override void ReportUpdate()
		{
			if (SocialAPI.Achievements != null && this._name != null)
			{
				SocialAPI.Achievements.UpdateIntStat(this._name, this._value);
			}
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x00672F99 File Offset: 0x00671199
		protected override void Load()
		{
		}
	}
}
