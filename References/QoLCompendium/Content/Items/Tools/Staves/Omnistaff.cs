// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Staves.Omnistaff
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

public class Omnistaff : ModItem
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
    this.Item.createTile = 633;
    this.Item.UseSound = new SoundStyle?(SoundID.Item1);
    this.Item.knockBack = 3f;
    this.Item.SetShopValues((ItemRarityColor) 6, Item.sellPrice(0, 0, 50, 0));
    this.Item.DamageType = DamageClass.Melee;
  }

  public virtual void SaveData(TagCompound tag) => tag["OmnistaffMode"] = (object) this.Mode;

  public virtual void LoadData(TagCompound tag) => this.Mode = tag.GetInt("OmnistaffMode");

  public virtual bool CanRightClick() => true;

  public virtual void UpdateInventory(Player player)
  {
    if (this.Mode == 0)
    {
      this.Item.createTile = 2;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfRegrowth"));
    }
    if (this.Mode == 1)
    {
      this.Item.createTile = 23;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfCysting"));
    }
    if (this.Mode == 2)
    {
      this.Item.createTile = 199;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfHemorrhaging"));
    }
    if (this.Mode == 3)
    {
      this.Item.createTile = 109;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfHallowing"));
    }
    if (this.Mode == 4)
    {
      this.Item.createTile = 60;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfOvergrowth"));
    }
    if (this.Mode == 5)
    {
      this.Item.createTile = 661;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfOvergrownCysting"));
    }
    if (this.Mode == 6)
    {
      this.Item.createTile = 662;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfOvergrownHemorrhaging"));
    }
    if (this.Mode == 7)
    {
      this.Item.createTile = 70;
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfShrooming"));
    }
    if (this.Mode != 8)
      return;
    this.Item.createTile = 633;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.Omnistaff.StaffOfAshing"));
  }

  public virtual void RightClick(Player player)
  {
    ++this.Mode;
    if (this.Mode <= 8)
      return;
    this.Mode = 0;
  }

  public virtual void OnConsumeItem(Player player) => ++this.Item.stack;

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Placeable"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "OmnistaffEffect", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.OmnistaffPlaceable"));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1), tooltipLine2);
    tooltips.RemoveAll((Predicate<TooltipLine>) (x => x.Name == "Placeable" && x.Mod == "Terraria"));
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.RegrowthStaves), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfAshing>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfCysting>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfHallowing>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfHemorrhaging>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfOvergrownCysting>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfOvergrownHemorrhaging>(), 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfOvergrowth>(), 1);
    itemRecipe.AddIngredient(213, 1);
    itemRecipe.AddIngredient(ModContent.ItemType<StaffOfShrooming>(), 1);
    itemRecipe.AddTile(283);
    itemRecipe.Register();
  }
}
