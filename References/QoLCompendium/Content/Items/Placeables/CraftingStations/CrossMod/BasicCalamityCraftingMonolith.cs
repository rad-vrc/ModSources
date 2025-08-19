// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Placeables.CraftingStations.CrossMod.BasicCalamityCraftingMonolith
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Tiles.CraftingStations.CrossMod;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Placeables.CraftingStations.CrossMod;

[JITWhenModsEnabled(new string[] {"CalamityMod"})]
[ExtendsFromMod(new string[] {"CalamityMod"})]
public class BasicCalamityCraftingMonolith : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.CraftingStations;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.DefaultToPlaceableTile(ModContent.TileType<BasicCalamityMonolithTile>(), 0);
    this.Item.SetShopValues((ItemRarityColor) 3, Item.buyPrice(0, 5, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.CraftingStations);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.CraftingStations), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "WulfrumLabstationItem"), 1);
    itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "EutrophicShelf"), 1);
    itemRecipe.AddIngredient(Common.GetModItem(ModConditions.calamityMod, "StaticRefiner"), 1);
    itemRecipe.Register();
  }
}
