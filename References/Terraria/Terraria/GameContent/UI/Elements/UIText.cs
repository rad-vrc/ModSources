using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000391 RID: 913
	public class UIText : UIElement
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600292E RID: 10542 RVA: 0x005916CE File Offset: 0x0058F8CE
		public string Text
		{
			get
			{
				return this._text.ToString();
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x005916DB File Offset: 0x0058F8DB
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x005916E3 File Offset: 0x0058F8E3
		public float TextOriginX { get; set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x005916EC File Offset: 0x0058F8EC
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x005916F4 File Offset: 0x0058F8F4
		public float TextOriginY { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x005916FD File Offset: 0x0058F8FD
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x00591705 File Offset: 0x0058F905
		public float WrappedTextBottomPadding { get; set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x0059170E File Offset: 0x0058F90E
		// (set) Token: 0x06002936 RID: 10550 RVA: 0x00591716 File Offset: 0x0058F916
		public bool IsWrapped
		{
			get
			{
				return this._isWrapped;
			}
			set
			{
				this._isWrapped = value;
				this.InternalSetText(this._text, this._textScale, this._isLarge);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002937 RID: 10551 RVA: 0x00591738 File Offset: 0x0058F938
		// (remove) Token: 0x06002938 RID: 10552 RVA: 0x00591770 File Offset: 0x0058F970
		public event Action OnInternalTextChange;

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x005917A5 File Offset: 0x0058F9A5
		// (set) Token: 0x0600293A RID: 10554 RVA: 0x005917AD File Offset: 0x0058F9AD
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

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x005917B6 File Offset: 0x0058F9B6
		// (set) Token: 0x0600293C RID: 10556 RVA: 0x005917BE File Offset: 0x0058F9BE
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

		// Token: 0x0600293D RID: 10557 RVA: 0x005917C8 File Offset: 0x0058F9C8
		public UIText(string text, float textScale = 1f, bool large = false)
		{
			this.TextOriginX = 0.5f;
			this.TextOriginY = 0f;
			this.IsWrapped = false;
			this.WrappedTextBottomPadding = 20f;
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x00591844 File Offset: 0x0058FA44
		public UIText(LocalizedText text, float textScale = 1f, bool large = false)
		{
			this.TextOriginX = 0.5f;
			this.TextOriginY = 0f;
			this.IsWrapped = false;
			this.WrappedTextBottomPadding = 20f;
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x005918BF File Offset: 0x0058FABF
		public override void Recalculate()
		{
			this.InternalSetText(this._text, this._textScale, this._isLarge);
			base.Recalculate();
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x005918DF File Offset: 0x0058FADF
		public void SetText(string text)
		{
			this.InternalSetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x005918DF File Offset: 0x0058FADF
		public void SetText(LocalizedText text)
		{
			this.InternalSetText(text, this._textScale, this._isLarge);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x005918F4 File Offset: 0x0058FAF4
		public void SetText(string text, float textScale, bool large)
		{
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x005918F4 File Offset: 0x0058FAF4
		public void SetText(LocalizedText text, float textScale, bool large)
		{
			this.InternalSetText(text, textScale, large);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00591900 File Offset: 0x0058FB00
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
			Vector2 value2 = value.MeasureString(this._visibleText);
			Color baseColor = this._shadowColor * ((float)this._color.A / 255f);
			Vector2 origin = new Vector2(0f, 0f) * value2;
			Vector2 baseScale = new Vector2(num);
			TextSnippet[] snippets = ChatManager.ParseMessage(this._visibleText, this._color).ToArray();
			ChatManager.ConvertNormalSnippets(snippets);
			ChatManager.DrawColorCodedStringShadow(spriteBatch, value, snippets, position, baseColor, 0f, origin, baseScale, -1f, 1.5f);
			int num2;
			ChatManager.DrawColorCodedString(spriteBatch, value, snippets, position, Color.White, 0f, origin, baseScale, out num2, -1f, false);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x00591AA6 File Offset: 0x0058FCA6
		private void VerifyTextState()
		{
			if (this._lastTextReference == this.Text)
			{
				return;
			}
			this.InternalSetText(this._text, this._textScale, this._isLarge);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x00591AD0 File Offset: 0x0058FCD0
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
			Vector2 vector = dynamicSpriteFont.MeasureString(this._visibleText);
			Vector2 vector2;
			if (this.IsWrapped)
			{
				vector2 = new Vector2(vector.X, vector.Y + this.WrappedTextBottomPadding) * textScale;
			}
			else
			{
				vector2 = new Vector2(vector.X, large ? 32f : 16f) * textScale;
			}
			this._textSize = vector2;
			this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0f);
			if (this.OnInternalTextChange != null)
			{
				this.OnInternalTextChange();
			}
		}

		// Token: 0x04004C5B RID: 19547
		private object _text = "";

		// Token: 0x04004C5C RID: 19548
		private float _textScale = 1f;

		// Token: 0x04004C5D RID: 19549
		private Vector2 _textSize = Vector2.Zero;

		// Token: 0x04004C5E RID: 19550
		private bool _isLarge;

		// Token: 0x04004C5F RID: 19551
		private Color _color = Color.White;

		// Token: 0x04004C60 RID: 19552
		private Color _shadowColor = Color.Black;

		// Token: 0x04004C61 RID: 19553
		private bool _isWrapped;

		// Token: 0x04004C65 RID: 19557
		public bool DynamicallyScaleDownToWidth;

		// Token: 0x04004C66 RID: 19558
		private string _visibleText;

		// Token: 0x04004C67 RID: 19559
		private string _lastTextReference;
	}
}
