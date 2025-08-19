using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000712 RID: 1810
	public interface IEntrySortStep<T> : IComparer<T>
	{
		// Token: 0x060049D4 RID: 18900
		string GetDisplayNameKey();
	}
}
