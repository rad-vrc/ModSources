using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
	// Token: 0x02000440 RID: 1088
	public struct VertexColors
	{
		// Token: 0x060035E2 RID: 13794 RVA: 0x00579953 File Offset: 0x00577B53
		public VertexColors(Color color)
		{
			this.TopLeftColor = color;
			this.TopRightColor = color;
			this.BottomRightColor = color;
			this.BottomLeftColor = color;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x00579971 File Offset: 0x00577B71
		public VertexColors(Color topLeft, Color topRight, Color bottomRight, Color bottomLeft)
		{
			this.TopLeftColor = topLeft;
			this.TopRightColor = topRight;
			this.BottomLeftColor = bottomLeft;
			this.BottomRightColor = bottomRight;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x00579990 File Offset: 0x00577B90
		public static implicit operator VertexColors(Color color)
		{
			return new VertexColors(color);
		}

		// Token: 0x04004FEE RID: 20462
		public Color TopLeftColor;

		// Token: 0x04004FEF RID: 20463
		public Color TopRightColor;

		// Token: 0x04004FF0 RID: 20464
		public Color BottomLeftColor;

		// Token: 0x04004FF1 RID: 20465
		public Color BottomRightColor;
	}
}
