using System;
using System.Linq;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x02000099 RID: 153
	public class NativeFileDialog : IFileBrowser
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x004A2F6C File Offset: 0x004A116C
		public string OpenFilePanel(string title, ExtensionFilter[] extensions)
		{
			string[] value = extensions.SelectMany((ExtensionFilter x) => x.Extensions).ToArray<string>();
			string outPath;
			if (nativefiledialog.NFD_OpenDialog(string.Join(",", value), null, out outPath) == nativefiledialog.nfdresult_t.NFD_OKAY)
			{
				return outPath;
			}
			return null;
		}
	}
}
