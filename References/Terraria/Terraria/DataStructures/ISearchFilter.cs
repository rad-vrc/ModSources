using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000401 RID: 1025
	public interface ISearchFilter<T> : IEntryFilter<T>
	{
		// Token: 0x06002AFC RID: 11004
		void SetSearch(string searchText);
	}
}
