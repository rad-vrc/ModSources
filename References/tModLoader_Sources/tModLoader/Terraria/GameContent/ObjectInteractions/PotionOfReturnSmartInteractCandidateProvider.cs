using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005CD RID: 1485
	public class PotionOfReturnSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x060042DE RID: 17118 RVA: 0x005FAA8A File Offset: 0x005F8C8A
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractPotionOfReturn = false;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x005FAA94 File Offset: 0x005F8C94
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			Rectangle homeHitbox;
			if (!PotionOfReturnHelper.TryGetGateHitbox(settings.player, out homeHitbox))
			{
				return false;
			}
			Vector2 vector = homeHitbox.ClosestPointInRect(settings.mousevec);
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

		// Token: 0x040059D2 RID: 22994
		private PotionOfReturnSmartInteractCandidateProvider.ReusableCandidate _candidate = new PotionOfReturnSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000C68 RID: 3176
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x17000967 RID: 2407
			// (get) Token: 0x06005FEF RID: 24559 RVA: 0x006D1A85 File Offset: 0x006CFC85
			// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x006D1A8D File Offset: 0x006CFC8D
			public float DistanceFromCursor { get; private set; }

			// Token: 0x06005FF1 RID: 24561 RVA: 0x006D1A96 File Offset: 0x006CFC96
			public void WinCandidacy()
			{
				Main.SmartInteractPotionOfReturn = true;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x06005FF2 RID: 24562 RVA: 0x006D1AA4 File Offset: 0x006CFCA4
			public void Reuse(float distanceFromCursor)
			{
				this.DistanceFromCursor = distanceFromCursor;
			}
		}
	}
}
