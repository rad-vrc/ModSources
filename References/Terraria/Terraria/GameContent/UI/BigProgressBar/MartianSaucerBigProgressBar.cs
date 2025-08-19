using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003B5 RID: 949
	public class MartianSaucerBigProgressBar : IBigProgressBar
	{
		// Token: 0x06002A07 RID: 10759 RVA: 0x0059670C File Offset: 0x0059490C
		public MartianSaucerBigProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x00596770 File Offset: 0x00594970
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active || npc.type != 395)
			{
				if (!this.TryFindingAnotherMartianSaucerPiece(ref info))
				{
					return false;
				}
				npc = Main.npc[info.npcIndexToAimAt];
			}
			int num = 0;
			if (Main.expertMode)
			{
				this._referenceDummy.SetDefaults(395, npc.GetMatchingSpawnParams());
				num += this._referenceDummy.lifeMax;
			}
			this._referenceDummy.SetDefaults(394, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 2;
			this._referenceDummy.SetDefaults(393, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 2;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && this.ValidIdsToScanHp.Contains(npc2.type) && (Main.expertMode || npc2.type != 395))
				{
					num2 += (float)npc2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x005968B4 File Offset: 0x00594AB4
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[395];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00596908 File Offset: 0x00594B08
		private bool TryFindingAnotherMartianSaucerPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && this.ValidIds.Contains(npc.type))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004CD8 RID: 19672
		private BigProgressBarCache _cache;

		// Token: 0x04004CD9 RID: 19673
		private NPC _referenceDummy;

		// Token: 0x04004CDA RID: 19674
		private HashSet<int> ValidIds = new HashSet<int>
		{
			395
		};

		// Token: 0x04004CDB RID: 19675
		private HashSet<int> ValidIdsToScanHp = new HashSet<int>
		{
			395,
			393,
			394
		};
	}
}
