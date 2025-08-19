using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x0200020A RID: 522
	public class QoLCItem : GlobalItem
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x0005AA97 File Offset: 0x00058C97
		public override void UpdateInventory(Item item, Player player)
		{
			if (item.stack >= item.ResearchUnlockCount)
			{
				ModConditions.ItemHasBeenOwned[item.type] = true;
			}
		}
	}
}
