using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000266 RID: 614
	public class ProjectileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06001F96 RID: 8086 RVA: 0x00516173 File Offset: 0x00514373
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractProj = -1;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0051617C File Offset: 0x0051437C
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			if (!settings.FullInteraction)
			{
				return false;
			}
			List<int> listOfProjectilesToInteractWithHack = settings.player.GetListOfProjectilesToInteractWithHack();
			bool flag = false;
			Vector2 mousevec = settings.mousevec;
			mousevec.ToPoint();
			int num = -1;
			float projectileDistanceFromCursor = -1f;
			for (int i = 0; i < listOfProjectilesToInteractWithHack.Count; i++)
			{
				int num2 = listOfProjectilesToInteractWithHack[i];
				Projectile projectile = Main.projectile[num2];
				if (projectile.active)
				{
					float num3 = projectile.Hitbox.Distance(mousevec);
					if (num == -1 || Main.projectile[num].Hitbox.Distance(mousevec) > num3)
					{
						num = num2;
						projectileDistanceFromCursor = num3;
					}
					if (num3 == 0f)
					{
						flag = true;
						num = num2;
						projectileDistanceFromCursor = num3;
						break;
					}
				}
			}
			if (settings.DemandOnlyZeroDistanceTargets && !flag)
			{
				return false;
			}
			if (num != -1)
			{
				this._candidate.Reuse(num, projectileDistanceFromCursor);
				candidate = this._candidate;
				return true;
			}
			return false;
		}

		// Token: 0x04004690 RID: 18064
		private ProjectileSmartInteractCandidateProvider.ReusableCandidate _candidate = new ProjectileSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000640 RID: 1600
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x170003CF RID: 975
			// (get) Token: 0x060033D7 RID: 13271 RVA: 0x00607585 File Offset: 0x00605785
			// (set) Token: 0x060033D8 RID: 13272 RVA: 0x0060758D File Offset: 0x0060578D
			public float DistanceFromCursor { get; private set; }

			// Token: 0x060033D9 RID: 13273 RVA: 0x00607596 File Offset: 0x00605796
			public void WinCandidacy()
			{
				Main.SmartInteractProj = this._projectileIndexToTarget;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x060033DA RID: 13274 RVA: 0x006075A9 File Offset: 0x006057A9
			public void Reuse(int projectileIndex, float projectileDistanceFromCursor)
			{
				this._projectileIndexToTarget = projectileIndex;
				this.DistanceFromCursor = projectileDistanceFromCursor;
			}

			// Token: 0x04006151 RID: 24913
			private int _projectileIndexToTarget;
		}
	}
}
