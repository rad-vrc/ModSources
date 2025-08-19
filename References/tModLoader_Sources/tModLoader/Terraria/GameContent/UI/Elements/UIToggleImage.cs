using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000535 RID: 1333
	public class UIToggleImage : UIElement
	{
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x005D9AD8 File Offset: 0x005D7CD8
		public bool IsOn
		{
			get
			{
				return this._isOn;
			}
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x005D9AE0 File Offset: 0x005D7CE0
		public UIToggleImage(Asset<Texture2D> texture, int width, int height, Point onTextureOffset, Point offTextureOffset)
		{
			this._onTexture = texture;
			this._offTexture = texture;
			this._offTextureOffset = offTextureOffset;
			this._onTextureOffset = onTextureOffset;
			this._drawWidth = width;
			this._drawHeight = height;
			this.Width.Set((float)width, 0f);
			this.Height.Set((float)height, 0f);
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x005D9B5C File Offset: 0x005D7D5C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Texture2D value;
			Point point;
			if (this._isOn)
			{
				value = this._onTexture.Value;
				point = this._onTextureOffset;
			}
			else
			{
				value = this._offTexture.Value;
				point = this._offTextureOffset;
			}
			Color color = base.IsMouseHovering ? Color.White : Color.Silver;
			spriteBatch.Draw(value, new Rectangle((int)dimensions.X, (int)dimensions.Y, this._drawWidth, this._drawHeight), new Rectangle?(new Rectangle(point.X, point.Y, this._drawWidth, this._drawHeight)), color);
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x005D9BFE File Offset: 0x005D7DFE
		public override void LeftClick(UIMouseEvent evt)
		{
			this.Toggle();
			base.LeftClick(evt);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x005D9C0D File Offset: 0x005D7E0D
		public void SetState(bool value)
		{
			this._isOn = value;
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x005D9C16 File Offset: 0x005D7E16
		public void Toggle()
		{
			this._isOn = !this._isOn;
		}

		// Token: 0x040057DE RID: 22494
		private Asset<Texture2D> _onTexture;

		// Token: 0x040057DF RID: 22495
		private Asset<Texture2D> _offTexture;

		// Token: 0x040057E0 RID: 22496
		private int _drawWidth;

		// Token: 0x040057E1 RID: 22497
		private int _drawHeight;

		// Token: 0x040057E2 RID: 22498
		private Point _onTextureOffset = Point.Zero;

		// Token: 0x040057E3 RID: 22499
		private Point _offTextureOffset = Point.Zero;

		// Token: 0x040057E4 RID: 22500
		private bool _isOn;
	}
}
