// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.WatchingEye
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

public class WatchingEye : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.WatchingEye;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 9;
    ((Entity) this.Item).height = 14;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.Item4);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 0, 80 /*0x50*/, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.WatchingEye);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.WatchingEye), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(38, 4);
    itemRecipe.AddIngredient(179, 2);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual bool? UseItem(Player player)
  {
    for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
    {
      for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
      {
        if (WorldGen.InWorld(index1, index2, 0))
          Main.Map.Update(index1, index2, byte.MaxValue);
      }
    }
    Main.refreshMap = true;
    return new bool?(true);
  }
}
