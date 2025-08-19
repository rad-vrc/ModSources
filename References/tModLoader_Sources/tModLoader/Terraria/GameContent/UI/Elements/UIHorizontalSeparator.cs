using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200051A RID: 1306
	public class UIHorizontalSeparator : UIElement
	{
		// Token: 0x06003EBE RID: 16062 RVA: 0x005D4EDC File Offset: 0x005D30DC
		public UIHorizontalSeparator(int EdgeWidth = 2, bool highlightSideUp = true)
		{
			this.Color = Color.White;
			if (highlightSideUp)
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Separator1");
			}
			else
			{
				this._texture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/Separator2");
			}
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x005D4F64 File Offset: 0x005D3164
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawPanel(this._texture.Value, this.EdgeWidth, 0, spriteBatch, dimensions.Position(), dimensions.Width, this.Color);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x005D4FA3 File Offset: 0x005D31A3
		public override bool ContainsPoint(Vector2 point)
		{
			return false;
		}

		// Token: 0x04005748 RID: 22344
		private Asset<Texture2D> _texture;

		// Token: 0x04005749 RID: 22345
		public Color Color;

		// Token: 0x0400574A RID: 22346
		public int EdgeWidth;
	}
}
