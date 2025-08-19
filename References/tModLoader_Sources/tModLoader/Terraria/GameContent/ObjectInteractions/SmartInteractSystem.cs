using System;
using System.Collections.Generic;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005D0 RID: 1488
	public class SmartInteractSystem
	{
		// Token: 0x060042E4 RID: 17124 RVA: 0x005FAC34 File Offset: 0x005F8E34
		public SmartInteractSystem()
		{
			this._candidateProvidersByOrderOfPriority.Add(new PotionOfReturnSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new ProjectileSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new NPCSmartInteractCandidateProvider());
			this._candidateProvidersByOrderOfPriority.Add(new TileSmartInteractCandidateProvider());
			this._blockProviders.Add(new BlockBecauseYouAreOverAnImportantTile());
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x005FACB8 File Offset: 0x005F8EB8
		public void Clear()
		{
			this._candidates.Clear();
			foreach (ISmartInteractCandidateProvider smartInteractCandidateProvider in this._candidateProvidersByOrderOfPriority)
			{
				smartInteractCandidateProvider.ClearSelfAndPrepareForCheck();
			}
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x005FAD14 File Offset: 0x005F8F14
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
					ISmartInteractCandidate candidate;
					if (enumerator2.Current.ProvideCandidate(settings, out candidate))
					{
						this._candidates.Add(candidate);
						if (candidate.DistanceFromCursor == 0f)
						{
							break;
						}
					}
				}
			}
			ISmartInteractCandidate smartInteractCandidate = null;
			foreach (ISmartInteractCandidate candidate2 in this._candidates)
			{
				if (smartInteractCandidate == null || smartInteractCandidate.DistanceFromCursor > candidate2.DistanceFromCursor)
				{
					smartInteractCandidate = candidate2;
				}
			}
			if (smartInteractCandidate != null)
			{
				smartInteractCandidate.WinCandidacy();
			}
		}

		// Token: 0x040059DC RID: 23004
		private List<ISmartInteractCandidateProvider> _candidateProvidersByOrderOfPriority = new List<ISmartInteractCandidateProvider>();

		// Token: 0x040059DD RID: 23005
		private List<ISmartInteractBlockReasonProvider> _blockProviders = new List<ISmartInteractBlockReasonProvider>();

		// Token: 0x040059DE RID: 23006
		private List<ISmartInteractCandidate> _candidates = new List<ISmartInteractCandidate>();
	}
}
