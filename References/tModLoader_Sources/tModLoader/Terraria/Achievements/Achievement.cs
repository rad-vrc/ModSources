using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Localization;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x02000771 RID: 1905
	[JsonObject(1)]
	public class Achievement
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x0067239F File Offset: 0x0067059F
		public AchievementCategory Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x006723A7 File Offset: 0x006705A7
		public bool HasTracker
		{
			get
			{
				return this._tracker != null;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x006723B2 File Offset: 0x006705B2
		public bool IsCompleted
		{
			get
			{
				return this._completedCount == this._conditions.Count;
			}
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06004D10 RID: 19728 RVA: 0x006723C8 File Offset: 0x006705C8
		// (remove) Token: 0x06004D11 RID: 19729 RVA: 0x00672400 File Offset: 0x00670600
		public event Achievement.AchievementCompleted OnCompleted;

		// Token: 0x06004D12 RID: 19730 RVA: 0x00672435 File Offset: 0x00670635
		public IAchievementTracker GetTracker()
		{
			return this._tracker;
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x00672440 File Offset: 0x00670640
		public Achievement(string name)
		{
			this.Name = name;
			this.FriendlyName = Language.GetText("Achievements." + name + "_Name");
			this.Description = Language.GetText("Achievements." + name + "_Description");
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x006724B0 File Offset: 0x006706B0
		public void ClearProgress()
		{
			this._completedCount = 0;
			foreach (KeyValuePair<string, AchievementCondition> condition in this._conditions)
			{
				condition.Value.Clear();
			}
			if (this._tracker != null)
			{
				this._tracker.Clear();
			}
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x00672524 File Offset: 0x00670724
		public void Load(Dictionary<string, JObject> conditions)
		{
			foreach (KeyValuePair<string, JObject> condition in conditions)
			{
				AchievementCondition value;
				if (this._conditions.TryGetValue(condition.Key, out value))
				{
					value.Load(condition.Value);
					if (value.IsCompleted)
					{
						this._completedCount++;
					}
				}
			}
			if (this._tracker != null)
			{
				this._tracker.Load();
			}
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x006725B8 File Offset: 0x006707B8
		public void AddCondition(AchievementCondition condition)
		{
			this._conditions[condition.Name] = condition;
			condition.OnComplete += this.OnConditionComplete;
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x006725E0 File Offset: 0x006707E0
		private void OnConditionComplete(AchievementCondition condition)
		{
			this._completedCount++;
			if (this._completedCount == this._conditions.Count)
			{
				if (this._tracker == null && SocialAPI.Achievements != null)
				{
					SocialAPI.Achievements.CompleteAchievement(this.Name);
				}
				if (this.OnCompleted != null)
				{
					this.OnCompleted(this);
				}
			}
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x00672641 File Offset: 0x00670841
		private void UseTracker(IAchievementTracker tracker)
		{
			tracker.ReportAs("STAT_" + this.Name);
			this._tracker = tracker;
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x00672660 File Offset: 0x00670860
		public void UseTrackerFromCondition(string conditionName)
		{
			this.UseTracker(this.GetConditionTracker(conditionName));
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x00672670 File Offset: 0x00670870
		public void UseConditionsCompletedTracker()
		{
			ConditionsCompletedTracker conditionsCompletedTracker = new ConditionsCompletedTracker();
			foreach (KeyValuePair<string, AchievementCondition> condition in this._conditions)
			{
				conditionsCompletedTracker.AddCondition(condition.Value);
			}
			this.UseTracker(conditionsCompletedTracker);
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x006726D8 File Offset: 0x006708D8
		public void UseConditionsCompletedTracker(params string[] conditions)
		{
			ConditionsCompletedTracker conditionsCompletedTracker = new ConditionsCompletedTracker();
			foreach (string key in conditions)
			{
				conditionsCompletedTracker.AddCondition(this._conditions[key]);
			}
			this.UseTracker(conditionsCompletedTracker);
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x00672718 File Offset: 0x00670918
		public void ClearTracker()
		{
			this._tracker = null;
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x00672721 File Offset: 0x00670921
		private IAchievementTracker GetConditionTracker(string name)
		{
			return this._conditions[name].GetAchievementTracker();
		}

		// Token: 0x06004D1E RID: 19742 RVA: 0x00672734 File Offset: 0x00670934
		public void AddConditions(params AchievementCondition[] conditions)
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				this.AddCondition(conditions[i]);
			}
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x00672758 File Offset: 0x00670958
		public AchievementCondition GetCondition(string conditionName)
		{
			AchievementCondition value;
			if (this._conditions.TryGetValue(conditionName, out value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x00672778 File Offset: 0x00670978
		public void SetCategory(AchievementCategory category)
		{
			this._category = category;
		}

		// Token: 0x04006140 RID: 24896
		private static int _totalAchievements;

		// Token: 0x04006141 RID: 24897
		public readonly string Name;

		// Token: 0x04006142 RID: 24898
		public readonly LocalizedText FriendlyName;

		// Token: 0x04006143 RID: 24899
		public readonly LocalizedText Description;

		// Token: 0x04006144 RID: 24900
		public readonly int Id = Achievement._totalAchievements++;

		// Token: 0x04006145 RID: 24901
		private AchievementCategory _category;

		// Token: 0x04006146 RID: 24902
		private IAchievementTracker _tracker;

		// Token: 0x04006147 RID: 24903
		[JsonProperty("Conditions")]
		private Dictionary<string, AchievementCondition> _conditions = new Dictionary<string, AchievementCondition>();

		// Token: 0x04006148 RID: 24904
		private int _completedCount;

		// Token: 0x02000D72 RID: 3442
		// (Invoke) Token: 0x06006429 RID: 25641
		public delegate void AchievementCompleted(Achievement achievement);
	}
}
