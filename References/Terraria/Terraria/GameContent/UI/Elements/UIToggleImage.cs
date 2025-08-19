using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000393 RID: 915
	public class UIToggleImage : UIElement
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x00591EE0 File Offset: 0x005900E0
		public bool IsOn
		{
			get
			{
				return this._isOn;
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x00591EE8 File Offset: 0x005900E8
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

		// Token: 0x06002958 RID: 10584 RVA: 0x00591F64 File Offset: 0x00590164
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

		// Token: 0x06002959 RID: 10585 RVA: 0x00592006 File Offset: 0x00590206
		public override void LeftClick(UIMouseEvent evt)
		{
			this.Toggle();
			base.LeftClick(evt);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x00592015 File Offset: 0x00590215
		public void SetState(bool value)
		{
			this._isOn = value;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x0059201E File Offset: 0x0059021E
		public void Toggle()
		{
			this._isOn = !this._isOn;
		}

		// Token: 0x04004C72 RID: 19570
		private Asset<Texture2D> _onTexture;

		// Token: 0x04004C73 RID: 19571
		private Asset<Texture2D> _offTexture;

		// Token: 0x04004C74 RID: 19572
		private int _drawWidth;

		// Token: 0x04004C75 RID: 19573
		private int _drawHeight;

		// Token: 0x04004C76 RID: 19574
		private Point _onTextureOffset = Point.Zero;

		// Token: 0x04004C77 RID: 19575
		private Point _offTextureOffset = Point.Zero;

		// Token: 0x04004C78 RID: 19576
		private bool _isOn;
	}
}
