using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004E1 RID: 1249
	public class UIVirtualKeyboard : UIState
	{
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x005BF600 File Offset: 0x005BD800
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x005BF60D File Offset: 0x005BD80D
		public string Text
		{
			get
			{
				return this._textBox.Text;
			}
			set
			{
				this._textBox.SetText(value);
				this.ValidateText();
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06003C3E RID: 15422 RVA: 0x005BF621 File Offset: 0x005BD821
		// (set) Token: 0x06003C3F RID: 15423 RVA: 0x005BF62E File Offset: 0x005BD82E
		public bool HideContents
		{
			get
			{
				return this._textBox.HideContents;
			}
			set
			{
				this._textBox.HideContents = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x005BF63C File Offset: 0x005BD83C
		public static bool CanSubmit
		{
			get
			{
				return UIVirtualKeyboard._currentInstance != null && UIVirtualKeyboard._currentInstance._canSubmit;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x005BF651 File Offset: 0x005BD851
		public static int KeyboardContext
		{
			get
			{
				if (UIVirtualKeyboard._currentInstance == null)
				{
					return -1;
				}
				return UIVirtualKeyboard._currentInstance._keyboardContext;
			}
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x005BF668 File Offset: 0x005BD868
		public UIVirtualKeyboard(string labelText, string startingText, UIVirtualKeyboard.KeyboardSubmitEvent submitAction, Action cancelAction, int inputMode = 0, bool allowEmpty = false)
		{
			this._keyboardContext = inputMode;
			this._allowEmpty = allowEmpty;
			UIVirtualKeyboard.OffsetDown = 0;
			UIVirtualKeyboard.ShouldHideText = false;
			this._lastOffsetDown = 0;
			this._edittingSign = (this._keyboardContext == 1);
			this._edittingChest = (this._keyboardContext == 2);
			UIVirtualKeyboard._currentInstance = this;
			this._submitAction = submitAction;
			this._cancelAction = cancelAction;
			this._textureShift = Main.Assets.Request<Texture2D>("Images/UI/VK_Shift");
			this._textureBackspace = Main.Assets.Request<Texture2D>("Images/UI/VK_Backspace");
			this.Top.Pixels = (float)this._lastOffsetDown;
			float num3 = (float)(-5000 * this._edittingSign.ToInt());
			float num4 = 270f;
			float num5 = 0f;
			float num6 = 516f;
			UIElement uIElement = new UIElement();
			uIElement.Width.Pixels = num6 + 8f + 16f;
			uIElement.Top.Precent = num5;
			uIElement.Top.Pixels = num4;
			uIElement.Height.Pixels = 266f;
			uIElement.HAlign = 0.5f;
			uIElement.SetPadding(0f);
			this.outerLayer1 = uIElement;
			UIElement uIElement2 = new UIElement();
			uIElement2.Width.Pixels = num6 + 8f + 16f;
			uIElement2.Top.Precent = num5;
			uIElement2.Top.Pixels = num4;
			uIElement2.Height.Pixels = 266f;
			uIElement2.HAlign = 0.5f;
			uIElement2.SetPadding(0f);
			this.outerLayer2 = uIElement2;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Precent = 1f;
			uipanel.Height.Pixels = 225f;
			uipanel.BackgroundColor = new Color(23, 33, 69) * 0.7f;
			UIPanel uIPanel = uipanel;
			uIElement.Append(uIPanel);
			float num7 = -50f;
			this._textBox = new UITextBox("", 0.78f, true);
			this._textBox.BackgroundColor = Color.Transparent;
			this._textBox.BorderColor = Color.Transparent;
			this._textBox.HAlign = 0.5f;
			this._textBox.Width.Pixels = num6;
			this._textBox.Top.Pixels = num7 + num4 - 10f + num3;
			this._textBox.Top.Precent = num5;
			this._textBox.Height.Pixels = 37f;
			base.Append(this._textBox);
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int index = j * 10 + i;
					UITextPanel<object> uITextPanel = this.CreateKeyboardButton("1234567890qwertyuiopasdfghjkl'zxcvbnm,.?"[index].ToString(), i, j, 1, true);
					uITextPanel.OnLeftClick += this.TypeText;
					uIPanel.Append(uITextPanel);
				}
			}
			this._shiftButton = this.CreateKeyboardButton("", 0, 4, 1, false);
			this._shiftButton.PaddingLeft = 0f;
			this._shiftButton.PaddingRight = 0f;
			this._shiftButton.PaddingBottom = (this._shiftButton.PaddingTop = 0f);
			this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
			this._shiftButton.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this._shiftButton.BorderColor = this._internalBorderColorSelected;
				if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
				{
					this._shiftButton.BackgroundColor = new Color(73, 94, 171);
				}
			};
			this._shiftButton.OnMouseOut += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
				if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
				{
					this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			};
			this._shiftButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.SetKeyState((this._keyState != UIVirtualKeyboard.KeyState.Shift) ? UIVirtualKeyboard.KeyState.Shift : UIVirtualKeyboard.KeyState.Default);
			};
			UIImage element = new UIImage(this._textureShift)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				ImageScale = 0.85f
			};
			this._shiftButton.Append(element);
			uIPanel.Append(this._shiftButton);
			this._symbolButton = this.CreateKeyboardButton("@%", 1, 4, 1, false);
			this._symbolButton.PaddingLeft = 0f;
			this._symbolButton.PaddingRight = 0f;
			this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
			this._symbolButton.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this._symbolButton.BorderColor = this._internalBorderColorSelected;
				if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
				{
					this._symbolButton.BackgroundColor = new Color(73, 94, 171);
				}
			};
			this._symbolButton.OnMouseOut += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
				if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
				{
					this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			};
			this._symbolButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.SetKeyState((this._keyState != UIVirtualKeyboard.KeyState.Symbol) ? UIVirtualKeyboard.KeyState.Symbol : UIVirtualKeyboard.KeyState.Default);
			};
			uIPanel.Append(this._symbolButton);
			this.BuildSpaceBarArea(uIPanel);
			this._submitButton = new UITextPanel<LocalizedText>((this._edittingSign || this._edittingChest) ? Language.GetText("UI.Save") : Language.GetText("UI.Submit"), 0.4f, true);
			this._submitButton.Height.Pixels = 37f;
			this._submitButton.Width.Precent = 0.4f;
			this._submitButton.HAlign = 1f;
			this._submitButton.VAlign = 1f;
			this._submitButton.PaddingLeft = 0f;
			this._submitButton.PaddingRight = 0f;
			this.ValidateText();
			this._submitButton.OnMouseOver += this.FadedMouseOver;
			this._submitButton.OnMouseOut += this.FadedMouseOut;
			this._submitButton.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this.ValidateText();
			};
			this._submitButton.OnMouseOut += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this.ValidateText();
			};
			this._submitButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				UIVirtualKeyboard.Submit();
			};
			uIElement.Append(this._submitButton);
			this._cancelButton = new UITextPanel<LocalizedText>(Language.GetText("UI.Cancel"), 0.4f, true);
			this.StyleKey<LocalizedText>(this._cancelButton, true);
			this._cancelButton.Height.Pixels = 37f;
			this._cancelButton.Width.Precent = 0.4f;
			this._cancelButton.VAlign = 1f;
			this._cancelButton.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				this._cancelAction();
			};
			this._cancelButton.OnMouseOver += this.FadedMouseOver;
			this._cancelButton.OnMouseOut += this.FadedMouseOut;
			uIElement.Append(this._cancelButton);
			this._submitButton2 = new UITextPanel<LocalizedText>((this._edittingSign || this._edittingChest) ? Language.GetText("UI.Save") : Language.GetText("UI.Submit"), 0.72f, true);
			this._submitButton2.TextColor = Color.Silver;
			this._submitButton2.DrawPanel = false;
			this._submitButton2.Height.Pixels = 60f;
			this._submitButton2.Width.Precent = 0.4f;
			this._submitButton2.HAlign = 0.5f;
			this._submitButton2.VAlign = 0f;
			this._submitButton2.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.85f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.White;
			};
			this._submitButton2.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.72f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.Silver;
			};
			this._submitButton2.Top.Pixels = 50f;
			this._submitButton2.PaddingLeft = 0f;
			this._submitButton2.PaddingRight = 0f;
			this.ValidateText();
			this._submitButton2.OnMouseOver += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this.ValidateText();
			};
			this._submitButton2.OnMouseOut += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				this.ValidateText();
			};
			this._submitButton2.OnMouseOver += this.FadedMouseOver;
			this._submitButton2.OnMouseOut += this.FadedMouseOut;
			this._submitButton2.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				if (this.TextIsValidForSubmit())
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					this._submitAction(this.Text.Trim());
				}
			};
			this.outerLayer2.Append(this._submitButton2);
			this._cancelButton2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Cancel"), 0.72f, true);
			this._cancelButton2.TextColor = Color.Silver;
			this._cancelButton2.DrawPanel = false;
			this._cancelButton2.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.85f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.White;
			};
			this._cancelButton2.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.72f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.Silver;
			};
			this._cancelButton2.Height.Pixels = 60f;
			this._cancelButton2.Width.Precent = 0.4f;
			this._cancelButton2.Top.Pixels = 114f;
			this._cancelButton2.VAlign = 0f;
			this._cancelButton2.HAlign = 0.5f;
			this._cancelButton2.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				this._cancelAction();
			};
			this.outerLayer2.Append(this._cancelButton2);
			UITextPanel<object> uITextPanel2 = this.CreateKeyboardButton("", 8, 4, 2, true);
			uITextPanel2.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this._textBox.Backspace();
				this.ValidateText();
			};
			uITextPanel2.PaddingLeft = 0f;
			uITextPanel2.PaddingRight = 0f;
			uITextPanel2.PaddingBottom = (uITextPanel2.PaddingTop = 0f);
			uITextPanel2.Append(new UIImage(this._textureBackspace)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				ImageScale = 0.92f
			});
			uIPanel.Append(uITextPanel2);
			UIText uitext = new UIText(labelText, 0.75f, true);
			uitext.HAlign = 0.5f;
			uitext.Width.Pixels = num6;
			uitext.Top.Pixels = num7 - 37f - 4f + num4 + num3;
			uitext.Top.Precent = num5;
			uitext.Height.Pixels = 37f;
			UIText uIText = uitext;
			base.Append(uIText);
			this._label = uIText;
			base.Append(uIElement);
			int textMaxLength = this._edittingSign ? 1200 : (this._edittingChest ? 63 : 20);
			this._textBox.SetTextMaxLength(textMaxLength);
			this.Text = startingText;
			if (this.Text.Length == 0)
			{
				this.SetKeyState(UIVirtualKeyboard.KeyState.Shift);
			}
			UIVirtualKeyboard.ShouldHideText = true;
			UIVirtualKeyboard.OffsetDown = 9999;
			this.UpdateOffsetDown();
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x005C01A1 File Offset: 0x005BE3A1
		public void SetMaxInputLength(int length)
		{
			this._textBox.SetTextMaxLength(length);
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x005C01B0 File Offset: 0x005BE3B0
		private void BuildSpaceBarArea(UIPanel mainPanel)
		{
			Action createTheseTwo = delegate()
			{
				bool flag = this.CanRestore();
				int x = flag ? 4 : 5;
				bool edittingSign = this._edittingSign;
				int num = (flag && edittingSign) ? 2 : 3;
				UITextPanel<object> uITextPanel = this.CreateKeyboardButton(Language.GetText("UI.SpaceButton"), 2, 4, (this._edittingSign || (this._edittingChest && flag)) ? num : 6, true);
				uITextPanel.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
				{
					this.PressSpace();
				};
				mainPanel.Append(uITextPanel);
				this._spacebarButton = uITextPanel;
				if (edittingSign)
				{
					UITextPanel<object> uITextPanel2 = this.CreateKeyboardButton(Language.GetText("UI.EnterButton"), x, 4, num, true);
					uITextPanel2.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this._textBox.Write("\n");
						this.ValidateText();
					};
					mainPanel.Append(uITextPanel2);
					this._enterButton = uITextPanel2;
				}
			};
			createTheseTwo();
			if (this.CanRestore())
			{
				UITextPanel<object> restoreBar = this.CreateKeyboardButton(Language.GetText("UI.RestoreButton"), 6, 4, 2, true);
				restoreBar.OnLeftClick += delegate(UIMouseEvent <p0>, UIElement <p1>)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					this.RestoreCanceledInput(this._keyboardContext);
					this.ValidateText();
					restoreBar.Remove();
					this._enterButton.Remove();
					this._spacebarButton.Remove();
					createTheseTwo();
				};
				mainPanel.Append(restoreBar);
				this._restoreButton = restoreBar;
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x005C0258 File Offset: 0x005BE458
		private void PressSpace()
		{
			string text = " ";
			if (this.CustomTextValidationForUpdate != null && !this.CustomTextValidationForUpdate(this.Text + text))
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._textBox.Write(text);
			this.ValidateText();
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x005C02C9 File Offset: 0x005BE4C9
		private bool CanRestore()
		{
			if (this._edittingSign)
			{
				return UIVirtualKeyboard._cancelCacheSign.Length > 0;
			}
			return this._edittingChest && UIVirtualKeyboard._cancelCacheChest.Length > 0;
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x005C02F8 File Offset: 0x005BE4F8
		private void TypeText(UIMouseEvent evt, UIElement listeningElement)
		{
			string text = ((UITextPanel<object>)listeningElement).Text;
			if (this.CustomTextValidationForUpdate != null && !this.CustomTextValidationForUpdate(this.Text + text))
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			bool flag = this.Text.Length == 0;
			this._textBox.Write(text);
			this.ValidateText();
			if (flag && this.Text.Length > 0 && this._keyState == UIVirtualKeyboard.KeyState.Shift)
			{
				this.SetKeyState(UIVirtualKeyboard.KeyState.Default);
			}
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x005C03A0 File Offset: 0x005BE5A0
		public void SetKeyState(UIVirtualKeyboard.KeyState keyState)
		{
			UITextPanel<object> uITextPanel = null;
			UIVirtualKeyboard.KeyState keyState2 = this._keyState;
			if (keyState2 != UIVirtualKeyboard.KeyState.Symbol)
			{
				if (keyState2 == UIVirtualKeyboard.KeyState.Shift)
				{
					uITextPanel = this._shiftButton;
				}
			}
			else
			{
				uITextPanel = this._symbolButton;
			}
			if (uITextPanel != null)
			{
				if (uITextPanel.IsMouseHovering)
				{
					uITextPanel.BackgroundColor = new Color(73, 94, 171);
				}
				else
				{
					uITextPanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			}
			string text = null;
			UITextPanel<object> uITextPanel2 = null;
			switch (keyState)
			{
			case UIVirtualKeyboard.KeyState.Default:
				text = "1234567890qwertyuiopasdfghjkl'zxcvbnm,.?";
				break;
			case UIVirtualKeyboard.KeyState.Symbol:
				text = "1234567890!@#$%^&*()-_+=/\\{}[]<>;:\"`|~£¥";
				uITextPanel2 = this._symbolButton;
				break;
			case UIVirtualKeyboard.KeyState.Shift:
				text = "1234567890QWERTYUIOPASDFGHJKL'ZXCVBNM,.?";
				uITextPanel2 = this._shiftButton;
				break;
			}
			for (int i = 0; i < text.Length; i++)
			{
				this._keyList[i].SetText(text[i].ToString());
			}
			this._keyState = keyState;
			if (uITextPanel2 != null)
			{
				uITextPanel2.BackgroundColor = new Color(93, 114, 191);
			}
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x005C049C File Offset: 0x005BE69C
		private void ValidateText()
		{
			if (this.TextIsValidForSubmit())
			{
				this._canSubmit = true;
				this._submitButton.TextColor = Color.White;
				if (this._submitButton.IsMouseHovering)
				{
					this._submitButton.BackgroundColor = new Color(73, 94, 171);
					return;
				}
				this._submitButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				return;
			}
			else
			{
				this._canSubmit = false;
				this._submitButton.TextColor = Color.Gray;
				if (this._submitButton.IsMouseHovering)
				{
					this._submitButton.BackgroundColor = new Color(180, 60, 60) * 0.85f;
					return;
				}
				this._submitButton.BackgroundColor = new Color(150, 40, 40) * 0.85f;
				return;
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x005C0580 File Offset: 0x005BE780
		private bool TextIsValidForSubmit()
		{
			if (this.CustomTextValidationForUpdate != null)
			{
				return this.CustomTextValidationForUpdate(this.Text);
			}
			return this.Text.Trim().Length > 0 || this._edittingSign || this._edittingChest || this._allowEmpty;
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x005C05D4 File Offset: 0x005BE7D4
		private void StyleKey<T>(UITextPanel<T> button, bool external = false)
		{
			button.PaddingLeft = 0f;
			button.PaddingRight = 0f;
			button.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			if (!external)
			{
				button.BorderColor = this._internalBorderColor * 0.7f;
			}
			button.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				((UITextPanel<T>)listeningElement).BackgroundColor = new Color(73, 94, 171) * 0.85f;
				if (!external)
				{
					((UITextPanel<T>)listeningElement).BorderColor = this._internalBorderColorSelected * 0.85f;
				}
			};
			button.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				((UITextPanel<T>)listeningElement).BackgroundColor = new Color(63, 82, 151) * 0.7f;
				if (!external)
				{
					((UITextPanel<T>)listeningElement).BorderColor = this._internalBorderColor * 0.7f;
				}
			};
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x005C066C File Offset: 0x005BE86C
		private UITextPanel<object> CreateKeyboardButton(object text, int x, int y, int width = 1, bool style = true)
		{
			float num = 516f;
			UITextPanel<object> uITextPanel = new UITextPanel<object>(text, 0.4f, true);
			uITextPanel.Width.Pixels = 48f * (float)width + 4f * (float)(width - 1);
			uITextPanel.Height.Pixels = 37f;
			uITextPanel.Left.Precent = 0.5f;
			uITextPanel.Left.Pixels = 52f * (float)x - num * 0.5f;
			uITextPanel.Top.Pixels = 41f * (float)y;
			if (style)
			{
				this.StyleKey<object>(uITextPanel, false);
			}
			for (int i = 0; i < width; i++)
			{
				this._keyList[y * 10 + x + i] = uITextPanel;
			}
			return uITextPanel;
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x005C0724 File Offset: 0x005BE924
		private bool ShouldShowKeyboard()
		{
			return PlayerInput.SettingsForUI.ShowGamepadHints;
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x005C072C File Offset: 0x005BE92C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu)
			{
				if (this.ShouldShowKeyboard())
				{
					this.outerLayer2.Remove();
					if (!this.Elements.Contains(this.outerLayer1))
					{
						base.Append(this.outerLayer1);
					}
					this.outerLayer1.Activate();
					this.outerLayer2.Deactivate();
					this.Recalculate();
					this.RecalculateChildren();
					if (this._labelHeight != 0f)
					{
						this._textBox.Top.Pixels = this._textBoxHeight;
						this._label.Top.Pixels = this._labelHeight;
						this._textBox.Recalculate();
						this._label.Recalculate();
						this._labelHeight = (this._textBoxHeight = 0f);
						UserInterface.ActiveInstance.ResetLasts();
					}
				}
				else
				{
					this.outerLayer1.Remove();
					if (!this.Elements.Contains(this.outerLayer2))
					{
						base.Append(this.outerLayer2);
					}
					this.outerLayer2.Activate();
					this.outerLayer1.Deactivate();
					this.Recalculate();
					this.RecalculateChildren();
					if (this._textBoxHeight == 0f)
					{
						this._textBoxHeight = this._textBox.Top.Pixels;
						this._labelHeight = this._label.Top.Pixels;
						UITextBox textBox = this._textBox;
						textBox.Top.Pixels = textBox.Top.Pixels + 50f;
						UIText label = this._label;
						label.Top.Pixels = label.Top.Pixels + 50f;
						this._textBox.Recalculate();
						this._label.Recalculate();
						UserInterface.ActiveInstance.ResetLasts();
					}
				}
			}
			if (!Main.editSign && this._edittingSign)
			{
				IngameFancyUI.Close();
				return;
			}
			if (!Main.editChest && this._edittingChest)
			{
				IngameFancyUI.Close();
				return;
			}
			base.DrawSelf(spriteBatch);
			this.UpdateOffsetDown();
			UIVirtualKeyboard.OffsetDown = 0;
			UIVirtualKeyboard.ShouldHideText = false;
			this.SetupGamepadPoints(spriteBatch);
			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			Vector2 position;
			position..ctor((float)(Main.screenWidth / 2), (float)(this._textBox.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.DrawWindowsIMEPanel(position, 0.5f);
			string text = Main.GetInputText(this.Text, this._edittingSign);
			if (this._edittingSign && Main.inputTextEnter)
			{
				text += "\n";
			}
			else
			{
				if (this._edittingChest && Main.inputTextEnter)
				{
					ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
					IngameFancyUI.Close();
					return;
				}
				if (Main.inputTextEnter && UIVirtualKeyboard.CanSubmit)
				{
					UIVirtualKeyboard.Submit();
				}
				else if (this._edittingChest && Main.player[Main.myPlayer].chest < 0)
				{
					ChestUI.RenameChestCancel();
				}
				else if (Main.inputTextEscape && this.TryEscapingMenu())
				{
					return;
				}
			}
			if (IngameFancyUI.CanShowVirtualKeyboard(this._keyboardContext))
			{
				if (text != this.Text)
				{
					if (this.CustomTextValidationForUpdate == null || this.CustomTextValidationForUpdate(text))
					{
						this.Text = text;
					}
					else
					{
						SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
				}
				if (this._edittingSign)
				{
					this.CopyTextToSign();
				}
				if (this._edittingChest)
				{
					this.CopyTextToChest();
				}
			}
			byte b = (byte.MaxValue + Main.tileColor.R * 2) / 3;
			Color value;
			value..ctor((int)b, (int)b, (int)b, 255);
			this._textBox.TextColor = Color.Lerp(Color.White, value, 0.2f);
			this._label.TextColor = Color.Lerp(Color.White, value, 0.2f);
			position..ctor((float)(Main.screenWidth / 2), (float)(this._textBox.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.DrawWindowsIMEPanel(position, 0.5f);
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x005C0B24 File Offset: 0x005BED24
		private bool TryEscapingMenu()
		{
			if (this.CustomEscapeAttempt != null)
			{
				return this.CustomEscapeAttempt();
			}
			if (this._edittingSign)
			{
				Main.InputTextSignCancel();
			}
			if (this._edittingChest)
			{
				ChestUI.RenameChestCancel();
			}
			if (Main.gameMenu && this._cancelAction != null)
			{
				UIVirtualKeyboard.Cancel();
			}
			else
			{
				IngameFancyUI.Close();
			}
			return true;
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x005C0B7C File Offset: 0x005BED7C
		private void UpdateOffsetDown()
		{
			this._textBox.HideSelf = UIVirtualKeyboard.ShouldHideText;
			int num = UIVirtualKeyboard.OffsetDown - this._lastOffsetDown;
			int num2 = num;
			if (Math.Abs(num) < 10)
			{
				num2 = num;
			}
			this._lastOffsetDown += num2;
			if (num2 != 0)
			{
				this.Top.Pixels = this.Top.Pixels + (float)num2;
				this.Recalculate();
			}
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x005C0BDC File Offset: 0x005BEDDC
		public override void OnActivate()
		{
			if (PlayerInput.UsingGamepadUI && this._restoreButton != null)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x005C0BF7 File Offset: 0x005BEDF7
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			PlayerInput.WritingText = false;
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x005C0C0C File Offset: 0x005BEE0C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 6;
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 1;
			int num = 3002;
			int num2 = 5;
			int num3 = 10;
			int num4 = num3 * num2 - 1;
			int num5 = num3 * (num2 - 1);
			UILinkPointNavigator.SetPosition(3000, this._cancelButton.GetDimensions().Center());
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[3000];
			uILinkPoint.Unlink();
			uILinkPoint.Right = 3001;
			uILinkPoint.Up = num + num5;
			UILinkPointNavigator.SetPosition(3001, this._submitButton.GetDimensions().Center());
			uILinkPoint = UILinkPointNavigator.Points[3001];
			uILinkPoint.Unlink();
			uILinkPoint.Left = 3000;
			uILinkPoint.Up = num + num4;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					int num6 = i * num3 + j;
					int num7 = num + num6;
					if (this._keyList[num6] != null)
					{
						UILinkPointNavigator.SetPosition(num7, this._keyList[num6].GetDimensions().Center());
						uILinkPoint = UILinkPointNavigator.Points[num7];
						uILinkPoint.Unlink();
						int num8 = j - 1;
						while (num8 >= 0 && this._keyList[i * num3 + num8] == this._keyList[num6])
						{
							num8--;
						}
						if (num8 != -1)
						{
							uILinkPoint.Left = i * num3 + num8 + num;
						}
						else
						{
							uILinkPoint.Left = i * num3 + (num3 - 1) + num;
						}
						int k = j + 1;
						while (k <= num3 - 1 && this._keyList[i * num3 + k] == this._keyList[num6])
						{
							k++;
						}
						if (k != num3 && this._keyList[num6] != this._keyList[k])
						{
							uILinkPoint.Right = i * num3 + k + num;
						}
						else
						{
							uILinkPoint.Right = i * num3 + num;
						}
						if (i != 0)
						{
							uILinkPoint.Up = num7 - num3;
						}
						if (i != num2 - 1)
						{
							uILinkPoint.Down = num7 + num3;
						}
						else
						{
							uILinkPoint.Down = ((j < num2) ? 3000 : 3001);
						}
					}
				}
			}
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x005C0E48 File Offset: 0x005BF048
		public static void CycleSymbols()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				switch (UIVirtualKeyboard._currentInstance._keyState)
				{
				case UIVirtualKeyboard.KeyState.Default:
					UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Shift);
					return;
				case UIVirtualKeyboard.KeyState.Symbol:
					UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Default);
					break;
				case UIVirtualKeyboard.KeyState.Shift:
					UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Symbol);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x005C0E9D File Offset: 0x005BF09D
		public static void BackSpace()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				UIVirtualKeyboard._currentInstance._textBox.Backspace();
				UIVirtualKeyboard._currentInstance.ValidateText();
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x005C0ED4 File Offset: 0x005BF0D4
		public static void Submit()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				UIVirtualKeyboard._currentInstance.InternalSubmit();
			}
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x005C0EE8 File Offset: 0x005BF0E8
		private void InternalSubmit()
		{
			string text = this.Text.Trim();
			if (this.TextIsValidForSubmit())
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				this._submitAction(text);
			}
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x005C0F2A File Offset: 0x005BF12A
		public static void Cancel()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				UIVirtualKeyboard._currentInstance._cancelAction();
			}
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x005C0F58 File Offset: 0x005BF158
		public static void Write(string text)
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				bool flag = UIVirtualKeyboard._currentInstance.Text.Length == 0;
				UIVirtualKeyboard._currentInstance._textBox.Write(text);
				UIVirtualKeyboard._currentInstance.ValidateText();
				if (flag && UIVirtualKeyboard._currentInstance.Text.Length > 0 && UIVirtualKeyboard._currentInstance._keyState == UIVirtualKeyboard.KeyState.Shift)
				{
					UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Default);
				}
			}
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x005C0FD9 File Offset: 0x005BF1D9
		public static void CursorLeft()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				UIVirtualKeyboard._currentInstance._textBox.CursorLeft();
			}
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x005C1006 File Offset: 0x005BF206
		public static void CursorRight()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				UIVirtualKeyboard._currentInstance._textBox.CursorRight();
			}
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x005C1033 File Offset: 0x005BF233
		public static bool CanDisplay(int keyboardContext)
		{
			return keyboardContext != 1 || Main.screenHeight > 700;
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x005C1047 File Offset: 0x005BF247
		public static void CacheCanceledInput(int cacheMode)
		{
			if (cacheMode == 1)
			{
				UIVirtualKeyboard._cancelCacheSign = Main.npcChatText;
			}
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x005C1057 File Offset: 0x005BF257
		private void RestoreCanceledInput(int cacheMode)
		{
			if (cacheMode == 1)
			{
				Main.npcChatText = UIVirtualKeyboard._cancelCacheSign;
				this.Text = Main.npcChatText;
				UIVirtualKeyboard._cancelCacheSign = "";
			}
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x005C107C File Offset: 0x005BF27C
		private void CopyTextToSign()
		{
			if (this._edittingSign)
			{
				int sign = Main.player[Main.myPlayer].sign;
				if (sign >= 0 && Main.sign[sign] != null)
				{
					Main.npcChatText = this.Text;
				}
			}
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x005C10BA File Offset: 0x005BF2BA
		private void CopyTextToChest()
		{
			if (this._edittingChest)
			{
				Main.npcChatText = this.Text;
			}
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x005C10D0 File Offset: 0x005BF2D0
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x005C1125 File Offset: 0x005BF325
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x04005587 RID: 21895
		private static UIVirtualKeyboard _currentInstance;

		// Token: 0x04005588 RID: 21896
		private static string _cancelCacheSign = "";

		// Token: 0x04005589 RID: 21897
		private static string _cancelCacheChest = "";

		// Token: 0x0400558A RID: 21898
		private const string DEFAULT_KEYS = "1234567890qwertyuiopasdfghjkl'zxcvbnm,.?";

		// Token: 0x0400558B RID: 21899
		private const string SHIFT_KEYS = "1234567890QWERTYUIOPASDFGHJKL'ZXCVBNM,.?";

		// Token: 0x0400558C RID: 21900
		private const string SYMBOL_KEYS = "1234567890!@#$%^&*()-_+=/\\{}[]<>;:\"`|~£¥";

		// Token: 0x0400558D RID: 21901
		private const float KEY_SPACING = 4f;

		// Token: 0x0400558E RID: 21902
		private const float KEY_WIDTH = 48f;

		// Token: 0x0400558F RID: 21903
		private const float KEY_HEIGHT = 37f;

		// Token: 0x04005590 RID: 21904
		private UITextPanel<object>[] _keyList = new UITextPanel<object>[50];

		// Token: 0x04005591 RID: 21905
		private UITextPanel<object> _shiftButton;

		// Token: 0x04005592 RID: 21906
		private UITextPanel<object> _symbolButton;

		// Token: 0x04005593 RID: 21907
		private UITextBox _textBox;

		// Token: 0x04005594 RID: 21908
		private UITextPanel<LocalizedText> _submitButton;

		// Token: 0x04005595 RID: 21909
		private UITextPanel<LocalizedText> _cancelButton;

		// Token: 0x04005596 RID: 21910
		private UIText _label;

		// Token: 0x04005597 RID: 21911
		private UITextPanel<object> _enterButton;

		// Token: 0x04005598 RID: 21912
		private UITextPanel<object> _spacebarButton;

		// Token: 0x04005599 RID: 21913
		private UITextPanel<object> _restoreButton;

		// Token: 0x0400559A RID: 21914
		private Asset<Texture2D> _textureShift;

		// Token: 0x0400559B RID: 21915
		private Asset<Texture2D> _textureBackspace;

		// Token: 0x0400559C RID: 21916
		private Color _internalBorderColor = new Color(89, 116, 213);

		// Token: 0x0400559D RID: 21917
		private Color _internalBorderColorSelected = Main.OurFavoriteColor;

		// Token: 0x0400559E RID: 21918
		private UITextPanel<LocalizedText> _submitButton2;

		// Token: 0x0400559F RID: 21919
		private UITextPanel<LocalizedText> _cancelButton2;

		// Token: 0x040055A0 RID: 21920
		private UIElement outerLayer1;

		// Token: 0x040055A1 RID: 21921
		private UIElement outerLayer2;

		// Token: 0x040055A2 RID: 21922
		private bool _allowEmpty;

		// Token: 0x040055A3 RID: 21923
		private UIVirtualKeyboard.KeyState _keyState;

		// Token: 0x040055A4 RID: 21924
		private UIVirtualKeyboard.KeyboardSubmitEvent _submitAction;

		// Token: 0x040055A5 RID: 21925
		private Action _cancelAction;

		// Token: 0x040055A6 RID: 21926
		private int _lastOffsetDown;

		// Token: 0x040055A7 RID: 21927
		public static int OffsetDown;

		// Token: 0x040055A8 RID: 21928
		public static bool ShouldHideText;

		// Token: 0x040055A9 RID: 21929
		private int _keyboardContext;

		// Token: 0x040055AA RID: 21930
		private bool _edittingSign;

		// Token: 0x040055AB RID: 21931
		private bool _edittingChest;

		// Token: 0x040055AC RID: 21932
		private float _textBoxHeight;

		// Token: 0x040055AD RID: 21933
		private float _labelHeight;

		// Token: 0x040055AE RID: 21934
		public Func<string, bool> CustomTextValidationForUpdate;

		// Token: 0x040055AF RID: 21935
		public Func<string, bool> CustomTextValidationForSubmit;

		// Token: 0x040055B0 RID: 21936
		public Func<bool> CustomEscapeAttempt;

		// Token: 0x040055B1 RID: 21937
		private bool _canSubmit;

		// Token: 0x02000BF6 RID: 3062
		// (Invoke) Token: 0x06005E90 RID: 24208
		public delegate void KeyboardSubmitEvent(string text);

		// Token: 0x02000BF7 RID: 3063
		public enum KeyState
		{
			// Token: 0x04007801 RID: 30721
			Default,
			// Token: 0x04007802 RID: 30722
			Symbol,
			// Token: 0x04007803 RID: 30723
			Shift
		}
	}
}
