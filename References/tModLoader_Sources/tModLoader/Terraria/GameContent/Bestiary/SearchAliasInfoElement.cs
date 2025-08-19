using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A5 RID: 1701
	public class SearchAliasInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x0600485F RID: 18527 RVA: 0x00649099 File Offset: 0x00647299
		public SearchAliasInfoElement(string alias)
		{
			this._alias = alias;
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x006490A8 File Offset: 0x006472A8
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return this._alias;
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x006490BA File Offset: 0x006472BA
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04005C2D RID: 23597
		private readonly string _alias;
	}
}
