using System;

namespace Terraria.Physics
{
	// Token: 0x020000B4 RID: 180
	public struct BallPassThroughEvent
	{
		// Token: 0x060013F5 RID: 5109 RVA: 0x004A1E3B File Offset: 0x004A003B
		public BallPassThroughEvent(float timeScale, Tile tile, Entity entity, BallPassThroughType type)
		{
			this.Tile = tile;
			this.Entity = entity;
			this.Type = type;
			this.TimeScale = timeScale;
		}

		// Token: 0x040011D0 RID: 4560
		public readonly Tile Tile;

		// Token: 0x040011D1 RID: 4561
		public readonly Entity Entity;

		// Token: 0x040011D2 RID: 4562
		public readonly BallPassThroughType Type;

		// Token: 0x040011D3 RID: 4563
		public readonly float TimeScale;
	}
}
