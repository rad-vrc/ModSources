using System;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000315 RID: 789
	public class ItemFromCatchingNPCBestiaryInfoElement : IItemBestiaryInfoElement, IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x0600241F RID: 9247 RVA: 0x00558ABF File Offset: 0x00556CBF
		public ItemFromCatchingNPCBestiaryInfoElement(int itemId)
		{
			this._itemType = itemId;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00558ACE File Offset: 0x00556CCE
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				return null;
			}
			return new UIBestiaryInfoLine<string>(("catch item #" + this._itemType) ?? "", 1f);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00558B03 File Offset: 0x00556D03
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				return null;
			}
			return ContentSamples.ItemsByType[this._itemType].Name;
		}

		// Token: 0x0400486F RID: 18543
		private int _itemType;
	}
}
