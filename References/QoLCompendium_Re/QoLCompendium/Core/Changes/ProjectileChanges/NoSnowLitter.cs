using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x02000227 RID: 551
	public class NoSnowLitter : GlobalProjectile
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x00067C9C File Offset: 0x00065E9C
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == 109;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00067CA8 File Offset: 0x00065EA8
		public override void SetDefaults(Projectile projectile)
		{
			if (!QoLCompendium.mainConfig.NoLittering)
			{
				return;
			}
			projectile.noDropItem = true;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00067CC0 File Offset: 0x00065EC0
		public override void OnKill(Projectile projectile, int timeLeft)
		{
			if (!QoLCompendium.mainConfig.NoLittering)
			{
				return;
			}
			if (projectile.owner != Main.myPlayer)
			{
				return;
			}
			int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(null), projectile.Hitbox, 593, 1, false, 0, false, false);
			Main.item[itemIndex].noGrabDelay = 0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(21, -1, -1, null, itemIndex, 1f, 0f, 0f, 0, 0, 0);
			}
		}
	}
}
