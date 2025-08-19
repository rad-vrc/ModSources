using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x0200007E RID: 126
	public struct CalculatedStyle
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x0048F439 File Offset: 0x0048D639
		public CalculatedStyle(float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0048F458 File Offset: 0x0048D658
		public Rectangle ToRectangle()
		{
			return new Rectangle((int)this.X, (int)this.Y, (int)this.Width, (int)this.Height);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0048F47B File Offset: 0x0048D67B
		public Vector2 Position()
		{
			return new Vector2(this.X, this.Y);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0048F48E File Offset: 0x0048D68E
		public Vector2 Center()
		{
			return new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f);
		}

		// Token: 0x04000FE1 RID: 4065
		public float X;

		// Token: 0x04000FE2 RID: 4066
		public float Y;

		// Token: 0x04000FE3 RID: 4067
		public float Width;

		// Token: 0x04000FE4 RID: 4068
		public float Height;
	}
}
