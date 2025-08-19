using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000398 RID: 920
	public class UIHeader : UIElement
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x00593B95 File Offset: 0x00591D95
		// (set) Token: 0x06002988 RID: 10632 RVA: 0x00593BA0 File Offset: 0x00591DA0
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this._text != value)
				{
					this._text = value;
					if (!Main.dedServ)
					{
						Vector2 vector = FontAssets.DeathText.Value.MeasureString(this.Text);
						this.Width.Pixels = vector.X;
						this.Height.Pixels = vector.Y;
					}
					this.Width.Precent = 0f;
					this.Height.Precent = 0f;
					this.Recalculate();
				}
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x00593C27 File Offset: 0x00591E27
		public UIHeader()
		{
			this.Text = "";
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x00593C3A File Offset: 0x00591E3A
		public UIHeader(string text)
		{
			this.Text = text;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x00593C4C File Offset: 0x00591E4C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			float num = 1.2f;
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X - num, dimensions.Y - num), Color.Black);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X + num, dimensions.Y - num), Color.Black);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X - num, dimensions.Y + num), Color.Black);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X + num, dimensions.Y + num), Color.Black);
			if (WorldGen.tenthAnniversaryWorldGen && !WorldGen.remixWorldGen)
			{
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X, dimensions.Y), Color.HotPink);
				return;
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.DeathText.Value, this.Text, new Vector2(dimensions.X, dimensions.Y), Color.White);
		}

		// Token: 0x04004C99 RID: 19609
		private string _text;
	}
}
