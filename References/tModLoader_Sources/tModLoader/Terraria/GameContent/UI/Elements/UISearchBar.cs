using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200052E RID: 1326
	public class UISearchBar : UIElement
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x005D8570 File Offset: 0x005D6770
		public bool HasContents
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.actualContents);
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x005D8580 File Offset: 0x005D6780
		public bool IsWritingText
		{
			get
			{
				return this.isWritingText;
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06003F3A RID: 16186 RVA: 0x005D8588 File Offset: 0x005D6788
		// (remove) Token: 0x06003F3B RID: 16187 RVA: 0x005D85C0 File Offset: 0x005D67C0
		public event Action<string> OnContentsChanged;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06003F3C RID: 16188 RVA: 0x005D85F8 File Offset: 0x005D67F8
		// (remove) Token: 0x06003F3D RID: 16189 RVA: 0x005D8630 File Offset: 0x005D6830
		public event Action OnStartTakingInput;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06003F3E RID: 16190 RVA: 0x005D8668 File Offset: 0x005D6868
		// (remove) Token: 0x06003F3F RID: 16191 RVA: 0x005D86A0 File Offset: 0x005D68A0
		public event Action OnEndTakingInput;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06003F40 RID: 16192 RVA: 0x005D86D8 File Offset: 0x005D68D8
		// (remove) Token: 0x06003F41 RID: 16193 RVA: 0x005D8710 File Offset: 0x005D6910
		public event Action OnCanceledTakingInput;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06003F42 RID: 16194 RVA: 0x005D8748 File Offset: 0x005D6948
		// (remove) Token: 0x06003F43 RID: 16195 RVA: 0x005D8780 File Offset: 0x005D6980
		public event Action OnNeedingVirtualKeyboard;

		// Token: 0x06003F44 RID: 16196 RVA: 0x005D87B8 File Offset: 0x005D69B8
		public UISearchBar(LocalizedText emptyContentText, float scale)
		{
			this._textToShowWhenEmpty = emptyContentText;
			this._textScale = scale;
			UITextBox uITextBox = new UITextBox("", scale, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				BackgroundColor = Color.Transparent,
				BorderColor = Color.Transparent,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				TextHAlign = 0f,
				ShowInputTicker = false
			};
			uITextBox.SetTextMaxLength(50);
			base.Append(uITextBox);
			this._text = uITextBox;
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x005D8864 File Offset: 0x005D6A64
		public void SetContents(string contents, bool forced = false)
		{
			if (!(this.actualContents == contents) || forced)
			{
				this.actualContents = contents;
				if (string.IsNullOrEmpty(this.actualContents))
				{
					this._text.TextColor = Color.Gray;
					this._text.SetText(this._textToShowWhenEmpty.Value, this._textScale, false);
				}
				else
				{
					this._text.TextColor = Color.White;
					this._text.SetText(this.actualContents);
					this.actualContents = this._text.Text;
				}
				this.TrimDisplayIfOverElementDimensions(0);
				if (this.OnContentsChanged != null)
				{
					this.OnContentsChanged(contents);
				}
			}
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x005D8918 File Offset: 0x005D6B18
		public void TrimDisplayIfOverElementDimensions(int padding)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			if (dimensions.Width != 0f || dimensions.Height != 0f)
			{
				Point point;
				point..ctor((int)dimensions.X, (int)dimensions.Y);
				Point point2;
				point2..ctor(point.X + (int)dimensions.Width, point.Y + (int)dimensions.Height);
				Rectangle rectangle;
				rectangle..ctor(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
				CalculatedStyle dimensions2 = this._text.GetDimensions();
				Point point3;
				point3..ctor((int)dimensions2.X, (int)dimensions2.Y);
				Point point4;
				point4..ctor(point3.X + (int)this._text.MinWidth.Pixels, point3.Y + (int)this._text.MinHeight.Pixels);
				Rectangle rectangle2;
				rectangle2..ctor(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
				int num = 0;
				while (rectangle2.Right > rectangle.Right - padding && this._text.Text.Length > 0)
				{
					this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1));
					num++;
					this.RecalculateChildren();
					dimensions2 = this._text.GetDimensions();
					point3..ctor((int)dimensions2.X, (int)dimensions2.Y);
					point4..ctor(point3.X + (int)this._text.MinWidth.Pixels, point3.Y + (int)this._text.MinHeight.Pixels);
					rectangle2..ctor(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
					this.actualContents = this._text.Text;
				}
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x005D8B49 File Offset: 0x005D6D49
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x005D8B52 File Offset: 0x005D6D52
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x005D8B70 File Offset: 0x005D6D70
		public override void Update(GameTime gameTime)
		{
			if (this.isWritingText)
			{
				if (this.NeedsVirtualkeyboard())
				{
					if (this.OnNeedingVirtualKeyboard != null)
					{
						this.OnNeedingVirtualKeyboard();
					}
					return;
				}
				PlayerInput.WritingText = true;
				Main.CurrentInputTextTakerOverride = this;
			}
			base.Update(gameTime);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x005D8BA9 File Offset: 0x005D6DA9
		private bool NeedsVirtualkeyboard()
		{
			return PlayerInput.SettingsForUI.ShowGamepadHints;
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x005D8BB0 File Offset: 0x005D6DB0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (!this.isWritingText)
			{
				return;
			}
			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			Vector2 position;
			position..ctor((float)(Main.screenWidth / 2), (float)(this._text.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.DrawWindowsIMEPanel(position, 0.5f);
			string inputText = Main.GetInputText(this.actualContents, false);
			if (Main.inputTextEnter)
			{
				this.ToggleTakingText();
			}
			else if (Main.inputTextEscape)
			{
				this.ToggleTakingText();
				if (this.OnCanceledTakingInput != null)
				{
					this.OnCanceledTakingInput();
				}
			}
			this.SetContents(inputText, false);
			position..ctor((float)(Main.screenWidth / 2), (float)(this._text.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.DrawWindowsIMEPanel(position, 0.5f);
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x005D8CA0 File Offset: 0x005D6EA0
		public void ToggleTakingText()
		{
			this.isWritingText = !this.isWritingText;
			this._text.ShowInputTicker = this.isWritingText;
			Main.clrInput();
			if (this.isWritingText)
			{
				if (this.OnStartTakingInput != null)
				{
					this.OnStartTakingInput();
					return;
				}
			}
			else if (this.OnEndTakingInput != null)
			{
				this.OnEndTakingInput();
			}
		}

		// Token: 0x040057AA RID: 22442
		private readonly LocalizedText _textToShowWhenEmpty;

		// Token: 0x040057AB RID: 22443
		private UITextBox _text;

		// Token: 0x040057AC RID: 22444
		private string actualContents;

		// Token: 0x040057AD RID: 22445
		private float _textScale;

		// Token: 0x040057AE RID: 22446
		private bool isWritingText;
	}
}
