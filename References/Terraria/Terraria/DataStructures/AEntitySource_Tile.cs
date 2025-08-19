using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000423 RID: 1059
	public abstract class AEntitySource_Tile : IEntitySource
	{
		// Token: 0x06002B82 RID: 11138 RVA: 0x0059E783 File Offset: 0x0059C983
		public AEntitySource_Tile(int tileCoordsX, int tileCoordsY)
		{
			this.TileCoords = new Point(tileCoordsX, tileCoordsY);
		}

		// Token: 0x04004FB7 RID: 20407
		public readonly Point TileCoords;
	}
}
