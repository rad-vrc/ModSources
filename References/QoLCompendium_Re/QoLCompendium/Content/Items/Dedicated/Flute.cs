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
	// Token: 0x020001F2 RID: 498
	public class Flute : ModItem
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00020C54 File Offset: 0x0001EE54
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(4425);
			base.Item.shoot = ModContent.ProjectileType<Snake>();
			base.Item.buffType = ModContent.BuffType<SnakeBuff>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00020CA7 File Offset: 0x0001EEA7
		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				player.AddBuff(base.Item.buffType, 3600, true, false);
			}
			return new bool?(true);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00020CD4 File Offset: 0x0001EED4
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<SnakeBuff>()) && base.CanUseItem(player);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00020CEC File Offset: 0x0001EEEC
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Jabon"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.Orange, Color.Yellow, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00020D50 File Offset: 0x0001EF50
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DedicatedItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2504, 16);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
