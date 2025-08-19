using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000550 RID: 1360
	public abstract class LunarPillarBigProgessBar : IBigProgressBar
	{
		// Token: 0x0600403E RID: 16446 RVA: 0x005DF64C File Offset: 0x005DD84C
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
			if (!this.IsPlayerInCombatArea())
			{
				return false;
			}
			if (nPC.ai[2] == 1f)
			{
				return false;
			}
			Utils.Clamp<float>((float)nPC.life / (float)nPC.lifeMax, 0f, 1f);
			float num = (float)((int)MathHelper.Clamp(this.GetCurrentShieldValue(), 0f, this.GetMaxShieldValue())) / this.GetMaxShieldValue();
			float num2 = 600f * Main.GameModeInfo.EnemyMaxLifeMultiplier * this.GetMaxShieldValue() / (float)nPC.lifeMax;
			this._cache.SetLife((float)nPC.life, (float)nPC.lifeMax);
			this._cache.SetShield(this.GetCurrentShieldValue(), this.GetMaxShieldValue());
			this._headIndex = bossHeadTextureIndex;
			return true;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x005DF74C File Offset: 0x005DD94C
		public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
		{
			Texture2D value = TextureAssets.NpcHeadBoss[this._headIndex].Value;
			Rectangle barIconFrame = value.Frame(1, 1, 0, 0, 0, 0);
			BigProgressBarHelper.DrawFancyBar(spriteBatch, this._cache.LifeCurrent, this._cache.LifeMax, value, barIconFrame, this._cache.ShieldCurrent, this._cache.ShieldMax);
		}

		// Token: 0x06004040 RID: 16448
		internal abstract float GetCurrentShieldValue();

		// Token: 0x06004041 RID: 16449
		internal abstract float GetMaxShieldValue();

		// Token: 0x06004042 RID: 16450
		internal abstract bool IsPlayerInCombatArea();

		// Token: 0x04005865 RID: 22629
		private BigProgressBarCache _cache;

		// Token: 0x04005866 RID: 22630
		private int _headIndex;
	}
}
