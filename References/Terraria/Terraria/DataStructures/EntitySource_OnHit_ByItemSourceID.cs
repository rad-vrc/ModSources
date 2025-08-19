using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000437 RID: 1079
	public class EntitySource_OnHit_ByItemSourceID : AEntitySource_OnHit
	{
		// Token: 0x06002B96 RID: 11158 RVA: 0x0059E85B File Offset: 0x0059CA5B
		public EntitySource_OnHit_ByItemSourceID(Entity entityStriking, Entity entityStruck, int itemSourceId) : base(entityStriking, entityStruck)
		{
			this.SourceId = itemSourceId;
		}

		// Token: 0x04004FC5 RID: 20421
		public readonly int SourceId;
	}
}
