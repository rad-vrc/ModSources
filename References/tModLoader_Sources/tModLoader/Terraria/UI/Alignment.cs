using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x0200009C RID: 156
	public struct Alignment
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x004A3B5C File Offset: 0x004A1D5C
		public Vector2 OffsetMultiplier
		{
			get
			{
				return new Vector2(this.HorizontalOffsetMultiplier, this.VerticalOffsetMultiplier);
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x004A3B6F File Offset: 0x004A1D6F
		private Alignment(float horizontal, float vertical)
		{
			this.HorizontalOffsetMultiplier = horizontal;
			this.VerticalOffsetMultiplier = vertical;
		}

		// Token: 0x040010CC RID: 4300
		public static readonly Alignment TopLeft = new Alignment(0f, 0f);

		// Token: 0x040010CD RID: 4301
		public static readonly Alignment Top = new Alignment(0.5f, 0f);

		// Token: 0x040010CE RID: 4302
		public static readonly Alignment TopRight = new Alignment(1f, 0f);

		// Token: 0x040010CF RID: 4303
		public static readonly Alignment Left = new Alignment(0f, 0.5f);

		// Token: 0x040010D0 RID: 4304
		public static readonly Alignment Center = new Alignment(0.5f, 0.5f);

		// Token: 0x040010D1 RID: 4305
		public static readonly Alignment Right = new Alignment(1f, 0.5f);

		// Token: 0x040010D2 RID: 4306
		public static readonly Alignment BottomLeft = new Alignment(0f, 1f);

		// Token: 0x040010D3 RID: 4307
		public static readonly Alignment Bottom = new Alignment(0.5f, 1f);

		// Token: 0x040010D4 RID: 4308
		public static readonly Alignment BottomRight = new Alignment(1f, 1f);

		// Token: 0x040010D5 RID: 4309
		public readonly float VerticalOffsetMultiplier;

		// Token: 0x040010D6 RID: 4310
		public readonly float HorizontalOffsetMultiplier;
	}
}
