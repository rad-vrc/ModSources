using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200037D RID: 893
	public class UIColoredSlider : UISliderBase
	{
		// Token: 0x060028A1 RID: 10401 RVA: 0x0058BE0C File Offset: 0x0058A00C
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
				slideKeyboardAction = delegate(float s)
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

		// Token: 0x060028A2 RID: 10402 RVA: 0x0058BEC0 File Offset: 0x0058A0C0
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
			float num2 = dimensions.Width + 1f;
			Vector2 value = new Vector2(dimensions.X, dimensions.Y);
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
			Vector2 drawPosition = value + new Vector2(0f, 2f);
			Color value2 = flag ? Color.Gold : (flag2 ? Color.White : Color.Silver);
			value2 = Color.Lerp(value2, Color.White, flag2 ? 0.5f : 0f);
			Vector2 vector = new Vector2(0.8f);
			drawPosition.X += 8f;
			drawPosition.Y += num;
			drawPosition.X -= 17f;
			TextureAssets.ColorBar.Frame(1, 1, 0, 0, 0, 0);
			drawPosition = new Vector2(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			bool flag3;
			float obj = this.DrawValueBar(spriteBatch, drawPosition, 1f, this._getStatusTextAct(), usageLevel, out flag3, this._blipFunc);
			if (UISliderBase.CurrentLockedSlider == this || flag3)
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

		// Token: 0x060028A3 RID: 10403 RVA: 0x0058C0B0 File Offset: 0x0058A2B0
		private float DrawValueBar(SpriteBatch sb, Vector2 drawPosition, float drawScale, float sliderPosition, int lockMode, out bool wasInBar, Func<float, Color> blipColorFunc)
		{
			Texture2D value = TextureAssets.ColorBar.Value;
			Vector2 vector = new Vector2((float)value.Width, (float)value.Height) * drawScale;
			drawPosition.X -= (float)((int)vector.X);
			Rectangle rectangle = new Rectangle((int)drawPosition.X, (int)drawPosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y);
			Rectangle destinationRectangle = rectangle;
			sb.Draw(value, rectangle, Color.White);
			float num = (float)rectangle.X + 5f * drawScale;
			float num2 = (float)rectangle.Y + 4f * drawScale;
			for (float num3 = 0f; num3 < 167f; num3 += 1f)
			{
				float arg = num3 / 167f;
				Color color = blipColorFunc(arg);
				sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(num + num3 * drawScale, num2), null, color, 0f, Vector2.Zero, drawScale, SpriteEffects.None, 0f);
			}
			rectangle.X = (int)num - 2;
			rectangle.Y = (int)num2;
			rectangle.Width -= 4;
			rectangle.Height -= 8;
			bool flag = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
			this._isReallyMouseOvered = flag;
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
				sb.Draw(TextureAssets.ColorSlider.Value, new Vector2(num + 167f * drawScale * sliderPosition, num2 + 4f * drawScale), null, Color.White, 0f, new Vector2(0.5f * (float)TextureAssets.ColorSlider.Value.Width, 0.5f * (float)TextureAssets.ColorSlider.Value.Height), drawScale, SpriteEffects.None, 0f);
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

		// Token: 0x04004BDB RID: 19419
		private Color _color;

		// Token: 0x04004BDC RID: 19420
		private LocalizedText _textKey;

		// Token: 0x04004BDD RID: 19421
		private Func<float> _getStatusTextAct;

		// Token: 0x04004BDE RID: 19422
		private Action<float> _slideKeyboardAction;

		// Token: 0x04004BDF RID: 19423
		private Func<float, Color> _blipFunc;

		// Token: 0x04004BE0 RID: 19424
		private Action _slideGamepadAction;

		// Token: 0x04004BE1 RID: 19425
		private const bool BOTHER_WITH_TEXT = false;

		// Token: 0x04004BE2 RID: 19426
		private bool _isReallyMouseOvered;

		// Token: 0x04004BE3 RID: 19427
		private bool _alreadyHovered;

		// Token: 0x04004BE4 RID: 19428
		private bool _soundedUsage;
	}
}
