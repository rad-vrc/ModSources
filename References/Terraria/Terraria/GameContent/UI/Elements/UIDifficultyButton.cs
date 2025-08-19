using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000378 RID: 888
	public class UIDifficultyButton : UIElement
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x0058B468 File Offset: 0x00589668
		public UIDifficultyButton(Player player, LocalizedText title, LocalizedText description, byte difficulty, Color color)
		{
			this._player = player;
			this._difficulty = difficulty;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(110f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			this._color = color;
			UIText element = new UIText(title, 0.9f, false)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(5f)
			};
			base.Append(element);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0058B544 File Offset: 0x00589744
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
			int num = 7;
			if (dimensions.Height < 30f)
			{
				num = 5;
			}
			int num2 = 10;
			int num3 = 10;
			bool flag = this._difficulty == this._player.difficulty;
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
			if (flag)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X + num, (int)dimensions.Y + num - 2, (int)dimensions.Width - num * 2, (int)dimensions.Height - num * 2, num2, num2, num3, num3, Color.Lerp(this._color, Color.White, 0.7f) * 0.5f);
			}
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.White);
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0058B6A5 File Offset: 0x005898A5
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.difficulty = this._difficulty;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0058B6D4 File Offset: 0x005898D4
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0058B6E4 File Offset: 0x005898E4
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04004BB7 RID: 19383
		private readonly Player _player;

		// Token: 0x04004BB8 RID: 19384
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004BB9 RID: 19385
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04004BBA RID: 19386
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004BBB RID: 19387
		private readonly byte _difficulty;

		// Token: 0x04004BBC RID: 19388
		private readonly Color _color;

		// Token: 0x04004BBD RID: 19389
		private bool _hovered;

		// Token: 0x04004BBE RID: 19390
		private bool _soundedHover;
	}
}
