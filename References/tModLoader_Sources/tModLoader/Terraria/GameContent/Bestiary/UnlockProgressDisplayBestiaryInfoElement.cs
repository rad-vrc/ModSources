using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006AC RID: 1708
	public class UnlockProgressDisplayBestiaryInfoElement : IBestiaryInfoElement
	{
		// Token: 0x06004880 RID: 18560 RVA: 0x0064997A File Offset: 0x00647B7A
		public UnlockProgressDisplayBestiaryInfoElement(BestiaryUnlockProgressReport progressReport)
		{
			this._progressReport = progressReport;
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x0064998C File Offset: 0x00647B8C
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			UIElement uIElement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7)
			{
				Width = new StyleDimension(-11f, 1f),
				Height = new StyleDimension(109f, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Left = new StyleDimension(3f, 0f)
			};
			uIElement.PaddingLeft = 4f;
			uIElement.PaddingRight = 4f;
			string text3 = Utils.PrettifyPercentDisplay((float)info.UnlockState / 4f, "P2") + " Entry Collected";
			string text2 = Utils.PrettifyPercentDisplay(this._progressReport.CompletionPercent, "P2") + " Bestiary Collected";
			int num = 8;
			UIText uIText = new UIText(text3, 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true,
				PaddingTop = (float)(-(float)num),
				PaddingBottom = (float)(-(float)num)
			};
			UIText uIText2 = new UIText(text2, 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true,
				PaddingTop = (float)(-(float)num),
				PaddingBottom = (float)(-(float)num)
			};
			this._text1 = uIText;
			this._text2 = uIText2;
			this.AddDynamicResize(uIElement, uIText);
			uIElement.Append(uIText);
			uIElement.Append(uIText2);
			return uIElement;
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x00649B5C File Offset: 0x00647D5C
		private void AddDynamicResize(UIElement container, UIText text)
		{
			text.OnInternalTextChange += delegate()
			{
				container.Height = new StyleDimension(this._text1.MinHeight.Pixels + 4f + this._text2.MinHeight.Pixels, 0f);
				this._text2.Top = new StyleDimension(this._text1.MinHeight.Pixels + 4f, 0f);
			};
		}

		// Token: 0x04005C3F RID: 23615
		private BestiaryUnlockProgressReport _progressReport;

		// Token: 0x04005C40 RID: 23616
		private UIElement _text1;

		// Token: 0x04005C41 RID: 23617
		private UIElement _text2;
	}
}
