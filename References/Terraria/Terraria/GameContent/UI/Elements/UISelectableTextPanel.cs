using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200035E RID: 862
	public class UISelectableTextPanel<T> : UITextPanel<T>
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x0058549E File Offset: 0x0058369E
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x005854A6 File Offset: 0x005836A6
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

		// Token: 0x060027BB RID: 10171 RVA: 0x005854AF File Offset: 0x005836AF
		public UISelectableTextPanel(T text, float textScale = 1f, bool large = false) : base(text, textScale, large)
		{
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x005854E8 File Offset: 0x005836E8
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

		// Token: 0x04004B11 RID: 19217
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04004B12 RID: 19218
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04004B13 RID: 19219
		private Func<UISelectableTextPanel<T>, bool> _isSelected;
	}
}
