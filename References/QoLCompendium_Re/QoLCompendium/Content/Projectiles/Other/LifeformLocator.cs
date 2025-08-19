using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Other
{
	// Token: 0x02000024 RID: 36
	internal class LifeformLocator : ModProjectile
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004554 File Offset: 0x00002754
		public override void SetDefaults()
		{
			base.Projectile.width = 22;
			base.Projectile.height = 28;
			base.Projectile.light = 0.75f;
			base.Projectile.friendly = false;
			base.Projectile.hostile = false;
			base.Projectile.timeLeft = 61;
			base.Projectile.penetrate = -1;
			base.Projectile.tileCollide = false;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000045C8 File Offset: 0x000027C8
		public override void AI()
		{
			int NPCnumber = (int)base.Projectile.ai[0];
			Vector2 npcpos;
			npcpos..ctor((float)((int)Main.npc[NPCnumber].Center.X), (float)((int)Main.npc[NPCnumber].Center.Y));
			Player player = Main.player[base.Projectile.owner];
			if (base.Projectile.owner == Main.myPlayer)
			{
				Vector2 diff = npcpos - player.Center;
				diff.Normalize();
				base.Projectile.velocity = diff;
				base.Projectile.direction = ((npcpos.X > player.Center.X) ? 1 : -1);
				base.Projectile.netUpdate = true;
			}
			base.Projectile.position = player.position + base.Projectile.velocity * 45f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		}
	}
}
