using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006B9 RID: 1721
	public class CustomFloatCondition : AchievementCondition
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x0064B7F9 File Offset: 0x006499F9
		// (set) Token: 0x060048C1 RID: 18625 RVA: 0x0064B804 File Offset: 0x00649A04
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

		// Token: 0x060048C2 RID: 18626 RVA: 0x0064B858 File Offset: 0x00649A58
		private CustomFloatCondition(string name, float maxValue) : base(name)
		{
			this._maxValue = maxValue;
			this._value = 0f;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0064B873 File Offset: 0x00649A73
		public override void Clear()
		{
			this._value = 0f;
			base.Clear();
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0064B886 File Offset: 0x00649A86
		public override void Load(JObject state)
		{
			base.Load(state);
			this._value = (float)state["Value"];
			if (this._tracker != null)
			{
				((ConditionFloatTracker)this._tracker).SetValue(this._value, false);
			}
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x0064B8C5 File Offset: 0x00649AC5
		protected override IAchievementTracker CreateAchievementTracker()
		{
			return new ConditionFloatTracker(this._maxValue);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0064B8D2 File Offset: 0x00649AD2
		public static AchievementCondition Create(string name, float maxValue)
		{
			return new CustomFloatCondition(name, maxValue);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x0064B8DB File Offset: 0x00649ADB
		public override void Complete()
		{
			if (this._tracker != null)
			{
				((ConditionFloatTracker)this._tracker).SetValue(this._maxValue, true);
			}
			this._value = this._maxValue;
			base.Complete();
		}

		// Token: 0x04005C70 RID: 23664
		[JsonProperty("Value")]
		private float _value;

		// Token: 0x04005C71 RID: 23665
		private float _maxValue;
	}
}
