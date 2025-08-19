using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006BA RID: 1722
	public class CustomIntCondition : AchievementCondition
	{
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x0064B90E File Offset: 0x00649B0E
		// (set) Token: 0x060048C9 RID: 18633 RVA: 0x0064B918 File Offset: 0x00649B18
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

		// Token: 0x060048CA RID: 18634 RVA: 0x0064B968 File Offset: 0x00649B68
		private CustomIntCondition(string name, int maxValue) : base(name)
		{
			this._maxValue = maxValue;
			this._value = 0;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0064B97F File Offset: 0x00649B7F
		public override void Clear()
		{
			this._value = 0;
			base.Clear();
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x0064B98E File Offset: 0x00649B8E
		public override void Load(JObject state)
		{
			base.Load(state);
			this._value = (int)state["Value"];
			if (this._tracker != null)
			{
				((ConditionIntTracker)this._tracker).SetValue(this._value, false);
			}
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x0064B9CC File Offset: 0x00649BCC
		protected override IAchievementTracker CreateAchievementTracker()
		{
			return new ConditionIntTracker(this._maxValue);
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0064B9D9 File Offset: 0x00649BD9
		public static AchievementCondition Create(string name, int maxValue)
		{
			return new CustomIntCondition(name, maxValue);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0064B9E2 File Offset: 0x00649BE2
		public override void Complete()
		{
			if (this._tracker != null)
			{
				((ConditionIntTracker)this._tracker).SetValue(this._maxValue, true);
			}
			this._value = this._maxValue;
			base.Complete();
		}

		// Token: 0x04005C72 RID: 23666
		[JsonProperty("Value")]
		private int _value;

		// Token: 0x04005C73 RID: 23667
		private int _maxValue;
	}
}
