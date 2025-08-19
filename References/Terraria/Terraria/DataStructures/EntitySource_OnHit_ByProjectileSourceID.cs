using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000436 RID: 1078
	public class EntitySource_OnHit_ByProjectileSourceID : AEntitySource_OnHit
	{
		// Token: 0x06002B95 RID: 11157 RVA: 0x0059E84A File Offset: 0x0059CA4A
		public EntitySource_OnHit_ByProjectileSourceID(Entity entityStriking, Entity entityStruck, int projectileSourceId) : base(entityStriking, entityStruck)
		{
			this.SourceId = projectileSourceId;
		}

		// Token: 0x04004FC4 RID: 20420
		public readonly int SourceId;
	}
}
