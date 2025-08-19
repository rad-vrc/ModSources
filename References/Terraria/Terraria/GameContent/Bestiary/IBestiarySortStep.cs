using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002EE RID: 750
	public interface IBestiarySortStep : IEntrySortStep<BestiaryEntry>, IComparer<BestiaryEntry>
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600237E RID: 9086
		bool HiddenFromSortOptions { get; }
	}
}
