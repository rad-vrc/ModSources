using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000379 RID: 889
	public class UIHairStyleButton : UIImageButton
	{
		// Token: 0x06002886 RID: 10374 RVA: 0x0058B6F4 File Offset: 0x005898F4
		public UIHairStyleButton(Player player, int hairStyleId) : base(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1))
		{
			this._player = player;
			this.HairStyleId = hairStyleId;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(44f);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0058B771 File Offset: 0x00589971
		public void SkipRenderingContent(int timeInFrames)
		{
			this._framesToSkip = timeInFrames;
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0058B77C File Offset: 0x0058997C
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
			Vector2 value = new Vector2(-5f, -5f);
			base.DrawSelf(spriteBatch);
			if (this._player.hair == this.HairStyleId)
			{
				spriteBatch.Draw(this._selectedBorderTexture.Value, base.GetDimensions().Center() - this._selectedBorderTexture.Size() / 2f, Color.White);
			}
			if (this._hovered)
			{
				spriteBatch.Draw(this._hoveredBorderTexture.Value, base.GetDimensions().Center() - this._hoveredBorderTexture.Size() / 2f, Color.White);
			}
			if (this._framesToSkip > 0)
			{
				this._framesToSkip--;
				return;
			}
			int hair = this._player.hair;
			this._player.hair = this.HairStyleId;
			Main.PlayerRenderer.DrawPlayerHead(Main.Camera, this._player, base.GetDimensions().Center() + value, 1f, 1f, default(Color));
			this._player.hair = hair;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0058B8EA File Offset: 0x00589AEA
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.hair = this.HairStyleId;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0058B919 File Offset: 0x00589B19
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0058B929 File Offset: 0x00589B29
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04004BBF RID: 19391
		private readonly Player _player;

		// Token: 0x04004BC0 RID: 19392
		public readonly int HairStyleId;

		// Token: 0x04004BC1 RID: 19393
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04004BC2 RID: 19394
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004BC3 RID: 19395
		private bool _hovered;

		// Token: 0x04004BC4 RID: 19396
		private bool _soundedHover;

		// Token: 0x04004BC5 RID: 19397
		private int _framesToSkip;
	}
}
