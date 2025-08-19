using System;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000708 RID: 1800
	public class EntryFilterer<T, U> where T : new() where U : IEntryFilter<T>
	{
		// Token: 0x06004993 RID: 18835 RVA: 0x0064DA92 File Offset: 0x0064BC92
		public EntryFilterer()
		{
			this.AvailableFilters = new List<U>();
			this.ActiveFilters = new List<U>();
			this.AlwaysActiveFilters = new List<U>();
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x0064DABB File Offset: 0x0064BCBB
		public void AddFilters(List<U> filters)
		{
			this.AvailableFilters.AddRange(filters);
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0064DACC File Offset: 0x0064BCCC
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

		// Token: 0x06004996 RID: 18838 RVA: 0x0064DB6C File Offset: 0x0064BD6C
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

		// Token: 0x06004997 RID: 18839 RVA: 0x0064DBB0 File Offset: 0x0064BDB0
		public bool IsFilterActive(int filterIndex)
		{
			if (!this.AvailableFilters.IndexInRange(filterIndex))
			{
				return false;
			}
			U item = this.AvailableFilters[filterIndex];
			return this.ActiveFilters.Contains(item);
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0064DBE6 File Offset: 0x0064BDE6
		public void SetSearchFilterObject<Z>(Z searchFilter) where Z : ISearchFilter<T>, U
		{
			this._searchFilterFromConstructor = searchFilter;
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x0064DBF4 File Offset: 0x0064BDF4
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

		// Token: 0x0600499A RID: 18842 RVA: 0x0064DC20 File Offset: 0x0064BE20
		public string GetDisplayName()
		{
			object obj = new
			{
				this.ActiveFilters.Count
			};
			return Language.GetTextValueWith("BestiaryInfo.Filters", obj);
		}

		// Token: 0x04005ECE RID: 24270
		public List<U> AvailableFilters;

		// Token: 0x04005ECF RID: 24271
		public List<U> ActiveFilters;

		// Token: 0x04005ED0 RID: 24272
		public List<U> AlwaysActiveFilters;

		// Token: 0x04005ED1 RID: 24273
		private ISearchFilter<T> _searchFilter;

		// Token: 0x04005ED2 RID: 24274
		private ISearchFilter<T> _searchFilterFromConstructor;
	}
}
