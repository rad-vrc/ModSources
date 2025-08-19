using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000402 RID: 1026
	public class EntryFilterer<T, U> where T : new() where U : IEntryFilter<T>
	{
		// Token: 0x06002AFD RID: 11005 RVA: 0x0059D49D File Offset: 0x0059B69D
		public EntryFilterer()
		{
			this.AvailableFilters = new List<U>();
			this.ActiveFilters = new List<U>();
			this.AlwaysActiveFilters = new List<U>();
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0059D4C6 File Offset: 0x0059B6C6
		public void AddFilters(List<U> filters)
		{
			this.AvailableFilters.AddRange(filters);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0059D4D4 File Offset: 0x0059B6D4
		public bool FitsFilter(T entry)
		{
			if (this._searchFilter != null && !this._searchFilter.FitsFilter(entry))
			{
				return false;
			}
			for (int i = 0; i < this.AlwaysActiveFilters.Count; i++)
			{
				U u = this.AlwaysActiveFilters[i];
				if (!u.FitsFilter(entry))
				{
					return false;
				}
			}
			if (this.ActiveFilters.Count == 0)
			{
				return true;
			}
			for (int j = 0; j < this.ActiveFilters.Count; j++)
			{
				U u = this.ActiveFilters[j];
				if (u.FitsFilter(entry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0059D574 File Offset: 0x0059B774
		public void ToggleFilter(int filterIndex)
		{
			U item = this.AvailableFilters[filterIndex];
			if (this.ActiveFilters.Contains(item))
			{
				this.ActiveFilters.Remove(item);
				return;
			}
			this.ActiveFilters.Add(item);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0059D5B8 File Offset: 0x0059B7B8
		public bool IsFilterActive(int filterIndex)
		{
			if (!this.AvailableFilters.IndexInRange(filterIndex))
			{
				return false;
			}
			U item = this.AvailableFilters[filterIndex];
			return this.ActiveFilters.Contains(item);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0059D5EE File Offset: 0x0059B7EE
		public void SetSearchFilterObject<Z>(Z searchFilter) where Z : ISearchFilter<T>, U
		{
			this._searchFilterFromConstructor = searchFilter;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0059D5FC File Offset: 0x0059B7FC
		public void SetSearchFilter(string searchFilter)
		{
			if (string.IsNullOrWhiteSpace(searchFilter))
			{
				this._searchFilter = null;
				return;
			}
			this._searchFilter = this._searchFilterFromConstructor;
			this._searchFilter.SetSearch(searchFilter);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0059D628 File Offset: 0x0059B828
		public string GetDisplayName()
		{
			object obj = new
			{
				this.ActiveFilters.Count
			};
			return Language.GetTextValueWith("BestiaryInfo.Filters", obj);
		}

		// Token: 0x04004F3D RID: 20285
		public List<U> AvailableFilters;

		// Token: 0x04004F3E RID: 20286
		public List<U> ActiveFilters;

		// Token: 0x04004F3F RID: 20287
		public List<U> AlwaysActiveFilters;

		// Token: 0x04004F40 RID: 20288
		private ISearchFilter<T> _searchFilter;

		// Token: 0x04004F41 RID: 20289
		private ISearchFilter<T> _searchFilterFromConstructor;
	}
}
