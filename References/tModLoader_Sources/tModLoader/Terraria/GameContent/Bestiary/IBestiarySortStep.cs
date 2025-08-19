using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200068C RID: 1676
	public interface IBestiarySortStep : IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060047F4 RID: 18420
		bool HiddenFromSortOptions { get; }
	}
}
