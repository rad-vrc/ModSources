using System;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A1 RID: 929
	internal class EnumElement : RangeElement
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x005420F6 File Offset: 0x005402F6
		public override int NumberTicks
		{
			get
			{
				return this.valueStrings.Length;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x00542100 File Offset: 0x00540300
		public override float TickIncrement
		{
			get
			{
				return 1f / (float)(this.valueStrings.Length - 1);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x00542113 File Offset: 0x00540313
		// (set) Token: 0x06003212 RID: 12818 RVA: 0x0054212B File Offset: 0x0054032B
		protected override float Proportion
		{
			get
			{
				return (float)this._getIndex() / (float)(this.max - 1);
			}
			set
			{
				this._setValue((int)Math.Round((double)(value * (float)(this.max - 1))));
			}
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x0054214C File Offset: 0x0054034C
		public override void OnBind()
		{
			base.OnBind();
			this.valueStrings = Enum.GetNames(base.MemberInfo.Type);
			for (int i = 0; i < this.valueStrings.Length; i++)
			{
				FieldInfo enumFieldFieldInfo = base.MemberInfo.Type.GetField(this.valueStrings[i]);
				if (enumFieldFieldInfo != null)
				{
					string name = ConfigManager.GetLocalizedLabel(new PropertyFieldWrapper(enumFieldFieldInfo));
					this.valueStrings[i] = name;
				}
			}
			this.max = this.valueStrings.Length;
			base.TextDisplayFunction = delegate()
			{
				string name2 = base.MemberInfo.Name;
				string str = ": ";
				object obj = this._getValueString();
				return name2 + str + ((obj != null) ? obj.ToString() : null);
			};
			this._getValue = (() => this.DefaultGetValue());
			this._getValueString = (() => this.DefaultGetStringValue());
			this._getIndex = (() => this.DefaultGetIndex());
			this._setValue = delegate(int value)
			{
				this.DefaultSetValue(value);
			};
			if (this.Label != null)
			{
				base.TextDisplayFunction = delegate()
				{
					string label = this.Label;
					string str = ": ";
					object obj = this._getValueString();
					return label + str + ((obj != null) ? obj.ToString() : null);
				};
			}
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00542244 File Offset: 0x00540444
		private void DefaultSetValue(int index)
		{
			if (!base.MemberInfo.CanWrite)
			{
				return;
			}
			base.MemberInfo.SetValue(base.Item, Enum.GetValues(base.MemberInfo.Type).GetValue(index));
			Interface.modConfig.SetPendingChanges(true);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00542291 File Offset: 0x00540491
		private object DefaultGetValue()
		{
			return base.MemberInfo.GetValue(base.Item);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x005422A4 File Offset: 0x005404A4
		private int DefaultGetIndex()
		{
			return Array.IndexOf(Enum.GetValues(base.MemberInfo.Type), this._getValue());
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x005422C8 File Offset: 0x005404C8
		private string DefaultGetStringValue()
		{
			int index = this._getIndex();
			if (index < 0)
			{
				return Language.GetTextValue("tModLoader.ModConfigUnknownEnum");
			}
			return this.valueStrings[index];
		}

		// Token: 0x04001D8B RID: 7563
		private Func<object> _getValue;

		// Token: 0x04001D8C RID: 7564
		private Func<object> _getValueString;

		// Token: 0x04001D8D RID: 7565
		private Func<int> _getIndex;

		// Token: 0x04001D8E RID: 7566
		private Action<int> _setValue;

		// Token: 0x04001D8F RID: 7567
		private int max;

		// Token: 0x04001D90 RID: 7568
		private string[] valueStrings;
	}
}
