using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003AA RID: 938
	public class GolemHeadProgressBar : IBigProgressBar
	{
		// Token: 0x060029DC RID: 10716 RVA: 0x00595DE5 File Offset: 0x00593FE5
		public GolemHeadProgressBar()
		{
			this._referenceDummy = new NPC();
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x00595E1C File Offset: 0x0059401C
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active && !this.TryFindingAnotherGolemPiece(ref info))
			{
				return false;
			}
			int num = 0;
			this._referenceDummy.SetDefaults(245, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax;
			this._referenceDummy.SetDefaults(246, npc.GetMatchingSpawnParams());
			num += this._referenceDummy.lifeMax;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && this.ValidIds.Contains(npc2.type))
				{
					num2 += (float)npc2.life;
				}
			}
			this._cache.SetLife(num2, (float)num);
			return true;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x00595F04 File Offset: 0x00594104
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[246];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00595F58 File Offset: 0x00594158
		private bool TryFindingAnotherGolemPiece(ref BigProgressBarInfo info)
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

		// Token: 0x04004CC7 RID: 19655
		private BigProgressBarCache _cache;

		// Token: 0x04004CC8 RID: 19656
		private NPC _referenceDummy;

		// Token: 0x04004CC9 RID: 19657
		private HashSet<int> ValidIds = new HashSet<int>
		{
			246,
			245
		};
	}
}
