using System;

namespace Terraria.ModLoader
{
	// Token: 0x020001DB RID: 475
	public readonly struct MultipliableFloat
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060024ED RID: 9453 RVA: 0x004EAF37 File Offset: 0x004E9137
		public float Value { get; }

		// Token: 0x060024EE RID: 9454 RVA: 0x004EAF3F File Offset: 0x004E913F
		public MultipliableFloat()
		{
			this.Value = 1f;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x004EAF4C File Offset: 0x004E914C
		private MultipliableFloat(float f)
		{
			this.Value = 1f;
			this.Value = f;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x004EAF60 File Offset: 0x004E9160
		public static MultipliableFloat operator *(MultipliableFloat f1, MultipliableFloat f2)
		{
			return new MultipliableFloat(f1.Value * f2.Value);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x004EAF76 File Offset: 0x004E9176
		public static MultipliableFloat operator *(MultipliableFloat f1, float f2)
		{
			return new MultipliableFloat(f1.Value * f2);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x004EAF86 File Offset: 0x004E9186
		public static MultipliableFloat operator /(MultipliableFloat f1, float f2)
		{
			return new MultipliableFloat(f1.Value / f2);
		}

		// Token: 0x0400174D RID: 5965
		public static MultipliableFloat One = new MultipliableFloat(1f);
	}
}
