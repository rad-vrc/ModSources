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
	// Token: 0x02000198 RID: 408
	public class StaffOfCysting : ModItem
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000196DC File Offset: 0x000178DC
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
			base.Item.createTile = 23;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000197A8 File Offset: 0x000179A8
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "StaffOfCystingEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.StaffOfCystingPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00019840 File Offset: 0x00017A40
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(619, 12);
			itemRecipe.AddIngredient(68, 3);
			itemRecipe.AddIngredient(59, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
