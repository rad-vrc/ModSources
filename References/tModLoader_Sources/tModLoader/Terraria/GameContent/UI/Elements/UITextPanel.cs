using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000534 RID: 1332
	public class UITextPanel<T> : UIPanel
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x005D9804 File Offset: 0x005D7A04
		public bool IsLarge
		{
			get
			{
				return this._isLarge;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x005D980C File Offset: 0x005D7A0C
		// (set) Token: 0x06003F7E RID: 16254 RVA: 0x005D9814 File Offset: 0x005D7A14
		public bool DrawPanel
		{
			get
			{
				return this._drawPanel;
			}
			set
			{
				this._drawPanel = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x005D981D File Offset: 0x005D7A1D
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x005D9825 File Offset: 0x005D7A25
		public float TextScale
		{
			get
			{
				return this._textScale;
			}
			set
			{
				this._textScale = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x005D982E File Offset: 0x005D7A2E
		public Vector2 TextSize
		{
			get
			{
				return this._textSize;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x005D9836 File Offset: 0x005D7A36
		public string Text
		{
			get
			{
				if (this._text != null)
				{
					return this._text.ToString();
				}
				return "";
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x005D985C File Offset: 0x005D7A5C
		// (set) Token: 0x06003F84 RID: 16260 RVA: 0x005D9864 File Offset: 0x005D7A64
		public Color TextColor
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x005D9870 File Offset: 0x005D7A70
		public UITextPanel(T text, float textScale = 1f, bool large = false)
		{
			this.SetText(text, textScale, large);
		}

		// Token: 0x06003F86 RID: 16262 RVA: 0x005D98BF File Offset: 0x005D7ABF
		public override void Recalculate()
		{
			this.SetText(this._text, this._textScale, this._isLarge);
			base.Recalculate();
		}

		// Token: 0x06003F87 RID: 16263 RVA: 0x005D98DF File Offset: 0x005D7ADF
		public void SetText(T text)
		{
			this.SetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x005D98F4 File Offset: 0x005D7AF4
		public virtual void SetText(T text, float textScale, bool large)
		{
			Vector2 textSize = ChatManager.GetStringSize(large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value, text.ToString(), new Vector2(textScale), -1f);
			textSize.Y = (large ? 32f : 16f) * textScale;
			this._text = text;
			this._textScale = textScale;
			this._textSize = textSize;
			this._isLarge = large;
			this.MinWidth.Set(textSize.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(textSize.Y + this.PaddingTop + this.PaddingBottom, 0f);
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x005D99B4 File Offset: 0x005D7BB4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				base.DrawSelf(spriteBatch);
			}
			this.DrawText(spriteBatch);
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x005D99CC File Offset: 0x005D7BCC
		protected void DrawText(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 pos = innerDimensions.Position();
			if (this._isLarge)
			{
				pos.Y -= 10f * this._textScale * this._textScale;
			}
			else
			{
				pos.Y -= 2f * this._textScale;
			}
			pos.X += (innerDimensions.Width - this._textSize.X) * this.TextHAlign;
			string text = this.Text;
			if (this.HideContents)
			{
				if (this._asterisks == null || this._asterisks.Length != text.Length)
				{
					this._asterisks = new string('*', text.Length);
				}
				text = this._asterisks;
			}
			if (this._isLarge)
			{
				Utils.DrawBorderStringBig(spriteBatch, text, pos, this._color, this._textScale, 0f, 0f, -1);
				return;
			}
			Utils.DrawBorderString(spriteBatch, text, pos, this._color, this._textScale, 0f, 0f, -1);
		}

		// Token: 0x040057D5 RID: 22485
		protected T _text;

		// Token: 0x040057D6 RID: 22486
		protected float _textScale = 1f;

		// Token: 0x040057D7 RID: 22487
		protected Vector2 _textSize = Vector2.Zero;

		// Token: 0x040057D8 RID: 22488
		protected bool _isLarge;

		// Token: 0x040057D9 RID: 22489
		protected Color _color = Color.White;

		// Token: 0x040057DA RID: 22490
		protected bool _drawPanel = true;

		// Token: 0x040057DB RID: 22491
		public float TextHAlign = 0.5f;

		// Token: 0x040057DC RID: 22492
		public bool HideContents;

		// Token: 0x040057DD RID: 22493
		private string _asterisks;
	}
}
