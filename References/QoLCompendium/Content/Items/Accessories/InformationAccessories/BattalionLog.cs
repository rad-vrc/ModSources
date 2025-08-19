// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.InformationAccessories.BattalionLog
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.UI.Other;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Accessories.InformationAccessories;

public class BattalionLog : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.InformationAccessories;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 16 /*0x10*/;
    ((Entity) this.Item).height = 15;
    this.Item.maxStack = 1;
    this.Item.accessory = true;
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 3, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.InformationAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.InformationAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(225, 8);
    itemRecipe.AddIngredient(9, 10);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
    itemRecipe.AddTile(86);
    itemRecipe.Register();
  }

  public virtual void UpdateInfoAccessory(Player player)
  {
    player.GetModPlayer<InfoPlayer>().battalionLog = true;
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual)
  {
    player.GetModPlayer<InfoPlayer>().battalionLog = true;
  }
}
