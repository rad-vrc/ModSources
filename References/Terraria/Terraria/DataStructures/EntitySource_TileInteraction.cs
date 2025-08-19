using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000424 RID: 1060
	public class EntitySource_TileInteraction : AEntitySource_Tile
	{
		// Token: 0x06002B83 RID: 11139 RVA: 0x0059E798 File Offset: 0x0059C998
		public EntitySource_TileInteraction(Entity entity, int tileCoordsX, int tileCoordsY) : base(tileCoordsX, tileCoordsY)
		{
			this.Entity = entity;
		}

		// Token: 0x04004FB8 RID: 20408
		public readonly Entity Entity;
	}
}
