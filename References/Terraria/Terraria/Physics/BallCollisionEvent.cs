using System;
using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
	// Token: 0x020000B1 RID: 177
	public struct BallCollisionEvent
	{
		// Token: 0x060013F2 RID: 5106 RVA: 0x004A1E14 File Offset: 0x004A0014
		public BallCollisionEvent(float timeScale, Vector2 normal, Vector2 impactPoint, Tile tile, Entity entity)
		{
			this.Normal = normal;
			this.ImpactPoint = impactPoint;
			this.Tile = tile;
			this.Entity = entity;
			this.TimeScale = timeScale;
		}

		// Token: 0x040011C5 RID: 4549
		public readonly Vector2 Normal;

		// Token: 0x040011C6 RID: 4550
		public readonly Vector2 ImpactPoint;

		// Token: 0x040011C7 RID: 4551
		public readonly Tile Tile;

		// Token: 0x040011C8 RID: 4552
		public readonly Entity Entity;

		// Token: 0x040011C9 RID: 4553
		public readonly float TimeScale;
	}
}
