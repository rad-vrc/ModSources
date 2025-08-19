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
using Terraria.ModLoader.IO;

namespace QoLCompendium.Content.Items.Tools.Staves
{
	// Token: 0x02000196 RID: 406
	public class Omnistaff : ModItem
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000190F4 File Offset: 0x000172F4
		public override void SetDefaults()
		{
			base.Item.useStyle = 1;
			base.Item.useTurn = true;
			base.Item.useAnimation = 25;
			base.Item.useTime = 13;
			base.Item.autoReuse = true;
			base.Item.width = 24;
			base.Item.height = 28;
			base.Item.damage = 14;
			base.Item.createTile = 633;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.LightPurple6, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000191C1 File Offset: 0x000173C1
		public override void SaveData(TagCompound tag)
		{
			tag["OmnistaffMode"] = this.Mode;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000191D9 File Offset: 0x000173D9
		public override void LoadData(TagCompound tag)
		{
			this.Mode = tag.GetInt("OmnistaffMode");
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000191EC File Offset: 0x000173EC
		public override void UpdateInventory(Player player)
		{
			if (this.Mode == 0)
			{
				base.Item.createTile = 2;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfRegrowth"));
			}
			if (this.Mode == 1)
			{
				base.Item.createTile = 23;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfCysting"));
			}
			if (this.Mode == 2)
			{
				base.Item.createTile = 199;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfHemorrhaging"));
			}
			if (this.Mode == 3)
			{
				base.Item.createTile = 109;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfHallowing"));
			}
			if (this.Mode == 4)
			{
				base.Item.createTile = 60;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfOvergrowth"));
			}
			if (this.Mode == 5)
			{
				base.Item.createTile = 661;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfOvergrownCysting"));
			}
			if (this.Mode == 6)
			{
				base.Item.createTile = 662;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfOvergrownHemorrhaging"));
			}
			if (this.Mode == 7)
			{
				base.Item.createTile = 70;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfShrooming"));
			}
			if (this.Mode == 8)
			{
				base.Item.createTile = 633;
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfAshing"));
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00019386 File Offset: 0x00017586
		public override void RightClick(Player player)
		{
			this.Mode++;
			if (this.Mode > 8)
			{
				this.Mode = 0;
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000193A8 File Offset: 0x000175A8
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "OmnistaffEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.OmnistaffPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00019440 File Offset: 0x00017640
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfAshing>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfCysting>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfHallowing>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfHemorrhaging>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfOvergrownCysting>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfOvergrownHemorrhaging>(), 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfOvergrowth>(), 1);
			itemRecipe.AddIngredient(213, 1);
			itemRecipe.AddIngredient(ModContent.ItemType<StaffOfShrooming>(), 1);
			itemRecipe.AddTile(283);
			itemRecipe.Register();
		}

		// Token: 0x0400003E RID: 62
		public int Mode;
	}
}
