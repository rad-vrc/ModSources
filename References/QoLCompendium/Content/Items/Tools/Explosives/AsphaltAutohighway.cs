// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Explosives.AsphaltAutohighway
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

public class AsphaltAutohighway : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    if (!QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems)
      return true;
    return QoLCompendium.QoLCompendium.itemConfig.AutoStructures && QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 37;
    ((Entity) this.Item).height = 26;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.useStyle = 1;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.noUseGraphic = true;
    this.Item.noMelee = true;
    this.Item.shoot = ModContent.ProjectileType<AsphaltAutohighwayProj>();
    this.Item.shootSpeed = 5f;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 5, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.AutoStructures && QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform);
  }

  public virtual bool AltFunctionUse(Player player) => true;

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
    if (player.altFunctionUse == 2)
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), mouseWorld, Vector2.Zero, ModContent.ProjectileType<AsphaltAutohighwaySingleProj>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    else
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), mouseWorld, Vector2.Zero, type, 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    return false;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.AutoStructures && QoLCompendium.QoLCompendium.itemConfig.AsphaltPlatform), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(775, 25);
    itemRecipe.AddIngredient(167, 25);
    itemRecipe.AddIngredient(182, 5);
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual void HoldItem(Player player) => this.HandleShadow(player);

  public void HandleShadow(Player player)
  {
    if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 400)
      return;
    for (int index = -100; index <= 100; ++index)
    {
      Vector2 mouseWorld = Main.MouseWorld;
      mouseWorld.X += (float) (index * 16 /*0x10*/);
      Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), mouseWorld, Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
    for (int index = -100; index <= 100; ++index)
    {
      Vector2 mouseWorld = Main.MouseWorld;
      mouseWorld.X += (float) (index * 16 /*0x10*/);
      Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), Vector2.op_Subtraction(mouseWorld, new Vector2(0.0f, 480f)), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
  }
}
