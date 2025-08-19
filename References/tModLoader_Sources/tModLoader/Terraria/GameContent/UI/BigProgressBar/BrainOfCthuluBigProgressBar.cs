using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200054A RID: 1354
	public class BrainOfCthuluBigProgressBar : IBigProgressBar
	{
		// Token: 0x0600402B RID: 16427 RVA: 0x005DF041 File Offset: 0x005DD241
		public BrainOfCthuluBigProgressBar()
		{
			this._creeperForReference = new NPC();
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x005DF054 File Offset: 0x005DD254
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
			int brainOfCthuluCreepersCount = NPC.GetBrainOfCthuluCreepersCount();
			this._creeperForReference.SetDefaults(267, nPC.GetMatchingSpawnParams());
			int num = this._creeperForReference.lifeMax * brainOfCthuluCreepersCount;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC nPC2 = Main.npc[i];
				if (nPC2.active && nPC2.type == this._creeperForReference.type)
				{
					num2 += (float)nPC2.life;
				}
			}
			float current = (float)nPC.life + num2;
			int num3 = nPC.lifeMax + num;
			this._cache.SetLife(current, (float)num3);
			return true;
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x005DF12C File Offset: 0x005DD32C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[266];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x0400585A RID: 22618
		private BigProgressBarCache _cache;

		// Token: 0x0400585B RID: 22619
		private NPC _creeperForReference;
	}
}
