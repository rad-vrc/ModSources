// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Placeables.CraftingStations.DemonAltar
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Tiles.CraftingStations;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Placeables.CraftingStations;

public class DemonAltar : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.CraftingStations;
  }

  public virtual void SetStaticDefaults()
  {
    ItemID.Sets.ShimmerTransformToItem[this.Item.type] = ModContent.ItemType<AetherAltar>();
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    this.Item.DefaultToPlaceableTile(ModContent.TileType<DemonAltarTile>(), 0);
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 1, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.CraftingStations);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.CraftingStations), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(57, 5);
    itemRecipe.AddIngredient(61, 12);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
