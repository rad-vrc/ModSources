using System;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000695 RID: 1685
	public class ItemFromCatchingNPCBestiaryInfoElement : IItemBestiaryInfoElement, IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x06004805 RID: 18437 RVA: 0x00646E9B File Offset: 0x0064509B
		public ItemFromCatchingNPCBestiaryInfoElement(int itemId)
		{
			this._itemType = itemId;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x00646EAA File Offset: 0x006450AA
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				return null;
			}
			return new UIBestiaryInfoLine<string>("catch item #" + this._itemType.ToString(), 1f);
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x00646ED6 File Offset: 0x006450D6
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				return null;
			}
			return ContentSamples.ItemsByType[this._itemType].Name;
		}

		// Token: 0x04005C0E RID: 23566
		private int _itemType;
	}
}
