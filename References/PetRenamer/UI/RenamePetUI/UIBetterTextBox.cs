using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.UI;

namespace PetRenamer.UI.RenamePetUI
{
	// Token: 0x0200000A RID: 10
	internal class UIBetterTextBox : UIPanel
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000073 RID: 115 RVA: 0x000039B8 File Offset: 0x00001BB8
		// (remove) Token: 0x06000074 RID: 116 RVA: 0x000039F0 File Offset: 0x00001BF0
		public event Action OnFocus;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000075 RID: 117 RVA: 0x00003A28 File Offset: 0x00001C28
		// (remove) Token: 0x06000076 RID: 118 RVA: 0x00003A60 File Offset: 0x00001C60
		public event Action OnUnfocus;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000077 RID: 119 RVA: 0x00003A98 File Offset: 0x00001C98
		// (remove) Token: 0x06000078 RID: 120 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public event Action OnTextChanged;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000079 RID: 121 RVA: 0x00003B08 File Offset: 0x00001D08
		// (remove) Token: 0x0600007A RID: 122 RVA: 0x00003B40 File Offset: 0x00001D40
		public event Action OnTabPressed;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600007B RID: 123 RVA: 0x00003B78 File Offset: 0x00001D78
		// (remove) Token: 0x0600007C RID: 124 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public event Action OnEnterPressed;

		// Token: 0x0600007D RID: 125 RVA: 0x00003BE8 File Offset: 0x00001DE8
		internal UIBetterTextBox(string hintText, string text = "")
		{
			this.hintText = hintText;
			this.currentString = text;
			base.SetPadding(0f);
			this.BackgroundColor = Color.White;
			this.BorderColor = Color.Black;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003C4E File Offset: 0x00001E4E
		public override void LeftClick(UIMouseEvent evt)
		{
			this.Focus();
			base.LeftClick(evt);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C5D File Offset: 0x00001E5D
		internal void Unfocus()
		{
			if (this.focused)
			{
				this.focused = false;
				Main.blockInput = false;
				Action onUnfocus = this.OnUnfocus;
				if (onUnfocus == null)
				{
					return;
				}
				onUnfocus();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C84 File Offset: 0x00001E84
		internal void Focus()
		{
			if (!this.focused)
			{
				Main.clrInput();
				this.focused = true;
				Main.blockInput = true;
				Action onFocus = this.OnFocus;
				if (onFocus == null)
				{
					return;
				}
				onFocus();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public override void Update(GameTime gameTime)
		{
			Vector2 MousePosition;
			MousePosition..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (!this.ContainsPoint(MousePosition) && (Main.mouseLeft || Main.mouseRight))
			{
				this.Unfocus();
			}
			base.Update(gameTime);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003CF4 File Offset: 0x00001EF4
		internal void SetText(string text)
		{
			if (text.Length > this._maxLength)
			{
				text = text.Substring(0, this._maxLength);
			}
			if (this.currentString != text)
			{
				this.currentString = text;
				Action onTextChanged = this.OnTextChanged;
				if (onTextChanged == null)
				{
					return;
				}
				onTextChanged();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D43 File Offset: 0x00001F43
		private static bool JustPressed(Keys key)
		{
			return Main.inputText.IsKeyDown(key) && !Main.oldInputText.IsKeyDown(key);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D64 File Offset: 0x00001F64
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.GetInnerDimensions().ToRectangle();
			base.DrawSelf(spriteBatch);
			if (this.focused)
			{
				PlayerInput.WritingText = true;
				Main.instance.HandleIME();
				string newString = Main.GetInputText(this.currentString, false);
				if (!newString.Equals(this.currentString))
				{
					this.currentString = newString;
					Action onTextChanged = this.OnTextChanged;
					if (onTextChanged != null)
					{
						onTextChanged();
					}
				}
				else
				{
					this.currentString = newString;
				}
				if (UIBetterTextBox.JustPressed(9))
				{
					if (this.unfocusOnTab)
					{
						this.Unfocus();
					}
					Action onTabPressed = this.OnTabPressed;
					if (onTabPressed != null)
					{
						onTabPressed();
					}
				}
				if (UIBetterTextBox.JustPressed(13))
				{
					Main.drawingPlayerChat = false;
					if (this.unfocusOnEnter)
					{
						this.Unfocus();
					}
					Action onEnterPressed = this.OnEnterPressed;
					if (onEnterPressed != null)
					{
						onEnterPressed();
					}
				}
				int num = this.textBlinkerCount + 1;
				this.textBlinkerCount = num;
				if (num >= 20)
				{
					this.textBlinkerState = (this.textBlinkerState + 1) % 2;
					this.textBlinkerCount = 0;
				}
				Main.instance.DrawWindowsIMEPanel(new Vector2(98f, (float)(Main.screenHeight - 36)), 0f);
			}
			string displayString = this.currentString;
			if (this.textBlinkerState == 1 && this.focused)
			{
				displayString += "|";
			}
			CalculatedStyle space = base.GetDimensions();
			Color color = Color.Black;
			Vector2 drawPos = space.Position() + new Vector2(4f, 2f);
			DynamicSpriteFont font = FontAssets.MouseText.Value;
			if (this.currentString.Length == 0 && !this.focused)
			{
				color *= 0.5f;
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.hintText, drawPos, color);
				return;
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, displayString, drawPos, color);
		}

		// Token: 0x0400003B RID: 59
		internal string currentString = string.Empty;

		// Token: 0x0400003C RID: 60
		internal bool focused;

		// Token: 0x0400003D RID: 61
		private readonly int _maxLength = int.MaxValue;

		// Token: 0x0400003E RID: 62
		private readonly string hintText;

		// Token: 0x0400003F RID: 63
		private int textBlinkerCount;

		// Token: 0x04000040 RID: 64
		private int textBlinkerState;

		// Token: 0x04000046 RID: 70
		internal bool unfocusOnEnter = true;

		// Token: 0x04000047 RID: 71
		internal bool unfocusOnTab = true;
	}
}
