using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000683 RID: 1667
	public class FlavorTextBestiaryInfoElement : IBestiaryInfoElement
	{
		// Token: 0x060047E1 RID: 18401 RVA: 0x00646AB4 File Offset: 0x00644CB4
		public FlavorTextBestiaryInfoElement(string languageKey)
		{
			this._key = languageKey;
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x00646AC4 File Offset: 0x00644CC4
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				return null;
			}
			UIPanel uipanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel"), null, 12, 7);
			uipanel.Width = new StyleDimension(-11f, 1f);
			uipanel.Height = new StyleDimension(109f, 0f);
			uipanel.BackgroundColor = new Color(43, 56, 101);
			uipanel.BorderColor = Color.Transparent;
			uipanel.Left = new StyleDimension(3f, 0f);
			uipanel.PaddingLeft = 4f;
			uipanel.PaddingRight = 4f;
			UIText uIText = new UIText(Language.GetText(this._key), 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true
			};
			FlavorTextBestiaryInfoElement.AddDynamicResize(uipanel, uIText);
			uipanel.Append(uIText);
			return uipanel;
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x00646BD4 File Offset: 0x00644DD4
		private static void AddDynamicResize(UIElement container, UIText text)
		{
			text.OnInternalTextChange += delegate()
			{
				container.Height = new StyleDimension(text.MinHeight.Pixels, 0f);
			};
		}

		// Token: 0x04005C08 RID: 23560
		private string _key;
	}
}
