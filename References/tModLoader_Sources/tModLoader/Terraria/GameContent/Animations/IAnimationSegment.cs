using System;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020006AF RID: 1711
	public interface IAnimationSegment
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06004884 RID: 18564
		float DedicatedTimeNeeded { get; }

		// Token: 0x06004885 RID: 18565
		void Draw(ref GameAnimationSegment info);
	}
}
