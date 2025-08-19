using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000431 RID: 1073
	public class EntitySource_DropAsItem : IEntitySource
	{
		// Token: 0x06002B90 RID: 11152 RVA: 0x0059E7F8 File Offset: 0x0059C9F8
		public EntitySource_DropAsItem(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FBE RID: 20414
		public readonly Entity Entity;
	}
}
