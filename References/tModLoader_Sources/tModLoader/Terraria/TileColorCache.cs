using System;

namespace Terraria
{
	// Token: 0x0200005A RID: 90
	public struct TileColorCache
	{
		// Token: 0x06000FAD RID: 4013 RVA: 0x003FD052 File Offset: 0x003FB252
		public void ApplyToBlock(Tile tile)
		{
			tile.color(this.Color);
			tile.fullbrightBlock(this.FullBright);
			tile.invisibleBlock(this.Invisible);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x003FD07B File Offset: 0x003FB27B
		public void ApplyToWall(Tile tile)
		{
			tile.wallColor(this.Color);
			tile.fullbrightWall(this.FullBright);
			tile.invisibleWall(this.Invisible);
		}

		// Token: 0x04000ECA RID: 3786
		public byte Color;

		// Token: 0x04000ECB RID: 3787
		public bool FullBright;

		// Token: 0x04000ECC RID: 3788
		public bool Invisible;
	}
}
