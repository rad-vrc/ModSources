using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200025C RID: 604
	internal class UIModStateText : UIElement
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x0051D09E File Offset: 0x0051B29E
		private string DisplayText
		{
			get
			{
				if (!this._enabled)
				{
					return Language.GetTextValue("GameUI.Disabled");
				}
				return Language.GetTextValue("GameUI.Enabled");
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x0051D0BD File Offset: 0x0051B2BD
		private Color DisplayColor
		{
			get
			{
				if (!this._enabled)
				{
					return Color.Red;
				}
				return Color.Green;
			}
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x0051D0D4 File Offset: 0x0051B2D4
		public UIModStateText(bool enabled = true)
		{
			this._enabled = enabled;
			this.PaddingLeft = (this.PaddingRight = 5f);
			this.PaddingBottom = (this.PaddingTop = 10f);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0051D116 File Offset: 0x0051B316
		public void SetEnabled()
		{
			this._enabled = true;
			this.Recalculate();
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x0051D125 File Offset: 0x0051B325
		public void SetDisabled()
		{
			this._enabled = false;
			this.Recalculate();
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0051D134 File Offset: 0x0051B334
		public override void Recalculate()
		{
			Vector2 textSize;
			textSize..ctor(FontAssets.MouseText.Value.MeasureString(this.DisplayText).X, 16f);
			this.Width.Set(textSize.X + this.PaddingLeft + this.PaddingRight, 0f);
			this.Height.Set(textSize.Y + this.PaddingTop + this.PaddingBottom, 0f);
			base.Recalculate();
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0051D1B5 File Offset: 0x0051B3B5
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.DrawPanel(spriteBatch);
			this.DrawEnabledText(spriteBatch);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0051D1CC File Offset: 0x0051B3CC
		private void DrawPanel(SpriteBatch spriteBatch)
		{
			Vector2 position = base.GetDimensions().Position();
			float width = this.Width.Pixels;
			spriteBatch.Draw(UICommon.InnerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, UICommon.InnerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(UICommon.InnerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, UICommon.InnerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), 0, 0f);
			spriteBatch.Draw(UICommon.InnerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, UICommon.InnerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0051D2D4 File Offset: 0x0051B4D4
		private void DrawEnabledText(SpriteBatch spriteBatch)
		{
			Vector2 pos = base.GetDimensions().Position() + new Vector2(this.PaddingLeft, this.PaddingTop * 0.5f);
			Utils.DrawBorderString(spriteBatch, this.DisplayText, pos, this.DisplayColor, 1f, 0f, 0f, -1);
		}

		// Token: 0x04001B32 RID: 6962
		private bool _enabled;
	}
}
