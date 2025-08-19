using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Specifies a range for primitive data values. Without this, default min and max are as follows:
	/// <br /><b>float:</b> 0, 1
	/// <br /><b>int/uint:</b> 0, 100
	/// <br /><b>byte:</b> 0, 255
	/// <br /><b>long/ulong:</b> Unchanged from the full range of the type
	/// <para /> When using this, you might need to cast the arguments to the desired numeric type to call the correct overload.
	/// </summary>
	// Token: 0x0200037E RID: 894
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class RangeAttribute : Attribute
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x0053D461 File Offset: 0x0053B661
		public object Min { get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x0053D469 File Offset: 0x0053B669
		public object Max { get; }

		// Token: 0x060030CD RID: 12493 RVA: 0x0053D471 File Offset: 0x0053B671
		public RangeAttribute(int min, int max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0053D491 File Offset: 0x0053B691
		public RangeAttribute(float min, float max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0053D4B1 File Offset: 0x0053B6B1
		public RangeAttribute(uint min, uint max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0053D4D1 File Offset: 0x0053B6D1
		public RangeAttribute(long min, long max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0053D4F1 File Offset: 0x0053B6F1
		public RangeAttribute(ulong min, ulong max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0053D511 File Offset: 0x0053B711
		public RangeAttribute(byte min, byte max)
		{
			this.Min = min;
			this.Max = max;
		}
	}
}
