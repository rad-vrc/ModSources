using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using QoLCompendium.Core.UI.Panels;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.FavoriteEffect
{
	// Token: 0x020001B5 RID: 437
	public class DestinationGlobe : ModItem
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0001C53A File Offset: 0x0001A73A
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DestinationGlobe;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001C554 File Offset: 0x0001A754
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 26;
			base.Item.maxStack = 1;
			base.Item.consumable = false;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Green2, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001C5E3 File Offset: 0x0001A7E3
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DestinationGlobe);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0001C5FB File Offset: 0x0001A7FB
		public override bool? UseItem(Player player)
		{
			if (!DestinationGlobeUI.visible)
			{
				DestinationGlobeUI.timeStart = Main.GameUpdateCount;
			}
			DestinationGlobeUI.visible = true;
			return base.UseItem(player);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001C61B File Offset: 0x0001A81B
		public override bool CanUseItem(Player player)
		{
			return !DestinationGlobeUI.visible;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001C628 File Offset: 0x0001A828
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DestinationGlobe, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(170, 15);
			itemRecipe.AddIngredient(2, 5);
			itemRecipe.AddIngredient(62, 5);
			itemRecipe.AddIngredient(206, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		public override void UpdateInventory(Player player)
		{
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.NoModifier"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Desert"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Snow"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Jungle"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.GlowingMushroom"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 5)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Corruption"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 6)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Crimson"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 7 && Main.hardMode)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Hallow"));
			}
			if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 8)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Purity"));
			}
			if (base.Item.favorited)
			{
				player.GetModPlayer<QoLCPlayer>().activeItems.Add(base.Item.type);
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 1)
				{
					player.ZoneDesert = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 2)
				{
					player.ZoneSnow = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 3)
				{
					player.ZoneJungle = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 4)
				{
					player.ZoneGlowshroom = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 5)
				{
					player.ZoneCorrupt = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 6)
				{
					player.ZoneCrimson = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 7 && Main.hardMode)
				{
					player.ZoneHallow = true;
				}
				if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 8)
				{
					player.ZonePurity = true;
				}
			}
		}
	}
}
