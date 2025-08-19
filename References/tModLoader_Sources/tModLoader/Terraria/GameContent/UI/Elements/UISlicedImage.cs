using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000530 RID: 1328
	public class UISlicedImage : UIElement
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x005D8E76 File Offset: 0x005D7076
		// (set) Token: 0x06003F52 RID: 16210 RVA: 0x005D8E7E File Offset: 0x005D707E
		public Color Color
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

		// Token: 0x06003F53 RID: 16211 RVA: 0x005D8E88 File Offset: 0x005D7088
		public UISlicedImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x005D8EDA File Offset: 0x005D70DA
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x005D8EE4 File Offset: 0x005D70E4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawSplicedPanel(spriteBatch, this._texture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, this._leftSliceDepth, this._rightSliceDepth, this._topSliceDepth, this._bottomSliceDepth, this._color);
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x005D8F43 File Offset: 0x005D7143
		public void SetSliceDepths(int top, int bottom, int left, int right)
		{
			this._leftSliceDepth = left;
			this._rightSliceDepth = right;
			this._topSliceDepth = top;
			this._bottomSliceDepth = bottom;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x005D8F62 File Offset: 0x005D7162
		public void SetSliceDepths(int fluff)
		{
			this._leftSliceDepth = fluff;
			this._rightSliceDepth = fluff;
			this._topSliceDepth = fluff;
			this._bottomSliceDepth = fluff;
		}

		// Token: 0x040057B7 RID: 22455
		private Asset<Texture2D> _texture;

		// Token: 0x040057B8 RID: 22456
		private Color _color;

		// Token: 0x040057B9 RID: 22457
		private int _leftSliceDepth;

		// Token: 0x040057BA RID: 22458
		private int _rightSliceDepth;

		// Token: 0x040057BB RID: 22459
		private int _topSliceDepth;

		// Token: 0x040057BC RID: 22460
		private int _bottomSliceDepth;
	}
}
