using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000508 RID: 1288
	public class UIBestiaryInfoLine<T> : UIElement, IManuallyOrderedUIElement
	{
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x005CEDE5 File Offset: 0x005CCFE5
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x005CEDED File Offset: 0x005CCFED
		public int OrderInUIList { get; set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x005CEDF6 File Offset: 0x005CCFF6
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x005CEDFE File Offset: 0x005CCFFE
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

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x005CEE07 File Offset: 0x005CD007
		public Vector2 TextSize
		{
			get
			{
				return this._textSize;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06003E09 RID: 15881 RVA: 0x005CEE0F File Offset: 0x005CD00F
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

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x005CEE35 File Offset: 0x005CD035
		// (set) Token: 0x06003E0B RID: 15883 RVA: 0x005CEE3D File Offset: 0x005CD03D
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

		// Token: 0x06003E0C RID: 15884 RVA: 0x005CEE46 File Offset: 0x005CD046
		public UIBestiaryInfoLine(T text, float textScale = 1f)
		{
			this.SetText(text, textScale);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x005CEE77 File Offset: 0x005CD077
		public override void Recalculate()
		{
			this.SetText(this._text, this._textScale);
			base.Recalculate();
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x005CEE91 File Offset: 0x005CD091
		public void SetText(T text)
		{
			this.SetText(text, this._textScale);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x005CEEA0 File Offset: 0x005CD0A0
		public virtual void SetText(T text, float textScale)
		{
			Vector2 textSize = new Vector2(FontAssets.MouseText.Value.MeasureString(text.ToString()).X, 16f) * textScale;
			this._text = text;
			this._textScale = textScale;
			this._textSize = textSize;
			this.MinWidth.Set(textSize.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(textSize.Y + this.PaddingTop + this.PaddingBottom, 0f);
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x005CEF3C File Offset: 0x005CD13C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 pos = innerDimensions.Position();
			pos.Y -= 2f * this._textScale;
			pos.X += (innerDimensions.Width - this._textSize.X) * 0.5f;
			Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0f, 0f, -1);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x005CEFB8 File Offset: 0x005CD1B8
		public override int CompareTo(object obj)
		{
			IManuallyOrderedUIElement manuallyOrderedUIElement = obj as IManuallyOrderedUIElement;
			if (manuallyOrderedUIElement != null)
			{
				return this.OrderInUIList.CompareTo(manuallyOrderedUIElement.OrderInUIList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x040056B0 RID: 22192
		private T _text;

		// Token: 0x040056B1 RID: 22193
		private float _textScale = 1f;

		// Token: 0x040056B2 RID: 22194
		private Vector2 _textSize = Vector2.Zero;

		// Token: 0x040056B3 RID: 22195
		private Color _color = Color.White;
	}
}
