using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000246 RID: 582
	internal class UIFocusInputTextField : UIElement
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x005132D4 File Offset: 0x005114D4
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x005132DC File Offset: 0x005114DC
		public bool UnfocusOnTab { get; internal set; }

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06002990 RID: 10640 RVA: 0x005132E8 File Offset: 0x005114E8
		// (remove) Token: 0x06002991 RID: 10641 RVA: 0x00513320 File Offset: 0x00511520
		public event UIFocusInputTextField.EventHandler OnTextChange;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002992 RID: 10642 RVA: 0x00513358 File Offset: 0x00511558
		// (remove) Token: 0x06002993 RID: 10643 RVA: 0x00513390 File Offset: 0x00511590
		public event UIFocusInputTextField.EventHandler OnUnfocus;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002994 RID: 10644 RVA: 0x005133C8 File Offset: 0x005115C8
		// (remove) Token: 0x06002995 RID: 10645 RVA: 0x00513400 File Offset: 0x00511600
		public event UIFocusInputTextField.EventHandler OnTab;

		// Token: 0x06002996 RID: 10646 RVA: 0x00513435 File Offset: 0x00511635
		public UIFocusInputTextField(string hintText)
		{
			this._hintText = hintText;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0051344F File Offset: 0x0051164F
		public void SetText(string text)
		{
			if (text == null)
			{
				text = "";
			}
			if (this.CurrentString != text)
			{
				this.CurrentString = text;
				UIFocusInputTextField.EventHandler onTextChange = this.OnTextChange;
				if (onTextChange == null)
				{
					return;
				}
				onTextChange(this, new EventArgs());
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x00513486 File Offset: 0x00511686
		public override void LeftClick(UIMouseEvent evt)
		{
			Main.clrInput();
			this.Focused = true;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00513494 File Offset: 0x00511694
		public override void Update(GameTime gameTime)
		{
			Vector2 MousePosition;
			MousePosition..ctor((float)Main.mouseX, (float)Main.mouseY);
			if (!this.ContainsPoint(MousePosition) && Main.mouseLeft)
			{
				this.Focused = false;
				UIFocusInputTextField.EventHandler onUnfocus = this.OnUnfocus;
				if (onUnfocus != null)
				{
					onUnfocus(this, new EventArgs());
				}
			}
			base.Update(gameTime);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x005134E9 File Offset: 0x005116E9
		private static bool JustPressed(Keys key)
		{
			return Main.inputText.IsKeyDown(key) && !Main.oldInputText.IsKeyDown(key);
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00513508 File Offset: 0x00511708
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.Focused)
			{
				PlayerInput.WritingText = true;
				Main.instance.HandleIME();
				string newString = Main.GetInputText(this.CurrentString, false);
				if (Main.inputTextEscape)
				{
					Main.inputTextEscape = false;
					this.Focused = false;
					UIFocusInputTextField.EventHandler onUnfocus = this.OnUnfocus;
					if (onUnfocus != null)
					{
						onUnfocus(this, new EventArgs());
					}
				}
				if (!newString.Equals(this.CurrentString))
				{
					this.CurrentString = newString;
					UIFocusInputTextField.EventHandler onTextChange = this.OnTextChange;
					if (onTextChange != null)
					{
						onTextChange(this, new EventArgs());
					}
				}
				else
				{
					this.CurrentString = newString;
				}
				if (UIFocusInputTextField.JustPressed(9))
				{
					if (this.UnfocusOnTab)
					{
						this.Focused = false;
						UIFocusInputTextField.EventHandler onUnfocus2 = this.OnUnfocus;
						if (onUnfocus2 != null)
						{
							onUnfocus2(this, new EventArgs());
						}
					}
					UIFocusInputTextField.EventHandler onTab = this.OnTab;
					if (onTab != null)
					{
						onTab(this, new EventArgs());
					}
				}
				int num = this._textBlinkerCount + 1;
				this._textBlinkerCount = num;
				if (num >= 20)
				{
					this._textBlinkerState = (this._textBlinkerState + 1) % 2;
					this._textBlinkerCount = 0;
				}
			}
			string displayString = this.CurrentString;
			if (this._textBlinkerState == 1 && this.Focused)
			{
				displayString += "|";
			}
			CalculatedStyle space = base.GetDimensions();
			if (this.CurrentString.Length == 0 && !this.Focused)
			{
				Utils.DrawBorderString(spriteBatch, this._hintText, new Vector2(space.X, space.Y), Color.Gray, 1f, 0f, 0f, -1);
				return;
			}
			Utils.DrawBorderString(spriteBatch, displayString, new Vector2(space.X, space.Y), Color.White, 1f, 0f, 0f, -1);
		}

		// Token: 0x04001A60 RID: 6752
		internal bool Focused;

		// Token: 0x04001A61 RID: 6753
		internal string CurrentString = "";

		// Token: 0x04001A62 RID: 6754
		private readonly string _hintText;

		// Token: 0x04001A63 RID: 6755
		private int _textBlinkerCount;

		// Token: 0x04001A64 RID: 6756
		private int _textBlinkerState;

		// Token: 0x020009FB RID: 2555
		// (Invoke) Token: 0x0600574B RID: 22347
		public delegate void EventHandler(object sender, EventArgs e);
	}
}
