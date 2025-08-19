using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000536 RID: 1334
	public class UIVerticalSeparator : UIElement
	{
		// Token: 0x06003F91 RID: 16273 RVA: 0x005D9C28 File Offset: 0x005D7E28
		public UIVerticalSeparator()
		{
			this.Color = Color.White;
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/OnePixel");
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x005D9C94 File Offset: 0x005D7E94
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			spriteBatch.Draw(this._texture.Value, rectangle, this.Color);
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x005D9CC8 File Offset: 0x005D7EC8
		public override bool ContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x040057E5 RID: 22501
		private Asset<Texture2D> _texture;

		// Token: 0x040057E6 RID: 22502
		public Color Color;

		// Token: 0x040057E7 RID: 22503
		public int EdgeWidth;
	}
}
