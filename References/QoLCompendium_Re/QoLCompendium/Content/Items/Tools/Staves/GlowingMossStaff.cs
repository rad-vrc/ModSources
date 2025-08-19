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
	// Token: 0x02000194 RID: 404
	public class GlowingMossStaff : ModItem
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x0001876F File Offset: 0x0001696F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.RegrowthStaves;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001878C File Offset: 0x0001698C
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
			base.Item.createTile = 539;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.knockBack = 3f;
			base.Item.SetShopValues(ItemRarityColor.LightPurple6, Item.sellPrice(0, 0, 50, 0));
			base.Item.DamageType = DamageClass.Melee;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00018859 File Offset: 0x00016A59
		public override void SaveData(TagCompound tag)
		{
			tag["GlowingMossStaffMode"] = this.Mode;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00018871 File Offset: 0x00016A71
		public override void LoadData(TagCompound tag)
		{
			this.Mode = tag.GetInt("GlowingMossStaffMode");
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick()
		{
			return true;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00018884 File Offset: 0x00016A84
		public unsafe override void HoldItem(Player player)
		{
			if (this.Mode == 0)
			{
				base.Item.createTile = 539;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 540;
				}
			}
			if (this.Mode == 1)
			{
				base.Item.createTile = 534;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 535;
				}
			}
			if (this.Mode == 2)
			{
				base.Item.createTile = 381;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 517;
				}
			}
			if (this.Mode == 3)
			{
				base.Item.createTile = 625;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 626;
				}
			}
			if (this.Mode == 4)
			{
				base.Item.createTile = 536;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 537;
				}
			}
			if (this.Mode == 5)
			{
				base.Item.createTile = 627;
				if (*Main.tile[Main.mouseX, Main.mouseY].TileType == 38)
				{
					base.Item.createTile = 628;
				}
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00018A4C File Offset: 0x00016C4C
		public override void UpdateInventory(Player player)
		{
			if (this.Mode == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.GlowingMossStaff.ArgonMoss"));
			}
			if (this.Mode == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.GlowingMossStaff.KryptonMoss"));
			}
			if (this.Mode == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.GlowingMossStaff.LavaMoss"));
			}
			if (this.Mode == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.GlowingMossStaff.NeonMoss"));
			}
			if (this.Mode == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.GlowingMossStaff.XenonMoss"));
			}
			if (this.Mode == 5)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.GlowingMossStaff.HeliumMoss"));
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00018B0C File Offset: 0x00016D0C
		public override void RightClick(Player player)
		{
			this.Mode++;
			if (this.Mode > 5)
			{
				this.Mode = 0;
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override void OnConsumeItem(Player player)
		{
			base.Item.stack++;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00018B2C File Offset: 0x00016D2C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine placeable = tooltips.Find((TooltipLine l) => l.Name == "Placeable");
			TooltipLine text = new TooltipLine(base.Mod, "MossStaffPlaceable", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.GlowingMossStaffPlaceable"));
			tooltips.Insert(tooltips.IndexOf(placeable), text);
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Placeable" && x.Mod == "Terraria");
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.RegrowthStaves);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00018BC4 File Offset: 0x00016DC4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.RegrowthStaves, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3, 12);
			itemRecipe.AddIngredient(129, 12);
			itemRecipe.AddIngredient(4389, 5);
			itemRecipe.AddIngredient(4377, 5);
			itemRecipe.AddIngredient(4354, 5);
			itemRecipe.AddIngredient(5127, 5);
			itemRecipe.AddIngredient(4378, 5);
			itemRecipe.AddIngredient(5128, 5);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x0400003C RID: 60
		public int Mode;
	}
}
