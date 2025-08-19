using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000257 RID: 599
	public static class EnabledFilterModesExtensions
	{
		// Token: 0x06002A53 RID: 10835 RVA: 0x0051B0FE File Offset: 0x005192FE
		public static string ToFriendlyString(this EnabledFilter updateFilterMode)
		{
			switch (updateFilterMode)
			{
			case EnabledFilter.All:
				return Language.GetTextValue("tModLoader.ModsShowAllMods");
			case EnabledFilter.EnabledOnly:
				return Language.GetTextValue("tModLoader.ModsShowEnabledMods");
			case EnabledFilter.DisabledOnly:
				return Language.GetTextValue("tModLoader.ModsShowDisabledMods");
			default:
				return "Unknown Sort";
			}
		}
	}
}
