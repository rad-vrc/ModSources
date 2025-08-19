using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020006DE RID: 1758
	public struct DrillDebugDraw
	{
		// Token: 0x0600493A RID: 18746 RVA: 0x0064D5B6 File Offset: 0x0064B7B6
		public DrillDebugDraw(Vector2 p, Color c)
		{
			this.point = p;
			this.color = c;
		}

		// Token: 0x04005EA2 RID: 24226
		public Vector2 point;

		// Token: 0x04005EA3 RID: 24227
		public Color color;
	}
}
