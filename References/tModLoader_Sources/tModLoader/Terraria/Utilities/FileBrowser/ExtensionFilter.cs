using System;

namespace Terraria.Utilities.FileBrowser
{
	// Token: 0x02000096 RID: 150
	public struct ExtensionFilter
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x004A2EF5 File Offset: 0x004A10F5
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this.Name = filterName;
			this.Extensions = filterExtensions;
		}

		// Token: 0x040010BC RID: 4284
		public string Name;

		// Token: 0x040010BD RID: 4285
		public string[] Extensions;
	}
}
