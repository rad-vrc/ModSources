using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200071B RID: 1819
	public struct LineSegment
	{
		// Token: 0x060049DE RID: 18910 RVA: 0x0064E5A0 File Offset: 0x0064C7A0
		public LineSegment(Vector2 start, Vector2 end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x04005F0B RID: 24331
		public Vector2 Start;

		// Token: 0x04005F0C RID: 24332
		public Vector2 End;
	}
}
