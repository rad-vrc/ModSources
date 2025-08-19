using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000382 RID: 898
	public class UIKeybindingSliderItem : UIElement
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x0058D50C File Offset: 0x0058B70C
		public UIKeybindingSliderItem(Func<string> getText, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, int sliderIDInPage, Color color)
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
				slideKeyboardAction = delegate(float s)
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

		// Token: 0x060028B2 RID: 10418 RVA: 0x0058D5F4 File Offset: 0x0058B7F4
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
			bool flag = false;
			bool flag2 = base.IsMouseHovering;
			if (num2 == 1)
			{
				flag2 = true;
			}
			if (num2 == 2)
			{
				flag2 = false;
			}
			Vector2 baseScale = new Vector2(0.8f);
			Color color = flag ? Color.Gold : (flag2 ? Color.White : Color.Silver);
			color = Color.Lerp(color, Color.White, flag2 ? 0.5f : 0f);
			Color color2 = flag2 ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180));
			Vector2 vector2 = vector;
			Utils.DrawSettingsPanel(spriteBatch, vector2, num3, color2);
			vector2.X += 8f;
			vector2.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._TextDisplayFunction(), vector2, color, 0f, Vector2.Zero, baseScale, num3, 2f);
			vector2.X -= 17f;
			TextureAssets.ColorBar.Frame(1, 1, 0, 0, 0, 0);
			vector2 = new Vector2(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			IngameOptions.valuePosition = vector2;
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

		// Token: 0x04004BF0 RID: 19440
		private Color _color;

		// Token: 0x04004BF1 RID: 19441
		private Func<string> _TextDisplayFunction;

		// Token: 0x04004BF2 RID: 19442
		private Func<float> _GetStatusFunction;

		// Token: 0x04004BF3 RID: 19443
		private Action<float> _SlideKeyboardAction;

		// Token: 0x04004BF4 RID: 19444
		private Action _SlideGamepadAction;

		// Token: 0x04004BF5 RID: 19445
		private int _sliderIDInPage;

		// Token: 0x04004BF6 RID: 19446
		private Asset<Texture2D> _toggleTexture;
	}
}
