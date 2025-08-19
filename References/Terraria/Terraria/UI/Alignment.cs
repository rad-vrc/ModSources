using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x0200007D RID: 125
	public struct Alignment
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0048F352 File Offset: 0x0048D552
		public Vector2 OffsetMultiplier
		{
			get
			{
				return new Vector2(this.HorizontalOffsetMultiplier, this.VerticalOffsetMultiplier);
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0048F365 File Offset: 0x0048D565
		private Alignment(float horizontal, float vertical)
		{
			this.HorizontalOffsetMultiplier = horizontal;
			this.VerticalOffsetMultiplier = vertical;
		}

		// Token: 0x04000FD6 RID: 4054
		public static readonly Alignment TopLeft = new Alignment(0f, 0f);

		// Token: 0x04000FD7 RID: 4055
		public static readonly Alignment Top = new Alignment(0.5f, 0f);

		// Token: 0x04000FD8 RID: 4056
		public static readonly Alignment TopRight = new Alignment(1f, 0f);

		// Token: 0x04000FD9 RID: 4057
		public static readonly Alignment Left = new Alignment(0f, 0.5f);

		// Token: 0x04000FDA RID: 4058
		public static readonly Alignment Center = new Alignment(0.5f, 0.5f);

		// Token: 0x04000FDB RID: 4059
		public static readonly Alignment Right = new Alignment(1f, 0.5f);

		// Token: 0x04000FDC RID: 4060
		public static readonly Alignment BottomLeft = new Alignment(0f, 1f);

		// Token: 0x04000FDD RID: 4061
		public static readonly Alignment Bottom = new Alignment(0.5f, 1f);

		// Token: 0x04000FDE RID: 4062
		public static readonly Alignment BottomRight = new Alignment(1f, 1f);

		// Token: 0x04000FDF RID: 4063
		public readonly float VerticalOffsetMultiplier;

		// Token: 0x04000FE0 RID: 4064
		public readonly float HorizontalOffsetMultiplier;
	}
}
