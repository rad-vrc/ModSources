using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200041D RID: 1053
	public class EntitySource_Parent : IEntitySource
	{
		// Token: 0x06002B7C RID: 11132 RVA: 0x0059E704 File Offset: 0x0059C904
		public EntitySource_Parent(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FAC RID: 20396
		public readonly Entity Entity;
	}
}
