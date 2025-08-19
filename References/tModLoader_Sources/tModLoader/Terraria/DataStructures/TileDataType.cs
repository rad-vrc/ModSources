using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000734 RID: 1844
	[Flags]
	public enum TileDataType
	{
		// Token: 0x04006017 RID: 24599
		Tile = 1,
		// Token: 0x04006018 RID: 24600
		TilePaint = 2,
		// Token: 0x04006019 RID: 24601
		Wall = 4,
		// Token: 0x0400601A RID: 24602
		WallPaint = 8,
		// Token: 0x0400601B RID: 24603
		Liquid = 16,
		// Token: 0x0400601C RID: 24604
		Wiring = 32,
		// Token: 0x0400601D RID: 24605
		Actuator = 64,
		// Token: 0x0400601E RID: 24606
		Slope = 128,
		// Token: 0x0400601F RID: 24607
		All = 255
	}
}
