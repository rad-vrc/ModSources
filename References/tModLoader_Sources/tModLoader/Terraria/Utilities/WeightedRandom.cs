using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Utilities
{
	// Token: 0x02000094 RID: 148
	public class WeightedRandom<T>
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x004A2C68 File Offset: 0x004A0E68
		public WeightedRandom()
		{
			this.random = new UnifiedRandom();
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x004A2C8D File Offset: 0x004A0E8D
		public WeightedRandom(int seed)
		{
			this.random = new UnifiedRandom(seed);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x004A2CB3 File Offset: 0x004A0EB3
		public WeightedRandom(UnifiedRandom random)
		{
			this.random = random;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x004A2CD4 File Offset: 0x004A0ED4
		public WeightedRandom(params Tuple<T, double>[] theElements)
		{
			this.random = new UnifiedRandom();
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x004A2D05 File Offset: 0x004A0F05
		public WeightedRandom(int seed, params Tuple<T, double>[] theElements)
		{
			this.random = new UnifiedRandom(seed);
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x004A2D37 File Offset: 0x004A0F37
		public WeightedRandom(UnifiedRandom random, params Tuple<T, double>[] theElements)
		{
			this.random = random;
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x004A2D64 File Offset: 0x004A0F64
		public void Add(T element, double weight = 1.0)
		{
			this.elements.Add(new Tuple<T, double>(element, weight));
			this.needsRefresh = true;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x004A2D80 File Offset: 0x004A0F80
		public T Get()
		{
			if (this.needsRefresh)
			{
				this.CalculateTotalWeight();
			}
			double num = this.random.NextDouble();
			num *= this._totalWeight;
			foreach (Tuple<T, double> element in this.elements)
			{
				if (num <= element.Item2)
				{
					return element.Item1;
				}
				num -= element.Item2;
			}
			return default(T);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x004A2E18 File Offset: 0x004A1018
		public void CalculateTotalWeight()
		{
			this._totalWeight = 0.0;
			foreach (Tuple<T, double> element in this.elements)
			{
				this._totalWeight += element.Item2;
			}
			this.needsRefresh = false;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x004A2E90 File Offset: 0x004A1090
		public void Clear()
		{
			this.elements.Clear();
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x004A2E9D File Offset: 0x004A109D
		public static implicit operator T(WeightedRandom<T> weightedRandom)
		{
			return weightedRandom.Get();
		}

		// Token: 0x040010B6 RID: 4278
		public readonly List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

		// Token: 0x040010B7 RID: 4279
		public readonly UnifiedRandom random;

		// Token: 0x040010B8 RID: 4280
		public bool needsRefresh = true;

		// Token: 0x040010B9 RID: 4281
		private double _totalWeight;
	}
}
