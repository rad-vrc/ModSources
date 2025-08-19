using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000390 RID: 912
	public class UIScrollbar : UIElement
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x00591271 File Offset: 0x0058F471
		// (set) Token: 0x06002923 RID: 10531 RVA: 0x00591279 File Offset: 0x0058F479
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x00591299 File Offset: 0x0058F499
		public bool CanScroll
		{
			get
			{
				return this._maxViewSize != this._viewSize;
			}
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x005912AC File Offset: 0x0058F4AC
		public void GoToBottom()
		{
			this.ViewPosition = this._maxViewSize - this._viewSize;
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x005912C4 File Offset: 0x0058F4C4
		public UIScrollbar()
		{
			this.Width.Set(20f, 0f);
			this.MaxWidth.Set(20f, 0f);
			this._texture = Main.Assets.Request<Texture2D>("Images/UI/Scrollbar", 1);
			this._innerTexture = Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner", 1);
			this.PaddingTop = 5f;
			this.PaddingBottom = 5f;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00591359 File Offset: 0x0058F559
		public void SetView(float viewSize, float maxViewSize)
		{
			viewSize = MathHelper.Clamp(viewSize, 0f, maxViewSize);
			this._viewPosition = MathHelper.Clamp(this._viewPosition, 0f, maxViewSize - viewSize);
			this._viewSize = viewSize;
			this._maxViewSize = maxViewSize;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x00591271 File Offset: 0x0058F471
		public float GetValue()
		{
			return this._viewPosition;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x00591390 File Offset: 0x0058F590
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

		// Token: 0x0600292A RID: 10538 RVA: 0x00591418 File Offset: 0x0058F618
		private void DrawBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle dimensions, Color color)
		{
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y - 6, dimensions.Width, 6), new Rectangle?(new Rectangle(0, 0, texture.Width, 6)), color);
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y, dimensions.Width, dimensions.Height), new Rectangle?(new Rectangle(0, 6, texture.Width, 4)), color);
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y + dimensions.Height, dimensions.Width, 6), new Rectangle?(new Rectangle(0, texture.Height - 6, texture.Width, 6)), color);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x005914D8 File Offset: 0x0058F6D8
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

		// Token: 0x0600292C RID: 10540 RVA: 0x005915FC File Offset: 0x0058F7FC
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

		// Token: 0x0600292D RID: 10541 RVA: 0x005916BE File Offset: 0x0058F8BE
		public override void LeftMouseUp(UIMouseEvent evt)
		{
			base.LeftMouseUp(evt);
			this._isDragging = false;
		}

		// Token: 0x04004C53 RID: 19539
		private float _viewPosition;

		// Token: 0x04004C54 RID: 19540
		private float _viewSize = 1f;

		// Token: 0x04004C55 RID: 19541
		private float _maxViewSize = 20f;

		// Token: 0x04004C56 RID: 19542
		private bool _isDragging;

		// Token: 0x04004C57 RID: 19543
		private bool _isHoveringOverHandle;

		// Token: 0x04004C58 RID: 19544
		private float _dragYOffset;

		// Token: 0x04004C59 RID: 19545
		private Asset<Texture2D> _texture;

		// Token: 0x04004C5A RID: 19546
		private Asset<Texture2D> _innerTexture;
	}
}
