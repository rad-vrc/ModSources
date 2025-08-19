using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200051E RID: 1310
	public class UIImageFramed : UIElement, IColorable
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06003ED5 RID: 16085 RVA: 0x005D56A6 File Offset: 0x005D38A6
		// (set) Token: 0x06003ED6 RID: 16086 RVA: 0x005D56AE File Offset: 0x005D38AE
		public Color Color { get; set; }

		// Token: 0x06003ED7 RID: 16087 RVA: 0x005D56B8 File Offset: 0x005D38B8
		public UIImageFramed(Asset<Texture2D> texture, Rectangle frame)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
			this.Color = Color.White;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x005D571C File Offset: 0x005D391C
		public void SetImage(Asset<Texture2D> texture, Rectangle frame)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x005D5770 File Offset: 0x005D3970
		public void SetFrame(Rectangle frame)
		{
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x005D57BC File Offset: 0x005D39BC
		public void SetFrame(int frameCountHorizontal, int frameCountVertical, int frameX, int frameY, int sizeOffsetX, int sizeOffsetY)
		{
			this.SetFrame(this._texture.Frame(frameCountHorizontal, frameCountVertical, frameX, frameY, 0, 0).OffsetSize(sizeOffsetX, sizeOffsetY));
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x005D57E0 File Offset: 0x005D39E0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Vector2 vector = base.GetDimensions().Position();
			spriteBatch.Draw(this._texture.Value, vector, new Rectangle?(this._frame), this.Color);
		}

		// Token: 0x04005763 RID: 22371
		private Asset<Texture2D> _texture;

		// Token: 0x04005764 RID: 22372
		private Rectangle _frame;
	}
}
