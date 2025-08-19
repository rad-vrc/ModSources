// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Explosives.Minibridge
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Explosives;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Explosives;

public class Minibridge : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.AutoStructures;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 25;
    ((Entity) this.Item).height = 17;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.useStyle = 1;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.noUseGraphic = true;
    this.Item.noMelee = true;
    this.Item.shoot = ModContent.ProjectileType<MinibridgeProj>();
    this.Item.shootSpeed = 5f;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 5, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.AutoStructures);
  }

  public virtual bool Shoot(
    Player player,
    EntitySource_ItemUse_WithAmmo source,
    Vector2 position,
    Vector2 velocity,
    int type,
    int damage,
    float knockback)
  {
    Vector2 mouseWorld = Main.MouseWorld;
    Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), mouseWorld, Vector2.Zero, type, 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    return false;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.AutoStructures), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(9, 10);
    itemRecipe.AddIngredient(167, 1);
    itemRecipe.AddIngredient(182, 2);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual void HoldItem(Player player) => this.HandleShadow(player);

  public void HandleShadow(Player player)
  {
    if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 200)
      return;
    for (int index = -100; index <= 100; ++index)
    {
      Vector2 mouseWorld = Main.MouseWorld;
      mouseWorld.X += (float) (index * 16 /*0x10*/);
      Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), mouseWorld, Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
