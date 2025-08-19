using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200054B RID: 1355
	public class CommonBossBigProgressBar : IBigProgressBar
	{
		// Token: 0x0600402E RID: 16430 RVA: 0x005DF180 File Offset: 0x005DD380
		public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
		{
			if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
			{
				return false;
			}
			NPC nPC = Main.npc[info.npcIndexToAimAt];
			if (!nPC.active)
			{
				return false;
			}
			int bossHeadTextureIndex = nPC.GetBossHeadTextureIndex();
			if (bossHeadTextureIndex == -1)
			{
				return false;
			}
			this._cache.SetLife((float)nPC.life, (float)nPC.lifeMax);
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x005DF1EC File Offset: 0x005DD3EC
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x0400585C RID: 22620
		private BigProgressBarCache _cache;

		// Token: 0x0400585D RID: 22621
		private int _headIndex;
	}
}
