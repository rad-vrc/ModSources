using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200037F RID: 895
	public class UIHorizontalSeparator : UIElement
	{
		// Token: 0x060028A7 RID: 10407 RVA: 0x0058C48C File Offset: 0x0058A68C
		public UIHorizontalSeparator(int EdgeWidth = 2, bool highlightSideUp = true)
		{
			this.Color = Color.White;
			if (highlightSideUp)
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Separator1", 1);
			}
			else
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Separator2", 1);
			}
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x0058C514 File Offset: 0x0058A714
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawPanel(this._texture.Value, this.EdgeWidth, 0, spriteBatch, dimensions.Position(), dimensions.Width, this.Color);
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool ContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x04004BE8 RID: 19432
		private Asset<Texture2D> _texture;

		// Token: 0x04004BE9 RID: 19433
		public Color Color;

		// Token: 0x04004BEA RID: 19434
		public int EdgeWidth;
	}
}
