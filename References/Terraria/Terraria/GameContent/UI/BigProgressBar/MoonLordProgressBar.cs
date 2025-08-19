using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003AB RID: 939
	public class MoonLordProgressBar : IBigProgressBar
	{
		// Token: 0x060029E0 RID: 10720 RVA: 0x00595FA4 File Offset: 0x005941A4
		public MoonLordProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00595FF4 File Offset: 0x005941F4
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if ((!npc.active || this.IsInBadAI(npc)) && !this.TryFindingAnotherMoonLordPiece(ref info))
			{
				return false;
			}
			int num = 0;
			NPCSpawnParams spawnparams = new NPCSpawnParams
			{
				strengthMultiplierOverride = new float?(npc.strengthMultiplier),
				playerCountForMultiplayerDifficultyOverride = new int?(npc.statsAreScaledForThisManyPlayers)
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
				NPC npc2 = Main.npc[i];
				if (npc2.active && this.ValidIds.Contains(npc2.type) && !this.IsInBadAI(npc2))
				{
					num2 += (float)npc2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00596144 File Offset: 0x00594344
		private bool IsInBadAI(NPC npc)
		{
			return (npc.type == 398 && (npc.ai[0] == 2f || npc.ai[0] == -1f)) || (npc.type == 398 && npc.localAI[3] == 0f) || (npc.ai[0] == -2f || npc.ai[0] == -3f);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x005961C0 File Offset: 0x005943C0
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[396];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00596214 File Offset: 0x00594414
		private bool TryFindingAnotherMoonLordPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && this.ValidIds.Contains(npc.type) && !this.IsInBadAI(npc))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004CCA RID: 19658
		private BigProgressBarCache _cache;

		// Token: 0x04004CCB RID: 19659
		private NPC _referenceDummy;

		// Token: 0x04004CCC RID: 19660
		private HashSet<int> ValidIds = new HashSet<int>
		{
			396,
			397,
			398
		};
	}
}
