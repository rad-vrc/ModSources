// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Weapons.Ammo.Other.CoinBag
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
namespace QoLCompendium.Content.Items.Weapons.Ammo.Other;

public abstract class CoinBag : BaseAmmo
{
  public override void SetDefaults()
  {
    this.Item.notAmmo = false;
    this.Item.useStyle = 0;
    this.Item.useTime = 0;
    this.Item.useAnimation = 0;
    this.Item.createTile = -1;
    this.Item.shoot = 0;
  }

  private class EndlessCopperCoinPouch : CoinBag
  {
    public override int AmmunitionItem => 71;
  }

  private class EndlessSilverCoinPouch : CoinBag
  {
    public override int AmmunitionItem => 72;
  }

  private class EndlessGoldCoinPouch : CoinBag
  {
    public override int AmmunitionItem => 73;
  }

  private class EndlessPlatinumCoinPouch : CoinBag
  {
    public override int AmmunitionItem => 74;
  }

  public class EndlessCandyCornPie : BaseAmmo
  {
    public override int AmmunitionItem => 1783;
  }

  public class EndlessExplosiveJackOLantern : BaseAmmo
  {
    public override int AmmunitionItem => 1785;
  }

  public class EndlessGelTank : ModItem
  {
    public virtual bool IsLoadingEnabled(Mod mod)
    {
      return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo;
    }

    public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

    public virtual void SetDefaults()
    {
      this.Item.ammo = AmmoID.Gel;
      this.Item.knockBack = 0.5f;
      this.Item.DamageType = DamageClass.Ranged;
      this.Item.SetShopValues((ItemRarityColor) 2, Item.sellPrice(0, 1, 0, 0));
    }

    public virtual void ModifyTooltips(List<TooltipLine> tooltips)
    {
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo);
    }

    public virtual void AddRecipes()
    {
      Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
      itemRecipe.AddIngredient(23, 3996);
      itemRecipe.AddTile(220);
      itemRecipe.Register();
    }
  }

  public class EndlessNailPouch : BaseAmmo
  {
    public override int AmmunitionItem => 3108;
  }

  public class EndlessSandPouch : ModItem
  {
    public virtual bool IsLoadingEnabled(Mod mod)
    {
      return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo;
    }

    public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(169);
      this.Item.consumable = false;
      this.Item.maxStack = 1;
      this.Item.useStyle = 0;
      this.Item.useTime = 0;
      this.Item.useAnimation = 0;
      this.Item.createTile = -1;
      this.Item.shoot = 0;
      this.Item.SetShopValues((ItemRarityColor) 2, Item.sellPrice(0, 1, 0, 0));
    }

    public virtual void ModifyTooltips(List<TooltipLine> tooltips)
    {
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo);
    }

    public virtual bool CanUseItem(Player player) => false;

    public virtual void AddRecipes()
    {
      Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
      itemRecipe.AddIngredient(169, 3996);
      itemRecipe.AddTile(220);
      itemRecipe.Register();
    }
  }

  public class EndlessSnowballPouch : ModItem
  {
    public virtual bool IsLoadingEnabled(Mod mod)
    {
      return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo;
    }

    public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(949);
      this.Item.consumable = false;
      this.Item.maxStack = 1;
      this.Item.shoot = 0;
      this.Item.useAnimation = 0;
      this.Item.useTime = 0;
      this.Item.useStyle = 0;
      this.Item.SetShopValues((ItemRarityColor) 2, Item.sellPrice(0, 1, 0, 0));
    }

    public virtual void ModifyTooltips(List<TooltipLine> tooltips)
    {
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo);
    }

    public virtual bool CanUseItem(Player player) => false;

    public virtual void AddRecipes()
    {
      Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
      itemRecipe.AddIngredient(949, 3996);
      itemRecipe.AddTile(220);
      itemRecipe.Register();
    }
  }

  public class EndlessStakeBundle : BaseAmmo
  {
    public override int AmmunitionItem => 1836;
  }

  public class EndlessStarPouch : ModItem
  {
    public virtual bool IsLoadingEnabled(Mod mod)
    {
      return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo;
    }

    public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

    public virtual void SetDefaults()
    {
      this.Item.ammo = AmmoID.FallenStar;
      this.Item.DamageType = DamageClass.Ranged;
      this.Item.SetShopValues((ItemRarityColor) 2, Item.sellPrice(0, 1, 0, 0));
    }

    public virtual void ModifyTooltips(List<TooltipLine> tooltips)
    {
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo);
    }

    public virtual bool CanUseItem(Player player) => false;

    public virtual void AddRecipes()
    {
      Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.EndlessAmmo), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
      itemRecipe.AddIngredient(75, 3996);
      itemRecipe.AddTile(220);
      itemRecipe.Register();
    }
  }

  public class EndlessStyngerBoltQuiver : BaseAmmo
  {
    public override int AmmunitionItem => 1261;
  }
}
