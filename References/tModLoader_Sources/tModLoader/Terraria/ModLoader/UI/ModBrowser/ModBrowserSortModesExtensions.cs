using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000263 RID: 611
	public static class ModBrowserSortModesExtensions
	{
		// Token: 0x06002ACB RID: 10955 RVA: 0x0051EAD8 File Offset: 0x0051CCD8
		public static string ToFriendlyString(this ModBrowserSortMode sortmode)
		{
			switch (sortmode)
			{
			case ModBrowserSortMode.DownloadsDescending:
				return Language.GetTextValue("tModLoader.MBSortDownloadDesc");
			case ModBrowserSortMode.RecentlyPublished:
				return Language.GetTextValue("tModLoader.MBSortByRecentlyPublished");
			case ModBrowserSortMode.RecentlyUpdated:
				return Language.GetTextValue("tModLoader.MBSortByRecentlyUpdated");
			case ModBrowserSortMode.Hot:
				if (!string.IsNullOrEmpty(Interface.modBrowser.Filter))
				{
					return Language.GetTextValue("tModLoader.MBSortByRelevance");
				}
				return Language.GetTextValue("tModLoader.MBSortByPopularity");
			default:
				return "Unknown Sort";
			}
		}
	}
}
