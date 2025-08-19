// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Explosives.AutoHouser
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Content.Tiles.AutoStructures;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Explosives;

public class AutoHouser : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.AutoStructures;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 17;
    ((Entity) this.Item).height = 13;
    this.Item.maxStack = 1;
    this.Item.useStyle = 1;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.consumable = false;
    this.Item.createTile = ModContent.TileType<AutoHouserTile>();
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 0, 50, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.AutoStructures);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.AutoStructures), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(9, 100);
    itemRecipe.AddIngredient(8, 1);
    itemRecipe.AddTile(18);
    itemRecipe.Register();
  }

  public virtual void HoldItem(Player player) => this.HandleShadow(player);

  public void HandleShadow(Player player)
  {
    if (player.ownedProjectileCounts[ModContent.ProjectileType<BuildIndicatorProjectile>()] > 50)
      return;
    if (((Entity) player).direction < 0)
    {
      for (int index1 = -9; index1 <= 0; ++index1)
      {
        for (int index2 = -5; index2 <= 0; ++index2)
        {
          Vector2 mouseWorld = Main.MouseWorld;
          mouseWorld.X += (float) (index1 * 16 /*0x10*/);
          mouseWorld.Y += (float) (index2 * 16 /*0x10*/);
          Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), Vector2.op_Addition(mouseWorld, new Vector2(0.0f, 16f)), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
        }
      }
    }
    else
    {
      for (int index3 = 0; index3 <= 9; ++index3)
      {
        for (int index4 = -5; index4 <= 0; ++index4)
        {
          Vector2 mouseWorld = Main.MouseWorld;
          mouseWorld.X += (float) (index3 * 16 /*0x10*/);
          mouseWorld.Y += (float) (index4 * 16 /*0x10*/);
          Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), Vector2.op_Addition(mouseWorld, new Vector2(0.0f, 16f)), Vector2.Zero, ModContent.ProjectileType<BuildIndicatorProjectile>(), 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
        }
      }
    }
  }
}
