using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200051D RID: 1309
	public class UIImageButton : UIElement
	{
		// Token: 0x06003ECE RID: 16078 RVA: 0x005D5518 File Offset: 0x005D3718
		public UIImageButton(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x005D5580 File Offset: 0x005D3780
		public void SetHoverImage(Asset<Texture2D> texture)
		{
			this._borderTexture = texture;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x005D558C File Offset: 0x005D378C
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x005D55D8 File Offset: 0x005D37D8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive));
			if (this._borderTexture != null && base.IsMouseHovering)
			{
				spriteBatch.Draw(this._borderTexture.Value, dimensions.Position(), Color.White);
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x005D5651 File Offset: 0x005D3851
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x005D566F File Offset: 0x005D386F
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x005D5678 File Offset: 0x005D3878
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this._visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this._visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x0400575F RID: 22367
		private Asset<Texture2D> _texture;

		// Token: 0x04005760 RID: 22368
		private float _visibilityActive = 1f;

		// Token: 0x04005761 RID: 22369
		private float _visibilityInactive = 0.4f;

		// Token: 0x04005762 RID: 22370
		private Asset<Texture2D> _borderTexture;
	}
}
