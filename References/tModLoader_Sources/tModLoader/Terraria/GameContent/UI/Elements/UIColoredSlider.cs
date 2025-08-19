using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000510 RID: 1296
	public class UIColoredSlider : UISliderBase
	{
		// Token: 0x06003E5E RID: 15966 RVA: 0x005D1DF0 File Offset: 0x005CFFF0
		public UIColoredSlider(LocalizedText textKey, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, Func<float, Color> blipColorFunction, Color color)
		{
			this._color = color;
			this._textKey = textKey;
			Func<float> getStatusTextAct;
			if (getStatus == null)
			{
				getStatusTextAct = (() => 0f);
			}
			else
			{
				getStatusTextAct = getStatus;
			}
			this._getStatusTextAct = getStatusTextAct;
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
			this._slideKeyboardAction = slideKeyboardAction;
			Func<float, Color> blipFunc;
			if (blipColorFunction == null)
			{
				blipFunc = ((float s) => Color.Lerp(Color.Black, Color.White, s));
			}
			else
			{
				blipFunc = blipColorFunction;
			}
			this._blipFunc = blipFunc;
			this._slideGamepadAction = setStatusGamepad;
			this._isReallyMouseOvered = false;
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x005D1EA4 File Offset: 0x005D00A4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UISliderBase.CurrentAimedSlider = null;
			if (!Main.mouseLeft)
			{
				UISliderBase.CurrentLockedSlider = null;
			}
			int usageLevel = base.GetUsageLevel();
			float num = 8f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector3 = new Vector2(dimensions.X, dimensions.Y);
			bool flag = false;
			bool flag2 = base.IsMouseHovering;
			if (usageLevel == 2)
			{
				flag2 = false;
			}
			if (usageLevel == 1)
			{
				flag2 = true;
			}
			Vector2 vector2 = vector3 + new Vector2(0f, 2f);
			Color.Lerp(flag ? Color.Gold : (flag2 ? Color.White : Color.Silver), Color.White, flag2 ? 0.5f : 0f);
			new Vector2(0.8f);
			vector2.X += 8f;
			vector2.Y += num;
			vector2.X -= 17f;
			TextureAssets.ColorBar.Frame(1, 1, 0, 0, 0, 0);
			vector2..ctor(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			bool wasInBar;
			float obj = this.DrawValueBar(spriteBatch, vector2, 1f, this._getStatusTextAct(), usageLevel, out wasInBar, this._blipFunc);
			if (UISliderBase.CurrentLockedSlider == this || wasInBar)
			{
				UISliderBase.CurrentAimedSlider = this;
				if (PlayerInput.Triggers.Current.MouseLeft && !PlayerInput.UsingGamepad && UISliderBase.CurrentLockedSlider == this)
				{
					this._slideKeyboardAction(obj);
					if (!this._soundedUsage)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					this._soundedUsage = true;
				}
				else
				{
					this._soundedUsage = false;
				}
			}
			if (UISliderBase.CurrentAimedSlider != null && UISliderBase.CurrentLockedSlider == null)
			{
				UISliderBase.CurrentLockedSlider = UISliderBase.CurrentAimedSlider;
			}
			if (this._isReallyMouseOvered)
			{
				this._slideGamepadAction();
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x005D2080 File Offset: 0x005D0280
		private float DrawValueBar(SpriteBatch sb, Vector2 drawPosition, float drawScale, float sliderPosition, int lockMode, out bool wasInBar, Func<float, Color> blipColorFunc)
		{
			Texture2D value = TextureAssets.ColorBar.Value;
			Vector2 vector = new Vector2((float)value.Width, (float)value.Height) * drawScale;
			drawPosition.X -= (float)((int)vector.X);
			Rectangle rectangle;
			rectangle..ctor((int)drawPosition.X, (int)drawPosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y);
			Rectangle destinationRectangle = rectangle;
			sb.Draw(value, rectangle, Color.White);
			float num = (float)rectangle.X + 5f * drawScale;
			float num2 = (float)rectangle.Y + 4f * drawScale;
			for (float num3 = 0f; num3 < 167f; num3 += 1f)
			{
				float arg = num3 / 167f;
				Color color = blipColorFunc(arg);
				sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(num + num3 * drawScale, num2), null, color, 0f, Vector2.Zero, drawScale, 0, 0f);
			}
			rectangle.X = (int)num - 2;
			rectangle.Y = (int)num2;
			rectangle.Width -= 4;
			rectangle.Height -= 8;
			bool flag = this._isReallyMouseOvered = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
			if (this.IgnoresMouseInteraction)
			{
				flag = false;
			}
			if (lockMode == 2)
			{
				flag = false;
			}
			if (flag || lockMode == 1)
			{
				sb.Draw(TextureAssets.ColorHighlight.Value, destinationRectangle, Main.OurFavoriteColor);
				if (!this._alreadyHovered)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._alreadyHovered = true;
			}
			else
			{
				this._alreadyHovered = false;
			}
			wasInBar = false;
			if (!this.IgnoresMouseInteraction)
			{
				sb.Draw(TextureAssets.ColorSlider.Value, new Vector2(num + 167f * drawScale * sliderPosition, num2 + 4f * drawScale), null, Color.White, 0f, new Vector2(0.5f * (float)TextureAssets.ColorSlider.Value.Width, 0.5f * (float)TextureAssets.ColorSlider.Value.Height), drawScale, 0, 0f);
				if (Main.mouseX >= rectangle.X && Main.mouseX <= rectangle.X + rectangle.Width)
				{
					wasInBar = flag;
					return (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
				}
			}
			if (rectangle.X >= Main.mouseX)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x040056F4 RID: 22260
		private Color _color;

		// Token: 0x040056F5 RID: 22261
		private LocalizedText _textKey;

		// Token: 0x040056F6 RID: 22262
		private Func<float> _getStatusTextAct;

		// Token: 0x040056F7 RID: 22263
		private Action<float> _slideKeyboardAction;

		// Token: 0x040056F8 RID: 22264
		private Func<float, Color> _blipFunc;

		// Token: 0x040056F9 RID: 22265
		private Action _slideGamepadAction;

		// Token: 0x040056FA RID: 22266
		private const bool BOTHER_WITH_TEXT = false;

		// Token: 0x040056FB RID: 22267
		private bool _isReallyMouseOvered;

		// Token: 0x040056FC RID: 22268
		private bool _alreadyHovered;

		// Token: 0x040056FD RID: 22269
		private bool _soundedUsage;
	}
}
