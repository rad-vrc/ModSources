using System;
using System.Collections.Generic;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000120 RID: 288
	public class ParticlePool<T> where T : IPooledParticle
	{
		// Token: 0x0600173E RID: 5950 RVA: 0x004D1E88 File Offset: 0x004D0088
		public int CountParticlesInUse()
		{
			int num = 0;
			for (int i = 0; i < num; i++)
			{
				T t = this._particles[i];
				if (!t.IsRestingInPool)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x004D1EC4 File Offset: 0x004D00C4
		public ParticlePool(int initialPoolSize, ParticlePool<T>.ParticleInstantiator instantiator)
		{
			this._particles = new List<T>(initialPoolSize);
			this._instantiator = instantiator;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x004D1EE0 File Offset: 0x004D00E0
		public T RequestParticle()
		{
			int count = this._particles.Count;
			for (int i = 0; i < count; i++)
			{
				T t = this._particles[i];
				if (t.IsRestingInPool)
				{
					t = this._particles[i];
					t.FetchFromPool();
					return this._particles[i];
				}
			}
			T t2 = this._instantiator();
			this._particles.Add(t2);
			t2.FetchFromPool();
			return t2;
		}

		// Token: 0x040013F3 RID: 5107
		private ParticlePool<T>.ParticleInstantiator _instantiator;

		// Token: 0x040013F4 RID: 5108
		private List<T> _particles;

		// Token: 0x020005A0 RID: 1440
		// (Invoke) Token: 0x0600324C RID: 12876
		public delegate T ParticleInstantiator();
	}
}
