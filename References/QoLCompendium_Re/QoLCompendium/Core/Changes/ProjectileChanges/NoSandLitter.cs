using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x02000226 RID: 550
	public class NoSandLitter : GlobalProjectile
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x00067BE8 File Offset: 0x00065DE8
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == 31;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00067BF4 File Offset: 0x00065DF4
		public override bool PreKill(Projectile projectile, int timeLeft)
		{
			if (!projectile.friendly && QoLCompendium.mainConfig.NoLittering)
			{
				projectile.noDropItem = true;
			}
			return base.PreKill(projectile, timeLeft);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00067C1C File Offset: 0x00065E1C
		public override void OnKill(Projectile projectile, int timeLeft)
		{
			if (projectile.friendly)
			{
				return;
			}
			if (!QoLCompendium.mainConfig.NoLittering)
			{
				return;
			}
			if (projectile.owner != Main.myPlayer)
			{
				return;
			}
			int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(null), projectile.Hitbox, 169, 1, false, 0, false, false);
			Main.item[itemIndex].noGrabDelay = 0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(21, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
			}
		}
	}
}
