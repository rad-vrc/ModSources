using System;
using System.Linq;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x0200014C RID: 332
	public class NativeFileDialog : IFileBrowser
	{
		// Token: 0x06001923 RID: 6435 RVA: 0x004E01F4 File Offset: 0x004DE3F4
		public string OpenFilePanel(string title, ExtensionFilter[] extensions)
		{
			string[] value = extensions.SelectMany((ExtensionFilter x) => x.Extensions).ToArray<string>();
			string result;
			if (nativefiledialog.NFD_OpenDialog(string.Join(",", value), null, out result) == nativefiledialog.nfdresult_t.NFD_OKAY)
			{
				return result;
			}
			return null;
		}
	}
}
