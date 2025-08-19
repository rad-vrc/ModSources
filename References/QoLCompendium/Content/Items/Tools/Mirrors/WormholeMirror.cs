// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Mirrors.WormholeMirror
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
namespace QoLCompendium.Content.Items.Tools.Mirrors;

public class WormholeMirror : ModItem
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

  public virtual bool CanUseItem(Player player) => false;

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Player.HasUnityPotion += WormholeMirror.\u003C\u003EO.\u003C0\u003E__Player_HasUnityPotion ?? (WormholeMirror.\u003C\u003EO.\u003C0\u003E__Player_HasUnityPotion = new On_Player.hook_HasUnityPotion((object) null, __methodptr(Player_HasUnityPotion)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_Player.TakeUnityPotion += WormholeMirror.\u003C\u003EO.\u003C1\u003E__Player_TakeUnityPotion ?? (WormholeMirror.\u003C\u003EO.\u003C1\u003E__Player_TakeUnityPotion = new On_Player.hook_TakeUnityPotion((object) null, __methodptr(Player_TakeUnityPotion)));
  }

  private static void Player_TakeUnityPotion(On_Player.orig_TakeUnityPotion orig, Player self)
  {
    if (self.HasItem(ModContent.ItemType<WormholeMirror>()) || self.HasItem(ModContent.ItemType<MosaicMirror>()))
      return;
    orig.Invoke(self);
  }

  private static bool Player_HasUnityPotion(On_Player.orig_HasUnityPotion orig, Player self)
  {
    return self.HasItem(ModContent.ItemType<WormholeMirror>()) || self.HasItem(ModContent.ItemType<MosaicMirror>()) || orig.Invoke(self);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.Mirrors), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(170, 10);
    itemRecipe.AddRecipeGroup("QoLCompendium:GoldBars", 8);
    itemRecipe.AddIngredient(2997, 3);
    itemRecipe.AddTile(17);
    itemRecipe.Register();
  }
}
