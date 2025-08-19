using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000209 RID: 521
	public class CustomIntCondition : AchievementCondition
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00506BAA File Offset: 0x00504DAA
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x00506BB4 File Offset: 0x00504DB4
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				int num = Utils.Clamp<int>(value, 0, this._maxValue);
				if (this._tracker != null)
				{
					((ConditionIntTracker)this._tracker).SetValue(num, true);
				}
				this._value = num;
				if (this._value == this._maxValue)
				{
					this.Complete();
				}
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00506C04 File Offset: 0x00504E04
		private CustomIntCondition(string name, int maxValue) : base(name)
		{
			this._maxValue = maxValue;
			this._value = 0;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00506C1B File Offset: 0x00504E1B
		public override void Clear()
		{
			this._value = 0;
			base.Clear();
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00506C2A File Offset: 0x00504E2A
		public override void Load(JObject state)
		{
			base.Load(state);
			this._value = (int)state["Value"];
			if (this._tracker != null)
			{
				((ConditionIntTracker)this._tracker).SetValue(this._value, false);
			}
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00506C68 File Offset: 0x00504E68
		protected override IAchievementTracker CreateAchievementTracker()
		{
			return new ConditionIntTracker(this._maxValue);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00506C75 File Offset: 0x00504E75
		public static AchievementCondition Create(string name, int maxValue)
		{
			return new CustomIntCondition(name, maxValue);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00506C7E File Offset: 0x00504E7E
		public override void Complete()
		{
			if (this._tracker != null)
			{
				((ConditionIntTracker)this._tracker).SetValue(this._maxValue, true);
			}
			this._value = this._maxValue;
			base.Complete();
		}

		// Token: 0x0400456C RID: 17772
		[JsonProperty("Value")]
		private int _value;

		// Token: 0x0400456D RID: 17773
		private int _maxValue;
	}
}
