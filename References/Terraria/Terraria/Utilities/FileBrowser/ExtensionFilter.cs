using System;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x0200014E RID: 334
	public struct ExtensionFilter
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x004E0245 File Offset: 0x004DE445
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this.Name = filterName;
			this.Extensions = filterExtensions;
		}

		// Token: 0x04001523 RID: 5411
		public string Name;

		// Token: 0x04001524 RID: 5412
		public string[] Extensions;
	}
}
