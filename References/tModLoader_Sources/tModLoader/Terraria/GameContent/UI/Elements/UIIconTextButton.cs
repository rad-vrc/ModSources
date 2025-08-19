using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200051B RID: 1307
	public class UIIconTextButton : UIElement
	{
		// Token: 0x06003EC1 RID: 16065 RVA: 0x005D4FA8 File Offset: 0x005D31A8
		public UIIconTextButton(LocalizedText title, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f)
		{
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(34f);
			this._hoverColor = Color.White;
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
			this._hoveredTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath);
			}
			this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
			if (title != null)
			{
				this.SetText(title, textSize, textColor);
			}
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x005D5074 File Offset: 0x005D3274
		public void SetText(LocalizedText text, float textSize, Color color)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uIText = new UIText(text, textSize, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Top = StyleDimension.FromPixels(0f),
				Left = StyleDimension.FromPixelsAndPercent(10f, 0f),
				IgnoresMouseInteraction = true
			};
			uIText.TextColor = color;
			base.Append(uIText);
			this._title = uIText;
			if (this._iconTexture != null)
			{
				this.Width.Set(this._title.GetDimensions().Width + (float)this._iconTexture.Width() + 26f, 0f);
				this.Height.Set(Math.Max(this._title.GetDimensions().Height, (float)this._iconTexture.Height()) + 16f, 0f);
			}
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x005D5168 File Offset: 0x005D3368
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._hovered)
			{
				if (!this._soundedHover)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._soundedHover = true;
			}
			else
			{
				this._soundedHover = false;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			Color color = this._color;
			float opacity = this._opacity;
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color, this.FadeFromBlack) * opacity);
			if (this._iconTexture != null)
			{
				Color color2 = Color.Lerp(color, Color.White, this._whiteLerp) * opacity;
				spriteBatch.Draw(this._iconTexture.Value, new Vector2(dimensions.X + dimensions.Width - (float)this._iconTexture.Width() - 5f, dimensions.Center().Y - (float)(this._iconTexture.Height() / 2)), color2);
			}
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x005D527D File Offset: 0x005D347D
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x005D529B File Offset: 0x005D349B
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColor(Color.Lerp(Colors.InventoryDefaultColor, Color.White, this._whiteLerp), 0.7f);
			this._hovered = true;
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x005D52CB File Offset: 0x005D34CB
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
			this._hovered = false;
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x005D52FB File Offset: 0x005D34FB
		public void SetColor(Color color, float opacity)
		{
			this._color = color;
			this._opacity = opacity;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x005D530B File Offset: 0x005D350B
		public void SetHoverColor(Color color)
		{
			this._hoverColor = color;
		}

		// Token: 0x0400574B RID: 22347
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x0400574C RID: 22348
		private readonly Asset<Texture2D> _hoveredTexture;

		// Token: 0x0400574D RID: 22349
		private readonly Asset<Texture2D> _iconTexture;

		// Token: 0x0400574E RID: 22350
		private Color _color;

		// Token: 0x0400574F RID: 22351
		private Color _hoverColor;

		// Token: 0x04005750 RID: 22352
		public float FadeFromBlack = 1f;

		// Token: 0x04005751 RID: 22353
		private float _whiteLerp = 0.7f;

		// Token: 0x04005752 RID: 22354
		private float _opacity = 0.7f;

		// Token: 0x04005753 RID: 22355
		private bool _hovered;

		// Token: 0x04005754 RID: 22356
		private bool _soundedHover;

		// Token: 0x04005755 RID: 22357
		private UIText _title;
	}
}
