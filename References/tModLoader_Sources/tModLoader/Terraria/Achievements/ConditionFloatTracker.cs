using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x02000776 RID: 1910
	public class ConditionFloatTracker : AchievementTracker<float>
	{
		// Token: 0x06004D48 RID: 19784 RVA: 0x00672F17 File Offset: 0x00671117
		public ConditionFloatTracker(float maxValue) : base(TrackerType.Float)
		{
			this._maxValue = maxValue;
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x00672F27 File Offset: 0x00671127
		public ConditionFloatTracker() : base(TrackerType.Float)
		{
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x00672F30 File Offset: 0x00671130
		public override void ReportUpdate()
		{
			if (SocialAPI.Achievements != null && this._name != null)
			{
				SocialAPI.Achievements.UpdateFloatStat(this._name, this._value);
			}
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x00672F57 File Offset: 0x00671157
		protected override void Load()
		{
		}
	}
}
