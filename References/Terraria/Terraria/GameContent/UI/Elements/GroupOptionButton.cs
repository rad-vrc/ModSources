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
	// Token: 0x02000375 RID: 885
	public class GroupOptionButton<T> : UIElement, IGroupOptionButton
	{
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x0058A8F4 File Offset: 0x00588AF4
		public T OptionValue
		{
			get
			{
				return this._myOption;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x0058A8FC File Offset: 0x00588AFC
		public bool IsSelected
		{
			get
			{
				return this._currentOption != null && this._currentOption.Equals(this._myOption);
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0058A92C File Offset: 0x00588B2C
		public GroupOptionButton(T option, LocalizedText title, LocalizedText description, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f)
		{
			this._borderColor = Color.White;
			this._currentOption = option;
			this._myOption = option;
			this.Description = description;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(34f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, 1);
			}
			this._color = Colors.InventoryDefaultColor;
			if (title != null)
			{
				UIText uitext = new UIText(title, textSize, false)
				{
					HAlign = titleAlignmentX,
					VAlign = 0.5f,
					Width = StyleDimension.FromPixelsAndPercent(-titleWidthReduction, 1f),
					Top = StyleDimension.FromPixels(0f)
				};
				uitext.TextColor = textColor;
				base.Append(uitext);
				this._title = uitext;
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0058AA7C File Offset: 0x00588C7C
		public void SetText(LocalizedText text, float textSize, Color color)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uitext = new UIText(text, textSize, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(0f)
			};
			uitext.TextColor = color;
			base.Append(uitext);
			this._title = uitext;
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0058AAF5 File Offset: 0x00588CF5
		public void SetCurrentOption(T option)
		{
			this._currentOption = option;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0058AB00 File Offset: 0x00588D00
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
			float scale = this._opacity;
			bool isSelected = this.IsSelected;
			if (this._UseOverrideColors)
			{
				color = (isSelected ? this._overridePickedColor : this._overrideUnpickedColor);
				scale = (isSelected ? this._overrideOpacityPicked : this._overrideOpacityUnpicked);
			}
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color, this.FadeFromBlack) * scale);
			if (isSelected && this.ShowHighlightWhenSelected)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._selectedBorderTexture.Value, (int)dimensions.X + 7, (int)dimensions.Y + 7, (int)dimensions.Width - 14, (int)dimensions.Height - 14, 10, 10, 10, 10, Color.Lerp(color, Color.White, this._whiteLerp) * scale);
			}
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, this._borderColor);
			}
			if (this._iconTexture != null)
			{
				Color color2 = Color.White;
				if (!this._hovered && !isSelected)
				{
					color2 = Color.Lerp(color, Color.White, this._whiteLerp) * scale;
				}
				spriteBatch.Draw(this._iconTexture.Value, new Vector2(dimensions.X + 1f, dimensions.Y + 1f), color2);
			}
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0058358D File Offset: 0x0058178D
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0058ACDD File Offset: 0x00588EDD
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0058ACED File Offset: 0x00588EED
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x0058ACFD File Offset: 0x00588EFD
		public void SetColor(Color color, float opacity)
		{
			this._color = color;
			this._opacity = opacity;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0058AD0D File Offset: 0x00588F0D
		public void SetColorsBasedOnSelectionState(Color pickedColor, Color unpickedColor, float opacityPicked, float opacityNotPicked)
		{
			this._UseOverrideColors = true;
			this._overridePickedColor = pickedColor;
			this._overrideUnpickedColor = unpickedColor;
			this._overrideOpacityPicked = opacityPicked;
			this._overrideOpacityUnpicked = opacityNotPicked;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x0058AD33 File Offset: 0x00588F33
		public void SetBorderColor(Color color)
		{
			this._borderColor = color;
		}

		// Token: 0x04004B8E RID: 19342
		private T _currentOption;

		// Token: 0x04004B8F RID: 19343
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004B90 RID: 19344
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04004B91 RID: 19345
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004B92 RID: 19346
		private readonly Asset<Texture2D> _iconTexture;

		// Token: 0x04004B93 RID: 19347
		private readonly T _myOption;

		// Token: 0x04004B94 RID: 19348
		private Color _color;

		// Token: 0x04004B95 RID: 19349
		private Color _borderColor;

		// Token: 0x04004B96 RID: 19350
		public float FadeFromBlack = 1f;

		// Token: 0x04004B97 RID: 19351
		private float _whiteLerp = 0.7f;

		// Token: 0x04004B98 RID: 19352
		private float _opacity = 0.7f;

		// Token: 0x04004B99 RID: 19353
		private bool _hovered;

		// Token: 0x04004B9A RID: 19354
		private bool _soundedHover;

		// Token: 0x04004B9B RID: 19355
		public bool ShowHighlightWhenSelected = true;

		// Token: 0x04004B9C RID: 19356
		private bool _UseOverrideColors;

		// Token: 0x04004B9D RID: 19357
		private Color _overrideUnpickedColor = Color.White;

		// Token: 0x04004B9E RID: 19358
		private Color _overridePickedColor = Color.White;

		// Token: 0x04004B9F RID: 19359
		private float _overrideOpacityPicked;

		// Token: 0x04004BA0 RID: 19360
		private float _overrideOpacityUnpicked;

		// Token: 0x04004BA1 RID: 19361
		public readonly LocalizedText Description;

		// Token: 0x04004BA2 RID: 19362
		private UIText _title;
	}
}
