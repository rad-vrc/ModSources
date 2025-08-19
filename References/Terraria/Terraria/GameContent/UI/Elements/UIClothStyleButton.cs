using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000377 RID: 887
	public class UIClothStyleButton : UIElement
	{
		// Token: 0x0600287B RID: 10363 RVA: 0x0058B1E4 File Offset: 0x005893E4
		public UIClothStyleButton(Player player, int clothStyleId)
		{
			this._player = player;
			this.ClothStyleId = clothStyleId;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(80f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			this._char = new UICharacter(this._player, false, false, 1f, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			base.Append(this._char);
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0058B2A2 File Offset: 0x005894A2
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._realSkinVariant = this._player.skinVariant;
			this._player.skinVariant = this.ClothStyleId;
			base.Draw(spriteBatch);
			this._player.skinVariant = this._realSkinVariant;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x0058B2E0 File Offset: 0x005894E0
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

		// Token: 0x0600287E RID: 10366 RVA: 0x0058B3FF File Offset: 0x005895FF
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.skinVariant = this.ClothStyleId;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0058B42E File Offset: 0x0058962E
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
			this._char.SetAnimated(true);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0058B44A File Offset: 0x0058964A
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
			this._char.SetAnimated(false);
		}

		// Token: 0x04004BAE RID: 19374
		private readonly Player _player;

		// Token: 0x04004BAF RID: 19375
		public readonly int ClothStyleId;

		// Token: 0x04004BB0 RID: 19376
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004BB1 RID: 19377
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04004BB2 RID: 19378
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004BB3 RID: 19379
		private readonly UICharacter _char;

		// Token: 0x04004BB4 RID: 19380
		private bool _hovered;

		// Token: 0x04004BB5 RID: 19381
		private bool _soundedHover;

		// Token: 0x04004BB6 RID: 19382
		private int _realSkinVariant;
	}
}
