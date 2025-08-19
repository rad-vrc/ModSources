using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005C9 RID: 1481
	public interface ISmartInteractCandidateProvider
	{
		// Token: 0x060042D2 RID: 17106
		void ClearSelfAndPrepareForCheck();

		// Token: 0x060042D3 RID: 17107
		bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate);
	}
}
