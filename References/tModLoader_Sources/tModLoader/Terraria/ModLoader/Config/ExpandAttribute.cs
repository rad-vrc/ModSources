using System;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// Affects whether this data will be expanded by default. The default value currently is true. Use the constructor with 2 parameters to control if list elements should be collapsed or expanded.
	/// </summary>
	// Token: 0x02000385 RID: 901
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class ExpandAttribute : Attribute
	{
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x0053D570 File Offset: 0x0053B770
		public bool Expand { get; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060030DB RID: 12507 RVA: 0x0053D578 File Offset: 0x0053B778
		public bool? ExpandListElements { get; }

		// Token: 0x060030DC RID: 12508 RVA: 0x0053D580 File Offset: 0x0053B780
		public ExpandAttribute(bool expand = true)
		{
			this.Expand = expand;
			this.ExpandListElements = null;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0053D59B File Offset: 0x0053B79B
		public ExpandAttribute(bool expand = true, bool expandListElements = true)
		{
			this.Expand = expand;
			this.ExpandListElements = new bool?(expandListElements);
		}
	}
}
