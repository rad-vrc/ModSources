using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A2 RID: 930
	public class FloatElement : PrimitiveRangeElement<float>
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x00542378 File Offset: 0x00540578
		public override int NumberTicks
		{
			get
			{
				return (int)((base.Max - base.Min) / base.Increment) + 1;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x00542391 File Offset: 0x00540591
		public override float TickIncrement
		{
			get
			{
				return base.Increment / (base.Max - base.Min);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x005423A7 File Offset: 0x005405A7
		// (set) Token: 0x06003222 RID: 12834 RVA: 0x005423C4 File Offset: 0x005405C4
		protected override float Proportion
		{
			get
			{
				return (this.GetValue() - base.Min) / (base.Max - base.Min);
			}
			set
			{
				this.SetValue((float)Math.Round((double)((value * (base.Max - base.Min) + base.Min) * (1f / base.Increment))) * base.Increment);
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x00542402 File Offset: 0x00540602
		public FloatElement()
		{
			base.Min = 0f;
			base.Max = 1f;
			base.Increment = 0.01f;
		}
	}
}
