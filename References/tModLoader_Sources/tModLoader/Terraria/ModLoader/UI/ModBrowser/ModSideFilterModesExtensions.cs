using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x02000269 RID: 617
	public static class ModSideFilterModesExtensions
	{
		// Token: 0x06002AD9 RID: 10969 RVA: 0x0051EE08 File Offset: 0x0051D008
		public static string ToFriendlyString(this ModSideFilter modSideFilterMode)
		{
			switch (modSideFilterMode)
			{
			case ModSideFilter.All:
				return Language.GetTextValue("tModLoader.MBShowMSAll");
			case ModSideFilter.Both:
				return Language.GetTextValue("tModLoader.MBShowMSBoth");
			case ModSideFilter.Client:
				return Language.GetTextValue("tModLoader.MBShowMSClient");
			case ModSideFilter.Server:
				return Language.GetTextValue("tModLoader.MBShowMSServer");
			case ModSideFilter.NoSync:
				return Language.GetTextValue("tModLoader.MBShowMSNoSync");
			default:
				return "Unknown Sort";
			}
		}
	}
}
