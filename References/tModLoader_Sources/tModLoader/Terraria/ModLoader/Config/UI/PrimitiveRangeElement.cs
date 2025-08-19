using System;
using System.Collections.Generic;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003B8 RID: 952
	public abstract class PrimitiveRangeElement<T> : RangeElement where T : IComparable<T>
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x00544DF1 File Offset: 0x00542FF1
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x00544DF9 File Offset: 0x00542FF9
		public T Min { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x00544E02 File Offset: 0x00543002
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x00544E0A File Offset: 0x0054300A
		public T Max { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x00544E13 File Offset: 0x00543013
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x00544E1B File Offset: 0x0054301B
		public T Increment { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x00544E24 File Offset: 0x00543024
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x00544E2C File Offset: 0x0054302C
		public IList<T> TList { get; set; }

		// Token: 0x060032A9 RID: 12969 RVA: 0x00544E38 File Offset: 0x00543038
		public override void OnBind()
		{
			base.OnBind();
			this.TList = (IList<T>)base.List;
			base.TextDisplayFunction = delegate()
			{
				string name = base.MemberInfo.Name;
				string str = ": ";
				T value = this.GetValue();
				return name + str + ((value != null) ? value.ToString() : null);
			};
			if (this.TList != null)
			{
				base.TextDisplayFunction = delegate()
				{
					string str = (base.Index + 1).ToString();
					string str2 = ": ";
					T t = this.TList[base.Index];
					return str + str2 + ((t != null) ? t.ToString() : null);
				};
			}
			if (this.Label != null)
			{
				base.TextDisplayFunction = delegate()
				{
					string label = this.Label;
					string str = ": ";
					T value = this.GetValue();
					return label + str + ((value != null) ? value.ToString() : null);
				};
			}
			if (this.RangeAttribute != null && this.RangeAttribute.Min is T && this.RangeAttribute.Max is T)
			{
				this.Min = (T)((object)this.RangeAttribute.Min);
				this.Max = (T)((object)this.RangeAttribute.Max);
			}
			if (this.IncrementAttribute != null && this.IncrementAttribute.Increment is T)
			{
				this.Increment = (T)((object)this.IncrementAttribute.Increment);
			}
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x00544F2A File Offset: 0x0054312A
		protected virtual T GetValue()
		{
			return (T)((object)this.GetObject());
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x00544F38 File Offset: 0x00543138
		protected virtual void SetValue(object value)
		{
			if (value is T)
			{
				T t = (T)((object)value);
				this.SetObject(Utils.Clamp<T>(t, this.Min, this.Max));
			}
		}
	}
}
