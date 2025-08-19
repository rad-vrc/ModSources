using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000518 RID: 1304
	public class UIHairStyleButton : UIImageButton
	{
		// Token: 0x06003EB3 RID: 16051 RVA: 0x005D4A98 File Offset: 0x005D2C98
		public UIHairStyleButton(Player player, int hairStyleId) : base(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel"))
		{
			this._player = player;
			this.HairStyleId = hairStyleId;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(44f);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
			this.UseImmediateMode = true;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x005D4B19 File Offset: 0x005D2D19
		public void SkipRenderingContent(int timeInFrames)
		{
			this._framesToSkip = timeInFrames;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x005D4B24 File Offset: 0x005D2D24
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
			Vector2 vector;
			vector..ctor(-5f, -5f);
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
			Main.PlayerRenderer.DrawPlayerHead(Main.Camera, this._player, base.GetDimensions().Center() + vector, 1f, 1f, default(Color));
			this._player.hair = hair;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x005D4C92 File Offset: 0x005D2E92
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.hair = this.HairStyleId;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x005D4CC1 File Offset: 0x005D2EC1
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x005D4CD1 File Offset: 0x005D2ED1
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04005740 RID: 22336
		private readonly Player _player;

		// Token: 0x04005741 RID: 22337
		public readonly int HairStyleId;

		// Token: 0x04005742 RID: 22338
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04005743 RID: 22339
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04005744 RID: 22340
		private bool _hovered;

		// Token: 0x04005745 RID: 22341
		private bool _soundedHover;

		// Token: 0x04005746 RID: 22342
		private int _framesToSkip;
	}
}
