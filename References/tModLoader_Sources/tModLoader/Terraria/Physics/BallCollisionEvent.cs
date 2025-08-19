using System;
using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
	// Token: 0x02000114 RID: 276
	public struct BallCollisionEvent
	{
		// Token: 0x0600197F RID: 6527 RVA: 0x004BFC02 File Offset: 0x004BDE02
		public BallCollisionEvent(float timeScale, Vector2 normal, Vector2 impactPoint, Tile tile, Entity entity)
		{
			this.Normal = normal;
			this.ImpactPoint = impactPoint;
			this.Tile = tile;
			this.Entity = entity;
			this.TimeScale = timeScale;
		}

		// Token: 0x040013BC RID: 5052
		public readonly Vector2 Normal;

		// Token: 0x040013BD RID: 5053
		public readonly Vector2 ImpactPoint;

		// Token: 0x040013BE RID: 5054
		public readonly Tile Tile;

		// Token: 0x040013BF RID: 5055
		public readonly Entity Entity;

		// Token: 0x040013C0 RID: 5056
		public readonly float TimeScale;
	}
}
