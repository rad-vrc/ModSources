using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200036A RID: 874
	public class UIBestiaryInfoLine<T> : UIElement, IManuallyOrderedUIElement
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x00587D5A File Offset: 0x00585F5A
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x00587D62 File Offset: 0x00585F62
		public int OrderInUIList { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x00587D6B File Offset: 0x00585F6B
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x00587D73 File Offset: 0x00585F73
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x00587D7C File Offset: 0x00585F7C
		public Vector2 TextSize
		{
			get
			{
				return this._textSize;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x00587D84 File Offset: 0x00585F84
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

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x00587DAA File Offset: 0x00585FAA
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x00587DB2 File Offset: 0x00585FB2
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

		// Token: 0x06002812 RID: 10258 RVA: 0x00587DBB File Offset: 0x00585FBB
		public UIBestiaryInfoLine(T text, float textScale = 1f)
		{
			this.SetText(text, textScale);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00587DEC File Offset: 0x00585FEC
		public override void Recalculate()
		{
			this.SetText(this._text, this._textScale);
			base.Recalculate();
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00587E06 File Offset: 0x00586006
		public void SetText(T text)
		{
			this.SetText(text, this._textScale);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x00587E18 File Offset: 0x00586018
		public virtual void SetText(T text, float textScale)
		{
			Vector2 vector = new Vector2(FontAssets.MouseText.Value.MeasureString(text.ToString()).X, 16f) * textScale;
			this._text = text;
			this._textScale = textScale;
			this._textSize = vector;
			this.MinWidth.Set(vector.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.MinHeight.Set(vector.Y + this.PaddingTop + this.PaddingBottom, 0f);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x00587EB4 File Offset: 0x005860B4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 pos = innerDimensions.Position();
			pos.Y -= 2f * this._textScale;
			pos.X += (innerDimensions.Width - this._textSize.X) * 0.5f;
			Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0f, 0f, -1);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00587F30 File Offset: 0x00586130
		public override int CompareTo(object obj)
		{
			IManuallyOrderedUIElement manuallyOrderedUIElement = obj as IManuallyOrderedUIElement;
			if (manuallyOrderedUIElement != null)
			{
				return this.OrderInUIList.CompareTo(manuallyOrderedUIElement.OrderInUIList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x04004B3C RID: 19260
		private T _text;

		// Token: 0x04004B3D RID: 19261
		private float _textScale = 1f;

		// Token: 0x04004B3E RID: 19262
		private Vector2 _textSize = Vector2.Zero;

		// Token: 0x04004B3F RID: 19263
		private Color _color = Color.White;
	}
}
