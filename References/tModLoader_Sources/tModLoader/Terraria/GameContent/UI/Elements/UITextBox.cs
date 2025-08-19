using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000533 RID: 1331
	public class UITextBox : UITextPanel<string>
	{
		// Token: 0x06003F74 RID: 16244 RVA: 0x005D9550 File Offset: 0x005D7750
		public UITextBox(string text, float textScale = 1f, bool large = false) : base(text, textScale, large)
		{
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x005D956A File Offset: 0x005D776A
		public void Write(string text)
		{
			base.SetText(base.Text.Insert(this._cursor, text));
			this._cursor += text.Length;
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x005D9598 File Offset: 0x005D7798
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

		// Token: 0x06003F77 RID: 16247 RVA: 0x005D95F1 File Offset: 0x005D77F1
		public void SetTextMaxLength(int maxLength)
		{
			this._maxLength = maxLength;
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x005D95FA File Offset: 0x005D77FA
		public void Backspace()
		{
			if (this._cursor != 0)
			{
				base.SetText(base.Text.Substring(0, base.Text.Length - 1));
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x005D9623 File Offset: 0x005D7823
		public void CursorLeft()
		{
			if (this._cursor != 0)
			{
				this._cursor--;
			}
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x005D963B File Offset: 0x005D783B
		public void CursorRight()
		{
			if (this._cursor < base.Text.Length)
			{
				this._cursor++;
			}
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x005D9660 File Offset: 0x005D7860
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.HideSelf)
			{
				return;
			}
			this._cursor = base.Text.Length;
			base.DrawSelf(spriteBatch);
			this._frameCount++;
			if ((this._frameCount %= 40) <= 20 && this.ShowInputTicker)
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

		// Token: 0x040057D0 RID: 22480
		private int _cursor;

		// Token: 0x040057D1 RID: 22481
		private int _frameCount;

		// Token: 0x040057D2 RID: 22482
		private int _maxLength = 20;

		// Token: 0x040057D3 RID: 22483
		public bool ShowInputTicker = true;

		// Token: 0x040057D4 RID: 22484
		public bool HideSelf;
	}
}
