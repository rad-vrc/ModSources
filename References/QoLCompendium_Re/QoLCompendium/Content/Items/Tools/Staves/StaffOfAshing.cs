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
	// Token: 0x02000197 RID: 407
	public class StaffOfAshing : ModItem
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00019504 File Offset: 0x00017704
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
			base.Item.createTile = 633;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000195D0 File Offset: 0x000177D0
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "StaffOfAshingEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.StaffOfAshingPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00019668 File Offset: 0x00017868
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(5215, 12);
			itemRecipe.AddIngredient(172, 3);
			itemRecipe.AddIngredient(5214, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
