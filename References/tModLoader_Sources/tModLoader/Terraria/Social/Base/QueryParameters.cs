using System;
using Terraria.ModLoader.UI.ModBrowser;

namespace Terraria.Social.Base
{
	// Token: 0x02000107 RID: 263
	public struct QueryParameters
	{
		// Token: 0x04001392 RID: 5010
		public string[] searchTags;

		// Token: 0x04001393 RID: 5011
		public ModPubId_t[] searchModIds;

		// Token: 0x04001394 RID: 5012
		public string[] searchModSlugs;

		// Token: 0x04001395 RID: 5013
		public string searchGeneric;

		// Token: 0x04001396 RID: 5014
		public string searchAuthor;

		// Token: 0x04001397 RID: 5015
		public uint days;

		// Token: 0x04001398 RID: 5016
		public ModBrowserSortMode sortingParamater;

		// Token: 0x04001399 RID: 5017
		public UpdateFilter updateStatusFilter;

		// Token: 0x0400139A RID: 5018
		public ModSideFilter modSideFilter;

		// Token: 0x0400139B RID: 5019
		public QueryType queryType;
	}
}
