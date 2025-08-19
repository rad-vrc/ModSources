using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000523 RID: 1315
	public class UIKeybindingSimpleListItem : UIElement
	{
		// Token: 0x06003EEB RID: 16107 RVA: 0x005D6931 File Offset: 0x005D4B31
		public UIKeybindingSimpleListItem(Func<string> getText, Color color)
		{
			this._color = color;
			Func<string> getTextFunction;
			if (getText == null)
			{
				getTextFunction = (() => "???");
			}
			else
			{
				getTextFunction = getText;
			}
			this._GetTextFunction = getTextFunction;
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x005D696C File Offset: 0x005D4B6C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			Vector2 baseScale;
			baseScale..ctor(0.8f);
			Color value = base.IsMouseHovering ? Color.White : Color.Silver;
			value = Color.Lerp(value, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color = base.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 position = vector;
			Utils.DrawSettings2Panel(spriteBatch, position, num2, color);
			position.X += 8f;
			position.Y += 2f + num;
			string text = this._GetTextFunction();
			Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, baseScale, -1f);
			if (stringSize.X > dimensions.Width)
			{
				float scaleToFit = dimensions.Width / stringSize.X;
				stringSize.X *= scaleToFit;
				baseScale.X *= scaleToFit;
			}
			position.X = dimensions.X + dimensions.Width / 2f - stringSize.X / 2f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, position, value, 0f, Vector2.Zero, baseScale, num2, 2f);
		}

		// Token: 0x04005770 RID: 22384
		private Color _color;

		// Token: 0x04005771 RID: 22385
		private Func<string> _GetTextFunction;
	}
}
