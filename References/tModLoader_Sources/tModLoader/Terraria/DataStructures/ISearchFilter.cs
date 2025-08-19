using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000715 RID: 1813
	public interface ISearchFilter<T> : IEntryFilter<T>
	{
		// Token: 0x060049D7 RID: 18903
		void SetSearch(string searchText);
	}
}
