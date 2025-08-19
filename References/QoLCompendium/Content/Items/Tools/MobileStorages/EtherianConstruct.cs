// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.MobileStorages.EtherianConstruct
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Projectiles.MobileStorages;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.MobileStorages;

public class EtherianConstruct : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.MobileStorages;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(3213);
    this.Item.shoot = ModContent.ProjectileType<EtherianConstructProjectile>();
    this.Item.SetShopValues((ItemRarityColor) 3, Item.buyPrice(0, 2, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.MobileStorages);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.MobileStorages), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(3813, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
