using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000430 RID: 1072
	public class EntitySource_BossSpawn : IEntitySource
	{
		// Token: 0x06002B8F RID: 11151 RVA: 0x0059E7E9 File Offset: 0x0059C9E9
		public EntitySource_BossSpawn(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FBD RID: 20413
		public readonly Entity Entity;
	}
}
