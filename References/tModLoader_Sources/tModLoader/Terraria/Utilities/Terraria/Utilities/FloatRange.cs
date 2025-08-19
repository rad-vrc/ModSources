using System;
using Newtonsoft.Json;

namespace Terraria.Utilities.Terraria.Utilities
{
	// Token: 0x02000095 RID: 149
	public struct FloatRange
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x004A2EA5 File Offset: 0x004A10A5
		public FloatRange(float minimum, float maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x004A2EB5 File Offset: 0x004A10B5
		public static FloatRange operator *(FloatRange range, float scale)
		{
			return new FloatRange(range.Minimum * scale, range.Maximum * scale);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x004A2ECC File Offset: 0x004A10CC
		public static FloatRange operator *(float scale, FloatRange range)
		{
			return range * scale;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x004A2ED5 File Offset: 0x004A10D5
		public static FloatRange operator /(FloatRange range, float scale)
		{
			return new FloatRange(range.Minimum / scale, range.Maximum / scale);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x004A2EEC File Offset: 0x004A10EC
		public static FloatRange operator /(float scale, FloatRange range)
		{
			return range / scale;
		}

		// Token: 0x040010BA RID: 4282
		[JsonProperty("Min")]
		public readonly float Minimum;

		// Token: 0x040010BB RID: 4283
		[JsonProperty("Max")]
		public readonly float Maximum;
	}
}
