using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A6 RID: 934
	internal class IntRangeElement : PrimitiveRangeElement<int>
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x00542F28 File Offset: 0x00541128
		public override int NumberTicks
		{
			get
			{
				return (base.Max - base.Min) / base.Increment + 1;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x00542F40 File Offset: 0x00541140
		public override float TickIncrement
		{
			get
			{
				return (float)base.Increment / (float)(base.Max - base.Min);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x00542F58 File Offset: 0x00541158
		// (set) Token: 0x0600324B RID: 12875 RVA: 0x00542F78 File Offset: 0x00541178
		protected override float Proportion
		{
			get
			{
				return (float)(this.GetValue() - base.Min) / (float)(base.Max - base.Min);
			}
			set
			{
				this.SetValue((int)Math.Round((double)((value * (float)(base.Max - base.Min) + (float)base.Min) * (1f / (float)base.Increment))) * base.Increment);
			}
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x00542FC4 File Offset: 0x005411C4
		public IntRangeElement()
		{
			base.Min = 0;
			base.Max = 100;
			base.Increment = 1;
		}
	}
}
