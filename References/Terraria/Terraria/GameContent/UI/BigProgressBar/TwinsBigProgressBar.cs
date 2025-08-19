using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003AE RID: 942
	public class TwinsBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029EA RID: 10730 RVA: 0x00596288 File Offset: 0x00594488
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
			int num = (npc.type == 126) ? 125 : 126;
			int num2 = npc.lifeMax;
			int num3 = npc.life;
			for (int i = 0; i < 200; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && npc2.type == num)
				{
					num2 += npc2.lifeMax;
					num3 += npc2.life;
					break;
				}
			}
			this._cache.SetLife((float)num3, (float)num2);
			this._headIndex = npc.GetBossHeadTextureIndex();
			return true;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00596348 File Offset: 0x00594548
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x04004CD1 RID: 19665
		private BigProgressBarCache _cache;

		// Token: 0x04004CD2 RID: 19666
		private int _headIndex;
	}
}
