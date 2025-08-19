using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003A9 RID: 937
	public class BrainOfCthuluBigProgressBar : IBigProgressBar
	{
		// Token: 0x060029D9 RID: 10713 RVA: 0x00595CA7 File Offset: 0x00593EA7
		public BrainOfCthuluBigProgressBar()
		{
			this._creeperForReference = new NPC();
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x00595CBC File Offset: 0x00593EBC
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
			int brainOfCthuluCreepersCount = NPC.GetBrainOfCthuluCreepersCount();
			this._creeperForReference.SetDefaults(267, npc.GetMatchingSpawnParams());
			int num = this._creeperForReference.lifeMax * brainOfCthuluCreepersCount;
			float num2 = 0f;
			for (int i = 0; i < 200; i++)
			{
				NPC npc2 = Main.npc[i];
				if (npc2.active && npc2.type == this._creeperForReference.type)
				{
					num2 += (float)npc2.life;
				}
			}
			float current = (float)npc.life + num2;
			int num3 = npc.lifeMax + num;
			this._cache.SetLife(current, (float)num3);
			return true;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x00595D94 File Offset: 0x00593F94
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			int num = NPCID.Sets.BossHeadTextures[266];
			Texture2D value = TextureAssets.NpcHeadBoss[num].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame);
		}

		// Token: 0x04004CC5 RID: 19653
		private BigProgressBarCache _cache;

		// Token: 0x04004CC6 RID: 19654
		private NPC _creeperForReference;
	}
}
