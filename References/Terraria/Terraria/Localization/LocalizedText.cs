using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Terraria.Localization
{
	// Token: 0x020000AE RID: 174
	public class LocalizedText
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x004A0CA8 File Offset: 0x0049EEA8
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x004A0CB0 File Offset: 0x0049EEB0
		public string Value { get; private set; }

		// Token: 0x060013C9 RID: 5065 RVA: 0x004A0CB9 File Offset: 0x0049EEB9
		internal LocalizedText(string key, string text)
		{
			this.Key = key;
			this.Value = text;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x004A0CCF File Offset: 0x0049EECF
		internal void SetValue(string text)
		{
			this.Value = text;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x004A0CD8 File Offset: 0x0049EED8
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
				if (propertyDescriptor == null)
				{
					return "";
				}
				return (propertyDescriptor.GetValue(obj) ?? "").ToString();
			});
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x004A0D24 File Offset: 0x0049EF24
		public bool CanFormatWith(object obj)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			foreach (object obj2 in LocalizedText._substitutionRegex.Matches(this.Value))
			{
				Match match = (Match)obj2;
				string name = match.Groups[2].ToString();
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
				if (match.Groups[1].Length != 0 && (((value as bool?) ?? false) ^ match.Groups[1].Length == 1))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x004A0E20 File Offset: 0x0049F020
		public NetworkText ToNetworkText()
		{
			return NetworkText.FromKey(this.Key, new object[0]);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x004A0E33 File Offset: 0x0049F033
		public NetworkText ToNetworkText(params object[] substitutions)
		{
			return NetworkText.FromKey(this.Key, substitutions);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x004A0E41 File Offset: 0x0049F041
		public static explicit operator string(LocalizedText text)
		{
			return text.Value;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x004A0E49 File Offset: 0x0049F049
		public string Format(object arg0)
		{
			return string.Format(this.Value, arg0);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x004A0E57 File Offset: 0x0049F057
		public string Format(object arg0, object arg1)
		{
			return string.Format(this.Value, arg0, arg1);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x004A0E66 File Offset: 0x0049F066
		public string Format(object arg0, object arg1, object arg2)
		{
			return string.Format(this.Value, arg0, arg1, arg2);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x004A0E76 File Offset: 0x0049F076
		public string Format(params object[] args)
		{
			return string.Format(this.Value, args);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x004A0E84 File Offset: 0x0049F084
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x040011BD RID: 4541
		public static readonly LocalizedText Empty = new LocalizedText("", "");

		// Token: 0x040011BE RID: 4542
		private static Regex _substitutionRegex = new Regex("{(\\?(?:!)?)?([a-zA-Z][\\w\\.]*)}", RegexOptions.Compiled);

		// Token: 0x040011BF RID: 4543
		public readonly string Key;
	}
}
