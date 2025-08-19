using System;
using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
	// Token: 0x02000119 RID: 281
	public interface IBallContactListener
	{
		// Token: 0x06001985 RID: 6533
		void OnCollision(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref BallCollisionEvent collision);

		// Token: 0x06001986 RID: 6534
		void OnPassThrough(PhysicsProperties properties, ref Vector2 position, ref Vector2 velocity, ref float angularVelocity, ref BallPassThroughEvent passThrough);
	}
}
