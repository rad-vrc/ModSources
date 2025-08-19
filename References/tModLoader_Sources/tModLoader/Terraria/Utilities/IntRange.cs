using System;
using Newtonsoft.Json;

namespace Terraria.Utilities
{
	// Token: 0x0200008E RID: 142
	public struct IntRange
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x004A231D File Offset: 0x004A051D
		public IntRange(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x004A232D File Offset: 0x004A052D
		public static IntRange operator *(IntRange range, float scale)
		{
			return new IntRange((int)((float)range.Minimum * scale), (int)((float)range.Maximum * scale));
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x004A2348 File Offset: 0x004A0548
		public static IntRange operator *(float scale, IntRange range)
		{
			return range * scale;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x004A2351 File Offset: 0x004A0551
		public static IntRange operator /(IntRange range, float scale)
		{
			return new IntRange((int)((float)range.Minimum / scale), (int)((float)range.Maximum / scale));
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x004A236C File Offset: 0x004A056C
		public static IntRange operator /(float scale, IntRange range)
		{
			return range / scale;
		}

		// Token: 0x040010AC RID: 4268
		[JsonProperty("Min")]
		public readonly int Minimum;

		// Token: 0x040010AD RID: 4269
		[JsonProperty("Max")]
		public readonly int Maximum;
	}
}
