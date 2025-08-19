using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x02000224 RID: 548
	public class ExtraLures : GlobalProjectile
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x000677C0 File Offset: 0x000659C0
		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if (projectile.bobber && projectile.owner == Main.myPlayer && QoLCompendium.mainConfig.ExtraLures > 1 && source is EntitySource_ItemUse)
			{
				int split = QoLCompendium.mainConfig.ExtraLures + 1;
				if (Main.player[projectile.owner].HasBuff(121))
				{
					split++;
				}
				if (split > 1)
				{
					ExtraLures.SplitProj(projectile, split);
				}
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00067828 File Offset: 0x00065A28
		public static void SplitProj(Projectile projectile, int number)
		{
			double spread = 0.2 / (double)number;
			for (int i = 0; i < number / 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int factor = (j == 0) ? 1 : -1;
					Projectile split = ExtraLures.NewProjectileDirectSafe(projectile.GetSource_FromThis(null), projectile.Center, projectile.velocity.RotatedBy((double)factor * spread * (double)(i + 1), default(Vector2)), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
					if (split != null)
					{
						split.friendly = true;
					}
				}
			}
			if (number % 2 == 0)
			{
				projectile.active = false;
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x000678D8 File Offset: 0x00065AD8
		public static Projectile NewProjectileDirectSafe(IEntitySource source, Vector2 pos, Vector2 vel, int type, int damage, float knockback, int owner = 255, float ai0 = 0f, float ai1 = 0f)
		{
			int p = Projectile.NewProjectile(source, pos, vel, type, damage, knockback, owner, ai0, ai1, 0f);
			if (p >= Main.maxProjectiles)
			{
				return null;
			}
			return Main.projectile[p];
		}
	}
}
