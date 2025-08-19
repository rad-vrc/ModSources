// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.CalamityFountainChanges
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using CalamityMod;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Furniture.Fountains;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"CalamityMod"})]
[JITWhenModsEnabled(new string[] {"CalamityMod"})]
public class CalamityFountainChanges : ModSystem
{
  private static readonly MethodInfo AstralInfectionActiveMethod = typeof (AstralInfectionBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);
  private static readonly MethodInfo BrimstoneCragsActiveMethod = typeof (BrimstoneCragsBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);
  private static readonly MethodInfo SulphurousSeaActiveMethod = typeof (SulphurousSeaBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);
  private static readonly MethodInfo SunkenSeaActiveMethod = typeof (SunkenSeaBiome).GetMethod("IsBiomeActive", Common.UniversalBindingFlags);

  public virtual void Load()
  {
    Hook hook1 = new Hook((MethodBase) CalamityFountainChanges.AstralInfectionActiveMethod, (Delegate) new Func<CalamityFountainChanges.Orig_AstralInfectionActive, AstralInfectionBiome, Player, bool>(CalamityFountainChanges.AstralInfectionActive_Detour));
    hook1.Apply();
    Common.detours.Add(hook1);
    Hook hook2 = new Hook((MethodBase) CalamityFountainChanges.BrimstoneCragsActiveMethod, (Delegate) new Func<CalamityFountainChanges.Orig_BrimstoneCragsActive, BrimstoneCragsBiome, Player, bool>(CalamityFountainChanges.BrimstoneCragsActive_Detour));
    hook2.Apply();
    Common.detours.Add(hook2);
    Hook hook3 = new Hook((MethodBase) CalamityFountainChanges.SulphurousSeaActiveMethod, (Delegate) new Func<CalamityFountainChanges.Orig_SulphurousSeaActive, SulphurousSeaBiome, Player, bool>(CalamityFountainChanges.SulphurousSeaActive_Detour));
    hook3.Apply();
    Common.detours.Add(hook3);
    Hook hook4 = new Hook((MethodBase) CalamityFountainChanges.SunkenSeaActiveMethod, (Delegate) new Func<CalamityFountainChanges.Orig_SunkenSeaActive, SunkenSeaBiome, Player, bool>(CalamityFountainChanges.SunkenSeaActive_Detour));
    hook4.Apply();
    Common.detours.Add(hook4);
  }

  internal static bool AstralInfectionActive_Detour(
    CalamityFountainChanges.Orig_AstralInfectionActive orig,
    AstralInfectionBiome self,
    Player player)
  {
    bool flag = orig(self, player);
    if (QoLCompendium.QoLCompendium.mainConfig.FountainsCauseBiomes && Main.SceneMetrics.ActiveFountainColor == ModContent.Find<ModWaterStyle>("CalamityMod/AstralWater").Slot)
      return true;
    if (!QoLCompendium.QoLCompendium.mainConfig.FountainsWorkFromInventories || !player.HasItemInAnyInventory(ModContent.ItemType<AstralFountainItem>()))
      return flag;
    Main.SceneMetrics.ActiveFountainColor = ModContent.Find<ModWaterStyle>("CalamityMod/AstralWater").Slot;
    return true;
  }

  internal static bool BrimstoneCragsActive_Detour(
    CalamityFountainChanges.Orig_BrimstoneCragsActive orig,
    BrimstoneCragsBiome self,
    Player player)
  {
    bool flag = orig(self, player);
    if (QoLCompendium.QoLCompendium.mainConfig.FountainsCauseBiomes && CalamityUtils.Calamity(player).BrimstoneLavaFountainCounter > 0)
      return true;
    if (!QoLCompendium.QoLCompendium.mainConfig.FountainsWorkFromInventories || !player.HasItemInAnyInventory(ModContent.ItemType<BrimstoneLavaFountainItem>()))
      return flag;
    CalamityUtils.Calamity(player).BrimstoneLavaFountainCounter = 1;
    return true;
  }

  internal static bool SulphurousSeaActive_Detour(
    CalamityFountainChanges.Orig_SulphurousSeaActive orig,
    SulphurousSeaBiome self,
    Player player)
  {
    bool flag = orig(self, player);
    string str = Main.zenithWorld ? "CalamityMod/PissWater" : "CalamityMod/SulphuricWater";
    if (QoLCompendium.QoLCompendium.mainConfig.FountainsCauseBiomes && Main.SceneMetrics.ActiveFountainColor == ModContent.Find<ModWaterStyle>(str).Slot)
      return true;
    if (!QoLCompendium.QoLCompendium.mainConfig.FountainsWorkFromInventories || !player.HasItemInAnyInventory(ModContent.ItemType<SulphurousFountainItem>()))
      return flag;
    Main.SceneMetrics.ActiveFountainColor = ModContent.Find<ModWaterStyle>(str).Slot;
    return true;
  }

  internal static bool SunkenSeaActive_Detour(
    CalamityFountainChanges.Orig_SunkenSeaActive orig,
    SunkenSeaBiome self,
    Player player)
  {
    bool flag = orig(self, player);
    if (QoLCompendium.QoLCompendium.mainConfig.FountainsCauseBiomes && Main.SceneMetrics.ActiveFountainColor == ModContent.Find<ModWaterStyle>("CalamityMod/SunkenSeaWater").Slot)
      return true;
    if (!QoLCompendium.QoLCompendium.mainConfig.FountainsWorkFromInventories || !player.HasItemInAnyInventory(ModContent.ItemType<SunkenSeaFountain>()))
      return flag;
    Main.SceneMetrics.ActiveFountainColor = ModContent.Find<ModWaterStyle>("CalamityMod/SunkenSeaWater").Slot;
    return true;
  }

  public delegate bool Orig_AstralInfectionActive(AstralInfectionBiome self, Player player);

  public delegate bool Orig_BrimstoneCragsActive(BrimstoneCragsBiome self, Player player);

  public delegate bool Orig_SulphurousSeaActive(SulphurousSeaBiome self, Player player);

  public delegate bool Orig_SunkenSeaActive(SunkenSeaBiome self, Player player);
}
