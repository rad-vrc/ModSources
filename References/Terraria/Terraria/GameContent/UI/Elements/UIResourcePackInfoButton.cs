using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200035D RID: 861
	public class UIResourcePackInfoButton<T> : UITextPanel<T>
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x0058539A File Offset: 0x0058359A
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x005853A2 File Offset: 0x005835A2
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

		// Token: 0x060027B7 RID: 10167 RVA: 0x005853AB File Offset: 0x005835AB
		public UIResourcePackInfoButton(T text, float textScale = 1f, bool large = false) : base(text, textScale, large)
		{
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x005853E4 File Offset: 0x005835E4
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

		// Token: 0x04004B0E RID: 19214
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004B0F RID: 19215
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004B10 RID: 19216
		private ResourcePack _resourcePack;
	}
}
