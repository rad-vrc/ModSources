using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200038F RID: 911
	public class UIPanel : UIElement
	{
		// Token: 0x0600291E RID: 10526 RVA: 0x00590DE8 File Offset: 0x0058EFE8
		public UIPanel()
		{
			if (this._borderTexture == null)
			{
				this._borderTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBorder", 1);
			}
			if (this._backgroundTexture == null)
			{
				this._backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBackground", 1);
			}
			base.SetPadding((float)this._cornerSize);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x00590E7C File Offset: 0x0058F07C
		public UIPanel(Asset<Texture2D> customBackground, Asset<Texture2D> customborder, int customCornerSize = 12, int customBarSize = 4)
		{
			if (this._borderTexture == null)
			{
				this._borderTexture = customborder;
			}
			if (this._backgroundTexture == null)
			{
				this._backgroundTexture = customBackground;
			}
			this._cornerSize = customCornerSize;
			this._barSize = customBarSize;
			base.SetPadding((float)this._cornerSize);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x00590F04 File Offset: 0x0058F104
		private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width - this._cornerSize, point.Y + (int)dimensions.Height - this._cornerSize);
			int width = point2.X - point.X - this._cornerSize;
			int height = point2.Y - point.Y - this._cornerSize;
			spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(0, 0, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize + this._barSize, 0, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X, point2.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(0, this._cornerSize + this._barSize, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point2.Y, this._cornerSize, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize + this._barSize, this._cornerSize + this._barSize, this._cornerSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + this._cornerSize, point.Y, width, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize, 0, this._barSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + this._cornerSize, point2.Y, width, this._cornerSize), new Rectangle?(new Rectangle(this._cornerSize, this._cornerSize + this._barSize, this._barSize, this._cornerSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X, point.Y + this._cornerSize, this._cornerSize, height), new Rectangle?(new Rectangle(0, this._cornerSize, this._cornerSize, this._barSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y + this._cornerSize, this._cornerSize, height), new Rectangle?(new Rectangle(this._cornerSize + this._barSize, this._cornerSize, this._cornerSize, this._barSize)), color);
			spriteBatch.Draw(texture, new Rectangle(point.X + this._cornerSize, point.Y + this._cornerSize, width, height), new Rectangle?(new Rectangle(this._cornerSize, this._cornerSize, this._barSize, this._barSize)), color);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x00591224 File Offset: 0x0058F424
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._backgroundTexture != null)
			{
				this.DrawPanel(spriteBatch, this._backgroundTexture.Value, this.BackgroundColor);
			}
			if (this._borderTexture != null)
			{
				this.DrawPanel(spriteBatch, this._borderTexture.Value, this.BorderColor);
			}
		}

		// Token: 0x04004C4D RID: 19533
		private int _cornerSize = 12;

		// Token: 0x04004C4E RID: 19534
		private int _barSize = 4;

		// Token: 0x04004C4F RID: 19535
		private Asset<Texture2D> _borderTexture;

		// Token: 0x04004C50 RID: 19536
		private Asset<Texture2D> _backgroundTexture;

		// Token: 0x04004C51 RID: 19537
		public Color BorderColor = Color.Black;

		// Token: 0x04004C52 RID: 19538
		public Color BackgroundColor = new Color(63, 82, 151) * 0.7f;
	}
}
