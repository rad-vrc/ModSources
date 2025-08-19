using System;
using System.Collections.Generic;
using Terraria.ModLoader.UI;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003BE RID: 958
	internal class StringOptionElement : RangeElement
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x00545A65 File Offset: 0x00543C65
		// (set) Token: 0x060032CF RID: 13007 RVA: 0x00545A6D File Offset: 0x00543C6D
		public IList<string> StringList { get; set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x00545A76 File Offset: 0x00543C76
		public override int NumberTicks
		{
			get
			{
				return this.options.Length;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x00545A80 File Offset: 0x00543C80
		public override float TickIncrement
		{
			get
			{
				return 1f / (float)(this.options.Length - 1);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x00545A93 File Offset: 0x00543C93
		// (set) Token: 0x060032D3 RID: 13011 RVA: 0x00545AAD File Offset: 0x00543CAD
		protected override float Proportion
		{
			get
			{
				return (float)this.getIndex() / (float)(this.options.Length - 1);
			}
			set
			{
				this.setValue((int)Math.Round((double)(value * (float)(this.options.Length - 1))));
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x00545AD0 File Offset: 0x00543CD0
		public override void OnBind()
		{
			base.OnBind();
			this.StringList = (IList<string>)base.List;
			OptionStringsAttribute optionsAttribute = ConfigManager.GetCustomAttributeFromMemberThenMemberType<OptionStringsAttribute>(base.MemberInfo, base.Item, this.StringList);
			this.options = optionsAttribute.OptionLabels;
			base.TextDisplayFunction = (() => base.MemberInfo.Name + ": " + this.getValue());
			this.getValue = (() => this.DefaultGetValue());
			this.getIndex = (() => this.DefaultGetIndex());
			this.setValue = delegate(int value)
			{
				this.DefaultSetValue(value);
			};
			if (this.StringList != null)
			{
				this.getValue = (() => this.StringList[base.Index]);
				this.setValue = delegate(int value)
				{
					this.StringList[base.Index] = this.options[value];
					Interface.modConfig.SetPendingChanges(true);
				};
				base.TextDisplayFunction = (() => (base.Index + 1).ToString() + ": " + this.StringList[base.Index]);
			}
			if (this.Label != null)
			{
				base.TextDisplayFunction = (() => this.Label + ": " + this.getValue());
			}
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x00545BB8 File Offset: 0x00543DB8
		private void DefaultSetValue(int index)
		{
			if (!base.MemberInfo.CanWrite)
			{
				return;
			}
			base.MemberInfo.SetValue(base.Item, this.options[index]);
			Interface.modConfig.SetPendingChanges(true);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x00545BEC File Offset: 0x00543DEC
		private string DefaultGetValue()
		{
			return (string)base.MemberInfo.GetValue(base.Item);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x00545C04 File Offset: 0x00543E04
		private int DefaultGetIndex()
		{
			return Array.IndexOf<string>(this.options, this.getValue());
		}

		// Token: 0x04001DBF RID: 7615
		private Func<string> getValue;

		// Token: 0x04001DC0 RID: 7616
		private Func<int> getIndex;

		// Token: 0x04001DC1 RID: 7617
		private Action<int> setValue;

		// Token: 0x04001DC2 RID: 7618
		private string[] options;
	}
}
