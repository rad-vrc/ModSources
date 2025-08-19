using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000392 RID: 914
	public class UITextPanel<T> : UIPanel
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x00591C10 File Offset: 0x0058FE10
		public bool IsLarge
		{
			get
			{
				return this._isLarge;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x00591C18 File Offset: 0x0058FE18
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x00591C20 File Offset: 0x0058FE20
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x00591C29 File Offset: 0x0058FE29
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x00591C31 File Offset: 0x0058FE31
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

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x00591C3A File Offset: 0x0058FE3A
		public Vector2 TextSize
		{
			get
			{
				return this._textSize;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x00591C42 File Offset: 0x0058FE42
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

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x00591C68 File Offset: 0x0058FE68
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x00591C70 File Offset: 0x0058FE70
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

		// Token: 0x06002950 RID: 10576 RVA: 0x00591C7C File Offset: 0x0058FE7C
		public UITextPanel(T text, float textScale = 1f, bool large = false)
		{
			this.SetText(text, textScale, large);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x00591CCB File Offset: 0x0058FECB
		public override void Recalculate()
		{
			this.SetText(this._text, this._textScale, this._isLarge);
			base.Recalculate();
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x00591CEB File Offset: 0x0058FEEB
		public void SetText(T text)
		{
			this.SetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x00591D00 File Offset: 0x0058FF00
		public virtual void SetText(T text, float textScale, bool large)
		{
			Vector2 vector = new Vector2((large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value).MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
			this._text = text;
			this._textScale = textScale;
			this._textSize = vector;
			this._isLarge = large;
			this.MinWidth.Set(vector.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(vector.Y + this.PaddingTop + this.PaddingBottom, 0f);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x00591DBC File Offset: 0x0058FFBC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				base.DrawSelf(spriteBatch);
			}
			this.DrawText(spriteBatch);
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x00591DD4 File Offset: 0x0058FFD4
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

		// Token: 0x04004C69 RID: 19561
		protected T _text;

		// Token: 0x04004C6A RID: 19562
		protected float _textScale = 1f;

		// Token: 0x04004C6B RID: 19563
		protected Vector2 _textSize = Vector2.Zero;

		// Token: 0x04004C6C RID: 19564
		protected bool _isLarge;

		// Token: 0x04004C6D RID: 19565
		protected Color _color = Color.White;

		// Token: 0x04004C6E RID: 19566
		protected bool _drawPanel = true;

		// Token: 0x04004C6F RID: 19567
		public float TextHAlign = 0.5f;

		// Token: 0x04004C70 RID: 19568
		public bool HideContents;

		// Token: 0x04004C71 RID: 19569
		private string _asterisks;
	}
}
