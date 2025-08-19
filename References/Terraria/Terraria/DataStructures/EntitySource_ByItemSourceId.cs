using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200042A RID: 1066
	public class EntitySource_ByItemSourceId : IEntitySource
	{
		// Token: 0x06002B89 RID: 11145 RVA: 0x0059E7D3 File Offset: 0x0059C9D3
		public EntitySource_ByItemSourceId(Entity entity, int itemSourceId)
		{
			this.Entity = entity;
			this.SourceId = itemSourceId;
		}

		// Token: 0x04004FBB RID: 20411
		public readonly Entity Entity;

		// Token: 0x04004FBC RID: 20412
		public readonly int SourceId;
	}
}
