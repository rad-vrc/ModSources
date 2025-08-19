using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000434 RID: 1076
	public class EntitySource_Loot : IEntitySource
	{
		// Token: 0x06002B93 RID: 11155 RVA: 0x0059E825 File Offset: 0x0059CA25
		public EntitySource_Loot(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FC1 RID: 20417
		public readonly Entity Entity;
	}
}
