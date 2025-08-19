using System;
using Newtonsoft.Json;

namespace Terraria.Utilities.Terraria.Utilities
{
	// Token: 0x0200014B RID: 331
	public struct FloatRange
	{
		// Token: 0x0600191E RID: 6430 RVA: 0x004E01A3 File Offset: 0x004DE3A3
		public FloatRange(float minimum, float maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x004E01B3 File Offset: 0x004DE3B3
		public static FloatRange operator *(FloatRange range, float scale)
		{
			return new FloatRange(range.Minimum * scale, range.Maximum * scale);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x004E01CA File Offset: 0x004DE3CA
		public static FloatRange operator *(float scale, FloatRange range)
		{
			return range * scale;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x004E01D3 File Offset: 0x004DE3D3
		public static FloatRange operator /(FloatRange range, float scale)
		{
			return new FloatRange(range.Minimum / scale, range.Maximum / scale);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x004E01EA File Offset: 0x004DE3EA
		public static FloatRange operator /(float scale, FloatRange range)
		{
			return range / scale;
		}

		// Token: 0x04001521 RID: 5409
		[JsonProperty("Min")]
		public readonly float Minimum;

		// Token: 0x04001522 RID: 5410
		[JsonProperty("Max")]
		public readonly float Maximum;
	}
}
