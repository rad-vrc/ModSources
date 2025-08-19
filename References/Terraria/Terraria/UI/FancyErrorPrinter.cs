using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ReLogic.Content;
using ReLogic.OS;

namespace Terraria.UI
{
	// Token: 0x0200007F RID: 127
	public class FancyErrorPrinter
	{
		// Token: 0x060011D2 RID: 4562 RVA: 0x0048F4BC File Offset: 0x0048D6BC
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
			if (!flag)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Failed to load asset: \"" + filePath.Replace("/", "\\") + "\"!");
			List<string> list = new List<string>();
			list.Add("Try verifying integrity of game files via Steam, the asset may be missing.");
			list.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Suggestions:");
			FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
			stringBuilder.AppendLine();
			FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
			FancyErrorPrinter.ShowTheBox(stringBuilder.ToString());
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0048F57C File Offset: 0x0048D77C
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
			if (!flag)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Failed to create the file: \"" + filePath.Replace("/", "\\") + "\"!");
			List<string> list = new List<string>();
			list.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
			list.Add("Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.");
			list.Add("Try verifying integrity of game files via Steam.");
			if (filePath.ToLower().Contains("onedrive"))
			{
				list.Add("Try updating OneDrive.");
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Suggestions:");
			FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
			stringBuilder.AppendLine();
			FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
			FancyErrorPrinter.ShowTheBox(stringBuilder.ToString());
			Console.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0048F65C File Offset: 0x0048D85C
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
			if (!flag)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Failed to create the folder: \"" + folderPath.Replace("/", "\\") + "\"!");
			List<string> list = new List<string>();
			list.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
			list.Add("Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.");
			list.Add("Try verifying integrity of game files via Steam.");
			if (folderPath.ToLower().Contains("onedrive"))
			{
				list.Add("Try updating OneDrive.");
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Suggestions:");
			FancyErrorPrinter.AppendSuggestions(stringBuilder, list);
			stringBuilder.AppendLine();
			FancyErrorPrinter.IncludeOriginalMessage(stringBuilder, exception);
			FancyErrorPrinter.ShowTheBox(stringBuilder.ToString());
			Console.WriteLine(exception);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0048F734 File Offset: 0x0048D934
		private static void IncludeOriginalMessage(StringBuilder text, Exception exception)
		{
			text.AppendLine("The original Error below");
			text.Append(exception);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0048F74C File Offset: 0x0048D94C
		private static void AppendSuggestions(StringBuilder text, List<string> suggestions)
		{
			for (int i = 0; i < suggestions.Count; i++)
			{
				string str = suggestions[i];
				text.AppendLine((i + 1).ToString() + ". " + str);
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0048F78F File Offset: 0x0048D98F
		private static void ShowTheBox(string preparedMessage)
		{
			if (Platform.IsWindows && !Main.dedServ)
			{
				MessageBox.Show(preparedMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}
}
