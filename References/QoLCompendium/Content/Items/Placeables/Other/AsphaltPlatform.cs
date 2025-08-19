// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Placeables.Other.AsphaltPlatform
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Tiles.Other;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Placeables.Other;

public class AsphaltPlatform : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 200;

  public virtual void SetDefaults()
  {
    this.Item.DefaultToPlaceableTile(ModContent.TileType<AsphaltPlatformTile>(), 0);
    this.Item.SetShopValues((ItemRarityColor) 0, Item.buyPrice(0, 0, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe1 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform), this.Type, 2, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe1.AddIngredient(775, 1);
    itemRecipe1.Register();
    Recipe itemRecipe2 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform), 775, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe2.AddIngredient(ModContent.ItemType<AsphaltPlatform>(), 2);
    itemRecipe2.Register();
  }
}
