using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Staves
{
	// Token: 0x0200019B RID: 411
	public class StaffOfOvergrownCysting : ModItem
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00019C60 File Offset: 0x00017E60
		public override void SetDefaults()
		{
			base.Item.useStyle = 1;
			base.Item.useTurn = true;
			base.Item.useAnimation = 25;
			base.Item.useTime = 13;
			base.Item.autoReuse = true;
			base.Item.width = 24;
			base.Item.height = 28;
			base.Item.damage = 7;
			base.Item.createTile = 661;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00019D2C File Offset: 0x00017F2C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "StaffOfOvergrownCystingEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.StaffOfOvergrownCystingPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00019DC4 File Offset: 0x00017FC4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(176, 12);
			itemRecipe.AddIngredient(68, 3);
			itemRecipe.AddIngredient(59, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
