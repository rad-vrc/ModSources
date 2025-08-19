using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000383 RID: 899
	public class UIKeybindingToggleListItem : UIElement
	{
		// Token: 0x060028B3 RID: 10419 RVA: 0x0058D850 File Offset: 0x0058BA50
		public UIKeybindingToggleListItem(Func<string> getText, Func<bool> getStatus, Color color)
		{
			this._color = color;
			this._toggleTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle", 1);
			Func<string> textDisplayFunction;
			if (getText == null)
			{
				textDisplayFunction = (() => "???");
			}
			else
			{
				textDisplayFunction = getText;
			}
			this._TextDisplayFunction = textDisplayFunction;
			Func<bool> isOnFunction;
			if (getStatus == null)
			{
				isOnFunction = (() => false);
			}
			else
			{
				isOnFunction = getStatus;
			}
			this._IsOnFunction = isOnFunction;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0058D8D8 File Offset: 0x0058BAD8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			bool flag = false;
			Vector2 baseScale = new Vector2(0.8f);
			Color color = flag ? Color.Gold : (base.IsMouseHovering ? Color.White : Color.Silver);
			color = Color.Lerp(color, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color2 = base.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 position = vector;
			Utils.DrawSettingsPanel(spriteBatch, position, num2, color2);
			position.X += 8f;
			position.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._TextDisplayFunction(), position, color, 0f, Vector2.Zero, baseScale, num2, 2f);
			position.X -= 17f;
			Rectangle rectangle = new Rectangle(this._IsOnFunction() ? ((this._toggleTexture.Width() - 2) / 2 + 2) : 0, 0, (this._toggleTexture.Width() - 2) / 2, this._toggleTexture.Height());
			Vector2 vector2 = new Vector2((float)rectangle.Width, 0f);
			position = new Vector2(dimensions.X + dimensions.Width - vector2.X - 10f, dimensions.Y + 2f + num);
			spriteBatch.Draw(this._toggleTexture.Value, position, new Rectangle?(rectangle), Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
		}

		// Token: 0x04004BF7 RID: 19447
		private Color _color;

		// Token: 0x04004BF8 RID: 19448
		private Func<string> _TextDisplayFunction;

		// Token: 0x04004BF9 RID: 19449
		private Func<bool> _IsOnFunction;

		// Token: 0x04004BFA RID: 19450
		private Asset<Texture2D> _toggleTexture;
	}
}
