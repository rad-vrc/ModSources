using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
	// Token: 0x02000633 RID: 1587
	public class ScreenObstruction
	{
		// Token: 0x06004597 RID: 17815 RVA: 0x006142AC File Offset: 0x006124AC
		public static void Update()
		{
			float value = 0f;
			float amount = 0.1f;
			if (Main.player[Main.myPlayer].headcovered)
			{
				value = 0.95f;
				amount = 0.3f;
			}
			ScreenObstruction.screenObstruction = MathHelper.Lerp(ScreenObstruction.screenObstruction, value, amount);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x006142F4 File Offset: 0x006124F4
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (ScreenObstruction.screenObstruction != 0f)
			{
				Color color = Color.Black * ScreenObstruction.screenObstruction;
				int num = TextureAssets.Extra[49].Width();
				int num2 = 10;
				Rectangle rect = Main.player[Main.myPlayer].getRect();
				rect.Inflate((num - rect.Width) / 2, (num - rect.Height) / 2 + num2 / 2);
				rect.Offset(-(int)Main.screenPosition.X, -(int)Main.screenPosition.Y + (int)Main.player[Main.myPlayer].gfxOffY - num2);
				Rectangle destinationRectangle = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(rect.Right - 1, rect.Top - 1, 1, 1));
				Rectangle destinationRectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(rect.Right, rect.Bottom - 1, 1, 1));
				Rectangle destinationRectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left, rect.Bottom, 1, 1));
				Rectangle destinationRectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left - 1, rect.Top, 1, 1));
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, destinationRectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
				spriteBatch.Draw(TextureAssets.Extra[49].Value, rect, color);
			}
		}

		// Token: 0x04005B03 RID: 23299
		public static float screenObstruction;
	}
}
