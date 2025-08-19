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

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000180 RID: 384
	public class PhaseInterrupter : ModItem
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0001579D File Offset: 0x0001399D
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.PhaseInterrupter;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000157B8 File Offset: 0x000139B8
		public override void SetDefaults()
		{
			base.Item.width = 7;
			base.Item.height = 18;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(0, 0, 90, 0));
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001582F File Offset: 0x00013A2F
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.PhaseInterrupter);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00015848 File Offset: 0x00013A48
		public override void UpdateInventory(Player player)
		{
			if (Main.moonPhase == 0)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.FullMoon"));
			}
			if (Main.moonPhase == 1)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaningGibbous"));
			}
			if (Main.moonPhase == 2)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.ThirdQuarter"));
			}
			if (Main.moonPhase == 3)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaningCrescent"));
			}
			if (Main.moonPhase == 4)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.NewMoon"));
			}
			if (Main.moonPhase == 5)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaxingCrescent"));
			}
			if (Main.moonPhase == 6)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.FirstQuarter"));
			}
			if (Main.moonPhase == 7)
			{
				base.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaxingGibbous"));
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001593C File Offset: 0x00013B3C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.PhaseInterrupter, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 7);
			itemRecipe.AddIngredient(182, 3);
			itemRecipe.AddIngredient(236, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000159AF File Offset: 0x00013BAF
		public override bool? UseItem(Player player)
		{
			if (!PhaseInterrupterUI.visible)
			{
				PhaseInterrupterUI.timeStart = Main.GameUpdateCount;
			}
			PhaseInterrupterUI.visible = true;
			return base.UseItem(player);
		}
	}
}
