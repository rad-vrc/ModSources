using System;
using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
	// Token: 0x020000B3 RID: 179
	public interface IBallContactListener
	{
		// Token: 0x060013F3 RID: 5107
		void OnCollision(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref BallCollisionEvent collision);

		// Token: 0x060013F4 RID: 5108
		void OnPassThrough(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref float angularVelocity, ref BallPassThroughEvent passThrough);
	}
}
