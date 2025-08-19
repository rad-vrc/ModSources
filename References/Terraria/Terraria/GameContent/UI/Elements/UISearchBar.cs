using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200036C RID: 876
	public class UISearchBar : UIElement
	{
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600281A RID: 10266 RVA: 0x00587FF4 File Offset: 0x005861F4
		// (remove) Token: 0x0600281B RID: 10267 RVA: 0x0058802C File Offset: 0x0058622C
		public event Action<string> OnContentsChanged;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600281C RID: 10268 RVA: 0x00588064 File Offset: 0x00586264
		// (remove) Token: 0x0600281D RID: 10269 RVA: 0x0058809C File Offset: 0x0058629C
		public event Action OnStartTakingInput;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600281E RID: 10270 RVA: 0x005880D4 File Offset: 0x005862D4
		// (remove) Token: 0x0600281F RID: 10271 RVA: 0x0058810C File Offset: 0x0058630C
		public event Action OnEndTakingInput;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002820 RID: 10272 RVA: 0x00588144 File Offset: 0x00586344
		// (remove) Token: 0x06002821 RID: 10273 RVA: 0x0058817C File Offset: 0x0058637C
		public event Action OnCanceledTakingInput;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002822 RID: 10274 RVA: 0x005881B4 File Offset: 0x005863B4
		// (remove) Token: 0x06002823 RID: 10275 RVA: 0x005881EC File Offset: 0x005863EC
		public event Action OnNeedingVirtualKeyboard;

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x00588221 File Offset: 0x00586421
		public bool HasContents
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.actualContents);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00588231 File Offset: 0x00586431
		public bool IsWritingText
		{
			get
			{
				return this.isWritingText;
			}
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0058823C File Offset: 0x0058643C
		public UISearchBar(LocalizedText emptyContentText, float scale)
		{
			this._textToShowWhenEmpty = emptyContentText;
			this._textScale = scale;
			UITextBox uitextBox = new UITextBox("", scale, false)
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
			uitextBox.SetTextMaxLength(50);
			base.Append(uitextBox);
			this._text = uitextBox;
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x005882E8 File Offset: 0x005864E8
		public void SetContents(string contents, bool forced = false)
		{
			if (this.actualContents == contents && !forced)
			{
				return;
			}
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

		// Token: 0x06002828 RID: 10280 RVA: 0x00588398 File Offset: 0x00586598
		public void TrimDisplayIfOverElementDimensions(int padding)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			if (dimensions.Width == 0f && dimensions.Height == 0f)
			{
				return;
			}
			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width, point.Y + (int)dimensions.Height);
			Rectangle rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			CalculatedStyle dimensions2 = this._text.GetDimensions();
			Point point3 = new Point((int)dimensions2.X, (int)dimensions2.Y);
			Point point4 = new Point(point3.X + (int)this._text.MinWidth.Pixels, point3.Y + (int)this._text.MinHeight.Pixels);
			Rectangle rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			int num = 0;
			while (rectangle2.Right > rectangle.Right - padding && this._text.Text.Length > 0)
			{
				this._text.SetText(this._text.Text.Substring(0, this._text.Text.Length - 1));
				num++;
				this.RecalculateChildren();
				dimensions2 = this._text.GetDimensions();
				point3 = new Point((int)dimensions2.X, (int)dimensions2.Y);
				point4 = new Point(point3.X + (int)this._text.MinWidth.Pixels, point3.Y + (int)this._text.MinHeight.Pixels);
				rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
				this.actualContents = this._text.Text;
			}
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x005885C7 File Offset: 0x005867C7
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x005885D0 File Offset: 0x005867D0
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x005885EE File Offset: 0x005867EE
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

		// Token: 0x0600282C RID: 10284 RVA: 0x0057FF98 File Offset: 0x0057E198
		private bool NeedsVirtualkeyboard()
		{
			return PlayerInput.SettingsForUI.ShowGamepadHints;
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x00588628 File Offset: 0x00586828
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (!this.isWritingText)
			{
				return;
			}
			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			Vector2 position = new Vector2((float)(Main.screenWidth / 2), (float)(this._text.GetDimensions().ToRectangle().Bottom + 32));
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
			position = new Vector2((float)(Main.screenWidth / 2), (float)(this._text.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.DrawWindowsIMEPanel(position, 0.5f);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00588718 File Offset: 0x00586918
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

		// Token: 0x04004B43 RID: 19267
		private readonly LocalizedText _textToShowWhenEmpty;

		// Token: 0x04004B44 RID: 19268
		private UITextBox _text;

		// Token: 0x04004B45 RID: 19269
		private string actualContents;

		// Token: 0x04004B46 RID: 19270
		private float _textScale;

		// Token: 0x04004B47 RID: 19271
		private bool isWritingText;
	}
}
