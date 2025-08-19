using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200054D RID: 1357
	public class EaterOfWorldsProgressBar : IBigProgressBar
	{
		// Token: 0x06004034 RID: 16436 RVA: 0x005DF306 File Offset: 0x005DD506
		public EaterOfWorldsProgressBar()
		{
			this._segmentForReference = new NPC();
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x005DF31C File Offset: 0x005DD51C
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[info.npcIndexToAimAt];
			if (!nPC.active && !this.TryFindingAnotherEOWPiece(ref info))
			{
				return false;
			}
			int num = 2;
			int num2 = NPC.GetEaterOfWorldsSegmentsCount() + num;
			this._segmentForReference.SetDefaults(14, nPC.GetMatchingSpawnParams());
			int num3 = 0;
			int num4 = this._segmentForReference.lifeMax * num2;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC2 = Main.npc[i];
				if (nPC2.active && nPC2.type >= 13 && nPC2.type <= 15)
				{
					num3 += nPC2.life;
				}
			}
			int num5 = num3;
			int num6 = num4;
			this._cache.SetLife((float)num5, (float)num6);
			return true;
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x005DF3F0 File Offset: 0x005DD5F0
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[13];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x005DF440 File Offset: 0x005DD640
		private bool TryFindingAnotherEOWPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC nPC = Main.npc[i];
				if (nPC.active && nPC.type >= 13 && nPC.type <= 15)
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04005860 RID: 22624
		private BigProgressBarCache _cache;

		// Token: 0x04005861 RID: 22625
		private NPC _segmentForReference;
	}
}
