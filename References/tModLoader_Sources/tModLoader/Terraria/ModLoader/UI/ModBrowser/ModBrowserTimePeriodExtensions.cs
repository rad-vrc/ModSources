using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000265 RID: 613
	public static class ModBrowserTimePeriodExtensions
	{
		// Token: 0x06002ACC RID: 10956 RVA: 0x0051EB4C File Offset: 0x0051CD4C
		public static string ToFriendlyString(this ModBrowserTimePeriod modBrowserTimePeriod)
		{
			switch (modBrowserTimePeriod)
			{
			case ModBrowserTimePeriod.Today:
				return Language.GetTextValue("tModLoader.Today");
			case ModBrowserTimePeriod.OneWeek:
				return Language.GetTextValue("tModLoader.OneWeek");
			case ModBrowserTimePeriod.ThreeMonths:
				return Language.GetTextValue("tModLoader.ThreeMonths");
			case ModBrowserTimePeriod.SixMonths:
				return Language.GetTextValue("tModLoader.SixMonths");
			case ModBrowserTimePeriod.OneYear:
				return Language.GetTextValue("tModLoader.OneYear");
			case ModBrowserTimePeriod.AllTime:
				return Language.GetTextValue("tModLoader.AllTime");
			default:
				return "Unknown Time Period";
			}
		}
	}
}
