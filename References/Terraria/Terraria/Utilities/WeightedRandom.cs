using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Utilities
{
	// Token: 0x02000148 RID: 328
	public class WeightedRandom<T>
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x004DF939 File Offset: 0x004DDB39
		public WeightedRandom()
		{
			this.random = new UnifiedRandom();
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x004DF95E File Offset: 0x004DDB5E
		public WeightedRandom(int seed)
		{
			this.random = new UnifiedRandom(seed);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x004DF984 File Offset: 0x004DDB84
		public WeightedRandom(UnifiedRandom random)
		{
			this.random = random;
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x004DF9A5 File Offset: 0x004DDBA5
		public WeightedRandom(params Tuple<T, double>[] theElements)
		{
			this.random = new UnifiedRandom();
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x004DF9D6 File Offset: 0x004DDBD6
		public WeightedRandom(int seed, params Tuple<T, double>[] theElements)
		{
			this.random = new UnifiedRandom(seed);
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x004DFA08 File Offset: 0x004DDC08
		public WeightedRandom(UnifiedRandom random, params Tuple<T, double>[] theElements)
		{
			this.random = random;
			this.elements = theElements.ToList<Tuple<T, double>>();
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x004DFA35 File Offset: 0x004DDC35
		public void Add(T element, double weight = 1.0)
		{
			this.elements.Add(new Tuple<T, double>(element, weight));
			this.needsRefresh = true;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x004DFA50 File Offset: 0x004DDC50
		public T Get()
		{
			if (this.needsRefresh)
			{
				this.CalculateTotalWeight();
			}
			double num = this.random.NextDouble();
			num *= this._totalWeight;
			foreach (Tuple<T, double> tuple in this.elements)
			{
				if (num <= tuple.Item2)
				{
					return tuple.Item1;
				}
				num -= tuple.Item2;
			}
			return default(T);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x004DFAE8 File Offset: 0x004DDCE8
		public void CalculateTotalWeight()
		{
			this._totalWeight = 0.0;
			foreach (Tuple<T, double> tuple in this.elements)
			{
				this._totalWeight += tuple.Item2;
			}
			this.needsRefresh = false;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x004DFB60 File Offset: 0x004DDD60
		public void Clear()
		{
			this.elements.Clear();
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x004DFB6D File Offset: 0x004DDD6D
		public static implicit operator T(WeightedRandom<T> weightedRandom)
		{
			return weightedRandom.Get();
		}

		// Token: 0x0400151C RID: 5404
		public readonly List<Tuple<T, double>> elements = new List<Tuple<T, double>>();

		// Token: 0x0400151D RID: 5405
		public readonly UnifiedRandom random;

		// Token: 0x0400151E RID: 5406
		public bool needsRefresh = true;

		// Token: 0x0400151F RID: 5407
		private double _totalWeight;
	}
}
