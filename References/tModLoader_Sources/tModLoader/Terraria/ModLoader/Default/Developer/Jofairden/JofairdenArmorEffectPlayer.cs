using System;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000343 RID: 835
	internal sealed class JofairdenArmorEffectPlayer : ModPlayer
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x005336C9 File Offset: 0x005318C9
		public bool HasAura
		{
			get
			{
				return this._auraTime > 0;
			}
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x005336D4 File Offset: 0x005318D4
		public override void ResetEffects()
		{
			this.HasSetBonus = false;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x005336DD File Offset: 0x005318DD
		public override void UpdateDead()
		{
			this.HasSetBonus = false;
			this._auraTime = 0;
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x005336F0 File Offset: 0x005318F0
		public override void PostUpdate()
		{
			if (!this.HasAura)
			{
				if (this.ShaderStrength > 0f)
				{
					this.ShaderStrength -= 0.02f;
				}
			}
			else if (this.ShaderStrength <= 1f)
			{
				this.ShaderStrength += 0.02f;
			}
			else
			{
				this._auraTime--;
			}
			if (!this.HasSetBonus)
			{
				if (this.LayerStrength > 0f)
				{
					this.LayerStrength -= 0.02f;
				}
			}
			else if (this.LayerStrength <= 1f)
			{
				this.LayerStrength += 0.02f;
			}
			if (this.ShaderStrength > 0f)
			{
				Lighting.AddLight(base.Player.Center, Main.DiscoColor.ToVector3() * this.LayerStrength * ((float)Main.time % 2f) * (float)Math.Abs(Math.Log10((double)(Main.essScale * 0.75f))));
			}
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x005337FF File Offset: 0x005319FF
		public override void PostHurt(Player.HurtInfo info)
		{
			if ((float)info.Damage >= 0.1f * (float)base.Player.statLifeMax2)
			{
				this._auraTime = 300 + info.Damage;
			}
		}

		// Token: 0x04001C8A RID: 7306
		public bool HasSetBonus;

		// Token: 0x04001C8B RID: 7307
		public float LayerStrength;

		// Token: 0x04001C8C RID: 7308
		public float ShaderStrength;

		// Token: 0x04001C8D RID: 7309
		private int _auraTime;
	}
}
