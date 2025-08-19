using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200038A RID: 906
	public class UIImage : UIElement
	{
		// Token: 0x060028F7 RID: 10487 RVA: 0x00590594 File Offset: 0x0058E794
		public UIImage(Asset<Texture2D> texture)
		{
			this.SetImage(texture);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x005905CB File Offset: 0x0058E7CB
		public UIImage(Texture2D nonReloadingTexture)
		{
			this.SetImage(nonReloadingTexture);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00590604 File Offset: 0x0058E804
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

		// Token: 0x060028FA RID: 10490 RVA: 0x00590660 File Offset: 0x0058E860
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

		// Token: 0x060028FB RID: 10491 RVA: 0x005906BC File Offset: 0x0058E8BC
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
			spriteBatch.Draw(texture2D, vector2, null, this.Color, this.Rotation, vector * this.NormalizedOrigin, this.ImageScale, SpriteEffects.None, 0f);
		}

		// Token: 0x04004C37 RID: 19511
		private Asset<Texture2D> _texture;

		// Token: 0x04004C38 RID: 19512
		public float ImageScale = 1f;

		// Token: 0x04004C39 RID: 19513
		public float Rotation;

		// Token: 0x04004C3A RID: 19514
		public bool ScaleToFit;

		// Token: 0x04004C3B RID: 19515
		public bool AllowResizingDimensions = true;

		// Token: 0x04004C3C RID: 19516
		public Color Color = Color.White;

		// Token: 0x04004C3D RID: 19517
		public Vector2 NormalizedOrigin = Vector2.Zero;

		// Token: 0x04004C3E RID: 19518
		public bool RemoveFloatingPointsFromDrawPosition;

		// Token: 0x04004C3F RID: 19519
		private Texture2D _nonReloadingTexture;
	}
}
