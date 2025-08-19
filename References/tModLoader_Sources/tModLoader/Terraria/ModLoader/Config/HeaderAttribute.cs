using System;
using Terraria.Localization;

namespace Terraria.ModLoader.Config
{
	/// <summary>
	/// This attribute adds a label above this property or field in the ModConfig UI that acts as a header. Use this to delineate sections within your config. <br />
	/// Note that fields will be in order, and properties will be in order, but fields and properties will not be interleaved together in the source code order. <br />
	/// <br />
	/// Header accept either a translation key or an identifier. <br />
	/// To use a translation key, the value passed in must start with "$". <br />
	/// A value passed in that does not start with "$" is interpreted as an identifier. The identifier is used to construct the localization key "Mods.{ModName}.Configs.{ConfigName}.Headers.{Identifier}" <br />
	/// No spaces are allowed in translation keys, so avoid spaces <br />
	/// Annotations on members of non-ModConfig classes need to supply a localization key using this attribute to be localized, no localization key can be correctly assumed using just an identifier. <br />
	/// </summary>
	// Token: 0x02000375 RID: 885
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class HeaderAttribute : Attribute
	{
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x0053CFDB File Offset: 0x0053B1DB
		public bool IsIdentifier
		{
			get
			{
				return this.identifier != null;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x0053CFE6 File Offset: 0x0053B1E6
		public string Header
		{
			get
			{
				return Language.GetTextValue(this.key);
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0053CFF4 File Offset: 0x0053B1F4
		public HeaderAttribute(string identifierOrKey)
		{
			if (string.IsNullOrWhiteSpace(identifierOrKey) || identifierOrKey.Contains(' '))
			{
				this.malformed = true;
				return;
			}
			if (!identifierOrKey.StartsWith("$"))
			{
				this.identifier = identifierOrKey;
				return;
			}
			this.key = identifierOrKey.Substring(1);
		}

		// Token: 0x04001D1B RID: 7451
		internal string key;

		// Token: 0x04001D1C RID: 7452
		internal string identifier;

		// Token: 0x04001D1D RID: 7453
		internal readonly bool malformed;
	}
}
