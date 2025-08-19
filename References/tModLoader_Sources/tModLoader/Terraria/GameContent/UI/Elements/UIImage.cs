using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200051C RID: 1308
	public class UIImage : UIElement
	{
		// Token: 0x06003EC9 RID: 16073 RVA: 0x005D5314 File Offset: 0x005D3514
		public UIImage(Asset<Texture2D> texture)
		{
			this.SetImage(texture);
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x005D534B File Offset: 0x005D354B
		public UIImage(Texture2D nonReloadingTexture)
		{
			this.SetImage(nonReloadingTexture);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x005D5384 File Offset: 0x005D3584
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this._nonReloadingTexture = null;
			if (this.AllowResizingDimensions)
			{
				this.Width.Set((float)this._texture.Width(), 0f);
				this.Height.Set((float)this._texture.Height(), 0f);
			}
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x005D53E0 File Offset: 0x005D35E0
		public void SetImage(Texture2D nonReloadingTexture)
		{
			this._texture = null;
			this._nonReloadingTexture = nonReloadingTexture;
			if (this.AllowResizingDimensions)
			{
				this.Width.Set((float)this._nonReloadingTexture.Width, 0f);
				this.Height.Set((float)this._nonReloadingTexture.Height, 0f);
			}
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x005D543C File Offset: 0x005D363C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Texture2D texture2D = null;
			if (this._texture != null)
			{
				texture2D = this._texture.Value;
			}
			if (this._nonReloadingTexture != null)
			{
				texture2D = this._nonReloadingTexture;
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

		// Token: 0x04005756 RID: 22358
		private Asset<Texture2D> _texture;

		// Token: 0x04005757 RID: 22359
		public float ImageScale = 1f;

		// Token: 0x04005758 RID: 22360
		public float Rotation;

		// Token: 0x04005759 RID: 22361
		public bool ScaleToFit;

		// Token: 0x0400575A RID: 22362
		public bool AllowResizingDimensions = true;

		// Token: 0x0400575B RID: 22363
		public Color Color = Color.White;

		// Token: 0x0400575C RID: 22364
		public Vector2 NormalizedOrigin = Vector2.Zero;

		// Token: 0x0400575D RID: 22365
		public bool RemoveFloatingPointsFromDrawPosition;

		// Token: 0x0400575E RID: 22366
		private Texture2D _nonReloadingTexture;
	}
}
