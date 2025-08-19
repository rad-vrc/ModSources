using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200051F RID: 1311
	public class UIImageWithBorder : UIImage
	{
		// Token: 0x06003EDC RID: 16092 RVA: 0x005D581F File Offset: 0x005D3A1F
		public UIImageWithBorder(Asset<Texture2D> texture, Asset<Texture2D> borderTexture) : base(texture)
		{
			this.SetBorder(borderTexture);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x005D582F File Offset: 0x005D3A2F
		public UIImageWithBorder(Texture2D nonReloadingTexture, Texture2D nonReloadingBorderTexture) : base(nonReloadingTexture)
		{
			this.SetBorder(nonReloadingBorderTexture);
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x005D5840 File Offset: 0x005D3A40
		public void SetBorder(Asset<Texture2D> texture)
		{
			this._borderTexture = texture;
			this._nonReloadingBorderTexture = null;
			this.Width.Set((float)this._borderTexture.Width(), 0f);
			this.Height.Set((float)this._borderTexture.Height(), 0f);
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x005D5894 File Offset: 0x005D3A94
		public void SetBorder(Texture2D nonReloadingTexture)
		{
			this._borderTexture = null;
			this._nonReloadingBorderTexture = nonReloadingTexture;
			this.Width.Set((float)this._nonReloadingBorderTexture.Width, 0f);
			this.Height.Set((float)this._nonReloadingBorderTexture.Height, 0f);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x005D58E8 File Offset: 0x005D3AE8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			Texture2D texture2D = null;
			if (this._borderTexture != null)
			{
				texture2D = this._borderTexture.Value;
			}
			if (this._nonReloadingBorderTexture != null)
			{
				texture2D = this._nonReloadingBorderTexture;
			}
			if (this.ScaleToFit)
			{
				spriteBatch.Draw(texture2D, dimensions.ToRectangle(), this.Color);
				return;
			}
			Vector2 vector = texture2D.Size();
			Vector2 vector2 = dimensions.Position() + vector * (1f - this.ImageScale) / 2f + vector * this.NormalizedOrigin;
			if (this.RemoveFloatingPointsFromDrawPosition)
			{
				vector2 = vector2.Floor();
			}
			spriteBatch.Draw(texture2D, vector2, null, this.Color, this.Rotation, vector * this.NormalizedOrigin, this.ImageScale, 0, 0f);
		}

		// Token: 0x04005766 RID: 22374
		private Asset<Texture2D> _borderTexture;

		// Token: 0x04005767 RID: 22375
		private Texture2D _nonReloadingBorderTexture;
	}
}
