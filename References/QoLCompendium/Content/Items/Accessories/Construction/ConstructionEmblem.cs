// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.Construction.ConstructionEmblem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Accessories.Construction;

public class ConstructionEmblem : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 14;
    ((Entity) this.Item).height = 14;
    this.Item.maxStack = 1;
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 1, 0, 0));
    this.Item.accessory = true;
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual)
  {
    player.tileSpeed += 0.15f;
    player.wallSpeed += 0.15f;
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(2, 50);
    itemRecipe.AddIngredient(3, 50);
    itemRecipe.AddIngredient(133, 50);
    itemRecipe.AddIngredient(169, 50);
    itemRecipe.AddIngredient(593, 50);
    itemRecipe.AddTile(283);
    itemRecipe.Register();
  }
}
