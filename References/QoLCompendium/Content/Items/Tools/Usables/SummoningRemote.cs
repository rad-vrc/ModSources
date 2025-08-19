// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.Items.Tools.Usables.SummoningRemote
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Humanizer;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.Items.Tools.Usables;

public class SummoningRemote : ModItem
{
  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return !QoLCompendium.QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.QoLCompendium.itemConfig.SummoningRemote;
  }

  public virtual void SetStaticDefaults()
  {
    ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 0;
    this.Item.ResearchUnlockCount = 1;
  }

  public virtual void SetDefaults()
  {
    ((Entity) this.Item).width = 7;
    ((Entity) this.Item).height = 17;
    this.Item.useStyle = 4;
    this.Item.useAnimation = 20;
    this.Item.useTime = 20;
    this.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
    this.Item.SetShopValues((ItemRarityColor) 0, Item.buyPrice(0, 1, 0, 0));
  }

  public virtual void ModifyTooltips(List<TooltipLine> tooltips)
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemDisabledTooltip(this.Item, tooltips, QoLCompendium.QoLCompendium.itemConfig.SummoningRemote);
  }

  public virtual bool Shoot(
    Player player,
    EntitySource_ItemUse_WithAmmo source,
    Vector2 position,
    Vector2 velocity,
    int type,
    int damage,
    float knockback)
  {
    if (player.altFunctionUse == 2)
      return false;
    Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitY, 800f));
    if (player.GetModPlayer<QoLCPlayer>().bossToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
    {
      if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 113)
        return false;
      if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 125)
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, 126f, 0.0f, 0.0f);
      Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, (float) player.GetModPlayer<QoLCPlayer>().bossToSpawn, 0.0f, 0.0f);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
    }
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
    {
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 14)
      {
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, 507f, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 15)
      {
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, 517f, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 16 /*0x10*/)
      {
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, 493f, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 17)
      {
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0.0f, ((Entity) player).whoAmI, 422f, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
    }
    return false;
  }

  public virtual bool? UseItem(Player player)
  {
    if (player.altFunctionUse == 2)
    {
      if (!SummoningRemoteUI.visible)
        SummoningRemoteUI.timeStart = Main.GameUpdateCount;
      SummoningRemoteUI.visible = true;
      SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      return new bool?(true);
    }
    if (player.GetModPlayer<QoLCPlayer>().bossToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().bossSpawn && player.GetModPlayer<QoLCPlayer>().bossToSpawn == 113)
    {
      NPC.SpawnWOF(((Entity) player).Center);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return new bool?(true);
    }
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn != 0 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
    {
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 1)
      {
        LanternNight.GenuineLanterns = false;
        LanternNight.ManualLanterns = false;
        Main.rainTime = (double) (86400 / 24 * 12);
        Main.raining = true;
        Main.maxRaining = Main.cloudAlpha = 0.9f;
        if (Main.netMode == 2)
        {
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          Main.SyncRain();
        }
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventRain"), new Color(50, (int) byte.MaxValue, 130));
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 2)
      {
        Main.windSpeedTarget = Main.windSpeedCurrent = 0.8f;
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventWind"), new Color(50, (int) byte.MaxValue, 130));
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 3)
      {
        Sandstorm.StartSandstorm();
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventSandstorm"), new Color(50, (int) byte.MaxValue, 130));
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 4)
      {
        BirthdayParty.PartyDaysOnCooldown = 0;
        if (Main.netMode != 1)
        {
          for (int index = 0; index < 100 && !BirthdayParty.PartyIsUp; ++index)
            BirthdayParty.CheckMorning();
        }
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 5)
      {
        Main.StartSlimeRain(true);
        Main.slimeWarningDelay = 1;
        Main.slimeWarningTime = 1;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 6 && !Main.dayTime)
      {
        Main.bloodMoon = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventBloodMoon"), new Color(50, (int) byte.MaxValue, 130));
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 7)
      {
        Main.StartInvasion(1);
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 8)
      {
        Main.StartInvasion(2);
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 9)
      {
        Main.StartInvasion(3);
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 10 && Main.dayTime)
      {
        Main.eclipse = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventEclipse"), new Color(50, (int) byte.MaxValue, 130));
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 11 && !Main.dayTime)
      {
        Main.startPumpkinMoon();
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventPumpkinMoon"), new Color(50, (int) byte.MaxValue, 130));
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 12 && !Main.dayTime)
      {
        Main.startSnowMoon();
        TextHelper.PrintText(Language.GetTextValue("Mods.QoLCompendium.Messages.EventFrostMoon"), new Color(50, (int) byte.MaxValue, 130));
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 13)
      {
        Main.StartInvasion(4);
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 18)
      {
        WorldGen.TriggerLunarApocalypse();
        NPC.LunarApocalypseIsUp = true;
      }
    }
    return new bool?(true);
  }

  public virtual void UpdateInventory(Player player)
  {
    if (player.GetModPlayer<QoLCPlayer>().bossToSpawn > 0 && player.GetModPlayer<QoLCPlayer>().bossToSpawn != 125 && player.GetModPlayer<QoLCPlayer>().bossToSpawn != 398 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
      this.Item.SetNameOverride(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.Boss"), new object[1]
      {
        (object) ContentSamples.NpcsByNetId[player.GetModPlayer<QoLCPlayer>().bossToSpawn].FullName
      }));
    if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 125 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.BossTwins"));
    if (player.GetModPlayer<QoLCPlayer>().bossToSpawn == 398 && player.GetModPlayer<QoLCPlayer>().bossSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.BossMoonLord"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 1 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventRain"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 2 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventWind"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 3 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSandstorm"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 4 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventParty"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 5 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSlimeRain"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 6 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventBloodMoon"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 7 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventGoblinArmy"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 8 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSnowLegion"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 9 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventPirateInvasion"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 10 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventEclipse"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 11 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventPumpkinMoon"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 12 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventFrostMoon"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 13 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventMartianMadness"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 14 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventNebulaPillar"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 15 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventSolarPillar"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 16 /*0x10*/ && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventStardustPillar"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 17 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventVortexPillar"));
    if (player.GetModPlayer<QoLCPlayer>().eventToSpawn == 18 && player.GetModPlayer<QoLCPlayer>().eventSpawn)
      this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.EventLunar"));
    if (player.GetModPlayer<QoLCPlayer>().bossSpawn || player.GetModPlayer<QoLCPlayer>().eventSpawn)
      return;
    this.Item.SetNameOverride(Language.GetTextValue("Mods.QoLCompendium.ItemNames.SummoningRemote.NoModifier"));
  }

  public virtual bool AltFunctionUse(Player player) => true;

  public virtual void AddRecipes()
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.itemConfig.SummoningRemote), this.Type, displayText: "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
    itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
    itemRecipe.AddIngredient(178, 5);
    itemRecipe.AddIngredient(38, 2);
    itemRecipe.AddTile(16 /*0x10*/);
    itemRecipe.Register();
  }
}
