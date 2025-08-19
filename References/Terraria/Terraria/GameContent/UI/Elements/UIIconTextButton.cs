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
	// Token: 0x02000356 RID: 854
	public class UIIconTextButton : UIElement
	{
		// Token: 0x06002767 RID: 10087 RVA: 0x005832B4 File Offset: 0x005814B4
		public UIIconTextButton(LocalizedText title, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f)
		{
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(34f);
			this._hoverColor = Color.White;
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._hoveredTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, 1);
			}
			this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
			if (title != null)
			{
				this.SetText(title, textSize, textColor);
			}
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00583384 File Offset: 0x00581584
		public void SetText(LocalizedText text, float textSize, Color color)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uitext = new UIText(text, textSize, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Top = StyleDimension.FromPixels(0f),
				Left = StyleDimension.FromPixelsAndPercent(10f, 0f),
				IgnoresMouseInteraction = true
			};
			uitext.TextColor = color;
			base.Append(uitext);
			this._title = uitext;
			if (this._iconTexture != null)
			{
				this.Width.Set(this._title.GetDimensions().Width + (float)this._iconTexture.Width() + 26f, 0f);
				this.Height.Set(Math.Max(this._title.GetDimensions().Height, (float)this._iconTexture.Height()) + 16f, 0f);
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00583478 File Offset: 0x00581678
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

		// Token: 0x0600276A RID: 10090 RVA: 0x0058358D File Offset: 0x0058178D
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x005835AB File Offset: 0x005817AB
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColor(Color.Lerp(Colors.InventoryDefaultColor, Color.White, this._whiteLerp), 0.7f);
			this._hovered = true;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x005835DB File Offset: 0x005817DB
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColor(Color.Lerp(Color.Black, Colors.InventoryDefaultColor, this.FadeFromBlack), 1f);
			this._hovered = false;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0058360B File Offset: 0x0058180B
		public void SetColor(Color color, float opacity)
		{
			this._color = color;
			this._opacity = opacity;
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0058361B File Offset: 0x0058181B
		public void SetHoverColor(Color color)
		{
			this._hoverColor = color;
		}

		// Token: 0x04004AD3 RID: 19155
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004AD4 RID: 19156
		private readonly Asset<Texture2D> _hoveredTexture;

		// Token: 0x04004AD5 RID: 19157
		private readonly Asset<Texture2D> _iconTexture;

		// Token: 0x04004AD6 RID: 19158
		private Color _color;

		// Token: 0x04004AD7 RID: 19159
		private Color _hoverColor;

		// Token: 0x04004AD8 RID: 19160
		public float FadeFromBlack = 1f;

		// Token: 0x04004AD9 RID: 19161
		private float _whiteLerp = 0.7f;

		// Token: 0x04004ADA RID: 19162
		private float _opacity = 0.7f;

		// Token: 0x04004ADB RID: 19163
		private bool _hovered;

		// Token: 0x04004ADC RID: 19164
		private bool _soundedHover;

		// Token: 0x04004ADD RID: 19165
		private UIText _title;
	}
}
