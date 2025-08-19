using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200044A RID: 1098
	public struct DrillDebugDraw
	{
		// Token: 0x06002BF1 RID: 11249 RVA: 0x005A0242 File Offset: 0x0059E442
		public DrillDebugDraw(Vector2 p, Color c)
		{
			this.point = p;
			this.color = c;
		}

		// Token: 0x04005013 RID: 20499
		public Vector2 point;

		// Token: 0x04005014 RID: 20500
		public Color color;
	}
}
