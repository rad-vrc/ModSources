using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000511 RID: 1297
	public class UIColoredSliderSimple : UIElement
	{
		// Token: 0x06003E61 RID: 15969 RVA: 0x005D231A File Offset: 0x005D051A
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this.DrawValueBarDynamicWidth(spriteBatch);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x005D2324 File Offset: 0x005D0524
		private void DrawValueBarDynamicWidth(SpriteBatch sb)
		{
			Texture2D value = TextureAssets.ColorBar.Value;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle rectangle2;
			rectangle2..ctor(5, 4, 4, 4);
			Utils.DrawSplicedPanel(sb, value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, rectangle2.X, rectangle2.Width, rectangle2.Y, rectangle2.Height, Color.White);
			Rectangle rectangle3 = rectangle;
			rectangle3.X += rectangle2.Left;
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Y += rectangle2.Top;
			rectangle3.Height -= rectangle2.Bottom;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			Rectangle value3;
			value3..ctor(0, 0, 1, 1);
			sb.Draw(value2, rectangle3, new Rectangle?(value3), this.EmptyColor);
			Rectangle destinationRectangle = rectangle3;
			destinationRectangle.Width = (int)((float)destinationRectangle.Width * this.FillPercent);
			sb.Draw(value2, destinationRectangle, new Rectangle?(value3), this.FilledColor);
		}

		// Token: 0x040056FE RID: 22270
		public float FillPercent;

		// Token: 0x040056FF RID: 22271
		public Color FilledColor = Main.OurFavoriteColor;

		// Token: 0x04005700 RID: 22272
		public Color EmptyColor = Color.Black;
	}
}
