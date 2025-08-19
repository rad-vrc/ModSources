using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000524 RID: 1316
	public class UIKeybindingSliderItem : UIElement
	{
		// Token: 0x06003EED RID: 16109 RVA: 0x005D6B00 File Offset: 0x005D4D00
		public UIKeybindingSliderItem(Func<string> getText, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, int sliderIDInPage, Color color)
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
			Func<float> getStatusFunction;
			if (getStatus == null)
			{
				getStatusFunction = (() => 0f);
			}
			else
			{
				getStatusFunction = getStatus;
			}
			this._GetStatusFunction = getStatusFunction;
			Action<float> slideKeyboardAction;
			if (setStatusKeyboard == null)
			{
				slideKeyboardAction = delegate(float <p0>)
				{
				};
			}
			else
			{
				slideKeyboardAction = setStatusKeyboard;
			}
			this._SlideKeyboardAction = slideKeyboardAction;
			Action slideGamepadAction;
			if (setStatusGamepad == null)
			{
				slideGamepadAction = delegate()
				{
				};
			}
			else
			{
				slideGamepadAction = setStatusGamepad;
			}
			this._SlideGamepadAction = slideGamepadAction;
			this._sliderIDInPage = sliderIDInPage;
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x005D6BE8 File Offset: 0x005D4DE8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			int num2 = 0;
			IngameOptions.rightHover = -1;
			if (!Main.mouseLeft)
			{
				IngameOptions.rightLock = -1;
			}
			if (IngameOptions.rightLock == this._sliderIDInPage)
			{
				num2 = 1;
			}
			else if (IngameOptions.rightLock != -1)
			{
				num2 = 2;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			float num3 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			bool flag = base.IsMouseHovering;
			if (num2 == 1)
			{
				flag = true;
			}
			if (num2 == 2)
			{
				flag = false;
			}
			Vector2 baseScale;
			baseScale..ctor(0.8f);
			Color value = flag ? Color.White : Color.Silver;
			value = Color.Lerp(value, Color.White, flag ? 0.5f : 0f);
			Color color = flag ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 position = vector;
			Utils.DrawSettingsPanel(spriteBatch, position, num3, color);
			position.X += 8f;
			position.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._TextDisplayFunction(), position, value, 0f, Vector2.Zero, baseScale, num3, 2f);
			position.X -= 17f;
			TextureAssets.ColorBar.Frame(1, 1, 0, 0, 0, 0);
			position..ctor(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			IngameOptions.valuePosition = position;
			float obj = IngameOptions.DrawValueBar(spriteBatch, 1f, this._GetStatusFunction(), num2, null);
			if (IngameOptions.inBar || IngameOptions.rightLock == this._sliderIDInPage)
			{
				IngameOptions.rightHover = this._sliderIDInPage;
				if (PlayerInput.Triggers.Current.MouseLeft && PlayerInput.CurrentProfile.AllowEditting && !PlayerInput.UsingGamepad && IngameOptions.rightLock == this._sliderIDInPage)
				{
					this._SlideKeyboardAction(obj);
				}
			}
			if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
			{
				IngameOptions.rightLock = IngameOptions.rightHover;
			}
			if (base.IsMouseHovering && PlayerInput.CurrentProfile.AllowEditting)
			{
				this._SlideGamepadAction();
			}
		}

		// Token: 0x04005772 RID: 22386
		private Color _color;

		// Token: 0x04005773 RID: 22387
		private Func<string> _TextDisplayFunction;

		// Token: 0x04005774 RID: 22388
		private Func<float> _GetStatusFunction;

		// Token: 0x04005775 RID: 22389
		private Action<float> _SlideKeyboardAction;

		// Token: 0x04005776 RID: 22390
		private Action _SlideGamepadAction;

		// Token: 0x04005777 RID: 22391
		private int _sliderIDInPage;

		// Token: 0x04005778 RID: 22392
		private Asset<Texture2D> _toggleTexture;
	}
}
