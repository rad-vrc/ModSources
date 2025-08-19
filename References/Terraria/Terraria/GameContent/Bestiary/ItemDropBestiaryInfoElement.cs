using System;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000314 RID: 788
	public class ItemDropBestiaryInfoElement : IItemBestiaryInfoElement, IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x005589DA File Offset: 0x00556BDA
		public ItemDropBestiaryInfoElement(DropRateInfo info)
		{
			this._droprateInfo = info;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x005589EC File Offset: 0x00556BEC
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

		// Token: 0x0600241D RID: 9245 RVA: 0x00558A28 File Offset: 0x00556C28
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

		// Token: 0x0600241E RID: 9246 RVA: 0x00558A7C File Offset: 0x00556C7C
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

		// Token: 0x0400486E RID: 18542
		protected DropRateInfo _droprateInfo;
	}
}
