using System;

namespace Terraria.Utilities
{
	// Token: 0x0200008C RID: 140
	public struct FastRandom
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x004A1994 File Offset: 0x0049FB94
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x004A199C File Offset: 0x0049FB9C
		public ulong Seed { readonly get; private set; }

		// Token: 0x06001452 RID: 5202 RVA: 0x004A19A5 File Offset: 0x0049FBA5
		public FastRandom(ulong seed)
		{
			this = default(FastRandom);
			this.Seed = seed;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x004A19B5 File Offset: 0x0049FBB5
		public FastRandom(int seed)
		{
			this = default(FastRandom);
			this.Seed = (ulong)((long)seed);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x004A19C6 File Offset: 0x0049FBC6
		public FastRandom WithModifier(ulong modifier)
		{
			return new FastRandom(FastRandom.NextSeed(modifier) ^ this.Seed);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x004A19DA File Offset: 0x0049FBDA
		public FastRandom WithModifier(int x, int y)
		{
			return this.WithModifier((ulong)((long)x + (long)((ulong)-1640531527) + ((long)y << 6) + (long)((ulong)((long)y) >> 2)));
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x004A19F8 File Offset: 0x0049FBF8
		public static FastRandom CreateWithRandomSeed()
		{
			return new FastRandom((ulong)((long)Guid.NewGuid().GetHashCode()));
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x004A1A1E File Offset: 0x0049FC1E
		public void NextSeed()
		{
			this.Seed = FastRandom.NextSeed(this.Seed);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x004A1A31 File Offset: 0x0049FC31
		private int NextBits(int bits)
		{
			this.Seed = FastRandom.NextSeed(this.Seed);
			return (int)(this.Seed >> 48 - bits);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x004A1A53 File Offset: 0x0049FC53
		public float NextFloat()
		{
			return (float)this.NextBits(24) * 5.9604645E-08f;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x004A1A64 File Offset: 0x0049FC64
		public double NextDouble()
		{
			return (double)((float)this.NextBits(32) * 4.656613E-10f);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x004A1A78 File Offset: 0x0049FC78
		public int Next(int max)
		{
			if ((max & -max) == max)
			{
				return (int)((long)max * (long)this.NextBits(31) >> 31);
			}
			int num;
			int num2;
			do
			{
				num = this.NextBits(31);
				num2 = num % max;
			}
			while (num - num2 + (max - 1) < 0);
			return num2;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x004A1AB5 File Offset: 0x0049FCB5
		public int Next(int min, int max)
		{
			return this.Next(max - min) + min;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x004A1AC2 File Offset: 0x0049FCC2
		private static ulong NextSeed(ulong seed)
		{
			return seed * 25214903917UL + 11UL & 281474976710655UL;
		}

		// Token: 0x040010A7 RID: 4263
		private const ulong RANDOM_MULTIPLIER = 25214903917UL;

		// Token: 0x040010A8 RID: 4264
		private const ulong RANDOM_ADD = 11UL;

		// Token: 0x040010A9 RID: 4265
		private const ulong RANDOM_MASK = 281474976710655UL;
	}
}
