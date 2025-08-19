// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Fishing.LegendaryCatcher
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Fishing;
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
namespace QoLCompendium.Content.Items.Tools.Fishing;

public class LegendaryCatcher : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.LegendaryCatcher;
  }

  public virtual void SetStaticDefaults()
  {
    ItemID.Sets.CanFishInLava[this.Item.type] = true;
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    this.Item.useStyle = 1;
    this.Item.useAnimation = 8;
    this.Item.useTime = 8;
    ((Entity) this.Item).width = 24;
    ((Entity) this.Item).height = 15;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.fishingPole = 200;
    this.Item.shootSpeed = 15f;
    this.Item.shoot = ModContent.ProjectileType<LegendaryBobber>();
    this.Item.SetShopValues((ItemRarityColor) 10, Item.buyPrice(0, 10, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.LegendaryCatcher);
  }

  public virtual void HoldItem(Player player)
  {
    player.accFishingLine = true;
    if (player.ZoneRain)
      player.fishingSkill += 100;
    if (Main.dayTime)
      return;
    player.fishingSkill += 50;
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
    int num1 = 50;
    if (player.ZoneRain)
      num1 += 20;
    if (!Main.dayTime)
      num1 += 10;
    float num2 = 75f;
    for (int index = 0; index < num1; ++index)
    {
      float num3 = velocity.X + Utils.NextFloat(Main.rand, 0.0f - num2, num2) * 0.05f;
      float num4 = velocity.Y + Utils.NextFloat(Main.rand, 0.0f - num2, num2) * 0.05f;
      Projectile.NewProjectile((IEntitySource) source, position.X, position.Y, num3, num4, type, 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }
    return false;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.LegendaryCatcher), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
    itemRecipe.AddIngredient(999, 6);
    itemRecipe.AddIngredient(74, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
