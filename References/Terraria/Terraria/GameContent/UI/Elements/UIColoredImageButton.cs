using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200037B RID: 891
	public class UIColoredImageButton : UIElement
	{
		// Token: 0x06002893 RID: 10387 RVA: 0x0058BA44 File Offset: 0x00589C44
		public UIColoredImageButton(Asset<Texture2D> texture, bool isSmall = false)
		{
			this._color = Color.White;
			this._texture = texture;
			if (isSmall)
			{
				this._backPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/SmallPanel", 1);
			}
			else
			{
				this._backPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1);
			}
			this.Width.Set((float)this._backPanelTexture.Width(), 0f);
			this.Height.Set((float)this._backPanelTexture.Height(), 0f);
			this._backPanelHighlightTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			if (isSmall)
			{
				this._backPanelBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/SmallPanelBorder", 1);
				return;
			}
			this._backPanelBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0058BB30 File Offset: 0x00589D30
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0058BB7C File Offset: 0x00589D7C
		public void SetImageWithoutSettingSize(Asset<Texture2D> texture)
		{
			this._texture = texture;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0058BB88 File Offset: 0x00589D88
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 position = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;
			spriteBatch.Draw(this._backPanelTexture.Value, position, null, Color.White * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive), 0f, this._backPanelTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			Color white = Color.White;
			if (this._hovered)
			{
				spriteBatch.Draw(this._backPanelBorderTexture.Value, position, null, Color.White, 0f, this._backPanelBorderTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
			if (this._selected)
			{
				spriteBatch.Draw(this._backPanelHighlightTexture.Value, position, null, Color.White, 0f, this._backPanelHighlightTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
			if (this._middleTexture != null)
			{
				spriteBatch.Draw(this._middleTexture.Value, position, null, Color.White, 0f, this._middleTexture.Size() / 2f, 1f, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(this._texture.Value, position, null, this._color, 0f, this._texture.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x0058BD57 File Offset: 0x00589F57
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._hovered = true;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0058BD7C File Offset: 0x00589F7C
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this._visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this._visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0058BDAA File Offset: 0x00589FAA
		public void SetColor(Color color)
		{
			this._color = color;
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0058BDB3 File Offset: 0x00589FB3
		public void SetMiddleTexture(Asset<Texture2D> texAsset)
		{
			this._middleTexture = texAsset;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0058BDBC File Offset: 0x00589FBC
		public void SetSelected(bool selected)
		{
			this._selected = selected;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0058BDC5 File Offset: 0x00589FC5
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04004BCC RID: 19404
		private Asset<Texture2D> _backPanelTexture;

		// Token: 0x04004BCD RID: 19405
		private Asset<Texture2D> _texture;

		// Token: 0x04004BCE RID: 19406
		private Asset<Texture2D> _middleTexture;

		// Token: 0x04004BCF RID: 19407
		private Asset<Texture2D> _backPanelHighlightTexture;

		// Token: 0x04004BD0 RID: 19408
		private Asset<Texture2D> _backPanelBorderTexture;

		// Token: 0x04004BD1 RID: 19409
		private Color _color;

		// Token: 0x04004BD2 RID: 19410
		private float _visibilityActive = 1f;

		// Token: 0x04004BD3 RID: 19411
		private float _visibilityInactive = 0.4f;

		// Token: 0x04004BD4 RID: 19412
		private bool _selected;

		// Token: 0x04004BD5 RID: 19413
		private bool _hovered;
	}
}
