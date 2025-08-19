using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000551 RID: 1361
	public class MartianSaucerBigProgressBar : IBigProgressBar
	{
		// Token: 0x06004044 RID: 16452 RVA: 0x005DF7B4 File Offset: 0x005DD9B4
		public MartianSaucerBigProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x005DF818 File Offset: 0x005DDA18
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[info.npcIndexToAimAt];
			if (!nPC.active || nPC.type != 395)
			{
				if (!this.TryFindingAnotherMartianSaucerPiece(ref info))
				{
					return false;
				}
				nPC = Main.npc[info.npcIndexToAimAt];
			}
			int num = 0;
			if (Main.expertMode)
			{
				this._referenceDummy.SetDefaults(395, nPC.GetMatchingSpawnParams());
				num += this._referenceDummy.lifeMax;
			}
			this._referenceDummy.SetDefaults(394, nPC.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 2;
			this._referenceDummy.SetDefaults(393, nPC.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 2;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC2 = Main.npc[i];
				if (nPC2.active && this.ValidIdsToScanHp.Contains(nPC2.type) && (Main.expertMode || nPC2.type != 395))
				{
					num2 += (float)nPC2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x005DF95C File Offset: 0x005DDB5C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[395];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x005DF9B0 File Offset: 0x005DDBB0
		private bool TryFindingAnotherMartianSaucerPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && this.ValidIds.Contains(nPC.type))
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04005867 RID: 22631
		private BigProgressBarCache _cache;

		// Token: 0x04005868 RID: 22632
		private NPC _referenceDummy;

		// Token: 0x04005869 RID: 22633
		private HashSet<int> ValidIds = new HashSet<int>
		{
			395
		};

		// Token: 0x0400586A RID: 22634
		private HashSet<int> ValidIdsToScanHp = new HashSet<int>
		{
			395,
			393,
			394
		};
	}
}
