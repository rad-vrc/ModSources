using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Achievements
{
	// Token: 0x0200048E RID: 1166
	[JsonObject(1)]
	public abstract class AchievementCondition
	{
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06002E6F RID: 11887 RVA: 0x005C40D4 File Offset: 0x005C22D4
		// (remove) Token: 0x06002E70 RID: 11888 RVA: 0x005C410C File Offset: 0x005C230C
		public event AchievementCondition.AchievementUpdate OnComplete;

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x005C4141 File Offset: 0x005C2341
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x005C4149 File Offset: 0x005C2349
		protected AchievementCondition(string name)
		{
			this.Name = name;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x005C4158 File Offset: 0x005C2358
		public virtual void Load(JObject state)
		{
			this._isCompleted = (bool)state["Completed"];
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x005C4170 File Offset: 0x005C2370
		public virtual void Clear()
		{
			this._isCompleted = false;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x005C4179 File Offset: 0x005C2379
		public virtual void Complete()
		{
			if (this._isCompleted)
			{
				return;
			}
			this._isCompleted = true;
			if (this.OnComplete != null)
			{
				this.OnComplete(this);
			}
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0006A9EF File Offset: 0x00068BEF
		protected virtual IAchievementTracker CreateAchievementTracker()
		{
			return null;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x005C419F File Offset: 0x005C239F
		public IAchievementTracker GetAchievementTracker()
		{
			if (this._tracker == null)
			{
				this._tracker = this.CreateAchievementTracker();
			}
			return this._tracker;
		}

		// Token: 0x040051D7 RID: 20951
		public readonly string Name;

		// Token: 0x040051D9 RID: 20953
		protected IAchievementTracker _tracker;

		// Token: 0x040051DA RID: 20954
		[JsonProperty("Completed")]
		private bool _isCompleted;

		// Token: 0x02000786 RID: 1926
		// (Invoke) Token: 0x0600393D RID: 14653
		public delegate void AchievementUpdate(AchievementCondition condition);
	}
}
