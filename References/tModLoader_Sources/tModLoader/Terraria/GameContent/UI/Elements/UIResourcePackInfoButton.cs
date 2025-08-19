using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200052B RID: 1323
	public class UIResourcePackInfoButton<T> : UITextPanel<T>
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x005D7F95 File Offset: 0x005D6195
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x005D7F9D File Offset: 0x005D619D
		public ResourcePack ResourcePack
		{
			get
			{
				return this._resourcePack;
			}
			set
			{
				this._resourcePack = value;
			}
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x005D7FA6 File Offset: 0x005D61A6
		public UIResourcePackInfoButton(T text, float textScale = 1f, bool large = false) : base(text, textScale, large)
		{
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x005D7FDC File Offset: 0x005D61DC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				int num = 10;
				int num2 = 10;
				Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num, num, num2, num2, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
				if (base.IsMouseHovering)
				{
					Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num, num, num2, num2, Color.White);
				}
			}
			base.DrawText(spriteBatch);
		}

		// Token: 0x0400579E RID: 22430
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x0400579F RID: 22431
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040057A0 RID: 22432
		private ResourcePack _resourcePack;
	}
}
