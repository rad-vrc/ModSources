using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Achievements
{
	// Token: 0x02000773 RID: 1907
	[JsonObject(1)]
	public abstract class AchievementCondition
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x00672781 File Offset: 0x00670981
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06004D22 RID: 19746 RVA: 0x0067278C File Offset: 0x0067098C
		// (remove) Token: 0x06004D23 RID: 19747 RVA: 0x006727C4 File Offset: 0x006709C4
		public event AchievementCondition.AchievementUpdate OnComplete;

		// Token: 0x06004D24 RID: 19748 RVA: 0x006727F9 File Offset: 0x006709F9
		protected AchievementCondition(string name)
		{
			this.Name = name;
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x00672808 File Offset: 0x00670A08
		public virtual void Load(JObject state)
		{
			this._isCompleted = (bool)state["Completed"];
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x00672820 File Offset: 0x00670A20
		public virtual void Clear()
		{
			this._isCompleted = false;
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x00672829 File Offset: 0x00670A29
		public virtual void Complete()
		{
			if (!this._isCompleted)
			{
				this._isCompleted = true;
				if (this.OnComplete != null)
				{
					this.OnComplete(this);
				}
			}
		}

		// Token: 0x06004D28 RID: 19752 RVA: 0x0067284E File Offset: 0x00670A4E
		protected virtual IAchievementTracker CreateAchievementTracker()
		{
			return null;
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x00672851 File Offset: 0x00670A51
		public IAchievementTracker GetAchievementTracker()
		{
			if (this._tracker == null)
			{
				this._tracker = this.CreateAchievementTracker();
			}
			return this._tracker;
		}

		// Token: 0x04006150 RID: 24912
		public readonly string Name;

		// Token: 0x04006151 RID: 24913
		protected IAchievementTracker _tracker;

		// Token: 0x04006152 RID: 24914
		[JsonProperty("Completed")]
		private bool _isCompleted;

		// Token: 0x02000D73 RID: 3443
		// (Invoke) Token: 0x0600642D RID: 25645
		public delegate void AchievementUpdate(AchievementCondition condition);
	}
}
