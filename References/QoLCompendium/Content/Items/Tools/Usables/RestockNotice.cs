// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.RestockNotice
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class RestockNotice : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.RestockNotice;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 18;
    ((Entity) this.Item).height = 15;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.Item4);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 0, 50, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.RestockNotice);
  }

  public virtual bool? UseItem(Player player)
  {
    if (Main.netMode == 0)
      Chest.SetupTravelShop();
    if (Main.netMode == 1)
    {
      Chest.SetupTravelShop();
      NetMessage.SendTravelShop(Main.myPlayer);
    }
    if (Main.netMode == 2)
    {
      Chest.SetupTravelShop();
      NetMessage.SendTravelShop(Main.myPlayer);
    }
    return new bool?(true);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.RestockNotice), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(225, 12);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
