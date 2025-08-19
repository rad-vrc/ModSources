using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003A7 RID: 935
	public class DeerclopsBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029D2 RID: 10706 RVA: 0x00595A64 File Offset: 0x00593C64
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC npc = Main.npc[info.npcIndexToAimAt];
			if (!npc.active)
			{
				return false;
			}
			int bossHeadTextureIndex = npc.GetBossHeadTextureIndex();
			if (bossHeadTextureIndex == -1)
			{
				return false;
			}
			if (!NPC.IsDeerclopsHostile())
			{
				return false;
			}
			this._cache.SetLife((float)npc.life, (float)npc.lifeMax);
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00595AD8 File Offset: 0x00593CD8
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x04004CC1 RID: 19649
		private BigProgressBarCache _cache;

		// Token: 0x04004CC2 RID: 19650
		private int _headIndex;
	}
}
