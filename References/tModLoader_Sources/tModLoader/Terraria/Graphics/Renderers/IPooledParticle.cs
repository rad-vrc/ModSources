using System;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000453 RID: 1107
	public interface IPooledParticle : IParticle
	{
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06003679 RID: 13945
		bool IsRestingInPool { get; }

		// Token: 0x0600367A RID: 13946
		void RestInPool();

		// Token: 0x0600367B RID: 13947
		void FetchFromPool();
	}
}
