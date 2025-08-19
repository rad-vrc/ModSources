using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000433 RID: 1075
	public class EntitySource_Gift : IEntitySource
	{
		// Token: 0x06002B92 RID: 11154 RVA: 0x0059E816 File Offset: 0x0059CA16
		public EntitySource_Gift(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FC0 RID: 20416
		public readonly Entity Entity;
	}
}
