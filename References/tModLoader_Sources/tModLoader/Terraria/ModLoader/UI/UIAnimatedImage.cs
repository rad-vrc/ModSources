using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200023B RID: 571
	public class UIAnimatedImage : UIElement
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x0050FDDC File Offset: 0x0050DFDC
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x0050FDE4 File Offset: 0x0050DFE4
		public int FrameStart { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x0050FDED File Offset: 0x0050DFED
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x0050FDF5 File Offset: 0x0050DFF5
		public int FrameCount { get; set; } = 1;

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x0050FDFE File Offset: 0x0050DFFE
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x0050FE06 File Offset: 0x0050E006
		public int TicksPerFrame { get; set; } = 5;

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0050FE0F File Offset: 0x0050E00F
		protected int DrawHeight
		{
			get
			{
				return (int)this.Height.Pixels;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x0050FE1D File Offset: 0x0050E01D
		protected int DrawWidth
		{
			get
			{
				return (int)this.Width.Pixels;
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0050FE2C File Offset: 0x0050E02C
		public UIAnimatedImage(Asset<Texture2D> texture, int width, int height, int textureOffsetX, int textureOffsetY, int countX, int countY, int padding = 2)
		{
			this._texture = texture;
			this._textureOffsetX = textureOffsetX;
			this._textureOffsetY = textureOffsetY;
			this._countX = countX;
			this._countY = countY;
			this.Width.Pixels = (float)width;
			this.Height.Pixels = (float)height;
			this._padding = padding;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0050FE98 File Offset: 0x0050E098
		private Rectangle FrameToRect(int frame)
		{
			if (frame < 0 || frame >= this._countX * this._countY)
			{
				return new Rectangle(0, 0, 0, 0);
			}
			int x = frame % this._countX;
			int y = frame / this._countX;
			return new Rectangle(this._textureOffsetX + (this._padding + this.DrawHeight) * x, this._textureOffsetY + (this._padding + this.DrawHeight) * y, this.DrawWidth, this.DrawHeight);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0050FF14 File Offset: 0x0050E114
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			int num = this._tickCounter + 1;
			this._tickCounter = num;
			if (num >= this.TicksPerFrame)
			{
				this._tickCounter = 0;
				num = this._frameCounter + 1;
				this._frameCounter = num;
				if (num >= this.FrameCount)
				{
					this._frameCounter = 0;
				}
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0050FF68 File Offset: 0x0050E168
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Color color = base.IsMouseHovering ? Color.White : Color.Silver;
			int frame = this.FrameStart + this._frameCounter % this.FrameCount;
			spriteBatch.Draw(this._texture.Value, dimensions.ToRectangle(), new Rectangle?(this.FrameToRect(frame)), color);
		}

		// Token: 0x040019E4 RID: 6628
		private readonly Asset<Texture2D> _texture;

		// Token: 0x040019E5 RID: 6629
		private readonly int _padding;

		// Token: 0x040019E6 RID: 6630
		private readonly int _textureOffsetX;

		// Token: 0x040019E7 RID: 6631
		private readonly int _textureOffsetY;

		// Token: 0x040019E8 RID: 6632
		private readonly int _countX;

		// Token: 0x040019E9 RID: 6633
		private readonly int _countY;

		// Token: 0x040019ED RID: 6637
		private int _tickCounter;

		// Token: 0x040019EE RID: 6638
		private int _frameCounter;
	}
}
