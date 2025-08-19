using System;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000260 RID: 608
	public interface ISmartInteractCandidateProvider
	{
		// Token: 0x06001F88 RID: 8072
		void ClearSelfAndPrepareForCheck();

		// Token: 0x06001F89 RID: 8073
		bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate);
	}
}
