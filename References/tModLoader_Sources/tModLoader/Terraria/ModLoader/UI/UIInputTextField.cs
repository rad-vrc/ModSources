using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200024A RID: 586
	internal class UIInputTextField : UIElement
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x00513D67 File Offset: 0x00511F67
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x00513D6F File Offset: 0x00511F6F
		public string Text
		{
			get
			{
				return this._currentString;
			}
			set
			{
				if (this._currentString != value)
				{
					this._currentString = value;
					UIInputTextField.EventHandler onTextChange = this.OnTextChange;
					if (onTextChange == null)
					{
						return;
					}
					onTextChange(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060029AF RID: 10671 RVA: 0x00513D9C File Offset: 0x00511F9C
		// (remove) Token: 0x060029B0 RID: 10672 RVA: 0x00513DD4 File Offset: 0x00511FD4
		public event UIInputTextField.EventHandler OnTextChange;

		// Token: 0x060029B1 RID: 10673 RVA: 0x00513E09 File Offset: 0x00512009
		public UIInputTextField(string hintText)
		{
			this._hintText = hintText;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x00513E24 File Offset: 0x00512024
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			string newString = Main.GetInputText(this._currentString, false);
			if (newString != this._currentString)
			{
				this._currentString = newString;
				UIInputTextField.EventHandler onTextChange = this.OnTextChange;
				if (onTextChange != null)
				{
					onTextChange(this, EventArgs.Empty);
				}
			}
			string displayString = this._currentString;
			int num = this._textBlinkerCount + 1;
			this._textBlinkerCount = num;
			if (num / 20 % 2 == 0)
			{
				displayString += "|";
			}
			CalculatedStyle space = base.GetDimensions();
			if (this._currentString.Length == 0)
			{
				Utils.DrawBorderString(spriteBatch, this._hintText, new Vector2(space.X, space.Y), Color.Gray, 1f, 0f, 0f, -1);
				return;
			}
			Utils.DrawBorderString(spriteBatch, displayString, new Vector2(space.X, space.Y), Color.White, 1f, 0f, 0f, -1);
		}

		// Token: 0x04001A78 RID: 6776
		private readonly string _hintText;

		// Token: 0x04001A79 RID: 6777
		private string _currentString = string.Empty;

		// Token: 0x04001A7A RID: 6778
		private int _textBlinkerCount;

		// Token: 0x020009FD RID: 2557
		// (Invoke) Token: 0x06005751 RID: 22353
		public delegate void EventHandler(object sender, EventArgs e);
	}
}
