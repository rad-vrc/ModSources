using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000355 RID: 853
	public class UIImageWithBorder : UIImage
	{
		// Token: 0x06002762 RID: 10082 RVA: 0x00583107 File Offset: 0x00581307
		public UIImageWithBorder(Asset<Texture2D> texture, Asset<Texture2D> borderTexture) : base(texture)
		{
			this.SetBorder(borderTexture);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00583117 File Offset: 0x00581317
		public UIImageWithBorder(Texture2D nonReloadingTexture, Texture2D nonReloadingBorderTexture) : base(nonReloadingTexture)
		{
			this.SetBorder(nonReloadingBorderTexture);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00583128 File Offset: 0x00581328
		public void SetBorder(Asset<Texture2D> texture)
		{
			this._borderTexture = texture;
			this._nonReloadingBorderTexture = null;
			this.Width.Set((float)this._borderTexture.Width(), 0f);
			this.Height.Set((float)this._borderTexture.Height(), 0f);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0058317C File Offset: 0x0058137C
		public void SetBorder(Texture2D nonReloadingTexture)
		{
			this._borderTexture = null;
			this._nonReloadingBorderTexture = nonReloadingTexture;
			this.Width.Set((float)this._nonReloadingBorderTexture.Width, 0f);
			this.Height.Set((float)this._nonReloadingBorderTexture.Height, 0f);
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x005831D0 File Offset: 0x005813D0
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
			spriteBatch.Draw(texture2D, vector2, null, this.Color, this.Rotation, vector * this.NormalizedOrigin, this.ImageScale, SpriteEffects.None, 0f);
		}

		// Token: 0x04004AD1 RID: 19153
		private Asset<Texture2D> _borderTexture;

		// Token: 0x04004AD2 RID: 19154
		private Texture2D _nonReloadingBorderTexture;
	}
}
