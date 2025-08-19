using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000425 RID: 1061
	public class EntitySource_OverfullChest : AEntitySource_Tile
	{
		// Token: 0x06002B84 RID: 11140 RVA: 0x0059E7A9 File Offset: 0x0059C9A9
		public EntitySource_OverfullChest(int tileCoordsX, int tileCoordsY, Chest chest) : base(tileCoordsX, tileCoordsY)
		{
			this.Chest = chest;
		}

		// Token: 0x04004FB9 RID: 20409
		public readonly Chest Chest;
	}
}
