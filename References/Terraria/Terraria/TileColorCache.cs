using System;

namespace Terraria
{
	// Token: 0x02000046 RID: 70
	public struct TileColorCache
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0033E19B File Offset: 0x0033C39B
		public void ApplyToBlock(Tile tile)
		{
			tile.color(this.Color);
			tile.fullbrightBlock(this.FullBright);
			tile.invisibleBlock(this.Invisible);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0033E1C1 File Offset: 0x0033C3C1
		public void ApplyToWall(Tile tile)
		{
			tile.wallColor(this.Color);
			tile.fullbrightWall(this.FullBright);
			tile.invisibleWall(this.Invisible);
		}

		// Token: 0x0400091F RID: 2335
		public byte Color;

		// Token: 0x04000920 RID: 2336
		public bool FullBright;

		// Token: 0x04000921 RID: 2337
		public bool Invisible;
	}
}
