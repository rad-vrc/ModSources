using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003A6 RID: 934
	public class CommonBossBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029CF RID: 10703 RVA: 0x005959AC File Offset: 0x00593BAC
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
			this._cache.SetLife((float)npc.life, (float)npc.lifeMax);
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00595A18 File Offset: 0x00593C18
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x04004CBF RID: 19647
		private BigProgressBarCache _cache;

		// Token: 0x04004CC0 RID: 19648
		private int _headIndex;
	}
}
