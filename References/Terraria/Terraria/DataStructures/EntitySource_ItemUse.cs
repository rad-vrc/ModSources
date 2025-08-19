using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200041F RID: 1055
	public class EntitySource_ItemUse : IEntitySource
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x0059E730 File Offset: 0x0059C930
		public EntitySource_ItemUse(Entity entity, Item item)
		{
			this.Entity = entity;
			this.Item = item;
		}

		// Token: 0x04004FB0 RID: 20400
		public readonly Entity Entity;

		// Token: 0x04004FB1 RID: 20401
		public readonly Item Item;
	}
}
