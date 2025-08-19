using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000537 RID: 1335
	public class UIVerticalSlider : UISliderBase
	{
		// Token: 0x06003F94 RID: 16276 RVA: 0x005D9CCC File Offset: 0x005D7ECC
		public UIVerticalSlider(Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, Color color)
		{
			Func<float> getSliderValue;
			if (getStatus == null)
			{
				getSliderValue = (() => 0f);
			}
			else
			{
				getSliderValue = getStatus;
			}
			this._getSliderValue = getSliderValue;
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
			this._slideGamepadAction = setStatusGamepad;
			this._isReallyMouseOvered = false;
		}

		// Token: 0x06003F95 RID: 16277 RVA: 0x005D9D5C File Offset: 0x005D7F5C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UISliderBase.CurrentAimedSlider = null;
			if (!Main.mouseLeft)
			{
				UISliderBase.CurrentLockedSlider = null;
			}
			base.GetUsageLevel();
			this.FillPercent = this._getSliderValue();
			float sliderValueThatWasSet = this.FillPercent;
			bool flag = false;
			if (this.DrawValueBarDynamicWidth(spriteBatch, out sliderValueThatWasSet))
			{
				flag = true;
			}
			if (UISliderBase.CurrentLockedSlider == this || flag)
			{
				UISliderBase.CurrentAimedSlider = this;
				if (PlayerInput.Triggers.Current.MouseLeft && !PlayerInput.UsingGamepad && UISliderBase.CurrentLockedSlider == this)
				{
					this._slideKeyboardAction(sliderValueThatWasSet);
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

		// Token: 0x06003F96 RID: 16278 RVA: 0x005D9E40 File Offset: 0x005D8040
		private bool DrawValueBarDynamicWidth(SpriteBatch spriteBatch, out float sliderValueThatWasSet)
		{
			sliderValueThatWasSet = 0f;
			Texture2D value = TextureAssets.ColorBar.Value;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle rectangle2;
			rectangle2..ctor(5, 4, 4, 4);
			Utils.DrawSplicedPanel(spriteBatch, value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, rectangle2.X, rectangle2.Width, rectangle2.Y, rectangle2.Height, Color.White);
			Rectangle rectangle3 = rectangle;
			rectangle3.X += rectangle2.Left;
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Y += rectangle2.Top;
			rectangle3.Height -= rectangle2.Bottom;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			Rectangle value3;
			value3..ctor(0, 0, 1, 1);
			spriteBatch.Draw(value2, rectangle3, new Rectangle?(value3), this.EmptyColor);
			Rectangle destinationRectangle = rectangle3;
			destinationRectangle.Height = (int)((float)destinationRectangle.Height * this.FillPercent);
			destinationRectangle.Y += rectangle3.Height - destinationRectangle.Height;
			spriteBatch.Draw(value2, destinationRectangle, new Rectangle?(value3), this.FilledColor);
			Vector2 center = new Vector2((float)(destinationRectangle.Center.X + 1), (float)destinationRectangle.Top);
			Vector2 size;
			size..ctor((float)(destinationRectangle.Width + 16), 4f);
			Rectangle rectangle4 = Utils.CenteredRectangle(center, size);
			Rectangle destinationRectangle2 = rectangle4;
			destinationRectangle2.Inflate(2, 2);
			spriteBatch.Draw(value2, destinationRectangle2, new Rectangle?(value3), Color.Black);
			spriteBatch.Draw(value2, rectangle4, new Rectangle?(value3), Color.White);
			Rectangle rectangle5 = rectangle3;
			rectangle5.Inflate(4, 0);
			bool flag = this._isReallyMouseOvered = rectangle5.Contains(Main.MouseScreen.ToPoint());
			if (this.IgnoresMouseInteraction)
			{
				flag = false;
			}
			int usageLevel = base.GetUsageLevel();
			if (usageLevel == 2)
			{
				flag = false;
			}
			if (usageLevel == 1)
			{
				flag = true;
			}
			if (flag || usageLevel == 1)
			{
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
			if (flag)
			{
				sliderValueThatWasSet = Utils.GetLerpValue((float)rectangle3.Bottom, (float)rectangle3.Top, (float)Main.mouseY, true);
				return true;
			}
			return false;
		}

		// Token: 0x040057E8 RID: 22504
		public float FillPercent;

		// Token: 0x040057E9 RID: 22505
		public Color FilledColor = Main.OurFavoriteColor;

		// Token: 0x040057EA RID: 22506
		public Color EmptyColor = Color.Black;

		// Token: 0x040057EB RID: 22507
		private Func<float> _getSliderValue;

		// Token: 0x040057EC RID: 22508
		private Action<float> _slideKeyboardAction;

		// Token: 0x040057ED RID: 22509
		private Func<float, Color> _blipFunc;

		// Token: 0x040057EE RID: 22510
		private Action _slideGamepadAction;

		// Token: 0x040057EF RID: 22511
		private bool _isReallyMouseOvered;

		// Token: 0x040057F0 RID: 22512
		private bool _soundedUsage;

		// Token: 0x040057F1 RID: 22513
		private bool _alreadyHovered;
	}
}
