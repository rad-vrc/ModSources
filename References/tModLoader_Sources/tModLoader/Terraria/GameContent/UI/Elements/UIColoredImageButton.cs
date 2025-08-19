using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200050F RID: 1295
	public class UIColoredImageButton : UIElement
	{
		// Token: 0x06003E54 RID: 15956 RVA: 0x005D1A60 File Offset: 0x005CFC60
		public UIColoredImageButton(Asset<Texture2D> texture, bool isSmall = false)
		{
			this._color = Color.White;
			this._texture = texture;
			if (isSmall)
			{
				this._backPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/SmallPanel");
			}
			else
			{
				this._backPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel");
			}
			this.Width.Set((float)this._backPanelTexture.Width(), 0f);
			this.Height.Set((float)this._backPanelTexture.Height(), 0f);
			this._backPanelHighlightTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			if (isSmall)
			{
				this._backPanelBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/SmallPanelBorder");
				return;
			}
			this._backPanelBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x005D1B48 File Offset: 0x005CFD48
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x005D1B94 File Offset: 0x005CFD94
		public void SetImageWithoutSettingSize(Asset<Texture2D> texture)
		{
			this._texture = texture;
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x005D1BA0 File Offset: 0x005CFDA0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 position = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;
			spriteBatch.Draw(this._backPanelTexture.Value, position, null, Color.White * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive), 0f, this._backPanelTexture.Size() / 2f, 1f, 0, 0f);
			Color white = Color.White;
			if (this._hovered)
			{
				spriteBatch.Draw(this._backPanelBorderTexture.Value, position, null, Color.White, 0f, this._backPanelBorderTexture.Size() / 2f, 1f, 0, 0f);
			}
			if (this._selected)
			{
				spriteBatch.Draw(this._backPanelHighlightTexture.Value, position, null, Color.White, 0f, this._backPanelHighlightTexture.Size() / 2f, 1f, 0, 0f);
			}
			if (this._middleTexture != null)
			{
				spriteBatch.Draw(this._middleTexture.Value, position, null, Color.White, 0f, this._middleTexture.Size() / 2f, 1f, 0, 0f);
			}
			spriteBatch.Draw(this._texture.Value, position, null, this._color, 0f, this._texture.Size() / 2f, 1f, 0, 0f);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x005D1D6F File Offset: 0x005CFF6F
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._hovered = true;
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x005D1D94 File Offset: 0x005CFF94
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this._visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this._visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x005D1DC2 File Offset: 0x005CFFC2
		public void SetColor(Color color)
		{
			this._color = color;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x005D1DCB File Offset: 0x005CFFCB
		public void SetMiddleTexture(Asset<Texture2D> texAsset)
		{
			this._middleTexture = texAsset;
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x005D1DD4 File Offset: 0x005CFFD4
		public void SetSelected(bool selected)
		{
			this._selected = selected;
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x005D1DDD File Offset: 0x005CFFDD
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x040056EA RID: 22250
		private Asset<Texture2D> _backPanelTexture;

		// Token: 0x040056EB RID: 22251
		private Asset<Texture2D> _texture;

		// Token: 0x040056EC RID: 22252
		private Asset<Texture2D> _middleTexture;

		// Token: 0x040056ED RID: 22253
		private Asset<Texture2D> _backPanelHighlightTexture;

		// Token: 0x040056EE RID: 22254
		private Asset<Texture2D> _backPanelBorderTexture;

		// Token: 0x040056EF RID: 22255
		private Color _color;

		// Token: 0x040056F0 RID: 22256
		private float _visibilityActive = 1f;

		// Token: 0x040056F1 RID: 22257
		private float _visibilityInactive = 0.4f;

		// Token: 0x040056F2 RID: 22258
		private bool _selected;

		// Token: 0x040056F3 RID: 22259
		private bool _hovered;
	}
}
