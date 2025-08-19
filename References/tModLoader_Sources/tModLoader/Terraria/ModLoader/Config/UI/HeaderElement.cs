using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x0200039B RID: 923
	internal class HeaderElement : UIElement
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x005411A4 File Offset: 0x0053F3A4
		public HeaderElement(string header)
		{
			this.header = header;
			Vector2 size = ChatManager.GetStringSize(FontAssets.ItemStack.Value, this.header, Vector2.One, 532f);
			this.Width.Set(0f, 1f);
			this.Height.Set(size.Y + 6f, 0f);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00541210 File Offset: 0x0053F410
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float settingsWidth = dimensions.Width + 1f;
			Vector2 position = new Vector2(dimensions.X, dimensions.Y) + new Vector2(8f);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)dimensions.X + 10, (int)dimensions.Y + (int)dimensions.Height - 2, (int)dimensions.Width - 20, 1), Color.LightGray);
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this.header, position, Color.White, 0f, Vector2.Zero, new Vector2(1f), settingsWidth - 20f, 2f);
		}

		// Token: 0x04001D70 RID: 7536
		private readonly string header;
	}
}
