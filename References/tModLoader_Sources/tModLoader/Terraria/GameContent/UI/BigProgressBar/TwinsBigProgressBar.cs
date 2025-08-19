using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000558 RID: 1368
	public class TwinsBigProgressBar : IBigProgressBar
	{
		// Token: 0x06004060 RID: 16480 RVA: 0x005DFEF8 File Offset: 0x005DE0F8
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
			int num = (nPC.type == 126) ? 125 : 126;
			int num2 = nPC.lifeMax;
			int num3 = nPC.life;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC2 = Main.npc[i];
				if (nPC2.active && nPC2.type == num)
				{
					num2 += nPC2.lifeMax;
					num3 += nPC2.life;
					break;
				}
			}
			this._cache.SetLife((float)num3, (float)num2);
			this._headIndex = nPC.GetBossHeadTextureIndex();
			return true;
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x005DFFB8 File Offset: 0x005DE1B8
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x04005871 RID: 22641
		private BigProgressBarCache _cache;

		// Token: 0x04005872 RID: 22642
		private int _headIndex;
	}
}
