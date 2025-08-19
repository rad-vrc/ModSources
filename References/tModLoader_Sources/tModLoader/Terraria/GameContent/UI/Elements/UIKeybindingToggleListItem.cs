using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000525 RID: 1317
	public class UIKeybindingToggleListItem : UIElement
	{
		// Token: 0x06003EEF RID: 16111 RVA: 0x005D6E38 File Offset: 0x005D5038
		public UIKeybindingToggleListItem(Func<string> getText, Func<bool> getStatus, Color color)
		{
			this._color = color;
			this._toggleTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle");
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

		// Token: 0x06003EF0 RID: 16112 RVA: 0x005D6EC0 File Offset: 0x005D50C0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector3 = new Vector2(dimensions.X, dimensions.Y);
			Vector2 baseScale;
			baseScale..ctor(0.8f);
			Color value = base.IsMouseHovering ? Color.White : Color.Silver;
			value = Color.Lerp(value, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color = base.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 position = vector3;
			Utils.DrawSettingsPanel(spriteBatch, position, num2, color);
			position.X += 8f;
			position.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._TextDisplayFunction(), position, value, 0f, Vector2.Zero, baseScale, num2, 2f);
			position.X -= 17f;
			Rectangle value2;
			value2..ctor(this._IsOnFunction() ? ((this._toggleTexture.Width() - 2) / 2 + 2) : 0, 0, (this._toggleTexture.Width() - 2) / 2, this._toggleTexture.Height());
			Vector2 vector2;
			vector2..ctor((float)value2.Width, 0f);
			Vector2 vector4;
			vector4..ctor(dimensions.X + dimensions.Width - vector2.X - 10f, dimensions.Y + 2f + num);
			spriteBatch.Draw(this._toggleTexture.Value, vector4, new Rectangle?(value2), Color.White, 0f, Vector2.Zero, Vector2.One, 0, 0f);
		}

		// Token: 0x04005779 RID: 22393
		private Color _color;

		// Token: 0x0400577A RID: 22394
		private Func<string> _TextDisplayFunction;

		// Token: 0x0400577B RID: 22395
		private Func<bool> _IsOnFunction;

		// Token: 0x0400577C RID: 22396
		private Asset<Texture2D> _toggleTexture;
	}
}
