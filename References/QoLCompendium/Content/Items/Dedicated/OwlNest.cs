// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Dedicated.OwlNest
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Buffs;
using QoLCompendium.Content.Projectiles.Dedicated;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Dedicated;

public class OwlNest : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.DedicatedItems;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(2420);
    this.Item.shoot = ModContent.ProjectileType<Owl>();
    this.Item.buffType = ModContent.BuffType<OwlBuff>();
    this.Item.SetShopValues((ItemRarityColor) 10, Item.buyPrice(0, 5, 0, 0));
  }

  public virtual bool? UseItem(Player player)
  {
    if (((Entity) player).whoAmI == Main.myPlayer)
      player.AddBuff(this.Item.buffType, 3600, true, false);
    return new bool?(true);
  }

  public virtual bool CanUseItem(Player player)
  {
    return !player.HasBuff(ModContent.BuffType<OwlBuff>()) && base.CanUseItem(player);
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Ned"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
    };
    tooltips.Add(tooltipLine);
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.DedicatedItems);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.DedicatedItems), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(9, 12);
    itemRecipe.AddIngredient(150, 7);
    itemRecipe.AddIngredient(210, 2);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
