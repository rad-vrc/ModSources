using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Accessories.Construction
{
	// Token: 0x020001EF RID: 495
	public class CreationClubMembersPass : ModItem
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x000206F7 File Offset: 0x0001E8F7
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.ConstructionAccessories;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00020838 File Offset: 0x0001EA38
		public override void SetDefaults()
		{
			base.Item.width = 21;
			base.Item.height = 13;
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.Lime7, Item.buyPrice(0, 10, 0, 0));
			base.Item.accessory = true;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00020890 File Offset: 0x0001EA90
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.tileSpeed += 0.25f;
			player.wallSpeed += 0.25f;
			player.pickSpeed -= 0.25f;
			player.equippedAnyTileSpeedAcc = true;
			player.equippedAnyTileRangeAcc = true;
			player.autoPaint = true;
			player.equippedAnyWallSpeedAcc = true;
			player.chiselSpeed = true;
			player.treasureMagnet = true;
			player.portableStoolInfo.SetStats(26, 26, 26);
			player.autoActuator = true;
			player.blockRange++;
			Player.tileRangeX++;
			Player.tileRangeY++;
			player.CanSeeInvisibleBlocks = true;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002078E File Offset: 0x0001E98E
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.ConstructionAccessories);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00020944 File Offset: 0x0001EB44
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.ConstructionAccessories, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<MiningEmblem>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<ConstructionEmblem>(), 1);
			itemRecipe.AddIngredient(5126, 1);
			itemRecipe.AddIngredient(407, 1);
			itemRecipe.AddIngredient(1923, 1);
			itemRecipe.AddIngredient(3624, 1);
			itemRecipe.AddIngredient(4409, 1);
			itemRecipe.AddTile(114);
			itemRecipe.Register();
		}
	}
}
