using System;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000309 RID: 777
	public class BossBestiaryInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x005585C1 File Offset: 0x005567C1
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
