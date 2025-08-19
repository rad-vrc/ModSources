using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200031E RID: 798
	public class SearchAliasInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x06002443 RID: 9283 RVA: 0x0055A0FD File Offset: 0x005582FD
		public SearchAliasInfoElement(string alias)
		{
			this._alias = alias;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x0055A10C File Offset: 0x0055830C
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return this._alias;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x04004881 RID: 18561
		private readonly string _alias;
	}
}
