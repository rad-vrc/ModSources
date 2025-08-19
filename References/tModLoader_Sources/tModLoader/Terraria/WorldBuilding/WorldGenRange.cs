using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000087 RID: 135
	public class WorldGenRange
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x004A104C File Offset: 0x0049F24C
		public int ScaledMinimum
		{
			get
			{
				return this.ScaleValue(this.Minimum);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x004A105A File Offset: 0x0049F25A
		public int ScaledMaximum
		{
			get
			{
				return this.ScaleValue(this.Maximum);
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x004A1068 File Offset: 0x0049F268
		public WorldGenRange(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x004A107E File Offset: 0x0049F27E
		public int GetRandom(UnifiedRandom random)
		{
			return random.Next(this.ScaledMinimum, this.ScaledMaximum + 1);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x004A1094 File Offset: 0x0049F294
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

		// Token: 0x0400109E RID: 4254
		public static readonly WorldGenRange Empty = new WorldGenRange(0, 0);

		// Token: 0x0400109F RID: 4255
		[JsonProperty("Min")]
		public readonly int Minimum;

		// Token: 0x040010A0 RID: 4256
		[JsonProperty("Max")]
		public readonly int Maximum;

		// Token: 0x040010A1 RID: 4257
		[JsonProperty]
		[JsonConverter(typeof(StringEnumConverter))]
		public readonly WorldGenRange.ScalingMode ScaleWith;

		// Token: 0x02000855 RID: 2133
		public enum ScalingMode
		{
			// Token: 0x040068D5 RID: 26837
			None,
			// Token: 0x040068D6 RID: 26838
			WorldArea,
			// Token: 0x040068D7 RID: 26839
			WorldWidth
		}
	}
}
