// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Dedicated.Flute
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

public class Flute : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.DedicatedItems;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(4425);
    this.Item.shoot = ModContent.ProjectileType<Snake>();
    this.Item.buffType = ModContent.BuffType<SnakeBuff>();
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
    return !player.HasBuff(ModContent.BuffType<SnakeBuff>()) && base.CanUseItem(player);
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Jabon"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.Orange, Color.Yellow, 3f))
    };
    tooltips.Add(tooltipLine);
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.DedicatedItems);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.DedicatedItems), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(2504, 16 /*0x10*/);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
