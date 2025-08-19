// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Mirrors.MirrorOfReturn
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Mirrors;

public class MirrorOfReturn : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.Mirrors;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(50);
    this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 2, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.Mirrors);
  }

  public virtual void UseStyle(Player player, Rectangle heldItemFrame)
  {
    if (Utils.NextBool(Main.rand))
    {
      Dust dust = Dust.NewDustDirect(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, Color.Cyan, 1.1f);
      dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
    }
    if (player.ItemTimeIsZero)
      player.ApplyItemTime(this.Item, 1f, new bool?());
    if (player.itemTime != player.itemTimeMax / 2)
      return;
    for (int index = 0; index < 70; ++index)
    {
      Dust dust = Dust.NewDustDirect(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, Color.Cyan, 1.5f);
      dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
    }
    player.grappling[0] = -1;
    player.grapCount = 0;
    for (int index = 0; index < Main.projectile.Length; ++index)
    {
      if (((Entity) Main.projectile[index]).active && Main.projectile[index].owner == ((Entity) player).whoAmI && Main.projectile[index].aiStyle == 7)
        Main.projectile[index].Kill();
    }
    player.DoPotionOfReturnTeleportationAndSetTheComebackPoint();
    for (int index = 0; index < 70; ++index)
    {
      Dust dust = Dust.NewDustDirect(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, Color.Cyan, 1.5f);
      dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
    }
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.Mirrors), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(170, 10);
    itemRecipe.AddRecipeGroup("QoLCompendium:GoldBars", 8);
    itemRecipe.AddIngredient(4870, 3);
    itemRecipe.AddTile(17);
    itemRecipe.Register();
  }
}
