// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.FavoriteEffect.MoonPedestal
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
namespace QoLCompendium.Content.Items.Tools.FavoriteEffect;

public class MoonPedestal : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.MoonPedestals;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 10;
    ((Entity) this.Item).height = 18;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 0, 90, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.MoonPedestals);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.MoonPedestals), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(129, 10);
    itemRecipe.AddIngredient(3, 8);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual void UpdateInventory(Player player)
  {
    if (!this.Item.favorited)
      return;
    player.GetModPlayer<QoLCPlayer>().activeItems.Add(this.Item.type);
    player.GetModPlayer<QoLCPlayer>().moonPedestal = true;
  }
}
