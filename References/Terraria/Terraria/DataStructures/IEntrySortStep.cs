using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000400 RID: 1024
	public interface IEntrySortStep<T> : IComparer<T>
	{
		// Token: 0x06002AFB RID: 11003
		string GetDisplayNameKey();
	}
}
