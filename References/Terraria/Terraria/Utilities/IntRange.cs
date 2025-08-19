using System;
using Newtonsoft.Json;

namespace Terraria.Utilities
{
	// Token: 0x02000146 RID: 326
	public struct IntRange
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x004DF633 File Offset: 0x004DD833
		public IntRange(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x004DF643 File Offset: 0x004DD843
		public static IntRange operator *(IntRange range, float scale)
		{
			return new IntRange((int)((float)range.Minimum * scale), (int)((float)range.Maximum * scale));
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x004DF65E File Offset: 0x004DD85E
		public static IntRange operator *(float scale, IntRange range)
		{
			return range * scale;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x004DF667 File Offset: 0x004DD867
		public static IntRange operator /(IntRange range, float scale)
		{
			return new IntRange((int)((float)range.Minimum / scale), (int)((float)range.Maximum / scale));
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x004DF682 File Offset: 0x004DD882
		public static IntRange operator /(float scale, IntRange range)
		{
			return range / scale;
		}

		// Token: 0x04001514 RID: 5396
		[JsonProperty("Min")]
		public readonly int Minimum;

		// Token: 0x04001515 RID: 5397
		[JsonProperty("Max")]
		public readonly int Maximum;
	}
}
