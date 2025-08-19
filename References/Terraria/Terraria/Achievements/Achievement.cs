using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Localization;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x0200048D RID: 1165
	[JsonObject(1)]
	public class Achievement
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x005C3CF0 File Offset: 0x005C1EF0
		public AchievementCategory Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06002E5C RID: 11868 RVA: 0x005C3CF8 File Offset: 0x005C1EF8
		// (remove) Token: 0x06002E5D RID: 11869 RVA: 0x005C3D30 File Offset: 0x005C1F30
		public event Achievement.AchievementCompleted OnCompleted;

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x005C3D65 File Offset: 0x005C1F65
		public bool HasTracker
		{
			get
			{
				return this._tracker != null;
			}
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x005C3D70 File Offset: 0x005C1F70
		public IAchievementTracker GetTracker()
		{
			return this._tracker;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x005C3D78 File Offset: 0x005C1F78
		public bool IsCompleted
		{
			get
			{
				return this._completedCount == this._conditions.Count;
			}
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x005C3D90 File Offset: 0x005C1F90
		public Achievement(string name)
		{
			this.Name = name;
			this.FriendlyName = Language.GetText("Achievements." + name + "_Name");
			this.Description = Language.GetText("Achievements." + name + "_Description");
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x005C3E00 File Offset: 0x005C2000
		public void ClearProgress()
		{
			this._completedCount = 0;
			foreach (KeyValuePair<string, AchievementCondition> keyValuePair in this._conditions)
			{
				keyValuePair.Value.Clear();
			}
			if (this._tracker != null)
			{
				this._tracker.Clear();
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x005C3E74 File Offset: 0x005C2074
		public void Load(Dictionary<string, JObject> conditions)
		{
			foreach (KeyValuePair<string, JObject> keyValuePair in conditions)
			{
				AchievementCondition achievementCondition;
				if (this._conditions.TryGetValue(keyValuePair.Key, out achievementCondition))
				{
					achievementCondition.Load(keyValuePair.Value);
					if (achievementCondition.IsCompleted)
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

		// Token: 0x06002E64 RID: 11876 RVA: 0x005C3F08 File Offset: 0x005C2108
		public void AddCondition(AchievementCondition condition)
		{
			this._conditions[condition.Name] = condition;
			condition.OnComplete += this.OnConditionComplete;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x005C3F30 File Offset: 0x005C2130
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

		// Token: 0x06002E66 RID: 11878 RVA: 0x005C3F91 File Offset: 0x005C2191
		private void UseTracker(IAchievementTracker tracker)
		{
			tracker.ReportAs("STAT_" + this.Name);
			this._tracker = tracker;
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x005C3FB0 File Offset: 0x005C21B0
		public void UseTrackerFromCondition(string conditionName)
		{
			this.UseTracker(this.GetConditionTracker(conditionName));
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x005C3FC0 File Offset: 0x005C21C0
		public void UseConditionsCompletedTracker()
		{
			ConditionsCompletedTracker conditionsCompletedTracker = new ConditionsCompletedTracker();
			foreach (KeyValuePair<string, AchievementCondition> keyValuePair in this._conditions)
			{
				conditionsCompletedTracker.AddCondition(keyValuePair.Value);
			}
			this.UseTracker(conditionsCompletedTracker);
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x005C4028 File Offset: 0x005C2228
		public void UseConditionsCompletedTracker(params string[] conditions)
		{
			ConditionsCompletedTracker conditionsCompletedTracker = new ConditionsCompletedTracker();
			foreach (string key in conditions)
			{
				conditionsCompletedTracker.AddCondition(this._conditions[key]);
			}
			this.UseTracker(conditionsCompletedTracker);
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x005C4066 File Offset: 0x005C2266
		public void ClearTracker()
		{
			this._tracker = null;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x005C406F File Offset: 0x005C226F
		private IAchievementTracker GetConditionTracker(string name)
		{
			return this._conditions[name].GetAchievementTracker();
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x005C4084 File Offset: 0x005C2284
		public void AddConditions(params AchievementCondition[] conditions)
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				this.AddCondition(conditions[i]);
			}
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x005C40A8 File Offset: 0x005C22A8
		public AchievementCondition GetCondition(string conditionName)
		{
			AchievementCondition result;
			if (this._conditions.TryGetValue(conditionName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x005C40C8 File Offset: 0x005C22C8
		public void SetCategory(AchievementCategory category)
		{
			this._category = category;
		}

		// Token: 0x040051CD RID: 20941
		private static int _totalAchievements;

		// Token: 0x040051CE RID: 20942
		public readonly string Name;

		// Token: 0x040051CF RID: 20943
		public readonly LocalizedText FriendlyName;

		// Token: 0x040051D0 RID: 20944
		public readonly LocalizedText Description;

		// Token: 0x040051D1 RID: 20945
		public readonly int Id = Achievement._totalAchievements++;

		// Token: 0x040051D2 RID: 20946
		private AchievementCategory _category;

		// Token: 0x040051D3 RID: 20947
		private IAchievementTracker _tracker;

		// Token: 0x040051D5 RID: 20949
		[JsonProperty("Conditions")]
		private Dictionary<string, AchievementCondition> _conditions = new Dictionary<string, AchievementCondition>();

		// Token: 0x040051D6 RID: 20950
		private int _completedCount;

		// Token: 0x02000785 RID: 1925
		// (Invoke) Token: 0x06003939 RID: 14649
		public delegate void AchievementCompleted(Achievement achievement);
	}
}
