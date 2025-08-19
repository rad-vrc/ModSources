using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000316 RID: 790
	public class FlavorTextBestiaryInfoElement : IBestiaryInfoElement
	{
		// Token: 0x06002422 RID: 9250 RVA: 0x00558B25 File Offset: 0x00556D25
		public FlavorTextBestiaryInfoElement(string languageKey)
		{
			this._key = languageKey;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00558B34 File Offset: 0x00556D34
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				return null;
			}
			UIPanel uipanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 12, 7);
			uipanel.Width = new StyleDimension(-11f, 1f);
			uipanel.Height = new StyleDimension(109f, 0f);
			uipanel.BackgroundColor = new Color(43, 56, 101);
			uipanel.BorderColor = Color.Transparent;
			uipanel.Left = new StyleDimension(3f, 0f);
			uipanel.PaddingLeft = 4f;
			uipanel.PaddingRight = 4f;
			UIText uitext = new UIText(Language.GetText(this._key), 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true
			};
			FlavorTextBestiaryInfoElement.AddDynamicResize(uipanel, uitext);
			uipanel.Append(uitext);
			return uipanel;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00558C44 File Offset: 0x00556E44
		private static void AddDynamicResize(UIElement container, UIText text)
		{
			text.OnInternalTextChange += delegate()
			{
				container.Height = new StyleDimension(text.MinHeight.Pixels, 0f);
			};
		}

		// Token: 0x04004870 RID: 18544
		private string _key;
	}
}
