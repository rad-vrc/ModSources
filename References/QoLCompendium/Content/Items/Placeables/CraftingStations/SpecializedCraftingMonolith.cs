// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Placeables.CraftingStations.SpecializedCraftingMonolith
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

public class SpecializedCraftingMonolith : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.CraftingStations;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.DefaultToPlaceableTile(ModContent.TileType<SpecializedMonolithTile>(), 0);
    this.Item.SetShopValues((ItemRarityColor) 3, Item.buyPrice(0, 10, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.CraftingStations);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.CraftingStations), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(2192, 1);
    itemRecipe.AddIngredient(2194, 1);
    itemRecipe.AddIngredient(2204, 1);
    itemRecipe.AddIngredient(2198, 1);
    itemRecipe.AddIngredient(2196, 1);
    itemRecipe.AddIngredient(2197, 1);
    itemRecipe.AddIngredient(998, 1);
    itemRecipe.AddIngredient(5008, 1);
    itemRecipe.AddIngredient(3000, 1);
    itemRecipe.AddIngredient(398, 1);
    itemRecipe.AddIngredient(1430, 1);
    itemRecipe.AddIngredient(1120, 1);
    itemRecipe.AddIngredient(221, 1);
    itemRecipe.AddIngredient(206, 1);
    itemRecipe.AddIngredient(207, 1);
    itemRecipe.AddIngredient(1128, 1);
    itemRecipe.AddRecipeGroup("QoLCompendium:AnyTombstone", 1);
    itemRecipe.Register();
  }
}
