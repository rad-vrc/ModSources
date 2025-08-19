using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Tiles.Dedicated;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Dedicated
{
	// Token: 0x020001F1 RID: 497
	public class Burger : ModItem
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00020B19 File Offset: 0x0001ED19
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 100;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00020B28 File Offset: 0x0001ED28
		public override void SetDefaults()
		{
			base.Item.width = 8;
			base.Item.height = 8;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.consumable = true;
			base.Item.useStyle = 2;
			base.Item.UseSound = new SoundStyle?(SoundID.Item2);
			base.Item.useTurn = true;
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.autoReuse = true;
			base.Item.noMelee = true;
			base.Item.createTile = ModContent.TileType<BurgerTile>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 1, 0, 0));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00020BF0 File Offset: 0x0001EDF0
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.BladeBurger"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}
	}
}
