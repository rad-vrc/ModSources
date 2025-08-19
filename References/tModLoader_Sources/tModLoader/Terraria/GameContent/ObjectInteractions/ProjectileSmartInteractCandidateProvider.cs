using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005CE RID: 1486
	public class ProjectileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x060042E1 RID: 17121 RVA: 0x005FAB36 File Offset: 0x005F8D36
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractProj = -1;
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x005FAB40 File Offset: 0x005F8D40
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

		// Token: 0x040059D3 RID: 22995
		private ProjectileSmartInteractCandidateProvider.ReusableCandidate _candidate = new ProjectileSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000C69 RID: 3177
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x17000968 RID: 2408
			// (get) Token: 0x06005FF4 RID: 24564 RVA: 0x006D1AB5 File Offset: 0x006CFCB5
			// (set) Token: 0x06005FF5 RID: 24565 RVA: 0x006D1ABD File Offset: 0x006CFCBD
			public float DistanceFromCursor { get; private set; }

			// Token: 0x06005FF6 RID: 24566 RVA: 0x006D1AC6 File Offset: 0x006CFCC6
			public void WinCandidacy()
			{
				Main.SmartInteractProj = this._projectileIndexToTarget;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x06005FF7 RID: 24567 RVA: 0x006D1AD9 File Offset: 0x006CFCD9
			public void Reuse(int projectileIndex, float projectileDistanceFromCursor)
			{
				this._projectileIndexToTarget = projectileIndex;
				this.DistanceFromCursor = projectileDistanceFromCursor;
			}

			// Token: 0x0400798B RID: 31115
			private int _projectileIndexToTarget;
		}
	}
}
