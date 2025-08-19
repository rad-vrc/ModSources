using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ProjectileChanges
{
	// Token: 0x0200022A RID: 554
	public class NoFallingBlockDamage : GlobalProjectile
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x00067F8C File Offset: 0x0006618C
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return Common.FallingBlocks.Contains(entity.type);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00067F9E File Offset: 0x0006619E
		public override void SetDefaults(Projectile entity)
		{
			if (QoLCompendium.mainConfig.NoFallingSandDamage)
			{
				entity.friendly = true;
				entity.hostile = false;
			}
		}
	}
}
