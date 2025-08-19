// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Humanizer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.TooltipChanges;

public class TooltipChanges : GlobalItem
{
  private static Item _shimmerItemDisplay;
  private static NPC _shimmerNPCDisplay;

  public virtual void SetStaticDefaults()
  {
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges._shimmerItemDisplay = new Item();
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges._shimmerNPCDisplay = new NPC();
  }

  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Favorite"));
    TooltipLine tooltipLine2 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "FavoriteDesc"));
    TooltipLine tooltipLine3 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "OneDropLogo"));
    TooltipLine tooltipLine4 = tooltips.Find((Predicate<TooltipLine>) (l => l.Mod == "AfterYM"));
    tooltips.Remove(tooltipLine4);
    if (QoLCompendium.QoLCompendium.tooltipConfig.NoFavoriteTooltip)
    {
      tooltips.Remove(tooltipLine1);
      tooltips.Remove(tooltipLine2);
    }
    if (QoLCompendium.QoLCompendium.tooltipConfig.ShimmerableTooltip)
      this.ShimmmerableTooltips(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.WorksInBanksTooltip && QoLCompendium.QoLCompendium.mainConfig.UtilityAccessoriesWorkInBanks)
      this.WorksInBankTooltip(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.WingStatsTooltips)
      this.WingStatsTooltip(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.HookStatsTooltips)
      this.HookStatsTooltip(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
      this.UsedPermanentUpgrade(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.AmmoTooltip)
      this.AmmoTooltip(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.ActiveTooltip)
      this.ActiveTooltip(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.NoYoyoTooltip)
      tooltips.Remove(tooltipLine3);
    if (QoLCompendium.QoLCompendium.tooltipConfig.FromModTooltip)
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemModTooltip(item, tooltips);
    if (QoLCompendium.QoLCompendium.tooltipConfig.ClassTagTooltip)
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.ItemClassTooltip(item, tooltips);
    if (!ModConditions.thoriumLoaded || !ModConditions.exhaustionDisablerLoaded)
      return;
    QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges.RemoveExhaustionToolTip(item, tooltips);
  }

  public void ShimmmerableTooltips(Item item, List<TooltipLine> tooltips)
  {
    if (!item.CanShimmer())
      return;
    int num1 = ItemID.Sets.ShimmerCountsAsItem[item.type];
    int index = num1 != -1 ? num1 : item.type;
    int num2 = ItemID.Sets.ShimmerTransformToItem[index];
    int num3 = -1;
    if (index == 4986 && !NPC.unlockedSlimeRainbowSpawn && !QoLCompendium.QoLCompendium.mainConfig.NoTownSlimes)
      num3 = 681;
    else if (item.makeNPC > 0)
    {
      int num4 = NPCID.Sets.ShimmerTransformToNPC[item.makeNPC];
      num3 = num4 != -1 ? num4 : item.makeNPC;
    }
    else if (index == 3461)
    {
      short num5;
      switch ((int) Main.GetMoonPhase())
      {
        case 0:
          num5 = (short) 5408;
          break;
        case 1:
          num5 = (short) 5401;
          break;
        case 2:
          num5 = (short) 5403;
          break;
        case 3:
          num5 = (short) 5402;
          break;
        case 5:
          num5 = (short) 5407;
          break;
        case 6:
          num5 = (short) 5405;
          break;
        case 7:
          num5 = (short) 5404;
          break;
        default:
          num5 = (short) 5406;
          break;
      }
      num2 = (int) num5;
    }
    else if (item.createTile == 139)
      num2 = 576;
    Common.GetTooltipValue("Shimmerable");
    string tooltipValue;
    if (num2 != -1)
    {
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges._shimmerItemDisplay.SetDefaults(num2);
      tooltipValue = Common.GetTooltipValue("ShimmerableIntoItem", (object) num2, (object) QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges._shimmerItemDisplay.Name);
    }
    else if (num3 != -1)
    {
      QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges._shimmerNPCDisplay.SetDefaults(num3, new NPCSpawnParams());
      tooltipValue = Common.GetTooltipValue("ShimmerableIntoNPC", (object) QoLCompendium.Core.Changes.TooltipChanges.TooltipChanges._shimmerNPCDisplay.GivenOrTypeName);
    }
    else
    {
      int num6 = ItemID.Sets.CoinLuckValue[index];
      if (num6 <= 0)
        return;
      tooltipValue = Common.GetTooltipValue("ShimmerCoinLuck", (object) $"+{num6:##,###}");
    }
    TooltipLine tooltip = new TooltipLine(((ModType) this).Mod, "ShimmerInfo", tooltipValue)
    {
      OverrideColor = new Color?(Color.Plum)
    };
    Common.AddLastTooltip(tooltips, tooltip);
  }

  public void WorksInBankTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (!Common.BankItems.Contains(item.type))
      return;
    TooltipLine tooltip = new TooltipLine(((ModType) this).Mod, "WorksInBanks", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.WorksInBanks"))
    {
      OverrideColor = new Color?(Color.Gray)
    };
    Common.AddLastTooltip(tooltips, tooltip);
  }

  public void WingStatsTooltip(Item item, List<TooltipLine> tooltips)
  {
    int wingSlot = item.wingSlot;
    if (wingSlot == -1 || item.social || ModConditions.calamityLoaded && item.type <= (int) ItemID.Count)
      return;
    if (ModConditions.calamityLoaded)
    {
      int capacity = 15;
      List<int> intList = new List<int>(capacity);
      CollectionsMarshal.SetCount<int>(intList, capacity);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList);
      int num1 = 0;
      span[num1] = Common.GetModItem(ModConditions.calamityMod, "AureateBooster");
      int num2 = num1 + 1;
      span[num2] = Common.GetModItem(ModConditions.calamityMod, "DrewsWings");
      int num3 = num2 + 1;
      span[num3] = Common.GetModItem(ModConditions.calamityMod, "ElysianWings");
      int num4 = num3 + 1;
      span[num4] = Common.GetModItem(ModConditions.calamityMod, "ExodusWings");
      int num5 = num4 + 1;
      span[num5] = Common.GetModItem(ModConditions.calamityMod, "HadalMantle");
      int num6 = num5 + 1;
      span[num6] = Common.GetModItem(ModConditions.calamityMod, "HadarianWings");
      int num7 = num6 + 1;
      span[num7] = Common.GetModItem(ModConditions.calamityMod, "MOAB");
      int num8 = num7 + 1;
      span[num8] = Common.GetModItem(ModConditions.calamityMod, "SilvaWings");
      int num9 = num8 + 1;
      span[num9] = Common.GetModItem(ModConditions.calamityMod, "SkylineWings");
      int num10 = num9 + 1;
      span[num10] = Common.GetModItem(ModConditions.calamityMod, "SoulofCryogen");
      int num11 = num10 + 1;
      span[num11] = Common.GetModItem(ModConditions.calamityMod, "StarlightWings");
      int num12 = num11 + 1;
      span[num12] = Common.GetModItem(ModConditions.calamityMod, "TarragonWings");
      int num13 = num12 + 1;
      span[num13] = Common.GetModItem(ModConditions.calamityMod, "TracersCelestial");
      int num14 = num13 + 1;
      span[num14] = Common.GetModItem(ModConditions.calamityMod, "TracersElysian");
      int num15 = num14 + 1;
      span[num15] = Common.GetModItem(ModConditions.calamityMod, "TracersSeraph");
      int num16 = num15 + 1;
      if (intList.Contains(item.type))
        return;
    }
    if (ModConditions.fargosSoulsLoaded && (item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "FlightMasterySoul") || item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "DimensionSoul") || item.type == Common.GetModItem(ModConditions.fargosSoulsMod, "EternitySoul")) || ModConditions.wrathOfTheGodsLoaded && item.type == Common.GetModItem(ModConditions.wrathOfTheGodsMod, "DivineWings"))
      return;
    WingStats stat = ArmorIDs.Wing.Sets.Stats[wingSlot];
    float num = (float) stat.FlyTime / 60f;
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Equipable"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "FlightTime", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.FlightTime", (object) num.ToString("0.##")));
    TooltipLine tooltipLine3 = new TooltipLine(((ModType) this).Mod, "HorizontalSpeed", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HorizontalSpeed", (object) stat.AccRunSpeedOverride.ToString("~0.##")));
    TooltipLine tooltipLine4 = new TooltipLine(((ModType) this).Mod, "VerticalSpeedMul", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.VerticalSpeedMul", (object) stat.AccRunAccelerationMult.ToString("~0.##")));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1) + 1, tooltipLine2);
    tooltips.Insert(tooltips.IndexOf(tooltipLine2) + 1, tooltipLine3);
    tooltips.Insert(tooltips.IndexOf(tooltipLine3) + 1, tooltipLine4);
  }

  public void HookStatsTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (!ModConditions.calamityLoaded)
    {
      if (item.type == 84)
        this.CreateVanillaHookTooltip(18.75f, 11.5f, tooltips);
      if (item.type == 1236)
        this.CreateVanillaHookTooltip(18.75f, 10f, tooltips);
      if (item.type == 4759)
        this.CreateVanillaHookTooltip(19f, 11.5f, tooltips);
      if (item.type == 1237)
        this.CreateVanillaHookTooltip(20.625f, 10.5f, tooltips);
      if (item.type == 1238)
        this.CreateVanillaHookTooltip(22.5f, 11f, tooltips);
      if (item.type == 1239)
        this.CreateVanillaHookTooltip(24.375f, 11.5f, tooltips);
      if (item.type == 1240)
        this.CreateVanillaHookTooltip(26.25f, 12f, tooltips);
      if (item.type == 4257)
        this.CreateVanillaHookTooltip(27.5f, 12.5f, tooltips);
      if (item.type == 1241)
        this.CreateVanillaHookTooltip(29.125f, 12.5f, tooltips);
      if (item.type == 939)
        this.CreateVanillaHookTooltip(22.625f, 10f, tooltips);
      if (item.type == 1273)
        this.CreateVanillaHookTooltip(21.875f, 15f, tooltips);
      if (item.type == 2585)
        this.CreateVanillaHookTooltip(18.75f, 13f, tooltips);
      if (item.type == 2360)
        this.CreateVanillaHookTooltip(25f, 13f, tooltips);
      if (item.type == 185)
        this.CreateVanillaHookTooltip(28f, 13f, tooltips);
      if (item.type == 1800)
        this.CreateVanillaHookTooltip(31.25f, 13.5f, tooltips);
      if (item.type == 1915)
        this.CreateVanillaHookTooltip(25f, 11.5f, tooltips);
      if (item.type == 437)
        this.CreateVanillaHookTooltip(27.5f, 14f, tooltips);
      if (item.type == 4980)
        this.CreateVanillaHookTooltip(30f, 16f, tooltips);
      if (item.type == 3021)
        this.CreateVanillaHookTooltip(30f, 16f, tooltips);
      if (item.type == 3022)
        this.CreateVanillaHookTooltip(30f, 15f, tooltips);
      if (item.type == 3023)
        this.CreateVanillaHookTooltip(30f, 15f, tooltips);
      if (item.type == 3020)
        this.CreateVanillaHookTooltip(30f, 15f, tooltips);
      if (item.type == 2800)
        this.CreateVanillaHookTooltip(31.25f, 14f, tooltips);
      if (item.type == 1829)
        this.CreateVanillaHookTooltip(34.375f, 15.5f, tooltips);
      if (item.type == 1916)
        this.CreateVanillaHookTooltip(34.375f, 15.5f, tooltips);
      if (item.type == 3572)
        this.CreateVanillaHookTooltip(34.375f, 18f, tooltips);
      if (item.type == 3623)
        this.CreateVanillaHookTooltip(37.5f, 16f, tooltips);
    }
    float num1 = 11f;
    if (ModConditions.calamityLoaded && (item.type == Common.GetModItem(ModConditions.calamityMod, "SerpentsBite") || item.type == Common.GetModItem(ModConditions.calamityMod, "BobbitHook")) || item.shoot == 0 || !Main.projHook[item.shoot] || item.type <= (int) ItemID.Count)
      return;
    ProjectileLoader.GetProjectile(item.shoot).GrapplePullSpeed(Main.CurrentPlayer, ref num1);
    float num2 = ProjectileLoader.GetProjectile(item.shoot).GrappleRange() / 16f;
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Equipable"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "HookReach", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookReach", (object) num2));
    TooltipLine tooltipLine3 = new TooltipLine(((ModType) this).Mod, "HookPullSpeed", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookPullSpeed", (object) num1));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1) + 1, tooltipLine2);
    tooltips.Insert(tooltips.IndexOf(tooltipLine2) + 1, tooltipLine3);
  }

  public void CreateVanillaHookTooltip(
    float hookReach,
    float hookSpeed,
    List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Equipable"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "HookReach", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookReach", (object) hookReach));
    TooltipLine tooltipLine3 = new TooltipLine(((ModType) this).Mod, "HookPullSpeed", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HookPullSpeed", (object) hookSpeed));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1) + 1, tooltipLine2);
    tooltips.Insert(tooltips.IndexOf(tooltipLine2) + 1, tooltipLine3);
  }

  public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
  {
    TooltipLine tooltip = new TooltipLine(((ModType) this).Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"))
    {
      OverrideColor = new Color?(Color.LightGreen)
    };
    if (item.type == 29)
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Main.LocalPlayer.ConsumedLifeCrystals, (object) 15);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type == 1291)
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Main.LocalPlayer.ConsumedLifeFruit, (object) 20);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type == 109)
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Main.LocalPlayer.ConsumedManaCrystals, (object) 9);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type == 5337 && Main.LocalPlayer.usedAegisCrystal)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5339 && Main.LocalPlayer.usedArcaneCrystal)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5338 && Main.LocalPlayer.usedAegisFruit)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5342 && Main.LocalPlayer.usedAmbrosia)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5341 && Main.LocalPlayer.usedGummyWorm)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5340 && Main.LocalPlayer.usedGalaxyPearl)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5343 && NPC.peddlersSatchelWasUsed)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5326 && Main.LocalPlayer.ateArtisanBread)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 4382 && NPC.combatBookWasUsed)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5336 && NPC.combatBookVolumeTwoWasUsed)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5043 && Main.LocalPlayer.unlockedBiomeTorches)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == 5289 && Main.LocalPlayer.unlockedSuperCart)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type != 3335 || !Main.LocalPlayer.CanDemonHeartAccessoryBeShown())
      return;
    Common.AddLastTooltip(tooltips, tooltip);
  }

  public void AmmoTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (item.useAmmo == AmmoID.None)
      return;
    Item obj = new Item(item.useAmmo, 1, 0);
    TooltipLine tooltip = new TooltipLine(((ModType) this).Mod, "UseAmmo", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UseAmmo"))
    {
      Text = Common.GetTooltipValue("UseAmmo", (object) item.useAmmo, (object) obj.Name)
    };
    Common.AddLastTooltip(tooltips, tooltip);
  }

  public void ActiveTooltip(Item item, List<TooltipLine> tooltips)
  {
    TooltipLine tooltip1 = new TooltipLine(((ModType) this).Mod, "Active", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Active"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.Lime, Color.YellowGreen, 3f))
    };
    TooltipLine tooltip2 = new TooltipLine(((ModType) this).Mod, "ActiveBuff", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ActiveBuff"))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.Lime, Color.YellowGreen, 3f))
    };
    if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeItems.Contains(item.type))
      Common.AddLastTooltip(tooltips, tooltip1);
    else
      tooltips.Remove(tooltip1);
    if (Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeBuffItems.Contains(item.type))
      Common.AddLastTooltip(tooltips, tooltip2);
    else
      tooltips.Remove(tooltip2);
  }

  public static void ItemDisabledTooltip(Item item, List<TooltipLine> tooltips, bool configOn)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "ItemName"));
    if (configOn)
      return;
    TooltipLine tooltipLine2 = tooltipLine1;
    tooltipLine2.Text = $"{tooltipLine2.Text} {Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ItemDisabled")}";
    tooltipLine1.OverrideColor = new Color?(Color.Red);
  }

  public static void RemoveExhaustionToolTip(Item item, List<TooltipLine> tooltips)
  {
    foreach (TooltipLine tooltip in tooltips)
    {
      if (item.type > (int) ItemID.Count && ((ModType) item.ModItem).Mod == ModConditions.thoriumMod && tooltip.Text == "Overuse of this weapon exhausts you, massively reducing its damage" || tooltip.Text == "Killing enemies recovers some of your exhaustion")
        tooltip.Hide();
    }
  }

  public static void ItemClassTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (item.pick > 0 || item.hammer > 0 || item.axe > 0 || item.damage > 0 && item.createTile > -1 && !item.IsCurrency)
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Tool")));
    if (item.CountsAsClass(DamageClass.Melee) && item.pick <= 0 && item.axe <= 0 && item.hammer <= 0 && item.createTile == -1 && !item.accessory)
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.WarriorClass")));
    if (item.CountsAsClass(DamageClass.Ranged) && !item.IsCurrency && !item.accessory)
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.RangerClass")));
    if (item.CountsAsClass(DamageClass.Magic) && item.type != 167 && item.damage > 0 && !item.accessory)
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.SorcererClass")));
    if (item.CountsAsClass(DamageClass.Summon) && !item.accessory)
    {
      if (!ProjectileID.Sets.IsAWhip[item.shoot])
        tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.SummonerClass")));
      else
        tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.SummonerClassWhip")));
    }
    if (item.CountsAsClass(DamageClass.Throwing) && !item.accessory && !ModConditions.thoriumLoaded && !item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")))
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ThrowerClass")));
    if (item.CountsAsClass(DamageClass.Generic) && !item.accessory)
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.Classless")));
    if (item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")) && ModConditions.calamityLoaded && !item.accessory)
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.RogueClass")));
    if (!item.CountsAsClass(Common.GetModDamageClass(ModConditions.ruptureMod, "DruidDamageClass")) || !ModConditions.ruptureLoaded || item.accessory)
      return;
    tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.DruidClass")));
  }

  public static void ItemModTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (item.ModItem == null)
      return;
    TooltipLine element = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "ItemName"));
    TooltipLine tooltipLine = new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "FromMod", StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.FromMod"), new object[1]
    {
      (object) ((ModType) item.ModItem).Mod.DisplayName
    }))
    {
      OverrideColor = new Color?(Common.ColorSwap(Color.AliceBlue, Color.Azure, 1f))
    };
    tooltips.AddAfter<TooltipLine>(element, tooltipLine);
  }
}
