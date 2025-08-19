using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.MobileStorages
{
	// Token: 0x02000029 RID: 41
	public class MobileStorageFollowing : GlobalProjectile
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002430 File Offset: 0x00000630
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004CF5 File Offset: 0x00002EF5
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == 525 || entity.type == 734 || entity.type == ModContent.ProjectileType<EtherianConstructProjectile>() || entity.type == ModContent.ProjectileType<FlyingSafeProjectile>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004D30 File Offset: 0x00002F30
		public override bool PreAI(Projectile projectile)
		{
			if (QoLCompendium.mainConfig.MobileStoragesFollowThePlayer)
			{
				Player player = Main.player[projectile.owner];
				float distance = Vector2.Distance(projectile.Center, player.Center);
				if (distance > 3000f)
				{
					projectile.Center = player.Top;
				}
				else if (projectile.Center != player.Center)
				{
					Vector2 val2 = (player.Center + projectile.DirectionFrom(player.Center) * 3f * 16f - projectile.Center) / ((distance < 48f) ? 30f : 60f);
					projectile.position += val2;
				}
				if (projectile.timeLeft < 2 && projectile.timeLeft > 0)
				{
					projectile.timeLeft = 2;
				}
			}
			return base.PreAI(projectile);
		}
	}
}
