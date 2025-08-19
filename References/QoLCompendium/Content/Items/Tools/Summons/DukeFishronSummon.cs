// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Summons.DukeFishronSummon
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
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
namespace QoLCompendium.Content.Items.Tools.Summons;

public class DukeFishronSummon : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.BossSummons;
  }

  public virtual void SetStaticDefaults()
  {
    ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 15;
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 14;
    ((Entity) this.Item).height = 15;
    this.Item.maxStack = Item.CommonMaxStack;
    this.Item.useAnimation = 30;
    this.Item.useTime = 30;
    this.Item.useStyle = 4;
    this.Item.consumable = true;
    this.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
    this.Item.SetShopValues((ItemRarityColor) 8, Item.sellPrice(0, 0, 0, 0));
  }

  public virtual bool CanUseItem(Player player) => Main.hardMode && !NPC.AnyNPCs(370);

  public virtual bool Shoot(
    Player player,
    EntitySource_ItemUse_WithAmmo source,
    Vector2 position,
    Vector2 velocity,
    int type,
    int damage,
    float knockback)
  {
    Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitY, 800f));
    Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, 370f, 0.0f, 0.0f);
    SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
    return false;
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.BossSummons);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.BossSummons), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(2673, 1);
    itemRecipe.AddIngredient(356, 1);
    itemRecipe.AddTile(26);
    itemRecipe.Register();
  }
}
