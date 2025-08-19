using System;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000694 RID: 1684
	public class ItemDropBestiaryInfoElement : IItemBestiaryInfoElement, IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x06004801 RID: 18433 RVA: 0x00646DB8 File Offset: 0x00644FB8
		public ItemDropBestiaryInfoElement(DropRateInfo info)
		{
			this._droprateInfo = info;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00646DC8 File Offset: 0x00644FC8
		public virtual UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			bool flag = ItemDropBestiaryInfoElement.ShouldShowItem(ref this._droprateInfo);
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				flag = false;
			}
			if (!flag)
			{
				return null;
			}
			return new UIBestiaryInfoItemLine(this._droprateInfo, info, 1f);
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00646E04 File Offset: 0x00645004
		private static bool ShouldShowItem(ref DropRateInfo dropRateInfo)
		{
			bool result = true;
			if (dropRateInfo.conditions != null && dropRateInfo.conditions.Count > 0)
			{
				for (int i = 0; i < dropRateInfo.conditions.Count; i++)
				{
					if (!dropRateInfo.conditions[i].CanShowItemDropInUI())
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x00646E58 File Offset: 0x00645058
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			bool flag = ItemDropBestiaryInfoElement.ShouldShowItem(ref this._droprateInfo);
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				flag = false;
			}
			if (!flag)
			{
				return null;
			}
			return ContentSamples.ItemsByType[this._droprateInfo.itemId].Name;
		}

		// Token: 0x04005C0D RID: 23565
		protected DropRateInfo _droprateInfo;
	}
}
