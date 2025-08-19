using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000552 RID: 1362
	public class MoonLordProgressBar : IBigProgressBar
	{
		// Token: 0x06004048 RID: 16456 RVA: 0x005DF9FC File Offset: 0x005DDBFC
		public MoonLordProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x005DFA4C File Offset: 0x005DDC4C
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[info.npcIndexToAimAt];
			if ((!nPC.active || this.IsInBadAI(nPC)) && !this.TryFindingAnotherMoonLordPiece(ref info))
			{
				return false;
			}
			int num = 0;
			NPCSpawnParams spawnparams = new NPCSpawnParams
			{
				strengthMultiplierOverride = new float?(nPC.strengthMultiplier),
				playerCountForMultiplayerDifficultyOverride = new int?(nPC.statsAreScaledForThisManyPlayers)
			};
			this._referenceDummy.SetDefaults(398, spawnparams);
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(396, spawnparams);
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(397, spawnparams);
			num += this._referenceDummy.lifeMax;
			num += this._referenceDummy.lifeMax;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC2 = Main.npc[i];
				if (nPC2.active && this.ValidIds.Contains(nPC2.type) && !this.IsInBadAI(nPC2))
				{
					num2 += (float)nPC2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x005DFBA0 File Offset: 0x005DDDA0
		private bool IsInBadAI(NPC npc)
		{
			return (npc.type == 398 && (npc.ai[0] == 2f || npc.ai[0] == -1f)) || (npc.type == 398 && npc.localAI[3] == 0f) || (npc.ai[0] == -2f || npc.ai[0] == -3f);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x005DFC1C File Offset: 0x005DDE1C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[396];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x005DFC70 File Offset: 0x005DDE70
		private bool TryFindingAnotherMoonLordPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && this.ValidIds.Contains(nPC.type) && !this.IsInBadAI(nPC))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400586B RID: 22635
		private BigProgressBarCache _cache;

		// Token: 0x0400586C RID: 22636
		private NPC _referenceDummy;

		// Token: 0x0400586D RID: 22637
		private HashSet<int> ValidIds = new HashSet<int>
		{
			396,
			397,
			398
		};
	}
}
