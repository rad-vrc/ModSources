using System;

namespace Terraria.Utilities
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	public class UnifiedRandom
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x004A281D File Offset: 0x004A0A1D
		public UnifiedRandom() : this(Environment.TickCount)
		{
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x004A282A File Offset: 0x004A0A2A
		public UnifiedRandom(int Seed)
		{
			this.SetSeed(Seed);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x004A2848 File Offset: 0x004A0A48
		public void SetSeed(int Seed)
		{
			for (int i = 0; i < this.SeedArray.Length; i++)
			{
				this.SeedArray[i] = 0;
			}
			int num = (Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed);
			int num2 = 161803398 - num;
			this.SeedArray[55] = num2;
			int num3 = 1;
			for (int j = 1; j < 55; j++)
			{
				int num4 = 21 * j % 55;
				this.SeedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = this.SeedArray[num4];
			}
			for (int k = 1; k < 5; k++)
			{
				for (int l = 1; l < 56; l++)
				{
					this.SeedArray[l] -= this.SeedArray[1 + (l + 30) % 55];
					if (this.SeedArray[l] < 0)
					{
						this.SeedArray[l] += int.MaxValue;
					}
				}
			}
			this.inext = 0;
			this.inextp = 21;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x004A294E File Offset: 0x004A0B4E
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x004A2964 File Offset: 0x004A0B64
		private int InternalSample()
		{
			int num = this.inext;
			int num2 = this.inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this.SeedArray[num] - this.SeedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this.SeedArray[num] = num3;
			this.inext = num;
			this.inextp = num2;
			return num3;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x004A29D7 File Offset: 0x004A0BD7
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x004A29E0 File Offset: 0x004A0BE0
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			if (this.InternalSample() % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}

		/// <summary>
		/// Generates a random value between <paramref name="minValue" /> (inclusive) and <paramref name="maxValue" /> (exclusive). <br />For example <c>Next(4, 8)</c> can return 4, 5, 6, or 7. It will not return 8.
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
		// Token: 0x0600148F RID: 5263 RVA: 0x004A2A1C File Offset: 0x004A0C1C
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		/// <summary>
		/// Generates a random value between 0 (inclusive) and <paramref name="maxValue" /> (exclusive). <br />For example <c>Next(4)</c> can return one of 4 values: 0, 1, 2, or 3. It will not return 4.
		/// </summary>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException"></exception>
		// Token: 0x06001490 RID: 5264 RVA: 0x004A2A67 File Offset: 0x004A0C67
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", "maxValue must be positive.");
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x004A2A87 File Offset: 0x004A0C87
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x004A2A90 File Offset: 0x004A0C90
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.InternalSample() % 256);
			}
		}

		// Token: 0x040010AF RID: 4271
		private const int MBIG = 2147483647;

		// Token: 0x040010B0 RID: 4272
		private const int MSEED = 161803398;

		// Token: 0x040010B1 RID: 4273
		private const int MZ = 0;

		// Token: 0x040010B2 RID: 4274
		private int inext;

		// Token: 0x040010B3 RID: 4275
		private int inextp;

		// Token: 0x040010B4 RID: 4276
		private int[] SeedArray = new int[56];
	}
}
