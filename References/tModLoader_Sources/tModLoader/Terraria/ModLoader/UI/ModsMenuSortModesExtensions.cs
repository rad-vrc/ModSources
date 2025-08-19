using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000256 RID: 598
	public static class ModsMenuSortModesExtensions
	{
		// Token: 0x06002A52 RID: 10834 RVA: 0x0051B0C2 File Offset: 0x005192C2
		public static string ToFriendlyString(this ModsMenuSortMode sortmode)
		{
			switch (sortmode)
			{
			case ModsMenuSortMode.RecentlyUpdated:
				return Language.GetTextValue("tModLoader.ModsSortRecently");
			case ModsMenuSortMode.DisplayNameAtoZ:
				return Language.GetTextValue("tModLoader.ModsSortNamesAlph");
			case ModsMenuSortMode.DisplayNameZtoA:
				return Language.GetTextValue("tModLoader.ModsSortNamesReverseAlph");
			default:
				return "Unknown Sort";
			}
		}
	}
}
