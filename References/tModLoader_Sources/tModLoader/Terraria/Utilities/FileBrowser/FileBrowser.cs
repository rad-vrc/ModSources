using System;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x02000097 RID: 151
	public class FileBrowser
	{
		// Token: 0x060014AB RID: 5291 RVA: 0x004A2F14 File Offset: 0x004A1114
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

		// Token: 0x060014AC RID: 5292 RVA: 0x004A2F55 File Offset: 0x004A1155
		public static string OpenFilePanel(string title, ExtensionFilter[] extensions)
		{
			return FileBrowser._platformWrapper.OpenFilePanel(title, extensions);
		}

		// Token: 0x040010BE RID: 4286
		private static IFileBrowser _platformWrapper = new NativeFileDialog();
	}
}
