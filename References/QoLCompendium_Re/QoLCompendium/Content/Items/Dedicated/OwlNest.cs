using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Buffs;
using QoLCompendium.Content.Projectiles.Dedicated;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Dedicated
{
	// Token: 0x020001F5 RID: 501
	public class OwlNest : ModItem
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00021018 File Offset: 0x0001F218
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(2420);
			base.Item.shoot = ModContent.ProjectileType<Owl>();
			base.Item.buffType = ModContent.BuffType<OwlBuff>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00020CA7 File Offset: 0x0001EEA7
		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				player.AddBuff(base.Item.buffType, 3600, true, false);
			}
			return new bool?(true);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002106B File Offset: 0x0001F26B
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<OwlBuff>()) && base.CanUseItem(player);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00021084 File Offset: 0x0001F284
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Ned"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DedicatedItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(9, 12);
			itemRecipe.AddIngredient(150, 7);
			itemRecipe.AddIngredient(210, 2);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
