using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Terraria.Localization
{
	/// <summary>
	/// Contains the localization value corresponding to a key for the current game language. Automatically updates as language, mods, and resource packs change. The <see href="https://github.com/tModLoader/tModLoader/wiki/Localization">Localization Guide</see> teaches more about localization.
	/// </summary>
	// Token: 0x020003D9 RID: 985
	public class LocalizedText
	{
		/// <summary>
		/// Retrieves the text value. This is the actual text the user should see.
		/// </summary>
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x00555778 File Offset: 0x00553978
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x00555780 File Offset: 0x00553980
		public string Value
		{
			get
			{
				return this._value;
			}
			private set
			{
				this._value = value;
				this._hasPlurals = null;
				this.BoundArgs = null;
			}
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x0055579C File Offset: 0x0055399C
		internal LocalizedText(string key, string text)
		{
			this.Key = key;
			this.Value = text;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x005557B2 File Offset: 0x005539B2
		internal void SetValue(string text)
		{
			this.Value = text;
		}

		/// <summary>
		/// Creates a string from this LocalizedText populated with data from the provided <paramref name="obj" /> parameter. The properties of the provided object are substituted by name into the placeholders of the original text. For example, when used with <see cref="M:Terraria.Lang.CreateDialogSubstitutionObject(Terraria.NPC)" />, the text "{Nurse}" will be replaced with the first name of the Nurse in the world. Modded substitutions are not currently supported. <br /><br />
		/// When used in conjunction with <see cref="M:Terraria.Localization.Language.SelectRandom(Terraria.Localization.LanguageSearchFilter,Terraria.Utilities.UnifiedRandom)" /> and <see cref="M:Terraria.Lang.CreateDialogFilter(System.String,System.Object)" />, simple boolean conditions expressed in each LocalizedText can be used to filter a collection of LocalizedText.  <br /><br />
		/// <see cref="M:Terraria.Localization.LocalizedText.Format(System.Object[])" /> is more commonly used to format LocalizedText placeholders. That method replaces placeholders such as "{0}", "{1}", etc with the string representation of the corresponding objects provided.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		// Token: 0x060033C7 RID: 13255 RVA: 0x005557BC File Offset: 0x005539BC
		public string FormatWith(object obj)
		{
			string value = this.Value;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			return LocalizedText._substitutionRegex.Replace(value, delegate(Match match)
			{
				if (match.Groups[1].Length != 0)
				{
					return "";
				}
				string name = match.Groups[2].ToString();
				PropertyDescriptor propertyDescriptor = properties.Find(name, false);
				if (propertyDescriptor != null)
				{
					return (propertyDescriptor.GetValue(obj) ?? "").ToString();
				}
				return "";
			});
		}

		/// <summary>
		/// Checks if the conditions embedded in this LocalizedText are satisfied by the <paramref name="obj" /> argument.
		/// For example when used with <see cref="M:Terraria.Lang.CreateDialogSubstitutionObject(Terraria.NPC)" /> as the <paramref name="obj" /> argument, "{?Rain}" at the start of a LocalizedText value will cause false to be returned if it is not raining. "{?!Rain}" would do the opposite. If all conditions are satisfied, true is returned.<br />
		/// The method is typically used indirectly by using <see cref="M:Terraria.Lang.CreateDialogFilter(System.String,System.Object)" />.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		// Token: 0x060033C8 RID: 13256 RVA: 0x00555808 File Offset: 0x00553A08
		public bool CanFormatWith(object obj)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			foreach (object obj2 in LocalizedText._substitutionRegex.Matches(this.Value))
			{
				Match item = (Match)obj2;
				string name = item.Groups[2].ToString();
				PropertyDescriptor propertyDescriptor = properties.Find(name, false);
				if (propertyDescriptor == null)
				{
					return false;
				}
				object value = propertyDescriptor.GetValue(obj);
				if (value == null)
				{
					return false;
				}
				if (item.Groups[1].Length != 0 && ((value as bool?).GetValueOrDefault() ^ item.Groups[1].Length == 1))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary> Convert this <see cref="T:Terraria.Localization.LocalizedText" /> to a <see cref="T:Terraria.Localization.NetworkText" /> for use in various network code applications. Non-chat messages sent to other players should be sent as <see cref="T:Terraria.Localization.NetworkText" /> to facilitate localization. </summary>
		// Token: 0x060033C9 RID: 13257 RVA: 0x005558F4 File Offset: 0x00553AF4
		public NetworkText ToNetworkText()
		{
			return NetworkText.FromKey(this.Key, this.BoundArgs ?? Array.Empty<object>());
		}

		/// <inheritdoc cref="M:Terraria.Localization.LocalizedText.ToNetworkText" />
		// Token: 0x060033CA RID: 13258 RVA: 0x00555910 File Offset: 0x00553B10
		public NetworkText ToNetworkText(params object[] substitutions)
		{
			return NetworkText.FromKey(this.Key, substitutions);
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x0055591E File Offset: 0x00553B1E
		public static explicit operator string(LocalizedText text)
		{
			return text.Value;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x00555926 File Offset: 0x00553B26
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>
		/// Returns the args used with <see cref="M:Terraria.Localization.LocalizedText.WithFormatArgs(System.Object[])" /> to create this text, if any.
		/// </summary>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x0055592E File Offset: 0x00553B2E
		// (set) Token: 0x060033CE RID: 13262 RVA: 0x00555936 File Offset: 0x00553B36
		public object[] BoundArgs { get; private set; }

		// Token: 0x060033CF RID: 13263 RVA: 0x00555940 File Offset: 0x00553B40
		public static int CardinalPluralRule(GameCulture culture, int count)
		{
			int mod_i10 = count % 10;
			int mod_i11 = count % 100;
			switch (culture.LegacyId)
			{
			case 1:
			case 2:
			case 3:
			case 5:
			case 8:
				return (count != 1) ? 1 : 0;
			case 4:
				if (count != 0 && count != 1)
				{
					return 1;
				}
				return 0;
			case 6:
				if (mod_i10 == 1 && mod_i11 != 11)
				{
					return 0;
				}
				if (LocalizedText.<CardinalPluralRule>g__contains|20_0(mod_i10, 2, 4) && !LocalizedText.<CardinalPluralRule>g__contains|20_0(mod_i11, 12, 14))
				{
					return 1;
				}
				return 2;
			case 9:
				if (count == 1)
				{
					return 0;
				}
				if (LocalizedText.<CardinalPluralRule>g__contains|20_0(mod_i10, 2, 4) && !LocalizedText.<CardinalPluralRule>g__contains|20_0(mod_i11, 12, 14))
				{
					return 1;
				}
				return 2;
			}
			return 0;
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x005559E8 File Offset: 0x00553BE8
		public static string ApplyPluralization(string value, params object[] args)
		{
			return LocalizedText.PluralizationPatternRegex.Replace(value, delegate(Match match)
			{
				int argIndex = Convert.ToInt32(match.Groups[1].Value);
				string[] options = match.Groups[2].Value.Split(';', StringSplitOptions.None);
				IConvertible c = args[argIndex] as IConvertible;
				object value2;
				if (c == null)
				{
					IConvertible convertible = args[argIndex].ToString();
					value2 = convertible;
				}
				else
				{
					value2 = c;
				}
				int count = Convert.ToInt32(value2);
				int rule = LocalizedText.CardinalPluralRule(Language.ActiveCulture, count);
				return options[Math.Min(rule, options.Length - 1)];
			});
		}

		/// <summary>
		/// Creates a string from this LocalizedText populated with data from the provided <paramref name="args" /> arguments. Formats the string in the same manner as <see href="https://learn.microsoft.com/en-us/dotnet/api/system.string.format?view=net-6.0">string.Format</see>. Placeholders such as "{0}", "{1}", etc will be replaced with the string representation of the corresponding objects provided.<br />
		/// Additionally, pluralization is supported as well. The <see href="https://github.com/tModLoader/tModLoader/wiki/Localization#string-formatting">Localization Guide</see> teaches more about placeholders and plural support.
		///
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		// Token: 0x060033D1 RID: 13265 RVA: 0x00555A1C File Offset: 0x00553C1C
		public string Format(params object[] args)
		{
			string value = this.Value;
			bool flag = this._hasPlurals.GetValueOrDefault();
			bool flag2;
			if (this._hasPlurals == null)
			{
				flag = LocalizedText.PluralizationPatternRegex.IsMatch(value);
				this._hasPlurals = new bool?(flag);
				flag2 = flag;
			}
			else
			{
				flag2 = flag;
			}
			if (flag2)
			{
				value = LocalizedText.ApplyPluralization(value, args);
			}
			string result;
			try
			{
				result = string.Format(value, args);
			}
			catch (FormatException e)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(93, 3);
				defaultInterpolatedStringHandler.AppendLiteral("The localization key:\n  \"");
				defaultInterpolatedStringHandler.AppendFormatted(this.Key);
				defaultInterpolatedStringHandler.AppendLiteral("\"\nwith a value of:\n  \"");
				defaultInterpolatedStringHandler.AppendFormatted(value);
				defaultInterpolatedStringHandler.AppendLiteral("\"\nfailed to be formatted with the inputs:\n  [");
				defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", args));
				defaultInterpolatedStringHandler.AppendLiteral("]");
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear(), e);
			}
			return result;
		}

		/// <summary>
		/// Creates a new LocalizedText with the supplied arguments formatted into the value (via <see cref="M:System.String.Format(System.String,System.Object[])" />)<br />
		/// Will automatically update to re-format the string with cached args when language changes. <br />
		///             <br />
		/// The resulting LocalizedText should be stored statically. Should not be used to create 'throwaway' LocalizedText instances. <br />
		/// Use <see cref="M:Terraria.Localization.LocalizedText.Format(System.Object[])" /> instead for repeated on-demand formatting with different args.
		/// <br /> The <see href="https://github.com/tModLoader/tModLoader/wiki/Localization#string-formatting">Localization Guide</see> teaches more about using placeholders in localization.
		/// </summary>
		/// <param name="args">The substitution args</param>
		/// <returns></returns>
		// Token: 0x060033D2 RID: 13266 RVA: 0x00555AFC File Offset: 0x00553CFC
		public LocalizedText WithFormatArgs(params object[] args)
		{
			return LanguageManager.Instance.BindFormatArgs(this.Key, args);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x00555B0F File Offset: 0x00553D0F
		internal void BindArgs(object[] args)
		{
			this.SetValue(this.Format(args));
			this.BoundArgs = args;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x00555B5B File Offset: 0x00553D5B
		[CompilerGenerated]
		internal static bool <CardinalPluralRule>g__contains|20_0(int i, int a, int b)
		{
			return i >= a && i <= b;
		}

		// Token: 0x04001E53 RID: 7763
		public static readonly LocalizedText Empty = new LocalizedText("", "");

		// Token: 0x04001E54 RID: 7764
		private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x04001E55 RID: 7765
		public readonly string Key;

		// Token: 0x04001E56 RID: 7766
		private string _value;

		// Token: 0x04001E57 RID: 7767
		private bool? _hasPlurals;

		// Token: 0x04001E59 RID: 7769
		public static readonly Regex PluralizationPatternRegex = new Regex("{\\^(\\d+):([^\\r\\n]+?)}", RegexOptions.Compiled);
	}
}
