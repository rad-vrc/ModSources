using System;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020003E9 RID: 1001
	public interface IAnimationSegment
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06002AD8 RID: 10968
		float DedicatedTimeNeeded { get; }

		// Token: 0x06002AD9 RID: 10969
		void Draw(ref GameAnimationSegment info);
	}
}
