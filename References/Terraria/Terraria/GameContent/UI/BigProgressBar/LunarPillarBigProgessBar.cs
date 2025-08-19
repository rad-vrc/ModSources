using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x020003AF RID: 943
	public abstract class LunarPillarBigProgessBar : IBigProgressBar
	{
		// Token: 0x060029ED RID: 10733 RVA: 0x00596394 File Offset: 0x00594594
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
			if (!this.IsPlayerInCombatArea())
			{
				return false;
			}
			if (npc.ai[2] == 1f)
			{
				return false;
			}
			Utils.Clamp<float>((float)npc.life / (float)npc.lifeMax, 0f, 1f);
			float num = (float)((int)MathHelper.Clamp(this.GetCurrentShieldValue(), 0f, this.GetMaxShieldValue())) / this.GetMaxShieldValue();
			float num2 = 600f * Main.GameModeInfo.EnemyMaxLifeMultiplier * this.GetMaxShieldValue() / (float)npc.lifeMax;
			this._cache.SetLife((float)npc.life, (float)npc.lifeMax);
			this._cache.SetShield(this.GetCurrentShieldValue(), this.GetMaxShieldValue());
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00596490 File Offset: 0x00594690
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame, this._cache.ShieldCurrent, this._cache.ShieldMax);
		}

		// Token: 0x060029EF RID: 10735
		internal abstract float GetCurrentShieldValue();

		// Token: 0x060029F0 RID: 10736
		internal abstract float GetMaxShieldValue();

		// Token: 0x060029F1 RID: 10737
		internal abstract bool IsPlayerInCombatArea();

		// Token: 0x04004CD3 RID: 19667
		private BigProgressBarCache _cache;

		// Token: 0x04004CD4 RID: 19668
		private int _headIndex;
	}
}
