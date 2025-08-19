using System;
using System.Text.RegularExpressions;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria.Localization
{
	/// <summary>
	/// Contains methods to access or retrieve localization values. The <see href="https://github.com/tModLoader/tModLoader/wiki/Localization">Localization Guide</see> teaches more about localization.
	/// </summary>
	// Token: 0x020003D5 RID: 981
	public static class Language
	{
		/// <summary>
		/// The language the game is currently using.
		/// </summary>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x00554570 File Offset: 0x00552770
		public static GameCulture ActiveCulture
		{
			get
			{
				return LanguageManager.Instance.ActiveCulture;
			}
		}

		/// <summary>
		/// Retrieves a LocalizedText object for a specified localization key. The actual text value can be retrieved from LocalizedText by accessing the <see cref="P:Terraria.Localization.LocalizedText.Value" /> property or by using the <see cref="M:Terraria.Localization.Language.GetTextValue(System.String)" /> method directly.<br /><br />
		/// Using LocalizedText instead of string is preferred when the value is stored. If the user switches languages or if resource packs or mods change text, the LocalizedText will automatically receive the new value. In turn, mods using those LocalizedText will also start displaying the updated values.<br /><br />
		///
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		// Token: 0x06003381 RID: 13185 RVA: 0x0055457C File Offset: 0x0055277C
		public static LocalizedText GetText(string key)
		{
			return LanguageManager.Instance.GetText(key);
		}

		/// <summary>
		/// Retrieves the text value for a specified localization key. The text returned will be for the currently selected language.
		/// <para /> Note that modded localization entries are not loaded until the <see cref="M:Terraria.ModLoader.Mod.SetupContent" /> stage, so attempting to use GetTextValue during Load methods will not work. Consider using <see cref="M:Terraria.Localization.Language.GetText(System.String)" /> instead.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		// Token: 0x06003382 RID: 13186 RVA: 0x00554589 File Offset: 0x00552789
		public static string GetTextValue(string key)
		{
			return LanguageManager.Instance.GetTextValue(key);
		}

		/// <inheritdoc cref="M:Terraria.Localization.LocalizedText.Format(System.Object[])" />
		// Token: 0x06003383 RID: 13187 RVA: 0x00554596 File Offset: 0x00552796
		public static string GetTextValue(string key, object arg0)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0);
		}

		/// <inheritdoc cref="M:Terraria.Localization.LocalizedText.Format(System.Object[])" />
		// Token: 0x06003384 RID: 13188 RVA: 0x005545A4 File Offset: 0x005527A4
		public static string GetTextValue(string key, object arg0, object arg1)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0, arg1);
		}

		/// <inheritdoc cref="M:Terraria.Localization.LocalizedText.Format(System.Object[])" />
		// Token: 0x06003385 RID: 13189 RVA: 0x005545B3 File Offset: 0x005527B3
		public static string GetTextValue(string key, object arg0, object arg1, object arg2)
		{
			return LanguageManager.Instance.GetTextValue(key, arg0, arg1, arg2);
		}

		/// <inheritdoc cref="M:Terraria.Localization.LocalizedText.Format(System.Object[])" />
		// Token: 0x06003386 RID: 13190 RVA: 0x005545C3 File Offset: 0x005527C3
		public static string GetTextValue(string key, params object[] args)
		{
			return LanguageManager.Instance.GetTextValue(key, args);
		}

		/// <inheritdoc cref="M:Terraria.Localization.LocalizedText.FormatWith(System.Object)" />
		// Token: 0x06003387 RID: 13191 RVA: 0x005545D1 File Offset: 0x005527D1
		public static string GetTextValueWith(string key, object obj)
		{
			return LanguageManager.Instance.GetText(key).FormatWith(obj);
		}

		/// <summary>
		/// Checks if a LocalizedText with the provided key has been registered or not. This can be used to avoid retrieving dummy values from <see cref="M:Terraria.Localization.Language.GetText(System.String)" /> and <see cref="M:Terraria.Localization.Language.GetTextValue(System.String)" /> and instead load a fallback value or do other logic. If the key should be created if it doesn't exist, <see cref="M:Terraria.Localization.Language.GetOrRegister(System.String,System.Func{System.String})" /> can be used instead.
		/// <br /><br /> Note that modded keys will be registered during mod loading and <see cref="M:Terraria.ModLoader.Mod.SetupContent" /> is the earliest point where all keys should be registered with values loaded for the current language.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		// Token: 0x06003388 RID: 13192 RVA: 0x005545E4 File Offset: 0x005527E4
		public static bool Exists(string key)
		{
			return LanguageManager.Instance.Exists(key);
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x005545F1 File Offset: 0x005527F1
		public static int GetCategorySize(string key)
		{
			return LanguageManager.Instance.GetCategorySize(key);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x005545FE File Offset: 0x005527FE
		public static LocalizedText[] FindAll(Regex regex)
		{
			return LanguageManager.Instance.FindAll(regex);
		}

		/// <summary>
		/// Finds all LocalizedText that satisfy the <paramref name="filter" /> parameter. Typically used with <see cref="M:Terraria.Lang.CreateDialogFilter(System.String)" /> or <see cref="M:Terraria.Lang.CreateDialogFilter(System.String,System.Object)" /> as the <paramref name="filter" /> argument to find all LocalizedText that have a specific key prefix and satisfy provided conditions.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		// Token: 0x0600338B RID: 13195 RVA: 0x0055460B File Offset: 0x0055280B
		public static LocalizedText[] FindAll(LanguageSearchFilter filter)
		{
			return LanguageManager.Instance.FindAll(filter);
		}

		/// <summary>
		/// Selects a single random LocalizedText that satisfies the <paramref name="filter" /> parameter. Typically used with <see cref="M:Terraria.Lang.CreateDialogFilter(System.String)" /> or <see cref="M:Terraria.Lang.CreateDialogFilter(System.String,System.Object)" /> as the <paramref name="filter" /> argument to find a random LocalizedText that has a specific key prefix and satisfies the provided conditions.
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="random"></param>
		/// <returns></returns>
		// Token: 0x0600338C RID: 13196 RVA: 0x00554618 File Offset: 0x00552818
		public static LocalizedText SelectRandom(LanguageSearchFilter filter, UnifiedRandom random = null)
		{
			return LanguageManager.Instance.SelectRandom(filter, random);
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x00554626 File Offset: 0x00552826
		public static LocalizedText RandomFromCategory(string categoryName, UnifiedRandom random = null)
		{
			return LanguageManager.Instance.RandomFromCategory(categoryName, random);
		}

		/// <summary>
		/// Returns a <see cref="T:Terraria.Localization.LocalizedText" /> for a given key.
		/// <br />If no existing localization exists for the key, it will be defined so it can be exported to a matching mod localization file.
		/// </summary>
		/// <param name="key">The localization key</param>
		/// <param name="makeDefaultValue">A factory method for creating the default value, used to update localization files with missing entries</param>
		/// <returns></returns>
		// Token: 0x0600338E RID: 13198 RVA: 0x00554634 File Offset: 0x00552834
		public static LocalizedText GetOrRegister(string key, Func<string> makeDefaultValue = null)
		{
			return LanguageManager.Instance.GetOrRegister(key, makeDefaultValue);
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x00554642 File Offset: 0x00552842
		[Obsolete("Pass mod.GetLocalizationKey(key) directly")]
		public static LocalizedText GetOrRegister(Mod mod, string key, Func<string> makeDefaultValue = null)
		{
			return Language.GetOrRegister(mod.GetLocalizationKey(key), makeDefaultValue);
		}
	}
}
