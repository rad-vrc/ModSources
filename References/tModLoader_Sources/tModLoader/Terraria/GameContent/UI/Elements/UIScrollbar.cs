using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200052C RID: 1324
	public class UIScrollbar : UIElement
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003F26 RID: 16166 RVA: 0x005D8096 File Offset: 0x005D6296
		// (set) Token: 0x06003F27 RID: 16167 RVA: 0x005D809E File Offset: 0x005D629E
		public float ViewPosition
		{
			get
			{
				return this._viewPosition;
			}
			set
			{
				this._viewPosition = MathHelper.Clamp(value, 0f, this._maxViewSize - this._viewSize);
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x005D80BE File Offset: 0x005D62BE
		public bool CanScroll
		{
			get
			{
				return this._maxViewSize != this._viewSize;
			}
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x005D80D1 File Offset: 0x005D62D1
		public void GoToBottom()
		{
			this.ViewPosition = this._maxViewSize - this._viewSize;
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x005D80E8 File Offset: 0x005D62E8
		public UIScrollbar()
		{
			this.Width.Set(20f, 0f);
			this.MaxWidth.Set(20f, 0f);
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/Scrollbar");
			this._innerTexture = Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner");
			this.PaddingTop = 5f;
			this.PaddingBottom = 5f;
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x005D817B File Offset: 0x005D637B
		public void SetView(float viewSize, float maxViewSize)
		{
			viewSize = MathHelper.Clamp(viewSize, 0f, maxViewSize);
			this._viewPosition = MathHelper.Clamp(this._viewPosition, 0f, maxViewSize - viewSize);
			this._viewSize = viewSize;
			this._maxViewSize = maxViewSize;
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x005D81B2 File Offset: 0x005D63B2
		public float GetValue()
		{
			return this._viewPosition;
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x005D81BC File Offset: 0x005D63BC
		private Rectangle GetHandleRectangle()
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			if (this._maxViewSize == 0f && this._viewSize == 0f)
			{
				this._viewSize = 1f;
				this._maxViewSize = 1f;
			}
			return new Rectangle((int)innerDimensions.X, (int)(innerDimensions.Y + innerDimensions.Height * (this._viewPosition / this._maxViewSize)) - 3, 20, (int)(innerDimensions.Height * (this._viewSize / this._maxViewSize)) + 7);
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x005D8244 File Offset: 0x005D6444
		internal void DrawBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle dimensions, Color color)
		{
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y - 6, dimensions.Width, 6), new Rectangle?(new Rectangle(0, 0, texture.Width, 6)), color);
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y, dimensions.Width, dimensions.Height), new Rectangle?(new Rectangle(0, 6, texture.Width, 4)), color);
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y + dimensions.Height, dimensions.Width, 6), new Rectangle?(new Rectangle(0, texture.Height - 6, texture.Width, 6)), color);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x005D8304 File Offset: 0x005D6504
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			if (this._isDragging)
			{
				float num = UserInterface.ActiveInstance.MousePosition.Y - innerDimensions.Y - this._dragYOffset;
				this._viewPosition = MathHelper.Clamp(num / innerDimensions.Height * this._maxViewSize, 0f, this._maxViewSize - this._viewSize);
			}
			Rectangle handleRectangle = this.GetHandleRectangle();
			Vector2 mousePosition = UserInterface.ActiveInstance.MousePosition;
			bool isHoveringOverHandle = this._isHoveringOverHandle;
			this._isHoveringOverHandle = handleRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));
			if (!isHoveringOverHandle && this._isHoveringOverHandle && Main.hasFocus)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			this.DrawBar(spriteBatch, this._texture.Value, dimensions.ToRectangle(), Color.White);
			this.DrawBar(spriteBatch, this._innerTexture.Value, handleRectangle, Color.White * ((this._isDragging || this._isHoveringOverHandle) ? 1f : 0.85f));
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x005D8428 File Offset: 0x005D6628
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
			if (evt.Target == this)
			{
				Rectangle handleRectangle = this.GetHandleRectangle();
				if (handleRectangle.Contains(new Point((int)evt.MousePosition.X, (int)evt.MousePosition.Y)))
				{
					this._isDragging = true;
					this._dragYOffset = evt.MousePosition.Y - (float)handleRectangle.Y;
					return;
				}
				CalculatedStyle innerDimensions = base.GetInnerDimensions();
				float num = UserInterface.ActiveInstance.MousePosition.Y - innerDimensions.Y - (float)(handleRectangle.Height >> 1);
				this._viewPosition = MathHelper.Clamp(num / innerDimensions.Height * this._maxViewSize, 0f, this._maxViewSize - this._viewSize);
			}
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x005D84EA File Offset: 0x005D66EA
		public override void LeftMouseUp(UIMouseEvent evt)
		{
			base.LeftMouseUp(evt);
			this._isDragging = false;
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x005D84FA File Offset: 0x005D66FA
		public float ViewSize
		{
			get
			{
				return this._viewSize;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x005D8502 File Offset: 0x005D6702
		public float MaxViewSize
		{
			get
			{
				return this._maxViewSize;
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x005D850A File Offset: 0x005D670A
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (base.IsMouseHovering)
			{
				PlayerInput.LockVanillaMouseScroll("ModLoader/UIList");
			}
		}

		// Token: 0x040057A1 RID: 22433
		private float _viewPosition;

		// Token: 0x040057A2 RID: 22434
		private float _viewSize = 1f;

		// Token: 0x040057A3 RID: 22435
		private float _maxViewSize = 20f;

		// Token: 0x040057A4 RID: 22436
		private bool _isDragging;

		// Token: 0x040057A5 RID: 22437
		private bool _isHoveringOverHandle;

		// Token: 0x040057A6 RID: 22438
		private float _dragYOffset;

		// Token: 0x040057A7 RID: 22439
		private Asset<Texture2D> _texture;

		// Token: 0x040057A8 RID: 22440
		private Asset<Texture2D> _innerTexture;
	}
}
