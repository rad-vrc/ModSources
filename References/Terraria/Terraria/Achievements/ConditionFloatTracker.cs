using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x0200048F RID: 1167
	public class ConditionFloatTracker : AchievementTracker<float>
	{
		// Token: 0x06002E78 RID: 11896 RVA: 0x005C41BB File Offset: 0x005C23BB
		public ConditionFloatTracker(float maxValue) : base(TrackerType.Float)
		{
			this._maxValue = maxValue;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x005C41CB File Offset: 0x005C23CB
		public ConditionFloatTracker() : base(TrackerType.Float)
		{
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x005C41D4 File Offset: 0x005C23D4
		public override void ReportUpdate()
		{
			if (SocialAPI.Achievements != null && this._name != null)
			{
				SocialAPI.Achievements.UpdateFloatStat(this._name, this._value);
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		protected override void Load()
		{
		}
	}
}
