using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
	// Token: 0x020000F8 RID: 248
	public struct VertexColors
	{
		// Token: 0x0600160B RID: 5643 RVA: 0x004C5777 File Offset: 0x004C3977
		public VertexColors(Color color)
		{
			this.TopLeftColor = color;
			this.TopRightColor = color;
			this.BottomRightColor = color;
			this.BottomLeftColor = color;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x004C5795 File Offset: 0x004C3995
		public VertexColors(Color topLeft, Color topRight, Color bottomRight, Color bottomLeft)
		{
			this.TopLeftColor = topLeft;
			this.TopRightColor = topRight;
			this.BottomLeftColor = bottomLeft;
			this.BottomRightColor = bottomRight;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x004C57B4 File Offset: 0x004C39B4
		public static implicit operator VertexColors(Color color)
		{
			return new VertexColors(color);
		}

		// Token: 0x04001305 RID: 4869
		public Color TopLeftColor;

		// Token: 0x04001306 RID: 4870
		public Color TopRightColor;

		// Token: 0x04001307 RID: 4871
		public Color BottomLeftColor;

		// Token: 0x04001308 RID: 4872
		public Color BottomRightColor;
	}
}
