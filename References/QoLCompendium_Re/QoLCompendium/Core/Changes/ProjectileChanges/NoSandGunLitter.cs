using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x02000228 RID: 552
	public class NoSandGunLitter : GlobalProjectile
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x00067D37 File Offset: 0x00065F37
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return NoSandGunLitter._sandGunProjectileToItem.ContainsKey(entity.type);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00067D4C File Offset: 0x00065F4C
		public override bool PreKill(Projectile projectile, int timeLeft)
		{
			if (!QoLCompendium.mainConfig.NoLittering)
			{
				return base.PreKill(projectile, timeLeft);
			}
			projectile.noDropItem = true;
			int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(null), projectile.Hitbox, NoSandGunLitter._sandGunProjectileToItem[projectile.type], 1, false, 0, false, false);
			Main.item[itemIndex].noGrabDelay = 0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(21, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
			}
			return base.PreKill(projectile, timeLeft);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00067DD8 File Offset: 0x00065FD8
		// Note: this type is marked as 'beforefieldinit'.
		static NoSandGunLitter()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			dictionary.Add(42, 169);
			dictionary.Add(65, 370);
			dictionary.Add(68, 408);
			dictionary.Add(354, 1246);
			NoSandGunLitter._sandGunProjectileToItem = dictionary;
		}

		// Token: 0x0400058B RID: 1419
		private static readonly Dictionary<int, int> _sandGunProjectileToItem;
	}
}
