using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200054E RID: 1358
	public class GolemHeadProgressBar : IBigProgressBar
	{
		// Token: 0x06004038 RID: 16440 RVA: 0x005DF48B File Offset: 0x005DD68B
		public GolemHeadProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x005DF4C4 File Offset: 0x005DD6C4
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[info.npcIndexToAimAt];
			if (!nPC.active && !this.TryFindingAnotherGolemPiece(ref info))
			{
				return false;
			}
			int num = 0;
			this._referenceDummy.SetDefaults(245, nPC.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(246, nPC.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC2 = Main.npc[i];
				if (nPC2.active && this.ValidIds.Contains(nPC2.type))
				{
					num2 += (float)nPC2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x005DF5AC File Offset: 0x005DD7AC
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[246];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x005DF600 File Offset: 0x005DD800
		private bool TryFindingAnotherGolemPiece(ref BigProgressBarInfo info)
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

		// Token: 0x04005862 RID: 22626
		private BigProgressBarCache _cache;

		// Token: 0x04005863 RID: 22627
		private NPC _referenceDummy;

		// Token: 0x04005864 RID: 22628
		private HashSet<int> ValidIds = new HashSet<int>
		{
			246,
			245
		};
	}
}
