using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200037E RID: 894
	public class UIColoredSliderSimple : UIElement
	{
		// Token: 0x060028A4 RID: 10404 RVA: 0x0058C348 File Offset: 0x0058A548
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.DrawValueBarDynamicWidth(spriteBatch);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x0058C354 File Offset: 0x0058A554
		private void DrawValueBarDynamicWidth(SpriteBatch sb)
		{
			Texture2D value = TextureAssets.ColorBar.Value;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle rectangle2 = new Rectangle(5, 4, 4, 4);
			Utils.DrawSplicedPanel(sb, value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, rectangle2.X, rectangle2.Width, rectangle2.Y, rectangle2.Height, Color.White);
			Rectangle rectangle3 = rectangle;
			rectangle3.X += rectangle2.Left;
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Y += rectangle2.Top;
			rectangle3.Height -= rectangle2.Bottom;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			Rectangle value3 = new Rectangle(0, 0, 1, 1);
			sb.Draw(value2, rectangle3, new Rectangle?(value3), this.EmptyColor);
			Rectangle rectangle4 = rectangle3;
			rectangle4.Width = (int)((float)rectangle4.Width * this.FillPercent);
			sb.Draw(value2, rectangle4, new Rectangle?(value3), this.FilledColor);
		}

		// Token: 0x04004BE5 RID: 19429
		public float FillPercent;

		// Token: 0x04004BE6 RID: 19430
		public Color FilledColor = Main.OurFavoriteColor;

		// Token: 0x04004BE7 RID: 19431
		public Color EmptyColor = Color.Black;
	}
}
