// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.PhaseInterrupter
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class PhaseInterrupter : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.PhaseInterrupter;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 7;
    ((Entity) this.Item).height = 18;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 3, Item.buyPrice(0, 0, 90, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.PhaseInterrupter);
  }

  public virtual void UpdateInventory(Player player)
  {
    if (Main.moonPhase == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.FullMoon"));
    if (Main.moonPhase == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaningGibbous"));
    if (Main.moonPhase == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.ThirdQuarter"));
    if (Main.moonPhase == 3)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaningCrescent"));
    if (Main.moonPhase == 4)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.NewMoon"));
    if (Main.moonPhase == 5)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaxingCrescent"));
    if (Main.moonPhase == 6)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.FirstQuarter"));
    if (Main.moonPhase != 7)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.PhaseInterrupter.WaxingGibbous"));
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.PhaseInterrupter), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 7);
    itemRecipe.AddIngredient(182, 3);
    itemRecipe.AddIngredient(236, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual bool? UseItem(Player player)
  {
    if (!PhaseInterrupterUI.visible)
      PhaseInterrupterUI.timeStart = Main.GameUpdateCount;
    PhaseInterrupterUI.visible = true;
    return base.UseItem(player);
  }
}
