using System;

namespace Terraria.Physics
{
	// Token: 0x02000115 RID: 277
	public struct BallPassThroughEvent
	{
		// Token: 0x06001980 RID: 6528 RVA: 0x004BFC29 File Offset: 0x004BDE29
		public BallPassThroughEvent(float timeScale, Tile tile, Entity entity, BallPassThroughType type)
		{
			this.Tile = tile;
			this.Entity = entity;
			this.Type = type;
			this.TimeScale = timeScale;
		}

		// Token: 0x040013C1 RID: 5057
		public readonly Tile Tile;

		// Token: 0x040013C2 RID: 5058
		public readonly Entity Entity;

		// Token: 0x040013C3 RID: 5059
		public readonly BallPassThroughType Type;

		// Token: 0x040013C4 RID: 5060
		public readonly float TimeScale;
	}
}
