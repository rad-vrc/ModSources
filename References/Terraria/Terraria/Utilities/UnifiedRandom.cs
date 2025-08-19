using System;

namespace Terraria.Utilities
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	public class UnifiedRandom
	{
		// Token: 0x060018ED RID: 6381 RVA: 0x004DF68B File Offset: 0x004DD88B
		public UnifiedRandom() : this(Environment.TickCount)
		{
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x004DF698 File Offset: 0x004DD898
		public UnifiedRandom(int Seed)
		{
			this.SetSeed(Seed);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x004DF6B4 File Offset: 0x004DD8B4
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

		// Token: 0x060018F0 RID: 6384 RVA: 0x004DF7BC File Offset: 0x004DD9BC
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x004DF7D0 File Offset: 0x004DD9D0
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

		// Token: 0x060018F2 RID: 6386 RVA: 0x004DF843 File Offset: 0x004DDA43
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x004DF84C File Offset: 0x004DDA4C
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			if (this.InternalSample() % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x004DF88C File Offset: 0x004DDA8C
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

		// Token: 0x060018F5 RID: 6389 RVA: 0x004DF8D7 File Offset: 0x004DDAD7
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", "maxValue must be positive.");
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x004DF8F7 File Offset: 0x004DDAF7
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x004DF900 File Offset: 0x004DDB00
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

		// Token: 0x04001516 RID: 5398
		private const int MBIG = 2147483647;

		// Token: 0x04001517 RID: 5399
		private const int MSEED = 161803398;

		// Token: 0x04001518 RID: 5400
		private const int MZ = 0;

		// Token: 0x04001519 RID: 5401
		private int inext;

		// Token: 0x0400151A RID: 5402
		private int inextp;

		// Token: 0x0400151B RID: 5403
		private int[] SeedArray = new int[56];
	}
}
