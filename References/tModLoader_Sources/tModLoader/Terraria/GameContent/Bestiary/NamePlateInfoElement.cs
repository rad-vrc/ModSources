using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200069B RID: 1691
	public class NamePlateInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x0600481B RID: 18459 RVA: 0x0064739B File Offset: 0x0064559B
		public NamePlateInfoElement(string languageKey, int npcNetId)
		{
			this._key = languageKey;
			this._npcNetId = npcNetId;
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x006473B4 File Offset: 0x006455B4
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			UIElement uIElement = (info.UnlockState != BestiaryEntryUnlockState.NotKnownAtAll_0) ? new UIText(Language.GetText(this._key), 1f, false) : new UIText("???", 1f, false);
			uIElement.HAlign = 0.5f;
			uIElement.VAlign = 0.5f;
			uIElement.Top = new StyleDimension(2f, 0f);
			uIElement.IgnoresMouseInteraction = true;
			UIElement uielement = new UIElement();
			uielement.Width = new StyleDimension(0f, 1f);
			uielement.Height = new StyleDimension(24f, 0f);
			uielement.Append(uIElement);
			return uielement;
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0064745A File Offset: 0x0064565A
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText(this._key).Value;
		}

		// Token: 0x04005C14 RID: 23572
		private string _key;

		// Token: 0x04005C15 RID: 23573
		private int _npcNetId;
	}
}
