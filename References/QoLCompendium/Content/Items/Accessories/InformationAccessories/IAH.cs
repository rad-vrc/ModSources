// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.InformationAccessories.IAH
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
namespace QoLCompendium.Content.Items.Accessories.InformationAccessories;

public class IAH : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.InformationAccessories;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 17;
    ((Entity) this.Item).height = 14;
    this.Item.maxStack = 1;
    this.Item.accessory = true;
    this.Item.SetShopValues((ItemRarityColor) 5, Item.buyPrice(0, 9, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.InformationAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.InformationAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<Fitbit>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<HeartbeatSensor>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<ToleranceDetector>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<VitalDisplay>(), 1);
    itemRecipe.AddTile(114);
    itemRecipe.Register();
  }

  public virtual void UpdateInfoAccessory(Player player)
  {
    player.GetModPlayer<InfoPlayer>().battalionLog = true;
    player.GetModPlayer<InfoPlayer>().harmInducer = true;
    player.GetModPlayer<InfoPlayer>().headCounter = true;
    player.GetModPlayer<InfoPlayer>().kettlebell = true;
    player.GetModPlayer<InfoPlayer>().luckyDie = true;
    player.GetModPlayer<InfoPlayer>().metallicClover = true;
    player.GetModPlayer<InfoPlayer>().plateCracker = true;
    player.GetModPlayer<InfoPlayer>().regenerator = true;
    player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
    player.GetModPlayer<InfoPlayer>().replenisher = true;
    player.GetModPlayer<InfoPlayer>().trackingDevice = true;
    player.GetModPlayer<InfoPlayer>().wingTimer = true;
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual)
  {
    player.GetModPlayer<InfoPlayer>().battalionLog = true;
    player.GetModPlayer<InfoPlayer>().harmInducer = true;
    player.GetModPlayer<InfoPlayer>().headCounter = true;
    player.GetModPlayer<InfoPlayer>().kettlebell = true;
    player.GetModPlayer<InfoPlayer>().luckyDie = true;
    player.GetModPlayer<InfoPlayer>().metallicClover = true;
    player.GetModPlayer<InfoPlayer>().plateCracker = true;
    player.GetModPlayer<InfoPlayer>().regenerator = true;
    player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
    player.GetModPlayer<InfoPlayer>().replenisher = true;
    player.GetModPlayer<InfoPlayer>().trackingDevice = true;
    player.GetModPlayer<InfoPlayer>().wingTimer = true;
  }
}
