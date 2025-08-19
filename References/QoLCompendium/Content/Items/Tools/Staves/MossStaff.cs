// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Staves.MossStaff
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

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
namespace QoLCompendium.Content.Items.Tools.Staves;

public class MossStaff : ModItem
{
  public int Mode;

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    this.Item.useStyle = 1;
    this.Item.useTurn = true;
    this.Item.useAnimation = 25;
    this.Item.useTime = 13;
    this.Item.autoReuse = true;
    ((Entity) this.Item).width = 24;
    ((Entity) this.Item).height = 28;
    this.Item.damage = 14;
    this.Item.createTile = 182;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.knockBack = 3f;
    this.Item.SetShopValues((ItemRarityColor) 6, Item.sellPrice(0, 0, 50, 0));
    this.Item.DamageType = DamageClass.Melee;
  }

  public virtual void SaveData(TagCompound tag) => tag["MossStaffMode"] = (object) this.Mode;

  public virtual void LoadData(TagCompound tag) => this.Mode = tag.GetInt("MossStaffMode");

  public virtual bool CanRightClick() => true;

  public virtual void HoldItem(Player player)
  {
    if (this.Mode == 0)
    {
      this.Item.createTile = 182;
      Tile tile = ((Tilemap) ref Main.tile)[Main.mouseX, Main.mouseY];
      if (((Tile) ref tile).TileType == (ushort) 38)
        this.Item.createTile = 515;
    }
    if (this.Mode == 1)
    {
      this.Item.createTile = 180;
      Tile tile = ((Tilemap) ref Main.tile)[Main.mouseX, Main.mouseY];
      if (((Tile) ref tile).TileType == (ushort) 38)
        this.Item.createTile = 513;
    }
    if (this.Mode == 2)
    {
      this.Item.createTile = 179;
      Tile tile = ((Tilemap) ref Main.tile)[Main.mouseX, Main.mouseY];
      if (((Tile) ref tile).TileType == (ushort) 38)
        this.Item.createTile = 512 /*0x0200*/;
    }
    if (this.Mode == 3)
    {
      this.Item.createTile = 183;
      Tile tile = ((Tilemap) ref Main.tile)[Main.mouseX, Main.mouseY];
      if (((Tile) ref tile).TileType == (ushort) 38)
        this.Item.createTile = 516;
    }
    if (this.Mode != 4)
      return;
    this.Item.createTile = 181;
    Tile tile1 = ((Tilemap) ref Main.tile)[Main.mouseX, Main.mouseY];
    if (((Tile) ref tile1).TileType != (ushort) 38)
      return;
    this.Item.createTile = 514;
  }

  public virtual void UpdateInventory(Player player)
  {
    if (this.Mode == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.BlueMoss"));
    if (this.Mode == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.BrownMoss"));
    if (this.Mode == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.GreenMoss"));
    if (this.Mode == 3)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.PurpleMoss"));
    if (this.Mode != 4)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.MossStaff.RedMoss"));
  }

  public virtual void RightClick(Player player)
  {
    ++this.Mode;
    if (this.Mode <= 4)
      return;
    this.Mode = 0;
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Placeable"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "MossStaffPlaceable", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.MossStaffPlaceable"));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1), tooltipLine2);
    tooltips.RemoveAll((Predicate<TooltipLine>) (x => x.Name == "Placeable" && x.Mod == "Terraria"));
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(3, 12);
    itemRecipe.AddIngredient(129, 12);
    itemRecipe.AddIngredient(4352, 5);
    itemRecipe.AddIngredient(4350, 5);
    itemRecipe.AddIngredient(4349, 5);
    itemRecipe.AddIngredient(4353, 5);
    itemRecipe.AddIngredient(4351, 5);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
