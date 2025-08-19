using System;

namespace Terraria.Utilities
{
	// Token: 0x02000143 RID: 323
	public struct FastRandom
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x004DF146 File Offset: 0x004DD346
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x004DF14E File Offset: 0x004DD34E
		public ulong Seed { get; private set; }

		// Token: 0x060018D3 RID: 6355 RVA: 0x004DF157 File Offset: 0x004DD357
		public FastRandom(ulong seed)
		{
			this = default(FastRandom);
			this.Seed = seed;
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x004DF167 File Offset: 0x004DD367
		public FastRandom(int seed)
		{
			this = default(FastRandom);
			this.Seed = (ulong)((long)seed);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x004DF178 File Offset: 0x004DD378
		public FastRandom WithModifier(ulong modifier)
		{
			return new FastRandom(FastRandom.NextSeed(modifier) ^ this.Seed);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x004DF18C File Offset: 0x004DD38C
		public FastRandom WithModifier(int x, int y)
		{
			return this.WithModifier((ulong)((long)x + (long)((ulong)-1640531527) + ((long)y << 6) + (long)((ulong)((long)y) >> 2)));
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x004DF1A8 File Offset: 0x004DD3A8
		public static FastRandom CreateWithRandomSeed()
		{
			return new FastRandom((ulong)((long)Guid.NewGuid().GetHashCode()));
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x004DF1CE File Offset: 0x004DD3CE
		public void NextSeed()
		{
			this.Seed = FastRandom.NextSeed(this.Seed);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x004DF1E1 File Offset: 0x004DD3E1
		private int NextBits(int bits)
		{
			this.Seed = FastRandom.NextSeed(this.Seed);
			return (int)(this.Seed >> 48 - bits);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x004DF203 File Offset: 0x004DD403
		public float NextFloat()
		{
			return (float)this.NextBits(24) * 5.9604645E-08f;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x004DF214 File Offset: 0x004DD414
		public double NextDouble()
		{
			return (double)((float)this.NextBits(32) * 4.656613E-10f);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x004DF228 File Offset: 0x004DD428
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

		// Token: 0x060018DD RID: 6365 RVA: 0x004DF265 File Offset: 0x004DD465
		public int Next(int min, int max)
		{
			return this.Next(max - min) + min;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x003EFBF1 File Offset: 0x003EDDF1
		private static ulong NextSeed(ulong seed)
		{
			return seed * 25214903917UL + 11UL & 281474976710655UL;
		}

		// Token: 0x04001510 RID: 5392
		private const ulong RANDOM_MULTIPLIER = 25214903917UL;

		// Token: 0x04001511 RID: 5393
		private const ulong RANDOM_ADD = 11UL;

		// Token: 0x04001512 RID: 5394
		private const ulong RANDOM_MASK = 281474976710655UL;
	}
}
