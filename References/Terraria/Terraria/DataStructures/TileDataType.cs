using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200043F RID: 1087
	[Flags]
	public enum TileDataType
	{
		// Token: 0x04004FCC RID: 20428
		Tile = 1,
		// Token: 0x04004FCD RID: 20429
		TilePaint = 2,
		// Token: 0x04004FCE RID: 20430
		Wall = 4,
		// Token: 0x04004FCF RID: 20431
		WallPaint = 8,
		// Token: 0x04004FD0 RID: 20432
		Liquid = 16,
		// Token: 0x04004FD1 RID: 20433
		Wiring = 32,
		// Token: 0x04004FD2 RID: 20434
		Actuator = 64,
		// Token: 0x04004FD3 RID: 20435
		Slope = 128,
		// Token: 0x04004FD4 RID: 20436
		All = 255
	}
}
