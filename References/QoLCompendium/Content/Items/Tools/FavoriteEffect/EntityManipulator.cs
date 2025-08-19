// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.FavoriteEffect.EntityManipulator
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
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.FavoriteEffect;

public class EntityManipulator : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.EntityManipulator;
  }

  public virtual void SetStaticDefaults() => this.Item.ResearchUnlockCount = 1;

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 16 /*0x10*/;
    ((Entity) this.Item).height = 16 /*0x10*/;
    this.Item.maxStack = 1;
    this.Item.consumable = false;
    this.Item.useStyle = 4;
    this.Item.UseSound = new SoundStyle?(SoundID.MenuOpen);
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.SetShopValues((ItemRarityColor) 2, Item.buyPrice(0, 5, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.EntityManipulator);
  }

  public virtual bool? UseItem(Player player)
  {
    if (!EntityManipulatorUI.visible)
      EntityManipulatorUI.timeStart = Main.GameUpdateCount;
    EntityManipulatorUI.visible = true;
    return base.UseItem(player);
  }

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.EntityManipulator), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddIngredient(300, 10);
    itemRecipe.AddIngredient(148, 3);
    itemRecipe.AddIngredient(2324, 10);
    itemRecipe.AddIngredient(3117, 3);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }

  public virtual void UpdateInventory(Player player)
  {
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 0)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.NoModifier"));
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 1)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.SpawnIncrease"));
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 2)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.SpawnDecrease"));
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 3)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.CanceledSpawns"));
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 4)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.CanceledEvents"));
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 5)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.EntityManipulator.CanceledSpawnsAndEvents"));
    if (!this.Item.favorited)
      return;
    player.GetModPlayer<QoLCPlayer>().activeItems.Add(this.Item.type);
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 1)
      player.GetModPlayer<QoLCPlayer>().increasedSpawns = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 2)
      player.GetModPlayer<QoLCPlayer>().decreasedSpawns = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 3)
      player.GetModPlayer<QoLCPlayer>().noSpawns = true;
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier == 4)
    {
      if (Main.invasionType != 0)
        Main.invasionType = 0;
      if (Main.pumpkinMoon)
        Main.pumpkinMoon = false;
      if (Main.snowMoon)
        Main.snowMoon = false;
      if (Main.eclipse)
        Main.eclipse = false;
      if (Main.bloodMoon)
        Main.bloodMoon = false;
      if (Main.WindyEnoughForKiteDrops)
      {
        Main.windSpeedTarget = 0.0f;
        Main.windSpeedCurrent = 0.0f;
      }
      if (Main.slimeRain)
      {
        Main.StopSlimeRain(true);
        Main.slimeWarningDelay = 1;
        Main.slimeWarningTime = 1;
      }
      if (BirthdayParty.PartyIsUp)
        BirthdayParty.CheckNight();
      if (DD2Event.Ongoing && Main.netMode != 1)
        DD2Event.StopInvasion(false);
      if (Sandstorm.Happening)
      {
        Sandstorm.Happening = false;
        Sandstorm.TimeLeft = 0.0;
        Sandstorm.IntendedSeverity = 0.0f;
      }
      if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
      {
        NPC.LunarApocalypseIsUp = false;
        NPC.ShieldStrengthTowerNebula = 0;
        NPC.ShieldStrengthTowerSolar = 0;
        NPC.ShieldStrengthTowerStardust = 0;
        NPC.ShieldStrengthTowerVortex = 0;
        for (int index = 0; index < 200; ++index)
        {
          if (((Entity) Main.npc[index]).active && (Main.npc[index].type == 507 || Main.npc[index].type == 517 || Main.npc[index].type == 493 || Main.npc[index].type == 422))
          {
            Main.npc[index].dontTakeDamage = false;
            Main.npc[index].StrikeInstantKill();
          }
        }
      }
      if (Main.IsItRaining || Main.IsItStorming)
      {
        Main.StopRain();
        Main.cloudAlpha = 0.0f;
        if (Main.netMode == 2)
          Main.SyncRain();
      }
    }
    if (player.GetModPlayer<QoLCPlayer>().selectedSpawnModifier != 5)
      return;
    player.GetModPlayer<QoLCPlayer>().noSpawns = true;
    if (Main.invasionType != 0)
      Main.invasionType = 0;
    if (Main.pumpkinMoon)
      Main.pumpkinMoon = false;
    if (Main.snowMoon)
      Main.snowMoon = false;
    if (Main.eclipse)
      Main.eclipse = false;
    if (Main.bloodMoon)
      Main.bloodMoon = false;
    if (Main.WindyEnoughForKiteDrops)
    {
      Main.windSpeedTarget = 0.0f;
      Main.windSpeedCurrent = 0.0f;
    }
    if (Main.slimeRain)
    {
      Main.StopSlimeRain(true);
      Main.slimeWarningDelay = 1;
      Main.slimeWarningTime = 1;
    }
    if (BirthdayParty.PartyIsUp)
      BirthdayParty.CheckNight();
    if (DD2Event.Ongoing && Main.netMode != 1)
      DD2Event.StopInvasion(false);
    if (Sandstorm.Happening)
    {
      Sandstorm.Happening = false;
      Sandstorm.TimeLeft = 0.0;
      Sandstorm.IntendedSeverity = 0.0f;
    }
    if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
    {
      NPC.LunarApocalypseIsUp = false;
      NPC.ShieldStrengthTowerNebula = 0;
      NPC.ShieldStrengthTowerSolar = 0;
      NPC.ShieldStrengthTowerStardust = 0;
      NPC.ShieldStrengthTowerVortex = 0;
      for (int index = 0; index < 200; ++index)
      {
        if (((Entity) Main.npc[index]).active && (Main.npc[index].type == 507 || Main.npc[index].type == 517 || Main.npc[index].type == 493 || Main.npc[index].type == 422))
        {
          Main.npc[index].dontTakeDamage = false;
          Main.npc[index].StrikeInstantKill();
        }
      }
    }
    if (!Main.IsItRaining && !Main.IsItStorming)
      return;
    Main.StopRain();
    Main.cloudAlpha = 0.0f;
    if (Main.netMode != 2)
      return;
    Main.SyncRain();
  }
}
