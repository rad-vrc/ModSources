using System;

namespace Terraria.GameContent.Animations
{
	// Token: 0x020003EC RID: 1004
	public interface IAnimationSegmentAction<T>
	{
		// Token: 0x06002ADF RID: 10975
		void BindTo(T obj);

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06002AE0 RID: 10976
		int ExpectedLengthOfActionInFrames { get; }

		// Token: 0x06002AE1 RID: 10977
		void ApplyTo(T obj, float localTimeForObj);

		// Token: 0x06002AE2 RID: 10978
		void SetDelay(float delay);
	}
}
