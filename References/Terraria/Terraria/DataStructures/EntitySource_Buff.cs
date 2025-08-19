using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200041E RID: 1054
	public class EntitySource_Buff : IEntitySource
	{
		// Token: 0x06002B7D RID: 11133 RVA: 0x0059E713 File Offset: 0x0059C913
		public EntitySource_Buff(Entity entity, int buffId, int buffIndex)
		{
			this.Entity = entity;
			this.BuffId = buffId;
			this.BuffIndex = buffIndex;
		}

		// Token: 0x04004FAD RID: 20397
		public readonly Entity Entity;

		// Token: 0x04004FAE RID: 20398
		public readonly int BuffId;

		// Token: 0x04004FAF RID: 20399
		public readonly int BuffIndex;
	}
}
