using System;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200067C RID: 1660
	public class BossBestiaryInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x060047C2 RID: 18370 RVA: 0x006465A3 File Offset: 0x006447A3
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x006465A6 File Offset: 0x006447A6
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowPortraitOnly_1)
			{
				return null;
			}
			return Language.GetText("BestiaryInfo.IsBoss").Value;
		}
	}
}
