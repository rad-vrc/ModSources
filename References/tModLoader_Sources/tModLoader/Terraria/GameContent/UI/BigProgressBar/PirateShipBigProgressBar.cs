using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000555 RID: 1365
	public class PirateShipBigProgressBar : IBigProgressBar
	{
		// Token: 0x06004054 RID: 16468 RVA: 0x005DFCF4 File Offset: 0x005DDEF4
		public PirateShipBigProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x005DFD20 File Offset: 0x005DDF20
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[info.npcIndexToAimAt];
			if (!nPC.active || nPC.type != 491)
			{
				if (!this.TryFindingAnotherPirateShipPiece(ref info))
				{
					return false;
				}
				nPC = Main.npc[info.npcIndexToAimAt];
			}
			int num = 0;
			this._referenceDummy.SetDefaults(492, nPC.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax * 4;
			float num2 = 0f;
			for (int i = 0; i < 4; i++)
			{
				int num3 = (int)nPC.ai[i];
				if (Main.npc.IndexInRange(num3))
				{
					NPC nPC2 = Main.npc[num3];
					if (nPC2.active && nPC2.type == 492)
					{
						num2 += (float)nPC2.life;
					}
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x005DFE10 File Offset: 0x005DE010
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[491];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x005DFE64 File Offset: 0x005DE064
		private bool TryFindingAnotherPirateShipPiece(ref BigProgressBarInfo info)
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

		// Token: 0x0400586E RID: 22638
		private BigProgressBarCache _cache;

		// Token: 0x0400586F RID: 22639
		private NPC _referenceDummy;

		// Token: 0x04005870 RID: 22640
		private HashSet<int> ValidIds = new HashSet<int>
		{
			491
		};
	}
}
