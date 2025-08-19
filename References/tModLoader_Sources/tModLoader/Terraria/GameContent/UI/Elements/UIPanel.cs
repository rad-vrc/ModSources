using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000527 RID: 1319
	public class UIPanel : UIElement
	{
		// Token: 0x06003F08 RID: 16136 RVA: 0x005D754B File Offset: 0x005D574B
		private void LoadTextures()
		{
			if (this._borderTexture == null)
			{
				this._borderTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBorder");
			}
			if (this._backgroundTexture == null)
			{
				this._backgroundTexture = Main.Assets.Request<Texture2D>("Images/UI/PanelBackground");
			}
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x005D7588 File Offset: 0x005D5788
		public UIPanel()
		{
			base.SetPadding((float)this._cornerSize);
			this._needsTextureLoading = true;
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x005D75E8 File Offset: 0x005D57E8
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

		// Token: 0x06003F0B RID: 16139 RVA: 0x005D7670 File Offset: 0x005D5870
		private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point;
			point..ctor((int)dimensions.X, (int)dimensions.Y);
			Point point2;
			point2..ctor(point.X + (int)dimensions.Width - this._cornerSize, point.Y + (int)dimensions.Height - this._cornerSize);
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

		// Token: 0x06003F0C RID: 16140 RVA: 0x005D7990 File Offset: 0x005D5B90
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._needsTextureLoading)
			{
				this._needsTextureLoading = false;
				this.LoadTextures();
			}
			if (this._backgroundTexture != null)
			{
				this.DrawPanel(spriteBatch, this._backgroundTexture.Value, this.BackgroundColor);
			}
			if (this._borderTexture != null)
			{
				this.DrawPanel(spriteBatch, this._borderTexture.Value, this.BorderColor);
			}
		}

		// Token: 0x04005783 RID: 22403
		private int _cornerSize = 12;

		// Token: 0x04005784 RID: 22404
		private int _barSize = 4;

		// Token: 0x04005785 RID: 22405
		private Asset<Texture2D> _borderTexture;

		// Token: 0x04005786 RID: 22406
		private Asset<Texture2D> _backgroundTexture;

		// Token: 0x04005787 RID: 22407
		public Color BorderColor = Color.Black;

		// Token: 0x04005788 RID: 22408
		public Color BackgroundColor = new Color(63, 82, 151) * 0.7f;

		// Token: 0x04005789 RID: 22409
		private bool _needsTextureLoading;
	}
}
