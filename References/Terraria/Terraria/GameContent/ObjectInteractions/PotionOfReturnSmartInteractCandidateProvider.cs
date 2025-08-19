using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000267 RID: 615
	public class PotionOfReturnSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06001F99 RID: 8089 RVA: 0x00516273 File Offset: 0x00514473
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractPotionOfReturn = false;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0051627C File Offset: 0x0051447C
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			Rectangle r;
			if (!PotionOfReturnHelper.TryGetGateHitbox(settings.player, out r))
			{
				return false;
			}
			Vector2 vector = r.ClosestPointInRect(settings.mousevec);
			float distanceFromCursor = vector.Distance(settings.mousevec);
			Point point = vector.ToTileCoordinates();
			if (point.X < settings.LX || point.X > settings.HX || point.Y < settings.LY || point.Y > settings.HY)
			{
				return false;
			}
			this._candidate.Reuse(distanceFromCursor);
			candidate = this._candidate;
			return true;
		}

		// Token: 0x04004691 RID: 18065
		private PotionOfReturnSmartInteractCandidateProvider.ReusableCandidate _candidate = new PotionOfReturnSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000641 RID: 1601
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x060033DC RID: 13276 RVA: 0x006075B9 File Offset: 0x006057B9
			// (set) Token: 0x060033DD RID: 13277 RVA: 0x006075C1 File Offset: 0x006057C1
			public float DistanceFromCursor { get; private set; }

			// Token: 0x060033DE RID: 13278 RVA: 0x006075CA File Offset: 0x006057CA
			public void WinCandidacy()
			{
				Main.SmartInteractPotionOfReturn = true;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x060033DF RID: 13279 RVA: 0x006075D8 File Offset: 0x006057D8
			public void Reuse(float distanceFromCursor)
			{
				this.DistanceFromCursor = distanceFromCursor;
			}
		}
	}
}
