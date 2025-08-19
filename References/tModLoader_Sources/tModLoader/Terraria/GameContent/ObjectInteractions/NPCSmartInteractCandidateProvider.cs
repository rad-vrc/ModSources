using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020005CA RID: 1482
	public class NPCSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x060042D4 RID: 17108 RVA: 0x005FA863 File Offset: 0x005F8A63
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractNPC = -1;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x005FA86C File Offset: 0x005F8A6C
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
				NPC nPC = Main.npc[i];
				if (nPC.active && (NPCLoader.CanChat(nPC) ?? nPC.townNPC) && nPC.Hitbox.Intersects(value) && !flag)
				{
					float num2 = nPC.Hitbox.Distance(mousevec);
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

		// Token: 0x040059D1 RID: 22993
		private NPCSmartInteractCandidateProvider.ReusableCandidate _candidate = new NPCSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000C67 RID: 3175
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x17000966 RID: 2406
			// (get) Token: 0x06005FEA RID: 24554 RVA: 0x006D1A49 File Offset: 0x006CFC49
			// (set) Token: 0x06005FEB RID: 24555 RVA: 0x006D1A51 File Offset: 0x006CFC51
			public float DistanceFromCursor { get; private set; }

			// Token: 0x06005FEC RID: 24556 RVA: 0x006D1A5A File Offset: 0x006CFC5A
			public void WinCandidacy()
			{
				Main.SmartInteractNPC = this._npcIndexToTarget;
				Main.SmartInteractShowingGenuine = true;
			}

			// Token: 0x06005FED RID: 24557 RVA: 0x006D1A6D File Offset: 0x006CFC6D
			public void Reuse(int npcIndex, float npcDistanceFromCursor)
			{
				this._npcIndexToTarget = npcIndex;
				this.DistanceFromCursor = npcDistanceFromCursor;
			}

			// Token: 0x04007988 RID: 31112
			private int _npcIndexToTarget;
		}
	}
}
