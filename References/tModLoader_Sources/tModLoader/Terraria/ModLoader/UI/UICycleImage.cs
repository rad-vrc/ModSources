using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000242 RID: 578
	public class UICycleImage : UIElement
	{
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06002964 RID: 10596 RVA: 0x00511F80 File Offset: 0x00510180
		// (remove) Token: 0x06002965 RID: 10597 RVA: 0x00511FB8 File Offset: 0x005101B8
		public event EventHandler OnStateChanged;

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x00511FED File Offset: 0x005101ED
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x00511FF5 File Offset: 0x005101F5
		public int CurrentState
		{
			get
			{
				return this._currentState;
			}
			set
			{
				if (value != this._currentState)
				{
					this._currentState = value;
					EventHandler onStateChanged = this.OnStateChanged;
					if (onStateChanged == null)
					{
						return;
					}
					onStateChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x0051201D File Offset: 0x0051021D
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x00512025 File Offset: 0x00510225
		public bool Disabled
		{
			get
			{
				return this.disabled;
			}
			set
			{
				this.disabled = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x0051202E File Offset: 0x0051022E
		protected int DrawHeight
		{
			get
			{
				return (int)this.Height.Pixels;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x0051203C File Offset: 0x0051023C
		protected int DrawWidth
		{
			get
			{
				return (int)this.Width.Pixels;
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0051204C File Offset: 0x0051024C
		public UICycleImage(Asset<Texture2D> texture, int states, int width, int height, int textureOffsetX, int textureOffsetY, int padding = 2)
		{
			this._texture = texture;
			this._textureOffsetX = textureOffsetX;
			this._textureOffsetY = textureOffsetY;
			this.Width.Pixels = (float)width;
			this.Height.Pixels = (float)height;
			this._states = states;
			this._padding = padding;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x005120A0 File Offset: 0x005102A0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point;
			point..ctor(this._textureOffsetX, this._textureOffsetY + (this._padding + this.DrawHeight) * this._currentState);
			Color color = base.IsMouseHovering ? Color.White : Color.Silver;
			if (this.disabled)
			{
				color..ctor(100, 100, 100);
			}
			spriteBatch.Draw(this._texture.Value, new Rectangle((int)dimensions.X, (int)dimensions.Y, this.DrawWidth, this.DrawHeight), new Rectangle?(new Rectangle(point.X, point.Y, this.DrawWidth, this.DrawHeight)), color);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00512159 File Offset: 0x00510359
		public override void LeftClick(UIMouseEvent evt)
		{
			if (this.disabled)
			{
				return;
			}
			this.CurrentState = (this._currentState + 1) % this._states;
			base.LeftClick(evt);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00512180 File Offset: 0x00510380
		public override void RightClick(UIMouseEvent evt)
		{
			if (this.disabled)
			{
				return;
			}
			this.CurrentState = (this._currentState + this._states - 1) % this._states;
			base.RightClick(evt);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x005121AE File Offset: 0x005103AE
		internal void SetCurrentState(int state)
		{
			this.CurrentState = state;
		}

		// Token: 0x04001A3D RID: 6717
		private readonly Asset<Texture2D> _texture;

		// Token: 0x04001A3E RID: 6718
		private readonly int _padding;

		// Token: 0x04001A3F RID: 6719
		private readonly int _textureOffsetX;

		// Token: 0x04001A40 RID: 6720
		private readonly int _textureOffsetY;

		// Token: 0x04001A41 RID: 6721
		private readonly int _states;

		// Token: 0x04001A43 RID: 6723
		private int _currentState;

		// Token: 0x04001A44 RID: 6724
		private bool disabled;
	}
}
