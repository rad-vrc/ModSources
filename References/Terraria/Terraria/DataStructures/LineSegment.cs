using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200044B RID: 1099
	public struct LineSegment
	{
		// Token: 0x06002BF2 RID: 11250 RVA: 0x005A0252 File Offset: 0x0059E452
		public LineSegment(Vector2 start, Vector2 end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x04005015 RID: 20501
		public Vector2 Start;

		// Token: 0x04005016 RID: 20502
		public Vector2 End;
	}
}
