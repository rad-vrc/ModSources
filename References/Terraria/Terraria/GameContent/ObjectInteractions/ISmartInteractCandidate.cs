using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x0200025E RID: 606
	public interface ISmartInteractCandidate
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001F85 RID: 8069
		float DistanceFromCursor { get; }

		// Token: 0x06001F86 RID: 8070
		void WinCandidacy();
	}
}
