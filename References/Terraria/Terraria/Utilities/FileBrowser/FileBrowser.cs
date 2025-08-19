using System;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x0200014F RID: 335
	public class FileBrowser
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x004E0264 File Offset: 0x004DE464
		public static string OpenFilePanel(string title, string extension)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[]
				{
					extension
				});
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] extensions = array;
			return FileBrowser.OpenFilePanel(title, extensions);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x004E02A5 File Offset: 0x004DE4A5
		public static string OpenFilePanel(string title, ExtensionFilter[] extensions)
		{
			return FileBrowser._platformWrapper.OpenFilePanel(title, extensions);
		}

		// Token: 0x04001525 RID: 5413
		private static IFileBrowser _platformWrapper = new NativeFileDialog();
	}
}
