// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.FavoriteEffect.DestinationGlobe
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.FavoriteEffect;

public class DestinationGlobe : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.DestinationGlobe;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 20;
    ((Entity) this.Item).height = 26;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 2, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.DestinationGlobe);
  }

  public virtual bool? UseItem(Player player)
  {
    if (!DestinationGlobeUI.visible)
      DestinationGlobeUI.timeStart = Main.GameUpdateCount;
    DestinationGlobeUI.visible = true;
    return base.UseItem(player);
  }

  public virtual bool CanUseItem(Player player) => !DestinationGlobeUI.visible;

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.DestinationGlobe), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(170, 15);
    itemRecipe.AddIngredient(2, 5);
    itemRecipe.AddIngredient(62, 5);
    itemRecipe.AddIngredient(206, 1);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual void UpdateInventory(Player player)
  {
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.NoModifier"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Desert"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Snow"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 3)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Jungle"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 4)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.GlowingMushroom"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 5)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Corruption"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 6)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Crimson"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 7 && Main.hardMode)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Hallow"));
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 8)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.DestinationGlobe.Purity"));
    if (!this.Item.favorited)
      return;
    player.GetModPlayer<QoLCPlayer>().activeItems.Add(this.Item.type);
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 1)
      player.ZoneDesert = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 2)
      player.ZoneSnow = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 3)
      player.ZoneJungle = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 4)
      player.ZoneGlowshroom = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 5)
      player.ZoneCorrupt = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 6)
      player.ZoneCrimson = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome == 7 && Main.hardMode)
      player.ZoneHallow = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedBiome != 8)
      return;
    player.ZonePurity = true;
  }
}
