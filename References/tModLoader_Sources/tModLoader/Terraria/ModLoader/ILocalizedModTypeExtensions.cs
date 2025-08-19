using System;
using System.Runtime.CompilerServices;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x0200017D RID: 381
	public static class ILocalizedModTypeExtensions
	{
		/// <summary>
		/// Gets a suitable localization key belonging to this piece of content. <br /><br />Localization keys follow the pattern of "Mods.{ModName}.{Category}.{ContentName}.{DataName}", in this case the <paramref name="suffix" /> corresponds to the DataName.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		// Token: 0x06001DFA RID: 7674 RVA: 0x004D4E68 File Offset: 0x004D3068
		public static string GetLocalizationKey(this ILocalizedModType self, string suffix)
		{
			Mod mod = self.Mod;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
			defaultInterpolatedStringHandler.AppendFormatted(self.LocalizationCategory);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted(self.Name);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted(suffix);
			return mod.GetLocalizationKey(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		/// <summary>
		/// Returns a <see cref="T:Terraria.Localization.LocalizedText" /> for this piece of content with the provided <paramref name="suffix" />.
		/// <br />If no existing localization exists for the key, it will be defined so it can be exported to a matching mod localization file.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="suffix"></param>
		/// <param name="makeDefaultValue">A factory method for creating the default value, used to update localization files with missing entries</param>
		/// <returns></returns>
		// Token: 0x06001DFB RID: 7675 RVA: 0x004D4ECA File Offset: 0x004D30CA
		public static LocalizedText GetLocalization(this ILocalizedModType self, string suffix, Func<string> makeDefaultValue = null)
		{
			return Language.GetOrRegister(self.GetLocalizationKey(suffix), makeDefaultValue);
		}

		/// <summary>
		/// Retrieves the text value for a localization key belonging to this piece of content with the given <paramref name="suffix" />. The text returned will be for the currently selected language.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="suffix"></param>
		/// <returns></returns>
		// Token: 0x06001DFC RID: 7676 RVA: 0x004D4ED9 File Offset: 0x004D30D9
		public static string GetLocalizedValue(this ILocalizedModType self, string suffix)
		{
			return Language.GetTextValue(self.GetLocalizationKey(suffix));
		}
	}
}
