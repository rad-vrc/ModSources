using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000515 RID: 1301
	public class UIDifficultyButton : UIElement
	{
		// Token: 0x06003E9E RID: 16030 RVA: 0x005D3E3C File Offset: 0x005D203C
		public UIDifficultyButton(Player player, LocalizedText title, LocalizedText description, byte difficulty, Color color)
		{
			this._player = player;
			this._difficulty = difficulty;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(110f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
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

		// Token: 0x06003E9F RID: 16031 RVA: 0x005D3F14 File Offset: 0x005D2114
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

		// Token: 0x06003EA0 RID: 16032 RVA: 0x005D4075 File Offset: 0x005D2275
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.difficulty = this._difficulty;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x005D40A4 File Offset: 0x005D22A4
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x005D40B4 File Offset: 0x005D22B4
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04005728 RID: 22312
		private readonly Player _player;

		// Token: 0x04005729 RID: 22313
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x0400572A RID: 22314
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x0400572B RID: 22315
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x0400572C RID: 22316
		private readonly byte _difficulty;

		// Token: 0x0400572D RID: 22317
		private readonly Color _color;

		// Token: 0x0400572E RID: 22318
		private bool _hovered;

		// Token: 0x0400572F RID: 22319
		private bool _soundedHover;
	}
}
