// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Summons.WallOfFleshSummon
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Summons;

public class WallOfFleshSummon : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.BossSummons;
  }

  public virtual void SetStaticDefaults()
  {
    ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 6;
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 11;
    ((Entity) this.Item).height = 14;
    this.Item.maxStack = Item.CommonMaxStack;
    this.Item.useAnimation = 30;
    this.Item.useTime = 30;
    this.Item.useStyle = 4;
    this.Item.consumable = true;
    this.Item.SetShopValues((ItemRarityColor) 3, Item.sellPrice(0, 0, 0, 0));
  }

  public virtual bool CanUseItem(Player player) => !NPC.AnyNPCs(113);

  public virtual bool? UseItem(Player player)
  {
    NPC.SpawnWOF(((Entity) player).Center);
    SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
    return new bool?(true);
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.BossSummons);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.BossSummons), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(5277, 1);
    itemRecipe.AddIngredient(154, 5);
    itemRecipe.AddTile(26);
    itemRecipe.Register();
  }
}
