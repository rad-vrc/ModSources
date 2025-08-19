// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.BottomlessLiquidBucket
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
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class BottomlessLiquidBucket : ModItem
{
  public int Mode;

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.BottomlessBuckets;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.useStyle = 1;
    this.Item.useTurn = true;
    this.Item.useAnimation = 12;
    this.Item.useTime = 12;
    this.Item.autoReuse = true;
    ((Entity) this.Item).width = 15;
    ((Entity) this.Item).height = 14;
    this.Item.SetShopValues((ItemRarityColor) 8, Item.sellPrice(0, 20, 0, 0));
  }

  public virtual void SaveData(TagCompound tag)
  {
    tag["BottomlessLiquidBucketMode"] = (object) this.Mode;
  }

  public virtual void LoadData(TagCompound tag)
  {
    this.Mode = tag.GetInt("BottomlessLiquidBucketMode");
  }

  public virtual bool CanRightClick() => true;

  public virtual void RightClick(Player player)
  {
    ++this.Mode;
    if (this.Mode <= 3)
      return;
    this.Mode = 0;
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual bool? UseItem(Player player)
  {
    if (this.Mode == 0)
      BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Water);
    if (this.Mode == 1)
      BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Lava);
    if (this.Mode == 2)
      BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Honey);
    if (this.Mode == 3)
      BottomlessLiquidBucket.UseBucket(player, BottomlessLiquidBucket.LiquidTypes.Shimmer);
    return new bool?(true);
  }

  public virtual void UpdateInventory(Player player)
  {
    if (this.Mode == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Water"));
    if (this.Mode == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Lava"));
    if (this.Mode == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Honey"));
    if (this.Mode != 3)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.BottomlessLiquidBucket.Shimmer"));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.BottomlessBuckets);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.BottomlessBuckets), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(3031, 1);
    itemRecipe.AddIngredient(4820, 1);
    itemRecipe.AddIngredient(5302, 1);
    itemRecipe.AddIngredient(5364, 1);
    itemRecipe.AddTile(283);
    itemRecipe.Register();
  }

  internal static bool PlaceLiquid(int x, int y, BottomlessLiquidBucket.LiquidTypes type)
  {
    Tile tileSafely = Framing.GetTileSafely(x, y);
    if (((Tile) ref tileSafely).LiquidAmount >= (byte) 230 || ((Tile) ref tileSafely).HasUnactuatedTile && Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType] || ((Tile) ref tileSafely).LiquidAmount != (byte) 0 && (BottomlessLiquidBucket.LiquidTypes) ((Tile) ref tileSafely).LiquidType != type)
      return false;
    ((Tile) ref tileSafely).LiquidType = (int) type;
    ((Tile) ref tileSafely).LiquidAmount = byte.MaxValue;
    WorldGen.SquareTileFrame(x, y, true);
    if (Main.netMode != 0)
      NetMessage.sendWater(x, y);
    return true;
  }

  internal static bool UseBucket(Player player, BottomlessLiquidBucket.LiquidTypes type)
  {
    if (((Entity) player).whoAmI == Main.myPlayer && !player.noBuilding && BottomlessLiquidBucket.PlaceLiquid(Player.tileTargetX, Player.tileTargetY, type))
      SoundEngine.PlaySound(ref SoundID.Item19, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
    return true;
  }

  internal enum LiquidTypes : byte
  {
    Water,
    Lava,
    Honey,
    Shimmer,
  }
}
