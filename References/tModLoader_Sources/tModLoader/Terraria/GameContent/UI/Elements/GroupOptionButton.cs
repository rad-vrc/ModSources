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
	// Token: 0x020004FC RID: 1276
	public class GroupOptionButton<T> : UIElement, IGroupOptionButton
	{
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x005CBD2C File Offset: 0x005C9F2C
		public T OptionValue
		{
			get
			{
				return this._myOption;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x005CBD34 File Offset: 0x005C9F34
		public bool IsSelected
		{
			get
			{
				return this._currentOption != null && this._currentOption.Equals(this._myOption);
			}
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x005CBD64 File Offset: 0x005C9F64
		public GroupOptionButton(T option, LocalizedText title, LocalizedText description, Color textColor, string iconTexturePath, float textSize = 1f, float titleAlignmentX = 0.5f, float titleWidthReduction = 10f)
		{
			this._borderColor = Color.White;
			this._currentOption = option;
			this._myOption = option;
			this.Description = description;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(34f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
			if (iconTexturePath != null)
			{
				this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath);
			}
			this._color = Colors.InventoryDefaultColor;
			if (title != null)
			{
				UIText uIText = new UIText(title, textSize, false)
				{
					HAlign = titleAlignmentX,
					VAlign = 0.5f,
					Width = StyleDimension.FromPixelsAndPercent(0f - titleWidthReduction, 1f),
					Top = StyleDimension.FromPixels(0f)
				};
				uIText.TextColor = textColor;
				base.Append(uIText);
				this._title = uIText;
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x005CBEB4 File Offset: 0x005CA0B4
		public void SetText(LocalizedText text, float textSize, Color color)
		{
			if (this._title != null)
			{
				this._title.Remove();
			}
			UIText uIText = new UIText(text, textSize, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(0f)
			};
			uIText.TextColor = color;
			base.Append(uIText);
			this._title = uIText;
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x005CBF2D File Offset: 0x005CA12D
		public void SetCurrentOption(T option)
		{
			this._currentOption = option;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x005CBF38 File Offset: 0x005CA138
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
			float num = this._opacity;
			bool isSelected = this.IsSelected;
			if (this._UseOverrideColors)
			{
				color = (isSelected ? this._overridePickedColor : this._overrideUnpickedColor);
				num = (isSelected ? this._overrideOpacityPicked : this._overrideOpacityUnpicked);
			}
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.Lerp(Color.Black, color, this.FadeFromBlack) * num);
			if (isSelected && this.ShowHighlightWhenSelected)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._selectedBorderTexture.Value, (int)dimensions.X + 7, (int)dimensions.Y + 7, (int)dimensions.Width - 14, (int)dimensions.Height - 14, 10, 10, 10, 10, Color.Lerp(color, Color.White, this._whiteLerp) * num);
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
					color2 = Color.Lerp(color, Color.White, this._whiteLerp) * num;
				}
				spriteBatch.Draw(this._iconTexture.Value, new Vector2(dimensions.X + 1f, dimensions.Y + 1f), color2);
			}
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x005CC115 File Offset: 0x005CA315
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x005CC133 File Offset: 0x005CA333
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x005CC143 File Offset: 0x005CA343
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x005CC153 File Offset: 0x005CA353
		public void SetColor(Color color, float opacity)
		{
			this._color = color;
			this._opacity = opacity;
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x005CC163 File Offset: 0x005CA363
		public void SetColorsBasedOnSelectionState(Color pickedColor, Color unpickedColor, float opacityPicked, float opacityNotPicked)
		{
			this._UseOverrideColors = true;
			this._overridePickedColor = pickedColor;
			this._overrideUnpickedColor = unpickedColor;
			this._overrideOpacityPicked = opacityPicked;
			this._overrideOpacityUnpicked = opacityNotPicked;
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x005CC189 File Offset: 0x005CA389
		public void SetBorderColor(Color color)
		{
			this._borderColor = color;
		}

		// Token: 0x0400566F RID: 22127
		private T _currentOption;

		// Token: 0x04005670 RID: 22128
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04005671 RID: 22129
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04005672 RID: 22130
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04005673 RID: 22131
		private readonly Asset<Texture2D> _iconTexture;

		// Token: 0x04005674 RID: 22132
		private readonly T _myOption;

		// Token: 0x04005675 RID: 22133
		private Color _color;

		// Token: 0x04005676 RID: 22134
		private Color _borderColor;

		// Token: 0x04005677 RID: 22135
		public float FadeFromBlack = 1f;

		// Token: 0x04005678 RID: 22136
		private float _whiteLerp = 0.7f;

		// Token: 0x04005679 RID: 22137
		private float _opacity = 0.7f;

		// Token: 0x0400567A RID: 22138
		private bool _hovered;

		// Token: 0x0400567B RID: 22139
		private bool _soundedHover;

		// Token: 0x0400567C RID: 22140
		public bool ShowHighlightWhenSelected = true;

		// Token: 0x0400567D RID: 22141
		private bool _UseOverrideColors;

		// Token: 0x0400567E RID: 22142
		private Color _overrideUnpickedColor = Color.White;

		// Token: 0x0400567F RID: 22143
		private Color _overridePickedColor = Color.White;

		// Token: 0x04005680 RID: 22144
		private float _overrideOpacityPicked;

		// Token: 0x04005681 RID: 22145
		private float _overrideOpacityUnpicked;

		// Token: 0x04005682 RID: 22146
		public readonly LocalizedText Description;

		// Token: 0x04005683 RID: 22147
		private UIText _title;
	}
}
