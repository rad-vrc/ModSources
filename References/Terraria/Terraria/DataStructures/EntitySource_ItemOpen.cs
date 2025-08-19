using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000420 RID: 1056
	public class EntitySource_ItemOpen : IEntitySource
	{
		// Token: 0x06002B7F RID: 11135 RVA: 0x0059E746 File Offset: 0x0059C946
		public EntitySource_ItemOpen(Entity entity, int itemType)
		{
			this.Entity = entity;
			this.ItemType = itemType;
		}

		// Token: 0x04004FB2 RID: 20402
		public readonly Entity Entity;

		// Token: 0x04004FB3 RID: 20403
		public readonly int ItemType;
	}
}
