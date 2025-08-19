using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x0200026B RID: 619
	public static class SearchFilterModesExtensions
	{
		// Token: 0x06002ADA RID: 10970 RVA: 0x0051EE6D File Offset: 0x0051D06D
		public static string ToFriendlyString(this SearchFilter searchFilterMode)
		{
			if (searchFilterMode == SearchFilter.Name)
			{
				return Language.GetTextValue("tModLoader.ModsSearchByModName");
			}
			if (searchFilterMode != SearchFilter.Author)
			{
				return "Unknown Sort";
			}
			return Language.GetTextValue("tModLoader.ModsSearchByAuthor");
		}
	}
}
