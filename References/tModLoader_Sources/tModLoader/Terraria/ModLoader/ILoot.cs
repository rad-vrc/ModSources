using System;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace Terraria.ModLoader
{
	// Token: 0x0200017E RID: 382
	public interface ILoot
	{
		// Token: 0x06001DFD RID: 7677
		List<IItemDropRule> Get(bool includeGlobalDrops = true);

		// Token: 0x06001DFE RID: 7678
		IItemDropRule Add(IItemDropRule entry);

		// Token: 0x06001DFF RID: 7679
		IItemDropRule Remove(IItemDropRule entry);

		// Token: 0x06001E00 RID: 7680
		void RemoveWhere(Predicate<IItemDropRule> predicate, bool includeGlobalDrops = true);
	}
}
