using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200054C RID: 1356
	public class DeerclopsBigProgressBar : IBigProgressBar
	{
		// Token: 0x06004031 RID: 16433 RVA: 0x005DF240 File Offset: 0x005DD440
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
			if (!NPC.IsDeerclopsHostile())
			{
				return false;
			}
			this._cache.SetLife((float)nPC.life, (float)nPC.lifeMax);
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x005DF2B4 File Offset: 0x005DD4B4
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x0400585E RID: 22622
		private BigProgressBarCache _cache;

		// Token: 0x0400585F RID: 22623
		private int _headIndex;
	}
}
