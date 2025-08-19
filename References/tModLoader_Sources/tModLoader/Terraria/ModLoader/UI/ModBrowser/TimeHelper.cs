using System;
using Terraria.Localization;

namespace Terraria.ModLoader.UI.ModBrowser
{
	// Token: 0x0200026C RID: 620
	internal class TimeHelper
	{
		// Token: 0x06002ADB RID: 10971 RVA: 0x0051EE93 File Offset: 0x0051D093
		public static string HumanTimeSpanString(DateTime yourDate)
		{
			return TimeHelper.HumanTimeSpanString(yourDate, false);
		}

		/// <summary>
		/// Returns a localized string in the form of "X timeunit(s) ago" comparing the current time to the provided DateTime. Use <paramref name="localTime" /> to indicate if the provided DateTime is expressed as local time or coordinated universal time (UTC).
		/// </summary>
		/// <param name="yourDate"></param>
		/// <param name="localTime"></param>
		/// <returns></returns>
		// Token: 0x06002ADC RID: 10972 RVA: 0x0051EE9C File Offset: 0x0051D09C
		public static string HumanTimeSpanString(DateTime yourDate, bool localTime)
		{
			TimeSpan ts = new TimeSpan((localTime ? DateTime.Now.Ticks : DateTime.UtcNow.Ticks) - yourDate.Ticks);
			double delta = Math.Abs(ts.TotalSeconds);
			if (delta < 60.0)
			{
				return Language.GetTextValue("tModLoader.XSecondsAgo", ts.Seconds);
			}
			if (delta < 3600.0)
			{
				return Language.GetTextValue("tModLoader.XMinutesAgo", ts.Minutes);
			}
			if (delta < 86400.0)
			{
				return Language.GetTextValue("tModLoader.XHoursAgo", ts.Hours);
			}
			if (delta < 2592000.0)
			{
				return Language.GetTextValue("tModLoader.XDaysAgo", ts.Days);
			}
			if (delta < 31104000.0)
			{
				int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30.0));
				return Language.GetTextValue("tModLoader.XMonthsAgo", months);
			}
			int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365.0));
			return Language.GetTextValue("tModLoader.XYearsAgo", years);
		}

		// Token: 0x04001B82 RID: 7042
		private const int SECOND = 1;

		// Token: 0x04001B83 RID: 7043
		private const int MINUTE = 60;

		// Token: 0x04001B84 RID: 7044
		private const int HOUR = 3600;

		// Token: 0x04001B85 RID: 7045
		private const int DAY = 86400;

		// Token: 0x04001B86 RID: 7046
		private const int MONTH = 2592000;
	}
}
