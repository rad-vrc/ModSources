using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005C8 RID: 1480
	public interface ISmartInteractCandidate
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060042D0 RID: 17104
		float DistanceFromCursor { get; }

		// Token: 0x060042D1 RID: 17105
		void WinCandidacy();
	}
}
