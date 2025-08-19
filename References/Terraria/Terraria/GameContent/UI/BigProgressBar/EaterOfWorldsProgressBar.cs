using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003A8 RID: 936
	public class EaterOfWorldsProgressBar : IBigProgressBar
	{
		// Token: 0x060029D5 RID: 10709 RVA: 0x00595B22 File Offset: 0x00593D22
		public EaterOfWorldsProgressBar()
		{
			this._segmentForReference = new NPC();
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00595B38 File Offset: 0x00593D38
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active && !this.TryFindingAnotherEOWPiece(ref info))
			{
				return false;
			}
			int num = 2;
			int num2 = NPC.GetEaterOfWorldsSegmentsCount() + num;
			this._segmentForReference.SetDefaults(14, npc.GetMatchingSpawnParams());
			int num3 = 0;
			int num4 = this._segmentForReference.lifeMax * num2;
			for (int i = 0; i < 200; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && npc2.type >= 13 && npc2.type <= 15)
				{
					num3 += npc2.life;
				}
			}
			int num5 = num3;
			int num6 = num4;
			this._cache.SetLife((float)num5, (float)num6);
			return true;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00595C0C File Offset: 0x00593E0C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[13];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00595C5C File Offset: 0x00593E5C
		private bool TryFindingAnotherEOWPiece(ref BigProgressBarInfo info)
		{
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.type >= 13 && npc.type <= 15)
				{
					info.npcIndexToAimAt = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004CC3 RID: 19651
		private BigProgressBarCache _cache;

		// Token: 0x04004CC4 RID: 19652
		private NPC _segmentForReference;
	}
}
