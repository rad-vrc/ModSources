using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000318 RID: 792
	public class NamePlateInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x06002428 RID: 9256 RVA: 0x00558E9B File Offset: 0x0055709B
		public NamePlateInfoElement(string languageKey, int npcNetId)
		{
			this._key = languageKey;
			this._npcNetId = npcNetId;
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00558EB4 File Offset: 0x005570B4
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			UIElement uielement;
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				uielement = new UIText("???", 1f, false);
			}
			else
			{
				uielement = new UIText(Language.GetText(this._key), 1f, false);
			}
			uielement.HAlign = 0.5f;
			uielement.VAlign = 0.5f;
			uielement.Top = new StyleDimension(2f, 0f);
			uielement.IgnoresMouseInteraction = true;
			UIElement uielement2 = new UIElement();
			uielement2.Width = new StyleDimension(0f, 1f);
			uielement2.Height = new StyleDimension(24f, 0f);
			uielement2.Append(uielement);
			return uielement2;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00558F5B File Offset: 0x0055715B
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText(this._key).Value;
		}

		// Token: 0x04004874 RID: 18548
		private string _key;

		// Token: 0x04004875 RID: 18549
		private int _npcNetId;
	}
}
