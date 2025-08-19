using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000529 RID: 1321
	public class UIProgressBar : UIElement
	{
		// Token: 0x06003F13 RID: 16147 RVA: 0x005D7AAE File Offset: 0x005D5CAE
		public UIProgressBar()
		{
			this._progressBar.Height.Precent = 1f;
			this._progressBar.Recalculate();
			base.Append(this._progressBar);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x005D7AED File Offset: 0x005D5CED
		public void SetProgress(float value)
		{
			this._targetProgress = value;
			if (value < this._visualProgress)
			{
				this._visualProgress = value;
			}
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x005D7B08 File Offset: 0x005D5D08
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this._visualProgress = this._visualProgress * 0.95f + 0.05f * this._targetProgress;
			this._progressBar.Width.Precent = this._visualProgress;
			this._progressBar.Recalculate();
		}

		// Token: 0x0400578D RID: 22413
		private UIProgressBar.UIInnerProgressBar _progressBar = new UIProgressBar.UIInnerProgressBar();

		// Token: 0x0400578E RID: 22414
		private float _visualProgress;

		// Token: 0x0400578F RID: 22415
		private float _targetProgress;

		// Token: 0x02000C1F RID: 3103
		private class UIInnerProgressBar : UIElement
		{
			// Token: 0x06005F16 RID: 24342 RVA: 0x006CD25C File Offset: 0x006CB45C
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(dimensions.X, dimensions.Y), null, Color.Blue, 0f, Vector2.Zero, new Vector2(dimensions.Width, dimensions.Height / 1000f), 0, 0f);
			}
		}
	}
}
