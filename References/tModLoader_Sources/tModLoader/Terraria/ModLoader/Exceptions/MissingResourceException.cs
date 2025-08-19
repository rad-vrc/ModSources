using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Terraria.Localization;

namespace Terraria.ModLoader.Exceptions
{
	// Token: 0x020002A3 RID: 675
	public class MissingResourceException : Exception
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x0052AC94 File Offset: 0x00528E94
		public override string HelpLink
		{
			get
			{
				return "https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-FAQ#terrariamodloadermodgettexturestring-name-error";
			}
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x0052AC9B File Offset: 0x00528E9B
		public MissingResourceException()
		{
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x0052ACA3 File Offset: 0x00528EA3
		public MissingResourceException(string message) : base(message)
		{
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x0052ACAC File Offset: 0x00528EAC
		public MissingResourceException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0052ACB6 File Offset: 0x00528EB6
		public MissingResourceException(List<string> reasons, string assetPath, ICollection<string> keys) : this(MissingResourceException.ProcessMessage(reasons, assetPath, keys))
		{
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0052ACC8 File Offset: 0x00528EC8
		public static string ProcessMessage(List<string> reasons, string assetPath, ICollection<string> keys)
		{
			if (reasons.Count > 0)
			{
				reasons.Insert(0, "Failed to load asset: \"" + assetPath + "\"");
				if (reasons.Any((string x) => x.Contains("Texture2D creation failed! Error Code: The parameter is incorrect.")))
				{
					reasons.Insert(1, "The most common reason for this \"Texture2D creation failed!\" error is a malformed .png file. Make sure you are saving textures in the .png format and are not just renaming the file extension of your texture files to .png, that does not work.");
				}
				return string.Join(Environment.NewLine, reasons);
			}
			string closestMatch = LevenshteinDistance.FolderAwareEditDistance(assetPath, keys.ToArray<string>());
			if (closestMatch != null && closestMatch != "")
			{
				ValueTuple<string, string> valueTuple = LevenshteinDistance.ComputeColorTaggedString(assetPath, closestMatch);
				string a = valueTuple.Item1;
				string b = valueTuple.Item2;
				string message = string.Concat(new string[]
				{
					Language.GetTextValue("tModLoader.LoadErrorResourceNotFoundPathHint", assetPath, closestMatch),
					"\n",
					a,
					"\n",
					b,
					"\n"
				});
				if (new StackTrace().ToString().Contains("Terraria.ModLoader.EquipLoader.AddEquipTexture"))
				{
					message = message + "\n" + Language.GetTextValue("tModLoader.LoadErrorResourceNotFoundEquipTextureHint") + "\n";
				}
				return message;
			}
			return assetPath;
		}
	}
}
