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
	// Token: 0x020001F3 RID: 499
	public class Lamp : ModItem
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00020DAC File Offset: 0x0001EFAC
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(2420);
			base.Item.shoot = ModContent.ProjectileType<Moth>();
			base.Item.buffType = ModContent.BuffType<MothBuff>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00020CA7 File Offset: 0x0001EEA7
		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				player.AddBuff(base.Item.buffType, 3600, true, false);
			}
			return new bool?(true);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00020DFF File Offset: 0x0001EFFF
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<MothBuff>()) && base.CanUseItem(player);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00020E18 File Offset: 0x0001F018
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Jack"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00020E7C File Offset: 0x0001F07C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DedicatedItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1118, 1);
			itemRecipe.AddIngredient(530, 17);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
