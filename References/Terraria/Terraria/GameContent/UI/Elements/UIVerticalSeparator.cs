using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000386 RID: 902
	public class UIVerticalSeparator : UIElement
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x0058E1B4 File Offset: 0x0058C3B4
		public UIVerticalSeparator()
		{
			this.Color = Color.White;
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/OnePixel", 1);
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0058E220 File Offset: 0x0058C420
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.ToRectangle(), this.Color);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool ContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x04004C0E RID: 19470
		private Asset<Texture2D> _texture;

		// Token: 0x04004C0F RID: 19471
		public Color Color;

		// Token: 0x04004C10 RID: 19472
		public int EdgeWidth;
	}
}
