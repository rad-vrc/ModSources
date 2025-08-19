using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200052F RID: 1327
	public class UISelectableTextPanel<T> : UITextPanel<T>
	{
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x005D8D01 File Offset: 0x005D6F01
		// (set) Token: 0x06003F4E RID: 16206 RVA: 0x005D8D09 File Offset: 0x005D6F09
		public Func<UISelectableTextPanel<T>, bool> IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				this._isSelected = value;
			}
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x005D8D12 File Offset: 0x005D6F12
		public UISelectableTextPanel(T text, float textScale = 1f, bool large = false) : base(text, textScale, large)
		{
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale");
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder");
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x005D8D48 File Offset: 0x005D6F48
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				int num = 4;
				int num2 = 10;
				int num3 = 10;
				Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
				if (this.IsSelected != null && this.IsSelected(this))
				{
					Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X + num, (int)dimensions.Y + num, (int)dimensions.Width - num * 2, (int)dimensions.Height - num * 2, num2, num2, num3, num3, Color.Lerp(this._color, Color.White, 0.7f) * 0.5f);
				}
				if (base.IsMouseHovering)
				{
					Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.White);
				}
			}
			base.DrawText(spriteBatch);
		}

		// Token: 0x040057B4 RID: 22452
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x040057B5 RID: 22453
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040057B6 RID: 22454
		private Func<UISelectableTextPanel<T>, bool> _isSelected;
	}
}
