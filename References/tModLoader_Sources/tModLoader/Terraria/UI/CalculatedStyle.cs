using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x0200009D RID: 157
	public struct CalculatedStyle
	{
		// Token: 0x060014C1 RID: 5313 RVA: 0x004A3C41 File Offset: 0x004A1E41
		public CalculatedStyle(float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x004A3C60 File Offset: 0x004A1E60
		public Rectangle ToRectangle()
		{
			return new Rectangle((int)this.X, (int)this.Y, (int)this.Width, (int)this.Height);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x004A3C83 File Offset: 0x004A1E83
		public Vector2 Position()
		{
			return new Vector2(this.X, this.Y);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x004A3C96 File Offset: 0x004A1E96
		public Vector2 Center()
		{
			return new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f);
		}

		// Token: 0x040010D7 RID: 4311
		public float X;

		// Token: 0x040010D8 RID: 4312
		public float Y;

		// Token: 0x040010D9 RID: 4313
		public float Width;

		// Token: 0x040010DA RID: 4314
		public float Height;
	}
}
