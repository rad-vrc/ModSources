using System;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020006B0 RID: 1712
	public interface IAnimationSegmentAction<T>
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06004886 RID: 18566
		int ExpectedLengthOfActionInFrames { get; }

		// Token: 0x06004887 RID: 18567
		void BindTo(T obj);

		// Token: 0x06004888 RID: 18568
		void ApplyTo(T obj, float localTimeForObj);

		// Token: 0x06004889 RID: 18569
		void SetDelay(float delay);
	}
}
