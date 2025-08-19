using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A7 RID: 935
	internal class UIntElement : PrimitiveRangeElement<uint>
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600324D RID: 12877 RVA: 0x00542FE2 File Offset: 0x005411E2
		public override int NumberTicks
		{
			get
			{
				return (int)((base.Max - base.Min) / base.Increment + 1U);
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x00542FFA File Offset: 0x005411FA
		public override float TickIncrement
		{
			get
			{
				return base.Increment / (base.Max - base.Min);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x00543014 File Offset: 0x00541214
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x00543038 File Offset: 0x00541238
		protected override float Proportion
		{
			get
			{
				return (this.GetValue() - base.Min) / (base.Max - base.Min);
			}
			set
			{
				this.SetValue((uint)Math.Round((double)((value * (base.Max - base.Min) + base.Min) * (1f / base.Increment))) * base.Increment);
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x00543087 File Offset: 0x00541287
		public UIntElement()
		{
			base.Min = 0U;
			base.Max = 100U;
			base.Increment = 1U;
		}
	}
}
