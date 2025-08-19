// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Mirrors.MosaicMirror
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.UI.Other;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Mirrors;

public class MosaicMirror : ModItem
{
  public int Mode;

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.Mirrors;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.CloneDefaults(50);
    this.Item.SetShopValues((ItemRarityColor) 7, Item.buyPrice(0, 10, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.Mirrors);
    if (QoLCompendium.QoLCompendium.itemConfig.InformationAccessories)
      return;
    TooltipLine tooltipLine = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Tooltip1"));
    tooltipLine.Text = $"{tooltipLine.Text} {Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Disabled")}";
    tooltipLine.OverrideColor = new Color?(Color.Red);
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual void SaveData(TagCompound tag) => tag["MosaicMirrorMode"] = (object) this.Mode;

  public virtual void LoadData(TagCompound tag) => this.Mode = tag.GetInt("MosaicMirrorMode");

  public virtual void UpdateInventory(Player player)
  {
    player.GetModPlayer<QoLCPlayer>().warpMirror = true;
    if (this.Mode == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.CursedMirror"));
    if (this.Mode == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.MirrorOfReturn"));
    if (this.Mode != 2)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MosaicMirror.TeleportationMirror"));
  }

  public virtual void UseStyle(Player player, Rectangle heldItemFrame)
  {
    if (this.Mode == 0)
    {
      if (Utils.NextBool(Main.rand))
        Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, Color.Yellow, 1.1f);
      if (player.itemTime == 0)
      {
        player.ApplyItemTime(this.Item, 1f, new bool?());
        return;
      }
      if (player.itemTime == player.itemTimeMax / 2)
      {
        for (int index = 0; index < 70; ++index)
          Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, ((Entity) player).velocity.X * 0.5f, ((Entity) player).velocity.Y * 0.5f, 150, new Color(), 1.5f);
        player.grappling[0] = -1;
        player.grapCount = 0;
        for (int index = 0; index < 1000; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].owner == ((Entity) player).whoAmI && Main.projectile[index].aiStyle == 7)
            Main.projectile[index].Kill();
        }
        if ((double) player.lastDeathPostion.X != 0.0 && (double) player.lastDeathPostion.Y != 0.0)
        {
          Vector2 vector2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2).\u002Ector(player.lastDeathPostion.X - 16f, player.lastDeathPostion.Y - 24f);
          player.Teleport(vector2, 0, 0);
        }
        else if (player == Main.player[Main.myPlayer])
          Main.NewText("No sign of recent death appears in the mirror", byte.MaxValue, byte.MaxValue, byte.MaxValue);
        for (int index = 0; index < 70; ++index)
          Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, new Color(), 1.5f);
      }
    }
    if (this.Mode == 1)
    {
      if (Utils.NextBool(Main.rand))
      {
        Dust dust = Dust.NewDustDirect(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, Color.Cyan, 1.1f);
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
      }
      if (player.ItemTimeIsZero)
        player.ApplyItemTime(this.Item, 1f, new bool?());
      if (player.itemTime == player.itemTimeMax / 2)
      {
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
    }
    if (this.Mode != 2)
      return;
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
    player.TeleportationPotion();
    for (int index = 0; index < 70; ++index)
    {
      Dust dust = Dust.NewDustDirect(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, Color.Cyan, 1.5f);
      dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
    }
  }

  public virtual bool CanRightClick() => true;

  public virtual void RightClick(Player player)
  {
    ++this.Mode;
    if (this.Mode <= 2)
      return;
    this.Mode = 0;
  }

  public virtual void UpdateInfoAccessory(Player player)
  {
    if (!QoLCompendium.QoLCompendium.itemConfig.InformationAccessories)
      return;
    player.GetModPlayer<InfoPlayer>().battalionLog = true;
    player.GetModPlayer<InfoPlayer>().harmInducer = true;
    player.GetModPlayer<InfoPlayer>().headCounter = true;
    player.GetModPlayer<InfoPlayer>().kettlebell = true;
    player.GetModPlayer<InfoPlayer>().luckyDie = true;
    player.GetModPlayer<InfoPlayer>().metallicClover = true;
    player.GetModPlayer<InfoPlayer>().plateCracker = true;
    player.GetModPlayer<InfoPlayer>().regenerator = true;
    player.GetModPlayer<InfoPlayer>().reinforcedPanel = true;
    player.GetModPlayer<InfoPlayer>().replenisher = true;
    player.GetModPlayer<InfoPlayer>().trackingDevice = true;
    player.GetModPlayer<InfoPlayer>().wingTimer = true;
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.Mirrors), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<CursedMirror>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<MirrorOfReturn>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<TeleportationMirror>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<WarpMirror>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<WormholeMirror>(), 1);
    itemRecipe.AddTile(114);
    itemRecipe.Register();
  }
}
