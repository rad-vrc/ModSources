using System;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003A8 RID: 936
	internal class ByteElement : PrimitiveRangeElement<byte>
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x005430A5 File Offset: 0x005412A5
		public override int NumberTicks
		{
			get
			{
				return (int)((base.Max - base.Min) / base.Increment + 1);
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x005430BD File Offset: 0x005412BD
		public override float TickIncrement
		{
			get
			{
				return (float)base.Increment / (float)(base.Max - base.Min);
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06003254 RID: 12884 RVA: 0x005430D5 File Offset: 0x005412D5
		// (set) Token: 0x06003255 RID: 12885 RVA: 0x005430F4 File Offset: 0x005412F4
		protected override float Proportion
		{
			get
			{
				return (float)(this.GetValue() - base.Min) / (float)(base.Max - base.Min);
			}
			set
			{
				this.SetValue(Convert.ToByte((int)Math.Round((double)((value * (float)(base.Max - base.Min) + (float)base.Min) * (1f / (float)base.Increment))) * (int)base.Increment));
			}
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x00543145 File Offset: 0x00541345
		public ByteElement()
		{
			base.Min = 0;
			base.Max = byte.MaxValue;
			base.Increment = 1;
		}
	}
}
