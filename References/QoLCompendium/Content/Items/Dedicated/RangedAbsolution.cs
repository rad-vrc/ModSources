// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Dedicated.RangedAbsolution
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Dedicated;

public class RangedAbsolution : ModItem
{
  public int shootType;

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.DedicatedItems;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 26;
    ((Entity) this.Item).height = 26;
    this.Item.useTime = 12;
    this.Item.useAnimation = 12;
    this.Item.useStyle = 1;
    this.Item.UseSound = new SoundStyle?(SoundID.Item11);
    this.Item.autoReuse = true;
    this.Item.damage = 26;
    this.Item.DamageType = DamageClass.Ranged;
    this.Item.knockBack = 7f;
    this.Item.shoot = 10;
    this.Item.useAmmo = AmmoID.Bullet;
    this.Item.shootSpeed = 16f;
    this.Item.SetShopValues((ItemRarityColor) 10, Item.buyPrice(0, 4, 0, 0));
  }

  public virtual bool RangedPrefix() => true;

  public virtual bool Shoot(
    Player player,
    EntitySource_ItemUse_WithAmmo source,
    Vector2 position,
    Vector2 velocity,
    int type,
    int damage,
    float knockback)
  {
    this.shootType = type;
    return false;
  }

  public virtual void MeleeEffects(Player player, Rectangle hitbox)
  {
    if (player.itemAnimation != (int) ((double) player.itemAnimationMax * 0.1) && player.itemAnimation != (int) ((double) player.itemAnimationMax * 0.3) && player.itemAnimation != (int) ((double) player.itemAnimationMax * 0.5) && player.itemAnimation != (int) ((double) player.itemAnimationMax * 0.7) && player.itemAnimation != (int) ((double) player.itemAnimationMax * 0.9))
      return;
    float num1 = 0.0f;
    float num2 = 0.0f;
    float num3 = 0.0f;
    float num4 = 0.0f;
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.9))
    {
      num1 = -7f;
      if (((Entity) player).direction == -1)
        num4 -= 8f;
    }
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.7))
    {
      num1 = -6f;
      num2 = 2f;
      if (((Entity) player).direction == -1)
        num4 -= 6f;
    }
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.5))
    {
      num1 = -4f;
      num2 = 4f;
    }
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.3))
    {
      num1 = -2f;
      num2 = 6f;
    }
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.1))
      num2 = 7f;
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.7))
      num4 = 26f;
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.3))
    {
      num4 -= 4f;
      num3 -= 20f;
    }
    if (player.itemAnimation == (int) ((double) player.itemAnimationMax * 0.1))
      num3 += 6f;
    float num5 = num1 * 1.5f;
    float num6 = num2 * 1.5f;
    float num7 = num4 * (float) ((Entity) player).direction;
    float num8 = num3 * player.gravDir;
    Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), (float) (hitbox.X + hitbox.Width / 2) + num7, (float) (hitbox.Y + hitbox.Height / 2) + num8, (float) ((Entity) player).direction * num6, num5 * player.gravDir, this.shootType, this.Item.damage, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Nobisyu"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
    };
    tooltips.Add(tooltipLine);
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.DedicatedItems);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.DedicatedItems), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(1257, 10);
    itemRecipe.AddIngredient(173, 5);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
