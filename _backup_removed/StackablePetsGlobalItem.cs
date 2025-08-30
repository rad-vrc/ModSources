using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StackablePetItems
{
	// RadQoL: StackablePetItems 互換のGlobalItem（機能維持）
	public class StackablePetsGlobalItem : GlobalItem
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021B8 File Offset: 0x000003B8
		private bool Check(Item item)
		{
			if (item.CanHavePrefixes())
			{
				return false;
			}
			bool flag = item.buffType > 0 && (Main.vanityPet[item.buffType] || Main.lightPet[item.buffType]);
			bool isMount = item.mountType >= 0;
			bool isGrapple = Main.projHook[item.shoot];
			bool isVanity = item.vanity;
			bool isTool = ItemID.Sets.DuplicationMenuToolsFilter[item.type];
			bool isTileWand = item.tileWand > 0 && item.createTile > 0;
			bool isFishRod = item.fishingPole > 0;
			return flag || isMount || isGrapple || isVanity || isTool || isTileWand || isFishRod;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002256 File Offset: 0x00000456
		public override void SetDefaults(Item item)
		{
			if (this.Check(item))
			{
				item.maxStack = Item.CommonMaxStack;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool CanStackInWorld(Item destination, Item source)
		{
			return (destination.type == source.type && this.Check(source)) || base.CanStackInWorld(destination, source);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000228F File Offset: 0x0000048F
		public override bool CanStack(Item destination, Item source)
		{
			return (destination.type == source.type && this.Check(source)) || base.CanStack(destination, source);
		}
	}
}
