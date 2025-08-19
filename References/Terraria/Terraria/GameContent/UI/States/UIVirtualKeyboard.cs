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
	// Token: 0x02000350 RID: 848
	public class UIVirtualKeyboard : UIState
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0057EE97 File Offset: 0x0057D097
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x0057EEA4 File Offset: 0x0057D0A4
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0057EEB8 File Offset: 0x0057D0B8
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x0057EEC5 File Offset: 0x0057D0C5
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

		// Token: 0x06002706 RID: 9990 RVA: 0x0057EED4 File Offset: 0x0057D0D4
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
			this._textureShift = Main.Assets.Request<Texture2D>("Images/UI/VK_Shift", 1);
			this._textureBackspace = Main.Assets.Request<Texture2D>("Images/UI/VK_Backspace", 1);
			this.Top.Pixels = (float)this._lastOffsetDown;
			float num = (float)(-5000 * this._edittingSign.ToInt());
			float num2 = 270f;
			float precent = 0f;
			float num3 = 516f;
			UIElement uielement = new UIElement();
			uielement.Width.Pixels = num3 + 8f + 16f;
			uielement.Top.Precent = precent;
			uielement.Top.Pixels = num2;
			uielement.Height.Pixels = 266f;
			uielement.HAlign = 0.5f;
			uielement.SetPadding(0f);
			this.outerLayer1 = uielement;
			UIElement uielement2 = new UIElement();
			uielement2.Width.Pixels = num3 + 8f + 16f;
			uielement2.Top.Precent = precent;
			uielement2.Top.Pixels = num2;
			uielement2.Height.Pixels = 266f;
			uielement2.HAlign = 0.5f;
			uielement2.SetPadding(0f);
			this.outerLayer2 = uielement2;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Precent = 1f;
			uipanel.Height.Pixels = 225f;
			uipanel.BackgroundColor = new Color(23, 33, 69) * 0.7f;
			uielement.Append(uipanel);
			float num4 = -50f;
			this._textBox = new UITextBox("", 0.78f, true);
			this._textBox.BackgroundColor = Color.Transparent;
			this._textBox.BorderColor = Color.Transparent;
			this._textBox.HAlign = 0.5f;
			this._textBox.Width.Pixels = num3;
			this._textBox.Top.Pixels = num4 + num2 - 10f + num;
			this._textBox.Top.Precent = precent;
			this._textBox.Height.Pixels = 37f;
			base.Append(this._textBox);
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int index = j * 10 + i;
					UITextPanel<object> uitextPanel = this.CreateKeyboardButton("1234567890qwertyuiopasdfghjkl'zxcvbnm,.?"[index].ToString(), i, j, 1, true);
					uitextPanel.OnLeftClick += this.TypeText;
					uipanel.Append(uitextPanel);
				}
			}
			this._shiftButton = this.CreateKeyboardButton("", 0, 4, 1, false);
			this._shiftButton.PaddingLeft = 0f;
			this._shiftButton.PaddingRight = 0f;
			this._shiftButton.PaddingBottom = (this._shiftButton.PaddingTop = 0f);
			this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
			this._shiftButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._shiftButton.BorderColor = this._internalBorderColorSelected;
				if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
				{
					this._shiftButton.BackgroundColor = new Color(73, 94, 171);
				}
			};
			this._shiftButton.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
				if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
				{
					this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			};
			this._shiftButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.SetKeyState((this._keyState == UIVirtualKeyboard.KeyState.Shift) ? UIVirtualKeyboard.KeyState.Default : UIVirtualKeyboard.KeyState.Shift);
			};
			UIImage uiimage = new UIImage(this._textureShift);
			uiimage.HAlign = 0.5f;
			uiimage.VAlign = 0.5f;
			uiimage.ImageScale = 0.85f;
			this._shiftButton.Append(uiimage);
			uipanel.Append(this._shiftButton);
			this._symbolButton = this.CreateKeyboardButton("@%", 1, 4, 1, false);
			this._symbolButton.PaddingLeft = 0f;
			this._symbolButton.PaddingRight = 0f;
			this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
			this._symbolButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._symbolButton.BorderColor = this._internalBorderColorSelected;
				if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
				{
					this._symbolButton.BackgroundColor = new Color(73, 94, 171);
				}
			};
			this._symbolButton.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
				if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
				{
					this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			};
			this._symbolButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.SetKeyState((this._keyState == UIVirtualKeyboard.KeyState.Symbol) ? UIVirtualKeyboard.KeyState.Default : UIVirtualKeyboard.KeyState.Symbol);
			};
			uipanel.Append(this._symbolButton);
			this.BuildSpaceBarArea(uipanel);
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
			this._submitButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				UIVirtualKeyboard.Submit();
			};
			uielement.Append(this._submitButton);
			this._cancelButton = new UITextPanel<LocalizedText>(Language.GetText("UI.Cancel"), 0.4f, true);
			this.StyleKey<LocalizedText>(this._cancelButton, true);
			this._cancelButton.Height.Pixels = 37f;
			this._cancelButton.Width.Precent = 0.4f;
			this._cancelButton.VAlign = 1f;
			this._cancelButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				this._cancelAction();
			};
			this._cancelButton.OnMouseOver += this.FadedMouseOver;
			this._cancelButton.OnMouseOut += this.FadedMouseOut;
			uielement.Append(this._cancelButton);
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
			this._submitButton2.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton2.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton2.OnMouseOver += this.FadedMouseOver;
			this._submitButton2.OnMouseOut += this.FadedMouseOut;
			this._submitButton2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
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
			this._cancelButton2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				this._cancelAction();
			};
			this.outerLayer2.Append(this._cancelButton2);
			UITextPanel<object> uitextPanel2 = this.CreateKeyboardButton("", 8, 4, 2, true);
			uitextPanel2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this._textBox.Backspace();
				this.ValidateText();
			};
			uitextPanel2.PaddingLeft = 0f;
			uitextPanel2.PaddingRight = 0f;
			uitextPanel2.PaddingBottom = (uitextPanel2.PaddingTop = 0f);
			uitextPanel2.Append(new UIImage(this._textureBackspace)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				ImageScale = 0.92f
			});
			uipanel.Append(uitextPanel2);
			UIText uitext = new UIText(labelText, 0.75f, true);
			uitext.HAlign = 0.5f;
			uitext.Width.Pixels = num3;
			uitext.Top.Pixels = num4 - 37f - 4f + num2 + num;
			uitext.Top.Precent = precent;
			uitext.Height.Pixels = 37f;
			base.Append(uitext);
			this._label = uitext;
			base.Append(uielement);
			int textMaxLength = this._edittingSign ? 1200 : 20;
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

		// Token: 0x06002707 RID: 9991 RVA: 0x0057FA15 File Offset: 0x0057DC15
		public void SetMaxInputLength(int length)
		{
			this._textBox.SetTextMaxLength(length);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0057FA24 File Offset: 0x0057DC24
		private void BuildSpaceBarArea(UIPanel mainPanel)
		{
			UIElement.MouseEvent <>9__1;
			UIElement.MouseEvent <>9__2;
			Action createTheseTwo = delegate()
			{
				bool flag = this.CanRestore();
				int x = flag ? 4 : 5;
				bool edittingSign = this._edittingSign;
				int num = (flag && edittingSign) ? 2 : 3;
				UITextPanel<object> uitextPanel = this.CreateKeyboardButton(Language.GetText("UI.SpaceButton"), 2, 4, (this._edittingSign || (this._edittingChest && flag)) ? num : 6, true);
				UIElement uielement = uitextPanel;
				UIElement.MouseEvent value;
				if ((value = <>9__1) == null)
				{
					value = (<>9__1 = delegate(UIMouseEvent evt, UIElement listeningElement)
					{
						this.PressSpace();
					});
				}
				uielement.OnLeftClick += value;
				mainPanel.Append(uitextPanel);
				this._spacebarButton = uitextPanel;
				if (edittingSign)
				{
					UITextPanel<object> uitextPanel2 = this.CreateKeyboardButton(Language.GetText("UI.EnterButton"), x, 4, num, true);
					UIElement uielement2 = uitextPanel2;
					UIElement.MouseEvent value2;
					if ((value2 = <>9__2) == null)
					{
						value2 = (<>9__2 = delegate(UIMouseEvent evt, UIElement listeningElement)
						{
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							this._textBox.Write("\n");
							this.ValidateText();
						});
					}
					uielement2.OnLeftClick += value2;
					mainPanel.Append(uitextPanel2);
					this._enterButton = uitextPanel2;
				}
			};
			createTheseTwo();
			if (this.CanRestore())
			{
				UITextPanel<object> restoreBar = this.CreateKeyboardButton(Language.GetText("UI.RestoreButton"), 6, 4, 2, true);
				restoreBar.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
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

		// Token: 0x06002709 RID: 9993 RVA: 0x0057FACC File Offset: 0x0057DCCC
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

		// Token: 0x0600270A RID: 9994 RVA: 0x0057FB3D File Offset: 0x0057DD3D
		private bool CanRestore()
		{
			if (this._edittingSign)
			{
				return UIVirtualKeyboard._cancelCacheSign.Length > 0;
			}
			return this._edittingChest && UIVirtualKeyboard._cancelCacheChest.Length > 0;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0057FB6C File Offset: 0x0057DD6C
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

		// Token: 0x0600270C RID: 9996 RVA: 0x0057FC14 File Offset: 0x0057DE14
		public void SetKeyState(UIVirtualKeyboard.KeyState keyState)
		{
			UITextPanel<object> uitextPanel = null;
			UIVirtualKeyboard.KeyState keyState2 = this._keyState;
			if (keyState2 != UIVirtualKeyboard.KeyState.Symbol)
			{
				if (keyState2 == UIVirtualKeyboard.KeyState.Shift)
				{
					uitextPanel = this._shiftButton;
				}
			}
			else
			{
				uitextPanel = this._symbolButton;
			}
			if (uitextPanel != null)
			{
				if (uitextPanel.IsMouseHovering)
				{
					uitextPanel.BackgroundColor = new Color(73, 94, 171);
				}
				else
				{
					uitextPanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			}
			string text = null;
			UITextPanel<object> uitextPanel2 = null;
			switch (keyState)
			{
			case UIVirtualKeyboard.KeyState.Default:
				text = "1234567890qwertyuiopasdfghjkl'zxcvbnm,.?";
				break;
			case UIVirtualKeyboard.KeyState.Symbol:
				text = "1234567890!@#$%^&*()-_+=/\\{}[]<>;:\"`|~£¥";
				uitextPanel2 = this._symbolButton;
				break;
			case UIVirtualKeyboard.KeyState.Shift:
				text = "1234567890QWERTYUIOPASDFGHJKL'ZXCVBNM,.?";
				uitextPanel2 = this._shiftButton;
				break;
			}
			for (int i = 0; i < text.Length; i++)
			{
				this._keyList[i].SetText(text[i].ToString());
			}
			this._keyState = keyState;
			if (uitextPanel2 != null)
			{
				uitextPanel2.BackgroundColor = new Color(93, 114, 191);
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x0057FD10 File Offset: 0x0057DF10
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

		// Token: 0x0600270E RID: 9998 RVA: 0x0057FDF4 File Offset: 0x0057DFF4
		private bool TextIsValidForSubmit()
		{
			if (this.CustomTextValidationForUpdate != null)
			{
				return this.CustomTextValidationForUpdate(this.Text);
			}
			return this.Text.Trim().Length > 0 || this._edittingSign || this._edittingChest || this._allowEmpty;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0057FE48 File Offset: 0x0057E048
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

		// Token: 0x06002710 RID: 10000 RVA: 0x0057FEE0 File Offset: 0x0057E0E0
		private UITextPanel<object> CreateKeyboardButton(object text, int x, int y, int width = 1, bool style = true)
		{
			float num = 516f;
			UITextPanel<object> uitextPanel = new UITextPanel<object>(text, 0.4f, true);
			uitextPanel.Width.Pixels = 48f * (float)width + 4f * (float)(width - 1);
			uitextPanel.Height.Pixels = 37f;
			uitextPanel.Left.Precent = 0.5f;
			uitextPanel.Left.Pixels = 52f * (float)x - num * 0.5f;
			uitextPanel.Top.Pixels = 41f * (float)y;
			if (style)
			{
				this.StyleKey<object>(uitextPanel, false);
			}
			for (int i = 0; i < width; i++)
			{
				this._keyList[y * 10 + x + i] = uitextPanel;
			}
			return uitextPanel;
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0057FF98 File Offset: 0x0057E198
		private bool ShouldShowKeyboard()
		{
			return PlayerInput.SettingsForUI.ShowGamepadHints;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0057FFA0 File Offset: 0x0057E1A0
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
			Vector2 position = new Vector2((float)(Main.screenWidth / 2), (float)(this._textBox.GetDimensions().ToRectangle().Bottom + 32));
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
			Color value = new Color((int)b, (int)b, (int)b, 255);
			this._textBox.TextColor = Color.Lerp(Color.White, value, 0.2f);
			this._label.TextColor = Color.Lerp(Color.White, value, 0.2f);
			position = new Vector2((float)(Main.screenWidth / 2), (float)(this._textBox.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.DrawWindowsIMEPanel(position, 0.5f);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00580398 File Offset: 0x0057E598
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

		// Token: 0x06002714 RID: 10004 RVA: 0x005803F0 File Offset: 0x0057E5F0
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
			if (num2 == 0)
			{
				return;
			}
			this.Top.Pixels = this.Top.Pixels + (float)num2;
			this.Recalculate();
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00580451 File Offset: 0x0057E651
		public override void OnActivate()
		{
			if (PlayerInput.UsingGamepadUI && this._restoreButton != null)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0058046C File Offset: 0x0057E66C
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			PlayerInput.WritingText = false;
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00580480 File Offset: 0x0057E680
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
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[3000];
			uilinkPoint.Unlink();
			uilinkPoint.Right = 3001;
			uilinkPoint.Up = num + num5;
			UILinkPointNavigator.SetPosition(3001, this._submitButton.GetDimensions().Center());
			uilinkPoint = UILinkPointNavigator.Points[3001];
			uilinkPoint.Unlink();
			uilinkPoint.Left = 3000;
			uilinkPoint.Up = num + num4;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					int num6 = i * num3 + j;
					int num7 = num + num6;
					if (this._keyList[num6] != null)
					{
						UILinkPointNavigator.SetPosition(num7, this._keyList[num6].GetDimensions().Center());
						uilinkPoint = UILinkPointNavigator.Points[num7];
						uilinkPoint.Unlink();
						int num8 = j - 1;
						while (num8 >= 0 && this._keyList[i * num3 + num8] == this._keyList[num6])
						{
							num8--;
						}
						if (num8 != -1)
						{
							uilinkPoint.Left = i * num3 + num8 + num;
						}
						else
						{
							uilinkPoint.Left = i * num3 + (num3 - 1) + num;
						}
						int num9 = j + 1;
						while (num9 <= num3 - 1 && this._keyList[i * num3 + num9] == this._keyList[num6])
						{
							num9++;
						}
						if (num9 != num3 && this._keyList[num6] != this._keyList[num9])
						{
							uilinkPoint.Right = i * num3 + num9 + num;
						}
						else
						{
							uilinkPoint.Right = i * num3 + num;
						}
						if (i != 0)
						{
							uilinkPoint.Up = num7 - num3;
						}
						if (i != num2 - 1)
						{
							uilinkPoint.Down = num7 + num3;
						}
						else
						{
							uilinkPoint.Down = ((j < num2) ? 3000 : 3001);
						}
					}
				}
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x005806BC File Offset: 0x0057E8BC
		public static void CycleSymbols()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			switch (UIVirtualKeyboard._currentInstance._keyState)
			{
			case UIVirtualKeyboard.KeyState.Default:
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Shift);
				return;
			case UIVirtualKeyboard.KeyState.Symbol:
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Default);
				return;
			case UIVirtualKeyboard.KeyState.Shift:
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Symbol);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00580712 File Offset: 0x0057E912
		public static void BackSpace()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._textBox.Backspace();
			UIVirtualKeyboard._currentInstance.ValidateText();
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x0058074A File Offset: 0x0057E94A
		public static bool CanSubmit
		{
			get
			{
				return UIVirtualKeyboard._currentInstance != null && UIVirtualKeyboard._currentInstance._canSubmit;
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x0058075F File Offset: 0x0057E95F
		public static void Submit()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				UIVirtualKeyboard._currentInstance.InternalSubmit();
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00580774 File Offset: 0x0057E974
		private void InternalSubmit()
		{
			string text = this.Text.Trim();
			if (this.TextIsValidForSubmit())
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				this._submitAction(text);
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x005807B6 File Offset: 0x0057E9B6
		public static void Cancel()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._cancelAction();
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x005807E4 File Offset: 0x0057E9E4
		public static void Write(string text)
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			bool flag = UIVirtualKeyboard._currentInstance.Text.Length == 0;
			UIVirtualKeyboard._currentInstance._textBox.Write(text);
			UIVirtualKeyboard._currentInstance.ValidateText();
			if (flag && UIVirtualKeyboard._currentInstance.Text.Length > 0 && UIVirtualKeyboard._currentInstance._keyState == UIVirtualKeyboard.KeyState.Shift)
			{
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Default);
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00580866 File Offset: 0x0057EA66
		public static void CursorLeft()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._textBox.CursorLeft();
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00580894 File Offset: 0x0057EA94
		public static void CursorRight()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._textBox.CursorRight();
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x005808C2 File Offset: 0x0057EAC2
		public static bool CanDisplay(int keyboardContext)
		{
			return keyboardContext != 1 || Main.screenHeight > 700;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x005808D6 File Offset: 0x0057EAD6
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

		// Token: 0x06002723 RID: 10019 RVA: 0x005808EB File Offset: 0x0057EAEB
		public static void CacheCanceledInput(int cacheMode)
		{
			if (cacheMode == 1)
			{
				UIVirtualKeyboard._cancelCacheSign = Main.npcChatText;
			}
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x005808FB File Offset: 0x0057EAFB
		private void RestoreCanceledInput(int cacheMode)
		{
			if (cacheMode == 1)
			{
				Main.npcChatText = UIVirtualKeyboard._cancelCacheSign;
				this.Text = Main.npcChatText;
				UIVirtualKeyboard._cancelCacheSign = "";
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00580920 File Offset: 0x0057EB20
		private void CopyTextToSign()
		{
			if (!this._edittingSign)
			{
				return;
			}
			int sign = Main.player[Main.myPlayer].sign;
			if (sign < 0 || Main.sign[sign] == null)
			{
				return;
			}
			Main.npcChatText = this.Text;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00580960 File Offset: 0x0057EB60
		private void CopyTextToChest()
		{
			if (!this._edittingChest)
			{
				return;
			}
			Main.npcChatText = this.Text;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x00580978 File Offset: 0x0057EB78
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0056E4FD File Offset: 0x0056C6FD
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x04004A8D RID: 19085
		private static UIVirtualKeyboard _currentInstance;

		// Token: 0x04004A8E RID: 19086
		private static string _cancelCacheSign = "";

		// Token: 0x04004A8F RID: 19087
		private static string _cancelCacheChest = "";

		// Token: 0x04004A90 RID: 19088
		private const string DEFAULT_KEYS = "1234567890qwertyuiopasdfghjkl'zxcvbnm,.?";

		// Token: 0x04004A91 RID: 19089
		private const string SHIFT_KEYS = "1234567890QWERTYUIOPASDFGHJKL'ZXCVBNM,.?";

		// Token: 0x04004A92 RID: 19090
		private const string SYMBOL_KEYS = "1234567890!@#$%^&*()-_+=/\\{}[]<>;:\"`|~£¥";

		// Token: 0x04004A93 RID: 19091
		private const float KEY_SPACING = 4f;

		// Token: 0x04004A94 RID: 19092
		private const float KEY_WIDTH = 48f;

		// Token: 0x04004A95 RID: 19093
		private const float KEY_HEIGHT = 37f;

		// Token: 0x04004A96 RID: 19094
		private UITextPanel<object>[] _keyList = new UITextPanel<object>[50];

		// Token: 0x04004A97 RID: 19095
		private UITextPanel<object> _shiftButton;

		// Token: 0x04004A98 RID: 19096
		private UITextPanel<object> _symbolButton;

		// Token: 0x04004A99 RID: 19097
		private UITextBox _textBox;

		// Token: 0x04004A9A RID: 19098
		private UITextPanel<LocalizedText> _submitButton;

		// Token: 0x04004A9B RID: 19099
		private UITextPanel<LocalizedText> _cancelButton;

		// Token: 0x04004A9C RID: 19100
		private UIText _label;

		// Token: 0x04004A9D RID: 19101
		private UITextPanel<object> _enterButton;

		// Token: 0x04004A9E RID: 19102
		private UITextPanel<object> _spacebarButton;

		// Token: 0x04004A9F RID: 19103
		private UITextPanel<object> _restoreButton;

		// Token: 0x04004AA0 RID: 19104
		private Asset<Texture2D> _textureShift;

		// Token: 0x04004AA1 RID: 19105
		private Asset<Texture2D> _textureBackspace;

		// Token: 0x04004AA2 RID: 19106
		private Color _internalBorderColor = new Color(89, 116, 213);

		// Token: 0x04004AA3 RID: 19107
		private Color _internalBorderColorSelected = Main.OurFavoriteColor;

		// Token: 0x04004AA4 RID: 19108
		private UITextPanel<LocalizedText> _submitButton2;

		// Token: 0x04004AA5 RID: 19109
		private UITextPanel<LocalizedText> _cancelButton2;

		// Token: 0x04004AA6 RID: 19110
		private UIElement outerLayer1;

		// Token: 0x04004AA7 RID: 19111
		private UIElement outerLayer2;

		// Token: 0x04004AA8 RID: 19112
		private bool _allowEmpty;

		// Token: 0x04004AA9 RID: 19113
		private UIVirtualKeyboard.KeyState _keyState;

		// Token: 0x04004AAA RID: 19114
		private UIVirtualKeyboard.KeyboardSubmitEvent _submitAction;

		// Token: 0x04004AAB RID: 19115
		private Action _cancelAction;

		// Token: 0x04004AAC RID: 19116
		private int _lastOffsetDown;

		// Token: 0x04004AAD RID: 19117
		public static int OffsetDown;

		// Token: 0x04004AAE RID: 19118
		public static bool ShouldHideText;

		// Token: 0x04004AAF RID: 19119
		private int _keyboardContext;

		// Token: 0x04004AB0 RID: 19120
		private bool _edittingSign;

		// Token: 0x04004AB1 RID: 19121
		private bool _edittingChest;

		// Token: 0x04004AB2 RID: 19122
		private float _textBoxHeight;

		// Token: 0x04004AB3 RID: 19123
		private float _labelHeight;

		// Token: 0x04004AB4 RID: 19124
		public Func<string, bool> CustomTextValidationForUpdate;

		// Token: 0x04004AB5 RID: 19125
		public Func<string, bool> CustomTextValidationForSubmit;

		// Token: 0x04004AB6 RID: 19126
		public Func<bool> CustomEscapeAttempt;

		// Token: 0x04004AB7 RID: 19127
		private bool _canSubmit;

		// Token: 0x0200073D RID: 1853
		// (Invoke) Token: 0x0600385C RID: 14428
		public delegate void KeyboardSubmitEvent(string text);

		// Token: 0x0200073E RID: 1854
		public enum KeyState
		{
			// Token: 0x040063EC RID: 25580
			Default,
			// Token: 0x040063ED RID: 25581
			Symbol,
			// Token: 0x040063EE RID: 25582
			Shift
		}
	}
}
