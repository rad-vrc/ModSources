// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.ModConditions
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Humanizer;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace QoLCompendium.Core;

public class ModConditions : ModSystem
{
  public static bool[] ItemHasBeenOwned;
  public static Condition expertOrMaster = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.inExpertOrMaster"), (Func<bool>) (() => Main.expertMode || Main.masterMode));
  public static bool downedDreadnautilus;
  public static Condition DownedDreadnautilus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDreadnautilus"), (Func<bool>) (() => ModConditions.DownedBoss[0]));
  public static bool downedMartianSaucer;
  public static Condition DownedMartianSaucer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMartianSaucer"), (Func<bool>) (() => ModConditions.DownedBoss[1]));
  public static Condition NotDownedMechBossAll = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.notDownedMechBossAll"), (Func<bool>) (() => !NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3));
  public bool waitForBloodMoon;
  public static bool downedBloodMoon;
  public static Condition DownedBloodMoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBloodMoon"), (Func<bool>) (() => ModConditions.downedBloodMoon));
  public bool waitForEclipse;
  public static bool downedEclipse;
  public static Condition DownedEclipse = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEclipse"), (Func<bool>) (() => ModConditions.downedEclipse));
  public static bool downedLunarEvent;
  public static Condition DownedLunarEvent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLunarEvent"), (Func<bool>) (() => ModConditions.downedLunarEvent));
  public static Condition DownedLunarPillarAny = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLunarPillarAny"), (Func<bool>) (() => NPC.downedTowerNebula || NPC.downedTowerSolar || NPC.downedTowerStardust || NPC.downedTowerVortex));
  public bool waitForNight;
  public static bool beenThroughNight;
  public static Condition HasBeenThroughNight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenThroughNight"), (Func<bool>) (() => ModConditions.beenThroughNight));
  public static bool beenToPurity;
  public static Condition HasBeenToPurity = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToPurity"), (Func<bool>) (() => ModConditions.beenToPurity));
  public static bool beenToCavernsOrUnderground;
  public static Condition HasBeenToCavernsOrUnderground = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCavernsOrUnderground"), (Func<bool>) (() => ModConditions.beenToCavernsOrUnderground));
  public static bool beenToUnderworld;
  public static Condition HasBeenToUnderworld = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToUnderworld"), (Func<bool>) (() => ModConditions.beenToUnderworld));
  public static bool beenToSky;
  public static Condition HasBeenToSky = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSky"), (Func<bool>) (() => ModConditions.beenToSky));
  public static bool beenToSnow;
  public static Condition HasBeenToSnow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSnow"), (Func<bool>) (() => ModConditions.beenToSnow));
  public static bool beenToDesert;
  public static Condition HasBeenToDesert = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDesert"), (Func<bool>) (() => ModConditions.beenToDesert));
  public static bool beenToOcean;
  public static Condition HasBeenToOcean = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToOcean"), (Func<bool>) (() => ModConditions.beenToOcean));
  public static bool beenToJungle;
  public static Condition HasBeenToJungle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToJungle"), (Func<bool>) (() => ModConditions.beenToJungle));
  public static bool beenToMushroom;
  public static Condition HasBeenToMushroom = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToMushroom"), (Func<bool>) (() => ModConditions.beenToMushroom));
  public static bool beenToCorruption;
  public static Condition HasBeenToCorruption = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCorruption"), (Func<bool>) (() => ModConditions.beenToCorruption));
  public static bool beenToCrimson;
  public static Condition HasBeenToCrimson = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCrimson"), (Func<bool>) (() => ModConditions.beenToCrimson));
  public static Condition HasBeenToEvil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToEvil"), (Func<bool>) (() => ModConditions.beenToCorruption || ModConditions.beenToCrimson));
  public static bool beenToHallow;
  public static Condition HasBeenToHallow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToHallow"), (Func<bool>) (() => ModConditions.beenToHallow));
  public static bool beenToTemple;
  public static Condition HasBeenToTemple = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToTemple"), (Func<bool>) (() => ModConditions.beenToTemple));
  public static bool beenToDungeon;
  public static Condition HasBeenToDungeon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDungeon"), (Func<bool>) (() => ModConditions.beenToDungeon));
  public static bool beenToAether;
  public static Condition HasBeenToAether = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAether"), (Func<bool>) (() => ModConditions.beenToAether));
  public static bool talkedToSkeletonMerchant;
  public static Condition HasTalkedToSkeletonMerchant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.talkedToSkeletonMerchant"), (Func<bool>) (() => ModConditions.talkedToSkeletonMerchant));
  public static bool talkedToTravelingMerchant;
  public static Condition HasTalkedToTravelingMerchant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.talkedToTravelingMerchant"), (Func<bool>) (() => ModConditions.talkedToTravelingMerchant));
  public static bool aequusLoaded;
  public static Mod aequusMod;
  public static bool downedCrabson;
  public static Condition DownedCrabson = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCrabson"), (Func<bool>) (() => ModConditions.downedCrabson));
  public static bool downedOmegaStarite;
  public static Condition DownedOmegaStarite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaStarite"), (Func<bool>) (() => ModConditions.downedOmegaStarite));
  public static bool downedDustDevil;
  public static Condition DownedDustDevil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDustDevil"), (Func<bool>) (() => ModConditions.downedDustDevil));
  public static bool downedHyperStarite;
  public static Condition DownedHyperStarite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHyperStarite"), (Func<bool>) (() => ModConditions.downedHyperStarite));
  public static bool downedUltraStarite;
  public static Condition DownedUltraStarite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUltraStarite"), (Func<bool>) (() => ModConditions.downedUltraStarite));
  public static bool downedRedSprite;
  public static Condition DownedRedSprite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRedSprite"), (Func<bool>) (() => ModConditions.downedRedSprite));
  public static bool downedSpaceSquid;
  public static Condition DownedSpaceSquid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSpaceSquid"), (Func<bool>) (() => ModConditions.downedSpaceSquid));
  public static bool downedDemonSiege;
  public static Condition DownedDemonSiege = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDemonSiege"), (Func<bool>) (() => ModConditions.downedDemonSiege));
  public static bool downedGlimmer;
  public static Condition DownedGlimmer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlimmer"), (Func<bool>) (() => ModConditions.downedGlimmer));
  public static bool downedGaleStreams;
  public static Condition DownedGaleStreams = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGaleStreams"), (Func<bool>) (() => ModConditions.downedGaleStreams));
  public static bool beenToCrabCrevice;
  public static Condition HasBeenToCrabCrevice = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCrabCrevice"), (Func<bool>) (() => ModConditions.beenToCrabCrevice));
  public static bool afkpetsLoaded;
  public static Mod afkpetsMod;
  public static bool downedSlayerOfEvil;
  public static Condition DownedSlayerOfEvil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlayerOfEvil"), (Func<bool>) (() => ModConditions.DownedBoss[5]));
  public static bool downedSATLA;
  public static Condition DownedSATLA = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSATLA"), (Func<bool>) (() => ModConditions.DownedBoss[6]));
  public static bool downedDrFetus;
  public static Condition DownedDrFetus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDrFetus"), (Func<bool>) (() => ModConditions.DownedBoss[7]));
  public static bool downedSlimesHope;
  public static Condition DownedSlimesHope = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlimesHope"), (Func<bool>) (() => ModConditions.DownedBoss[8]));
  public static bool downedPoliticianSlime;
  public static Condition DownedPoliticianSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPoliticianSlime"), (Func<bool>) (() => ModConditions.DownedBoss[9]));
  public static bool downedAncientTrio;
  public static Condition DownedAncientTrio = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientTrio"), (Func<bool>) (() => ModConditions.DownedBoss[10]));
  public static bool downedLavalGolem;
  public static Condition DownedLavalGolem = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLavalGolem"), (Func<bool>) (() => ModConditions.DownedBoss[11]));
  public static bool downedAntony;
  public static Condition DownedAntony = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAntony"), (Func<bool>) (() => ModConditions.DownedBoss[12]));
  public static bool downedBunnyZeppelin;
  public static Condition DownedBunnyZeppelin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBunnyZeppelin"), (Func<bool>) (() => ModConditions.DownedBoss[13]));
  public static bool downedOkiku;
  public static Condition DownedOkiku = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOkiku"), (Func<bool>) (() => ModConditions.DownedBoss[14]));
  public static bool downedHarpyAirforce;
  public static Condition DownedHarpyAirforce = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHarpyAirforce"), (Func<bool>) (() => ModConditions.DownedBoss[15]));
  public static bool downedIsaac;
  public static Condition DownedIsaac = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIsaac"), (Func<bool>) (() => ModConditions.DownedBoss[16 /*0x10*/]));
  public static bool downedAncientGuardian;
  public static Condition DownedAncientGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientGuardian"), (Func<bool>) (() => ModConditions.DownedBoss[17]));
  public static bool downedHeroicSlime;
  public static Condition DownedHeroicSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHeroicSlime"), (Func<bool>) (() => ModConditions.DownedBoss[18]));
  public static bool downedHoloSlime;
  public static Condition DownedHoloSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHoloSlime"), (Func<bool>) (() => ModConditions.DownedBoss[19]));
  public static bool downedSecurityBot;
  public static Condition DownedSecurityBot = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSecurityBot"), (Func<bool>) (() => ModConditions.DownedBoss[20]));
  public static bool downedUndeadChef;
  public static Condition DownedUndeadChef = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUndeadChef"), (Func<bool>) (() => ModConditions.DownedBoss[21]));
  public static bool downedGuardianOfFrost;
  public static Condition DownedGuardianOfFrost = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGuardianOfFrost"), (Func<bool>) (() => ModConditions.DownedBoss[22]));
  public static bool amuletOfManyMinionsLoaded;
  public static Mod amuletOfManyMinionsMod;
  public static bool arbourLoaded;
  public static Mod arbourMod;
  public static bool assortedCrazyThingsLoaded;
  public static Mod assortedCrazyThingsMod;
  public static bool downedSoulHarvester;
  public static Condition DownedSoulHarvester = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSoulHarvester"), (Func<bool>) (() => ModConditions.DownedBoss[23]));
  public static bool awfulGarbageLoaded;
  public static Mod awfulGarbageMod;
  public static bool downedTreeToad;
  public static Condition DownedTreeToad = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTreeToad"), (Func<bool>) (() => ModConditions.DownedBoss[24]));
  public static bool downedSeseKitsugai;
  public static Condition DownedSeseKitsugai = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeseKitsugai"), (Func<bool>) (() => ModConditions.DownedBoss[25]));
  public static bool downedEyeOfTheStorm;
  public static Condition DownedEyeOfTheStorm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEyeOfTheStorm"), (Func<bool>) (() => ModConditions.DownedBoss[26]));
  public static bool downedFrigidius;
  public static Condition DownedFrigidius = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFrigidius"), (Func<bool>) (() => ModConditions.DownedBoss[27]));
  public static bool blocksArsenalLoaded;
  public static Mod blocksArsenalMod;
  public static bool blocksArtificerLoaded;
  public static Mod blocksArtificerMod;
  public static bool blocksCoreBossLoaded;
  public static Mod blocksCoreBossMod;
  public static bool downedCoreBoss;
  public static Condition DownedCoreBoss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCoreBoss"), (Func<bool>) (() => ModConditions.DownedBoss[28]));
  public static bool blocksInfoAccessoriesLoaded;
  public static Mod blocksInfoAccessoriesMod;
  public static bool blocksThrowerLoaded;
  public static Mod blocksThrowerMod;
  public static bool bombusApisLoaded;
  public static Mod bombusApisMod;
  public static bool buffariaLoaded;
  public static Mod buffariaMod;
  public static bool calamityLoaded;
  public static Mod calamityMod;
  public static bool downedDesertScourge;
  public static Condition DownedDesertScourge = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDesertScourge"), (Func<bool>) (() => ModConditions.downedDesertScourge));
  public static bool downedCrabulon;
  public static Condition DownedCrabulon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCrabulon"), (Func<bool>) (() => ModConditions.downedCrabulon));
  public static bool downedHiveMind;
  public static Condition DownedHiveMind = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHiveMind"), (Func<bool>) (() => ModConditions.downedHiveMind));
  public static bool downedPerforators;
  public static Condition DownedPerforators = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPerforators"), (Func<bool>) (() => ModConditions.downedPerforators));
  public static Condition DownedPerforatorsOrHiveMind = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPerfOrHive"), (Func<bool>) (() => ModConditions.downedPerforators || ModConditions.downedHiveMind));
  public static bool downedSlimeGod;
  public static Condition DownedSlimeGod = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlimeGod"), (Func<bool>) (() => ModConditions.downedSlimeGod));
  public static bool downedCryogen;
  public static Condition DownedCryogen = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCryogen"), (Func<bool>) (() => ModConditions.downedCryogen));
  public static bool downedAquaticScourge;
  public static Condition DownedAquaticScourge = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAquaticScourge"), (Func<bool>) (() => ModConditions.downedAquaticScourge));
  public static bool downedBrimstoneElemental;
  public static Condition DownedBrimstoneElemental = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBrimstoneElemental"), (Func<bool>) (() => ModConditions.downedBrimstoneElemental));
  public static bool downedCalamitasClone;
  public static Condition DownedCalamitasClone = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCalamitasClone"), (Func<bool>) (() => ModConditions.downedCalamitasClone));
  public static bool downedLeviathanAndAnahita;
  public static Condition DownedLeviathanAndAnahita = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLeviathanAndAnahita"), (Func<bool>) (() => ModConditions.downedLeviathanAndAnahita));
  public static bool downedAstrumAureus;
  public static Condition DownedAstrumAureus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAstrumAureus"), (Func<bool>) (() => ModConditions.downedAstrumAureus));
  public static bool downedPlaguebringerGoliath;
  public static Condition DownedPlaguebringerGoliath = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPlaguebringerGoliath"), (Func<bool>) (() => ModConditions.downedPlaguebringerGoliath));
  public static bool downedRavager;
  public static Condition DownedRavager = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRavager"), (Func<bool>) (() => ModConditions.downedRavager));
  public static bool downedAstrumDeus;
  public static Condition DownedAstrumDeus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAstrumDeus"), (Func<bool>) (() => ModConditions.downedAstrumDeus));
  public static bool downedProfanedGuardians;
  public static Condition DownedProfanedGuardians = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProfanedGuardians"), (Func<bool>) (() => ModConditions.downedProfanedGuardians));
  public static bool downedDragonfolly;
  public static Condition DownedDragonfolly = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDragonfolly"), (Func<bool>) (() => ModConditions.downedDragonfolly));
  public static bool downedProvidence;
  public static Condition DownedProvidence = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProvidence"), (Func<bool>) (() => ModConditions.downedProvidence));
  public static bool downedStormWeaver;
  public static Condition DownedStormWeaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStormWeaver"), (Func<bool>) (() => ModConditions.downedStormWeaver));
  public static bool downedCeaselessVoid;
  public static Condition DownedCeaselessVoid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCeaselessVoid"), (Func<bool>) (() => ModConditions.downedCeaselessVoid));
  public static bool downedSignus;
  public static Condition DownedSignus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSignus"), (Func<bool>) (() => ModConditions.downedSignus));
  public static bool downedPolterghast;
  public static Condition DownedPolterghast = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPolterghast"), (Func<bool>) (() => ModConditions.downedPolterghast));
  public static bool downedOldDuke;
  public static Condition DownedOldDuke = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOldDuke"), (Func<bool>) (() => ModConditions.downedOldDuke));
  public static bool downedDevourerOfGods;
  public static Condition DownedDevourerOfGods = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDevourerOfGods"), (Func<bool>) (() => ModConditions.downedDevourerOfGods));
  public static bool downedYharon;
  public static Condition DownedYharon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedYharon"), (Func<bool>) (() => ModConditions.downedYharon));
  public static bool downedExoMechs;
  public static Condition DownedExoMechs = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedExoMechs"), (Func<bool>) (() => ModConditions.downedExoMechs));
  public static bool downedSupremeCalamitas;
  public static Condition DownedSupremeCalamitas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSupremeCalamitas"), (Func<bool>) (() => ModConditions.downedSupremeCalamitas));
  public static bool downedGiantClam;
  public static Condition DownedGiantClam = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGiantClam"), (Func<bool>) (() => ModConditions.downedGiantClam));
  public static bool downedCragmawMire;
  public static Condition DownedCragmawMire = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCragmawMire"), (Func<bool>) (() => ModConditions.downedCragmawMire));
  public static bool downedGreatSandShark;
  public static Condition DownedGreatSandShark = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGreatSandShark"), (Func<bool>) (() => ModConditions.downedGreatSandShark));
  public static bool downedNuclearTerror;
  public static Condition DownedNuclearTerror = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNuclearTerror"), (Func<bool>) (() => ModConditions.downedNuclearTerror));
  public static bool downedMauler;
  public static Condition DownedMauler = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMauler"), (Func<bool>) (() => ModConditions.downedMauler));
  public static bool downedEidolonWyrm;
  public static Condition DownedEidolonWyrm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEidolonWyrm"), (Func<bool>) (() => ModConditions.downedEidolonWyrm));
  public static bool downedAcidRain1;
  public static Condition DownedAcidRain1 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAcidRain1"), (Func<bool>) (() => ModConditions.downedAcidRain1));
  public static bool downedAcidRain2;
  public static Condition DownedAcidRain2 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAcidRain2"), (Func<bool>) (() => ModConditions.downedAcidRain2));
  public static bool downedBossRush;
  public static Condition DownedBossRush = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBossRush"), (Func<bool>) (() => ModConditions.downedBossRush));
  public static bool beenToCrags;
  public static Condition HasBeenToCrags = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCrags"), (Func<bool>) (() => ModConditions.beenToCrags));
  public static bool beenToAstral;
  public static Condition HasBeenToAstral = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAstral"), (Func<bool>) (() => ModConditions.beenToAstral));
  public static bool beenToSunkenSea;
  public static Condition HasBeenToSunkenSea = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSunkenSea"), (Func<bool>) (() => ModConditions.beenToSunkenSea));
  public static bool beenToSulphurSea;
  public static Condition HasBeenToSulphurSea = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSulphurSea"), (Func<bool>) (() => ModConditions.beenToSulphurSea));
  public static bool beenToAbyss;
  public static Condition HasBeenToAbyss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyss"), (Func<bool>) (() => ModConditions.beenToAbyss));
  public static bool beenToAbyssLayer1;
  public static Condition HasBeenToAbyssLayer1 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer1"), (Func<bool>) (() => ModConditions.beenToAbyssLayer1));
  public static bool beenToAbyssLayer2;
  public static Condition HasBeenToAbyssLayer2 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer2"), (Func<bool>) (() => ModConditions.beenToAbyssLayer2));
  public static bool beenToAbyssLayer3;
  public static Condition HasBeenToAbyssLayer3 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer3"), (Func<bool>) (() => ModConditions.beenToAbyssLayer3));
  public static bool beenToAbyssLayer4;
  public static Condition HasBeenToAbyssLayer4 = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAbyssLayer4"), (Func<bool>) (() => ModConditions.beenToAbyssLayer4));
  public static Condition ShimmerableAfterMoonLordOrSupremeCalamitas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.shimmerableAfterMoonLordOrSupremeCalamitas"), (Func<bool>) (() =>
  {
    if (ModConditions.calamityLoaded && ModConditions.downedSupremeCalamitas && NPC.downedMoonlord)
      return true;
    return !ModConditions.calamityLoaded && NPC.downedMoonlord;
  }));
  public static bool calamityCommunityRemixLoaded;
  public static Mod calamityCommunityRemixMod;
  public static bool downedWulfrumExcavator;
  public static Condition DownedWulfrumExcavator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWulfrumExcavator"), (Func<bool>) (() => ModConditions.DownedBoss[32 /*0x20*/]));
  public static bool calamityEntropyLoaded;
  public static Mod calamityEntropyMod;
  public static bool downedLuminaris;
  public static Condition DownedLuminaris = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLuminaris"), (Func<bool>) (() => ModConditions.DownedBoss[33]));
  public static bool downedProphet;
  public static Condition DownedProphet = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProphet"), (Func<bool>) (() => ModConditions.DownedBoss[34]));
  public static bool downedNihilityTwin;
  public static Condition DownedNihilityTwin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNihilityTwin"), (Func<bool>) (() => ModConditions.DownedBoss[35]));
  public static bool downedCruiser;
  public static Condition DownedCruiser = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCruiser"), (Func<bool>) (() => ModConditions.DownedBoss[36]));
  public static bool calamityOverhaulLoaded;
  public static Mod calamityOverhaulMod;
  public static bool calamityVanitiesLoaded;
  public static Mod calamityVanitiesMod;
  public static bool beenToAstralBlight;
  public static Condition HasBeenToAstralBlight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAstralBlight"), (Func<bool>) (() => ModConditions.beenToAstralBlight));
  public static bool captureDiscsClassLoaded;
  public static Mod captureDiscsClassMod;
  public static bool catalystLoaded;
  public static Mod catalystMod;
  public static bool downedAstrageldon;
  public static Condition DownedAstrageldon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAstrageldon"), (Func<bool>) (() => ModConditions.DownedBoss[37]));
  public static bool cerebralLoaded;
  public static Mod cerebralMod;
  public static bool clamityAddonLoaded;
  public static Mod clamityAddonMod;
  public static bool downedClamitas;
  public static Condition DownedClamitas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedClamitas"), (Func<bool>) (() => ModConditions.DownedBoss[38]));
  public static bool downedPyrogen;
  public static Condition DownedPyrogen = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPyrogen"), (Func<bool>) (() => ModConditions.DownedBoss[39]));
  public static bool downedWallOfBronze;
  public static Condition DownedWallOfBronze = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWallOfBronze"), (Func<bool>) (() => ModConditions.DownedBoss[40]));
  public static bool clickerClassLoaded;
  public static Mod clickerClassMod;
  public static bool confectionRebakedLoaded;
  public static Mod confectionRebakedMod;
  public static bool beenToConfection;
  public static Condition HasBeenToConfection = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToConfection"), (Func<bool>) (() => ModConditions.beenToConfection));
  public static Condition HasBeenToConfectionOrHallow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToConfectionOrHallow"), (Func<bool>) (() => ModConditions.beenToConfection || ModConditions.beenToHallow));
  public static bool consolariaLoaded;
  public static Mod consolariaMod;
  public static bool downedLepus;
  public static Condition DownedLepus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLepus"), (Func<bool>) (() => ModConditions.DownedBoss[41]));
  public static bool downedTurkor;
  public static Condition DownedTurkor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTurkor"), (Func<bool>) (() => ModConditions.DownedBoss[42]));
  public static bool downedOcram;
  public static Condition DownedOcram = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOcram"), (Func<bool>) (() => ModConditions.DownedBoss[43]));
  public static bool coraliteLoaded;
  public static Mod coraliteMod;
  public static bool downedRediancie;
  public static Condition DownedRediancie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRediancie"), (Func<bool>) (() => ModConditions.DownedBoss[44]));
  public static bool downedBabyIceDragon;
  public static Condition DownedBabyIceDragon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBabyIceDragon"), (Func<bool>) (() => ModConditions.DownedBoss[45]));
  public static bool downedSlimeEmperor;
  public static Condition DownedSlimeEmperor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSlimeEmperor"), (Func<bool>) (() => ModConditions.DownedBoss[46]));
  public static bool downedBloodiancie;
  public static Condition DownedBloodiancie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBloodiancie"), (Func<bool>) (() => ModConditions.DownedBoss[47]));
  public static bool downedThunderveinDragon;
  public static Condition DownedThunderveinDragon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedThunderveinDragon"), (Func<bool>) (() => ModConditions.DownedBoss[48 /*0x30*/]));
  public static bool downedNightmarePlantera;
  public static Condition DownedNightmarePlantera = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNightmarePlantera"), (Func<bool>) (() => ModConditions.DownedBoss[49]));
  public static bool crystalDragonsLoaded;
  public static Mod crystalDragonsMod;
  public static bool depthsLoaded;
  public static Mod depthsMod;
  public static bool downedChasme;
  public static Condition DownedChasme = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChasme"), (Func<bool>) (() => ModConditions.DownedBoss[50]));
  public static bool beenToDepths;
  public static Condition HasBeenToDepths = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDepths"), (Func<bool>) (() => ModConditions.beenToDepths));
  public static Condition HasBeenToDepthsOrUnderworld = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDepthsOrUnderworld"), (Func<bool>) (() => ModConditions.beenToDepths || ModConditions.beenToUnderworld));
  public static bool dormantDawnLoaded;
  public static Mod dormantDawnMod;
  public static bool downedLifeGuardian;
  public static Condition DownedLifeGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLifeGuardian"), (Func<bool>) (() => ModConditions.DownedBoss[51]));
  public static bool downedManaGuardian;
  public static Condition DownedManaGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedManaGuardian"), (Func<bool>) (() => ModConditions.DownedBoss[52]));
  public static bool downedMeteorExcavator;
  public static Condition DownedMeteorExcavator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMeteorExcavator"), (Func<bool>) (() => ModConditions.DownedBoss[53]));
  public static bool downedMeteorAnnihilator;
  public static Condition DownedMeteorAnnihilator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMeteorAnnihilator"), (Func<bool>) (() => ModConditions.DownedBoss[54]));
  public static bool downedHellfireSerpent;
  public static Condition DownedHellfireSerpent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHellfireSerpent"), (Func<bool>) (() => ModConditions.DownedBoss[55]));
  public static bool downedWitheredAcornSpirit;
  public static Condition DownedWitheredAcornSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWitheredAcornSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[56]));
  public static bool downedGoblinSorcererChieftain;
  public static Condition DownedGoblinSorcererChieftain = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGoblinSorcererChieftain"), (Func<bool>) (() => ModConditions.DownedBoss[57]));
  public static bool draedonExpansionLoaded;
  public static Mod draedonExpansionMod;
  public static bool dragonBallTerrariaLoaded;
  public static Mod dragonBallTerrariaMod;
  public static bool echoesOfTheAncientsLoaded;
  public static Mod echoesOfTheAncientsMod;
  public static bool downedGalahis;
  public static Condition DownedGalahis = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGalahis"), (Func<bool>) (() => ModConditions.DownedBoss[58]));
  public static bool downedCreation;
  public static Condition DownedCreation = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCreation"), (Func<bool>) (() => ModConditions.DownedBoss[59]));
  public static bool downedDestruction;
  public static Condition DownedDestruction = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDestruction"), (Func<bool>) (() => ModConditions.DownedBoss[60]));
  public static bool edorbisLoaded;
  public static Mod edorbisMod;
  public static bool downedBlightKing;
  public static Condition DownedBlightKing = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlightKing"), (Func<bool>) (() => ModConditions.DownedBoss[61]));
  public static bool downedGardener;
  public static Condition DownedGardener = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGardener"), (Func<bool>) (() => ModConditions.DownedBoss[62]));
  public static bool downedGlaciation;
  public static Condition DownedGlaciation = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlaciation"), (Func<bool>) (() => ModConditions.DownedBoss[63 /*0x3F*/]));
  public static bool downedHandOfCthulhu;
  public static Condition DownedHandOfCthulhu = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHandOfCthulhu"), (Func<bool>) (() => ModConditions.DownedBoss[64 /*0x40*/]));
  public static bool downedCursePreacher;
  public static Condition DownedCursePreacher = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCursePreacher"), (Func<bool>) (() => ModConditions.DownedBoss[65]));
  public static bool enchantedMoonsLoaded;
  public static Mod enchantedMoonsMod;
  public static bool everjadeLoaded;
  public static Mod everjadeMod;
  public static bool beenToJadeLake;
  public static Condition HasBeenToJadeLake = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToJadeLake"), (Func<bool>) (() => ModConditions.beenToJadeLake));
  public static bool exaltLoaded;
  public static Mod exaltMod;
  public static bool downedEffulgence;
  public static Condition DownedEffulgence = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEffulgence"), (Func<bool>) (() => ModConditions.DownedBoss[66]));
  public static bool downedIceLich;
  public static Condition DownedIceLich = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIceLich"), (Func<bool>) (() => ModConditions.DownedBoss[67]));
  public static bool excelsiorLoaded;
  public static Mod excelsiorMod;
  public static bool downedNiflheim;
  public static Condition DownedNiflheim = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNiflheim"), (Func<bool>) (() => ModConditions.DownedBoss[68]));
  public static bool downedStellarStarship;
  public static Condition DownedStellarStarship = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStellarStarship"), (Func<bool>) (() => ModConditions.DownedBoss[69]));
  public static bool exxoAvalonOriginsLoaded;
  public static Mod exxoAvalonOriginsMod;
  public static bool downedBacteriumPrime;
  public static Condition DownedBacteriumPrime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBacteriumPrime"), (Func<bool>) (() => ModConditions.DownedBoss[70]));
  public static Condition DownedAvalonEvilBosses = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAvalonEvilBosses"), (Func<bool>) (() => ModConditions.DownedBoss[70] || NPC.downedBoss2));
  public static bool downedDesertBeak;
  public static Condition DownedDesertBeak = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDesertBeak"), (Func<bool>) (() => ModConditions.DownedBoss[71]));
  public static bool downedKingSting;
  public static Condition DownedKingSting = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKingSting"), (Func<bool>) (() => ModConditions.DownedBoss[72]));
  public static bool downedMechasting;
  public static Condition DownedMechasting = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMechasting"), (Func<bool>) (() => ModConditions.DownedBoss[73]));
  public static bool downedPhantasm;
  public static Condition DownedPhantasm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPhantasm"), (Func<bool>) (() => ModConditions.DownedBoss[74]));
  public static bool beenToContagion;
  public static Condition HasBeenToContagion = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToContagion"), (Func<bool>) (() => ModConditions.beenToContagion));
  public static Condition HasBeenToAvalonEvilBiomes = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAvalonEvilBiomes"), (Func<bool>) (() => ModConditions.beenToContagion || ModConditions.beenToCorruption || ModConditions.beenToCrimson));
  public static bool fargosMutantLoaded;
  public static Mod fargosMutantMod;
  public static bool fargosSoulsLoaded;
  public static Mod fargosSoulsMod;
  public static bool downedTrojanSquirrel;
  public static Condition DownedTrojanSquirrel = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTrojanSquirrel"), (Func<bool>) (() => ModConditions.DownedBoss[75]));
  public static bool downedCursedCoffin;
  public static Condition DownedCursedCoffin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCursedCoffin"), (Func<bool>) (() => ModConditions.DownedBoss[76]));
  public static bool downedDeviantt;
  public static Condition DownedDeviantt = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDeviantt"), (Func<bool>) (() => ModConditions.DownedBoss[77]));
  public static bool downedLifelight;
  public static Condition DownedLifelight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLifelight"), (Func<bool>) (() => ModConditions.DownedBoss[78]));
  public static bool downedBanishedBaron;
  public static Condition DownedBanishedBaron = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBanishedBaron"), (Func<bool>) (() => ModConditions.DownedBoss[79]));
  public static bool downedEridanus;
  public static Condition DownedEridanus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEridanus"), (Func<bool>) (() => ModConditions.DownedBoss[80 /*0x50*/]));
  public static bool downedAbominationn;
  public static Condition DownedAbominationn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAbominationn"), (Func<bool>) (() => ModConditions.DownedBoss[81]));
  public static bool downedMutant;
  public static Condition DownedMutant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMutant"), (Func<bool>) (() => ModConditions.DownedBoss[82]));
  public static bool fargosSoulsDLCLoaded;
  public static Mod fargosSoulsDLCMod;
  public static bool fargosSoulsExtrasLoaded;
  public static Mod fargosSoulsExtrasMod;
  public static bool fracturesOfPenumbraLoaded;
  public static Mod fracturesOfPenumbraMod;
  public static bool downedAlphaFrostjaw;
  public static Condition DownedAlphaFrostjaw = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAlphaFrostjaw"), (Func<bool>) (() => ModConditions.DownedBoss[83]));
  public static bool downedSanguineElemental;
  public static Condition DownedSanguineElemental = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSanguineElemental"), (Func<bool>) (() => ModConditions.DownedBoss[84]));
  public static bool beenToDread;
  public static Condition HasBeenToDread = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToDread"), (Func<bool>) (() => ModConditions.beenToDread));
  public static bool furnitureFoodAndFunLoaded;
  public static Mod furnitureFoodAndFunMod;
  public static bool gameTerrariaLoaded;
  public static Mod gameTerrariaMod;
  public static bool downedLad;
  public static Condition DownedLad = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLad"), (Func<bool>) (() => ModConditions.DownedBoss[85]));
  public static bool downedHornlitz;
  public static Condition DownedHornlitz = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHornlitz"), (Func<bool>) (() => ModConditions.DownedBoss[86]));
  public static bool downedSnowDon;
  public static Condition DownedSnowDon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSnowDon"), (Func<bool>) (() => ModConditions.DownedBoss[87]));
  public static bool downedStoffie;
  public static Condition DownedStoffie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStoffie"), (Func<bool>) (() => ModConditions.DownedBoss[88]));
  public static bool gensokyoLoaded;
  public static Mod gensokyoMod;
  public static bool downedLilyWhite;
  public static Condition DownedLilyWhite = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLilyWhite"), (Func<bool>) (() => ModConditions.DownedBoss[89]));
  public static bool downedRumia;
  public static Condition DownedRumia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRumia"), (Func<bool>) (() => ModConditions.DownedBoss[90]));
  public static bool downedEternityLarva;
  public static Condition DownedEternityLarva = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEternityLarva"), (Func<bool>) (() => ModConditions.DownedBoss[91]));
  public static bool downedNazrin;
  public static Condition DownedNazrin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNazrin"), (Func<bool>) (() => ModConditions.DownedBoss[92]));
  public static bool downedHinaKagiyama;
  public static Condition DownedHinaKagiyama = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHinaKagiyama"), (Func<bool>) (() => ModConditions.DownedBoss[93]));
  public static bool downedSekibanki;
  public static Condition DownedSekibanki = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSekibanki"), (Func<bool>) (() => ModConditions.DownedBoss[94]));
  public static bool downedSeiran;
  public static Condition DownedSeiran = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeiran"), (Func<bool>) (() => ModConditions.DownedBoss[95]));
  public static bool downedNitoriKawashiro;
  public static Condition DownedNitoriKawashiro = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNitoriKawashiro"), (Func<bool>) (() => ModConditions.DownedBoss[96 /*0x60*/]));
  public static bool downedMedicineMelancholy;
  public static Condition DownedMedicineMelancholy = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMedicineMelancholy"), (Func<bool>) (() => ModConditions.DownedBoss[97]));
  public static bool downedCirno;
  public static Condition DownedCirno = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCirno"), (Func<bool>) (() => ModConditions.DownedBoss[98]));
  public static bool downedMinamitsuMurasa;
  public static Condition DownedMinamitsuMurasa = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMinamitsuMurasa"), (Func<bool>) (() => ModConditions.DownedBoss[99]));
  public static bool downedAliceMargatroid;
  public static Condition DownedAliceMargatroid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAliceMargatroid"), (Func<bool>) (() => ModConditions.DownedBoss[100]));
  public static bool downedSakuyaIzayoi;
  public static Condition DownedSakuyaIzayoi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSakuyaIzayoi"), (Func<bool>) (() => ModConditions.DownedBoss[101]));
  public static bool downedSeijaKijin;
  public static Condition DownedSeijaKijin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeijaKijin"), (Func<bool>) (() => ModConditions.DownedBoss[102]));
  public static bool downedMayumiJoutouguu;
  public static Condition DownedMayumiJoutouguu = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMayumiJoutouguu"), (Func<bool>) (() => ModConditions.DownedBoss[103]));
  public static bool downedToyosatomimiNoMiko;
  public static Condition DownedToyosatomimiNoMiko = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedToyosatomimiNoMiko"), (Func<bool>) (() => ModConditions.DownedBoss[104]));
  public static bool downedKaguyaHouraisan;
  public static Condition DownedKaguyaHouraisan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKaguyaHouraisan"), (Func<bool>) (() => ModConditions.DownedBoss[105]));
  public static bool downedUtsuhoReiuji;
  public static Condition DownedUtsuhoReiuji = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUtsuhoReiuji"), (Func<bool>) (() => ModConditions.DownedBoss[106]));
  public static bool downedTenshiHinanawi;
  public static Condition DownedTenshiHinanawi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTenshiHinanawi"), (Func<bool>) (() => ModConditions.DownedBoss[107]));
  public static bool downedKisume;
  public static Condition DownedKisume = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKisume"), (Func<bool>) (() => ModConditions.DownedBoss[108]));
  public static bool gerdsLabLoaded;
  public static Mod gerdsLabMod;
  public static bool downedTrerios;
  public static Condition DownedTrerios = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTrerios"), (Func<bool>) (() => ModConditions.DownedBoss[109]));
  public static bool downedMagmaEye;
  public static Condition DownedMagmaEye = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMagmaEye"), (Func<bool>) (() => ModConditions.DownedBoss[110]));
  public static bool downedJack;
  public static Condition DownedJack = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJack"), (Func<bool>) (() => ModConditions.DownedBoss[111]));
  public static bool downedAcheron;
  public static Condition DownedAcheron = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAcheron"), (Func<bool>) (() => ModConditions.DownedBoss[112 /*0x70*/]));
  public static bool heartbeatariaLoaded;
  public static Mod heartbeatariaMod;
  public static bool homewardJourneyLoaded;
  public static Mod homewardJourneyMod;
  public static bool downedMarquisMoonsquid;
  public static Condition DownedMarquisMoonsquid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMarquisMoonsquid"), (Func<bool>) (() => ModConditions.DownedBoss[113]));
  public static bool downedPriestessRod;
  public static Condition DownedPriestessRod = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPriestessRod"), (Func<bool>) (() => ModConditions.DownedBoss[114]));
  public static bool downedDiver;
  public static Condition DownedDiver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDiver"), (Func<bool>) (() => ModConditions.DownedBoss[115]));
  public static bool downedMotherbrain;
  public static Condition DownedMotherbrain = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMotherbrain"), (Func<bool>) (() => ModConditions.DownedBoss[116]));
  public static bool downedWallOfShadow;
  public static Condition DownedWallOfShadow = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWallOfShadow"), (Func<bool>) (() => ModConditions.DownedBoss[117]));
  public static bool downedSunSlimeGod;
  public static Condition DownedSunSlimeGod = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSunSlimeGod"), (Func<bool>) (() => ModConditions.DownedBoss[118]));
  public static bool downedOverwatcher;
  public static Condition DownedOverwatcher = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOverwatcher"), (Func<bool>) (() => ModConditions.DownedBoss[119]));
  public static bool downedLifebringer;
  public static Condition DownedLifebringer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLifebringer"), (Func<bool>) (() => ModConditions.DownedBoss[120]));
  public static bool downedMaterealizer;
  public static Condition DownedMaterealizer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMaterealizer"), (Func<bool>) (() => ModConditions.DownedBoss[121]));
  public static bool downedScarabBelief;
  public static Condition DownedScarabBelief = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedScarabBelief"), (Func<bool>) (() => ModConditions.DownedBoss[122]));
  public static bool downedWorldsEndWhale;
  public static Condition DownedWorldsEndWhale = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWorldsEndWhale"), (Func<bool>) (() => ModConditions.DownedBoss[123]));
  public static bool downedSon;
  public static Condition DownedSon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSon"), (Func<bool>) (() => ModConditions.DownedBoss[124]));
  public static bool downedCaveOrdeal;
  public static Condition DownedCaveOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCaveOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[125]));
  public static bool downedCorruptOrdeal;
  public static Condition DownedCorruptOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCorruptOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[126]));
  public static bool downedCrimsonOrdeal;
  public static Condition DownedCrimsonOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCrimsonOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[(int) sbyte.MaxValue]));
  public static bool downedDesertOrdeal;
  public static Condition DownedDesertOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDesertOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[128 /*0x80*/]));
  public static bool downedForestOrdeal;
  public static Condition DownedForestOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForestOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[129]));
  public static bool downedHallowOrdeal;
  public static Condition DownedHallowOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHallowOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[130]));
  public static bool downedJungleOrdeal;
  public static Condition DownedJungleOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJungleOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[131]));
  public static bool downedSkyOrdeal;
  public static Condition DownedSkyOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSkyOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[132]));
  public static bool downedSnowOrdeal;
  public static Condition DownedSnowOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSnowOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[133]));
  public static bool downedUnderworldOrdeal;
  public static Condition DownedUnderworldOrdeal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUnderworldOrdeal"), (Func<bool>) (() => ModConditions.DownedBoss[134]));
  public static Condition DownedOrdealAny = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOrdealAny"), (Func<bool>) (() => ModConditions.DownedBoss[125] || ModConditions.DownedBoss[126] || ModConditions.DownedBoss[(int) sbyte.MaxValue] || ModConditions.DownedBoss[128 /*0x80*/] || ModConditions.DownedBoss[129] || ModConditions.DownedBoss[130] || ModConditions.DownedBoss[131] || ModConditions.DownedBoss[132] || ModConditions.DownedBoss[133] || ModConditions.DownedBoss[134]));
  public static bool beenToHomewardAbyss;
  public static Condition HasBeenToHomewardAbyss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToHomewardAbyss"), (Func<bool>) (() => ModConditions.beenToHomewardAbyss));
  public static bool huntOfTheOldGodLoaded;
  public static Mod huntOfTheOldGodMod;
  public static bool downedGoozma;
  public static Condition DownedGoozma = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGoozma"), (Func<bool>) (() => ModConditions.DownedBoss[135]));
  public static bool infectedQualitiesLoaded;
  public static Mod infectedQualitiesMod;
  public static bool infernumLoaded;
  public static Mod infernumMod;
  public static bool downedBereftVassal;
  public static Condition DownedBereftVassal = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBereftVassal"), (Func<bool>) (() => ModConditions.DownedBoss[136]));
  public static bool beenToProfanedGardens;
  public static Condition HasBeenToProfanedGardens = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToProfanedGardens"), (Func<bool>) (() => ModConditions.beenToProfanedGardens));
  public static bool luiAFKLoaded;
  public static Mod luiAFKMod;
  public static bool luiAFKDLCLoaded;
  public static Mod luiAFKDLCMod;
  public static bool lunarVeilLoaded;
  public static Mod lunarVeilMod;
  public static bool downedStoneGuardian;
  public static Condition DownedStoneGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStoneGuardian"), (Func<bool>) (() => ModConditions.DownedBoss[137]));
  public static bool downedCommanderGintzia;
  public static Condition DownedCommanderGintzia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCommanderGintzia"), (Func<bool>) (() => ModConditions.DownedBoss[138]));
  public static bool downedSunStalker;
  public static Condition DownedSunStalker = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSunStalker"), (Func<bool>) (() => ModConditions.DownedBoss[139]));
  public static bool downedPumpkinJack;
  public static Condition DownedPumpkinJack = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPumpkinJack"), (Func<bool>) (() => ModConditions.DownedBoss[140]));
  public static bool downedForgottenPuppetDaedus;
  public static Condition DownedForgottenPuppetDaedus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForgottenPuppetDaedus"), (Func<bool>) (() => ModConditions.DownedBoss[141]));
  public static bool downedDreadMire;
  public static Condition DownedDreadMire = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDreadMire"), (Func<bool>) (() => ModConditions.DownedBoss[142]));
  public static bool downedSingularityFragment;
  public static Condition DownedSingularityFragment = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSingularityFragment"), (Func<bool>) (() => ModConditions.DownedBoss[143]));
  public static bool downedVerlia;
  public static Condition DownedVerlia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVerlia"), (Func<bool>) (() => ModConditions.DownedBoss[144 /*0x90*/]));
  public static bool downedIrradia;
  public static Condition DownedIrradia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIrradia"), (Func<bool>) (() => ModConditions.DownedBoss[145]));
  public static bool downedSylia;
  public static Condition DownedSylia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSylia"), (Func<bool>) (() => ModConditions.DownedBoss[146]));
  public static bool downedFenix;
  public static Condition DownedFenix = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFenix"), (Func<bool>) (() => ModConditions.DownedBoss[147]));
  public static bool downedBlazingSerpent;
  public static Condition DownedBlazingSerpent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlazingSerpent"), (Func<bool>) (() => ModConditions.DownedBoss[148]));
  public static bool downedCogwork;
  public static Condition DownedCogwork = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCogwork"), (Func<bool>) (() => ModConditions.DownedBoss[149]));
  public static bool downedWaterCogwork;
  public static Condition DownedWaterCogwork = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWaterCogwork"), (Func<bool>) (() => ModConditions.DownedBoss[150]));
  public static bool downedWaterJellyfish;
  public static Condition DownedWaterJellyfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWaterJellyfish"), (Func<bool>) (() => ModConditions.DownedBoss[151]));
  public static bool downedSparn;
  public static Condition DownedSparn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSparn"), (Func<bool>) (() => ModConditions.DownedBoss[152]));
  public static bool downedPandorasFlamebox;
  public static Condition DownedPandorasFlamebox = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPandorasFlamebox"), (Func<bool>) (() => ModConditions.DownedBoss[153]));
  public static bool downedSTARBOMBER;
  public static Condition DownedSTARBOMBER = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSTARBOMBER"), (Func<bool>) (() => ModConditions.DownedBoss[154]));
  public static Condition DownedWaterJellyfishOrWaterCogwork = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWaterJellyfishOrWaterCogwork"), (Func<bool>) (() => ModConditions.DownedBoss[150] || ModConditions.DownedBoss[151]));
  public static Condition DownedCogworkOrSparn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCogworkOrSparn"), (Func<bool>) (() => ModConditions.DownedBoss[149] || ModConditions.DownedBoss[152]));
  public static Condition DownedBlazingSerpentOrPandorasFlamebox = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlazingSerpentOrPandorasFlamebox"), (Func<bool>) (() => ModConditions.DownedBoss[148] || ModConditions.DownedBoss[153]));
  public static bool downedGintzeArmy;
  public static Condition DownedGintzeArmy = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGintzeArmy"), (Func<bool>) (() => ModConditions.DownedBoss[155]));
  public static bool beenToLunarVeilAbyss;
  public static Condition HasBeenToLunarVeilAbyss = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToLunarVeilAbyss"), (Func<bool>) (() => ModConditions.beenToLunarVeilAbyss));
  public static bool beenToAcid;
  public static Condition HasBeenToAcid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAcid"), (Func<bool>) (() => ModConditions.beenToAcid));
  public static bool beenToAurelus;
  public static Condition HasBeenToAurelus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAurelus"), (Func<bool>) (() => ModConditions.beenToAurelus));
  public static bool beenToFable;
  public static Condition HasBeenToFable = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToFable"), (Func<bool>) (() => ModConditions.beenToFable));
  public static bool beenToGovheilCastle;
  public static Condition HasBeenToGovheilCastle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToGovheilCastle"), (Func<bool>) (() => ModConditions.beenToGovheilCastle));
  public static bool beenToCathedral;
  public static Condition HasBeenToCathedral = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCathedral"), (Func<bool>) (() => ModConditions.beenToCathedral));
  public static bool beenToMarrowSurface;
  public static Condition HasBeenToMarrowSurface = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToMarrowSurface"), (Func<bool>) (() => ModConditions.beenToMarrowSurface));
  public static bool beenToMorrowUnderground;
  public static Condition HasBeenToMorrowUnderground = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToMorrowUnderground"), (Func<bool>) (() => ModConditions.beenToMorrowUnderground));
  public static bool magicStorageLoaded;
  public static Mod magicStorageMod;
  public static bool martainsOrderLoaded;
  public static Mod martainsOrderMod;
  public static bool downedBritzz;
  public static Condition DownedBritzz = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBritzz"), (Func<bool>) (() => ModConditions.DownedBoss[156]));
  public static bool downedTheAlchemist;
  public static Condition DownedTheAlchemist = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTheAlchemist"), (Func<bool>) (() => ModConditions.DownedBoss[157]));
  public static bool downedCarnagePillar;
  public static Condition DownedCarnagePillar = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCarnagePillar"), (Func<bool>) (() => ModConditions.DownedBoss[158]));
  public static bool downedVoidDigger;
  public static Condition DownedVoidDigger = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVoidDigger"), (Func<bool>) (() => ModConditions.DownedBoss[159]));
  public static bool downedPrinceSlime;
  public static Condition DownedPrinceSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrinceSlime"), (Func<bool>) (() => ModConditions.DownedBoss[160 /*0xA0*/]));
  public static bool downedTriplets;
  public static Condition DownedTriplets = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTriplets"), (Func<bool>) (() => ModConditions.DownedBoss[161]));
  public static bool downedJungleDefenders;
  public static Condition DownedJungleDefenders = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJungleDefenders"), (Func<bool>) (() => ModConditions.DownedBoss[162]));
  public static bool mechReworkLoaded;
  public static Mod mechReworkMod;
  public static bool downedSt4sys;
  public static Condition DownedSt4sys = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSt4sys"), (Func<bool>) (() => ModConditions.DownedBoss[163]));
  public static bool downedTerminator;
  public static Condition DownedTerminator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTerminator"), (Func<bool>) (() => ModConditions.DownedBoss[164]));
  public static bool downedCaretaker;
  public static Condition DownedCaretaker = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCaretaker"), (Func<bool>) (() => ModConditions.DownedBoss[165]));
  public static bool downedSiegeEngine;
  public static Condition DownedSiegeEngine = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSiegeEngine"), (Func<bool>) (() => ModConditions.DownedBoss[166]));
  public static bool medialRiftLoaded;
  public static Mod medialRiftMod;
  public static bool downedSuperVoltaicMotherSlime;
  public static Condition DownedSuperVoltaicMotherSlime = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSuperVoltaicMotherSlime"), (Func<bool>) (() => ModConditions.DownedBoss[167]));
  public static bool metroidLoaded;
  public static Mod metroidMod;
  public static bool downedTorizo;
  public static Condition DownedTorizo = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTorizo"), (Func<bool>) (() => ModConditions.DownedBoss[168]));
  public static bool downedSerris;
  public static Condition DownedSerris = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSerris"), (Func<bool>) (() => ModConditions.DownedBoss[169]));
  public static bool downedKraid;
  public static Condition DownedKraid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKraid"), (Func<bool>) (() => ModConditions.DownedBoss[170]));
  public static bool downedPhantoon;
  public static Condition DownedPhantoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPhantoon"), (Func<bool>) (() => ModConditions.DownedBoss[171]));
  public static bool downedOmegaPirate;
  public static Condition DownedOmegaPirate = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaPirate"), (Func<bool>) (() => ModConditions.DownedBoss[172]));
  public static bool downedNightmare;
  public static Condition DownedNightmare = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNightmare"), (Func<bool>) (() => ModConditions.DownedBoss[173]));
  public static bool downedGoldenTorizo;
  public static Condition DownedGoldenTorizo = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGoldenTorizo"), (Func<bool>) (() => ModConditions.DownedBoss[174]));
  public static bool moomoosUltimateYoyoRevampLoaded;
  public static Mod moomoosUltimateYoyoRevampMod;
  public static bool mrPlagueRacesLoaded;
  public static Mod mrPlagueRacesMod;
  public static bool orchidLoaded;
  public static Mod orchidMod;
  public static bool ophioidLoaded;
  public static Mod ophioidMod;
  public static bool downedOphiopede;
  public static Condition DownedOphiopede = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOphiopede"), (Func<bool>) (() => ModConditions.DownedBoss[175]));
  public static bool downedOphiocoon;
  public static Condition DownedOphiocoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOphiocoon"), (Func<bool>) (() => ModConditions.DownedBoss[176 /*0xB0*/]));
  public static bool downedOphiofly;
  public static Condition DownedOphiofly = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOphiofly"), (Func<bool>) (() => ModConditions.DownedBoss[177]));
  public static bool polaritiesLoaded;
  public static Mod polaritiesMod;
  public static bool downedStormCloudfish;
  public static Condition DownedStormCloudfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStormCloudfish"), (Func<bool>) (() => ModConditions.DownedBoss[178]));
  public static bool downedStarConstruct;
  public static Condition DownedStarConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[179]));
  public static bool downedGigabat;
  public static Condition DownedGigabat = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGigabat"), (Func<bool>) (() => ModConditions.DownedBoss[180]));
  public static bool downedSunPixie;
  public static Condition DownedSunPixie = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSunPixie"), (Func<bool>) (() => ModConditions.DownedBoss[181]));
  public static bool downedEsophage;
  public static Condition DownedEsophage = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEsophage"), (Func<bool>) (() => ModConditions.DownedBoss[182]));
  public static bool downedConvectiveWanderer;
  public static Condition DownedConvectiveWanderer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedConvectiveWanderer"), (Func<bool>) (() => ModConditions.DownedBoss[183]));
  public static bool projectZeroLoaded;
  public static Mod projectZeroMod;
  public static bool downedForestGuardian;
  public static Condition DownedForestGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForestGuardian"), (Func<bool>) (() => ModConditions.DownedBoss[184]));
  public static bool downedCryoGuardian;
  public static Condition DownedCryoGuardian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCryoGuardian"), (Func<bool>) (() => ModConditions.DownedBoss[185]));
  public static bool downedPrimordialWorm;
  public static Condition DownedPrimordialWorm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrimordialWorm"), (Func<bool>) (() => ModConditions.DownedBoss[186]));
  public static bool downedTheGuardianOfHell;
  public static Condition DownedTheGuardianOfHell = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTheGuardianOfHell"), (Func<bool>) (() => ModConditions.DownedBoss[187]));
  public static bool downedVoid;
  public static Condition DownedVoid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVoid"), (Func<bool>) (() => ModConditions.DownedBoss[188]));
  public static bool downedArmagem;
  public static Condition DownedArmagem = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedArmagem"), (Func<bool>) (() => ModConditions.DownedBoss[189]));
  public static bool qwertyLoaded;
  public static Mod qwertyMod;
  public static bool downedPolarExterminator;
  public static Condition DownedPolarExterminator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPolarExterminator"), (Func<bool>) (() => ModConditions.DownedBoss[190]));
  public static bool downedDivineLight;
  public static Condition DownedDivineLight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDivineLight"), (Func<bool>) (() => ModConditions.DownedBoss[191]));
  public static bool downedAncientMachine;
  public static Condition DownedAncientMachine = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientMachine"), (Func<bool>) (() => ModConditions.DownedBoss[192 /*0xC0*/]));
  public static bool downedNoehtnap;
  public static Condition DownedNoehtnap = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNoehtnap"), (Func<bool>) (() => ModConditions.DownedBoss[193]));
  public static bool downedHydra;
  public static Condition DownedHydra = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHydra"), (Func<bool>) (() => ModConditions.DownedBoss[194]));
  public static bool downedImperious;
  public static Condition DownedImperious = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedImperious"), (Func<bool>) (() => ModConditions.DownedBoss[195]));
  public static bool downedRuneGhost;
  public static Condition DownedRuneGhost = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRuneGhost"), (Func<bool>) (() => ModConditions.DownedBoss[196]));
  public static bool downedInvaderBattleship;
  public static Condition DownedInvaderBattleship = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInvaderBattleship"), (Func<bool>) (() => ModConditions.DownedBoss[197]));
  public static bool downedInvaderNoehtnap;
  public static Condition DownedInvaderNoehtnap = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInvaderNoehtnap"), (Func<bool>) (() => ModConditions.DownedBoss[198]));
  public static bool downedOLORD;
  public static Condition DownedOLORD = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOLORD"), (Func<bool>) (() => ModConditions.DownedBoss[199]));
  public static bool downedGreatTyrannosaurus;
  public static Condition DownedGreatTyrannosaurus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGreatTyrannosaurus"), (Func<bool>) (() => ModConditions.DownedBoss[200]));
  public static bool downedDinoMilitia;
  public static Condition DownedDinoMilitia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDinoMilitia"), (Func<bool>) (() => ModConditions.DownedBoss[201]));
  public static bool downedInvaders;
  public static Condition DownedInvaders = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInvaders"), (Func<bool>) (() => ModConditions.DownedBoss[202]));
  public static bool beenToSkyFortress;
  public static Condition HasBeenToSkyFortress = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSkyFortress"), (Func<bool>) (() => ModConditions.beenToSkyFortress));
  public static bool ragnarokLoaded;
  public static Mod ragnarokMod;
  public static bool redemptionLoaded;
  public static Mod redemptionMod;
  public static bool downedThorn;
  public static Condition DownedThorn = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedThorn"), (Func<bool>) (() => ModConditions.DownedBoss[203]));
  public static bool downedErhan;
  public static Condition DownedErhan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedErhan"), (Func<bool>) (() => ModConditions.DownedBoss[204]));
  public static bool downedKeeper;
  public static Condition DownedKeeper = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKeeper"), (Func<bool>) (() => ModConditions.DownedBoss[205]));
  public static bool downedSeedOfInfection;
  public static Condition DownedSeedOfInfection = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSeedOfInfection"), (Func<bool>) (() => ModConditions.DownedBoss[206]));
  public static bool downedKingSlayerIII;
  public static Condition DownedKingSlayerIII = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedKingSlayerIII"), (Func<bool>) (() => ModConditions.DownedBoss[207]));
  public static bool downedOmegaCleaver;
  public static Condition DownedOmegaCleaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaCleaver"), (Func<bool>) (() => ModConditions.DownedBoss[208 /*0xD0*/]));
  public static bool downedOmegaGigapora;
  public static Condition DownedOmegaGigapora = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaGigapora"), (Func<bool>) (() => ModConditions.DownedBoss[209]));
  public static bool downedOmegaObliterator;
  public static Condition DownedOmegaObliterator = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOmegaObliterator"), (Func<bool>) (() => ModConditions.DownedBoss[210]));
  public static bool downedPatientZero;
  public static Condition DownedPatientZero = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPatientZero"), (Func<bool>) (() => ModConditions.DownedBoss[211]));
  public static bool downedAkka;
  public static Condition DownedAkka = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAkka"), (Func<bool>) (() => ModConditions.DownedBoss[212]));
  public static bool downedUkko;
  public static Condition DownedUkko = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedUkko"), (Func<bool>) (() => ModConditions.DownedBoss[213]));
  public static bool downedAncientDeityDuo;
  public static Condition DownedAncientDeityDuo = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientDeityDuo"), (Func<bool>) (() => ModConditions.DownedBoss[214]));
  public static bool downedNebuleus;
  public static Condition DownedNebuleus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNebuleus"), (Func<bool>) (() => ModConditions.DownedBoss[215]));
  public static bool downedFowlEmperor;
  public static Condition DownedFowlEmperor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFowlEmperor"), (Func<bool>) (() => ModConditions.DownedBoss[216]));
  public static bool downedCockatrice;
  public static Condition DownedCockatrice = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCockatrice"), (Func<bool>) (() => ModConditions.DownedBoss[217]));
  public static bool downedBasan;
  public static Condition DownedBasan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBasan"), (Func<bool>) (() => ModConditions.DownedBoss[218]));
  public static bool downedSkullDigger;
  public static Condition DownedSkullDigger = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSkullDigger"), (Func<bool>) (() => ModConditions.DownedBoss[219]));
  public static bool downedEaglecrestGolem;
  public static Condition DownedEaglecrestGolem = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEaglecrestGolem"), (Func<bool>) (() => ModConditions.DownedBoss[220]));
  public static bool downedCalavia;
  public static Condition DownedCalavia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCalavia"), (Func<bool>) (() => ModConditions.DownedBoss[221]));
  public static bool downedTheJanitor;
  public static Condition DownedTheJanitor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTheJanitor"), (Func<bool>) (() => ModConditions.DownedBoss[222]));
  public static bool downedIrradiatedBehemoth;
  public static Condition DownedIrradiatedBehemoth = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIrradiatedBehemoth"), (Func<bool>) (() => ModConditions.DownedBoss[223]));
  public static bool downedBlisterface;
  public static Condition DownedBlisterface = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBlisterface"), (Func<bool>) (() => ModConditions.DownedBoss[224 /*0xE0*/]));
  public static bool downedProtectorVolt;
  public static Condition DownedProtectorVolt = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedProtectorVolt"), (Func<bool>) (() => ModConditions.DownedBoss[225]));
  public static bool downedMACEProject;
  public static Condition DownedMACEProject = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMACEProject"), (Func<bool>) (() => ModConditions.DownedBoss[226]));
  public static bool downedFowlMorning;
  public static Condition DownedFowlMorning = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFowlMorning"), (Func<bool>) (() => ModConditions.DownedBoss[227]));
  public static bool downedRaveyard;
  public static Condition DownedRaveyard = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRaveyard"), (Func<bool>) (() => ModConditions.DownedBoss[228]));
  public static bool beenToLab;
  public static Condition HasBeenToLab = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToLab"), (Func<bool>) (() => ModConditions.beenToLab));
  public static bool beenToWasteland;
  public static Condition HasBeenToWasteland = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToWasteland"), (Func<bool>) (() => ModConditions.beenToWasteland));
  public static bool reforgedLoaded;
  public static Mod reforgedMod;
  public static bool remnantsLoaded;
  public static Mod remnantsMod;
  public static bool ruptureLoaded;
  public static Mod ruptureMod;
  public static bool secretsOfTheShadowsLoaded;
  public static Mod secretsOfTheShadowsMod;
  public static bool downedPutridPinky;
  public static Condition DownedPutridPinky = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPutridPinky"), (Func<bool>) (() => ModConditions.DownedBoss[229]));
  public static bool downedGlowmoth;
  public static Condition DownedGlowmoth = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlowmoth"), (Func<bool>) (() => ModConditions.DownedBoss[230]));
  public static bool downedPharaohsCurse;
  public static Condition DownedPharaohsCurse = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPharaohsCurse"), (Func<bool>) (() => ModConditions.DownedBoss[231]));
  public static bool downedAdvisor;
  public static Condition DownedAdvisor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAdvisor"), (Func<bool>) (() => ModConditions.DownedBoss[232]));
  public static bool downedPolaris;
  public static Condition DownedPolaris = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPolaris"), (Func<bool>) (() => ModConditions.DownedBoss[233]));
  public static bool downedLux;
  public static Condition DownedLux = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLux"), (Func<bool>) (() => ModConditions.DownedBoss[234]));
  public static bool downedSubspaceSerpent;
  public static Condition DownedSubspaceSerpent = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSubspaceSerpent"), (Func<bool>) (() => ModConditions.DownedBoss[235]));
  public static bool downedNatureConstruct;
  public static Condition DownedNatureConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNatureConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[236]));
  public static bool downedEarthenConstruct;
  public static Condition DownedEarthenConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEarthenConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[237]));
  public static bool downedPermafrostConstruct;
  public static Condition DownedPermafrostConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPermafrostConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[238]));
  public static bool downedTidalConstruct;
  public static Condition DownedTidalConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTidalConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[239]));
  public static bool downedOtherworldlyConstruct;
  public static Condition DownedOtherworldlyConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOtherworldlyConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[240 /*0xF0*/]));
  public static bool downedEvilConstruct;
  public static Condition DownedEvilConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEvilConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[241]));
  public static bool downedInfernoConstruct;
  public static Condition DownedInfernoConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfernoConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[242]));
  public static bool downedChaosConstruct;
  public static Condition DownedChaosConstruct = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChaosConstruct"), (Func<bool>) (() => ModConditions.DownedBoss[243]));
  public static bool downedNatureSpirit;
  public static Condition DownedNatureSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNatureSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[244]));
  public static bool downedEarthenSpirit;
  public static Condition DownedEarthenSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEarthenSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[245]));
  public static bool downedPermafrostSpirit;
  public static Condition DownedPermafrostSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPermafrostSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[246]));
  public static bool downedTidalSpirit;
  public static Condition DownedTidalSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTidalSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[247]));
  public static bool downedOtherworldlySpirit;
  public static Condition DownedOtherworldlySpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOtherworldlySpirit"), (Func<bool>) (() => ModConditions.DownedBoss[248]));
  public static bool downedEvilSpirit;
  public static Condition DownedEvilSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEvilSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[249]));
  public static bool downedInfernoSpirit;
  public static Condition DownedInfernoSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfernoSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[250]));
  public static bool downedChaosSpirit;
  public static Condition DownedChaosSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChaosSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[251]));
  public static bool beenToPyramid;
  public static Condition HasBeenToPyramid = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToPyramid"), (Func<bool>) (() => ModConditions.beenToPyramid));
  public static bool beenToPlanetarium;
  public static Condition HasBeenToPlanetarium = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToPlanetarium"), (Func<bool>) (() => ModConditions.beenToPlanetarium));
  public static bool shadowsOfAbaddonLoaded;
  public static Mod shadowsOfAbaddonMod;
  public static bool downedDecree;
  public static Condition DownedDecree = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDecree"), (Func<bool>) (() => ModConditions.DownedBoss[252]));
  public static bool downedFlamingPumpkin;
  public static Condition DownedFlamingPumpkin = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFlamingPumpkin"), (Func<bool>) (() => ModConditions.DownedBoss[253]));
  public static bool downedZombiePiglinBrute;
  public static Condition DownedZombiePiglinBrute = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedZombiePiglinBrute"), (Func<bool>) (() => ModConditions.DownedBoss[254]));
  public static bool downedJensenTheGrandHarpy;
  public static Condition DownedJensenTheGrandHarpy = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJensenTheGrandHarpy"), (Func<bool>) (() => ModConditions.DownedBoss[(int) byte.MaxValue]));
  public static bool downedAraneas;
  public static Condition DownedAraneas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAraneas"), (Func<bool>) (() => ModConditions.DownedBoss[256 /*0x0100*/]));
  public static bool downedHarpyQueenRaynare;
  public static Condition DownedHarpyQueenRaynare = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHarpyQueenRaynare"), (Func<bool>) (() => ModConditions.DownedBoss[257]));
  public static bool downedPrimordia;
  public static Condition DownedPrimordia = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrimordia"), (Func<bool>) (() => ModConditions.DownedBoss[258]));
  public static bool downedAbaddon;
  public static Condition DownedAbaddon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAbaddon"), (Func<bool>) (() => ModConditions.DownedBoss[259]));
  public static bool downedAraghur;
  public static Condition DownedAraghur = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAraghur"), (Func<bool>) (() => ModConditions.DownedBoss[260]));
  public static bool downedLostSiblings;
  public static Condition DownedLostSiblings = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLostSiblings"), (Func<bool>) (() => ModConditions.DownedBoss[261]));
  public static bool downedErazor;
  public static Condition DownedErazor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedErazor"), (Func<bool>) (() => ModConditions.DownedBoss[262]));
  public static bool downedNihilus;
  public static Condition DownedNihilus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNihilus"), (Func<bool>) (() => ModConditions.DownedBoss[263]));
  public static bool beenToCinderForest;
  public static Condition HasBeenToCinderForest = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCinderForest"), (Func<bool>) (() => ModConditions.beenToCinderForest));
  public static bool sloomeLoaded;
  public static Mod sloomeMod;
  public static bool downedExodygen;
  public static Condition DownedExodygen = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedExodygen"), (Func<bool>) (() => ModConditions.DownedBoss[264]));
  public static bool spiritLoaded;
  public static Mod spiritMod;
  public static bool downedScarabeus;
  public static Condition DownedScarabeus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedScarabeus"), (Func<bool>) (() => ModConditions.DownedBoss[265]));
  public static bool downedMoonJellyWizard;
  public static Condition DownedMoonJellyWizard = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMoonJellyWizard"), (Func<bool>) (() => ModConditions.DownedBoss[266]));
  public static bool downedVinewrathBane;
  public static Condition DownedVinewrathBane = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVinewrathBane"), (Func<bool>) (() => ModConditions.DownedBoss[267]));
  public static bool downedAncientAvian;
  public static Condition DownedAncientAvian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientAvian"), (Func<bool>) (() => ModConditions.DownedBoss[268]));
  public static bool downedStarplateVoyager;
  public static Condition DownedStarplateVoyager = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarplateVoyager"), (Func<bool>) (() => ModConditions.DownedBoss[269]));
  public static bool downedInfernon;
  public static Condition DownedInfernon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfernon"), (Func<bool>) (() => ModConditions.DownedBoss[270]));
  public static bool downedDusking;
  public static Condition DownedDusking = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDusking"), (Func<bool>) (() => ModConditions.DownedBoss[271]));
  public static bool downedAtlas;
  public static Condition DownedAtlas = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAtlas"), (Func<bool>) (() => ModConditions.DownedBoss[272]));
  public bool waitForJellyDeluge;
  public static bool downedJellyDeluge;
  public static Condition DownedJellyDeluge = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedJellyDeluge"), (Func<bool>) (() => ModConditions.DownedBoss[273]));
  public static bool downedTide;
  public static Condition DownedTide = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTide"), (Func<bool>) (() => ModConditions.DownedBoss[274]));
  public bool waitForMysticMoon;
  public static bool downedMysticMoon;
  public static Condition DownedMysticMoon = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMysticMoon"), (Func<bool>) (() => ModConditions.DownedBoss[275]));
  public static bool beenToBriar;
  public static Condition HasBeenToBriar = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToBriar"), (Func<bool>) (() => ModConditions.beenToBriar));
  public static bool beenToSpirit;
  public static Condition HasBeenToSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpirit"), (Func<bool>) (() => ModConditions.beenToSpirit));
  public static bool spookyLoaded;
  public static Mod spookyMod;
  public static bool downedSpookySpirit;
  public static Condition DownedSpookySpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSpookySpirit"), (Func<bool>) (() => ModConditions.DownedBoss[276]));
  public static bool downedRotGourd;
  public static Condition DownedRotGourd = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedRotGourd"), (Func<bool>) (() => ModConditions.DownedBoss[277]));
  public static bool downedMoco;
  public static Condition DownedMoco = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMoco"), (Func<bool>) (() => ModConditions.DownedBoss[278]));
  public static bool downedDaffodil;
  public static Condition DownedDaffodil = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDaffodil"), (Func<bool>) (() => ModConditions.DownedBoss[279]));
  public static bool downedOrroBoro;
  public static bool downedOrro;
  public static bool downedBoro;
  public static Condition DownedOrroBoro = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOrroBoro"), (Func<bool>) (() => ModConditions.DownedBoss[280]));
  public static bool downedBigBone;
  public static Condition DownedBigBone = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBigBone"), (Func<bool>) (() => ModConditions.DownedBoss[281]));
  public static bool beenToSpookyForest;
  public static Condition HasBeenToSpookyForest = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpookyForest"), (Func<bool>) (() => ModConditions.beenToSpookyForest));
  public static bool beenToSpookyUnderground;
  public static Condition HasBeenToSpookyUnderground = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpookyUnderground"), (Func<bool>) (() => ModConditions.beenToSpookyUnderground));
  public static bool beenToEyeValley;
  public static Condition HasBeenToEyeValley = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToEyeValley"), (Func<bool>) (() => ModConditions.beenToEyeValley));
  public static bool beenToSpiderCave;
  public static Condition HasBeenToSpiderCave = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToSpiderCave"), (Func<bool>) (() => ModConditions.beenToSpiderCave));
  public static bool beenToCatacombs;
  public static Condition HasBeenToCatacombs = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCatacombs"), (Func<bool>) (() => ModConditions.beenToCatacombs));
  public static bool beenToCemetery;
  public static Condition HasBeenToCemetery = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToCemetery"), (Func<bool>) (() => ModConditions.beenToCemetery));
  public static bool starlightRiverLoaded;
  public static Mod starlightRiverMod;
  public static bool downedAuroracle;
  public static Condition DownedAuroracle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAuroracle"), (Func<bool>) (() => ModConditions.DownedBoss[282]));
  public static bool downedCeiros;
  public static Condition DownedCeiros = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCeiros"), (Func<bool>) (() => ModConditions.DownedBoss[283]));
  public static bool downedGlassweaver;
  public static Condition DownedGlassweaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGlassweaver"), (Func<bool>) (() => ModConditions.DownedBoss[284]));
  public static bool beenToAuroracleTemple;
  public static Condition HasBeenToAuroracleTemple = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAuroracleTemple"), (Func<bool>) (() => ModConditions.beenToAuroracleTemple));
  public static bool beenToVitricDesert;
  public static Condition HasBeenToVitricDesert = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToVitricDesert"), (Func<bool>) (() => ModConditions.beenToVitricDesert));
  public static bool beenToVitricTemple;
  public static Condition HasBeenToVitricTemple = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToVitricTemple"), (Func<bool>) (() => ModConditions.beenToVitricTemple));
  public static bool starsAboveLoaded;
  public static Mod starsAboveMod;
  public static bool downedVagrantofSpace;
  public static Condition DownedVagrantofSpace = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedVagrantofSpace"), (Func<bool>) (() => ModConditions.downedVagrantofSpace));
  public static bool downedThespian;
  public static Condition DownedThespian = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedThespian"), (Func<bool>) (() => ModConditions.downedThespian));
  public static bool downedCastor;
  public static bool downedPollux;
  public static bool downedDioskouroi;
  public static Condition DownedDioskouroi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDioskouroi"), (Func<bool>) (() => ModConditions.downedDioskouroi));
  public static bool downedNalhaun;
  public static Condition DownedNalhaun = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNalhaun"), (Func<bool>) (() => ModConditions.downedNalhaun));
  public static bool downedStarfarers;
  public static Condition DownedStarfarers = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarfarers"), (Func<bool>) (() => ModConditions.downedStarfarers));
  public static bool downedPenthesilea;
  public static Condition DownedPenthesilea = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPenthesilea"), (Func<bool>) (() => ModConditions.downedPenthesilea));
  public static bool downedArbitration;
  public static Condition DownedArbitration = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedArbitration"), (Func<bool>) (() => ModConditions.downedArbitration));
  public static bool downedWarriorOfLight;
  public static Condition DownedWarriorOfLight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedWarriorOfLight"), (Func<bool>) (() => ModConditions.downedWarriorOfLight));
  public static bool downedTsukiyomi;
  public static Condition DownedTsukiyomi = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTsukiyomi"), (Func<bool>) (() => ModConditions.downedTsukiyomi));
  public static bool stormsAdditionsLoaded;
  public static Mod stormsAdditionsMod;
  public static bool downedAncientHusk;
  public static Condition DownedAncientHusk = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAncientHusk"), (Func<bool>) (() => ModConditions.DownedBoss[294]));
  public static bool downedOverloadedScandrone;
  public static Condition DownedOverloadedScandrone = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedOverloadedScandrone"), (Func<bool>) (() => ModConditions.DownedBoss[295]));
  public static bool downedPainbringer;
  public static Condition DownedPainbringer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPainbringer"), (Func<bool>) (() => ModConditions.DownedBoss[296]));
  public static bool stramsClassesLoaded;
  public static Mod stramsClassesMod;
  public static bool supernovaLoaded;
  public static Mod supernovaMod;
  public static bool downedHarbingerOfAnnihilation;
  public static Condition DownedHarbingerOfAnnihilation = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHarbingerOfAnnihilation"), (Func<bool>) (() => ModConditions.DownedBoss[297]));
  public static bool downedFlyingTerror;
  public static Condition DownedFlyingTerror = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFlyingTerror"), (Func<bool>) (() => ModConditions.DownedBoss[298]));
  public static bool downedStoneMantaRay;
  public static Condition DownedStoneMantaRay = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStoneMantaRay"), (Func<bool>) (() => ModConditions.DownedBoss[299]));
  public static bool downedBloodweaver;
  public static Condition DownedBloodweaver = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBloodweaver"), (Func<bool>) (() => ModConditions.DownedBoss[300]));
  public static bool terrorbornLoaded;
  public static Mod terrorbornMod;
  public static bool downedInfectedIncarnate;
  public static Condition DownedInfectedIncarnate = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedInfectedIncarnate"), (Func<bool>) (() => ModConditions.DownedBoss[301]));
  public static bool downedTidalTitan;
  public static Condition DownedTidalTitan = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedTidalTitan"), (Func<bool>) (() => ModConditions.DownedBoss[302]));
  public static bool downedDunestock;
  public static Condition DownedDunestock = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDunestock"), (Func<bool>) (() => ModConditions.DownedBoss[303]));
  public static bool downedHexedConstructor;
  public static Condition DownedHexedConstructor = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedHexedConstructor"), (Func<bool>) (() => ModConditions.DownedBoss[304]));
  public static bool downedShadowcrawler;
  public static Condition DownedShadowcrawler = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedShadowcrawler"), (Func<bool>) (() => ModConditions.DownedBoss[305]));
  public static bool downedPrototypeI;
  public static Condition DownedPrototypeI = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrototypeI"), (Func<bool>) (() => ModConditions.DownedBoss[306]));
  public static bool thoriumLoaded;
  public static Mod thoriumMod;
  public static bool downedGrandThunderBird;
  public static Condition DownedGrandThunderBird = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGrandThunderBird"), (Func<bool>) (() => ModConditions.downedGrandThunderBird));
  public static bool downedQueenJellyfish;
  public static Condition DownedQueenJellyfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedQueenJellyfish"), (Func<bool>) (() => ModConditions.downedQueenJellyfish));
  public static bool downedViscount;
  public static Condition DownedViscount = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedViscount"), (Func<bool>) (() => ModConditions.downedViscount));
  public static bool downedGraniteEnergyStorm;
  public static Condition DownedGraniteEnergyStorm = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGraniteEnergyStorm"), (Func<bool>) (() => ModConditions.downedGraniteEnergyStorm));
  public static bool downedBuriedChampion;
  public static Condition DownedBuriedChampion = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBuriedChampion"), (Func<bool>) (() => ModConditions.downedBuriedChampion));
  public static bool downedStarScouter;
  public static Condition DownedStarScouter = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStarScouter"), (Func<bool>) (() => ModConditions.downedStarScouter));
  public static bool downedBoreanStrider;
  public static Condition DownedBoreanStrider = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBoreanStrider"), (Func<bool>) (() => ModConditions.downedBoreanStrider));
  public static bool downedFallenBeholder;
  public static Condition DownedFallenBeholder = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedFallenBeholder"), (Func<bool>) (() => ModConditions.downedFallenBeholder));
  public static bool downedLich;
  public static Condition DownedLich = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedLich"), (Func<bool>) (() => ModConditions.downedLich));
  public static bool downedForgottenOne;
  public static Condition DownedForgottenOne = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedForgottenOne"), (Func<bool>) (() => ModConditions.downedForgottenOne));
  public static bool downedPrimordials;
  public static Condition DownedPrimordials = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPrimordials"), (Func<bool>) (() => ModConditions.downedPrimordials));
  public static bool downedPatchWerk;
  public static Condition DownedPatchWerk = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPatchWerk"), (Func<bool>) (() => ModConditions.downedPatchWerk));
  public static bool downedCorpseBloom;
  public static Condition DownedCorpseBloom = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCorpseBloom"), (Func<bool>) (() => ModConditions.downedCorpseBloom));
  public static bool downedIllusionist;
  public static Condition DownedIllusionist = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedIllusionist"), (Func<bool>) (() => ModConditions.downedIllusionist));
  public static bool beenToAquaticDepths;
  public static Condition HasBeenToAquaticDepths = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToAquaticDepths"), (Func<bool>) (() => ModConditions.beenToAquaticDepths));
  public static bool thoriumBossReworkLoaded;
  public static Mod thoriumBossReworkMod;
  public static bool exhaustionDisablerLoaded;
  public static Mod exhaustionDisablerMod;
  public static bool traeLoaded;
  public static Mod traeMod;
  public static bool downedGraniteOvergrowth;
  public static Condition DownedGraniteOvergrowth = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGraniteOvergrowth"), (Func<bool>) (() => ModConditions.DownedBoss[307]));
  public static bool downedBeholder;
  public static Condition DownedBeholder = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedBeholder"), (Func<bool>) (() => ModConditions.DownedBoss[308]));
  public static bool uhtricLoaded;
  public static Mod uhtricMod;
  public static bool downedDredger;
  public static Condition DownedDredger = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDredger"), (Func<bool>) (() => ModConditions.DownedBoss[309]));
  public static bool downedCharcoolSnowman;
  public static Condition DownedCharcoolSnowman = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCharcoolSnowman"), (Func<bool>) (() => ModConditions.DownedBoss[310]));
  public static bool downedCosmicMenace;
  public static Condition DownedCosmicMenace = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedCosmicMenace"), (Func<bool>) (() => ModConditions.DownedBoss[311]));
  public static bool universeOfSwordsLoaded;
  public static Mod universeOfSwordsMod;
  public static bool downedEvilFlyingBlade;
  public static Condition DownedEvilFlyingBlade = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEvilFlyingBlade"), (Func<bool>) (() => ModConditions.DownedBoss[312]));
  public static bool valhallaLoaded;
  public static Mod valhallaMod;
  public static bool downedColossalCarnage;
  public static Condition DownedColossalCarnage = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedColossalCarnage"), (Func<bool>) (() => ModConditions.DownedBoss[313]));
  public static bool downedYurnero;
  public static Condition DownedYurnero = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedYurnero"), (Func<bool>) (() => ModConditions.DownedBoss[314]));
  public static bool verdantLoaded;
  public static Mod verdantMod;
  public static bool beenToVerdant;
  public static Condition HasBeenToVerdant = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.beenToVerdant"), (Func<bool>) (() => ModConditions.beenToVerdant));
  public static bool vitalityLoaded;
  public static Mod vitalityMod;
  public static bool downedStormCloud;
  public static Condition DownedStormCloud = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedStormCloud"), (Func<bool>) (() => ModConditions.DownedBoss[315]));
  public static bool downedGrandAntlion;
  public static Condition DownedGrandAntlion = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGrandAntlion"), (Func<bool>) (() => ModConditions.DownedBoss[316]));
  public static bool downedGemstoneElemental;
  public static Condition DownedGemstoneElemental = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedGemstoneElemental"), (Func<bool>) (() => ModConditions.DownedBoss[317]));
  public static bool downedMoonlightDragonfly;
  public static Condition DownedMoonlightDragonfly = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMoonlightDragonfly"), (Func<bool>) (() => ModConditions.DownedBoss[318]));
  public static bool downedDreadnaught;
  public static Condition DownedDreadnaught = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDreadnaught"), (Func<bool>) (() => ModConditions.DownedBoss[319]));
  public static bool downedMosquitoMonarch;
  public static Condition DownedMosquitoMonarch = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMosquitoMonarch"), (Func<bool>) (() => ModConditions.DownedBoss[320]));
  public static bool downedAnarchulesBeetle;
  public static Condition DownedAnarchulesBeetle = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAnarchulesBeetle"), (Func<bool>) (() => ModConditions.DownedBoss[321]));
  public static bool downedChaosbringer;
  public static Condition DownedChaosbringer = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedChaosbringer"), (Func<bool>) (() => ModConditions.DownedBoss[322]));
  public static bool downedPaladinSpirit;
  public static Condition DownedPaladinSpirit = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedPaladinSpirit"), (Func<bool>) (() => ModConditions.DownedBoss[323]));
  public static bool wayfairContentLoaded;
  public static Mod wayfairContentMod;
  public static bool downedManaflora;
  public static Condition DownedManaflora = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedManaflora"), (Func<bool>) (() => ModConditions.DownedBoss[324]));
  public static bool wrathOfTheGodsLoaded;
  public static Mod wrathOfTheGodsMod;
  public static bool downedNoxus;
  public static Condition DownedNoxus = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNoxus"), (Func<bool>) (() => ModConditions.DownedBoss[325]));
  public static bool downedNamelessDeityOfLight;
  public static Condition DownedNamelessDeityOfLight = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedNamelessDeityOfLight"), (Func<bool>) (() => ModConditions.DownedBoss[326]));
  public static bool zylonLoaded;
  public static Mod zylonMod;
  public static bool downedDirtball;
  public static Condition DownedDirtball = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedDirtball"), (Func<bool>) (() => ModConditions.DownedBoss[327]));
  public static bool downedMetelord;
  public static Condition DownedMetelord = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedMetelord"), (Func<bool>) (() => ModConditions.DownedBoss[328]));
  public static bool downedAdeneb;
  public static Condition DownedAdeneb = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedAdeneb"), (Func<bool>) (() => ModConditions.DownedBoss[329]));
  public static bool downedEldritchJellyfish;
  public static Condition DownedEldritchJellyfish = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedEldritchJellyfish"), (Func<bool>) (() => ModConditions.DownedBoss[330]));
  public static bool downedSaburRex;
  public static Condition DownedSaburRex = new Condition(Language.GetTextValue("Mods.QoLCompendium.ModConditions.downedSaburRex"), (Func<bool>) (() => ModConditions.DownedBoss[331]));
  public static bool[] downedBoss = new bool[Enum.GetValues(typeof (ModConditions.Downed)).Length];

  public static Condition ItemToggled(string displayText, Func<bool> toggle)
  {
    return new Condition(Language.GetTextValue(displayText), toggle);
  }

  public static Recipe GetItemRecipe(
    Func<bool> toggle,
    int itemType,
    int amount = 1,
    string displayText = "")
  {
    Recipe itemRecipe = Recipe.Create(itemType, amount);
    itemRecipe.AddCondition(ModConditions.ItemToggled(displayText, toggle));
    return itemRecipe;
  }

  public static Condition ItemObtained(Item item)
  {
    return new Condition(StringExtensions.FormatWith(Language.GetTextValue("Mods.QoLCompendium.ModConditions.ItemObtained"), new object[1]
    {
      (object) item.ResearchUnlockCount
    }), (Func<bool>) (() => ModConditions.ItemHasBeenOwned[item.type]));
  }

  public static bool[] DownedBoss
  {
    get => ModConditions.downedBoss;
    set => ModConditions.downedBoss = value;
  }

  public virtual void Unload() => ModConditions.DownedBoss = (bool[]) null;

  public virtual void OnWorldLoad() => ModConditions.ResetDowned();

  public virtual void OnWorldUnload() => ModConditions.ResetDowned();

  public virtual void PostUpdateEverything()
  {
    if (Main.netMode != 1 && Main.netMode != 2 && !QoLCompendium.QoLCompendium.mainConfig.RemoveBiomeShopRequirements)
      return;
    ModConditions.beenThroughNight = true;
    ModConditions.beenToPurity = true;
    ModConditions.beenToCavernsOrUnderground = true;
    ModConditions.beenToUnderworld = true;
    ModConditions.beenToSky = true;
    ModConditions.beenToSnow = true;
    ModConditions.beenToDesert = true;
    ModConditions.beenToOcean = true;
    ModConditions.beenToJungle = true;
    ModConditions.beenToMushroom = true;
    ModConditions.beenToCorruption = true;
    ModConditions.beenToCrimson = true;
    ModConditions.beenToHallow = true;
    ModConditions.beenToTemple = true;
    ModConditions.beenToDungeon = true;
    ModConditions.beenToAether = true;
    ModConditions.beenToCrabCrevice = true;
    ModConditions.beenToCrags = true;
    ModConditions.beenToAstral = true;
    ModConditions.beenToSunkenSea = true;
    ModConditions.beenToSulphurSea = true;
    ModConditions.beenToAbyss = true;
    ModConditions.beenToAbyssLayer1 = true;
    ModConditions.beenToAbyssLayer2 = true;
    ModConditions.beenToAbyssLayer3 = true;
    ModConditions.beenToAbyssLayer4 = true;
    ModConditions.beenToAstralBlight = true;
    ModConditions.beenToConfection = true;
    ModConditions.beenToDepths = true;
    ModConditions.beenToJadeLake = true;
    ModConditions.beenToContagion = true;
    ModConditions.beenToDread = true;
    ModConditions.beenToHomewardAbyss = true;
    ModConditions.beenToProfanedGardens = true;
    ModConditions.beenToLunarVeilAbyss = true;
    ModConditions.beenToAcid = true;
    ModConditions.beenToAurelus = true;
    ModConditions.beenToFable = true;
    ModConditions.beenToGovheilCastle = true;
    ModConditions.beenToCathedral = true;
    ModConditions.beenToMarrowSurface = true;
    ModConditions.beenToMorrowUnderground = true;
    ModConditions.beenToSkyFortress = true;
    ModConditions.beenToLab = true;
    ModConditions.beenToWasteland = true;
    ModConditions.beenToPyramid = true;
    ModConditions.beenToPlanetarium = true;
    ModConditions.beenToCinderForest = true;
    ModConditions.beenToBriar = true;
    ModConditions.beenToSpirit = true;
    ModConditions.beenToSpookyForest = true;
    ModConditions.beenToSpookyUnderground = true;
    ModConditions.beenToEyeValley = true;
    ModConditions.beenToSpiderCave = true;
    ModConditions.beenToCatacombs = true;
    ModConditions.beenToCemetery = true;
    ModConditions.beenToAuroracleTemple = true;
    ModConditions.beenToVitricDesert = true;
    ModConditions.beenToVitricTemple = true;
    ModConditions.beenToAquaticDepths = true;
    ModConditions.beenToVerdant = true;
  }

  public virtual void PreUpdatePlayers()
  {
    if (Main.bloodMoon && !this.waitForBloodMoon)
      this.waitForBloodMoon = true;
    if (this.waitForBloodMoon && !Main.bloodMoon && Main.dayTime)
      ModConditions.downedBloodMoon = true;
    if (this.waitForBloodMoon && !Main.bloodMoon && !Main.dayTime)
      this.waitForBloodMoon = false;
    if (Main.eclipse && !this.waitForEclipse)
      this.waitForEclipse = true;
    if (this.waitForEclipse && !Main.eclipse && !Main.dayTime)
      ModConditions.downedEclipse = true;
    if (this.waitForEclipse && !Main.eclipse && Main.dayTime)
      this.waitForEclipse = false;
    if (NPC.downedTowerNebula && NPC.downedTowerSolar && NPC.downedTowerStardust && NPC.downedTowerVortex)
      ModConditions.downedLunarEvent = true;
    if (!Main.dayTime)
      ModConditions.beenThroughNight = true;
    if (Main.LocalPlayer.ZoneForest)
      ModConditions.beenToPurity = true;
    if (Main.LocalPlayer.ZoneNormalCaverns || Main.LocalPlayer.ZoneNormalUnderground)
      ModConditions.beenToCavernsOrUnderground = true;
    if (Main.LocalPlayer.ZoneUnderworldHeight)
      ModConditions.beenToUnderworld = true;
    if (Main.LocalPlayer.ZoneSkyHeight)
      ModConditions.beenToSky = true;
    if (Main.LocalPlayer.ZoneSnow)
      ModConditions.beenToSnow = true;
    if (Main.LocalPlayer.ZoneDesert || Main.LocalPlayer.ZoneUndergroundDesert)
      ModConditions.beenToDesert = true;
    if (Main.LocalPlayer.ZoneBeach)
      ModConditions.beenToOcean = true;
    if (Main.LocalPlayer.ZoneJungle)
      ModConditions.beenToJungle = true;
    if (Main.LocalPlayer.ZoneGlowshroom)
      ModConditions.beenToMushroom = true;
    if (Main.LocalPlayer.ZoneCorrupt)
      ModConditions.beenToCorruption = true;
    if (Main.LocalPlayer.ZoneCrimson)
      ModConditions.beenToCrimson = true;
    if (Main.LocalPlayer.ZoneHallow)
      ModConditions.beenToHallow = true;
    if (Main.LocalPlayer.ZoneLihzhardTemple)
      ModConditions.beenToTemple = true;
    if (Main.LocalPlayer.ZoneDungeon)
      ModConditions.beenToDungeon = true;
    if (Main.LocalPlayer.ZoneShimmer)
      ModConditions.beenToAether = true;
    if (ModConditions.aequusLoaded)
    {
      ModConditions.downedCrabson = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedCrabson"
      });
      ModConditions.downedOmegaStarite = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedOmegaStarite"
      });
      ModConditions.downedDustDevil = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedDustDevil"
      });
      ModConditions.downedRedSprite = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedRedSprite"
      });
      ModConditions.downedSpaceSquid = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedSpaceSquid"
      });
      ModConditions.downedHyperStarite = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedHyperStarite"
      });
      ModConditions.downedUltraStarite = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedUltraStarite"
      });
      ModConditions.downedDemonSiege = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedEventDemon"
      });
      ModConditions.downedGlimmer = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedEventCosmic"
      });
      ModConditions.downedGaleStreams = (bool) ModConditions.aequusMod.Call(new object[1]
      {
        (object) "downedEventAtmosphere"
      });
      ModBiome modBiome;
      if (ModConditions.aequusMod.TryFind<ModBiome>("CrabCreviceBiome", ref modBiome) && Main.LocalPlayer.InModBiome(modBiome))
        ModConditions.beenToCrabCrevice = true;
    }
    if (ModConditions.calamityLoaded)
    {
      ModConditions.downedDesertScourge = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "DesertScourge"
      });
      ModConditions.downedCrabulon = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Crabulon"
      });
      ModConditions.downedHiveMind = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "HiveMind"
      });
      ModConditions.downedPerforators = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Perforator"
      });
      ModConditions.downedSlimeGod = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "SlimeGod"
      });
      ModConditions.downedCryogen = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Cryogen"
      });
      ModConditions.downedAquaticScourge = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "AquaticScourge"
      });
      ModConditions.downedBrimstoneElemental = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "BrimstoneElemental"
      });
      ModConditions.downedCalamitasClone = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "CalamitasClone"
      });
      ModConditions.downedLeviathanAndAnahita = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "AnahitaLeviathan"
      });
      ModConditions.downedAstrumAureus = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "AstrumAureus"
      });
      ModConditions.downedPlaguebringerGoliath = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "PlaguebringerGoliath"
      });
      ModConditions.downedRavager = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Ravager"
      });
      ModConditions.downedAstrumDeus = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "AstrumDeus"
      });
      ModConditions.downedProfanedGuardians = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Guardians"
      });
      ModConditions.downedDragonfolly = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Dragonfolly"
      });
      ModConditions.downedProvidence = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Providence"
      });
      ModConditions.downedStormWeaver = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "StormWeaver"
      });
      ModConditions.downedCeaselessVoid = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "CeaselessVoid"
      });
      ModConditions.downedSignus = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Signus"
      });
      ModConditions.downedPolterghast = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Polterghast"
      });
      ModConditions.downedOldDuke = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "OldDuke"
      });
      ModConditions.downedDevourerOfGods = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "DevourerOfGods"
      });
      ModConditions.downedYharon = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "Yharon"
      });
      ModConditions.downedExoMechs = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "ExoMechs"
      });
      ModConditions.downedSupremeCalamitas = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "SupremeCalamitas"
      });
      ModConditions.downedGiantClam = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "GiantClam"
      });
      ModConditions.downedCragmawMire = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "cragmawmire"
      });
      ModConditions.downedGreatSandShark = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "GreatSandShark"
      });
      ModConditions.downedMauler = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "mauler"
      });
      ModConditions.downedNuclearTerror = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "nuclearterror"
      });
      ModConditions.downedEidolonWyrm = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "primordialwyrm"
      });
      ModConditions.downedAcidRain1 = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "acidraineoc"
      });
      ModConditions.downedAcidRain2 = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "acidrainscourge"
      });
      ModConditions.downedBossRush = (bool) ModConditions.calamityMod.Call(new object[2]
      {
        (object) "GetBossDowned",
        (object) "bossrush"
      });
      ModBiome modBiome1;
      if (ModConditions.calamityMod.TryFind<ModBiome>("AstralInfectionBiome", ref modBiome1) && Main.LocalPlayer.InModBiome(modBiome1))
        ModConditions.beenToAstral = true;
      ModBiome modBiome2;
      if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer1Biome", ref modBiome2) && Main.LocalPlayer.InModBiome(modBiome2))
      {
        ModConditions.beenToAbyss = true;
        ModConditions.beenToAbyssLayer1 = true;
      }
      ModBiome modBiome3;
      if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer2Biome", ref modBiome3) && Main.LocalPlayer.InModBiome(modBiome3))
      {
        ModConditions.beenToAbyss = true;
        ModConditions.beenToAbyssLayer2 = true;
      }
      ModBiome modBiome4;
      if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer3Biome", ref modBiome4) && Main.LocalPlayer.InModBiome(modBiome4))
      {
        ModConditions.beenToAbyss = true;
        ModConditions.beenToAbyssLayer3 = true;
      }
      ModBiome modBiome5;
      if (ModConditions.calamityMod.TryFind<ModBiome>("AbyssLayer4Biome", ref modBiome5) && Main.LocalPlayer.InModBiome(modBiome5))
      {
        ModConditions.beenToAbyss = true;
        ModConditions.beenToAbyssLayer4 = true;
      }
      ModBiome modBiome6;
      if (ModConditions.calamityMod.TryFind<ModBiome>("BrimstoneCragsBiome", ref modBiome6) && Main.LocalPlayer.InModBiome(modBiome6))
        ModConditions.beenToCrags = true;
      ModBiome modBiome7;
      if (ModConditions.calamityMod.TryFind<ModBiome>("SulphurousSeaBiome", ref modBiome7) && Main.LocalPlayer.InModBiome(modBiome7))
        ModConditions.beenToSulphurSea = true;
      ModBiome modBiome8;
      if (ModConditions.calamityMod.TryFind<ModBiome>("SunkenSeaBiome", ref modBiome8) && Main.LocalPlayer.InModBiome(modBiome8))
        ModConditions.beenToSunkenSea = true;
    }
    ModBiome modBiome9;
    if (ModConditions.calamityVanitiesLoaded && ModConditions.calamityVanitiesMod.TryFind<ModBiome>("AstralBlight", ref modBiome9) && Main.LocalPlayer.InModBiome(modBiome9))
      ModConditions.beenToAstralBlight = true;
    ModBiome modBiome10;
    ModBiome modBiome11;
    ModBiome modBiome12;
    ModBiome modBiome13;
    ModBiome modBiome14;
    ModBiome modBiome15;
    if (ModConditions.confectionRebakedLoaded && (ModConditions.confectionRebakedMod.TryFind<ModBiome>("ConfectionBiome", ref modBiome10) && Main.LocalPlayer.InModBiome(modBiome10) || ModConditions.confectionRebakedMod.TryFind<ModBiome>("ConfectionUndergroundBiome", ref modBiome11) && Main.LocalPlayer.InModBiome(modBiome11) || ModConditions.confectionRebakedMod.TryFind<ModBiome>("IceConfectionSurfaceBiome", ref modBiome12) && Main.LocalPlayer.InModBiome(modBiome12) || ModConditions.confectionRebakedMod.TryFind<ModBiome>("IceConfectionUndergroundBiome", ref modBiome13) && Main.LocalPlayer.InModBiome(modBiome13) || ModConditions.confectionRebakedMod.TryFind<ModBiome>("SandConfectionSurfaceBiome", ref modBiome14) && Main.LocalPlayer.InModBiome(modBiome14) || ModConditions.confectionRebakedMod.TryFind<ModBiome>("SandConfectionUndergroundBiome", ref modBiome15) && Main.LocalPlayer.InModBiome(modBiome15)))
    {
      ModConditions.beenToConfection = true;
      ModConditions.beenToHallow = true;
    }
    ModBiome modBiome16;
    if (ModConditions.depthsLoaded && ModConditions.depthsMod.TryFind<ModBiome>("DepthsBiome", ref modBiome16) && Main.LocalPlayer.InModBiome(modBiome16))
    {
      ModConditions.beenToDepths = true;
      ModConditions.beenToUnderworld = true;
    }
    ModBiome modBiome17;
    if (ModConditions.everjadeLoaded && ModConditions.everjadeMod.TryFind<ModBiome>("JadeLakeBiome", ref modBiome17) && Main.LocalPlayer.InModBiome(modBiome17))
      ModConditions.beenToJadeLake = true;
    ModBiome modBiome18;
    ModBiome modBiome19;
    ModBiome modBiome20;
    ModBiome modBiome21;
    if (ModConditions.exxoAvalonOriginsLoaded && (ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("Contagion", ref modBiome18) && Main.LocalPlayer.InModBiome(modBiome18) || ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("UndergroundContagion", ref modBiome19) && Main.LocalPlayer.InModBiome(modBiome19) || ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("ContagionDesert", ref modBiome20) && Main.LocalPlayer.InModBiome(modBiome20) || ModConditions.exxoAvalonOriginsMod.TryFind<ModBiome>("ContagionCaveDesert", ref modBiome21) && Main.LocalPlayer.InModBiome(modBiome21)))
      ModConditions.beenToContagion = true;
    ModBiome modBiome22;
    ModBiome modBiome23;
    if (ModConditions.fracturesOfPenumbraLoaded && (ModConditions.fracturesOfPenumbraMod.TryFind<ModBiome>("DreadSurfaceBiome", ref modBiome22) && Main.LocalPlayer.InModBiome(modBiome22) || ModConditions.fracturesOfPenumbraMod.TryFind<ModBiome>("DreadUndergroundBiome", ref modBiome23) && Main.LocalPlayer.InModBiome(modBiome23)))
      ModConditions.beenToDread = true;
    ModBiome modBiome24;
    if (ModConditions.homewardJourneyLoaded && ModConditions.homewardJourneyMod.TryFind<ModBiome>("AbyssUndergroundBiome", ref modBiome24) && Main.LocalPlayer.InModBiome(modBiome24))
      ModConditions.beenToHomewardAbyss = true;
    ModBiome modBiome25;
    if (ModConditions.infernumLoaded && ModConditions.infernumMod.TryFind<ModBiome>("ProfanedTempleBiome", ref modBiome25) && Main.LocalPlayer.InModBiome(modBiome25))
      ModConditions.beenToProfanedGardens = true;
    if (ModConditions.lunarVeilLoaded)
    {
      ModBiome modBiome26;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("AbyssBiome", ref modBiome26) && Main.LocalPlayer.InModBiome(modBiome26))
        ModConditions.beenToLunarVeilAbyss = true;
      ModBiome modBiome27;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("AcidBiome", ref modBiome27) && Main.LocalPlayer.InModBiome(modBiome27))
        ModConditions.beenToAcid = true;
      ModBiome modBiome28;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("AurelusBiome", ref modBiome28) && Main.LocalPlayer.InModBiome(modBiome28))
        ModConditions.beenToAurelus = true;
      ModBiome modBiome29;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("FableBiome", ref modBiome29) && Main.LocalPlayer.InModBiome(modBiome29))
        ModConditions.beenToFable = true;
      ModBiome modBiome30;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("GovheilCastle", ref modBiome30) && Main.LocalPlayer.InModBiome(modBiome30))
        ModConditions.beenToGovheilCastle = true;
      ModBiome modBiome31;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("CathedralBiome", ref modBiome31) && Main.LocalPlayer.InModBiome(modBiome31))
        ModConditions.beenToCathedral = true;
      ModBiome modBiome32;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("MarrowSurfaceBiome", ref modBiome32) && Main.LocalPlayer.InModBiome(modBiome32))
        ModConditions.beenToMarrowSurface = true;
      ModBiome modBiome33;
      if (ModConditions.lunarVeilMod.TryFind<ModBiome>("MorrowUndergroundBiome", ref modBiome33) && Main.LocalPlayer.InModBiome(modBiome33))
        ModConditions.beenToMorrowUnderground = true;
    }
    ModBiome modBiome34;
    if (ModConditions.qwertyLoaded && ModConditions.qwertyMod.TryFind<ModBiome>("FortressBiome", ref modBiome34) && Main.LocalPlayer.InModBiome(modBiome34))
      ModConditions.beenToSkyFortress = true;
    if (ModConditions.redemptionLoaded)
    {
      ModBiome modBiome35;
      if (ModConditions.redemptionMod.TryFind<ModBiome>("LabBiome", ref modBiome35) && Main.LocalPlayer.InModBiome(modBiome35))
        ModConditions.beenToLab = true;
      ModBiome modBiome36;
      if (ModConditions.redemptionMod.TryFind<ModBiome>("WastelandPurityBiome", ref modBiome36) && Main.LocalPlayer.InModBiome(modBiome36))
        ModConditions.beenToWasteland = true;
    }
    if (ModConditions.secretsOfTheShadowsLoaded)
    {
      ModBiome modBiome37;
      if (ModConditions.secretsOfTheShadowsMod.TryFind<ModBiome>("PyramidBiome", ref modBiome37) && Main.LocalPlayer.InModBiome(modBiome37))
        ModConditions.beenToPyramid = true;
      ModBiome modBiome38;
      if (ModConditions.secretsOfTheShadowsMod.TryFind<ModBiome>("PlanetariumBiome", ref modBiome38) && Main.LocalPlayer.InModBiome(modBiome38))
        ModConditions.beenToPlanetarium = true;
    }
    ModBiome modBiome39;
    ModBiome modBiome40;
    ModBiome modBiome41;
    if (ModConditions.shadowsOfAbaddonLoaded && (ModConditions.shadowsOfAbaddonMod.TryFind<ModBiome>("CinderDesertBiome", ref modBiome39) && Main.LocalPlayer.InModBiome(modBiome39) || ModConditions.shadowsOfAbaddonMod.TryFind<ModBiome>("CinderForestBiome", ref modBiome40) && Main.LocalPlayer.InModBiome(modBiome40) || ModConditions.shadowsOfAbaddonMod.TryFind<ModBiome>("CinderForestUndergroundBiome", ref modBiome41) && Main.LocalPlayer.InModBiome(modBiome41)))
      ModConditions.beenToCinderForest = true;
    if (ModConditions.spiritLoaded)
    {
      ModBiome modBiome42;
      ModBiome modBiome43;
      if (ModConditions.spiritMod.TryFind<ModBiome>("BriarSurfaceBiome", ref modBiome42) && Main.LocalPlayer.InModBiome(modBiome42) || ModConditions.spiritMod.TryFind<ModBiome>("BriarUndergroundBiome", ref modBiome43) && Main.LocalPlayer.InModBiome(modBiome43))
        ModConditions.beenToBriar = true;
      ModBiome modBiome44;
      ModBiome modBiome45;
      if (ModConditions.spiritMod.TryFind<ModBiome>("SpiritSurfaceBiome", ref modBiome44) && Main.LocalPlayer.InModBiome(modBiome44) || ModConditions.spiritMod.TryFind<ModBiome>("SpiritUndergroundBiome", ref modBiome45) && Main.LocalPlayer.InModBiome(modBiome45))
        ModConditions.beenToSpirit = true;
    }
    if (ModConditions.spookyLoaded)
    {
      ModBiome modBiome46;
      if (ModConditions.spookyMod.TryFind<ModBiome>("SpookyBiome", ref modBiome46) && Main.LocalPlayer.InModBiome(modBiome46))
        ModConditions.beenToSpookyForest = true;
      ModBiome modBiome47;
      if (ModConditions.spookyMod.TryFind<ModBiome>("SpookyBiomeUg", ref modBiome47) && Main.LocalPlayer.InModBiome(modBiome47))
        ModConditions.beenToSpookyUnderground = true;
      ModBiome modBiome48;
      if (ModConditions.spookyMod.TryFind<ModBiome>("SpookyHellBiome", ref modBiome48) && Main.LocalPlayer.InModBiome(modBiome48))
        ModConditions.beenToEyeValley = true;
      ModBiome modBiome49;
      if (ModConditions.spookyMod.TryFind<ModBiome>("SpiderCaveBiome", ref modBiome49) && Main.LocalPlayer.InModBiome(modBiome49))
        ModConditions.beenToSpiderCave = true;
      ModBiome modBiome50;
      if (ModConditions.spookyMod.TryFind<ModBiome>("CatacombBiome", ref modBiome50) && Main.LocalPlayer.InModBiome(modBiome50))
        ModConditions.beenToCatacombs = true;
      ModBiome modBiome51;
      if (ModConditions.spookyMod.TryFind<ModBiome>("CemeteryBiome", ref modBiome51) && Main.LocalPlayer.InModBiome(modBiome51))
        ModConditions.beenToCemetery = true;
    }
    if (ModConditions.starlightRiverLoaded)
    {
      ModBiome modBiome52;
      if (ModConditions.starlightRiverMod.TryFind<ModBiome>("PermafrostTempleBiome", ref modBiome52) && Main.LocalPlayer.InModBiome(modBiome52))
        ModConditions.beenToAuroracleTemple = true;
      ModBiome modBiome53;
      if (ModConditions.starlightRiverMod.TryFind<ModBiome>("VitricDesertBiome", ref modBiome53) && Main.LocalPlayer.InModBiome(modBiome53))
        ModConditions.beenToVitricDesert = true;
      ModBiome modBiome54;
      if (ModConditions.starlightRiverMod.TryFind<ModBiome>("VitricTempleBiome", ref modBiome54) && Main.LocalPlayer.InModBiome(modBiome54))
        ModConditions.beenToVitricTemple = true;
    }
    if (ModConditions.starsAboveLoaded)
    {
      ModConditions.downedVagrantofSpace = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedVagrant",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedThespian = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedThespian",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedDioskouroi = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedDioskouroi",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedNalhaun = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedNalhaun",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedStarfarers = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedStarfarers",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedPenthesilea = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedPenthesilea",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedArbitration = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedArbitration",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedWarriorOfLight = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedWarriorOfLight",
        (object) ((ModType) this).Mod
      });
      ModConditions.downedTsukiyomi = (bool) ModConditions.starsAboveMod.Call(new object[2]
      {
        (object) "downedTsukiyomi",
        (object) ((ModType) this).Mod
      });
      if (ModConditions.downedCastor && ModConditions.downedPollux)
        ModConditions.downedDioskouroi = true;
    }
    if (ModConditions.thoriumLoaded)
    {
      ModConditions.downedGrandThunderBird = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "TheGrandThunderBird"
      });
      ModConditions.downedQueenJellyfish = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "QueenJellyfish"
      });
      ModConditions.downedViscount = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "Viscount"
      });
      ModConditions.downedGraniteEnergyStorm = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "GraniteEnergyStorm"
      });
      ModConditions.downedBuriedChampion = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "BuriedChampion"
      });
      ModConditions.downedStarScouter = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "StarScouter"
      });
      ModConditions.downedBoreanStrider = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "BoreanStrider"
      });
      ModConditions.downedFallenBeholder = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "FallenBeholder"
      });
      ModConditions.downedLich = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "Lich"
      });
      ModConditions.downedForgottenOne = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "ForgottenOne"
      });
      ModConditions.downedPrimordials = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "ThePrimordials"
      });
      ModConditions.downedPatchWerk = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "PatchWerk"
      });
      ModConditions.downedCorpseBloom = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "CorpseBloom"
      });
      ModConditions.downedIllusionist = (bool) ModConditions.thoriumMod.Call(new object[2]
      {
        (object) "GetDownedBoss",
        (object) "Illusionist"
      });
      ModBiome modBiome55;
      if (ModConditions.thoriumMod.TryFind<ModBiome>("DepthsBiome", ref modBiome55) && Main.LocalPlayer.InModBiome(modBiome55))
        ModConditions.beenToAquaticDepths = true;
    }
    ModBiome modBiome56;
    if (ModConditions.verdantLoaded && ModConditions.verdantMod.TryFind<ModBiome>("VerdantBiome", ref modBiome56) && Main.LocalPlayer.InModBiome(modBiome56))
      ModConditions.beenToVerdant = true;
    if (!ModConditions.wrathOfTheGodsLoaded)
      return;
    ModConditions.downedNoxus = (bool) ModConditions.wrathOfTheGodsMod.Call(new object[2]
    {
      (object) "GetBossDefeated",
      (object) "noxus"
    });
    ModConditions.downedNamelessDeityOfLight = (bool) ModConditions.wrathOfTheGodsMod.Call(new object[2]
    {
      (object) "GetBossDefeated",
      (object) "namelessdeity"
    });
  }

  public virtual void SaveWorldData(TagCompound tag)
  {
    tag.Add("downedBloodMoon", (object) ModConditions.downedBloodMoon);
    tag.Add("downedEclipse", (object) ModConditions.downedEclipse);
    tag.Add("downedLunarEvent", (object) ModConditions.downedLunarEvent);
    tag.Add("beenThroughNight", (object) ModConditions.beenThroughNight);
    tag.Add("beenToPurity", (object) ModConditions.beenToPurity);
    tag.Add("beenToCavernsOrUnderground", (object) ModConditions.beenToCavernsOrUnderground);
    tag.Add("beenToUnderworld", (object) ModConditions.beenToUnderworld);
    tag.Add("beenToSky", (object) ModConditions.beenToSky);
    tag.Add("beenToSnow", (object) ModConditions.beenToSnow);
    tag.Add("beenToDesert", (object) ModConditions.beenToDesert);
    tag.Add("beenToOcean", (object) ModConditions.beenToOcean);
    tag.Add("beenToJungle", (object) ModConditions.beenToJungle);
    tag.Add("beenToMushroom", (object) ModConditions.beenToMushroom);
    tag.Add("beenToCorruption", (object) ModConditions.beenToCorruption);
    tag.Add("beenToCrimson", (object) ModConditions.beenToCrimson);
    tag.Add("beenToHallow", (object) ModConditions.beenToHallow);
    tag.Add("beenToTemple", (object) ModConditions.beenToTemple);
    tag.Add("beenToDungeon", (object) ModConditions.beenToDungeon);
    tag.Add("beenToAether", (object) ModConditions.beenToAether);
    tag.Add("talkedToSkeletonMerchant", (object) ModConditions.talkedToSkeletonMerchant);
    tag.Add("talkedToTravelingMerchant", (object) ModConditions.talkedToTravelingMerchant);
    tag.Add("beenToCrabCrevice", (object) ModConditions.beenToCrabCrevice);
    tag.Add("beenToCrags", (object) ModConditions.beenToCrags);
    tag.Add("beenToAstral", (object) ModConditions.beenToAstral);
    tag.Add("beenToSunkenSea", (object) ModConditions.beenToSunkenSea);
    tag.Add("beenToSulphurSea", (object) ModConditions.beenToSulphurSea);
    tag.Add("beenToAbyss", (object) ModConditions.beenToAbyss);
    tag.Add("beenToAbyssLayer1", (object) ModConditions.beenToAbyssLayer1);
    tag.Add("beenToAbyssLayer2", (object) ModConditions.beenToAbyssLayer2);
    tag.Add("beenToAbyssLayer3", (object) ModConditions.beenToAbyssLayer3);
    tag.Add("beenToAbyssLayer4", (object) ModConditions.beenToAbyssLayer4);
    tag.Add("beenToAstralBlight", (object) ModConditions.beenToAstralBlight);
    tag.Add("beenToDepths", (object) ModConditions.beenToDepths);
    tag.Add("beenToJadeLake", (object) ModConditions.beenToJadeLake);
    tag.Add("beenToContagion", (object) ModConditions.beenToContagion);
    tag.Add("beenToDread", (object) ModConditions.beenToDread);
    tag.Add("beenToHomewardAbyss", (object) ModConditions.beenToHomewardAbyss);
    tag.Add("beenToProfanedGardens", (object) ModConditions.beenToProfanedGardens);
    tag.Add("beenToLunarVeilAbyss", (object) ModConditions.beenToLunarVeilAbyss);
    tag.Add("beenToAcid", (object) ModConditions.beenToAcid);
    tag.Add("beenToAurelus", (object) ModConditions.beenToAurelus);
    tag.Add("beenToFable", (object) ModConditions.beenToFable);
    tag.Add("beenToGovheilCastle", (object) ModConditions.beenToGovheilCastle);
    tag.Add("beenToCathedral", (object) ModConditions.beenToCathedral);
    tag.Add("beenToMarrowSurface", (object) ModConditions.beenToMarrowSurface);
    tag.Add("beenToMorrowUnderground", (object) ModConditions.beenToMorrowUnderground);
    tag.Add("beenToSkyFortress", (object) ModConditions.beenToSkyFortress);
    tag.Add("beenToLab", (object) ModConditions.beenToLab);
    tag.Add("beenToWasteland", (object) ModConditions.beenToWasteland);
    tag.Add("beenToPyramid", (object) ModConditions.beenToPyramid);
    tag.Add("beenToPlanetarium", (object) ModConditions.beenToPlanetarium);
    tag.Add("beenToCinderForest", (object) ModConditions.beenToCinderForest);
    tag.Add("beenToBriar", (object) ModConditions.beenToBriar);
    tag.Add("beenToSpirit", (object) ModConditions.beenToSpirit);
    tag.Add("beenToSpookyForest", (object) ModConditions.beenToSpookyForest);
    tag.Add("beenToSpookyUnderground", (object) ModConditions.beenToSpookyUnderground);
    tag.Add("beenToEyeValley", (object) ModConditions.beenToEyeValley);
    tag.Add("beenToSpiderCave", (object) ModConditions.beenToSpiderCave);
    tag.Add("beenToCatacombs", (object) ModConditions.beenToCatacombs);
    tag.Add("beenToCemetery", (object) ModConditions.beenToCemetery);
    tag.Add("beenToAuroracleTemple", (object) ModConditions.beenToAuroracleTemple);
    tag.Add("beenToVitricDesert", (object) ModConditions.beenToVitricDesert);
    tag.Add("beenToVitricTemple", (object) ModConditions.beenToVitricTemple);
    tag.Add("beenToAquaticDepths", (object) ModConditions.beenToAquaticDepths);
    tag.Add("beenToVerdant", (object) ModConditions.beenToVerdant);
    List<string> stringList1 = new List<string>();
    for (int index = 0; index < ModConditions.DownedBoss.Length; ++index)
    {
      if (ModConditions.DownedBoss[index])
        stringList1.Add("QoLCdownedBoss" + index.ToString());
    }
    tag.Add("QoLCdowned", (object) stringList1);
    List<string> stringList2 = new List<string>();
    for (int index = 0; index < ModConditions.ItemHasBeenOwned.Length; ++index)
    {
      if (ModConditions.ItemHasBeenOwned[index])
      {
        if (index > (int) ItemID.Count)
        {
          ModItem modItem = ItemLoader.GetItem(index);
          switch (modItem)
          {
            case null:
            case null:
              continue;
            default:
              stringList2.Add(((ModType) modItem).FullName ?? "");
              continue;
          }
        }
        else
          stringList2.Add($"{index}");
      }
    }
    tag.Add("OwnedItemsList", (object) stringList2);
  }

  public virtual void LoadWorldData(TagCompound tag)
  {
    ModConditions.downedBloodMoon = tag.Get<bool>("downedBloodMoon");
    ModConditions.downedEclipse = tag.Get<bool>("downedEclipse");
    ModConditions.downedLunarEvent = tag.Get<bool>("downedLunarEvent");
    ModConditions.beenThroughNight = tag.Get<bool>("beenThroughNight");
    ModConditions.beenToPurity = tag.Get<bool>("beenToPurity");
    ModConditions.beenToCavernsOrUnderground = tag.Get<bool>("beenToCavernsOrUnderground");
    ModConditions.beenToUnderworld = tag.Get<bool>("beenToUnderworld");
    ModConditions.beenToSky = tag.Get<bool>("beenToSky");
    ModConditions.beenToSnow = tag.Get<bool>("beenToSnow");
    ModConditions.beenToDesert = tag.Get<bool>("beenToDesert");
    ModConditions.beenToOcean = tag.Get<bool>("beenToOcean");
    ModConditions.beenToJungle = tag.Get<bool>("beenToJungle");
    ModConditions.beenToMushroom = tag.Get<bool>("beenToMushroom");
    ModConditions.beenToCorruption = tag.Get<bool>("beenToCorruption");
    ModConditions.beenToCrimson = tag.Get<bool>("beenToCrimson");
    ModConditions.beenToHallow = tag.Get<bool>("beenToHallow");
    ModConditions.beenToTemple = tag.Get<bool>("beenToTemple");
    ModConditions.beenToDungeon = tag.Get<bool>("beenToDungeon");
    ModConditions.beenToAether = tag.Get<bool>("beenToAether");
    ModConditions.talkedToSkeletonMerchant = tag.Get<bool>("talkedToSkeletonMerchant");
    ModConditions.talkedToTravelingMerchant = tag.Get<bool>("talkedToTravelingMerchant");
    ModConditions.beenToCrabCrevice = tag.Get<bool>("beenToCrabCrevice");
    ModConditions.beenToCrags = tag.Get<bool>("beenToCrags");
    ModConditions.beenToAstral = tag.Get<bool>("beenToAstral");
    ModConditions.beenToSunkenSea = tag.Get<bool>("beenToSunkenSea");
    ModConditions.beenToSulphurSea = tag.Get<bool>("beenToSulphurSea");
    ModConditions.beenToAbyss = tag.Get<bool>("beenToAbyss");
    ModConditions.beenToAbyssLayer1 = tag.Get<bool>("beenToAbyssLayer1");
    ModConditions.beenToAbyssLayer2 = tag.Get<bool>("beenToAbyssLayer2");
    ModConditions.beenToAbyssLayer3 = tag.Get<bool>("beenToAbyssLayer3");
    ModConditions.beenToAbyssLayer4 = tag.Get<bool>("beenToAbyssLayer4");
    ModConditions.beenToAstralBlight = tag.Get<bool>("beenToAstralBlight");
    ModConditions.beenToDepths = tag.Get<bool>("beenToDepths");
    ModConditions.beenToJadeLake = tag.Get<bool>("beenToJadeLake");
    ModConditions.beenToContagion = tag.Get<bool>("beenToContagion");
    ModConditions.beenToDread = tag.Get<bool>("beenToDread");
    ModConditions.beenToHomewardAbyss = tag.Get<bool>("beenToHomewardAbyss");
    ModConditions.beenToProfanedGardens = tag.Get<bool>("beenToProfanedGardens");
    ModConditions.beenToLunarVeilAbyss = tag.Get<bool>("beenToLunarVeilAbyss");
    ModConditions.beenToAcid = tag.Get<bool>("beenToAcid");
    ModConditions.beenToAurelus = tag.Get<bool>("beenToAurelus");
    ModConditions.beenToFable = tag.Get<bool>("beenToFable");
    ModConditions.beenToGovheilCastle = tag.Get<bool>("beenToGovheilCastle");
    ModConditions.beenToCathedral = tag.Get<bool>("beenToCathedral");
    ModConditions.beenToMarrowSurface = tag.Get<bool>("beenToMarrowSurface");
    ModConditions.beenToMorrowUnderground = tag.Get<bool>("beenToMorrowUnderground");
    ModConditions.beenToSkyFortress = tag.Get<bool>("beenToSkyFortress");
    ModConditions.beenToLab = tag.Get<bool>("beenToLab");
    ModConditions.beenToWasteland = tag.Get<bool>("beenToWasteland");
    ModConditions.beenToPyramid = tag.Get<bool>("beenToPyramid");
    ModConditions.beenToPlanetarium = tag.Get<bool>("beenToPlanetarium");
    ModConditions.beenToCinderForest = tag.Get<bool>("beenToCinderForest");
    ModConditions.beenToBriar = tag.Get<bool>("beenToBriar");
    ModConditions.beenToSpirit = tag.Get<bool>("beenToSpirit");
    ModConditions.beenToSpookyForest = tag.Get<bool>("beenToSpookyForest");
    ModConditions.beenToSpookyUnderground = tag.Get<bool>("beenToSpookyUnderground");
    ModConditions.beenToEyeValley = tag.Get<bool>("beenToEyeValley");
    ModConditions.beenToSpiderCave = tag.Get<bool>("beenToSpiderCave");
    ModConditions.beenToCatacombs = tag.Get<bool>("beenToCatacombs");
    ModConditions.beenToCemetery = tag.Get<bool>("beenToCemetery");
    ModConditions.beenToAuroracleTemple = tag.Get<bool>("beenToAuroracleTemple");
    ModConditions.beenToVitricDesert = tag.Get<bool>("beenToVitricDesert");
    ModConditions.beenToVitricTemple = tag.Get<bool>("beenToVitricTemple");
    ModConditions.beenToAquaticDepths = tag.Get<bool>("beenToAquaticDepths");
    ModConditions.beenToVerdant = tag.Get<bool>("beenToVerdant");
    IList<string> list = tag.GetList<string>("QoLCdowned");
    for (int index = 0; index < ModConditions.DownedBoss.Length; ++index)
      ModConditions.DownedBoss[index] = (list.Contains($"QoLCdownedBoss{index}") ? 1 : 0) != 0;
    ModConditions.ItemHasBeenOwned = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
    foreach (string s in (IEnumerable<string>) tag.GetList<string>("OwnedItemsList"))
    {
      int result;
      if (int.TryParse(s, out result) && result < (int) ItemID.Count)
      {
        ModConditions.ItemHasBeenOwned[result] = true;
      }
      else
      {
        ModItem modItem;
        if (ModContent.TryFind<ModItem>(s, ref modItem))
          ModConditions.ItemHasBeenOwned[modItem.Type] = true;
      }
    }
  }

  public virtual void NetSend(BinaryWriter writer)
  {
    BinaryWriter binaryWriter1 = writer;
    BitsByte bitsByte1 = new BitsByte();
    ((BitsByte) ref bitsByte1)[0] = ModConditions.downedDreadnautilus;
    ((BitsByte) ref bitsByte1)[1] = ModConditions.downedMartianSaucer;
    ((BitsByte) ref bitsByte1)[2] = ModConditions.downedBloodMoon;
    ((BitsByte) ref bitsByte1)[3] = ModConditions.downedEclipse;
    ((BitsByte) ref bitsByte1)[4] = ModConditions.downedLunarEvent;
    int num1 = (int) BitsByte.op_Implicit(bitsByte1);
    binaryWriter1.Write((byte) num1);
    BinaryWriter binaryWriter2 = writer;
    BitsByte bitsByte2 = new BitsByte();
    ((BitsByte) ref bitsByte2)[0] = ModConditions.downedSlayerOfEvil;
    ((BitsByte) ref bitsByte2)[1] = ModConditions.downedSATLA;
    ((BitsByte) ref bitsByte2)[2] = ModConditions.downedDrFetus;
    ((BitsByte) ref bitsByte2)[3] = ModConditions.downedSlimesHope;
    ((BitsByte) ref bitsByte2)[4] = ModConditions.downedPoliticianSlime;
    ((BitsByte) ref bitsByte2)[5] = ModConditions.downedAncientTrio;
    ((BitsByte) ref bitsByte2)[6] = ModConditions.downedLavalGolem;
    ((BitsByte) ref bitsByte2)[7] = ModConditions.downedAntony;
    int num2 = (int) BitsByte.op_Implicit(bitsByte2);
    binaryWriter2.Write((byte) num2);
    BinaryWriter binaryWriter3 = writer;
    BitsByte bitsByte3 = new BitsByte();
    ((BitsByte) ref bitsByte3)[0] = ModConditions.downedBunnyZeppelin;
    ((BitsByte) ref bitsByte3)[1] = ModConditions.downedOkiku;
    ((BitsByte) ref bitsByte3)[2] = ModConditions.downedHarpyAirforce;
    ((BitsByte) ref bitsByte3)[3] = ModConditions.downedIsaac;
    ((BitsByte) ref bitsByte3)[4] = ModConditions.downedAncientGuardian;
    ((BitsByte) ref bitsByte3)[5] = ModConditions.downedHeroicSlime;
    ((BitsByte) ref bitsByte3)[6] = ModConditions.downedHoloSlime;
    ((BitsByte) ref bitsByte3)[7] = ModConditions.downedSecurityBot;
    int num3 = (int) BitsByte.op_Implicit(bitsByte3);
    binaryWriter3.Write((byte) num3);
    BinaryWriter binaryWriter4 = writer;
    BitsByte bitsByte4 = new BitsByte();
    ((BitsByte) ref bitsByte4)[0] = ModConditions.downedUndeadChef;
    ((BitsByte) ref bitsByte4)[1] = ModConditions.downedGuardianOfFrost;
    int num4 = (int) BitsByte.op_Implicit(bitsByte4);
    binaryWriter4.Write((byte) num4);
    BinaryWriter binaryWriter5 = writer;
    BitsByte bitsByte5 = new BitsByte();
    ((BitsByte) ref bitsByte5)[0] = ModConditions.downedSoulHarvester;
    int num5 = (int) BitsByte.op_Implicit(bitsByte5);
    binaryWriter5.Write((byte) num5);
    BinaryWriter binaryWriter6 = writer;
    BitsByte bitsByte6 = new BitsByte();
    ((BitsByte) ref bitsByte6)[0] = ModConditions.downedTreeToad;
    ((BitsByte) ref bitsByte6)[1] = ModConditions.downedSeseKitsugai;
    ((BitsByte) ref bitsByte6)[2] = ModConditions.downedEyeOfTheStorm;
    ((BitsByte) ref bitsByte6)[3] = ModConditions.downedFrigidius;
    int num6 = (int) BitsByte.op_Implicit(bitsByte6);
    binaryWriter6.Write((byte) num6);
    BinaryWriter binaryWriter7 = writer;
    BitsByte bitsByte7 = new BitsByte();
    ((BitsByte) ref bitsByte7)[0] = ModConditions.downedCoreBoss;
    int num7 = (int) BitsByte.op_Implicit(bitsByte7);
    binaryWriter7.Write((byte) num7);
    BinaryWriter binaryWriter8 = writer;
    BitsByte bitsByte8 = new BitsByte();
    ((BitsByte) ref bitsByte8)[0] = ModConditions.downedCragmawMire;
    ((BitsByte) ref bitsByte8)[1] = ModConditions.downedNuclearTerror;
    ((BitsByte) ref bitsByte8)[2] = ModConditions.downedMauler;
    int num8 = (int) BitsByte.op_Implicit(bitsByte8);
    binaryWriter8.Write((byte) num8);
    BinaryWriter binaryWriter9 = writer;
    BitsByte bitsByte9 = new BitsByte();
    ((BitsByte) ref bitsByte9)[0] = ModConditions.downedWulfrumExcavator;
    int num9 = (int) BitsByte.op_Implicit(bitsByte9);
    binaryWriter9.Write((byte) num9);
    BinaryWriter binaryWriter10 = writer;
    BitsByte bitsByte10 = new BitsByte();
    ((BitsByte) ref bitsByte10)[0] = ModConditions.downedCruiser;
    int num10 = (int) BitsByte.op_Implicit(bitsByte10);
    binaryWriter10.Write((byte) num10);
    BinaryWriter binaryWriter11 = writer;
    BitsByte bitsByte11 = new BitsByte();
    ((BitsByte) ref bitsByte11)[0] = ModConditions.downedAstrageldon;
    int num11 = (int) BitsByte.op_Implicit(bitsByte11);
    binaryWriter11.Write((byte) num11);
    BinaryWriter binaryWriter12 = writer;
    BitsByte bitsByte12 = new BitsByte();
    ((BitsByte) ref bitsByte12)[0] = ModConditions.downedClamitas;
    ((BitsByte) ref bitsByte12)[1] = ModConditions.downedPyrogen;
    ((BitsByte) ref bitsByte12)[2] = ModConditions.downedWallOfBronze;
    int num12 = (int) BitsByte.op_Implicit(bitsByte12);
    binaryWriter12.Write((byte) num12);
    BinaryWriter binaryWriter13 = writer;
    BitsByte bitsByte13 = new BitsByte();
    ((BitsByte) ref bitsByte13)[0] = ModConditions.downedLepus;
    ((BitsByte) ref bitsByte13)[1] = ModConditions.downedTurkor;
    ((BitsByte) ref bitsByte13)[2] = ModConditions.downedOcram;
    int num13 = (int) BitsByte.op_Implicit(bitsByte13);
    binaryWriter13.Write((byte) num13);
    BinaryWriter binaryWriter14 = writer;
    BitsByte bitsByte14 = new BitsByte();
    ((BitsByte) ref bitsByte14)[0] = ModConditions.downedRediancie;
    ((BitsByte) ref bitsByte14)[1] = ModConditions.downedBabyIceDragon;
    ((BitsByte) ref bitsByte14)[2] = ModConditions.downedSlimeEmperor;
    ((BitsByte) ref bitsByte14)[3] = ModConditions.downedBloodiancie;
    ((BitsByte) ref bitsByte14)[4] = ModConditions.downedThunderveinDragon;
    ((BitsByte) ref bitsByte14)[5] = ModConditions.downedNightmarePlantera;
    int num14 = (int) BitsByte.op_Implicit(bitsByte14);
    binaryWriter14.Write((byte) num14);
    BinaryWriter binaryWriter15 = writer;
    BitsByte bitsByte15 = new BitsByte();
    ((BitsByte) ref bitsByte15)[0] = ModConditions.downedChasme;
    int num15 = (int) BitsByte.op_Implicit(bitsByte15);
    binaryWriter15.Write((byte) num15);
    BinaryWriter binaryWriter16 = writer;
    BitsByte bitsByte16 = new BitsByte();
    ((BitsByte) ref bitsByte16)[0] = ModConditions.downedLifeGuardian;
    ((BitsByte) ref bitsByte16)[1] = ModConditions.downedManaGuardian;
    ((BitsByte) ref bitsByte16)[2] = ModConditions.downedMeteorExcavator;
    ((BitsByte) ref bitsByte16)[3] = ModConditions.downedMeteorAnnihilator;
    ((BitsByte) ref bitsByte16)[4] = ModConditions.downedHellfireSerpent;
    ((BitsByte) ref bitsByte16)[5] = ModConditions.downedWitheredAcornSpirit;
    ((BitsByte) ref bitsByte16)[6] = ModConditions.downedGoblinSorcererChieftain;
    int num16 = (int) BitsByte.op_Implicit(bitsByte16);
    binaryWriter16.Write((byte) num16);
    BinaryWriter binaryWriter17 = writer;
    BitsByte bitsByte17 = new BitsByte();
    ((BitsByte) ref bitsByte17)[0] = ModConditions.downedGalahis;
    ((BitsByte) ref bitsByte17)[1] = ModConditions.downedCreation;
    ((BitsByte) ref bitsByte17)[2] = ModConditions.downedDestruction;
    int num17 = (int) BitsByte.op_Implicit(bitsByte17);
    binaryWriter17.Write((byte) num17);
    BinaryWriter binaryWriter18 = writer;
    BitsByte bitsByte18 = new BitsByte();
    ((BitsByte) ref bitsByte18)[0] = ModConditions.downedBlightKing;
    ((BitsByte) ref bitsByte18)[1] = ModConditions.downedGardener;
    ((BitsByte) ref bitsByte18)[2] = ModConditions.downedGlaciation;
    ((BitsByte) ref bitsByte18)[3] = ModConditions.downedHandOfCthulhu;
    ((BitsByte) ref bitsByte18)[4] = ModConditions.downedCursePreacher;
    int num18 = (int) BitsByte.op_Implicit(bitsByte18);
    binaryWriter18.Write((byte) num18);
    BinaryWriter binaryWriter19 = writer;
    BitsByte bitsByte19 = new BitsByte();
    ((BitsByte) ref bitsByte19)[0] = ModConditions.downedEffulgence;
    ((BitsByte) ref bitsByte19)[1] = ModConditions.downedIceLich;
    int num19 = (int) BitsByte.op_Implicit(bitsByte19);
    binaryWriter19.Write((byte) num19);
    BinaryWriter binaryWriter20 = writer;
    BitsByte bitsByte20 = new BitsByte();
    ((BitsByte) ref bitsByte20)[0] = ModConditions.downedNiflheim;
    ((BitsByte) ref bitsByte20)[1] = ModConditions.downedStellarStarship;
    int num20 = (int) BitsByte.op_Implicit(bitsByte20);
    binaryWriter20.Write((byte) num20);
    BinaryWriter binaryWriter21 = writer;
    BitsByte bitsByte21 = new BitsByte();
    ((BitsByte) ref bitsByte21)[0] = ModConditions.downedBacteriumPrime;
    ((BitsByte) ref bitsByte21)[1] = ModConditions.downedDesertBeak;
    ((BitsByte) ref bitsByte21)[2] = ModConditions.downedKingSting;
    ((BitsByte) ref bitsByte21)[3] = ModConditions.downedMechasting;
    ((BitsByte) ref bitsByte21)[4] = ModConditions.downedPhantasm;
    int num21 = (int) BitsByte.op_Implicit(bitsByte21);
    binaryWriter21.Write((byte) num21);
    BinaryWriter binaryWriter22 = writer;
    BitsByte bitsByte22 = new BitsByte();
    ((BitsByte) ref bitsByte22)[0] = ModConditions.downedTrojanSquirrel;
    ((BitsByte) ref bitsByte22)[1] = ModConditions.downedCursedCoffin;
    ((BitsByte) ref bitsByte22)[2] = ModConditions.downedDeviantt;
    ((BitsByte) ref bitsByte22)[3] = ModConditions.downedLifelight;
    ((BitsByte) ref bitsByte22)[4] = ModConditions.downedBanishedBaron;
    ((BitsByte) ref bitsByte22)[5] = ModConditions.downedEridanus;
    ((BitsByte) ref bitsByte22)[6] = ModConditions.downedAbominationn;
    ((BitsByte) ref bitsByte22)[7] = ModConditions.downedMutant;
    int num22 = (int) BitsByte.op_Implicit(bitsByte22);
    binaryWriter22.Write((byte) num22);
    BinaryWriter binaryWriter23 = writer;
    BitsByte bitsByte23 = new BitsByte();
    ((BitsByte) ref bitsByte23)[0] = ModConditions.downedAlphaFrostjaw;
    ((BitsByte) ref bitsByte23)[1] = ModConditions.downedSanguineElemental;
    int num23 = (int) BitsByte.op_Implicit(bitsByte23);
    binaryWriter23.Write((byte) num23);
    BinaryWriter binaryWriter24 = writer;
    BitsByte bitsByte24 = new BitsByte();
    ((BitsByte) ref bitsByte24)[0] = ModConditions.downedLad;
    ((BitsByte) ref bitsByte24)[1] = ModConditions.downedHornlitz;
    ((BitsByte) ref bitsByte24)[2] = ModConditions.downedSnowDon;
    ((BitsByte) ref bitsByte24)[3] = ModConditions.downedStoffie;
    int num24 = (int) BitsByte.op_Implicit(bitsByte24);
    binaryWriter24.Write((byte) num24);
    BinaryWriter binaryWriter25 = writer;
    BitsByte bitsByte25 = new BitsByte();
    ((BitsByte) ref bitsByte25)[0] = ModConditions.downedLilyWhite;
    ((BitsByte) ref bitsByte25)[1] = ModConditions.downedRumia;
    ((BitsByte) ref bitsByte25)[2] = ModConditions.downedEternityLarva;
    ((BitsByte) ref bitsByte25)[3] = ModConditions.downedNazrin;
    ((BitsByte) ref bitsByte25)[4] = ModConditions.downedHinaKagiyama;
    ((BitsByte) ref bitsByte25)[5] = ModConditions.downedSekibanki;
    ((BitsByte) ref bitsByte25)[6] = ModConditions.downedSeiran;
    ((BitsByte) ref bitsByte25)[7] = ModConditions.downedNitoriKawashiro;
    int num25 = (int) BitsByte.op_Implicit(bitsByte25);
    binaryWriter25.Write((byte) num25);
    BinaryWriter binaryWriter26 = writer;
    BitsByte bitsByte26 = new BitsByte();
    ((BitsByte) ref bitsByte26)[0] = ModConditions.downedMedicineMelancholy;
    ((BitsByte) ref bitsByte26)[1] = ModConditions.downedCirno;
    ((BitsByte) ref bitsByte26)[2] = ModConditions.downedMinamitsuMurasa;
    ((BitsByte) ref bitsByte26)[3] = ModConditions.downedAliceMargatroid;
    ((BitsByte) ref bitsByte26)[4] = ModConditions.downedSakuyaIzayoi;
    ((BitsByte) ref bitsByte26)[5] = ModConditions.downedSeijaKijin;
    ((BitsByte) ref bitsByte26)[6] = ModConditions.downedMayumiJoutouguu;
    ((BitsByte) ref bitsByte26)[7] = ModConditions.downedToyosatomimiNoMiko;
    int num26 = (int) BitsByte.op_Implicit(bitsByte26);
    binaryWriter26.Write((byte) num26);
    BinaryWriter binaryWriter27 = writer;
    BitsByte bitsByte27 = new BitsByte();
    ((BitsByte) ref bitsByte27)[0] = ModConditions.downedKaguyaHouraisan;
    ((BitsByte) ref bitsByte27)[1] = ModConditions.downedUtsuhoReiuji;
    ((BitsByte) ref bitsByte27)[2] = ModConditions.downedTenshiHinanawi;
    ((BitsByte) ref bitsByte27)[3] = ModConditions.downedKisume;
    int num27 = (int) BitsByte.op_Implicit(bitsByte27);
    binaryWriter27.Write((byte) num27);
    BinaryWriter binaryWriter28 = writer;
    BitsByte bitsByte28 = new BitsByte();
    ((BitsByte) ref bitsByte28)[0] = ModConditions.downedTrerios;
    ((BitsByte) ref bitsByte28)[1] = ModConditions.downedMagmaEye;
    ((BitsByte) ref bitsByte28)[2] = ModConditions.downedJack;
    ((BitsByte) ref bitsByte28)[3] = ModConditions.downedAcheron;
    int num28 = (int) BitsByte.op_Implicit(bitsByte28);
    binaryWriter28.Write((byte) num28);
    BinaryWriter binaryWriter29 = writer;
    BitsByte bitsByte29 = new BitsByte();
    ((BitsByte) ref bitsByte29)[0] = ModConditions.downedMarquisMoonsquid;
    ((BitsByte) ref bitsByte29)[1] = ModConditions.downedPriestessRod;
    ((BitsByte) ref bitsByte29)[2] = ModConditions.downedDiver;
    ((BitsByte) ref bitsByte29)[3] = ModConditions.downedMotherbrain;
    ((BitsByte) ref bitsByte29)[4] = ModConditions.downedWallOfShadow;
    ((BitsByte) ref bitsByte29)[5] = ModConditions.downedSunSlimeGod;
    ((BitsByte) ref bitsByte29)[6] = ModConditions.downedOverwatcher;
    ((BitsByte) ref bitsByte29)[7] = ModConditions.downedLifebringer;
    int num29 = (int) BitsByte.op_Implicit(bitsByte29);
    binaryWriter29.Write((byte) num29);
    BinaryWriter binaryWriter30 = writer;
    BitsByte bitsByte30 = new BitsByte();
    ((BitsByte) ref bitsByte30)[0] = ModConditions.downedMaterealizer;
    ((BitsByte) ref bitsByte30)[1] = ModConditions.downedScarabBelief;
    ((BitsByte) ref bitsByte30)[2] = ModConditions.downedWorldsEndWhale;
    ((BitsByte) ref bitsByte30)[3] = ModConditions.downedSon;
    ((BitsByte) ref bitsByte30)[4] = ModConditions.downedCaveOrdeal;
    ((BitsByte) ref bitsByte30)[5] = ModConditions.downedCorruptOrdeal;
    ((BitsByte) ref bitsByte30)[6] = ModConditions.downedCrimsonOrdeal;
    ((BitsByte) ref bitsByte30)[7] = ModConditions.downedDesertOrdeal;
    int num30 = (int) BitsByte.op_Implicit(bitsByte30);
    binaryWriter30.Write((byte) num30);
    BinaryWriter binaryWriter31 = writer;
    BitsByte bitsByte31 = new BitsByte();
    ((BitsByte) ref bitsByte31)[0] = ModConditions.downedForestOrdeal;
    ((BitsByte) ref bitsByte31)[1] = ModConditions.downedHallowOrdeal;
    ((BitsByte) ref bitsByte31)[2] = ModConditions.downedJungleOrdeal;
    ((BitsByte) ref bitsByte31)[3] = ModConditions.downedSkyOrdeal;
    ((BitsByte) ref bitsByte31)[4] = ModConditions.downedSnowOrdeal;
    ((BitsByte) ref bitsByte31)[5] = ModConditions.downedUnderworldOrdeal;
    int num31 = (int) BitsByte.op_Implicit(bitsByte31);
    binaryWriter31.Write((byte) num31);
    BinaryWriter binaryWriter32 = writer;
    BitsByte bitsByte32 = new BitsByte();
    ((BitsByte) ref bitsByte32)[0] = ModConditions.downedGoozma;
    int num32 = (int) BitsByte.op_Implicit(bitsByte32);
    binaryWriter32.Write((byte) num32);
    BinaryWriter binaryWriter33 = writer;
    BitsByte bitsByte33 = new BitsByte();
    ((BitsByte) ref bitsByte33)[0] = ModConditions.downedBereftVassal;
    int num33 = (int) BitsByte.op_Implicit(bitsByte33);
    binaryWriter33.Write((byte) num33);
    BinaryWriter binaryWriter34 = writer;
    BitsByte bitsByte34 = new BitsByte();
    ((BitsByte) ref bitsByte34)[0] = ModConditions.downedStoneGuardian;
    ((BitsByte) ref bitsByte34)[1] = ModConditions.downedCommanderGintzia;
    ((BitsByte) ref bitsByte34)[2] = ModConditions.downedSunStalker;
    ((BitsByte) ref bitsByte34)[3] = ModConditions.downedPumpkinJack;
    ((BitsByte) ref bitsByte34)[4] = ModConditions.downedForgottenPuppetDaedus;
    ((BitsByte) ref bitsByte34)[5] = ModConditions.downedDreadMire;
    ((BitsByte) ref bitsByte34)[6] = ModConditions.downedSingularityFragment;
    ((BitsByte) ref bitsByte34)[7] = ModConditions.downedVerlia;
    int num34 = (int) BitsByte.op_Implicit(bitsByte34);
    binaryWriter34.Write((byte) num34);
    BinaryWriter binaryWriter35 = writer;
    BitsByte bitsByte35 = new BitsByte();
    ((BitsByte) ref bitsByte35)[0] = ModConditions.downedIrradia;
    ((BitsByte) ref bitsByte35)[1] = ModConditions.downedSylia;
    ((BitsByte) ref bitsByte35)[2] = ModConditions.downedFenix;
    ((BitsByte) ref bitsByte35)[3] = ModConditions.downedBlazingSerpent;
    ((BitsByte) ref bitsByte35)[4] = ModConditions.downedCogwork;
    ((BitsByte) ref bitsByte35)[5] = ModConditions.downedWaterCogwork;
    ((BitsByte) ref bitsByte35)[6] = ModConditions.downedWaterJellyfish;
    ((BitsByte) ref bitsByte35)[7] = ModConditions.downedSparn;
    int num35 = (int) BitsByte.op_Implicit(bitsByte35);
    binaryWriter35.Write((byte) num35);
    BinaryWriter binaryWriter36 = writer;
    BitsByte bitsByte36 = new BitsByte();
    ((BitsByte) ref bitsByte36)[0] = ModConditions.downedPandorasFlamebox;
    ((BitsByte) ref bitsByte36)[1] = ModConditions.downedSTARBOMBER;
    ((BitsByte) ref bitsByte36)[2] = ModConditions.downedGintzeArmy;
    int num36 = (int) BitsByte.op_Implicit(bitsByte36);
    binaryWriter36.Write((byte) num36);
    BinaryWriter binaryWriter37 = writer;
    BitsByte bitsByte37 = new BitsByte();
    ((BitsByte) ref bitsByte37)[0] = ModConditions.downedBritzz;
    ((BitsByte) ref bitsByte37)[1] = ModConditions.downedTheAlchemist;
    ((BitsByte) ref bitsByte37)[2] = ModConditions.downedCarnagePillar;
    ((BitsByte) ref bitsByte37)[3] = ModConditions.downedVoidDigger;
    ((BitsByte) ref bitsByte37)[4] = ModConditions.downedPrinceSlime;
    ((BitsByte) ref bitsByte37)[5] = ModConditions.downedTriplets;
    ((BitsByte) ref bitsByte37)[6] = ModConditions.downedJungleDefenders;
    int num37 = (int) BitsByte.op_Implicit(bitsByte37);
    binaryWriter37.Write((byte) num37);
    BinaryWriter binaryWriter38 = writer;
    BitsByte bitsByte38 = new BitsByte();
    ((BitsByte) ref bitsByte38)[0] = ModConditions.downedSt4sys;
    ((BitsByte) ref bitsByte38)[1] = ModConditions.downedTerminator;
    ((BitsByte) ref bitsByte38)[2] = ModConditions.downedCaretaker;
    ((BitsByte) ref bitsByte38)[3] = ModConditions.downedSiegeEngine;
    int num38 = (int) BitsByte.op_Implicit(bitsByte38);
    binaryWriter38.Write((byte) num38);
    BinaryWriter binaryWriter39 = writer;
    BitsByte bitsByte39 = new BitsByte();
    ((BitsByte) ref bitsByte39)[0] = ModConditions.downedSuperVoltaicMotherSlime;
    int num39 = (int) BitsByte.op_Implicit(bitsByte39);
    binaryWriter39.Write((byte) num39);
    BinaryWriter binaryWriter40 = writer;
    BitsByte bitsByte40 = new BitsByte();
    ((BitsByte) ref bitsByte40)[0] = ModConditions.downedTorizo;
    ((BitsByte) ref bitsByte40)[1] = ModConditions.downedSerris;
    ((BitsByte) ref bitsByte40)[2] = ModConditions.downedKraid;
    ((BitsByte) ref bitsByte40)[3] = ModConditions.downedPhantoon;
    ((BitsByte) ref bitsByte40)[4] = ModConditions.downedOmegaPirate;
    ((BitsByte) ref bitsByte40)[5] = ModConditions.downedNightmare;
    ((BitsByte) ref bitsByte40)[6] = ModConditions.downedGoldenTorizo;
    int num40 = (int) BitsByte.op_Implicit(bitsByte40);
    binaryWriter40.Write((byte) num40);
    BinaryWriter binaryWriter41 = writer;
    BitsByte bitsByte41 = new BitsByte();
    ((BitsByte) ref bitsByte41)[0] = ModConditions.downedOphiopede;
    ((BitsByte) ref bitsByte41)[1] = ModConditions.downedOphiocoon;
    ((BitsByte) ref bitsByte41)[2] = ModConditions.downedOphiofly;
    int num41 = (int) BitsByte.op_Implicit(bitsByte41);
    binaryWriter41.Write((byte) num41);
    BinaryWriter binaryWriter42 = writer;
    BitsByte bitsByte42 = new BitsByte();
    ((BitsByte) ref bitsByte42)[0] = ModConditions.downedStormCloudfish;
    ((BitsByte) ref bitsByte42)[1] = ModConditions.downedStarConstruct;
    ((BitsByte) ref bitsByte42)[2] = ModConditions.downedGigabat;
    ((BitsByte) ref bitsByte42)[3] = ModConditions.downedSunPixie;
    ((BitsByte) ref bitsByte42)[4] = ModConditions.downedEsophage;
    ((BitsByte) ref bitsByte42)[5] = ModConditions.downedConvectiveWanderer;
    int num42 = (int) BitsByte.op_Implicit(bitsByte42);
    binaryWriter42.Write((byte) num42);
    BinaryWriter binaryWriter43 = writer;
    BitsByte bitsByte43 = new BitsByte();
    ((BitsByte) ref bitsByte43)[0] = ModConditions.downedForestGuardian;
    ((BitsByte) ref bitsByte43)[1] = ModConditions.downedCryoGuardian;
    ((BitsByte) ref bitsByte43)[2] = ModConditions.downedPrimordialWorm;
    ((BitsByte) ref bitsByte43)[3] = ModConditions.downedTheGuardianOfHell;
    ((BitsByte) ref bitsByte43)[4] = ModConditions.downedVoid;
    ((BitsByte) ref bitsByte43)[5] = ModConditions.downedArmagem;
    int num43 = (int) BitsByte.op_Implicit(bitsByte43);
    binaryWriter43.Write((byte) num43);
    BinaryWriter binaryWriter44 = writer;
    BitsByte bitsByte44 = new BitsByte();
    ((BitsByte) ref bitsByte44)[0] = ModConditions.downedPolarExterminator;
    ((BitsByte) ref bitsByte44)[1] = ModConditions.downedDivineLight;
    ((BitsByte) ref bitsByte44)[2] = ModConditions.downedAncientMachine;
    ((BitsByte) ref bitsByte44)[3] = ModConditions.downedNoehtnap;
    ((BitsByte) ref bitsByte44)[4] = ModConditions.downedHydra;
    ((BitsByte) ref bitsByte44)[5] = ModConditions.downedImperious;
    ((BitsByte) ref bitsByte44)[6] = ModConditions.downedRuneGhost;
    ((BitsByte) ref bitsByte44)[7] = ModConditions.downedInvaderBattleship;
    int num44 = (int) BitsByte.op_Implicit(bitsByte44);
    binaryWriter44.Write((byte) num44);
    BinaryWriter binaryWriter45 = writer;
    BitsByte bitsByte45 = new BitsByte();
    ((BitsByte) ref bitsByte45)[0] = ModConditions.downedInvaderNoehtnap;
    ((BitsByte) ref bitsByte45)[1] = ModConditions.downedOLORD;
    ((BitsByte) ref bitsByte45)[2] = ModConditions.downedGreatTyrannosaurus;
    ((BitsByte) ref bitsByte45)[3] = ModConditions.downedDinoMilitia;
    ((BitsByte) ref bitsByte45)[4] = ModConditions.downedInvaders;
    int num45 = (int) BitsByte.op_Implicit(bitsByte45);
    binaryWriter45.Write((byte) num45);
    BinaryWriter binaryWriter46 = writer;
    BitsByte bitsByte46 = new BitsByte();
    ((BitsByte) ref bitsByte46)[0] = ModConditions.downedThorn;
    ((BitsByte) ref bitsByte46)[1] = ModConditions.downedErhan;
    ((BitsByte) ref bitsByte46)[2] = ModConditions.downedKeeper;
    ((BitsByte) ref bitsByte46)[3] = ModConditions.downedSeedOfInfection;
    ((BitsByte) ref bitsByte46)[4] = ModConditions.downedKingSlayerIII;
    ((BitsByte) ref bitsByte46)[5] = ModConditions.downedOmegaCleaver;
    ((BitsByte) ref bitsByte46)[6] = ModConditions.downedOmegaGigapora;
    ((BitsByte) ref bitsByte46)[7] = ModConditions.downedOmegaObliterator;
    int num46 = (int) BitsByte.op_Implicit(bitsByte46);
    binaryWriter46.Write((byte) num46);
    BinaryWriter binaryWriter47 = writer;
    BitsByte bitsByte47 = new BitsByte();
    ((BitsByte) ref bitsByte47)[0] = ModConditions.downedPatientZero;
    ((BitsByte) ref bitsByte47)[1] = ModConditions.downedAkka;
    ((BitsByte) ref bitsByte47)[2] = ModConditions.downedUkko;
    ((BitsByte) ref bitsByte47)[3] = ModConditions.downedAncientDeityDuo;
    ((BitsByte) ref bitsByte47)[4] = ModConditions.downedNebuleus;
    ((BitsByte) ref bitsByte47)[5] = ModConditions.downedFowlEmperor;
    ((BitsByte) ref bitsByte47)[6] = ModConditions.downedCockatrice;
    ((BitsByte) ref bitsByte47)[7] = ModConditions.downedBasan;
    int num47 = (int) BitsByte.op_Implicit(bitsByte47);
    binaryWriter47.Write((byte) num47);
    BinaryWriter binaryWriter48 = writer;
    BitsByte bitsByte48 = new BitsByte();
    ((BitsByte) ref bitsByte48)[0] = ModConditions.downedSkullDigger;
    ((BitsByte) ref bitsByte48)[1] = ModConditions.downedEaglecrestGolem;
    ((BitsByte) ref bitsByte48)[2] = ModConditions.downedCalavia;
    ((BitsByte) ref bitsByte48)[3] = ModConditions.downedTheJanitor;
    ((BitsByte) ref bitsByte48)[4] = ModConditions.downedIrradiatedBehemoth;
    ((BitsByte) ref bitsByte48)[5] = ModConditions.downedBlisterface;
    ((BitsByte) ref bitsByte48)[6] = ModConditions.downedProtectorVolt;
    ((BitsByte) ref bitsByte48)[7] = ModConditions.downedMACEProject;
    int num48 = (int) BitsByte.op_Implicit(bitsByte48);
    binaryWriter48.Write((byte) num48);
    BinaryWriter binaryWriter49 = writer;
    BitsByte bitsByte49 = new BitsByte();
    ((BitsByte) ref bitsByte49)[0] = ModConditions.downedFowlMorning;
    ((BitsByte) ref bitsByte49)[1] = ModConditions.downedRaveyard;
    int num49 = (int) BitsByte.op_Implicit(bitsByte49);
    binaryWriter49.Write((byte) num49);
    BinaryWriter binaryWriter50 = writer;
    BitsByte bitsByte50 = new BitsByte();
    ((BitsByte) ref bitsByte50)[0] = ModConditions.downedPutridPinky;
    ((BitsByte) ref bitsByte50)[1] = ModConditions.downedGlowmoth;
    ((BitsByte) ref bitsByte50)[2] = ModConditions.downedPharaohsCurse;
    ((BitsByte) ref bitsByte50)[3] = ModConditions.downedAdvisor;
    ((BitsByte) ref bitsByte50)[4] = ModConditions.downedPolaris;
    ((BitsByte) ref bitsByte50)[5] = ModConditions.downedLux;
    ((BitsByte) ref bitsByte50)[6] = ModConditions.downedSubspaceSerpent;
    ((BitsByte) ref bitsByte50)[7] = ModConditions.downedNatureConstruct;
    int num50 = (int) BitsByte.op_Implicit(bitsByte50);
    binaryWriter50.Write((byte) num50);
    BinaryWriter binaryWriter51 = writer;
    BitsByte bitsByte51 = new BitsByte();
    ((BitsByte) ref bitsByte51)[0] = ModConditions.downedEarthenConstruct;
    ((BitsByte) ref bitsByte51)[1] = ModConditions.downedPermafrostConstruct;
    ((BitsByte) ref bitsByte51)[2] = ModConditions.downedTidalConstruct;
    ((BitsByte) ref bitsByte51)[3] = ModConditions.downedOtherworldlyConstruct;
    ((BitsByte) ref bitsByte51)[4] = ModConditions.downedEvilConstruct;
    ((BitsByte) ref bitsByte51)[5] = ModConditions.downedInfernoConstruct;
    ((BitsByte) ref bitsByte51)[6] = ModConditions.downedChaosConstruct;
    ((BitsByte) ref bitsByte51)[7] = ModConditions.downedNatureSpirit;
    int num51 = (int) BitsByte.op_Implicit(bitsByte51);
    binaryWriter51.Write((byte) num51);
    BinaryWriter binaryWriter52 = writer;
    BitsByte bitsByte52 = new BitsByte();
    ((BitsByte) ref bitsByte52)[0] = ModConditions.downedEarthenSpirit;
    ((BitsByte) ref bitsByte52)[1] = ModConditions.downedPermafrostSpirit;
    ((BitsByte) ref bitsByte52)[2] = ModConditions.downedTidalSpirit;
    ((BitsByte) ref bitsByte52)[3] = ModConditions.downedOtherworldlySpirit;
    ((BitsByte) ref bitsByte52)[4] = ModConditions.downedEvilSpirit;
    ((BitsByte) ref bitsByte52)[5] = ModConditions.downedInfernoSpirit;
    ((BitsByte) ref bitsByte52)[6] = ModConditions.downedChaosSpirit;
    int num52 = (int) BitsByte.op_Implicit(bitsByte52);
    binaryWriter52.Write((byte) num52);
    BinaryWriter binaryWriter53 = writer;
    BitsByte bitsByte53 = new BitsByte();
    ((BitsByte) ref bitsByte53)[0] = ModConditions.downedDecree;
    ((BitsByte) ref bitsByte53)[1] = ModConditions.downedFlamingPumpkin;
    ((BitsByte) ref bitsByte53)[2] = ModConditions.downedZombiePiglinBrute;
    ((BitsByte) ref bitsByte53)[3] = ModConditions.downedJensenTheGrandHarpy;
    ((BitsByte) ref bitsByte53)[4] = ModConditions.downedAraneas;
    ((BitsByte) ref bitsByte53)[5] = ModConditions.downedHarpyQueenRaynare;
    ((BitsByte) ref bitsByte53)[6] = ModConditions.downedPrimordia;
    ((BitsByte) ref bitsByte53)[7] = ModConditions.downedAbaddon;
    int num53 = (int) BitsByte.op_Implicit(bitsByte53);
    binaryWriter53.Write((byte) num53);
    BinaryWriter binaryWriter54 = writer;
    BitsByte bitsByte54 = new BitsByte();
    ((BitsByte) ref bitsByte54)[0] = ModConditions.downedAraghur;
    ((BitsByte) ref bitsByte54)[1] = ModConditions.downedLostSiblings;
    ((BitsByte) ref bitsByte54)[2] = ModConditions.downedErazor;
    ((BitsByte) ref bitsByte54)[3] = ModConditions.downedNihilus;
    int num54 = (int) BitsByte.op_Implicit(bitsByte54);
    binaryWriter54.Write((byte) num54);
    BinaryWriter binaryWriter55 = writer;
    BitsByte bitsByte55 = new BitsByte();
    ((BitsByte) ref bitsByte55)[0] = ModConditions.downedExodygen;
    int num55 = (int) BitsByte.op_Implicit(bitsByte55);
    binaryWriter55.Write((byte) num55);
    BinaryWriter binaryWriter56 = writer;
    BitsByte bitsByte56 = new BitsByte();
    ((BitsByte) ref bitsByte56)[0] = ModConditions.downedScarabeus;
    ((BitsByte) ref bitsByte56)[1] = ModConditions.downedMoonJellyWizard;
    ((BitsByte) ref bitsByte56)[2] = ModConditions.downedVinewrathBane;
    ((BitsByte) ref bitsByte56)[3] = ModConditions.downedAncientAvian;
    ((BitsByte) ref bitsByte56)[4] = ModConditions.downedStarplateVoyager;
    ((BitsByte) ref bitsByte56)[5] = ModConditions.downedInfernon;
    ((BitsByte) ref bitsByte56)[6] = ModConditions.downedDusking;
    ((BitsByte) ref bitsByte56)[7] = ModConditions.downedAtlas;
    int num56 = (int) BitsByte.op_Implicit(bitsByte56);
    binaryWriter56.Write((byte) num56);
    BinaryWriter binaryWriter57 = writer;
    BitsByte bitsByte57 = new BitsByte();
    ((BitsByte) ref bitsByte57)[0] = ModConditions.downedJellyDeluge;
    ((BitsByte) ref bitsByte57)[1] = ModConditions.downedTide;
    ((BitsByte) ref bitsByte57)[2] = ModConditions.downedMysticMoon;
    int num57 = (int) BitsByte.op_Implicit(bitsByte57);
    binaryWriter57.Write((byte) num57);
    BinaryWriter binaryWriter58 = writer;
    BitsByte bitsByte58 = new BitsByte();
    ((BitsByte) ref bitsByte58)[0] = ModConditions.downedSpookySpirit;
    ((BitsByte) ref bitsByte58)[1] = ModConditions.downedRotGourd;
    ((BitsByte) ref bitsByte58)[2] = ModConditions.downedMoco;
    ((BitsByte) ref bitsByte58)[3] = ModConditions.downedDaffodil;
    ((BitsByte) ref bitsByte58)[4] = ModConditions.downedOrroBoro;
    ((BitsByte) ref bitsByte58)[5] = ModConditions.downedBigBone;
    int num58 = (int) BitsByte.op_Implicit(bitsByte58);
    binaryWriter58.Write((byte) num58);
    BinaryWriter binaryWriter59 = writer;
    BitsByte bitsByte59 = new BitsByte();
    ((BitsByte) ref bitsByte59)[0] = ModConditions.downedAuroracle;
    ((BitsByte) ref bitsByte59)[1] = ModConditions.downedCeiros;
    ((BitsByte) ref bitsByte59)[2] = ModConditions.downedGlassweaver;
    int num59 = (int) BitsByte.op_Implicit(bitsByte59);
    binaryWriter59.Write((byte) num59);
    BinaryWriter binaryWriter60 = writer;
    BitsByte bitsByte60 = new BitsByte();
    ((BitsByte) ref bitsByte60)[0] = ModConditions.downedVagrantofSpace;
    ((BitsByte) ref bitsByte60)[1] = ModConditions.downedThespian;
    ((BitsByte) ref bitsByte60)[2] = ModConditions.downedDioskouroi;
    ((BitsByte) ref bitsByte60)[3] = ModConditions.downedNalhaun;
    ((BitsByte) ref bitsByte60)[4] = ModConditions.downedStarfarers;
    ((BitsByte) ref bitsByte60)[5] = ModConditions.downedPenthesilea;
    ((BitsByte) ref bitsByte60)[6] = ModConditions.downedArbitration;
    ((BitsByte) ref bitsByte60)[7] = ModConditions.downedWarriorOfLight;
    int num60 = (int) BitsByte.op_Implicit(bitsByte60);
    binaryWriter60.Write((byte) num60);
    BinaryWriter binaryWriter61 = writer;
    BitsByte bitsByte61 = new BitsByte();
    ((BitsByte) ref bitsByte61)[0] = ModConditions.downedTsukiyomi;
    int num61 = (int) BitsByte.op_Implicit(bitsByte61);
    binaryWriter61.Write((byte) num61);
    BinaryWriter binaryWriter62 = writer;
    BitsByte bitsByte62 = new BitsByte();
    ((BitsByte) ref bitsByte62)[0] = ModConditions.downedAncientHusk;
    ((BitsByte) ref bitsByte62)[1] = ModConditions.downedOverloadedScandrone;
    ((BitsByte) ref bitsByte62)[2] = ModConditions.downedPainbringer;
    int num62 = (int) BitsByte.op_Implicit(bitsByte62);
    binaryWriter62.Write((byte) num62);
    BinaryWriter binaryWriter63 = writer;
    BitsByte bitsByte63 = new BitsByte();
    ((BitsByte) ref bitsByte63)[0] = ModConditions.downedHarbingerOfAnnihilation;
    ((BitsByte) ref bitsByte63)[1] = ModConditions.downedFlyingTerror;
    ((BitsByte) ref bitsByte63)[2] = ModConditions.downedStoneMantaRay;
    ((BitsByte) ref bitsByte63)[3] = ModConditions.downedBloodweaver;
    int num63 = (int) BitsByte.op_Implicit(bitsByte63);
    binaryWriter63.Write((byte) num63);
    BinaryWriter binaryWriter64 = writer;
    BitsByte bitsByte64 = new BitsByte();
    ((BitsByte) ref bitsByte64)[0] = ModConditions.downedInfectedIncarnate;
    ((BitsByte) ref bitsByte64)[1] = ModConditions.downedTidalTitan;
    ((BitsByte) ref bitsByte64)[2] = ModConditions.downedDunestock;
    ((BitsByte) ref bitsByte64)[3] = ModConditions.downedHexedConstructor;
    ((BitsByte) ref bitsByte64)[4] = ModConditions.downedShadowcrawler;
    ((BitsByte) ref bitsByte64)[5] = ModConditions.downedPrototypeI;
    int num64 = (int) BitsByte.op_Implicit(bitsByte64);
    binaryWriter64.Write((byte) num64);
    BinaryWriter binaryWriter65 = writer;
    BitsByte bitsByte65 = new BitsByte();
    ((BitsByte) ref bitsByte65)[0] = ModConditions.downedGraniteOvergrowth;
    ((BitsByte) ref bitsByte65)[1] = ModConditions.downedBeholder;
    int num65 = (int) BitsByte.op_Implicit(bitsByte65);
    binaryWriter65.Write((byte) num65);
    BinaryWriter binaryWriter66 = writer;
    BitsByte bitsByte66 = new BitsByte();
    ((BitsByte) ref bitsByte66)[0] = ModConditions.downedDredger;
    ((BitsByte) ref bitsByte66)[1] = ModConditions.downedCharcoolSnowman;
    ((BitsByte) ref bitsByte66)[2] = ModConditions.downedCosmicMenace;
    int num66 = (int) BitsByte.op_Implicit(bitsByte66);
    binaryWriter66.Write((byte) num66);
    BinaryWriter binaryWriter67 = writer;
    BitsByte bitsByte67 = new BitsByte();
    ((BitsByte) ref bitsByte67)[0] = ModConditions.downedEvilFlyingBlade;
    int num67 = (int) BitsByte.op_Implicit(bitsByte67);
    binaryWriter67.Write((byte) num67);
    BinaryWriter binaryWriter68 = writer;
    BitsByte bitsByte68 = new BitsByte();
    ((BitsByte) ref bitsByte68)[0] = ModConditions.downedColossalCarnage;
    ((BitsByte) ref bitsByte68)[1] = ModConditions.downedYurnero;
    int num68 = (int) BitsByte.op_Implicit(bitsByte68);
    binaryWriter68.Write((byte) num68);
    BinaryWriter binaryWriter69 = writer;
    BitsByte bitsByte69 = new BitsByte();
    ((BitsByte) ref bitsByte69)[0] = ModConditions.downedStormCloud;
    ((BitsByte) ref bitsByte69)[1] = ModConditions.downedGrandAntlion;
    ((BitsByte) ref bitsByte69)[2] = ModConditions.downedGemstoneElemental;
    ((BitsByte) ref bitsByte69)[3] = ModConditions.downedMoonlightDragonfly;
    ((BitsByte) ref bitsByte69)[4] = ModConditions.downedDreadnaught;
    ((BitsByte) ref bitsByte69)[5] = ModConditions.downedMosquitoMonarch;
    ((BitsByte) ref bitsByte69)[6] = ModConditions.downedAnarchulesBeetle;
    ((BitsByte) ref bitsByte69)[7] = ModConditions.downedChaosbringer;
    int num69 = (int) BitsByte.op_Implicit(bitsByte69);
    binaryWriter69.Write((byte) num69);
    BinaryWriter binaryWriter70 = writer;
    BitsByte bitsByte70 = new BitsByte();
    ((BitsByte) ref bitsByte70)[0] = ModConditions.downedPaladinSpirit;
    int num70 = (int) BitsByte.op_Implicit(bitsByte70);
    binaryWriter70.Write((byte) num70);
    BinaryWriter binaryWriter71 = writer;
    BitsByte bitsByte71 = new BitsByte();
    ((BitsByte) ref bitsByte71)[0] = ModConditions.downedManaflora;
    int num71 = (int) BitsByte.op_Implicit(bitsByte71);
    binaryWriter71.Write((byte) num71);
    BinaryWriter binaryWriter72 = writer;
    BitsByte bitsByte72 = new BitsByte();
    ((BitsByte) ref bitsByte72)[0] = ModConditions.downedNoxus;
    ((BitsByte) ref bitsByte72)[1] = ModConditions.downedNamelessDeityOfLight;
    int num72 = (int) BitsByte.op_Implicit(bitsByte72);
    binaryWriter72.Write((byte) num72);
    BinaryWriter binaryWriter73 = writer;
    BitsByte bitsByte73 = new BitsByte();
    ((BitsByte) ref bitsByte73)[0] = ModConditions.downedDirtball;
    ((BitsByte) ref bitsByte73)[1] = ModConditions.downedMetelord;
    ((BitsByte) ref bitsByte73)[2] = ModConditions.downedAdeneb;
    ((BitsByte) ref bitsByte73)[3] = ModConditions.downedEldritchJellyfish;
    ((BitsByte) ref bitsByte73)[4] = ModConditions.downedSaburRex;
    int num73 = (int) BitsByte.op_Implicit(bitsByte73);
    binaryWriter73.Write((byte) num73);
    BitsByte bitsByte74 = new BitsByte();
    for (int index = 0; index < ModConditions.DownedBoss.Length; ++index)
    {
      int num74 = index % 8;
      if (num74 == 0 && index != 0)
      {
        writer.Write(BitsByte.op_Implicit(bitsByte74));
        bitsByte74 = new BitsByte();
      }
      ((BitsByte) ref bitsByte74)[num74] = ModConditions.DownedBoss[index];
    }
    writer.Write(BitsByte.op_Implicit(bitsByte74));
  }

  public virtual void NetReceive(BinaryReader reader)
  {
    BitsByte bitsByte1 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedDreadnautilus = ((BitsByte) ref bitsByte1)[0];
    ModConditions.downedMartianSaucer = ((BitsByte) ref bitsByte1)[1];
    ModConditions.downedBloodMoon = ((BitsByte) ref bitsByte1)[2];
    ModConditions.downedEclipse = ((BitsByte) ref bitsByte1)[3];
    ModConditions.downedLunarEvent = ((BitsByte) ref bitsByte1)[4];
    BitsByte bitsByte2 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedSlayerOfEvil = ((BitsByte) ref bitsByte2)[0];
    ModConditions.downedSATLA = ((BitsByte) ref bitsByte2)[1];
    ModConditions.downedDrFetus = ((BitsByte) ref bitsByte2)[2];
    ModConditions.downedSlimesHope = ((BitsByte) ref bitsByte2)[3];
    ModConditions.downedPoliticianSlime = ((BitsByte) ref bitsByte2)[4];
    ModConditions.downedAncientTrio = ((BitsByte) ref bitsByte2)[5];
    ModConditions.downedLavalGolem = ((BitsByte) ref bitsByte2)[6];
    ModConditions.downedAntony = ((BitsByte) ref bitsByte2)[7];
    BitsByte bitsByte3 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedBunnyZeppelin = ((BitsByte) ref bitsByte3)[0];
    ModConditions.downedOkiku = ((BitsByte) ref bitsByte3)[1];
    ModConditions.downedHarpyAirforce = ((BitsByte) ref bitsByte3)[2];
    ModConditions.downedIsaac = ((BitsByte) ref bitsByte3)[3];
    ModConditions.downedAncientGuardian = ((BitsByte) ref bitsByte3)[4];
    ModConditions.downedHeroicSlime = ((BitsByte) ref bitsByte3)[5];
    ModConditions.downedHoloSlime = ((BitsByte) ref bitsByte3)[6];
    ModConditions.downedSecurityBot = ((BitsByte) ref bitsByte3)[7];
    BitsByte bitsByte4 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedUndeadChef = ((BitsByte) ref bitsByte4)[0];
    ModConditions.downedGuardianOfFrost = ((BitsByte) ref bitsByte4)[1];
    ModConditions.downedGuardianOfFrost = ((BitsByte) ref bitsByte4)[1];
    BitsByte bitsByte5 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedSoulHarvester = ((BitsByte) ref bitsByte5)[0];
    BitsByte bitsByte6 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedTreeToad = ((BitsByte) ref bitsByte6)[0];
    ModConditions.downedSeseKitsugai = ((BitsByte) ref bitsByte6)[1];
    ModConditions.downedEyeOfTheStorm = ((BitsByte) ref bitsByte6)[2];
    ModConditions.downedFrigidius = ((BitsByte) ref bitsByte6)[3];
    BitsByte bitsByte7 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedCoreBoss = ((BitsByte) ref bitsByte7)[0];
    BitsByte bitsByte8 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedCragmawMire = ((BitsByte) ref bitsByte8)[0];
    ModConditions.downedNuclearTerror = ((BitsByte) ref bitsByte8)[1];
    ModConditions.downedMauler = ((BitsByte) ref bitsByte8)[2];
    BitsByte bitsByte9 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedWulfrumExcavator = ((BitsByte) ref bitsByte9)[0];
    BitsByte bitsByte10 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedCruiser = ((BitsByte) ref bitsByte10)[0];
    BitsByte bitsByte11 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedAstrageldon = ((BitsByte) ref bitsByte11)[0];
    BitsByte bitsByte12 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedClamitas = ((BitsByte) ref bitsByte12)[0];
    ModConditions.downedPyrogen = ((BitsByte) ref bitsByte12)[1];
    ModConditions.downedWallOfBronze = ((BitsByte) ref bitsByte12)[2];
    BitsByte bitsByte13 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedLepus = ((BitsByte) ref bitsByte13)[0];
    ModConditions.downedTurkor = ((BitsByte) ref bitsByte13)[1];
    ModConditions.downedOcram = ((BitsByte) ref bitsByte13)[2];
    BitsByte bitsByte14 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedRediancie = ((BitsByte) ref bitsByte14)[0];
    ModConditions.downedBabyIceDragon = ((BitsByte) ref bitsByte14)[1];
    ModConditions.downedSlimeEmperor = ((BitsByte) ref bitsByte14)[2];
    ModConditions.downedBloodiancie = ((BitsByte) ref bitsByte14)[3];
    ModConditions.downedThunderveinDragon = ((BitsByte) ref bitsByte14)[4];
    ModConditions.downedNightmarePlantera = ((BitsByte) ref bitsByte14)[5];
    BitsByte bitsByte15 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedChasme = ((BitsByte) ref bitsByte15)[0];
    BitsByte bitsByte16 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedLifeGuardian = ((BitsByte) ref bitsByte16)[0];
    ModConditions.downedManaGuardian = ((BitsByte) ref bitsByte16)[1];
    ModConditions.downedMeteorExcavator = ((BitsByte) ref bitsByte16)[2];
    ModConditions.downedMeteorAnnihilator = ((BitsByte) ref bitsByte16)[3];
    ModConditions.downedHellfireSerpent = ((BitsByte) ref bitsByte16)[4];
    ModConditions.downedWitheredAcornSpirit = ((BitsByte) ref bitsByte16)[5];
    ModConditions.downedGoblinSorcererChieftain = ((BitsByte) ref bitsByte16)[6];
    BitsByte bitsByte17 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedGalahis = ((BitsByte) ref bitsByte17)[0];
    ModConditions.downedCreation = ((BitsByte) ref bitsByte17)[1];
    ModConditions.downedDestruction = ((BitsByte) ref bitsByte17)[2];
    BitsByte bitsByte18 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedBlightKing = ((BitsByte) ref bitsByte18)[0];
    ModConditions.downedGardener = ((BitsByte) ref bitsByte18)[1];
    ModConditions.downedGlaciation = ((BitsByte) ref bitsByte18)[2];
    ModConditions.downedHandOfCthulhu = ((BitsByte) ref bitsByte18)[3];
    ModConditions.downedCursePreacher = ((BitsByte) ref bitsByte18)[4];
    BitsByte bitsByte19 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedEffulgence = ((BitsByte) ref bitsByte19)[0];
    ModConditions.downedIceLich = ((BitsByte) ref bitsByte19)[1];
    BitsByte bitsByte20 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedNiflheim = ((BitsByte) ref bitsByte20)[0];
    ModConditions.downedStellarStarship = ((BitsByte) ref bitsByte20)[1];
    BitsByte bitsByte21 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedBacteriumPrime = ((BitsByte) ref bitsByte21)[0];
    ModConditions.downedDesertBeak = ((BitsByte) ref bitsByte21)[1];
    ModConditions.downedKingSting = ((BitsByte) ref bitsByte21)[2];
    ModConditions.downedMechasting = ((BitsByte) ref bitsByte21)[3];
    ModConditions.downedPhantasm = ((BitsByte) ref bitsByte21)[4];
    BitsByte bitsByte22 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedTrojanSquirrel = ((BitsByte) ref bitsByte22)[0];
    ModConditions.downedCursedCoffin = ((BitsByte) ref bitsByte22)[1];
    ModConditions.downedDeviantt = ((BitsByte) ref bitsByte22)[2];
    ModConditions.downedLifelight = ((BitsByte) ref bitsByte22)[3];
    ModConditions.downedBanishedBaron = ((BitsByte) ref bitsByte22)[4];
    ModConditions.downedEridanus = ((BitsByte) ref bitsByte22)[5];
    ModConditions.downedAbominationn = ((BitsByte) ref bitsByte22)[6];
    ModConditions.downedMutant = ((BitsByte) ref bitsByte22)[7];
    BitsByte bitsByte23 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedAlphaFrostjaw = ((BitsByte) ref bitsByte23)[0];
    ModConditions.downedSanguineElemental = ((BitsByte) ref bitsByte23)[1];
    BitsByte bitsByte24 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedLad = ((BitsByte) ref bitsByte24)[0];
    ModConditions.downedHornlitz = ((BitsByte) ref bitsByte24)[1];
    ModConditions.downedSnowDon = ((BitsByte) ref bitsByte24)[2];
    ModConditions.downedStoffie = ((BitsByte) ref bitsByte24)[3];
    BitsByte bitsByte25 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedLilyWhite = ((BitsByte) ref bitsByte25)[0];
    ModConditions.downedRumia = ((BitsByte) ref bitsByte25)[1];
    ModConditions.downedEternityLarva = ((BitsByte) ref bitsByte25)[2];
    ModConditions.downedNazrin = ((BitsByte) ref bitsByte25)[3];
    ModConditions.downedHinaKagiyama = ((BitsByte) ref bitsByte25)[4];
    ModConditions.downedSekibanki = ((BitsByte) ref bitsByte25)[5];
    ModConditions.downedSeiran = ((BitsByte) ref bitsByte25)[6];
    ModConditions.downedNitoriKawashiro = ((BitsByte) ref bitsByte25)[7];
    BitsByte bitsByte26 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedMedicineMelancholy = ((BitsByte) ref bitsByte26)[0];
    ModConditions.downedCirno = ((BitsByte) ref bitsByte26)[1];
    ModConditions.downedMinamitsuMurasa = ((BitsByte) ref bitsByte26)[2];
    ModConditions.downedAliceMargatroid = ((BitsByte) ref bitsByte26)[3];
    ModConditions.downedSakuyaIzayoi = ((BitsByte) ref bitsByte26)[4];
    ModConditions.downedSeijaKijin = ((BitsByte) ref bitsByte26)[5];
    ModConditions.downedMayumiJoutouguu = ((BitsByte) ref bitsByte26)[6];
    ModConditions.downedToyosatomimiNoMiko = ((BitsByte) ref bitsByte26)[7];
    BitsByte bitsByte27 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedKaguyaHouraisan = ((BitsByte) ref bitsByte27)[0];
    ModConditions.downedUtsuhoReiuji = ((BitsByte) ref bitsByte27)[1];
    ModConditions.downedTenshiHinanawi = ((BitsByte) ref bitsByte27)[2];
    ModConditions.downedKisume = ((BitsByte) ref bitsByte27)[3];
    BitsByte bitsByte28 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedTrerios = ((BitsByte) ref bitsByte28)[0];
    ModConditions.downedMagmaEye = ((BitsByte) ref bitsByte28)[1];
    ModConditions.downedJack = ((BitsByte) ref bitsByte28)[2];
    ModConditions.downedAcheron = ((BitsByte) ref bitsByte28)[3];
    BitsByte bitsByte29 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedMarquisMoonsquid = ((BitsByte) ref bitsByte29)[0];
    ModConditions.downedPriestessRod = ((BitsByte) ref bitsByte29)[1];
    ModConditions.downedDiver = ((BitsByte) ref bitsByte29)[2];
    ModConditions.downedMotherbrain = ((BitsByte) ref bitsByte29)[3];
    ModConditions.downedWallOfShadow = ((BitsByte) ref bitsByte29)[4];
    ModConditions.downedSunSlimeGod = ((BitsByte) ref bitsByte29)[5];
    ModConditions.downedOverwatcher = ((BitsByte) ref bitsByte29)[6];
    ModConditions.downedLifebringer = ((BitsByte) ref bitsByte29)[7];
    BitsByte bitsByte30 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedMaterealizer = ((BitsByte) ref bitsByte30)[0];
    ModConditions.downedScarabBelief = ((BitsByte) ref bitsByte30)[1];
    ModConditions.downedWorldsEndWhale = ((BitsByte) ref bitsByte30)[2];
    ModConditions.downedSon = ((BitsByte) ref bitsByte30)[3];
    ModConditions.downedCaveOrdeal = ((BitsByte) ref bitsByte30)[4];
    ModConditions.downedCorruptOrdeal = ((BitsByte) ref bitsByte30)[5];
    ModConditions.downedCrimsonOrdeal = ((BitsByte) ref bitsByte30)[6];
    ModConditions.downedDesertOrdeal = ((BitsByte) ref bitsByte30)[7];
    BitsByte bitsByte31 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedForestOrdeal = ((BitsByte) ref bitsByte31)[0];
    ModConditions.downedHallowOrdeal = ((BitsByte) ref bitsByte31)[1];
    ModConditions.downedJungleOrdeal = ((BitsByte) ref bitsByte31)[2];
    ModConditions.downedSkyOrdeal = ((BitsByte) ref bitsByte31)[3];
    ModConditions.downedSnowOrdeal = ((BitsByte) ref bitsByte31)[4];
    ModConditions.downedUnderworldOrdeal = ((BitsByte) ref bitsByte31)[5];
    BitsByte bitsByte32 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedGoozma = ((BitsByte) ref bitsByte32)[0];
    BitsByte bitsByte33 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedBereftVassal = ((BitsByte) ref bitsByte33)[0];
    BitsByte bitsByte34 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedStoneGuardian = ((BitsByte) ref bitsByte34)[0];
    ModConditions.downedCommanderGintzia = ((BitsByte) ref bitsByte34)[1];
    ModConditions.downedSunStalker = ((BitsByte) ref bitsByte34)[2];
    ModConditions.downedPumpkinJack = ((BitsByte) ref bitsByte34)[3];
    ModConditions.downedForgottenPuppetDaedus = ((BitsByte) ref bitsByte34)[4];
    ModConditions.downedDreadMire = ((BitsByte) ref bitsByte34)[5];
    ModConditions.downedSingularityFragment = ((BitsByte) ref bitsByte34)[6];
    ModConditions.downedVerlia = ((BitsByte) ref bitsByte34)[7];
    BitsByte bitsByte35 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedIrradia = ((BitsByte) ref bitsByte35)[0];
    ModConditions.downedSylia = ((BitsByte) ref bitsByte35)[1];
    ModConditions.downedFenix = ((BitsByte) ref bitsByte35)[2];
    ModConditions.downedBlazingSerpent = ((BitsByte) ref bitsByte35)[3];
    ModConditions.downedCogwork = ((BitsByte) ref bitsByte35)[4];
    ModConditions.downedWaterCogwork = ((BitsByte) ref bitsByte35)[5];
    ModConditions.downedWaterJellyfish = ((BitsByte) ref bitsByte35)[6];
    ModConditions.downedSparn = ((BitsByte) ref bitsByte35)[7];
    BitsByte bitsByte36 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedPandorasFlamebox = ((BitsByte) ref bitsByte36)[0];
    ModConditions.downedSTARBOMBER = ((BitsByte) ref bitsByte36)[1];
    ModConditions.downedGintzeArmy = ((BitsByte) ref bitsByte36)[2];
    BitsByte bitsByte37 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedBritzz = ((BitsByte) ref bitsByte37)[0];
    ModConditions.downedTheAlchemist = ((BitsByte) ref bitsByte37)[1];
    ModConditions.downedCarnagePillar = ((BitsByte) ref bitsByte37)[2];
    ModConditions.downedVoidDigger = ((BitsByte) ref bitsByte37)[3];
    ModConditions.downedPrinceSlime = ((BitsByte) ref bitsByte37)[4];
    ModConditions.downedTriplets = ((BitsByte) ref bitsByte37)[5];
    ModConditions.downedJungleDefenders = ((BitsByte) ref bitsByte37)[6];
    BitsByte bitsByte38 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedSt4sys = ((BitsByte) ref bitsByte38)[0];
    ModConditions.downedTerminator = ((BitsByte) ref bitsByte38)[1];
    ModConditions.downedCaretaker = ((BitsByte) ref bitsByte38)[2];
    ModConditions.downedSiegeEngine = ((BitsByte) ref bitsByte38)[3];
    BitsByte bitsByte39 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedSuperVoltaicMotherSlime = ((BitsByte) ref bitsByte39)[0];
    BitsByte bitsByte40 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedTorizo = ((BitsByte) ref bitsByte40)[0];
    ModConditions.downedSerris = ((BitsByte) ref bitsByte40)[1];
    ModConditions.downedKraid = ((BitsByte) ref bitsByte40)[2];
    ModConditions.downedPhantoon = ((BitsByte) ref bitsByte40)[3];
    ModConditions.downedOmegaPirate = ((BitsByte) ref bitsByte40)[4];
    ModConditions.downedNightmare = ((BitsByte) ref bitsByte40)[5];
    ModConditions.downedGoldenTorizo = ((BitsByte) ref bitsByte40)[6];
    BitsByte bitsByte41 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedOphiopede = ((BitsByte) ref bitsByte41)[0];
    ModConditions.downedOphiocoon = ((BitsByte) ref bitsByte41)[1];
    ModConditions.downedOphiofly = ((BitsByte) ref bitsByte41)[2];
    BitsByte bitsByte42 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedStormCloudfish = ((BitsByte) ref bitsByte42)[0];
    ModConditions.downedStarConstruct = ((BitsByte) ref bitsByte42)[1];
    ModConditions.downedGigabat = ((BitsByte) ref bitsByte42)[2];
    ModConditions.downedSunPixie = ((BitsByte) ref bitsByte42)[3];
    ModConditions.downedEsophage = ((BitsByte) ref bitsByte42)[4];
    ModConditions.downedConvectiveWanderer = ((BitsByte) ref bitsByte42)[5];
    BitsByte bitsByte43 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedForestGuardian = ((BitsByte) ref bitsByte43)[0];
    ModConditions.downedCryoGuardian = ((BitsByte) ref bitsByte43)[1];
    ModConditions.downedPrimordialWorm = ((BitsByte) ref bitsByte43)[2];
    ModConditions.downedTheGuardianOfHell = ((BitsByte) ref bitsByte43)[3];
    ModConditions.downedVoid = ((BitsByte) ref bitsByte43)[4];
    ModConditions.downedArmagem = ((BitsByte) ref bitsByte43)[5];
    BitsByte bitsByte44 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedPolarExterminator = ((BitsByte) ref bitsByte44)[0];
    ModConditions.downedDivineLight = ((BitsByte) ref bitsByte44)[1];
    ModConditions.downedAncientMachine = ((BitsByte) ref bitsByte44)[2];
    ModConditions.downedNoehtnap = ((BitsByte) ref bitsByte44)[3];
    ModConditions.downedHydra = ((BitsByte) ref bitsByte44)[4];
    ModConditions.downedImperious = ((BitsByte) ref bitsByte44)[5];
    ModConditions.downedRuneGhost = ((BitsByte) ref bitsByte44)[6];
    ModConditions.downedInvaderBattleship = ((BitsByte) ref bitsByte44)[7];
    BitsByte bitsByte45 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedInvaderNoehtnap = ((BitsByte) ref bitsByte45)[0];
    ModConditions.downedOLORD = ((BitsByte) ref bitsByte45)[1];
    ModConditions.downedGreatTyrannosaurus = ((BitsByte) ref bitsByte45)[2];
    ModConditions.downedDinoMilitia = ((BitsByte) ref bitsByte45)[3];
    ModConditions.downedInvaders = ((BitsByte) ref bitsByte45)[4];
    BitsByte bitsByte46 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedThorn = ((BitsByte) ref bitsByte46)[0];
    ModConditions.downedErhan = ((BitsByte) ref bitsByte46)[1];
    ModConditions.downedKeeper = ((BitsByte) ref bitsByte46)[2];
    ModConditions.downedSeedOfInfection = ((BitsByte) ref bitsByte46)[3];
    ModConditions.downedKingSlayerIII = ((BitsByte) ref bitsByte46)[4];
    ModConditions.downedOmegaCleaver = ((BitsByte) ref bitsByte46)[5];
    ModConditions.downedOmegaGigapora = ((BitsByte) ref bitsByte46)[6];
    ModConditions.downedOmegaObliterator = ((BitsByte) ref bitsByte46)[7];
    BitsByte bitsByte47 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedPatientZero = ((BitsByte) ref bitsByte47)[0];
    ModConditions.downedAkka = ((BitsByte) ref bitsByte47)[1];
    ModConditions.downedUkko = ((BitsByte) ref bitsByte47)[2];
    ModConditions.downedAncientDeityDuo = ((BitsByte) ref bitsByte47)[3];
    ModConditions.downedNebuleus = ((BitsByte) ref bitsByte47)[4];
    ModConditions.downedFowlEmperor = ((BitsByte) ref bitsByte47)[5];
    ModConditions.downedCockatrice = ((BitsByte) ref bitsByte47)[6];
    ModConditions.downedBasan = ((BitsByte) ref bitsByte47)[7];
    BitsByte bitsByte48 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedSkullDigger = ((BitsByte) ref bitsByte48)[0];
    ModConditions.downedEaglecrestGolem = ((BitsByte) ref bitsByte48)[1];
    ModConditions.downedCalavia = ((BitsByte) ref bitsByte48)[2];
    ModConditions.downedTheJanitor = ((BitsByte) ref bitsByte48)[3];
    ModConditions.downedIrradiatedBehemoth = ((BitsByte) ref bitsByte48)[4];
    ModConditions.downedBlisterface = ((BitsByte) ref bitsByte48)[5];
    ModConditions.downedProtectorVolt = ((BitsByte) ref bitsByte48)[6];
    ModConditions.downedMACEProject = ((BitsByte) ref bitsByte48)[7];
    BitsByte bitsByte49 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedFowlMorning = ((BitsByte) ref bitsByte49)[0];
    ModConditions.downedRaveyard = ((BitsByte) ref bitsByte49)[1];
    BitsByte bitsByte50 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedPutridPinky = ((BitsByte) ref bitsByte50)[0];
    ModConditions.downedGlowmoth = ((BitsByte) ref bitsByte50)[1];
    ModConditions.downedPharaohsCurse = ((BitsByte) ref bitsByte50)[2];
    ModConditions.downedAdvisor = ((BitsByte) ref bitsByte50)[3];
    ModConditions.downedPolaris = ((BitsByte) ref bitsByte50)[4];
    ModConditions.downedLux = ((BitsByte) ref bitsByte50)[5];
    ModConditions.downedSubspaceSerpent = ((BitsByte) ref bitsByte50)[6];
    ModConditions.downedNatureConstruct = ((BitsByte) ref bitsByte50)[7];
    BitsByte bitsByte51 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedEarthenConstruct = ((BitsByte) ref bitsByte51)[0];
    ModConditions.downedPermafrostConstruct = ((BitsByte) ref bitsByte51)[1];
    ModConditions.downedTidalConstruct = ((BitsByte) ref bitsByte51)[2];
    ModConditions.downedOtherworldlyConstruct = ((BitsByte) ref bitsByte51)[3];
    ModConditions.downedEvilConstruct = ((BitsByte) ref bitsByte51)[4];
    ModConditions.downedInfernoConstruct = ((BitsByte) ref bitsByte51)[5];
    ModConditions.downedChaosConstruct = ((BitsByte) ref bitsByte51)[6];
    ModConditions.downedNatureSpirit = ((BitsByte) ref bitsByte51)[7];
    BitsByte bitsByte52 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedEarthenSpirit = ((BitsByte) ref bitsByte52)[0];
    ModConditions.downedPermafrostSpirit = ((BitsByte) ref bitsByte52)[1];
    ModConditions.downedTidalSpirit = ((BitsByte) ref bitsByte52)[2];
    ModConditions.downedOtherworldlySpirit = ((BitsByte) ref bitsByte52)[3];
    ModConditions.downedEvilSpirit = ((BitsByte) ref bitsByte52)[4];
    ModConditions.downedInfernoSpirit = ((BitsByte) ref bitsByte52)[5];
    ModConditions.downedChaosSpirit = ((BitsByte) ref bitsByte52)[6];
    BitsByte bitsByte53 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedDecree = ((BitsByte) ref bitsByte53)[0];
    ModConditions.downedFlamingPumpkin = ((BitsByte) ref bitsByte53)[1];
    ModConditions.downedZombiePiglinBrute = ((BitsByte) ref bitsByte53)[2];
    ModConditions.downedJensenTheGrandHarpy = ((BitsByte) ref bitsByte53)[3];
    ModConditions.downedAraneas = ((BitsByte) ref bitsByte53)[4];
    ModConditions.downedHarpyQueenRaynare = ((BitsByte) ref bitsByte53)[5];
    ModConditions.downedPrimordia = ((BitsByte) ref bitsByte53)[6];
    ModConditions.downedAbaddon = ((BitsByte) ref bitsByte53)[7];
    BitsByte bitsByte54 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedAraghur = ((BitsByte) ref bitsByte54)[0];
    ModConditions.downedLostSiblings = ((BitsByte) ref bitsByte54)[1];
    ModConditions.downedErazor = ((BitsByte) ref bitsByte54)[2];
    ModConditions.downedNihilus = ((BitsByte) ref bitsByte54)[3];
    BitsByte bitsByte55 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedExodygen = ((BitsByte) ref bitsByte55)[0];
    BitsByte bitsByte56 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedScarabeus = ((BitsByte) ref bitsByte56)[0];
    ModConditions.downedMoonJellyWizard = ((BitsByte) ref bitsByte56)[1];
    ModConditions.downedVinewrathBane = ((BitsByte) ref bitsByte56)[2];
    ModConditions.downedAncientAvian = ((BitsByte) ref bitsByte56)[3];
    ModConditions.downedStarplateVoyager = ((BitsByte) ref bitsByte56)[4];
    ModConditions.downedInfernon = ((BitsByte) ref bitsByte56)[5];
    ModConditions.downedDusking = ((BitsByte) ref bitsByte56)[6];
    ModConditions.downedAtlas = ((BitsByte) ref bitsByte56)[7];
    BitsByte bitsByte57 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedJellyDeluge = ((BitsByte) ref bitsByte57)[0];
    ModConditions.downedTide = ((BitsByte) ref bitsByte57)[1];
    ModConditions.downedMysticMoon = ((BitsByte) ref bitsByte57)[2];
    BitsByte bitsByte58 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedSpookySpirit = ((BitsByte) ref bitsByte58)[0];
    ModConditions.downedRotGourd = ((BitsByte) ref bitsByte58)[1];
    ModConditions.downedMoco = ((BitsByte) ref bitsByte58)[2];
    ModConditions.downedDaffodil = ((BitsByte) ref bitsByte58)[3];
    ModConditions.downedOrroBoro = ((BitsByte) ref bitsByte58)[4];
    ModConditions.downedBigBone = ((BitsByte) ref bitsByte58)[5];
    BitsByte bitsByte59 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedAuroracle = ((BitsByte) ref bitsByte59)[0];
    ModConditions.downedCeiros = ((BitsByte) ref bitsByte59)[1];
    ModConditions.downedGlassweaver = ((BitsByte) ref bitsByte59)[2];
    BitsByte bitsByte60 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedVagrantofSpace = ((BitsByte) ref bitsByte60)[0];
    ModConditions.downedThespian = ((BitsByte) ref bitsByte60)[1];
    ModConditions.downedDioskouroi = ((BitsByte) ref bitsByte60)[2];
    ModConditions.downedNalhaun = ((BitsByte) ref bitsByte60)[3];
    ModConditions.downedStarfarers = ((BitsByte) ref bitsByte60)[4];
    ModConditions.downedPenthesilea = ((BitsByte) ref bitsByte60)[5];
    ModConditions.downedArbitration = ((BitsByte) ref bitsByte60)[6];
    ModConditions.downedWarriorOfLight = ((BitsByte) ref bitsByte60)[7];
    BitsByte bitsByte61 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedTsukiyomi = ((BitsByte) ref bitsByte61)[0];
    BitsByte bitsByte62 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedAncientHusk = ((BitsByte) ref bitsByte62)[0];
    ModConditions.downedOverloadedScandrone = ((BitsByte) ref bitsByte62)[1];
    ModConditions.downedPainbringer = ((BitsByte) ref bitsByte62)[2];
    BitsByte bitsByte63 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedHarbingerOfAnnihilation = ((BitsByte) ref bitsByte63)[0];
    ModConditions.downedFlyingTerror = ((BitsByte) ref bitsByte63)[1];
    ModConditions.downedStoneMantaRay = ((BitsByte) ref bitsByte63)[2];
    ModConditions.downedBloodweaver = ((BitsByte) ref bitsByte63)[3];
    BitsByte bitsByte64 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedInfectedIncarnate = ((BitsByte) ref bitsByte64)[0];
    ModConditions.downedTidalTitan = ((BitsByte) ref bitsByte64)[1];
    ModConditions.downedDunestock = ((BitsByte) ref bitsByte64)[2];
    ModConditions.downedHexedConstructor = ((BitsByte) ref bitsByte64)[3];
    ModConditions.downedShadowcrawler = ((BitsByte) ref bitsByte64)[4];
    ModConditions.downedPrototypeI = ((BitsByte) ref bitsByte64)[5];
    BitsByte bitsByte65 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedGraniteOvergrowth = ((BitsByte) ref bitsByte65)[0];
    ModConditions.downedBeholder = ((BitsByte) ref bitsByte65)[1];
    BitsByte bitsByte66 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedDredger = ((BitsByte) ref bitsByte66)[0];
    ModConditions.downedCharcoolSnowman = ((BitsByte) ref bitsByte66)[1];
    ModConditions.downedCosmicMenace = ((BitsByte) ref bitsByte66)[2];
    BitsByte bitsByte67 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedEvilFlyingBlade = ((BitsByte) ref bitsByte67)[0];
    BitsByte bitsByte68 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedColossalCarnage = ((BitsByte) ref bitsByte68)[0];
    ModConditions.downedYurnero = ((BitsByte) ref bitsByte68)[1];
    BitsByte bitsByte69 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedStormCloud = ((BitsByte) ref bitsByte69)[0];
    ModConditions.downedGrandAntlion = ((BitsByte) ref bitsByte69)[1];
    ModConditions.downedGemstoneElemental = ((BitsByte) ref bitsByte69)[2];
    ModConditions.downedMoonlightDragonfly = ((BitsByte) ref bitsByte69)[3];
    ModConditions.downedDreadnaught = ((BitsByte) ref bitsByte69)[4];
    ModConditions.downedMosquitoMonarch = ((BitsByte) ref bitsByte69)[5];
    ModConditions.downedAnarchulesBeetle = ((BitsByte) ref bitsByte69)[6];
    ModConditions.downedChaosbringer = ((BitsByte) ref bitsByte69)[7];
    BitsByte bitsByte70 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedPaladinSpirit = ((BitsByte) ref bitsByte70)[0];
    BitsByte bitsByte71 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedManaflora = ((BitsByte) ref bitsByte71)[0];
    BitsByte bitsByte72 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedNoxus = ((BitsByte) ref bitsByte72)[0];
    ModConditions.downedNamelessDeityOfLight = ((BitsByte) ref bitsByte72)[1];
    BitsByte bitsByte73 = BitsByte.op_Implicit(reader.ReadByte());
    ModConditions.downedDirtball = ((BitsByte) ref bitsByte73)[0];
    ModConditions.downedMetelord = ((BitsByte) ref bitsByte73)[1];
    ModConditions.downedAdeneb = ((BitsByte) ref bitsByte73)[2];
    ModConditions.downedEldritchJellyfish = ((BitsByte) ref bitsByte73)[3];
    ModConditions.downedSaburRex = ((BitsByte) ref bitsByte73)[4];
    for (int index = 0; index < ModConditions.DownedBoss.Length; ++index)
    {
      int num = index % 8;
      if (num == 0)
        bitsByte73 = BitsByte.op_Implicit(reader.ReadByte());
      ModConditions.DownedBoss[index] = ((BitsByte) ref bitsByte73)[num];
    }
  }

  public static void ResetDowned()
  {
    ModConditions.ItemHasBeenOwned = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
    ModConditions.downedDreadnautilus = false;
    ModConditions.downedMartianSaucer = false;
    ModConditions.downedBloodMoon = false;
    ModConditions.downedEclipse = false;
    ModConditions.downedLunarEvent = false;
    ModConditions.beenThroughNight = false;
    ModConditions.beenToPurity = false;
    ModConditions.beenToCavernsOrUnderground = false;
    ModConditions.beenToUnderworld = false;
    ModConditions.beenToSky = false;
    ModConditions.beenToSnow = false;
    ModConditions.beenToDesert = false;
    ModConditions.beenToOcean = false;
    ModConditions.beenToJungle = false;
    ModConditions.beenToMushroom = false;
    ModConditions.beenToCorruption = false;
    ModConditions.beenToCrimson = false;
    ModConditions.beenToHallow = false;
    ModConditions.beenToTemple = false;
    ModConditions.beenToDungeon = false;
    ModConditions.beenToAether = false;
    ModConditions.talkedToSkeletonMerchant = false;
    ModConditions.talkedToTravelingMerchant = false;
    ModConditions.downedCrabson = false;
    ModConditions.downedOmegaStarite = false;
    ModConditions.downedDustDevil = false;
    ModConditions.downedRedSprite = false;
    ModConditions.downedSpaceSquid = false;
    ModConditions.downedHyperStarite = false;
    ModConditions.downedUltraStarite = false;
    ModConditions.downedDemonSiege = false;
    ModConditions.downedGlimmer = false;
    ModConditions.downedGaleStreams = false;
    ModConditions.beenToCrabCrevice = false;
    ModConditions.downedSlayerOfEvil = false;
    ModConditions.downedSATLA = false;
    ModConditions.downedDrFetus = false;
    ModConditions.downedSlimesHope = false;
    ModConditions.downedPoliticianSlime = false;
    ModConditions.downedAncientTrio = false;
    ModConditions.downedLavalGolem = false;
    ModConditions.downedAntony = false;
    ModConditions.downedBunnyZeppelin = false;
    ModConditions.downedOkiku = false;
    ModConditions.downedHarpyAirforce = false;
    ModConditions.downedIsaac = false;
    ModConditions.downedAncientGuardian = false;
    ModConditions.downedHeroicSlime = false;
    ModConditions.downedHoloSlime = false;
    ModConditions.downedSecurityBot = false;
    ModConditions.downedUndeadChef = false;
    ModConditions.downedGuardianOfFrost = false;
    ModConditions.downedSoulHarvester = false;
    ModConditions.downedTreeToad = false;
    ModConditions.downedSeseKitsugai = false;
    ModConditions.downedEyeOfTheStorm = false;
    ModConditions.downedFrigidius = false;
    ModConditions.downedCoreBoss = false;
    ModConditions.downedDesertScourge = false;
    ModConditions.downedCrabulon = false;
    ModConditions.downedHiveMind = false;
    ModConditions.downedPerforators = false;
    ModConditions.downedSlimeGod = false;
    ModConditions.downedCryogen = false;
    ModConditions.downedAquaticScourge = false;
    ModConditions.downedBrimstoneElemental = false;
    ModConditions.downedCalamitasClone = false;
    ModConditions.downedLeviathanAndAnahita = false;
    ModConditions.downedAstrumAureus = false;
    ModConditions.downedPlaguebringerGoliath = false;
    ModConditions.downedRavager = false;
    ModConditions.downedAstrumDeus = false;
    ModConditions.downedProfanedGuardians = false;
    ModConditions.downedDragonfolly = false;
    ModConditions.downedProvidence = false;
    ModConditions.downedStormWeaver = false;
    ModConditions.downedCeaselessVoid = false;
    ModConditions.downedSignus = false;
    ModConditions.downedPolterghast = false;
    ModConditions.downedOldDuke = false;
    ModConditions.downedDevourerOfGods = false;
    ModConditions.downedYharon = false;
    ModConditions.downedExoMechs = false;
    ModConditions.downedSupremeCalamitas = false;
    ModConditions.downedGiantClam = false;
    ModConditions.downedGreatSandShark = false;
    ModConditions.downedCragmawMire = false;
    ModConditions.downedNuclearTerror = false;
    ModConditions.downedMauler = false;
    ModConditions.downedEidolonWyrm = false;
    ModConditions.downedAcidRain1 = false;
    ModConditions.downedAcidRain2 = false;
    ModConditions.downedBossRush = false;
    ModConditions.beenToCrags = false;
    ModConditions.beenToAstral = false;
    ModConditions.beenToSunkenSea = false;
    ModConditions.beenToSulphurSea = false;
    ModConditions.beenToAbyss = false;
    ModConditions.beenToAbyssLayer1 = false;
    ModConditions.beenToAbyssLayer2 = false;
    ModConditions.beenToAbyssLayer3 = false;
    ModConditions.beenToAbyssLayer4 = false;
    ModConditions.downedWulfrumExcavator = false;
    ModConditions.downedCruiser = false;
    ModConditions.beenToAstralBlight = false;
    ModConditions.downedAstrageldon = false;
    ModConditions.downedClamitas = false;
    ModConditions.downedPyrogen = false;
    ModConditions.downedWallOfBronze = false;
    ModConditions.beenToConfection = false;
    ModConditions.downedLepus = false;
    ModConditions.downedTurkor = false;
    ModConditions.downedOcram = false;
    ModConditions.downedRediancie = false;
    ModConditions.downedBabyIceDragon = false;
    ModConditions.downedSlimeEmperor = false;
    ModConditions.downedBloodiancie = false;
    ModConditions.downedThunderveinDragon = false;
    ModConditions.downedNightmarePlantera = false;
    ModConditions.beenToDepths = false;
    ModConditions.downedLifeGuardian = false;
    ModConditions.downedManaGuardian = false;
    ModConditions.downedMeteorExcavator = false;
    ModConditions.downedMeteorAnnihilator = false;
    ModConditions.downedHellfireSerpent = false;
    ModConditions.downedWitheredAcornSpirit = false;
    ModConditions.downedGoblinSorcererChieftain = false;
    ModConditions.downedGalahis = false;
    ModConditions.downedCreation = false;
    ModConditions.downedDestruction = false;
    ModConditions.downedBlightKing = false;
    ModConditions.downedGardener = false;
    ModConditions.downedGlaciation = false;
    ModConditions.downedHandOfCthulhu = false;
    ModConditions.downedCursePreacher = false;
    ModConditions.beenToJadeLake = false;
    ModConditions.downedEffulgence = false;
    ModConditions.downedIceLich = false;
    ModConditions.downedBacteriumPrime = false;
    ModConditions.downedDesertBeak = false;
    ModConditions.downedKingSting = false;
    ModConditions.downedMechasting = false;
    ModConditions.downedPhantasm = false;
    ModConditions.beenToContagion = false;
    ModConditions.downedTrojanSquirrel = false;
    ModConditions.downedCursedCoffin = false;
    ModConditions.downedDeviantt = false;
    ModConditions.downedBanishedBaron = false;
    ModConditions.downedLifelight = false;
    ModConditions.downedEridanus = false;
    ModConditions.downedAbominationn = false;
    ModConditions.downedMutant = false;
    ModConditions.downedAlphaFrostjaw = false;
    ModConditions.downedSanguineElemental = false;
    ModConditions.beenToDread = false;
    ModConditions.downedLad = false;
    ModConditions.downedHornlitz = false;
    ModConditions.downedSnowDon = false;
    ModConditions.downedStoffie = false;
    ModConditions.downedLilyWhite = false;
    ModConditions.downedRumia = false;
    ModConditions.downedEternityLarva = false;
    ModConditions.downedNazrin = false;
    ModConditions.downedHinaKagiyama = false;
    ModConditions.downedSekibanki = false;
    ModConditions.downedSeiran = false;
    ModConditions.downedNitoriKawashiro = false;
    ModConditions.downedMedicineMelancholy = false;
    ModConditions.downedCirno = false;
    ModConditions.downedMinamitsuMurasa = false;
    ModConditions.downedAliceMargatroid = false;
    ModConditions.downedSakuyaIzayoi = false;
    ModConditions.downedSeijaKijin = false;
    ModConditions.downedMayumiJoutouguu = false;
    ModConditions.downedToyosatomimiNoMiko = false;
    ModConditions.downedKaguyaHouraisan = false;
    ModConditions.downedUtsuhoReiuji = false;
    ModConditions.downedTenshiHinanawi = false;
    ModConditions.downedKisume = false;
    ModConditions.downedTrerios = false;
    ModConditions.downedMagmaEye = false;
    ModConditions.downedJack = false;
    ModConditions.downedAcheron = false;
    ModConditions.downedMarquisMoonsquid = false;
    ModConditions.downedPriestessRod = false;
    ModConditions.downedDiver = false;
    ModConditions.downedMotherbrain = false;
    ModConditions.downedWallOfShadow = false;
    ModConditions.downedSunSlimeGod = false;
    ModConditions.downedOverwatcher = false;
    ModConditions.downedLifebringer = false;
    ModConditions.downedMaterealizer = false;
    ModConditions.downedScarabBelief = false;
    ModConditions.downedWorldsEndWhale = false;
    ModConditions.downedSon = false;
    ModConditions.downedCaveOrdeal = false;
    ModConditions.downedCorruptOrdeal = false;
    ModConditions.downedCrimsonOrdeal = false;
    ModConditions.downedDesertOrdeal = false;
    ModConditions.downedForestOrdeal = false;
    ModConditions.downedHallowOrdeal = false;
    ModConditions.downedJungleOrdeal = false;
    ModConditions.downedSkyOrdeal = false;
    ModConditions.downedSnowOrdeal = false;
    ModConditions.downedUnderworldOrdeal = false;
    ModConditions.beenToHomewardAbyss = false;
    ModConditions.downedGoozma = false;
    ModConditions.downedBereftVassal = false;
    ModConditions.beenToProfanedGardens = false;
    ModConditions.downedStoneGuardian = false;
    ModConditions.downedCommanderGintzia = false;
    ModConditions.downedSunStalker = false;
    ModConditions.downedPumpkinJack = false;
    ModConditions.downedForgottenPuppetDaedus = false;
    ModConditions.downedDreadMire = false;
    ModConditions.downedSingularityFragment = false;
    ModConditions.downedVerlia = false;
    ModConditions.downedIrradia = false;
    ModConditions.downedSylia = false;
    ModConditions.downedFenix = false;
    ModConditions.downedBlazingSerpent = false;
    ModConditions.downedCogwork = false;
    ModConditions.downedWaterCogwork = false;
    ModConditions.downedWaterJellyfish = false;
    ModConditions.downedSparn = false;
    ModConditions.downedPandorasFlamebox = false;
    ModConditions.downedSTARBOMBER = false;
    ModConditions.downedGintzeArmy = false;
    ModConditions.beenToLunarVeilAbyss = false;
    ModConditions.beenToAcid = false;
    ModConditions.beenToAurelus = false;
    ModConditions.beenToFable = false;
    ModConditions.beenToGovheilCastle = false;
    ModConditions.beenToCathedral = false;
    ModConditions.beenToMarrowSurface = false;
    ModConditions.beenToMorrowUnderground = false;
    ModConditions.downedBritzz = false;
    ModConditions.downedTheAlchemist = false;
    ModConditions.downedCarnagePillar = false;
    ModConditions.downedVoidDigger = false;
    ModConditions.downedPrinceSlime = false;
    ModConditions.downedTriplets = false;
    ModConditions.downedJungleDefenders = false;
    ModConditions.downedSt4sys = false;
    ModConditions.downedTerminator = false;
    ModConditions.downedCaretaker = false;
    ModConditions.downedSiegeEngine = false;
    ModConditions.downedSuperVoltaicMotherSlime = false;
    ModConditions.downedTorizo = false;
    ModConditions.downedSerris = false;
    ModConditions.downedKraid = false;
    ModConditions.downedPhantoon = false;
    ModConditions.downedOmegaPirate = false;
    ModConditions.downedNightmare = false;
    ModConditions.downedGoldenTorizo = false;
    ModConditions.downedOphiopede = false;
    ModConditions.downedOphiocoon = false;
    ModConditions.downedOphiofly = false;
    ModConditions.downedStormCloudfish = false;
    ModConditions.downedStarConstruct = false;
    ModConditions.downedGigabat = false;
    ModConditions.downedSunPixie = false;
    ModConditions.downedEsophage = false;
    ModConditions.downedConvectiveWanderer = false;
    ModConditions.downedForestGuardian = false;
    ModConditions.downedCryoGuardian = false;
    ModConditions.downedPrimordialWorm = false;
    ModConditions.downedTheGuardianOfHell = false;
    ModConditions.downedVoid = false;
    ModConditions.downedArmagem = false;
    ModConditions.downedPolarExterminator = false;
    ModConditions.downedDivineLight = false;
    ModConditions.downedAncientMachine = false;
    ModConditions.downedNoehtnap = false;
    ModConditions.downedHydra = false;
    ModConditions.downedImperious = false;
    ModConditions.downedRuneGhost = false;
    ModConditions.downedInvaderBattleship = false;
    ModConditions.downedInvaderNoehtnap = false;
    ModConditions.downedOLORD = false;
    ModConditions.downedGreatTyrannosaurus = false;
    ModConditions.downedDinoMilitia = false;
    ModConditions.downedInvaders = false;
    ModConditions.beenToSkyFortress = false;
    ModConditions.downedThorn = false;
    ModConditions.downedErhan = false;
    ModConditions.downedKeeper = false;
    ModConditions.downedSeedOfInfection = false;
    ModConditions.downedKingSlayerIII = false;
    ModConditions.downedOmegaCleaver = false;
    ModConditions.downedOmegaGigapora = false;
    ModConditions.downedOmegaObliterator = false;
    ModConditions.downedPatientZero = false;
    ModConditions.downedAkka = false;
    ModConditions.downedUkko = false;
    ModConditions.downedAncientDeityDuo = false;
    ModConditions.downedNebuleus = false;
    ModConditions.downedFowlEmperor = false;
    ModConditions.downedCockatrice = false;
    ModConditions.downedBasan = false;
    ModConditions.downedSkullDigger = false;
    ModConditions.downedEaglecrestGolem = false;
    ModConditions.downedCalavia = false;
    ModConditions.downedTheJanitor = false;
    ModConditions.downedIrradiatedBehemoth = false;
    ModConditions.downedBlisterface = false;
    ModConditions.downedProtectorVolt = false;
    ModConditions.downedMACEProject = false;
    ModConditions.downedFowlMorning = false;
    ModConditions.downedRaveyard = false;
    ModConditions.beenToLab = false;
    ModConditions.beenToWasteland = false;
    ModConditions.downedGlowmoth = false;
    ModConditions.downedPutridPinky = false;
    ModConditions.downedPharaohsCurse = false;
    ModConditions.downedAdvisor = false;
    ModConditions.downedPolaris = false;
    ModConditions.downedLux = false;
    ModConditions.downedSubspaceSerpent = false;
    ModConditions.downedNatureConstruct = false;
    ModConditions.downedEarthenConstruct = false;
    ModConditions.downedPermafrostConstruct = false;
    ModConditions.downedTidalConstruct = false;
    ModConditions.downedOtherworldlyConstruct = false;
    ModConditions.downedEvilConstruct = false;
    ModConditions.downedInfernoConstruct = false;
    ModConditions.downedChaosConstruct = false;
    ModConditions.downedNatureSpirit = false;
    ModConditions.downedEarthenSpirit = false;
    ModConditions.downedPermafrostSpirit = false;
    ModConditions.downedTidalSpirit = false;
    ModConditions.downedOtherworldlySpirit = false;
    ModConditions.downedEvilSpirit = false;
    ModConditions.downedInfernoSpirit = false;
    ModConditions.downedChaosSpirit = false;
    ModConditions.beenToPyramid = false;
    ModConditions.beenToPlanetarium = false;
    ModConditions.downedDecree = false;
    ModConditions.downedFlamingPumpkin = false;
    ModConditions.downedZombiePiglinBrute = false;
    ModConditions.downedJensenTheGrandHarpy = false;
    ModConditions.downedAraneas = false;
    ModConditions.downedHarpyQueenRaynare = false;
    ModConditions.downedPrimordia = false;
    ModConditions.downedAbaddon = false;
    ModConditions.downedAraghur = false;
    ModConditions.downedLostSiblings = false;
    ModConditions.downedErazor = false;
    ModConditions.downedNihilus = false;
    ModConditions.beenToCinderForest = false;
    ModConditions.downedExodygen = false;
    ModConditions.downedScarabeus = false;
    ModConditions.downedMoonJellyWizard = false;
    ModConditions.downedVinewrathBane = false;
    ModConditions.downedAncientAvian = false;
    ModConditions.downedStarplateVoyager = false;
    ModConditions.downedInfernon = false;
    ModConditions.downedDusking = false;
    ModConditions.downedAtlas = false;
    ModConditions.downedJellyDeluge = false;
    ModConditions.downedTide = false;
    ModConditions.downedMysticMoon = false;
    ModConditions.beenToBriar = false;
    ModConditions.beenToSpirit = false;
    ModConditions.downedSpookySpirit = false;
    ModConditions.downedRotGourd = false;
    ModConditions.downedMoco = false;
    ModConditions.downedDaffodil = false;
    ModConditions.downedOrroBoro = false;
    ModConditions.downedOrro = false;
    ModConditions.downedBoro = false;
    ModConditions.downedBigBone = false;
    ModConditions.beenToSpookyForest = false;
    ModConditions.beenToSpookyUnderground = false;
    ModConditions.beenToEyeValley = false;
    ModConditions.beenToSpiderCave = false;
    ModConditions.beenToCatacombs = false;
    ModConditions.beenToCemetery = false;
    ModConditions.downedAuroracle = false;
    ModConditions.downedCeiros = false;
    ModConditions.downedGlassweaver = false;
    ModConditions.beenToAuroracleTemple = false;
    ModConditions.beenToVitricDesert = false;
    ModConditions.beenToVitricTemple = false;
    ModConditions.downedVagrantofSpace = false;
    ModConditions.downedThespian = false;
    ModConditions.downedDioskouroi = false;
    ModConditions.downedNalhaun = false;
    ModConditions.downedStarfarers = false;
    ModConditions.downedPenthesilea = false;
    ModConditions.downedArbitration = false;
    ModConditions.downedWarriorOfLight = false;
    ModConditions.downedTsukiyomi = false;
    ModConditions.downedAncientHusk = false;
    ModConditions.downedOverloadedScandrone = false;
    ModConditions.downedPainbringer = false;
    ModConditions.downedHarbingerOfAnnihilation = false;
    ModConditions.downedFlyingTerror = false;
    ModConditions.downedStoneMantaRay = false;
    ModConditions.downedBloodweaver = false;
    ModConditions.downedInfectedIncarnate = false;
    ModConditions.downedTidalTitan = false;
    ModConditions.downedDunestock = false;
    ModConditions.downedShadowcrawler = false;
    ModConditions.downedHexedConstructor = false;
    ModConditions.downedPrototypeI = false;
    ModConditions.downedGrandThunderBird = false;
    ModConditions.downedQueenJellyfish = false;
    ModConditions.downedViscount = false;
    ModConditions.downedGraniteEnergyStorm = false;
    ModConditions.downedBuriedChampion = false;
    ModConditions.downedBoreanStrider = false;
    ModConditions.downedFallenBeholder = false;
    ModConditions.downedLich = false;
    ModConditions.downedForgottenOne = false;
    ModConditions.downedPrimordials = false;
    ModConditions.downedPatchWerk = false;
    ModConditions.downedCorpseBloom = false;
    ModConditions.downedIllusionist = false;
    ModConditions.beenToAquaticDepths = false;
    ModConditions.downedGraniteOvergrowth = false;
    ModConditions.downedBeholder = false;
    ModConditions.downedDredger = false;
    ModConditions.downedCharcoolSnowman = false;
    ModConditions.downedCosmicMenace = false;
    ModConditions.downedEvilFlyingBlade = false;
    ModConditions.downedColossalCarnage = false;
    ModConditions.downedYurnero = false;
    ModConditions.beenToVerdant = false;
    ModConditions.downedStormCloud = false;
    ModConditions.downedGrandAntlion = false;
    ModConditions.downedGemstoneElemental = false;
    ModConditions.downedMoonlightDragonfly = false;
    ModConditions.downedDreadnaught = false;
    ModConditions.downedMosquitoMonarch = false;
    ModConditions.downedAnarchulesBeetle = false;
    ModConditions.downedChaosbringer = false;
    ModConditions.downedPaladinSpirit = false;
    ModConditions.downedManaflora = false;
    ModConditions.downedNoxus = false;
    ModConditions.downedNamelessDeityOfLight = false;
    ModConditions.downedDirtball = false;
    ModConditions.downedMetelord = false;
    ModConditions.downedAdeneb = false;
    ModConditions.downedEldritchJellyfish = false;
    ModConditions.downedSaburRex = false;
    for (int index = 0; index < ModConditions.DownedBoss.Length; ++index)
      ModConditions.DownedBoss[index] = false;
  }

  public static void LoadSupportedMods()
  {
    Mod mod1;
    ModConditions.aequusLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Aequus", ref mod1);
    ModConditions.aequusMod = mod1;
    Mod mod2;
    ModConditions.afkpetsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("AFKPETS", ref mod2);
    ModConditions.afkpetsMod = mod2;
    Mod mod3;
    ModConditions.amuletOfManyMinionsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("AmuletOfManyMinions", ref mod3);
    ModConditions.amuletOfManyMinionsMod = mod3;
    Mod mod4;
    ModConditions.arbourLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Arbour", ref mod4);
    ModConditions.arbourMod = mod4;
    Mod mod5;
    ModConditions.assortedCrazyThingsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("AssortedCrazyThings", ref mod5);
    ModConditions.assortedCrazyThingsMod = mod5;
    Mod mod6;
    ModConditions.awfulGarbageLoaded = Terraria.ModLoader.ModLoader.TryGetMod("AwfulGarbageMod", ref mod6);
    ModConditions.awfulGarbageMod = mod6;
    Mod mod7;
    ModConditions.blocksArsenalLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Arsenal_Mod", ref mod7);
    ModConditions.blocksArsenalMod = mod7;
    Mod mod8;
    ModConditions.blocksArtificerLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ArtificerMod", ref mod8);
    ModConditions.blocksArtificerMod = mod8;
    Mod mod9;
    ModConditions.blocksCoreBossLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CorruptionBoss", ref mod9);
    ModConditions.blocksCoreBossMod = mod9;
    Mod mod10;
    ModConditions.blocksInfoAccessoriesLoaded = Terraria.ModLoader.ModLoader.TryGetMod("BInfoAcc", ref mod10);
    ModConditions.blocksInfoAccessoriesMod = mod10;
    Mod mod11;
    ModConditions.blocksThrowerLoaded = Terraria.ModLoader.ModLoader.TryGetMod("BCThrower", ref mod11);
    ModConditions.blocksThrowerMod = mod11;
    Mod mod12;
    ModConditions.bombusApisLoaded = Terraria.ModLoader.ModLoader.TryGetMod("BombusApisBee", ref mod12);
    ModConditions.bombusApisMod = mod12;
    Mod mod13;
    ModConditions.buffariaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Buffaria", ref mod13);
    ModConditions.buffariaMod = mod13;
    Mod mod14;
    ModConditions.calamityLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CalamityMod", ref mod14);
    ModConditions.calamityMod = mod14;
    Mod mod15;
    ModConditions.calamityCommunityRemixLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CalRemix", ref mod15);
    ModConditions.calamityCommunityRemixMod = mod15;
    Mod mod16;
    ModConditions.calamityEntropyLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CalamityEntropy", ref mod16);
    ModConditions.calamityEntropyMod = mod16;
    Mod mod17;
    ModConditions.calamityOverhaulLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CalamityOverhaul", ref mod17);
    ModConditions.calamityOverhaulMod = mod17;
    Mod mod18;
    ModConditions.calamityVanitiesLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CalValEX", ref mod18);
    ModConditions.calamityVanitiesMod = mod18;
    Mod mod19;
    ModConditions.captureDiscsClassLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CaptureDiscClass", ref mod19);
    ModConditions.captureDiscsClassMod = mod19;
    Mod mod20;
    ModConditions.catalystLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CatalystMod", ref mod20);
    ModConditions.catalystMod = mod20;
    Mod mod21;
    ModConditions.cerebralLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CerebralMod", ref mod21);
    ModConditions.cerebralMod = mod21;
    Mod mod22;
    ModConditions.clamityAddonLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Clamity", ref mod22);
    ModConditions.clamityAddonMod = mod22;
    Mod mod23;
    ModConditions.clickerClassLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ClickerClass", ref mod23);
    ModConditions.clickerClassMod = mod23;
    Mod mod24;
    ModConditions.confectionRebakedLoaded = Terraria.ModLoader.ModLoader.TryGetMod("TheConfectionRebirth", ref mod24);
    ModConditions.confectionRebakedMod = mod24;
    Mod mod25;
    ModConditions.consolariaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Consolaria", ref mod25);
    ModConditions.consolariaMod = mod25;
    Mod mod26;
    ModConditions.coraliteLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Coralite", ref mod26);
    ModConditions.coraliteMod = mod26;
    Mod mod27;
    ModConditions.crystalDragonsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CrystalDragons", ref mod27);
    ModConditions.crystalDragonsMod = mod27;
    Mod mod28;
    ModConditions.depthsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("TheDepths", ref mod28);
    ModConditions.depthsMod = mod28;
    Mod mod29;
    ModConditions.dormantDawnLoaded = Terraria.ModLoader.ModLoader.TryGetMod("DDmod", ref mod29);
    ModConditions.dormantDawnMod = mod29;
    Mod mod30;
    ModConditions.draedonExpansionLoaded = Terraria.ModLoader.ModLoader.TryGetMod("DraedonExpansion", ref mod30);
    ModConditions.draedonExpansionMod = mod30;
    Mod mod31;
    ModConditions.dragonBallTerrariaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("DBZMODPORT", ref mod31);
    ModConditions.dragonBallTerrariaMod = mod31;
    Mod mod32;
    ModConditions.echoesOfTheAncientsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("EchoesoftheAncients", ref mod32);
    ModConditions.echoesOfTheAncientsMod = mod32;
    Mod mod33;
    ModConditions.edorbisLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Edorbis", ref mod33);
    ModConditions.edorbisMod = mod33;
    Mod mod34;
    ModConditions.enchantedMoonsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("BlueMoon", ref mod34);
    ModConditions.enchantedMoonsMod = mod34;
    Mod mod35;
    ModConditions.everjadeLoaded = Terraria.ModLoader.ModLoader.TryGetMod("JadeFables", ref mod35);
    ModConditions.everjadeMod = mod35;
    Mod mod36;
    ModConditions.exaltLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ExaltMod", ref mod36);
    ModConditions.exaltMod = mod36;
    Mod mod37;
    ModConditions.excelsiorLoaded = Terraria.ModLoader.ModLoader.TryGetMod("excels", ref mod37);
    ModConditions.excelsiorMod = mod37;
    Mod mod38;
    ModConditions.exxoAvalonOriginsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Avalon", ref mod38);
    ModConditions.exxoAvalonOriginsMod = mod38;
    Mod mod39;
    ModConditions.fargosMutantLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Fargowiltas", ref mod39);
    ModConditions.fargosMutantMod = mod39;
    Mod mod40;
    ModConditions.fargosSoulsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasSouls", ref mod40);
    ModConditions.fargosSoulsMod = mod40;
    Mod mod41;
    ModConditions.fargosSoulsDLCLoaded = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasCrossmod", ref mod41);
    ModConditions.fargosSoulsDLCMod = mod41;
    Mod mod42;
    ModConditions.fargosSoulsExtrasLoaded = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasSoulsDLC", ref mod42);
    ModConditions.fargosSoulsExtrasMod = mod42;
    Mod mod43;
    ModConditions.fracturesOfPenumbraLoaded = Terraria.ModLoader.ModLoader.TryGetMod("FPenumbra", ref mod43);
    ModConditions.fracturesOfPenumbraMod = mod43;
    Mod mod44;
    ModConditions.furnitureFoodAndFunLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CosmeticVariety", ref mod44);
    ModConditions.furnitureFoodAndFunMod = mod44;
    Mod mod45;
    ModConditions.gameTerrariaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("GMT", ref mod45);
    ModConditions.gameTerrariaMod = mod45;
    Mod mod46;
    ModConditions.gensokyoLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Gensokyo", ref mod46);
    ModConditions.gensokyoMod = mod46;
    Mod mod47;
    ModConditions.gerdsLabLoaded = Terraria.ModLoader.ModLoader.TryGetMod("GMR", ref mod47);
    ModConditions.gerdsLabMod = mod47;
    Mod mod48;
    ModConditions.heartbeatariaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("XDContentMod", ref mod48);
    ModConditions.heartbeatariaMod = mod48;
    Mod mod49;
    ModConditions.homewardJourneyLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ContinentOfJourney", ref mod49);
    ModConditions.homewardJourneyMod = mod49;
    Mod mod50;
    ModConditions.huntOfTheOldGodLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CalamityHunt", ref mod50);
    ModConditions.huntOfTheOldGodMod = mod50;
    Mod mod51;
    ModConditions.infectedQualitiesLoaded = Terraria.ModLoader.ModLoader.TryGetMod("InfectedQualities", ref mod51);
    ModConditions.infectedQualitiesMod = mod51;
    Mod mod52;
    ModConditions.infernumLoaded = Terraria.ModLoader.ModLoader.TryGetMod("InfernumMode", ref mod52);
    ModConditions.infernumMod = mod52;
    Mod mod53;
    ModConditions.luiAFKLoaded = Terraria.ModLoader.ModLoader.TryGetMod("miningcracks_take_on_luiafk", ref mod53);
    ModConditions.luiAFKMod = mod53;
    Mod mod54;
    ModConditions.luiAFKDLCLoaded = Terraria.ModLoader.ModLoader.TryGetMod("UnofficialLuiAFKDLC", ref mod54);
    ModConditions.luiAFKDLCMod = mod54;
    Mod mod55;
    ModConditions.lunarVeilLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Stellamod", ref mod55);
    ModConditions.lunarVeilMod = mod55;
    Mod mod56;
    ModConditions.magicStorageLoaded = Terraria.ModLoader.ModLoader.TryGetMod("MagicStorage", ref mod56);
    ModConditions.magicStorageMod = mod56;
    Mod mod57;
    ModConditions.martainsOrderLoaded = Terraria.ModLoader.ModLoader.TryGetMod("MartainsOrder", ref mod57);
    ModConditions.martainsOrderMod = mod57;
    Mod mod58;
    ModConditions.mechReworkLoaded = Terraria.ModLoader.ModLoader.TryGetMod("PrimeRework", ref mod58);
    ModConditions.mechReworkMod = mod58;
    Mod mod59;
    ModConditions.medialRiftLoaded = Terraria.ModLoader.ModLoader.TryGetMod("MedRift", ref mod59);
    ModConditions.medialRiftMod = mod59;
    Mod mod60;
    ModConditions.metroidLoaded = Terraria.ModLoader.ModLoader.TryGetMod("MetroidMod", ref mod60);
    ModConditions.metroidMod = mod60;
    Mod mod61;
    ModConditions.moomoosUltimateYoyoRevampLoaded = Terraria.ModLoader.ModLoader.TryGetMod("CombinationsMod", ref mod61);
    ModConditions.moomoosUltimateYoyoRevampMod = mod61;
    Mod mod62;
    ModConditions.mrPlagueRacesLoaded = Terraria.ModLoader.ModLoader.TryGetMod("MrPlagueRaces", ref mod62);
    ModConditions.mrPlagueRacesMod = mod62;
    Mod mod63;
    ModConditions.orchidLoaded = Terraria.ModLoader.ModLoader.TryGetMod("OrchidMod", ref mod63);
    ModConditions.orchidMod = mod63;
    Mod mod64;
    ModConditions.ophioidLoaded = Terraria.ModLoader.ModLoader.TryGetMod("OphioidMod", ref mod64);
    ModConditions.ophioidMod = mod64;
    Mod mod65;
    ModConditions.polaritiesLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Polarities", ref mod65);
    ModConditions.polaritiesMod = mod65;
    Mod mod66;
    ModConditions.projectZeroLoaded = Terraria.ModLoader.ModLoader.TryGetMod("FM", ref mod66);
    ModConditions.projectZeroMod = mod66;
    Mod mod67;
    ModConditions.qwertyLoaded = Terraria.ModLoader.ModLoader.TryGetMod("QwertyMod", ref mod67);
    ModConditions.qwertyMod = mod67;
    Mod mod68;
    ModConditions.ragnarokLoaded = Terraria.ModLoader.ModLoader.TryGetMod("RagnarokMod", ref mod68);
    ModConditions.ragnarokMod = mod68;
    Mod mod69;
    ModConditions.redemptionLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Redemption", ref mod69);
    ModConditions.redemptionMod = mod69;
    Mod mod70;
    ModConditions.reforgedLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ReforgeOverhaul", ref mod70);
    ModConditions.reforgedMod = mod70;
    Mod mod71;
    ModConditions.remnantsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Remnants", ref mod71);
    ModConditions.remnantsMod = mod71;
    Mod mod72;
    ModConditions.ruptureLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Rupture", ref mod72);
    ModConditions.ruptureMod = mod72;
    Mod mod73;
    ModConditions.secretsOfTheShadowsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("SOTS", ref mod73);
    ModConditions.secretsOfTheShadowsMod = mod73;
    Mod mod74;
    ModConditions.shadowsOfAbaddonLoaded = Terraria.ModLoader.ModLoader.TryGetMod("SacredTools", ref mod74);
    ModConditions.shadowsOfAbaddonMod = mod74;
    Mod mod75;
    ModConditions.sloomeLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Bloopsitems", ref mod75);
    ModConditions.sloomeMod = mod75;
    Mod mod76;
    ModConditions.spiritLoaded = Terraria.ModLoader.ModLoader.TryGetMod("SpiritMod", ref mod76);
    ModConditions.spiritMod = mod76;
    Mod mod77;
    ModConditions.spookyLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Spooky", ref mod77);
    ModConditions.spookyMod = mod77;
    Mod mod78;
    ModConditions.starlightRiverLoaded = Terraria.ModLoader.ModLoader.TryGetMod("StarlightRiver", ref mod78);
    ModConditions.starlightRiverMod = mod78;
    Mod mod79;
    ModConditions.starsAboveLoaded = Terraria.ModLoader.ModLoader.TryGetMod("StarsAbove", ref mod79);
    ModConditions.starsAboveMod = mod79;
    Mod mod80;
    ModConditions.stormsAdditionsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("StormDiversMod", ref mod80);
    ModConditions.stormsAdditionsMod = mod80;
    Mod mod81;
    ModConditions.stramsClassesLoaded = Terraria.ModLoader.ModLoader.TryGetMod("StramClasses", ref mod81);
    ModConditions.stramsClassesMod = mod81;
    Mod mod82;
    ModConditions.supernovaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("SupernovaMod", ref mod82);
    ModConditions.supernovaMod = mod82;
    Mod mod83;
    ModConditions.terrorbornLoaded = Terraria.ModLoader.ModLoader.TryGetMod("TerrorbornMod", ref mod83);
    ModConditions.terrorbornMod = mod83;
    Mod mod84;
    ModConditions.thoriumLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ThoriumMod", ref mod84);
    ModConditions.thoriumMod = mod84;
    Mod mod85;
    ModConditions.thoriumBossReworkLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ThoriumRework", ref mod85);
    ModConditions.thoriumBossReworkMod = mod85;
    Mod mod86;
    ModConditions.exhaustionDisablerLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ExhaustionDisabler", ref mod86);
    ModConditions.exhaustionDisablerMod = mod86;
    Mod mod87;
    ModConditions.traeLoaded = Terraria.ModLoader.ModLoader.TryGetMod("TRAEProject", ref mod87);
    ModConditions.traeMod = mod87;
    Mod mod88;
    ModConditions.uhtricLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Uhtric", ref mod88);
    ModConditions.uhtricMod = mod88;
    Mod mod89;
    ModConditions.universeOfSwordsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("UniverseOfSwordsMod", ref mod89);
    ModConditions.universeOfSwordsMod = mod89;
    Mod mod90;
    ModConditions.valhallaLoaded = Terraria.ModLoader.ModLoader.TryGetMod("ValhallaMod", ref mod90);
    ModConditions.valhallaMod = mod90;
    Mod mod91;
    ModConditions.verdantLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Verdant", ref mod91);
    ModConditions.verdantMod = mod91;
    Mod mod92;
    ModConditions.vitalityLoaded = Terraria.ModLoader.ModLoader.TryGetMod("VitalityMod", ref mod92);
    ModConditions.vitalityMod = mod92;
    Mod mod93;
    ModConditions.wayfairContentLoaded = Terraria.ModLoader.ModLoader.TryGetMod("WAYFAIRContent", ref mod93);
    ModConditions.wayfairContentMod = mod93;
    Mod mod94;
    ModConditions.wrathOfTheGodsLoaded = Terraria.ModLoader.ModLoader.TryGetMod("NoxusBoss", ref mod94);
    ModConditions.wrathOfTheGodsMod = mod94;
    Mod mod95;
    ModConditions.zylonLoaded = Terraria.ModLoader.ModLoader.TryGetMod("Zylon", ref mod95);
    ModConditions.zylonMod = mod95;
  }

  public enum Downed
  {
    Dreadnautilus,
    MartianSaucer,
    BloodMoon,
    Eclipse,
    LunarEvent,
    SlayerOfEvil,
    SATLA,
    DrFetus,
    SlimesHope,
    PoliticianSlime,
    AncientTrio,
    LavalGolem,
    Antony,
    BunnyZeppelin,
    Okiku,
    HarpyAirforce,
    Isaac,
    AncientGuardian,
    HeroicSlime,
    HoloSlime,
    SecurityBot,
    UndeadChef,
    GuardianOfFrost,
    SoulHarvester,
    TreeToad,
    SeseKitsugai,
    EyeOfTheStorm,
    Frigidius,
    CoreBoss,
    CragmawMire,
    NuclearTerror,
    Mauler,
    WulfrumExcavator,
    Luminaris,
    Prophet,
    NihilityTwin,
    Cruiser,
    Astrageldon,
    Clamitas,
    Pyrogen,
    WallOfBronze,
    Lepus,
    Turkor,
    Ocram,
    Rediancie,
    BabyIceDragon,
    SlimeEmperor,
    Bloodiancie,
    ThunderveinDragon,
    NightmarePlantera,
    Chasme,
    LifeGuardian,
    ManaGuardian,
    MeteorExcavator,
    MeteorAnnihilator,
    HellfireSerpent,
    WitheredAcornSpirit,
    GoblinSorcererChieftain,
    Galahis,
    Creation,
    Destruction,
    BlightKing,
    Gardener,
    Glaciation,
    HandOfCthulhu,
    CursePreacher,
    Effulgence,
    IceLich,
    Niflheim,
    StellarStarship,
    BacteriumPrime,
    DesertBeak,
    KingSting,
    Mechasting,
    Phantasm,
    TrojanSquirrel,
    CursedCoffin,
    Deviantt,
    Lifelight,
    BanishedBaron,
    Eridanus,
    Abominationn,
    Mutant,
    AlphaFrostjaw,
    SanguineElemental,
    Lad,
    Hornlitz,
    SnowDon,
    Stoffie,
    LilyWhite,
    Rumia,
    EternityLarva,
    Nazrin,
    HinaKagiyama,
    Sekibanki,
    Seiran,
    NitoriKawashiro,
    MedicineMelancholy,
    Cirno,
    MinamitsuMurasa,
    AliceMargatroid,
    SakuyaIzayoi,
    SeijaKijin,
    MayumiJoutouguu,
    ToyosatomimiNoMiko,
    KaguyaHouraisan,
    UtsuhoReiuji,
    TenshiHinanawi,
    Kisume,
    Trerios,
    MagmaEye,
    Jack,
    Acheron,
    MarquisMoonsquid,
    PriestessRod,
    Diver,
    Motherbrain,
    WallOfShadow,
    SunSlimeGod,
    Overwatcher,
    Lifebringer,
    Materealizer,
    ScarabBelief,
    WorldsEndWhale,
    Son,
    CaveOrdeal,
    CorruptOrdeal,
    CrimsonOrdeal,
    DesertOrdeal,
    ForestOrdeal,
    HallowOrdeal,
    JungleOrdeal,
    SkyOrdeal,
    SnowOrdeal,
    UnderworldOrdeal,
    Goozma,
    BereftVassal,
    StoneGuardian,
    CommanderGintzia,
    SunStalker,
    PumpkinJack,
    ForgottenPuppetDaedus,
    DreadMire,
    SingularityFragment,
    Verlia,
    Irradia,
    Sylia,
    Fenix,
    BlazingSerpent,
    Cogwork,
    WaterCogwork,
    WaterJellyfish,
    Sparn,
    PandorasFlamebox,
    STARBOMBER,
    GintzeArmy,
    Britzz,
    TheAlchemist,
    CarnagePillar,
    VoidDigger,
    PrinceSlime,
    Triplets,
    JungleDefenders,
    St4sys,
    Terminator,
    Caretaker,
    SiegeEngine,
    SuperVMS,
    Torizo,
    Serris,
    Kraid,
    Phantoon,
    OmegaPirate,
    Nightmare,
    GoldenTorizo,
    Ophiopede,
    Ophiocoon,
    Ophiofly,
    StormCloudfish,
    StarConstruct,
    Gigabat,
    SunPixie,
    Esophage,
    ConvectiveWanderer,
    ForestGuardian,
    CryoGuardian,
    PrimordialWorm,
    TheGuardianOfHell,
    Void,
    Armagem,
    PolarExterminator,
    DivineLight,
    AncientMachine,
    Noehtnap,
    Hydra,
    Imperious,
    RuneGhost,
    InvaderBattleship,
    InvaderNoehtnap,
    OLORD,
    GreatTyrannosaurus,
    DinoMilitia,
    Invaders,
    Thorn,
    Erhan,
    Keeper,
    SeedOfInfection,
    KingSlayerIII,
    OmegaCleaver,
    OmegaGigapora,
    OmegaObliterator,
    PatientZero,
    Akka,
    Ukko,
    AncientDeityDuo,
    Nebuleus,
    FowlEmperor,
    Cockatrice,
    Basan,
    SkullDigger,
    EaglecrestGolem,
    Calavia,
    TheJanitor,
    IrradiatedBehemoth,
    Blisterface,
    ProtectorVolt,
    MACEProject,
    FowlMorning,
    Raveyard,
    PutridPinky,
    Glowmoth,
    PharaohsCurse,
    Advisor,
    Polaris,
    Lux,
    SubspaceSerpent,
    NatureConstruct,
    EarthenConstruct,
    PermafrostConstruct,
    TidalConstruct,
    OtherworldlyConstruct,
    EvilConstruct,
    InfernoConstruct,
    ChaosConstruct,
    NatureSpirit,
    EarthenSpirit,
    PermafrostSpirit,
    TidalSpirit,
    OtherworldlySpirit,
    EvilSpirit,
    InfernoSpirit,
    ChaosSpirit,
    Decree,
    FlamingPumpkin,
    ZombiePiglinBrute,
    JensenTheGrandHarpy,
    Araneas,
    HarpyQueenRaynare,
    Primordia,
    Abaddon,
    Araghur,
    LostSiblings,
    Erazor,
    Nihilus,
    Exodygen,
    Scarabeus,
    MoonJellyWizard,
    VinewrathBane,
    AncientAvian,
    StarplateVoyager,
    Infernon,
    Dusking,
    Atlas,
    JellyDeluge,
    Tide,
    MysticMoon,
    SpookySpirit,
    RotGourd,
    Moco,
    Daffodil,
    OrroBoro,
    BigBone,
    Auroracle,
    Ceiros,
    Glassweaver,
    VagrantofSpace,
    Thespian,
    Dioskouroi,
    Nalhaun,
    Starfarers,
    Penthesilea,
    Arbitration,
    WarriorOfLight,
    Tsukiyomi,
    AncientHusk,
    OverloadedScandrone,
    Painbringer,
    HarbingerOfAnnihilation,
    FlyingTerror,
    StoneMantaRay,
    Bloodweaver,
    InfectedIncarnate,
    TidalTitan,
    Dunestock,
    HexedConstructor,
    Shadowcrawler,
    PrototypeI,
    GraniteOvergrowth,
    Beholder,
    Dredger,
    CharcoolSnowman,
    CosmicMenace,
    EvilFlyingBlade,
    ColossalCarnage,
    Yurnero,
    StormCloud,
    GrandAntlion,
    GemstoneElemental,
    MoonlightDragonfly,
    Dreadnaught,
    MosquitoMonarch,
    AnarchulesBeetle,
    Chaosbringer,
    PaladinSpirit,
    Manaflora,
    Noxus,
    NamelessDeityOfLight,
    Dirtball,
    Metelord,
    Adeneb,
    EldritchJellyfish,
    SaburRex,
  }
}
