// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.BuffSystem
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

public class BuffSystem : ModSystem
{
  internal static Dictionary<int, int> ModdedPlaceableItemBuffs = new Dictionary<int, int>();

  public static void DoBuffIntegration()
  {
    BuffSystem.AddBuffIntegration(ModConditions.afkpetsMod, "ChaosAmplifier", "ChaosAmplifier");
    BuffSystem.AddBuffIntegration(ModConditions.afkpetsMod, "PortableRocketLauncher", "Dispenser");
    BuffSystem.AddVanillaBuffIntegration(ModConditions.afkpetsMod, "DruidicArtifact", 165);
    BuffSystem.AddBuffIntegration(ModConditions.afkpetsMod, "EchoFlower", "EchoFlower");
    BuffSystem.AddBuffIntegration(ModConditions.afkpetsMod, "FallenSoulContainer", "FallenSoul");
    BuffSystem.AddBuffIntegration(ModConditions.afkpetsMod, "SacrificialAltar", "SacrificialAltar");
    BuffSystem.AddBuffIntegration(ModConditions.blocksThrowerMod, "ThrowingBoard", "DeadlyPrecision");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "WeightlessCandle", "BlueCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "VigorousCandle", "PinkCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "SpitefulCandle", "YellowCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "ResilientCandle", "PurpleCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "ChaosCandle", "ChaosCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "TranquilityCandle", "TranquilityCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "EffigyOfDecay", "EffigyOfDecayBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "CrimsonEffigy", "CrimsonEffigyBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityMod, "CorruptionEffigy", "CorruptionEffigyBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityCommunityRemixMod, "AstralEffigy", "AstralEffigyBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityCommunityRemixMod, "HallowEffigy", "HallowEffigyBuff");
    BuffSystem.AddBuffIntegration(ModConditions.calamityEntropyMod, "VoidCandle", "VoidCandleBuff");
    BuffSystem.AddBuffIntegration(ModConditions.captureDiscsClassMod, "DiscCharger", "ChargedDisc");
    BuffSystem.AddBuffIntegration(ModConditions.clickerClassMod, "DesktopComputer", "DesktopComputerBuff");
    BuffSystem.AddBuffIntegration(ModConditions.ruptureMod, "TrinketRack", "SleightOfHand");
    BuffSystem.AddBuffIntegration(ModConditions.fargosMutantMod, "Semistation", "Semistation");
    BuffSystem.AddBuffIntegration(ModConditions.fargosMutantMod, "Omnistation", "Omnistation");
    BuffSystem.AddBuffIntegration(ModConditions.fargosMutantMod, "Omnistation2", "Omnistation");
    BuffSystem.AddBuffIntegration(ModConditions.homewardJourneyMod, "BushOfLife", "BushOfLifeBuff");
    BuffSystem.AddBuffIntegration(ModConditions.homewardJourneyMod, "LifeLantern", "LifeLanternBuff");
    BuffSystem.AddBuffIntegration(ModConditions.martainsOrderMod, "ArcheologyTable", "ReschBuff");
    BuffSystem.AddBuffIntegration(ModConditions.martainsOrderMod, "SporeFarm", "SporeSave");
    BuffSystem.AddBuffIntegration(ModConditions.redemptionMod, "EnergyStation", "EnergyStationBuff");
    BuffSystem.AddBuffIntegration(ModConditions.secretsOfTheShadowsMod, "DigitalDisplay", "CyberneticEnhancements");
    BuffSystem.AddBuffIntegration(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent", "DEFEBuff");
    BuffSystem.AddBuffIntegration(ModConditions.shadowsOfAbaddonMod, "FruitLantern", "FruitBuff");
    BuffSystem.AddBuffIntegration(ModConditions.spiritMod, "SunPot", "SunPotBuff");
    BuffSystem.AddBuffIntegration(ModConditions.spiritMod, "CoilEnergizerItem", "OverDrive");
    BuffSystem.AddBuffIntegration(ModConditions.spiritMod, "TheCouch", "CouchPotato");
    BuffSystem.AddBuffIntegration(ModConditions.spiritMod, "KoiTotem", "KoiTotemBuff");
    BuffSystem.AddBuffIntegration(ModConditions.thoriumMod, "Altar", "AltarBuff");
    BuffSystem.AddBuffIntegration(ModConditions.thoriumMod, "ConductorsStand", "ConductorsStandBuff");
    BuffSystem.AddBuffIntegration(ModConditions.thoriumMod, "Mistletoe", "MistletoeBuff");
    BuffSystem.AddBuffIntegration(ModConditions.thoriumMod, "NinjaRack", "NinjaBuff");
  }

  public static void AddBuffIntegration(Mod mod, string itemName, string buffName)
  {
    BuffSystem.ModdedPlaceableItemBuffs[Common.GetModItem(mod, itemName)] = Common.GetModBuff(mod, buffName);
  }

  public static void AddVanillaBuffIntegration(Mod mod, string itemName, int buffID)
  {
    BuffSystem.ModdedPlaceableItemBuffs[Common.GetModItem(mod, itemName)] = buffID;
  }
}
