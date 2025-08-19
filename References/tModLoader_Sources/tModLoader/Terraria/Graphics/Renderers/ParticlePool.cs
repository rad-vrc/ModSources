using System;
using System.Collections.Generic;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000459 RID: 1113
	public class ParticlePool<T> where T : IPooledParticle
	{
		// Token: 0x060036A9 RID: 13993 RVA: 0x0057E108 File Offset: 0x0057C308
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

		// Token: 0x060036AA RID: 13994 RVA: 0x0057E144 File Offset: 0x0057C344
		public ParticlePool(int initialPoolSize, ParticlePool<T>.ParticleInstantiator instantiator)
		{
			this._particles = new List<T>(initialPoolSize);
			this._instantiator = instantiator;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x0057E160 File Offset: 0x0057C360
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
			T val = this._instantiator();
			this._particles.Add(val);
			val.FetchFromPool();
			return val;
		}

		// Token: 0x04005074 RID: 20596
		private ParticlePool<T>.ParticleInstantiator _instantiator;

		// Token: 0x04005075 RID: 20597
		private List<T> _particles;

		// Token: 0x02000B79 RID: 2937
		// (Invoke) Token: 0x06005CEF RID: 23791
		public delegate T ParticleInstantiator();
	}
}
