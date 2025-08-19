using System;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000121 RID: 289
	public interface IPooledParticle : IParticle
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001741 RID: 5953
		bool IsRestingInPool { get; }

		// Token: 0x06001742 RID: 5954
		void RestInPool();

		// Token: 0x06001743 RID: 5955
		void FetchFromPool();
	}
}
