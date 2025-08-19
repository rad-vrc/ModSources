using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x02000492 RID: 1170
	public abstract class AchievementTracker<T> : IAchievementTracker
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x005C484C File Offset: 0x005C2A4C
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x005C4854 File Offset: 0x005C2A54
		public T MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x005C485C File Offset: 0x005C2A5C
		protected AchievementTracker(TrackerType type)
		{
			this._type = type;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x005C486B File Offset: 0x005C2A6B
		void IAchievementTracker.ReportAs(string name)
		{
			this._name = name;
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x005C4874 File Offset: 0x005C2A74
		TrackerType IAchievementTracker.GetTrackerType()
		{
			return this._type;
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x005C487C File Offset: 0x005C2A7C
		void IAchievementTracker.Clear()
		{
			this.SetValue(default(T), true);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x005C489C File Offset: 0x005C2A9C
		public void SetValue(T newValue, bool reportUpdate = true)
		{
			if (!newValue.Equals(this._value))
			{
				this._value = newValue;
				if (reportUpdate)
				{
					this.ReportUpdate();
					if (this._value.Equals(this._maxValue))
					{
						this.OnComplete();
					}
				}
			}
		}

		// Token: 0x06002E9A RID: 11930
		public abstract void ReportUpdate();

		// Token: 0x06002E9B RID: 11931
		protected abstract void Load();

		// Token: 0x06002E9C RID: 11932 RVA: 0x005C48F7 File Offset: 0x005C2AF7
		void IAchievementTracker.Load()
		{
			this.Load();
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x005C48FF File Offset: 0x005C2AFF
		protected void OnComplete()
		{
			if (SocialAPI.Achievements != null)
			{
				SocialAPI.Achievements.StoreStats();
			}
		}

		// Token: 0x040051E3 RID: 20963
		protected T _value;

		// Token: 0x040051E4 RID: 20964
		protected T _maxValue;

		// Token: 0x040051E5 RID: 20965
		protected string _name;

		// Token: 0x040051E6 RID: 20966
		private TrackerType _type;
	}
}
