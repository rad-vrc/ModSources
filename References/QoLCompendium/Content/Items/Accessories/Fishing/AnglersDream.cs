// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.Fishing.AnglersDream
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.UI.Other;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Accessories.Fishing;

public class AnglersDream : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.FishingAccessories;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 14;
    ((Entity) this.Item).height = 17;
    this.Item.maxStack = 1;
    this.Item.SetShopValues((ItemRarityColor) 8, Item.buyPrice(0, 8, 0, 0));
    this.Item.accessory = true;
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual)
  {
    player.accFishingLine = true;
    player.accTackleBox = true;
    player.fishingSkill += 10;
    player.accLavaFishing = true;
    player.accFishingBobber = true;
    player.sonarPotion = true;
    player.GetModPlayer<InfoPlayer>().anglerRadar = true;
  }

  public virtual void UpdateInfoAccessory(Player player)
  {
    player.GetModPlayer<InfoPlayer>().anglerRadar = true;
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.FishingAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.FishingAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(5064, 1);
    itemRecipe.AddRecipeGroup("QoLCompendium:FishingBobbers", 1);
    itemRecipe.AddIngredient(ModContent.ItemType<SonarDevice>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<AnglerRadar>(), 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
