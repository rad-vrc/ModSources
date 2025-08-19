using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000078 RID: 120
	public class WorldGenRange
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0048E1A4 File Offset: 0x0048C3A4
		public int ScaledMinimum
		{
			get
			{
				return this.ScaleValue(this.Minimum);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0048E1B2 File Offset: 0x0048C3B2
		public int ScaledMaximum
		{
			get
			{
				return this.ScaleValue(this.Maximum);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0048E1C0 File Offset: 0x0048C3C0
		public WorldGenRange(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0048E1D6 File Offset: 0x0048C3D6
		public int GetRandom(UnifiedRandom random)
		{
			return random.Next(this.ScaledMinimum, this.ScaledMaximum + 1);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0048E1EC File Offset: 0x0048C3EC
		private int ScaleValue(int value)
		{
			double num = 1.0;
			switch (this.ScaleWith)
			{
			case WorldGenRange.ScalingMode.None:
				num = 1.0;
				break;
			case WorldGenRange.ScalingMode.WorldArea:
				num = (double)(Main.maxTilesX * Main.maxTilesY) / 5040000.0;
				break;
			case WorldGenRange.ScalingMode.WorldWidth:
				num = (double)Main.maxTilesX / 4200.0;
				break;
			}
			return (int)(num * (double)value);
		}

		// Token: 0x04000FC1 RID: 4033
		public static readonly WorldGenRange Empty = new WorldGenRange(0, 0);

		// Token: 0x04000FC2 RID: 4034
		[JsonProperty("Min")]
		public readonly int Minimum;

		// Token: 0x04000FC3 RID: 4035
		[JsonProperty("Max")]
		public readonly int Maximum;

		// Token: 0x04000FC4 RID: 4036
		[JsonProperty]
		[JsonConverter(typeof(StringEnumConverter))]
		public readonly WorldGenRange.ScalingMode ScaleWith;

		// Token: 0x0200053A RID: 1338
		public enum ScalingMode
		{
			// Token: 0x0400581D RID: 22557
			None,
			// Token: 0x0400581E RID: 22558
			WorldArea,
			// Token: 0x0400581F RID: 22559
			WorldWidth
		}
	}
}
