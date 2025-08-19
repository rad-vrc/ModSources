using System;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000255 RID: 597
	public class UIModsFilterResults
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x0051B09B File Offset: 0x0051929B
		public bool AnyFiltered
		{
			get
			{
				return this.filteredBySearch > 0 || this.filteredByModSide > 0 || this.filteredByEnabled > 0;
			}
		}

		// Token: 0x04001B0E RID: 6926
		public int filteredBySearch;

		// Token: 0x04001B0F RID: 6927
		public int filteredByModSide;

		// Token: 0x04001B10 RID: 6928
		public int filteredByEnabled;
	}
}
