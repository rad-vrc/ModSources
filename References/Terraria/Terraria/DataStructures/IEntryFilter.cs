using System;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x020003FF RID: 1023
	public interface IEntryFilter<T>
	{
		// Token: 0x06002AF8 RID: 11000
		bool FitsFilter(T entry);

		// Token: 0x06002AF9 RID: 11001
		string GetDisplayNameKey();

		// Token: 0x06002AFA RID: 11002
		UIElement GetImage();
	}
}
