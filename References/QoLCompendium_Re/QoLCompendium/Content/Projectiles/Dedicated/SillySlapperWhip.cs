using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Dedicated
{
	// Token: 0x0200003B RID: 59
	public class SillySlapperWhip : WhipProjectile
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00007DCB File Offset: 0x00005FCB
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.IsAWhip[base.Type] = true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007DDC File Offset: 0x00005FDC
		public override void SetWhipStats()
		{
			base.Projectile.width = 26;
			base.Projectile.height = 36;
			base.Projectile.WhipSettings.RangeMultiplier = 1f;
			base.Projectile.WhipSettings.Segments = 30;
			base.Projectile.DamageType = DamageClass.Generic;
			base.Projectile.damage = 100;
			this.fishingLineColor = Color.Green;
			this.dustAmount = 0;
			this.swingDust = new int?(0);
			this.tagDuration = 0;
			this.whipCrackSound = new SoundStyle?(new SoundStyle("QoLCompendium/Assets/Sounds/SillySlapperSFX", SoundType.Sound));
			this.multihitModifier = 1f;
		}
	}
}
