using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;

namespace Terraria.GameContent.UI.Minimap
{
	// Token: 0x020003C1 RID: 961
	public class MinimapFrame : IConfigKeyHolder
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00599202 File Offset: 0x00597402
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x0059920A File Offset: 0x0059740A
		public string ConfigKey { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00599213 File Offset: 0x00597413
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x0059921B File Offset: 0x0059741B
		public string NameKey { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x00599224 File Offset: 0x00597424
		// (set) Token: 0x06002A5C RID: 10844 RVA: 0x0059922C File Offset: 0x0059742C
		public Vector2 MinimapPosition { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x00599235 File Offset: 0x00597435
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x00599248 File Offset: 0x00597448
		private Vector2 FramePosition
		{
			get
			{
				return this.MinimapPosition + this._frameOffset;
			}
			set
			{
				this.MinimapPosition = value - this._frameOffset;
			}
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0059925C File Offset: 0x0059745C
		public MinimapFrame(Asset<Texture2D> frameTexture, Vector2 frameOffset)
		{
			this._frameTexture = frameTexture;
			this._frameOffset = frameOffset;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00599272 File Offset: 0x00597472
		public void SetResetButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._resetButton = new MinimapFrame.Button(hoverTexture, position, delegate()
			{
				this.ResetZoom();
			});
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0059928D File Offset: 0x0059748D
		private void ResetZoom()
		{
			Main.mapMinimapScale = 1.05f;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00599299 File Offset: 0x00597499
		public void SetZoomInButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._zoomInButton = new MinimapFrame.Button(hoverTexture, position, delegate()
			{
				this.ZoomInButton();
			});
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x005992B4 File Offset: 0x005974B4
		private void ZoomInButton()
		{
			Main.mapMinimapScale *= 1.025f;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x005992C6 File Offset: 0x005974C6
		public void SetZoomOutButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._zoomOutButton = new MinimapFrame.Button(hoverTexture, position, delegate()
			{
				this.ZoomOutButton();
			});
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x005992E1 File Offset: 0x005974E1
		private void ZoomOutButton()
		{
			Main.mapMinimapScale *= 0.975f;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x005992F4 File Offset: 0x005974F4
		public void Update()
		{
			MinimapFrame.Button buttonUnderMouse = this.GetButtonUnderMouse();
			this._zoomInButton.IsHighlighted = (buttonUnderMouse == this._zoomInButton);
			this._zoomOutButton.IsHighlighted = (buttonUnderMouse == this._zoomOutButton);
			this._resetButton.IsHighlighted = (buttonUnderMouse == this._resetButton);
			if (buttonUnderMouse != null && !Main.LocalPlayer.lastMouseInterface)
			{
				buttonUnderMouse.IsHighlighted = true;
				if (PlayerInput.IgnoreMouseInterface)
				{
					return;
				}
				Main.LocalPlayer.mouseInterface = true;
				if (Main.mouseLeft)
				{
					buttonUnderMouse.Click();
					if (Main.mouseLeftRelease)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
				}
			}
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x00599398 File Offset: 0x00597598
		public void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)this.MinimapPosition.X - 6, (int)this.MinimapPosition.Y - 6, 244, 244), Color.Black * Main.mapMinimapAlpha);
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x005993F0 File Offset: 0x005975F0
		public void DrawForeground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this._frameTexture.Value, this.FramePosition, Color.White);
			this._zoomInButton.Draw(spriteBatch, this.FramePosition);
			this._zoomOutButton.Draw(spriteBatch, this.FramePosition);
			this._resetButton.Draw(spriteBatch, this.FramePosition);
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x00599450 File Offset: 0x00597650
		private MinimapFrame.Button GetButtonUnderMouse()
		{
			Vector2 testPoint = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (this._zoomInButton.IsTouchingPoint(testPoint, this.FramePosition))
			{
				return this._zoomInButton;
			}
			if (this._zoomOutButton.IsTouchingPoint(testPoint, this.FramePosition))
			{
				return this._zoomOutButton;
			}
			if (this._resetButton.IsTouchingPoint(testPoint, this.FramePosition))
			{
				return this._resetButton;
			}
			return null;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		[Conditional("DEBUG")]
		private void ValidateState()
		{
		}

		// Token: 0x04004D29 RID: 19753
		private const float DEFAULT_ZOOM = 1.05f;

		// Token: 0x04004D2A RID: 19754
		private const float ZOOM_OUT_MULTIPLIER = 0.975f;

		// Token: 0x04004D2B RID: 19755
		private const float ZOOM_IN_MULTIPLIER = 1.025f;

		// Token: 0x04004D2F RID: 19759
		private readonly Asset<Texture2D> _frameTexture;

		// Token: 0x04004D30 RID: 19760
		private readonly Vector2 _frameOffset;

		// Token: 0x04004D31 RID: 19761
		private MinimapFrame.Button _resetButton;

		// Token: 0x04004D32 RID: 19762
		private MinimapFrame.Button _zoomInButton;

		// Token: 0x04004D33 RID: 19763
		private MinimapFrame.Button _zoomOutButton;

		// Token: 0x0200075E RID: 1886
		private class Button
		{
			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x060038CD RID: 14541 RVA: 0x0061445F File Offset: 0x0061265F
			private Vector2 Size
			{
				get
				{
					return new Vector2((float)this._hoverTexture.Width(), (float)this._hoverTexture.Height());
				}
			}

			// Token: 0x060038CE RID: 14542 RVA: 0x0061447E File Offset: 0x0061267E
			public Button(Asset<Texture2D> hoverTexture, Vector2 position, Action mouseDownCallback)
			{
				this._position = position;
				this._hoverTexture = hoverTexture;
				this._onMouseDown = mouseDownCallback;
			}

			// Token: 0x060038CF RID: 14543 RVA: 0x0061449B File Offset: 0x0061269B
			public void Click()
			{
				this._onMouseDown();
			}

			// Token: 0x060038D0 RID: 14544 RVA: 0x006144A8 File Offset: 0x006126A8
			public void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
			{
				if (!this.IsHighlighted)
				{
					return;
				}
				spriteBatch.Draw(this._hoverTexture.Value, this._position + parentPosition, Color.White);
			}

			// Token: 0x060038D1 RID: 14545 RVA: 0x006144D8 File Offset: 0x006126D8
			public bool IsTouchingPoint(Vector2 testPoint, Vector2 parentPosition)
			{
				Vector2 value = this._position + parentPosition + this.Size * 0.5f;
				Vector2 vector = Vector2.Max(this.Size, new Vector2(22f, 22f)) * 0.5f;
				Vector2 vector2 = testPoint - value;
				return Math.Abs(vector2.X) < vector.X && Math.Abs(vector2.Y) < vector.Y;
			}

			// Token: 0x04006444 RID: 25668
			public bool IsHighlighted;

			// Token: 0x04006445 RID: 25669
			private readonly Vector2 _position;

			// Token: 0x04006446 RID: 25670
			private readonly Asset<Texture2D> _hoverTexture;

			// Token: 0x04006447 RID: 25671
			private readonly Action _onMouseDown;
		}
	}
}
