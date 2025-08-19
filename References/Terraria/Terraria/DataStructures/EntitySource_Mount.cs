using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000422 RID: 1058
	public class EntitySource_Mount : IEntitySource
	{
		// Token: 0x06002B81 RID: 11137 RVA: 0x0059E76D File Offset: 0x0059C96D
		public EntitySource_Mount(Entity entity, int mountId)
		{
			this.Entity = entity;
			this.MountId = mountId;
		}

		// Token: 0x04004FB5 RID: 20405
		public readonly Entity Entity;

		// Token: 0x04004FB6 RID: 20406
		public readonly int MountId;
	}
}
