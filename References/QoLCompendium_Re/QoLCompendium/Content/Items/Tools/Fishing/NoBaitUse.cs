using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Fishing
{
	// Token: 0x020001B2 RID: 434
	public class NoBaitUse : GlobalItem
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x0001C143 File Offset: 0x0001A343
		public override bool? CanConsumeBait(Player player, Item bait)
		{
			if ((player.HeldItem.type == ModContent.ItemType<LegendaryCatcher>() && bait.bait > 0) || bait.type == ModContent.ItemType<Eightworm>())
			{
				return new bool?(false);
			}
			return base.CanConsumeBait(player, bait);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001C17C File Offset: 0x0001A37C
		public override bool ConsumeItem(Item item, Player player)
		{
			return (player.HeldItem.type != ModContent.ItemType<LegendaryCatcher>() || item.bait <= 0) && item.type != ModContent.ItemType<Eightworm>() && base.ConsumeItem(item, player);
		}
	}
}
