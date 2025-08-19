using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000429 RID: 1065
	public class EntitySource_ByProjectileSourceId : IEntitySource
	{
		// Token: 0x06002B88 RID: 11144 RVA: 0x0059E7C4 File Offset: 0x0059C9C4
		public EntitySource_ByProjectileSourceId(int projectileSourceId)
		{
			this.SourceId = projectileSourceId;
		}

		// Token: 0x04004FBA RID: 20410
		public readonly int SourceId;
	}
}
