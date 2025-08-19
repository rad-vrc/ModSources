using System;
using System.Collections.Generic;

namespace Terraria.Achievements
{
	// Token: 0x02000778 RID: 1912
	public class ConditionsCompletedTracker : ConditionIntTracker
	{
		// Token: 0x06004D50 RID: 19792 RVA: 0x00672F9B File Offset: 0x0067119B
		public void AddCondition(AchievementCondition condition)
		{
			this._maxValue++;
			condition.OnComplete += this.OnConditionCompleted;
			this._conditions.Add(condition);
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00672FC9 File Offset: 0x006711C9
		private void OnConditionCompleted(AchievementCondition condition)
		{
			base.SetValue(Math.Min(this._value + 1, this._maxValue), true);
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x00672FE8 File Offset: 0x006711E8
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

		// Token: 0x04006160 RID: 24928
		private List<AchievementCondition> _conditions = new List<AchievementCondition>();
	}
}
