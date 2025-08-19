using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000385 RID: 901
	internal class UITextBox : UITextPanel<string>
	{
		// Token: 0x060028C1 RID: 10433 RVA: 0x0058DF01 File Offset: 0x0058C101
		public UITextBox(string text, float textScale = 1f, bool large = false) : base(text, textScale, large)
		{
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0058DF1B File Offset: 0x0058C11B
		public void Write(string text)
		{
			base.SetText(base.Text.Insert(this._cursor, text));
			this._cursor += text.Length;
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0058DF48 File Offset: 0x0058C148
		public override void SetText(string text, float textScale, bool large)
		{
			if (text == null)
			{
				text = "";
			}
			if (text.Length > this._maxLength)
			{
				text = text.Substring(0, this._maxLength);
			}
			base.SetText(text, textScale, large);
			this._cursor = Math.Min(base.Text.Length, this._cursor);
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0058DFA1 File Offset: 0x0058C1A1
		public void SetTextMaxLength(int maxLength)
		{
			this._maxLength = maxLength;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0058DFAA File Offset: 0x0058C1AA
		public void Backspace()
		{
			if (this._cursor == 0)
			{
				return;
			}
			base.SetText(base.Text.Substring(0, base.Text.Length - 1));
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0058DFD4 File Offset: 0x0058C1D4
		public void CursorLeft()
		{
			if (this._cursor == 0)
			{
				return;
			}
			this._cursor--;
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0058DFED File Offset: 0x0058C1ED
		public void CursorRight()
		{
			if (this._cursor < base.Text.Length)
			{
				this._cursor++;
			}
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0058E010 File Offset: 0x0058C210
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.HideSelf)
			{
				return;
			}
			this._cursor = base.Text.Length;
			base.DrawSelf(spriteBatch);
			this._frameCount++;
			if ((this._frameCount %= 40) > 20)
			{
				return;
			}
			if (this.ShowInputTicker)
			{
				CalculatedStyle innerDimensions = base.GetInnerDimensions();
				Vector2 pos = innerDimensions.Position();
				Vector2 vector = new Vector2((base.IsLarge ? FontAssets.DeathText.Value : FontAssets.MouseText.Value).MeasureString(base.Text.Substring(0, this._cursor)).X, base.IsLarge ? 32f : 16f) * base.TextScale;
				if (base.IsLarge)
				{
					pos.Y -= 8f * base.TextScale;
				}
				else
				{
					pos.Y -= 2f * base.TextScale;
				}
				pos.X += (innerDimensions.Width - base.TextSize.X) * this.TextHAlign + vector.X - (base.IsLarge ? 8f : 4f) * base.TextScale + 6f;
				if (base.IsLarge)
				{
					Utils.DrawBorderStringBig(spriteBatch, "|", pos, base.TextColor, base.TextScale, 0f, 0f, -1);
					return;
				}
				Utils.DrawBorderString(spriteBatch, "|", pos, base.TextColor, base.TextScale, 0f, 0f, -1);
			}
		}

		// Token: 0x04004C09 RID: 19465
		private int _cursor;

		// Token: 0x04004C0A RID: 19466
		private int _frameCount;

		// Token: 0x04004C0B RID: 19467
		private int _maxLength = 20;

		// Token: 0x04004C0C RID: 19468
		public bool ShowInputTicker = true;

		// Token: 0x04004C0D RID: 19469
		public bool HideSelf;
	}
}
