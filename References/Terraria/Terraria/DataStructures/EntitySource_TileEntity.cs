using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200043A RID: 1082
	public class EntitySource_TileEntity : IEntitySource
	{
		// Token: 0x06002B99 RID: 11161 RVA: 0x0059E86C File Offset: 0x0059CA6C
		public EntitySource_TileEntity(TileEntity tileEntity)
		{
			this.TileEntity = tileEntity;
		}

		// Token: 0x04004FC6 RID: 20422
		public TileEntity TileEntity;
	}
}
