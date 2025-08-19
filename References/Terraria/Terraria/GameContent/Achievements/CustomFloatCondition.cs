using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000208 RID: 520
	public class CustomFloatCondition : AchievementCondition
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00506A95 File Offset: 0x00504C95
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00506AA0 File Offset: 0x00504CA0
		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				float num = Utils.Clamp<float>(value, 0f, this._maxValue);
				if (this._tracker != null)
				{
					((ConditionFloatTracker)this._tracker).SetValue(num, true);
				}
				this._value = num;
				if (this._value == this._maxValue)
				{
					this.Complete();
				}
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00506AF4 File Offset: 0x00504CF4
		private CustomFloatCondition(string name, float maxValue) : base(name)
		{
			this._maxValue = maxValue;
			this._value = 0f;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00506B0F File Offset: 0x00504D0F
		public override void Clear()
		{
			this._value = 0f;
			base.Clear();
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00506B22 File Offset: 0x00504D22
		public override void Load(JObject state)
		{
			base.Load(state);
			this._value = (float)state["Value"];
			if (this._tracker != null)
			{
				((ConditionFloatTracker)this._tracker).SetValue(this._value, false);
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00506B61 File Offset: 0x00504D61
		protected override IAchievementTracker CreateAchievementTracker()
		{
			return new ConditionFloatTracker(this._maxValue);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00506B6E File Offset: 0x00504D6E
		public static AchievementCondition Create(string name, float maxValue)
		{
			return new CustomFloatCondition(name, maxValue);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00506B77 File Offset: 0x00504D77
		public override void Complete()
		{
			if (this._tracker != null)
			{
				((ConditionFloatTracker)this._tracker).SetValue(this._maxValue, true);
			}
			this._value = this._maxValue;
			base.Complete();
		}

		// Token: 0x0400456A RID: 17770
		[JsonProperty("Value")]
		private float _value;

		// Token: 0x0400456B RID: 17771
		private float _maxValue;
	}
}
