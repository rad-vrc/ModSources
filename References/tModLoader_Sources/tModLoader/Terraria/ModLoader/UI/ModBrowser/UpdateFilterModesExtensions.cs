using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000273 RID: 627
	public static class UpdateFilterModesExtensions
	{
		// Token: 0x06002B43 RID: 11075 RVA: 0x00521C54 File Offset: 0x0051FE54
		public static string ToFriendlyString(this UpdateFilter updateFilterMode)
		{
			switch (updateFilterMode)
			{
			case UpdateFilter.All:
				return Language.GetTextValue("tModLoader.MBShowAllMods");
			case UpdateFilter.Available:
				return Language.GetTextValue("tModLoader.MBShowNotInstalledUpdates");
			case UpdateFilter.UpdateOnly:
				return Language.GetTextValue("tModLoader.MBShowUpdates");
			case UpdateFilter.InstalledOnly:
				return Language.GetTextValue("tModLoader.MBShowInstalled");
			default:
				return "Unknown Sort";
			}
		}
	}
}
