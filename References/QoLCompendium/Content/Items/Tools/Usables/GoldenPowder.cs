// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.GoldenPowder
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class GoldenPowder : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.GoldenPowder;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 99;

  public virtual void SetDefaults()
  {
    this.Item.consumable = true;
    this.Item.maxStack = Item.CommonMaxStack;
    this.Item.useAnimation = 15;
    this.Item.useTime = 15;
    this.Item.shootSpeed = 4f;
    this.Item.shoot = ModContent.ProjectileType<GoldenPowderProjectile>();
    this.Item.useStyle = 1;
    ((Entity) this.Item).width = 11;
    ((Entity) this.Item).height = 26;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.noMelee = true;
    this.Item.useTurn = true;
    this.Item.autoReuse = true;
    this.Item.SetShopValues((ItemRarityColor) 0, Item.sellPrice(0, 0, 10, 0));
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.GoldenPowder), this.Type, 10, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(66, 10);
    itemRecipe.AddIngredient(73, 1);
    itemRecipe.AddTile(13);
    itemRecipe.Register();
  }
}
