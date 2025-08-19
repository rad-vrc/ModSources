// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.FavoriteEffect.Paperweight
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
namespace QoLCompendium.Content.Items.Tools.FavoriteEffect;

public class Paperweight : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.Paperweight;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 11;
    ((Entity) this.Item).height = 8;
    this.Item.maxStack = 1;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 1, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.Paperweight);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.Paperweight), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 6);
    itemRecipe.AddIngredient(170, 5);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual void UpdateInventory(Player player)
  {
    if (!this.Item.favorited)
      return;
    player.GetModPlayer<QoLCPlayer>().activeItems.Add(this.Item.type);
    if (Main.playerInventory)
      return;
    player.controlUseItem = true;
  }
}
