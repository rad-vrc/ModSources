using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x02000265 RID: 613
	public class NPCSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x06001F93 RID: 8083 RVA: 0x0051602E File Offset: 0x0051422E
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractNPC = -1;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00516038 File Offset: 0x00514238
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			if (!settings.FullInteraction)
			{
				return false;
			}
			Rectangle value = Utils.CenteredRectangle(settings.player.Center, new Vector2((float)Player.tileRangeX, (float)Player.tileRangeY) * 16f * 2f);
			Vector2 mousevec = settings.mousevec;
			mousevec.ToPoint();
			bool flag = false;
			int num = -1;
			float npcDistanceFromCursor = -1f;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.townNPC && npc.Hitbox.Intersects(value) && !flag)
				{
					float num2 = npc.Hitbox.Distance(mousevec);
					if (num == -1 || Main.npc[num].Hitbox.Distance(mousevec) > num2)
					{
						num = i;
						npcDistanceFromCursor = num2;
					}
					if (num2 == 0f)
					{
						flag = true;
						num = i;
						npcDistanceFromCursor = num2;
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
				this._candidate.Reuse(num, npcDistanceFromCursor);
				candidate = this._candidate;
				return true;
			}
			return false;
		}

		// Token: 0x0400468F RID: 18063
		private NPCSmartInteractCandidateProvider.ReusableCandidate _candidate = new NPCSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x0200063F RID: 1599
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x170003CE RID: 974
			// (get) Token: 0x060033D2 RID: 13266 RVA: 0x00607551 File Offset: 0x00605751
			// (set) Token: 0x060033D3 RID: 13267 RVA: 0x00607559 File Offset: 0x00605759
			public float DistanceFromCursor { get; private set; }

			// Token: 0x060033D4 RID: 13268 RVA: 0x00607562 File Offset: 0x00605762
			public void WinCandidacy()
			{
				Main.SmartInteractNPC = this._npcIndexToTarget;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x060033D5 RID: 13269 RVA: 0x00607575 File Offset: 0x00605775
			public void Reuse(int npcIndex, float npcDistanceFromCursor)
			{
				this._npcIndexToTarget = npcIndex;
				this.DistanceFromCursor = npcDistanceFromCursor;
			}

			// Token: 0x0400614F RID: 24911
			private int _npcIndexToTarget;
		}
	}
}
