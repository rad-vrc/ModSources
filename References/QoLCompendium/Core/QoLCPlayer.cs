// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.QoLCPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Humanizer;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Items.Accessories.Fishing;
using QoLCompendium.Content.Items.Tools.Fishing;
using QoLCompendium.Content.Items.Tools.Usables;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

#nullable disable
namespace QoLCompendium.Core;

public class QoLCPlayer : ModPlayer
{
  public bool sunPedestal;
  public bool moonPedestal;
  public bool bloodMoonPedestal;
  public bool eclipsePedestal;
  public bool pausePedestal;
  public bool increasedSpawns;
  public bool decreasedSpawns;
  public bool noSpawns;
  public int selectedSpawnModifier;
  public int spawnRate;
  public int spawnRateUpdateTimer;
  private static FieldInfo spawnRateFieldInfo;
  public int bossToSpawn;
  public bool bossSpawn;
  public int eventToSpawn;
  public bool eventSpawn;
  public int flaskEffectMode;
  public int thoriumCoatingMode;
  public bool sillySlapper;
  public bool warpMirror;
  public bool HasGoldenLockpick;
  public List<int> activeItems = new List<int>();
  public List<int> activeBuffItems = new List<int>();
  public List<int> activeBuffs = new List<int>();
  public int selectedBiome;

  public virtual void Load()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_ItemSlot.RightClick_ItemArray_int_int += QoLCPlayer.\u003C\u003EO.\u003C0\u003E__ItemSlot_RightClick ?? (QoLCPlayer.\u003C\u003EO.\u003C0\u003E__ItemSlot_RightClick = new On_ItemSlot.hook_RightClick_ItemArray_int_int((object) null, __methodptr(ItemSlot_RightClick)));
  }

  public virtual void Unload()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    On_ItemSlot.RightClick_ItemArray_int_int -= QoLCPlayer.\u003C\u003EO.\u003C0\u003E__ItemSlot_RightClick ?? (QoLCPlayer.\u003C\u003EO.\u003C0\u003E__ItemSlot_RightClick = new On_ItemSlot.hook_RightClick_ItemArray_int_int((object) null, __methodptr(ItemSlot_RightClick)));
  }

  public virtual void ResetEffects() => this.Reset();

  public virtual void UpdateDead() => this.Reset();

  public virtual void SaveData(TagCompound tag)
  {
    tag.Add("flaskEffectMode", (object) this.flaskEffectMode);
    tag.Add("thoriumCoatingMode", (object) this.thoriumCoatingMode);
    tag.Add("SelectedBiome", (object) this.selectedBiome);
    tag.Add("SelectedSpawnModifier", (object) this.selectedSpawnModifier);
    tag.Add("bossToSpawn", (object) this.bossToSpawn);
    tag.Add("bossSpawn", (object) this.bossSpawn);
    tag.Add("eventToSpawn", (object) this.eventToSpawn);
    tag.Add("eventSpawn", (object) this.eventSpawn);
  }

  public virtual void LoadData(TagCompound tag)
  {
    this.flaskEffectMode = tag.GetInt("flaskEffectMode");
    this.thoriumCoatingMode = tag.GetInt("thoriumCoatingMode");
    this.selectedBiome = tag.GetInt("SelectedBiome");
    this.selectedSpawnModifier = tag.GetInt("SelectedSpawnModifier");
    this.bossToSpawn = tag.GetInt("bossToSpawn");
    this.bossSpawn = tag.GetBool("bossSpawn");
    this.eventToSpawn = tag.GetInt("eventToSpawn");
    this.eventSpawn = tag.GetBool("eventSpawn");
  }

  public virtual void PreUpdate()
  {
    if (this.spawnRateUpdateTimer <= 0)
      return;
    --this.spawnRateUpdateTimer;
  }

  public virtual void PostUpdate()
  {
    if (!ModConditions.reforgedLoaded || ModAccessorySlot.Player.equippedWings.social)
      return;
    ModConditions.reforgedMod.Call(new object[3]
    {
      (object) "PostUpdateModPlayer",
      (object) ((Entity) Main.LocalPlayer).whoAmI,
      (object) ModAccessorySlot.Player.equippedWings
    });
  }

  public virtual void PostUpdateMiscEffects()
  {
    if (this.spawnRateUpdateTimer <= 0)
    {
      this.spawnRateUpdateTimer = 60;
      QoLCPlayer.spawnRateFieldInfo = typeof (NPC).GetField("spawnRate", (BindingFlags) 40);
      this.spawnRate = (int) QoLCPlayer.spawnRateFieldInfo.GetValue((object) null);
    }
    if (((Entity) this.Player).whoAmI != Main.myPlayer || !Main.mapFullscreen || !Main.mouseLeft || !Main.mouseLeftRelease || !this.warpMirror)
      return;
    PlayerInput.SetZoom_Unscaled();
    float num1 = 16f / Main.mapFullscreenScale;
    float num2 = (float) ((double) Main.mapFullscreenPos.X * 16.0 - 10.0);
    double num3 = (double) Main.mapFullscreenPos.Y * 16.0 - 21.0;
    float num4 = (float) (Main.mouseX - Main.screenWidth / 2);
    float num5 = (float) (Main.mouseY - Main.screenHeight / 2);
    float num6 = num2 + num4 * num1;
    double num7 = (double) num5 * (double) num1;
    float num8 = (float) (num3 + num7);
    for (int index = 0; index < Main.npc.Length; ++index)
    {
      NPC npc = Main.npc[index];
      if (((Entity) npc).active && npc.townNPC)
      {
        float num9 = ((Entity) npc).position.X - 14f * num1;
        float num10 = ((Entity) npc).position.Y - 14f * num1;
        float num11 = ((Entity) npc).position.X + 14f * num1;
        float num12 = ((Entity) npc).position.Y + 14f * num1;
        if ((double) num6 >= (double) num9 && (double) num6 <= (double) num11 && (double) num8 >= (double) num10 && (double) num8 <= (double) num12)
        {
          Main.mouseLeftRelease = false;
          Main.mapFullscreen = false;
          this.Player.Teleport(Vector2.op_Addition(((Entity) npc).position, new Vector2(0.0f, -6f)), 0, 0);
          PlayerInput.SetZoom_Unscaled();
          break;
        }
      }
    }
  }

  public virtual void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
  {
    if (!QoLCompendium.QoLCompendium.itemConfig.FishingAccessories)
      return;
    if (this.Player.anglerQuestsFinished == 1)
    {
      rewardItems.Add(new Item(ModContent.ItemType<AnglerRadar>(), 1, 0)
      {
        stack = 1
      });
    }
    else
    {
      if (this.Player.anglerQuestsFinished < 1 || !Utils.NextBool(Main.rand, 10))
        return;
      rewardItems.Add(new Item(ModContent.ItemType<AnglerRadar>(), 1, 0)
      {
        stack = 1
      });
    }
  }

  public virtual void CatchFish(
    FishingAttempt attempt,
    ref int itemDrop,
    ref int npcSpawn,
    ref AdvancedPopupRequest sonar,
    ref Vector2 sonarPosition)
  {
    if ((attempt.inLava ? 0 : (!attempt.inHoney ? 1 : 0)) == 0 || !Main.bloodMoon || !attempt.crate || !QoLCompendium.QoLCompendium.itemConfig.BottomlessBuckets || attempt.uncommon || attempt.rare || !attempt.veryrare && !attempt.legendary || !Utils.NextBool(Main.rand))
      return;
    itemDrop = ModContent.ItemType<BottomlessChumBucket>();
  }

  public virtual void OnHurt(Player.HurtInfo info)
  {
    if (!this.sillySlapper)
      return;
    this.Player.KillMe(PlayerDeathReason.ByCustomReason(NetworkText.FromKey(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.Messages.SillySlapper"), new object[1]
    {
      (object) this.Player.name
    }), Array.Empty<object>())), (double) int.MaxValue, 0, false);
  }

  public virtual IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
  {
    // ISSUE: object of a compiler-generated type is created
    return QoLCompendium.QoLCompendium.itemConfig.StarterBag ? (IEnumerable<Item>) new \u003C\u003Ez__ReadOnlySingleElementList<Item>(new Item(ModContent.ItemType<StarterBag>(), 1, 0)) : (IEnumerable<Item>) Array.Empty<Item>();
  }

  private static void ItemSlot_RightClick(
    On_ItemSlot.orig_RightClick_ItemArray_int_int orig,
    Item[] inv,
    int context,
    int slot)
  {
    if (Main.mouseRight && Main.mouseRightRelease)
    {
      Player localPlayer = Main.LocalPlayer;
      QoLCPlayer qPlayer;
      localPlayer.TryGetModPlayer<QoLCPlayer>(ref qPlayer);
      if (Main.mouseItem.ModItem is Common.IRightClickOverrideWhenHeld modItem && modItem.RightClickOverrideWhileHeld(ref Main.mouseItem, inv, context, slot, localPlayer, qPlayer) || context == 0 && GoldenLockpick.UseKey(inv, slot, localPlayer, qPlayer))
        return;
    }
    orig.Invoke(inv, context, slot);
  }

  public void Reset()
  {
    this.sunPedestal = false;
    this.moonPedestal = false;
    this.bloodMoonPedestal = false;
    this.eclipsePedestal = false;
    this.pausePedestal = false;
    this.increasedSpawns = false;
    this.decreasedSpawns = false;
    this.noSpawns = false;
    this.sillySlapper = false;
    this.warpMirror = false;
    this.HasGoldenLockpick = false;
    this.activeItems.Clear();
    this.activeBuffItems.Clear();
    this.activeBuffs.Clear();
    Common.Reset();
    if (Main.netMode == 2 || Main.player[Main.myPlayer].talkNPC != -1)
      return;
    Mod mod;
    if (Terraria.ModLoader.ModLoader.TryGetMod("terraguardians", ref mod))
    {
      if (!(bool) mod.Call(new object[2]
      {
        (object) "IsPC",
        (object) Main.LocalPlayer
      }))
        return;
    }
    BlackMarketDealerNPCUI.visible = false;
    EtherealCollectorNPCUI.visible = false;
  }
}
