using System;
using System.Text.RegularExpressions;
using Terraria.Utilities;

namespace Terraria.Localization
{
	// Token: 0x020000AC RID: 172
	public static class Language
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0049FDF4 File Offset: 0x0049DFF4
		public static GameCulture ActiveCulture
		{
			get
			{
				return LanguageManager.Instance.ActiveCulture;
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0049FE00 File Offset: 0x0049E000
		public static LocalizedText GetText(string key)
		{
			return LanguageManager.Instance.GetText(key);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0049FE0D File Offset: 0x0049E00D
		public static string GetTextValue(string key)
		{
			return LanguageManager.Instance.GetTextValue(key);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0049FE1A File Offset: 0x0049E01A
		public static string GetTextValue(string key, object arg0)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0049FE28 File Offset: 0x0049E028
		public static string GetTextValue(string key, object arg0, object arg1)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0, arg1);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0049FE37 File Offset: 0x0049E037
		public static string GetTextValue(string key, object arg0, object arg1, object arg2)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0, arg1, arg2);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0049FE47 File Offset: 0x0049E047
		public static string GetTextValue(string key, params object[] args)
		{
			return LanguageManager.Instance.GetTextValue(key, args);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0049FE55 File Offset: 0x0049E055
		public static string GetTextValueWith(string key, object obj)
		{
			return LanguageManager.Instance.GetText(key).FormatWith(obj);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0049FE68 File Offset: 0x0049E068
		public static bool Exists(string key)
		{
			return LanguageManager.Instance.Exists(key);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0049FE75 File Offset: 0x0049E075
		public static int GetCategorySize(string key)
		{
			return LanguageManager.Instance.GetCategorySize(key);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0049FE82 File Offset: 0x0049E082
		public static LocalizedText[] FindAll(Regex regex)
		{
			return LanguageManager.Instance.FindAll(regex);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0049FE8F File Offset: 0x0049E08F
		public static LocalizedText[] FindAll(LanguageSearchFilter filter)
		{
			return LanguageManager.Instance.FindAll(filter);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0049FE9C File Offset: 0x0049E09C
		public static LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
		{
			return LanguageManager.Instance.SelectRandom(filter, random);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0049FEAA File Offset: 0x0049E0AA
		public static LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
		{
			return LanguageManager.Instance.RandomFromCategory(categoryName, random);
		}
	}
}
