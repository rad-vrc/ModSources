using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200038C RID: 908
	public class UIImageFramed : UIElement, IColorable
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x00590908 File Offset: 0x0058EB08
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x00590910 File Offset: 0x0058EB10
		public Color Color { get; set; }

		// Token: 0x06002905 RID: 10501 RVA: 0x0059091C File Offset: 0x0058EB1C
		public UIImageFramed(Asset<Texture2D> texture, Rectangle frame)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
			this.Color = Color.White;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00590980 File Offset: 0x0058EB80
		public void SetImage(Asset<Texture2D> texture, Rectangle frame)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x005909D4 File Offset: 0x0058EBD4
		public void SetFrame(Rectangle frame)
		{
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00590A20 File Offset: 0x0058EC20
		public void SetFrame(int frameCountHorizontal, int frameCountVertical, int frameX, int frameY, int sizeOffsetX, int sizeOffsetY)
		{
			this.SetFrame(this._texture.Frame(frameCountHorizontal, frameCountVertical, frameX, frameY, 0, 0).OffsetSize(sizeOffsetX, sizeOffsetY));
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x00590A44 File Offset: 0x0058EC44
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.Position(), new Rectangle?(this._frame), this.Color);
		}

		// Token: 0x04004C44 RID: 19524
		private Asset<Texture2D> _texture;

		// Token: 0x04004C45 RID: 19525
		private Rectangle _frame;
	}
}
