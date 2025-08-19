﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200036D RID: 877
	public class UIVerticalSlider : UISliderBase
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x0058877C File Offset: 0x0058697C
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
				slideKeyboardAction = delegate(float s)
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

		// Token: 0x06002830 RID: 10288 RVA: 0x0058880C File Offset: 0x00586A0C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UISliderBase.CurrentAimedSlider = null;
			if (!Main.mouseLeft)
			{
				UISliderBase.CurrentLockedSlider = null;
			}
			base.GetUsageLevel();
			this.FillPercent = this._getSliderValue();
			float fillPercent = this.FillPercent;
			bool flag = false;
			if (this.DrawValueBarDynamicWidth(spriteBatch, out fillPercent))
			{
				flag = true;
			}
			if (UISliderBase.CurrentLockedSlider == this || flag)
			{
				UISliderBase.CurrentAimedSlider = this;
				if (PlayerInput.Triggers.Current.MouseLeft && !PlayerInput.UsingGamepad && UISliderBase.CurrentLockedSlider == this)
				{
					this._slideKeyboardAction(fillPercent);
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

		// Token: 0x06002831 RID: 10289 RVA: 0x005888F0 File Offset: 0x00586AF0
		private bool DrawValueBarDynamicWidth(SpriteBatch spriteBatch, out float sliderValueThatWasSet)
		{
			sliderValueThatWasSet = 0f;
			Texture2D value = TextureAssets.ColorBar.Value;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle rectangle2 = new Rectangle(5, 4, 4, 4);
			Utils.DrawSplicedPanel(spriteBatch, value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, rectangle2.X, rectangle2.Width, rectangle2.Y, rectangle2.Height, Color.White);
			Rectangle rectangle3 = rectangle;
			rectangle3.X += rectangle2.Left;
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Y += rectangle2.Top;
			rectangle3.Height -= rectangle2.Bottom;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			Rectangle value3 = new Rectangle(0, 0, 1, 1);
			spriteBatch.Draw(value2, rectangle3, new Rectangle?(value3), this.EmptyColor);
			Rectangle rectangle4 = rectangle3;
			rectangle4.Height = (int)((float)rectangle4.Height * this.FillPercent);
			rectangle4.Y += rectangle3.Height - rectangle4.Height;
			spriteBatch.Draw(value2, rectangle4, new Rectangle?(value3), this.FilledColor);
			Vector2 center = new Vector2((float)(rectangle4.Center.X + 1), (float)rectangle4.Top);
			Vector2 size = new Vector2((float)(rectangle4.Width + 16), 4f);
			Rectangle rectangle5 = Utils.CenteredRectangle(center, size);
			Rectangle destinationRectangle = rectangle5;
			destinationRectangle.Inflate(2, 2);
			spriteBatch.Draw(value2, destinationRectangle, new Rectangle?(value3), Color.Black);
			spriteBatch.Draw(value2, rectangle5, new Rectangle?(value3), Color.White);
			Rectangle rectangle6 = rectangle3;
			rectangle6.Inflate(4, 0);
			bool flag = rectangle6.Contains(Main.MouseScreen.ToPoint());
			this._isReallyMouseOvered = flag;
			bool flag2 = flag;
			if (this.IgnoresMouseInteraction)
			{
				flag2 = false;
			}
			int usageLevel = base.GetUsageLevel();
			if (usageLevel == 2)
			{
				flag2 = false;
			}
			if (usageLevel == 1)
			{
				flag2 = true;
			}
			if (flag2 || usageLevel == 1)
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
			if (flag2)
			{
				sliderValueThatWasSet = Utils.GetLerpValue((float)rectangle3.Bottom, (float)rectangle3.Top, (float)Main.mouseY, true);
				return true;
			}
			return false;
		}

		// Token: 0x04004B4D RID: 19277
		public float FillPercent;

		// Token: 0x04004B4E RID: 19278
		public Color FilledColor = Main.OurFavoriteColor;

		// Token: 0x04004B4F RID: 19279
		public Color EmptyColor = Color.Black;

		// Token: 0x04004B50 RID: 19280
		private Func<float> _getSliderValue;

		// Token: 0x04004B51 RID: 19281
		private Action<float> _slideKeyboardAction;

		// Token: 0x04004B52 RID: 19282
		private Func<float, Color> _blipFunc;

		// Token: 0x04004B53 RID: 19283
		private Action _slideGamepadAction;

		// Token: 0x04004B54 RID: 19284
		private bool _isReallyMouseOvered;

		// Token: 0x04004B55 RID: 19285
		private bool _soundedUsage;

		// Token: 0x04004B56 RID: 19286
		private bool _alreadyHovered;
	}
}
