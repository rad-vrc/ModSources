using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200013B RID: 315
	public readonly struct AddableFloat
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x004CCFFA File Offset: 0x004CB1FA
		public float Value { get; }

		// Token: 0x06001AAA RID: 6826 RVA: 0x004CD002 File Offset: 0x004CB202
		private AddableFloat(float f)
		{
			this.Value = f;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x004CD00B File Offset: 0x004CB20B
		public static AddableFloat operator +(AddableFloat f1, AddableFloat f2)
		{
			return new AddableFloat(f1.Value + f2.Value);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x004CD021 File Offset: 0x004CB221
		public static AddableFloat operator +(AddableFloat f1, float f2)
		{
			return new AddableFloat(f1.Value + f2);
		}

		// Token: 0x0400146C RID: 5228
		public static AddableFloat Zero = new AddableFloat(0f);
	}
}
