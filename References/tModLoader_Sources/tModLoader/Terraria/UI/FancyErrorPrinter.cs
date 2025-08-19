using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ReLogic.Content;
using Terraria.ModLoader.Engine;

namespace Terraria.UI
{
	// Token: 0x020000A0 RID: 160
	public class FancyErrorPrinter
	{
		// Token: 0x060014E0 RID: 5344 RVA: 0x004A6808 File Offset: 0x004A4A08
		public static void ShowFailedToLoadAssetError(Exception exception, string filePath)
		{
			bool flag = false;
			if (exception is UnauthorizedAccessException)
			{
				flag = true;
			}
			if (exception is FileNotFoundException)
			{
				flag = true;
			}
			if (exception is DirectoryNotFoundException)
			{
				flag = true;
			}
			if (exception is AssetLoadException)
			{
				flag = true;
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Failed to load asset: \"" + filePath.Replace("/", "\\") + "\"!");
				List<string> suggestions = new List<string>
				{
					"Try verifying integrity of game files via Steam, the asset may be missing.",
					"If you are using an Anti-virus, please make sure it does not block Terraria in any way."
				};
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Suggestions:");
				FancyErrorPrinter.AppendSuggestions(stringBuilder, suggestions);
				stringBuilder.AppendLine();
				FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
				ErrorReporting.MessageBoxShow(stringBuilder.ToString(), false);
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x004A68BC File Offset: 0x004A4ABC
		public static void ShowFileSavingFailError(Exception exception, string filePath)
		{
			bool flag = false;
			if (exception is UnauthorizedAccessException)
			{
				flag = true;
			}
			if (exception is FileNotFoundException)
			{
				flag = true;
			}
			if (exception is DirectoryNotFoundException)
			{
				flag = true;
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Failed to create the file: \"" + filePath.Replace("/", "\\") + "\"!");
				List<string> list = new List<string>
				{
					"If you are using an Anti-virus, please make sure it does not block Terraria in any way.",
					"Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.",
					"Try verifying integrity of game files via Steam."
				};
				if (filePath.ToLower().Contains("onedrive"))
				{
					list.Add("Try updating OneDrive.");
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Suggestions:");
				FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
				stringBuilder.AppendLine();
				FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
				ErrorReporting.MessageBoxShow(stringBuilder.ToString(), false);
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x004A6994 File Offset: 0x004A4B94
		public static void ShowDirectoryCreationFailError(Exception exception, string folderPath)
		{
			bool flag = false;
			if (exception is UnauthorizedAccessException)
			{
				flag = true;
			}
			if (exception is FileNotFoundException)
			{
				flag = true;
			}
			if (exception is DirectoryNotFoundException)
			{
				flag = true;
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Failed to create the folder: \"" + folderPath.Replace("/", "\\") + "\"!");
				List<string> list = new List<string>
				{
					"If you are using an Anti-virus, please make sure it does not block Terraria in any way.",
					"Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.",
					"Try verifying integrity of game files via Steam."
				};
				if (folderPath.ToLower().Contains("onedrive"))
				{
					list.Add("Try updating OneDrive.");
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Suggestions:");
				FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
				stringBuilder.AppendLine();
				FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
				ErrorReporting.MessageBoxShow(stringBuilder.ToString(), false);
			}
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x004A6A69 File Offset: 0x004A4C69
		private static void IncludeOriginalMessage(StringBuilder text, Exception exception)
		{
			text.AppendLine("The original Error below");
			text.Append(exception);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x004A6A80 File Offset: 0x004A4C80
		private static void AppendSuggestions(StringBuilder text, List<string> suggestions)
		{
			for (int i = 0; i < suggestions.Count; i++)
			{
				string text2 = suggestions[i];
				text.AppendLine((i + 1).ToString() + ". " + text2);
			}
		}
	}
}
