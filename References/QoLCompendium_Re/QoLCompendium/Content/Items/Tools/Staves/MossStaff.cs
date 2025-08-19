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
	// Token: 0x02000195 RID: 405
	public class MossStaff : ModItem
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00018C78 File Offset: 0x00016E78
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
			base.Item.createTile = 182;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.LightPurple6, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00018D45 File Offset: 0x00016F45
		public override void SaveData(TagCompound tag)
		{
			tag["MossStaffMode"] = this.Mode;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00018D5D File Offset: 0x00016F5D
		public override void LoadData(TagCompound tag)
		{
			this.Mode = tag.GetInt("MossStaffMode");
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00018D70 File Offset: 0x00016F70
		public unsafe override void HoldItem(Player player)
		{
			if (this.Mode == 0)
			{
				base.Item.createTile = 182;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 515;
				}
			}
			if (this.Mode == 1)
			{
				base.Item.createTile = 180;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 513;
				}
			}
			if (this.Mode == 2)
			{
				base.Item.createTile = 179;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 512;
				}
			}
			if (this.Mode == 3)
			{
				base.Item.createTile = 183;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 516;
				}
			}
			if (this.Mode == 4)
			{
				base.Item.createTile = 181;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 514;
				}
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00018EF0 File Offset: 0x000170F0
		public override void UpdateInventory(Player player)
		{
			if (this.Mode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.BlueMoss"));
			}
			if (this.Mode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.BrownMoss"));
			}
			if (this.Mode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.GreenMoss"));
			}
			if (this.Mode == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.PurpleMoss"));
			}
			if (this.Mode == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.RedMoss"));
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00018F92 File Offset: 0x00017192
		public override void RightClick(Player player)
		{
			this.Mode++;
			if (this.Mode > 4)
			{
				this.Mode = 0;
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00018FB4 File Offset: 0x000171B4
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "MossStaffPlaceable", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.MossStaffPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001904C File Offset: 0x0001724C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3, 12);
			itemRecipe.AddIngredient(129, 12);
			itemRecipe.AddIngredient(4352, 5);
			itemRecipe.AddIngredient(4350, 5);
			itemRecipe.AddIngredient(4349, 5);
			itemRecipe.AddIngredient(4353, 5);
			itemRecipe.AddIngredient(4351, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x0400003D RID: 61
		public int Mode;
	}
}
