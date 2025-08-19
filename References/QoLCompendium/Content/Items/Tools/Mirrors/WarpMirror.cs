// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Mirrors.WarpMirror
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Mirrors;

public class WarpMirror : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.Mirrors;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(50);
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 2, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.Mirrors);
  }

  public virtual void UpdateInventory(Player player)
  {
    player.GetModPlayer<QoLCPlayer>().warpMirror = true;
  }

  public virtual bool CanUseItem(Player player) => false;

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.Mirrors), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(170, 10);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 8);
    itemRecipe.AddIngredient(73, 3);
    itemRecipe.AddTile(17);
    itemRecipe.Register();
  }
}
