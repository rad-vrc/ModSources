using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000262 RID: 610
	public class SmartInteractSystem
	{
		// Token: 0x06001F8A RID: 8074 RVA: 0x005151C0 File Offset: 0x005133C0
		public SmartInteractSystem()
		{
			this._candidateProvidersByOrderOfPriority.Add(new PotionOfReturnSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new ProjectileSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new NPCSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new TileSmartInteractCandidateProvider());
			this._blockProviders.Add(new BlockBecauseYouAreOverAnImportantTile());
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00515244 File Offset: 0x00513444
		public void Clear()
		{
			this._candidates.Clear();
			foreach (ISmartInteractCandidateProvider smartInteractCandidateProvider in this._candidateProvidersByOrderOfPriority)
			{
				smartInteractCandidateProvider.ClearSelfAndPrepareForCheck();
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x005152A0 File Offset: 0x005134A0
		public void RunQuery(SmartInteractScanSettings settings)
		{
			this.Clear();
			using (List<ISmartInteractBlockReasonProvider>.Enumerator enumerator = this._blockProviders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ShouldBlockSmartInteract(settings))
					{
						return;
					}
				}
			}
			using (List<ISmartInteractCandidateProvider>.Enumerator enumerator2 = this._candidateProvidersByOrderOfPriority.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ISmartInteractCandidate smartInteractCandidate;
					if (enumerator2.Current.ProvideCandidate(settings, out smartInteractCandidate))
					{
						this._candidates.Add(smartInteractCandidate);
						if (smartInteractCandidate.DistanceFromCursor == 0f)
						{
							break;
						}
					}
				}
			}
			ISmartInteractCandidate smartInteractCandidate2 = null;
			foreach (ISmartInteractCandidate smartInteractCandidate3 in this._candidates)
			{
				if (smartInteractCandidate2 == null || smartInteractCandidate2.DistanceFromCursor > smartInteractCandidate3.DistanceFromCursor)
				{
					smartInteractCandidate2 = smartInteractCandidate3;
				}
			}
			if (smartInteractCandidate2 == null)
			{
				return;
			}
			smartInteractCandidate2.WinCandidacy();
		}

		// Token: 0x0400468A RID: 18058
		private List<ISmartInteractCandidateProvider> _candidateProvidersByOrderOfPriority = new List<ISmartInteractCandidateProvider>();

		// Token: 0x0400468B RID: 18059
		private List<ISmartInteractBlockReasonProvider> _blockProviders = new List<ISmartInteractBlockReasonProvider>();

		// Token: 0x0400468C RID: 18060
		private List<ISmartInteractCandidate> _candidates = new List<ISmartInteractCandidate>();
	}
}
