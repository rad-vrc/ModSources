using System;

namespace Terraria.Physics
{
	// Token: 0x020000B7 RID: 183
	public class PhysicsProperties
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x004A1E7B File Offset: 0x004A007B
		public PhysicsProperties(float gravity, float drag)
		{
			this.Gravity = gravity;
			this.Drag = drag;
		}

		// Token: 0x040011D9 RID: 4569
		public readonly float Gravity;

		// Token: 0x040011DA RID: 4570
		public readonly float Drag;
	}
}
