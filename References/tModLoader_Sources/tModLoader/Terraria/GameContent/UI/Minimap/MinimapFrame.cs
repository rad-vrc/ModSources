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
	// Token: 0x020004F5 RID: 1269
	public class MinimapFrame : IConfigKeyHolder
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x005CAB57 File Offset: 0x005C8D57
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x005CAB5F File Offset: 0x005C8D5F
		public string ConfigKey { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x005CAB68 File Offset: 0x005C8D68
		// (set) Token: 0x06003D78 RID: 15736 RVA: 0x005CAB70 File Offset: 0x005C8D70
		public string NameKey { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x005CAB79 File Offset: 0x005C8D79
		// (set) Token: 0x06003D7A RID: 15738 RVA: 0x005CAB81 File Offset: 0x005C8D81
		public Vector2 MinimapPosition { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x005CAB8A File Offset: 0x005C8D8A
		// (set) Token: 0x06003D7C RID: 15740 RVA: 0x005CAB9D File Offset: 0x005C8D9D
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

		// Token: 0x06003D7D RID: 15741 RVA: 0x005CABB1 File Offset: 0x005C8DB1
		public MinimapFrame(Asset<Texture2D> frameTexture, Vector2 frameOffset)
		{
			this._frameTexture = frameTexture;
			this._frameOffset = frameOffset;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x005CABC7 File Offset: 0x005C8DC7
		public void SetResetButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._resetButton = new MinimapFrame.Button(hoverTexture, position, delegate()
			{
				this.ResetZoom();
			});
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x005CABE2 File Offset: 0x005C8DE2
		private void ResetZoom()
		{
			Main.mapMinimapScale = 1.05f;
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x005CABEE File Offset: 0x005C8DEE
		public void SetZoomInButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._zoomInButton = new MinimapFrame.Button(hoverTexture, position, delegate()
			{
				this.ZoomInButton();
			});
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x005CAC09 File Offset: 0x005C8E09
		private void ZoomInButton()
		{
			Main.mapMinimapScale *= 1.025f;
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x005CAC1B File Offset: 0x005C8E1B
		public void SetZoomOutButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._zoomOutButton = new MinimapFrame.Button(hoverTexture, position, delegate()
			{
				this.ZoomOutButton();
			});
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x005CAC36 File Offset: 0x005C8E36
		private void ZoomOutButton()
		{
			Main.mapMinimapScale *= 0.975f;
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x005CAC48 File Offset: 0x005C8E48
		public void Update()
		{
			MinimapFrame.Button buttonUnderMouse = this.GetButtonUnderMouse();
			this._zoomInButton.IsHighlighted = (buttonUnderMouse == this._zoomInButton);
			this._zoomOutButton.IsHighlighted = (buttonUnderMouse == this._zoomOutButton);
			this._resetButton.IsHighlighted = (buttonUnderMouse == this._resetButton);
			if (buttonUnderMouse == null || Main.LocalPlayer.lastMouseInterface)
			{
				return;
			}
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

		// Token: 0x06003D85 RID: 15749 RVA: 0x005CACEC File Offset: 0x005C8EEC
		public void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)this.MinimapPosition.X - 6, (int)this.MinimapPosition.Y - 6, 244, 244), Color.Black * Main.mapMinimapAlpha);
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x005CAD44 File Offset: 0x005C8F44
		public void DrawForeground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this._frameTexture.Value, this.FramePosition, Color.White);
			this._zoomInButton.Draw(spriteBatch, this.FramePosition);
			this._zoomOutButton.Draw(spriteBatch, this.FramePosition);
			this._resetButton.Draw(spriteBatch, this.FramePosition);
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x005CADA4 File Offset: 0x005C8FA4
		private MinimapFrame.Button GetButtonUnderMouse()
		{
			Vector2 testPoint;
			testPoint..ctor((float)Main.mouseX, (float)Main.mouseY);
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

		// Token: 0x06003D88 RID: 15752 RVA: 0x005CAE16 File Offset: 0x005C9016
		[Conditional("DEBUG")]
		private void ValidateState()
		{
		}

		// Token: 0x0400564E RID: 22094
		private const float DEFAULT_ZOOM = 1.05f;

		// Token: 0x0400564F RID: 22095
		private const float ZOOM_OUT_MULTIPLIER = 0.975f;

		// Token: 0x04005650 RID: 22096
		private const float ZOOM_IN_MULTIPLIER = 1.025f;

		// Token: 0x04005651 RID: 22097
		private readonly Asset<Texture2D> _frameTexture;

		// Token: 0x04005652 RID: 22098
		private readonly Vector2 _frameOffset;

		// Token: 0x04005653 RID: 22099
		private MinimapFrame.Button _resetButton;

		// Token: 0x04005654 RID: 22100
		private MinimapFrame.Button _zoomInButton;

		// Token: 0x04005655 RID: 22101
		private MinimapFrame.Button _zoomOutButton;

		// Token: 0x02000C0C RID: 3084
		private class Button
		{
			// Token: 0x17000958 RID: 2392
			// (get) Token: 0x06005EDB RID: 24283 RVA: 0x006CCE32 File Offset: 0x006CB032
			private Vector2 Size
			{
				get
				{
					return new Vector2((float)this._hoverTexture.Width(), (float)this._hoverTexture.Height());
				}
			}

			// Token: 0x06005EDC RID: 24284 RVA: 0x006CCE51 File Offset: 0x006CB051
			public Button(Asset<Texture2D> hoverTexture, Vector2 position, Action mouseDownCallback)
			{
				this._position = position;
				this._hoverTexture = hoverTexture;
				this._onMouseDown = mouseDownCallback;
			}

			// Token: 0x06005EDD RID: 24285 RVA: 0x006CCE6E File Offset: 0x006CB06E
			public void Click()
			{
				this._onMouseDown();
			}

			// Token: 0x06005EDE RID: 24286 RVA: 0x006CCE7B File Offset: 0x006CB07B
			public void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
			{
				if (this.IsHighlighted)
				{
					spriteBatch.Draw(this._hoverTexture.Value, this._position + parentPosition, Color.White);
				}
			}

			// Token: 0x06005EDF RID: 24287 RVA: 0x006CCEA8 File Offset: 0x006CB0A8
			public bool IsTouchingPoint(Vector2 testPoint, Vector2 parentPosition)
			{
				Vector2 vector = this._position + parentPosition + this.Size * 0.5f;
				Vector2 vector2 = Vector2.Max(this.Size, new Vector2(22f, 22f)) * 0.5f;
				Vector2 vector3 = testPoint - vector;
				return Math.Abs(vector3.X) < vector2.X && Math.Abs(vector3.Y) < vector2.Y;
			}

			// Token: 0x04007849 RID: 30793
			public bool IsHighlighted;

			// Token: 0x0400784A RID: 30794
			private readonly Vector2 _position;

			// Token: 0x0400784B RID: 30795
			private readonly Asset<Texture2D> _hoverTexture;

			// Token: 0x0400784C RID: 30796
			private readonly Action _onMouseDown;
		}
	}
}
