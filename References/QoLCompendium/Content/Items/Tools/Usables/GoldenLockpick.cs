// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.GoldenLockpick
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
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

public class GoldenLockpick : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.GoldenLockpick;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(327);
    this.Item.maxStack = 1;
    this.Item.SetShopValues((ItemRarityColor) 0, Item.buyPrice(0, 1, 75, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.GoldenLockpick);
  }

  public virtual void UpdateInventory(Player player)
  {
    player.GetModPlayer<QoLCPlayer>().HasGoldenLockpick = true;
  }

  public static bool UseKey(Item[] inv, int slot, Player player, QoLCPlayer qPlayer)
  {
    if (inv[slot].type != 3085 || !qPlayer.HasGoldenLockpick)
      return false;
    if (ItemLoader.ConsumeItem(inv[slot], player))
      --inv[slot].stack;
    if (inv[slot].stack < 0)
      inv[slot].SetDefaults(0);
    SoundEngine.PlaySound(ref SoundID.Unlock, new Vector2?(), (SoundUpdateCallback) null);
    Main.stackSplit = 30;
    Main.mouseRightRelease = false;
    player.OpenLockBox(inv[slot].type);
    Recipe.FindRecipes(false);
    return true;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.GoldenLockpick), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(327, 1);
    itemRecipe.AddIngredient(154, 25);
    itemRecipe.AddTile(283);
    itemRecipe.Register();
  }
}
