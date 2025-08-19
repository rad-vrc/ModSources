using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200037A RID: 890
	public class UISlicedImage : UIElement
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x0058B939 File Offset: 0x00589B39
		// (set) Token: 0x0600288D RID: 10381 RVA: 0x0058B941 File Offset: 0x00589B41
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

		// Token: 0x0600288E RID: 10382 RVA: 0x0058B94C File Offset: 0x00589B4C
		public UISlicedImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0058B99E File Offset: 0x00589B9E
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0058B9A8 File Offset: 0x00589BA8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawSplicedPanel(spriteBatch, this._texture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, this._leftSliceDepth, this._rightSliceDepth, this._topSliceDepth, this._bottomSliceDepth, this._color);
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0058BA07 File Offset: 0x00589C07
		public void SetSliceDepths(int top, int bottom, int left, int right)
		{
			this._leftSliceDepth = left;
			this._rightSliceDepth = right;
			this._topSliceDepth = top;
			this._bottomSliceDepth = bottom;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0058BA26 File Offset: 0x00589C26
		public void SetSliceDepths(int fluff)
		{
			this._leftSliceDepth = fluff;
			this._rightSliceDepth = fluff;
			this._topSliceDepth = fluff;
			this._bottomSliceDepth = fluff;
		}

		// Token: 0x04004BC6 RID: 19398
		private Asset<Texture2D> _texture;

		// Token: 0x04004BC7 RID: 19399
		private Color _color;

		// Token: 0x04004BC8 RID: 19400
		private int _leftSliceDepth;

		// Token: 0x04004BC9 RID: 19401
		private int _rightSliceDepth;

		// Token: 0x04004BCA RID: 19402
		private int _topSliceDepth;

		// Token: 0x04004BCB RID: 19403
		private int _bottomSliceDepth;
	}
}
