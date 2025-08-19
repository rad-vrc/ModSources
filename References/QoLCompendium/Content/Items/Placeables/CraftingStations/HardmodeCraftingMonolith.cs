// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Placeables.CraftingStations.HardmodeCraftingMonolith
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Tiles.CraftingStations;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Placeables.CraftingStations;

public class HardmodeCraftingMonolith : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.CraftingStations;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.DefaultToPlaceableTile(ModContent.TileType<HardmodeMonolithTile>(), 0);
    this.Item.SetShopValues((ItemRarityColor) 5, Item.buyPrice(0, 10, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.CraftingStations);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.CraftingStations), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup("QoLCompendium:HardmodeAnvils", 1);
    itemRecipe.AddRecipeGroup("QoLCompendium:HardmodeForges", 1);
    itemRecipe.AddRecipeGroup("QoLCompendium:AnyBookcase", 1);
    itemRecipe.AddIngredient(487, 1);
    itemRecipe.AddIngredient(2193, 1);
    itemRecipe.AddIngredient(4142, 1);
    itemRecipe.AddIngredient(2203, 1);
    itemRecipe.AddIngredient(995, 1);
    itemRecipe.AddIngredient(996, 1);
    itemRecipe.Register();
  }
}
