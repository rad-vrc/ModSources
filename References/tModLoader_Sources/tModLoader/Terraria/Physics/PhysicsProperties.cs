using System;

namespace Terraria.Physics
{
	// Token: 0x0200011A RID: 282
	public class PhysicsProperties
	{
		// Token: 0x06001987 RID: 6535 RVA: 0x004BFC69 File Offset: 0x004BDE69
		public PhysicsProperties(float gravity, float drag)
		{
			this.Gravity = gravity;
			this.Drag = drag;
		}

		// Token: 0x040013D0 RID: 5072
		public readonly float Gravity;

		// Token: 0x040013D1 RID: 5073
		public readonly float Drag;
	}
}
