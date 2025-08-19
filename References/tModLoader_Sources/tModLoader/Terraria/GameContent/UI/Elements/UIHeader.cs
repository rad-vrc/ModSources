using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000519 RID: 1305
	public class UIHeader : UIElement
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x005D4CE1 File Offset: 0x005D2EE1
		// (set) Token: 0x06003EBA RID: 16058 RVA: 0x005D4CEC File Offset: 0x005D2EEC
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

		// Token: 0x06003EBB RID: 16059 RVA: 0x005D4D73 File Offset: 0x005D2F73
		public UIHeader()
		{
			this.Text = "";
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x005D4D86 File Offset: 0x005D2F86
		public UIHeader(string text)
		{
			this.Text = text;
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x005D4D98 File Offset: 0x005D2F98
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

		// Token: 0x04005747 RID: 22343
		private string _text;
	}
}
