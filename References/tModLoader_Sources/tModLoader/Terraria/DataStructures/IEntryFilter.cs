using System;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x02000711 RID: 1809
	public interface IEntryFilter<T>
	{
		// Token: 0x060049D1 RID: 18897
		bool FitsFilter(T entry);

		// Token: 0x060049D2 RID: 18898
		string GetDisplayNameKey();

		// Token: 0x060049D3 RID: 18899
		UIElement GetImage();
	}
}
