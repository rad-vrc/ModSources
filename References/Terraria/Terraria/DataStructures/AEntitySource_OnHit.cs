using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000435 RID: 1077
	public abstract class AEntitySource_OnHit : IEntitySource
	{
		// Token: 0x06002B94 RID: 11156 RVA: 0x0059E834 File Offset: 0x0059CA34
		public AEntitySource_OnHit(Entity entityStriking, Entity entityStruck)
		{
			this.EntityStriking = entityStriking;
			this.EntityStruck = entityStruck;
		}

		// Token: 0x04004FC2 RID: 20418
		public readonly Entity EntityStriking;

		// Token: 0x04004FC3 RID: 20419
		public readonly Entity EntityStruck;
	}
}
