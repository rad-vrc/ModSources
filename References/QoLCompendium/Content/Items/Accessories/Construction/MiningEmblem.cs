// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.Construction.MiningEmblem
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

public class MiningEmblem : ModItem
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

  public virtual void UpdateAccessory(Player player, bool hideVisual) => player.pickSpeed -= 0.15f;

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(20, 5);
    itemRecipe.AddIngredient(703, 5);
    itemRecipe.AddIngredient(22, 5);
    itemRecipe.AddIngredient(704, 5);
    itemRecipe.AddIngredient(21, 5);
    itemRecipe.AddIngredient(705, 5);
    itemRecipe.AddIngredient(19, 5);
    itemRecipe.AddIngredient(706, 5);
    itemRecipe.AddTile(283);
    itemRecipe.Register();
  }
}
