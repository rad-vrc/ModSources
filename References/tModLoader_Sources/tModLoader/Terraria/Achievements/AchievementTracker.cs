using System;
using Terraria.Social;

namespace Terraria.Achievements
{
	// Token: 0x02000775 RID: 1909
	public abstract class AchievementTracker<T> : IAchievementTracker
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06004D3D RID: 19773 RVA: 0x00672E50 File Offset: 0x00671050
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x00672E58 File Offset: 0x00671058
		public T MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00672E60 File Offset: 0x00671060
		protected AchievementTracker(TrackerType type)
		{
			this._type = type;
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00672E6F File Offset: 0x0067106F
		void IAchievementTracker.ReportAs(string name)
		{
			this._name = name;
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00672E78 File Offset: 0x00671078
		TrackerType IAchievementTracker.GetTrackerType()
		{
			return this._type;
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00672E80 File Offset: 0x00671080
		void IAchievementTracker.Clear()
		{
			this.SetValue(default(T), true);
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00672EA0 File Offset: 0x006710A0
		public void SetValue(T newValue, bool reportUpdate = true)
		{
			if (newValue.Equals(this._value))
			{
				return;
			}
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

		// Token: 0x06004D44 RID: 19780
		public abstract void ReportUpdate();

		// Token: 0x06004D45 RID: 19781
		protected abstract void Load();

		// Token: 0x06004D46 RID: 19782 RVA: 0x00672EFC File Offset: 0x006710FC
		void IAchievementTracker.Load()
		{
			this.Load();
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x00672F04 File Offset: 0x00671104
		protected void OnComplete()
		{
			if (SocialAPI.Achievements != null)
			{
				SocialAPI.Achievements.StoreStats();
			}
		}

		// Token: 0x0400615C RID: 24924
		protected T _value;

		// Token: 0x0400615D RID: 24925
		protected T _maxValue;

		// Token: 0x0400615E RID: 24926
		protected string _name;

		// Token: 0x0400615F RID: 24927
		private TrackerType _type;
	}
}
