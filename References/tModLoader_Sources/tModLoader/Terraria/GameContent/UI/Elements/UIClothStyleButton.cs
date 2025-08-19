using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200050E RID: 1294
	public class UIClothStyleButton : UIElement
	{
		// Token: 0x06003E4E RID: 15950 RVA: 0x005D17E0 File Offset: 0x005CF9E0
		public UIClothStyleButton(Player player, int clothStyleId)
		{
			this._player = player;
			this.ClothStyleId = clothStyleId;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(80f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel");
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
			this._char = new UICharacter(this._player, false, false, 1f, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			base.Append(this._char);
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x005D189B File Offset: 0x005CFA9B
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._realSkinVariant = this._player.skinVariant;
			this._player.skinVariant = this.ClothStyleId;
			base.Draw(spriteBatch);
			this._player.skinVariant = this._realSkinVariant;
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x005D18D8 File Offset: 0x005CFAD8
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
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White * 0.5f);
			if (this._realSkinVariant == this.ClothStyleId)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._selectedBorderTexture.Value, (int)dimensions.X + 3, (int)dimensions.Y + 3, (int)dimensions.Width - 6, (int)dimensions.Height - 6, 10, 10, 10, 10, Color.White);
			}
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White);
			}
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x005D19F7 File Offset: 0x005CFBF7
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.skinVariant = this.ClothStyleId;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x005D1A26 File Offset: 0x005CFC26
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
			this._char.SetAnimated(true);
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x005D1A42 File Offset: 0x005CFC42
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
			this._char.SetAnimated(false);
		}

		// Token: 0x040056E1 RID: 22241
		private readonly Player _player;

		// Token: 0x040056E2 RID: 22242
		public readonly int ClothStyleId;

		// Token: 0x040056E3 RID: 22243
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x040056E4 RID: 22244
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x040056E5 RID: 22245
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040056E6 RID: 22246
		private readonly UICharacter _char;

		// Token: 0x040056E7 RID: 22247
		private bool _hovered;

		// Token: 0x040056E8 RID: 22248
		private bool _soundedHover;

		// Token: 0x040056E9 RID: 22249
		private int _realSkinVariant;
	}
}
