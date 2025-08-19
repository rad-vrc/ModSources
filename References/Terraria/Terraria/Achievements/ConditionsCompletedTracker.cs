using System;
using System.Collections.Generic;

namespace Terraria.Achievements
{
	// Token: 0x02000493 RID: 1171
	public class ConditionsCompletedTracker : ConditionIntTracker
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x005C4912 File Offset: 0x005C2B12
		public void AddCondition(AchievementCondition condition)
		{
			this._maxValue++;
			condition.OnComplete += this.OnConditionCompleted;
			this._conditions.Add(condition);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x005C4940 File Offset: 0x005C2B40
		private void OnConditionCompleted(AchievementCondition condition)
		{
			base.SetValue(Math.Min(this._value + 1, this._maxValue), true);
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x005C495C File Offset: 0x005C2B5C
		protected override void Load()
		{
			for (int i = 0; i < this._conditions.Count; i++)
			{
				if (this._conditions[i].IsCompleted)
				{
					this._value++;
				}
			}
		}

		// Token: 0x040051E7 RID: 20967
		private List<AchievementCondition> _conditions = new List<AchievementCondition>();
	}
}
