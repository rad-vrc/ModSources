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
	// Token: 0x0200019C RID: 412
	public class StaffOfOvergrownHemorrhaging : ModItem
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00019E34 File Offset: 0x00018034
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
			base.Item.createTile = 662;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00019F00 File Offset: 0x00018100
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "StaffOfOvergrownHemorrhagingEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.StaffOfOvergrownHemorrhagingPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00019F98 File Offset: 0x00018198
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(176, 12);
			itemRecipe.AddIngredient(1330, 3);
			itemRecipe.AddIngredient(2171, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
