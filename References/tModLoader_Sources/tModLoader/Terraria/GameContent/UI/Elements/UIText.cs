using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000532 RID: 1330
	public class UIText : UIElement
	{
		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x005D8FBB File Offset: 0x005D71BB
		public string Text
		{
			get
			{
				return this._text.ToString();
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x005D8FC8 File Offset: 0x005D71C8
		// (set) Token: 0x06003F5D RID: 16221 RVA: 0x005D8FD0 File Offset: 0x005D71D0
		public float TextOriginX { get; set; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06003F5E RID: 16222 RVA: 0x005D8FD9 File Offset: 0x005D71D9
		// (set) Token: 0x06003F5F RID: 16223 RVA: 0x005D8FE1 File Offset: 0x005D71E1
		public float TextOriginY { get; set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x005D8FEA File Offset: 0x005D71EA
		// (set) Token: 0x06003F61 RID: 16225 RVA: 0x005D8FF2 File Offset: 0x005D71F2
		public float WrappedTextBottomPadding { get; set; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x005D8FFB File Offset: 0x005D71FB
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x005D9003 File Offset: 0x005D7203
		public bool IsWrapped
		{
			get
			{
				return this._isWrapped;
			}
			set
			{
				this._isWrapped = value;
				if (value)
				{
					this.MinWidth.Set(0f, 0f);
				}
				this.InternalSetText(this._text, this._textScale, this._isLarge);
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x005D903C File Offset: 0x005D723C
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x005D9044 File Offset: 0x005D7244
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

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x005D904D File Offset: 0x005D724D
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x005D9055 File Offset: 0x005D7255
		public Color ShadowColor
		{
			get
			{
				return this._shadowColor;
			}
			set
			{
				this._shadowColor = value;
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06003F68 RID: 16232 RVA: 0x005D9060 File Offset: 0x005D7260
		// (remove) Token: 0x06003F69 RID: 16233 RVA: 0x005D9098 File Offset: 0x005D7298
		public event Action OnInternalTextChange;

		// Token: 0x06003F6A RID: 16234 RVA: 0x005D90D0 File Offset: 0x005D72D0
		public UIText(string text, float textScale = 1f, bool large = false)
		{
			this.TextOriginX = 0.5f;
			this.TextOriginY = 0f;
			this.IsWrapped = false;
			this.WrappedTextBottomPadding = 20f;
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x005D914C File Offset: 0x005D734C
		public UIText(LocalizedText text, float textScale = 1f, bool large = false)
		{
			this.TextOriginX = 0.5f;
			this.TextOriginY = 0f;
			this.IsWrapped = false;
			this.WrappedTextBottomPadding = 20f;
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x005D91C7 File Offset: 0x005D73C7
		public override void Recalculate()
		{
			this.InternalSetText(this._text, this._textScale, this._isLarge);
			base.Recalculate();
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x005D91E7 File Offset: 0x005D73E7
		public void SetText(string text)
		{
			this.InternalSetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x005D91FC File Offset: 0x005D73FC
		public void SetText(LocalizedText text)
		{
			this.InternalSetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x005D9211 File Offset: 0x005D7411
		public void SetText(string text, float textScale, bool large)
		{
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x005D921C File Offset: 0x005D741C
		public void SetText(LocalizedText text, float textScale, bool large)
		{
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x005D9228 File Offset: 0x005D7428
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.VerifyTextState();
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 position = innerDimensions.Position();
			if (this._isLarge)
			{
				position.Y -= 10f * this._textScale;
			}
			else
			{
				position.Y -= 2f * this._textScale;
			}
			position.X += (innerDimensions.Width - this._textSize.X) * this.TextOriginX;
			position.Y += (innerDimensions.Height - this._textSize.Y) * this.TextOriginY;
			float num = this._textScale;
			if (this.DynamicallyScaleDownToWidth && this._textSize.X > innerDimensions.Width)
			{
				num *= innerDimensions.Width / this._textSize.X;
			}
			DynamicSpriteFont value = (this._isLarge ? FontAssets.DeathText : FontAssets.MouseText).Value;
			Vector2 vector = value.MeasureString(this._visibleText);
			Color baseColor = this._shadowColor * ((float)this._color.A / 255f);
			Vector2 origin = new Vector2(0f, 0f) * vector;
			Vector2 baseScale;
			baseScale..ctor(num);
			TextSnippet[] snippets = ChatManager.ParseMessage(this._visibleText, this._color).ToArray();
			ChatManager.ConvertNormalSnippets(snippets);
			ChatManager.DrawColorCodedStringShadow(spriteBatch, value, snippets, position, baseColor, 0f, origin, baseScale, -1f, 1.5f);
			int num2;
			ChatManager.DrawColorCodedString(spriteBatch, value, snippets, position, Color.White, 0f, origin, baseScale, out num2, -1f, false);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x005D93CE File Offset: 0x005D75CE
		private void VerifyTextState()
		{
			if (this._lastTextReference != this.Text)
			{
				this.InternalSetText(this._text, this._textScale, this._isLarge);
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x005D93F8 File Offset: 0x005D75F8
		private void InternalSetText(object text, float textScale, bool large)
		{
			DynamicSpriteFont dynamicSpriteFont = large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;
			this._text = text;
			this._isLarge = large;
			this._textScale = textScale;
			this._lastTextReference = this._text.ToString();
			if (this.IsWrapped)
			{
				this._visibleText = dynamicSpriteFont.CreateWrappedText(this._lastTextReference, base.GetInnerDimensions().Width / this._textScale);
			}
			else
			{
				this._visibleText = this._lastTextReference;
			}
			Vector2 vector = ChatManager.GetStringSize(dynamicSpriteFont, this._visibleText, new Vector2(1f), -1f);
			Vector2 vector2 = this._textSize = ((!this.IsWrapped) ? (new Vector2(vector.X, large ? 32f : 16f) * textScale) : (new Vector2(vector.X, vector.Y + this.WrappedTextBottomPadding) * textScale));
			if (!this.IsWrapped)
			{
				this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0f);
			}
			this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0f);
			if (this.OnInternalTextChange != null)
			{
				this.OnInternalTextChange();
			}
		}

		// Token: 0x040057C2 RID: 22466
		private object _text = "";

		// Token: 0x040057C3 RID: 22467
		private float _textScale = 1f;

		// Token: 0x040057C4 RID: 22468
		private Vector2 _textSize = Vector2.Zero;

		// Token: 0x040057C5 RID: 22469
		private bool _isLarge;

		// Token: 0x040057C6 RID: 22470
		private Color _color = Color.White;

		// Token: 0x040057C7 RID: 22471
		private Color _shadowColor = Color.Black;

		// Token: 0x040057C8 RID: 22472
		private bool _isWrapped;

		// Token: 0x040057C9 RID: 22473
		public bool DynamicallyScaleDownToWidth;

		// Token: 0x040057CA RID: 22474
		private string _visibleText;

		// Token: 0x040057CB RID: 22475
		private string _lastTextReference;
	}
}
