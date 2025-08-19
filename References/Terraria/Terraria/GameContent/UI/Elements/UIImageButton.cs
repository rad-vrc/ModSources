using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200038B RID: 907
	public class UIImageButton : UIElement
	{
		// Token: 0x060028FC RID: 10492 RVA: 0x00590798 File Offset: 0x0058E998
		public UIImageButton(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00590800 File Offset: 0x0058EA00
		public void SetHoverImage(Asset<Texture2D> texture)
		{
			this._borderTexture = texture;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0059080C File Offset: 0x0058EA0C
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00590858 File Offset: 0x0058EA58
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive));
			if (this._borderTexture != null && base.IsMouseHovering)
			{
				spriteBatch.Draw(this._borderTexture.Value, dimensions.Position(), Color.White);
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x005885D0 File Offset: 0x005867D0
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x005908D1 File Offset: 0x0058EAD1
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x005908DA File Offset: 0x0058EADA
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this._visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this._visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x04004C40 RID: 19520
		private Asset<Texture2D> _texture;

		// Token: 0x04004C41 RID: 19521
		private float _visibilityActive = 1f;

		// Token: 0x04004C42 RID: 19522
		private float _visibilityInactive = 0.4f;

		// Token: 0x04004C43 RID: 19523
		private Asset<Texture2D> _borderTexture;
	}
}
