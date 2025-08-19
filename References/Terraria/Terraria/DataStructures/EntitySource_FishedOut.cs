using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000432 RID: 1074
	public class EntitySource_FishedOut : IEntitySource
	{
		// Token: 0x06002B91 RID: 11153 RVA: 0x0059E807 File Offset: 0x0059CA07
		public EntitySource_FishedOut(Entity entity)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FBF RID: 20415
		public readonly Entity Entity;
	}
}
