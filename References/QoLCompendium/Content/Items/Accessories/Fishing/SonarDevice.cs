// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.Fishing.SonarDevice
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
namespace QoLCompendium.Content.Items.Accessories.Fishing;

public class SonarDevice : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.FishingAccessories;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 19;
    ((Entity) this.Item).height = 15;
    this.Item.maxStack = 1;
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 1, 0, 0));
    this.Item.accessory = true;
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual) => player.sonarPotion = true;

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.FishingAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.FishingAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
    itemRecipe.AddIngredient(2355, 5);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
