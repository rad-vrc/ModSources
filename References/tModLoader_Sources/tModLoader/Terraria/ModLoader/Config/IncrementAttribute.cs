using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Use this to set an increment for sliders (if using <see cref="T:Terraria.ModLoader.Config.SliderAttribute" />) or the +/- buttons. The slider will move by the amount assigned. The +/- buttons will adjust the value by the amount as well.
	/// <para /> Remember that this is just a UI suggestion and manual editing of config files can specify other values, so validate your values.
	/// <para /> Defaults are as follows:
	/// <br /><b>float:</b> 0.01f
	/// <br /><b>byte/int/uint/long/ulong:</b> 1
	/// <para /> When using this, you might need to cast the arguments to the desired numeric type to call the correct overload.
	/// </summary>
	// Token: 0x0200037D RID: 893
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class IncrementAttribute : Attribute
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x0053D3E1 File Offset: 0x0053B5E1
		public object Increment { get; }

		// Token: 0x060030C5 RID: 12485 RVA: 0x0053D3E9 File Offset: 0x0053B5E9
		public IncrementAttribute(int increment)
		{
			this.Increment = increment;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0053D3FD File Offset: 0x0053B5FD
		public IncrementAttribute(float increment)
		{
			this.Increment = increment;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0053D411 File Offset: 0x0053B611
		public IncrementAttribute(uint increment)
		{
			this.Increment = increment;
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0053D425 File Offset: 0x0053B625
		public IncrementAttribute(long increment)
		{
			this.Increment = increment;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0053D439 File Offset: 0x0053B639
		public IncrementAttribute(ulong increment)
		{
			this.Increment = increment;
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0053D44D File Offset: 0x0053B64D
		public IncrementAttribute(byte increment)
		{
			this.Increment = increment;
		}
	}
}
