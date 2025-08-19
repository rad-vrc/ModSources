using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200025E RID: 606
	internal class UIProgressBar : UIPanel
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x0051D5E6 File Offset: 0x0051B7E6
		// (set) Token: 0x06002A9A RID: 10906 RVA: 0x0051D604 File Offset: 0x0051B804
		public string DisplayText
		{
			get
			{
				UIAutoScaleTextTextPanel<string> textPanel = this._textPanel;
				return ((textPanel != null) ? textPanel.Text : null) ?? this._cachedText;
			}
			set
			{
				if (this._textPanel == null)
				{
					this._cachedText = value;
					return;
				}
				this._textPanel.SetText(value ?? this._textPanel.Text);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06002A9B RID: 10907 RVA: 0x0051D631 File Offset: 0x0051B831
		// (set) Token: 0x06002A9C RID: 10908 RVA: 0x0051D639 File Offset: 0x0051B839
		public float Progress { get; private set; }

		// Token: 0x06002A9D RID: 10909 RVA: 0x0051D644 File Offset: 0x0051B844
		public override void OnInitialize()
		{
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(this._cachedText ?? "", 1f, true);
			uiautoScaleTextTextPanel.Top.Pixels = 10f;
			uiautoScaleTextTextPanel.HAlign = 0.5f;
			uiautoScaleTextTextPanel.Width.Percent = 1f;
			uiautoScaleTextTextPanel.Height.Pixels = 60f;
			uiautoScaleTextTextPanel.DrawPanel = false;
			this._textPanel = uiautoScaleTextTextPanel;
			base.Append(this._textPanel);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0051D6BF File Offset: 0x0051B8BF
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (!string.IsNullOrEmpty(this._cachedText) && this._textPanel != null)
			{
				this._textPanel.SetText(this._cachedText);
				this._cachedText = string.Empty;
			}
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0051D6FC File Offset: 0x0051B8FC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle space = base.GetInnerDimensions();
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)space.X + 10, (int)space.Y + (int)space.Height / 2 + 20, (int)space.Width - 20, 10), new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(0, 0, 70));
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)space.X + 10, (int)space.Y + (int)space.Height / 2 + 20, (int)((space.Width - 20f) * this.Progress), 10), new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(200, 200, 70));
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0051D7D5 File Offset: 0x0051B9D5
		public void UpdateProgress(float value)
		{
			this.Progress = value;
		}

		// Token: 0x04001B39 RID: 6969
		private string _cachedText = "";

		// Token: 0x04001B3B RID: 6971
		private UIAutoScaleTextTextPanel<string> _textPanel;
	}
}
