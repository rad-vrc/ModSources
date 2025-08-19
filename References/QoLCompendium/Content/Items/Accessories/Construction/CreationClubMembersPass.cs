// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Accessories.Construction.CreationClubMembersPass
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Accessories.Construction;

public class CreationClubMembersPass : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 21;
    ((Entity) this.Item).height = 13;
    this.Item.maxStack = 1;
    this.Item.SetShopValues((ItemRarityColor) 7, Item.buyPrice(0, 10, 0, 0));
    this.Item.accessory = true;
  }

  public virtual void UpdateAccessory(Player player, bool hideVisual)
  {
    player.tileSpeed += 0.25f;
    player.wallSpeed += 0.25f;
    player.pickSpeed -= 0.25f;
    player.equippedAnyTileSpeedAcc = true;
    player.equippedAnyTileRangeAcc = true;
    player.autoPaint = true;
    player.equippedAnyWallSpeedAcc = true;
    player.chiselSpeed = true;
    player.treasureMagnet = true;
    ((PortableStoolUsage) ref player.portableStoolInfo).SetStats(26, 26, 26);
    player.autoActuator = true;
    ++player.blockRange;
    ++Player.tileRangeX;
    ++Player.tileRangeY;
    player.CanSeeInvisibleBlocks = true;
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.ConstructionAccessories), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<MiningEmblem>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<ConstructionEmblem>(), 1);
    itemRecipe.AddIngredient(5126, 1);
    itemRecipe.AddIngredient(407, 1);
    itemRecipe.AddIngredient(1923, 1);
    itemRecipe.AddIngredient(3624, 1);
    itemRecipe.AddIngredient(4409, 1);
    itemRecipe.AddTile(114);
    itemRecipe.Register();
  }
}
