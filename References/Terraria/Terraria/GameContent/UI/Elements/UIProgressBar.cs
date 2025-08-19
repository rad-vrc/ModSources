using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000397 RID: 919
	public class UIProgressBar : UIElement
	{
		// Token: 0x06002984 RID: 10628 RVA: 0x00593AED File Offset: 0x00591CED
		public UIProgressBar()
		{
			this._progressBar.Height.Precent = 1f;
			this._progressBar.Recalculate();
			base.Append(this._progressBar);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x00593B2C File Offset: 0x00591D2C
		public void SetProgress(float value)
		{
			this._targetProgress = value;
			if (value < this._visualProgress)
			{
				this._visualProgress = value;
			}
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x00593B48 File Offset: 0x00591D48
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			this._visualProgress = this._visualProgress * 0.95f + 0.05f * this._targetProgress;
			this._progressBar.Width.Precent = this._visualProgress;
			this._progressBar.Recalculate();
		}

		// Token: 0x04004C96 RID: 19606
		private UIProgressBar.UIInnerProgressBar _progressBar = new UIProgressBar.UIInnerProgressBar();

		// Token: 0x04004C97 RID: 19607
		private float _visualProgress;

		// Token: 0x04004C98 RID: 19608
		private float _targetProgress;

		// Token: 0x02000756 RID: 1878
		private class UIInnerProgressBar : UIElement
		{
			// Token: 0x060038B7 RID: 14519 RVA: 0x00614100 File Offset: 0x00612300
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Vector2(dimensions.X, dimensions.Y), null, Color.Blue, 0f, Vector2.Zero, new Vector2(dimensions.Width, dimensions.Height / 1000f), SpriteEffects.None, 0f);
			}
		}
	}
}
