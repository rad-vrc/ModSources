// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Common
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using QoLCompendium.Content.Items.Accessories.Construction;
using QoLCompendium.Content.Items.Accessories.Fishing;
using QoLCompendium.Content.Items.Accessories.InformationAccessories;
using QoLCompendium.Content.Items.Tools.Mirrors;
using QoLCompendium.Content.Items.Tools.Summons;
using QoLCompendium.Content.Items.Tools.Summons.CrossMod;
using QoLCompendium.Content.Projectiles.MobileStorages;
using QoLCompendium.Content.Tiles.Other;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

#nullable disable
namespace QoLCompendium.Core;

public static class Common
{
  public static readonly BindingFlags UniversalBindingFlags = (BindingFlags) 60;
  public static List<Hook> detours = new List<Hook>();
  public static readonly HashSet<int> TownSlimeIDs = new HashSet<int>(Enumerable.Range(678, 10))
  {
    670
  };
  public static readonly HashSet<int> LunarPillarIDs = new HashSet<int>()
  {
    507,
    517,
    493,
    422
  };
  public static readonly HashSet<int> CoinIDs = new HashSet<int>()
  {
    71,
    72,
    73,
    74
  };
  public static readonly HashSet<int> RedPotionBuffs = new HashSet<int>()
  {
    1,
    2,
    3,
    5,
    6,
    7,
    8,
    9,
    16 /*0x10*/,
    105,
    17,
    114,
    113,
    116,
    104,
    115,
    117,
    111
  };
  public static int PlatinumMaxStack = 9999;
  public const ulong CopperValue = 1;
  public const ulong SilverValue = 100;
  public const ulong GoldValue = 10000;
  public const ulong PlatinumValue = 1000000;
  public static HashSet<int> PermanentUpgrades = new HashSet<int>()
  {
    5337,
    5339,
    5338,
    5342,
    5341,
    5340,
    5343,
    5326,
    4382,
    5336,
    5043,
    5289
  };
  public static HashSet<int> PermanentMultiUseUpgrades = new HashSet<int>()
  {
    29,
    109,
    1291
  };
  public static readonly int[] BossIDs = new int[19]
  {
    50,
    4,
    13,
    266,
    222,
    668,
    35,
    113,
    657,
    125,
    126,
    134,
    (int) sbyte.MaxValue,
    262,
    245,
    370,
    636,
    439,
    398
  };
  public static readonly HashSet<int> VanillaBossAndEventSummons = new HashSet<int>()
  {
    560,
    43,
    70,
    1331,
    1133,
    5120,
    4988,
    556,
    544,
    557,
    5334,
    3601,
    4271,
    361,
    1315,
    2767,
    602,
    1844,
    1958
  };
  public static readonly HashSet<int> VanillaRightClickBossAndEventSummons = new HashSet<int>()
  {
    1293,
    3828
  };
  public static HashSet<int> ModdedBossAndEventSummons = new HashSet<int>();
  public static HashSet<int> FargosBossAndEventSummons = new HashSet<int>();
  public static readonly ushort[] EvilWallIDs = new ushort[47]
  {
    (ushort) 264,
    (ushort) 69,
    (ushort) 268,
    (ushort) 81,
    (ushort) 265,
    (ushort) 70,
    (ushort) 246,
    (ushort) 3,
    (ushort) 269,
    (ushort) 83,
    (ushort) 248,
    (ushort) 305,
    (ushort) 217,
    (ushort) 306,
    (ushort) 218,
    (ushort) 307,
    (ushort) 219,
    (ushort) 308,
    (ushort) 220,
    (ushort) 309,
    (ushort) 221,
    (ushort) 310,
    (ushort) 222,
    (ushort) 276,
    (ushort) 188,
    (ushort) 277,
    (ushort) 189,
    (ushort) 278,
    (ushort) 190,
    (ushort) 279,
    (ushort) 191,
    (ushort) 280,
    (ushort) 192 /*0xC0*/,
    (ushort) 281,
    (ushort) 193,
    (ushort) 282,
    (ushort) 194,
    (ushort) 283,
    (ushort) 195,
    (ushort) 288,
    (ushort) 200,
    (ushort) 289,
    (ushort) 201,
    (ushort) 290,
    (ushort) 202,
    (ushort) 291,
    (ushort) 203
  };
  public static readonly ushort[] PureWallIDs = new ushort[47]
  {
    (ushort) 66,
    (ushort) 63 /*0x3F*/,
    (ushort) 66,
    (ushort) 63 /*0x3F*/,
    (ushort) 66,
    (ushort) 63 /*0x3F*/,
    (ushort) 1,
    (ushort) 1,
    (ushort) 1,
    (ushort) 1,
    (ushort) 1,
    (ushort) 304,
    (ushort) 216,
    (ushort) 304,
    (ushort) 216,
    (ushort) 304,
    (ushort) 216,
    (ushort) 275,
    (ushort) 187,
    (ushort) 275,
    (ushort) 187,
    (ushort) 275,
    (ushort) 187,
    (ushort) 284,
    (ushort) 196,
    (ushort) 285,
    (ushort) 197,
    (ushort) 286,
    (ushort) 198,
    (ushort) 287,
    (ushort) 199,
    (ushort) 284,
    (ushort) 196,
    (ushort) 285,
    (ushort) 197,
    (ushort) 286,
    (ushort) 198,
    (ushort) 287,
    (ushort) 199,
    (ushort) 284,
    (ushort) 196,
    (ushort) 285,
    (ushort) 197,
    (ushort) 286,
    (ushort) 198,
    (ushort) 287,
    (ushort) 199
  };
  public static readonly HashSet<int> FallingBlocks = new HashSet<int>()
  {
    31 /*0x1F*/,
    56,
    241,
    67,
    71,
    179
  };
  public static HashSet<int> Emblems = new HashSet<int>()
  {
    490,
    491,
    489,
    2998
  };
  public static HashSet<int> PowerUpItems = new HashSet<int>()
  {
    58,
    1734,
    1867,
    184,
    1735,
    1868,
    3453,
    3454,
    3455
  };
  public static readonly int[] VanillaFountains = new int[8]
  {
    909,
    942,
    940,
    944,
    941,
    910,
    4417,
    943
  };
  public static readonly HashSet<int> MobileStorages = new HashSet<int>()
  {
    525,
    734,
    ModContent.ProjectileType<FlyingSafeProjectile>(),
    ModContent.ProjectileType<EtherianConstructProjectile>()
  };
  public static readonly bool[] NormalBunnies = NPCID.Sets.Factory.CreateBoolSet(new int[14]
  {
    46,
    647,
    648,
    650,
    649,
    651,
    646,
    652,
    614,
    303,
    337,
    47,
    464,
    540
  });
  public static readonly bool[] NormalSquirrels = NPCID.Sets.Factory.CreateBoolSet(new int[9]
  {
    299,
    538,
    640,
    641,
    643,
    642,
    644,
    639,
    645
  });
  public static readonly bool[] NormalButterflies = NPCID.Sets.Factory.CreateBoolSet(new int[3]
  {
    356,
    653,
    661
  });
  public static readonly bool[] NormalBirds = NPCID.Sets.Factory.CreateBoolSet(new int[3]
  {
    74,
    297,
    298
  });
  public static HashSet<int> Prefixes = new HashSet<int>()
  {
    81,
    84,
    59,
    15,
    17,
    60,
    82,
    83,
    57,
    65,
    66,
    68,
    72,
    76,
    80 /*0x50*/
  };
  public static HashSet<int> BankItems = new HashSet<int>()
  {
    854,
    855,
    3033,
    3034,
    3035,
    3619,
    2799,
    3611,
    3123,
    3124,
    5437,
    5358,
    5359,
    5360,
    5361,
    395,
    3122,
    3121,
    3036,
    15,
    707,
    16 /*0x10*/,
    708,
    17,
    709,
    18,
    393,
    3084,
    3118,
    3095,
    3102,
    3099,
    3119,
    3120,
    3037,
    3096,
    4056,
    407,
    1923,
    2215,
    2217,
    2216,
    2214,
    3061,
    5126,
    3624,
    2373,
    2374,
    2375,
    4881,
    3721,
    5064,
    5139,
    5140,
    5141,
    5142,
    5143,
    5144,
    5145,
    5146,
    5010,
    3090,
    4409,
    4767,
    5309,
    5323,
    5355,
    5113,
    4346
  };
  public static HashSet<int> IgnoredTilesForExplosives = new HashSet<int>()
  {
    ModContent.TileType<AsphaltPlatformTile>()
  };
  public static HashSet<Mod> IgnoredModsForExplosives = new HashSet<Mod>();
  public static HashSet<int> FlaskBuffs = new HashSet<int>()
  {
    78,
    73,
    74,
    75,
    76,
    77,
    79,
    71
  };
  public static HashSet<int> ThoriumCoatings = new HashSet<int>();
  public static readonly int[] kingSlimeDrops = new int[6]
  {
    2430,
    256 /*0x0100*/,
    257,
    258,
    2585,
    2610
  };
  public static readonly int[] eyeOfCthulhuDrops = new int[1]
  {
    1299
  };
  public static readonly int[] eaterOfWorldsDrops = new int[1]
  {
    994
  };
  public static readonly int[] brainOfCthulhuDrops = new int[1]
  {
    3060
  };
  public static readonly int[] queenBeeDrops = new int[10]
  {
    1121,
    1123,
    2888,
    1129,
    842,
    843,
    844,
    1132,
    1170,
    2502
  };
  public static readonly int[] deerclopsDrops = new int[8]
  {
    5098,
    5101,
    5113,
    5385,
    5117,
    5118,
    5119,
    5095
  };
  public static readonly int[] skeletronDrops = new int[3]
  {
    1273,
    1313,
    4993
  };
  public static readonly int[] wallOfFleshDrops = new int[8]
  {
    426,
    434,
    514,
    4912,
    490,
    491,
    489,
    2998
  };
  public static readonly int[] queenSlimeDrops = new int[6]
  {
    4982,
    4983,
    4984,
    4758,
    4981,
    4980
  };
  public static readonly int[] planteraDrops = new int[11]
  {
    758,
    1255,
    788,
    1178,
    1259,
    1155,
    3018,
    1157,
    3021,
    1305,
    1182
  };
  public static readonly int[] golemDrops = new int[8]
  {
    1294,
    1258,
    1122,
    899,
    1248,
    1295,
    1296,
    1297
  };
  public static readonly int[] betsyDrops = new int[5]
  {
    3883,
    3859,
    3858,
    3870,
    3827
  };
  public static readonly int[] dukeFishronDrops = new int[6]
  {
    2609,
    2623,
    2611,
    2622,
    2621,
    2624
  };
  public static readonly int[] empressOfLightDrops = new int[7]
  {
    4952,
    4923,
    4914,
    4953,
    4823,
    4715,
    5075
  };
  public static readonly int[] moonLordDrops = new int[10]
  {
    3063,
    3389,
    3065,
    1553,
    3930,
    3541,
    3570,
    3571,
    3569,
    4469
  };
  public static int AnyPirateBanner;
  public static int AnyArmoredBonesBanner;
  public static int AnySlimeBanner;
  public static int AnyBatBanner;
  public static int AnyHallowBanner;
  public static int AnyCorruptionBanner;
  public static int AnyCrimsonBanner;
  public static int AnyJungleBanner;
  public static int AnySnowBanner;
  public static int AnyDesertBanner;
  public static int AnyUnderworldBanner;

  public static void UnloadTasks()
  {
    foreach (Hook detour in Common.detours)
      detour.Undo();
  }

  public static void PostSetupTasks()
  {
    HashSet<int> other1 = new HashSet<int>()
    {
      Common.GetModItem(ModConditions.orchidMod, "Chip"),
      Common.GetModItem(ModConditions.orchidMod, "Guard"),
      Common.GetModItem(ModConditions.orchidMod, "Potency"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationNote"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationNoteStatue"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationNoteNoble"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationNoteRhapsodist"),
      Common.GetModItem(ModConditions.thoriumMod, "MeatSlab"),
      Common.GetModItem(ModConditions.thoriumMod, "GreatFlesh"),
      Common.GetModItem(ModConditions.vitalityMod, "BloodClot")
    };
    Common.PowerUpItems.UnionWith((IEnumerable<int>) other1);
    HashSet<int> other2 = new HashSet<int>()
    {
      ModContent.ItemType<BattalionLog>(),
      ModContent.ItemType<HarmInducer>(),
      ModContent.ItemType<HeadCounter>(),
      ModContent.ItemType<Kettlebell>(),
      ModContent.ItemType<LuckyDie>(),
      ModContent.ItemType<MetallicClover>(),
      ModContent.ItemType<PlateCracker>(),
      ModContent.ItemType<Regenerator>(),
      ModContent.ItemType<ReinforcedPanel>(),
      ModContent.ItemType<Replenisher>(),
      ModContent.ItemType<TrackingDevice>(),
      ModContent.ItemType<WingTimer>(),
      ModContent.ItemType<Fitbit>(),
      ModContent.ItemType<HeartbeatSensor>(),
      ModContent.ItemType<ToleranceDetector>(),
      ModContent.ItemType<VitalDisplay>(),
      ModContent.ItemType<IAH>(),
      ModContent.ItemType<MosaicMirror>(),
      ModContent.ItemType<SkullWatch>(),
      ModContent.ItemType<DeteriorationDisplay>(),
      ModContent.ItemType<MiningEmblem>(),
      ModContent.ItemType<ConstructionEmblem>(),
      ModContent.ItemType<CreationClubMembersPass>(),
      ModContent.ItemType<SonarDevice>(),
      ModContent.ItemType<AnglerRadar>(),
      ModContent.ItemType<AnglersDream>(),
      Common.GetModItem(ModConditions.aequusMod, "AnglerBroadcaster"),
      Common.GetModItem(ModConditions.aequusMod, "Calendar"),
      Common.GetModItem(ModConditions.aequusMod, "GeigerCounter"),
      Common.GetModItem(ModConditions.aequusMod, "HoloLens"),
      Common.GetModItem(ModConditions.aequusMod, "RichMansMonocle"),
      Common.GetModItem(ModConditions.aequusMod, "DevilsTongue"),
      Common.GetModItem(ModConditions.aequusMod, "NeonGenesis"),
      Common.GetModItem(ModConditions.aequusMod, "RadonFishingBobber"),
      Common.GetModItem(ModConditions.aequusMod, "Ramishroom"),
      Common.GetModItem(ModConditions.aequusMod, "RegrowingBait"),
      Common.GetModItem(ModConditions.aequusMod, "LavaproofMitten"),
      Common.GetModItem(ModConditions.aequusMod, "BusinessCard"),
      Common.GetModItem(ModConditions.aequusMod, "HaltingMachine"),
      Common.GetModItem(ModConditions.aequusMod, "HaltingMagnet"),
      Common.GetModItem(ModConditions.aequusMod, "HyperJet"),
      Common.GetModItem(ModConditions.afkpetsMod, "FishermansPride"),
      Common.GetModItem(ModConditions.afkpetsMod, "LampyridaeHairpin"),
      Common.GetModItem(ModConditions.afkpetsMod, "Piracy"),
      Common.GetModItem(ModConditions.afkpetsMod, "PortableSonar"),
      Common.GetModItem(ModConditions.afkpetsMod, "TheHandyman"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "AttendanceLog"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "BiomeCrystal"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "EngiRegistry"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "FortuneMirror"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "HitMarker"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "Magimeter"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "RSH"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "SafteyScanner"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "ScryingMirror"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "ThreatAnalyzer"),
      Common.GetModItem(ModConditions.blocksInfoAccessoriesMod, "WantedPoster"),
      Common.GetModItem(ModConditions.calamityMod, "AlluringBait"),
      Common.GetModItem(ModConditions.calamityMod, "EnchantedPearl"),
      Common.GetModItem(ModConditions.calamityMod, "SupremeBaitTackleBoxFishingStation"),
      Common.GetModItem(ModConditions.calamityMod, "AncientFossil"),
      Common.GetModItem(ModConditions.calamityMod, "OceanCrest"),
      Common.GetModItem(ModConditions.calamityMod, "SpelunkersAmulet"),
      Common.GetModItem(ModConditions.clickerClassMod, "ButtonMasher"),
      Common.GetModItem(ModConditions.depthsMod, "LodeStone"),
      Common.GetModItem(ModConditions.depthsMod, "MercuryMossFishingBobber"),
      Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofFishingHook"),
      Common.GetModItem(ModConditions.depthsMod, "QuicksilverproofTackleBag"),
      Common.GetModItem(ModConditions.luiAFKMod, "FasterMining"),
      Common.GetModItem(ModConditions.luiAFKMod, "SuperToolTime"),
      Common.GetModItem(ModConditions.luiAFKMod, "ToolTime"),
      Common.GetModItem(ModConditions.luiAFKDLCMod, "ArchitectHeavyEquipment"),
      Common.GetModItem(ModConditions.luiAFKDLCMod, "EnchantedSupremeFishingBundle"),
      Common.GetModItem(ModConditions.martainsOrderMod, "ArmorDisplayer"),
      Common.GetModItem(ModConditions.martainsOrderMod, "FlightTimer"),
      Common.GetModItem(ModConditions.martainsOrderMod, "Journal"),
      Common.GetModItem(ModConditions.martainsOrderMod, "IronWatch"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LeadWatch"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LeprechaunSensor"),
      Common.GetModItem(ModConditions.martainsOrderMod, "MinionCounter"),
      Common.GetModItem(ModConditions.martainsOrderMod, "SentryCounter"),
      Common.GetModItem(ModConditions.martainsOrderMod, "SummonersTracker"),
      Common.GetModItem(ModConditions.martainsOrderMod, "SurvivalTracker"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "AnomalyLocator"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ArchaeologistToolbelt"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "GoldenTrowel"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "InfiniteVoid"),
      Common.GetModItem(ModConditions.spiritMod, "FisheyeGem"),
      Common.GetModItem(ModConditions.spiritMod, "MetalBand"),
      Common.GetModItem(ModConditions.spiritMod, "MimicRepellent"),
      Common.GetModItem(ModConditions.thoriumMod, "HeartRateMonitor"),
      Common.GetModItem(ModConditions.thoriumMod, "HightechSonarDevice"),
      Common.GetModItem(ModConditions.thoriumMod, "GlitteringChalice"),
      Common.GetModItem(ModConditions.thoriumMod, "GreedyGoblet"),
      Common.GetModItem(ModConditions.thoriumMod, "LuckyRabbitsFoot")
    };
    Common.BankItems.UnionWith((IEnumerable<int>) other2);
    HashSet<int> other3 = new HashSet<int>()
    {
      ModContent.ItemType<CultistSummon>(),
      ModContent.ItemType<DukeFishronSummon>(),
      ModContent.ItemType<EmpressOfLightSummon>(),
      ModContent.ItemType<PlanteraSummon>(),
      ModContent.ItemType<SkeletronSummon>(),
      ModContent.ItemType<WallOfFleshSummon>(),
      ModContent.ItemType<ForgottenOneSummon>(),
      ModContent.ItemType<GiantClamSummon>(),
      ModContent.ItemType<LeviathanAnahitaSummon>(),
      ModContent.ItemType<OldDukeSummon>(),
      Common.GetModItem(ModConditions.aequusMod, "GalacticStarfruit"),
      Common.GetModItem(ModConditions.afkpetsMod, "AncientSand"),
      Common.GetModItem(ModConditions.afkpetsMod, "BlackenedHeart"),
      Common.GetModItem(ModConditions.afkpetsMod, "BrokenDelftPlate"),
      Common.GetModItem(ModConditions.afkpetsMod, "CookingBook"),
      Common.GetModItem(ModConditions.afkpetsMod, "CorruptedServer"),
      Common.GetModItem(ModConditions.afkpetsMod, "DemonicAnalysis"),
      Common.GetModItem(ModConditions.afkpetsMod, "DesertMirror"),
      Common.GetModItem(ModConditions.afkpetsMod, "DuckWhistle"),
      Common.GetModItem(ModConditions.afkpetsMod, "FallingSlimeReplica"),
      Common.GetModItem(ModConditions.afkpetsMod, "FrozenSkull"),
      Common.GetModItem(ModConditions.afkpetsMod, "GoldenKingSlimeIdol"),
      Common.GetModItem(ModConditions.afkpetsMod, "GoldenSkull"),
      Common.GetModItem(ModConditions.afkpetsMod, "HaniwaIdol"),
      Common.GetModItem(ModConditions.afkpetsMod, "HolographicSlimeReplica"),
      Common.GetModItem(ModConditions.afkpetsMod, "IceBossCrystal"),
      Common.GetModItem(ModConditions.afkpetsMod, "MagicWand"),
      Common.GetModItem(ModConditions.afkpetsMod, "NightmareFuel"),
      Common.GetModItem(ModConditions.afkpetsMod, "PinkDiamond"),
      Common.GetModItem(ModConditions.afkpetsMod, "PlantAshContainer"),
      Common.GetModItem(ModConditions.afkpetsMod, "PreyTrackingChip"),
      Common.GetModItem(ModConditions.afkpetsMod, "RoastChickenPlate"),
      Common.GetModItem(ModConditions.afkpetsMod, "SeveredClothierHead"),
      Common.GetModItem(ModConditions.afkpetsMod, "SeveredDryadHead"),
      Common.GetModItem(ModConditions.afkpetsMod, "SeveredHarpyHead"),
      Common.GetModItem(ModConditions.afkpetsMod, "ShogunSlimesHelmet"),
      Common.GetModItem(ModConditions.afkpetsMod, "SlimeinaGlassCube"),
      Common.GetModItem(ModConditions.afkpetsMod, "SlimyWarBanner"),
      Common.GetModItem(ModConditions.afkpetsMod, "SoulofAgonyinaBottle"),
      Common.GetModItem(ModConditions.afkpetsMod, "SpineWormFood"),
      Common.GetModItem(ModConditions.afkpetsMod, "SpiritofFunPot"),
      Common.GetModItem(ModConditions.afkpetsMod, "SpiritualHeart"),
      Common.GetModItem(ModConditions.afkpetsMod, "StoryBook"),
      Common.GetModItem(ModConditions.afkpetsMod, "SuspiciousLookingChest"),
      Common.GetModItem(ModConditions.afkpetsMod, "SwissChocolate"),
      Common.GetModItem(ModConditions.afkpetsMod, "TiedBunny"),
      Common.GetModItem(ModConditions.afkpetsMod, "TinyMeatIdol"),
      Common.GetModItem(ModConditions.afkpetsMod, "TradeDeal"),
      Common.GetModItem(ModConditions.afkpetsMod, "UnstableRainbowCookie"),
      Common.GetModItem(ModConditions.afkpetsMod, "UntoldBurial"),
      Common.GetModItem(ModConditions.awfulGarbageMod, "InsectOnAStick"),
      Common.GetModItem(ModConditions.awfulGarbageMod, "PileOfFakeBones"),
      Common.GetModItem(ModConditions.blocksCoreBossMod, "ChargedOrb"),
      Common.GetModItem(ModConditions.blocksCoreBossMod, "ChargedOrbCrim"),
      Common.GetModItem(ModConditions.consolariaMod, "SuspiciousLookingEgg"),
      Common.GetModItem(ModConditions.consolariaMod, "CursedStuffing"),
      Common.GetModItem(ModConditions.consolariaMod, "SuspiciousLookingSkull"),
      Common.GetModItem(ModConditions.consolariaMod, "Wishbone"),
      Common.GetModItem(ModConditions.coraliteMod, "RedBerry"),
      Common.GetModItem(ModConditions.edorbisMod, "BiomechanicalMatter"),
      Common.GetModItem(ModConditions.edorbisMod, "CursedSoul"),
      Common.GetModItem(ModConditions.edorbisMod, "KelviniteRadar"),
      Common.GetModItem(ModConditions.edorbisMod, "SlayerTrophy"),
      Common.GetModItem(ModConditions.edorbisMod, "ThePrettiestFlower"),
      Common.GetModItem(ModConditions.enchantedMoonsMod, "BlueMedallion"),
      Common.GetModItem(ModConditions.enchantedMoonsMod, "CherryAmulet"),
      Common.GetModItem(ModConditions.enchantedMoonsMod, "HarvestLantern"),
      Common.GetModItem(ModConditions.enchantedMoonsMod, "MintRing"),
      Common.GetModItem(ModConditions.everjadeMod, "FestivalLantern"),
      Common.GetModItem(ModConditions.excelsiorMod, "ReflectiveIceShard"),
      Common.GetModItem(ModConditions.excelsiorMod, "PlanetaryTrackingDevice"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "BloodyAmulet"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "InfestedCarcass"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "DesertHorn"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "GoblinRetreatOrder"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "FalseTreasureMap"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "OddFertilizer"),
      Common.GetModItem(ModConditions.gensokyoMod, "AliceMargatroidSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "CirnoSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "EternityLarvaSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "HinaKagiyamaSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "KaguyaHouraisanSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "LilyWhiteSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "MayumiJoutouguuSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "MedicineMelancholySpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "MinamitsuMurasaSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "NazrinSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "NitoriKawashiroSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "RumiaSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "SakuyaIzayoiSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "SeijaKijinSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "SeiranSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "SekibankiSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "TenshiHinanawiSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "ToyosatomimiNoMikoSpawner"),
      Common.GetModItem(ModConditions.gensokyoMod, "UtsuhoReiujiSpawner"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "CannedSoulofFlight"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "MetalSpine"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "SouthernPotting"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "SunlightCrown"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "UltimateTorch"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "UnstableGlobe"),
      Common.GetModItem(ModConditions.martainsOrderMod, "FrigidEgg"),
      Common.GetModItem(ModConditions.martainsOrderMod, "SuspiciousLookingCloud"),
      Common.GetModItem(ModConditions.martainsOrderMod, "Catnip"),
      Common.GetModItem(ModConditions.martainsOrderMod, "CarnageSuspiciousRazor"),
      Common.GetModItem(ModConditions.martainsOrderMod, "VoidWorm"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LuminiteSlimeCrown"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LuminiteEye"),
      Common.GetModItem(ModConditions.martainsOrderMod, "JunglesLastTreasure"),
      Common.GetModItem(ModConditions.martainsOrderMod, "TeslaRemote"),
      Common.GetModItem(ModConditions.martainsOrderMod, "BloodyNight"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LucidDay"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LucidFestival"),
      Common.GetModItem(ModConditions.martainsOrderMod, "LucidNight"),
      Common.GetModItem(ModConditions.medialRiftMod, "RemoteOfTheMetalHeads"),
      Common.GetModItem(ModConditions.metroidMod, "GoldenTorizoSummon"),
      Common.GetModItem(ModConditions.metroidMod, "KraidSummon"),
      Common.GetModItem(ModConditions.metroidMod, "NightmareSummon"),
      Common.GetModItem(ModConditions.metroidMod, "OmegaPirateSummon"),
      Common.GetModItem(ModConditions.metroidMod, "PhantoonSummon"),
      Common.GetModItem(ModConditions.metroidMod, "SerrisSummon"),
      Common.GetModItem(ModConditions.metroidMod, "TorizoSummon"),
      Common.GetModItem(ModConditions.ophioidMod, "DeadFungusbug"),
      Common.GetModItem(ModConditions.ophioidMod, "InfestedCompost"),
      Common.GetModItem(ModConditions.ophioidMod, "LivingCarrion"),
      Common.GetModItem(ModConditions.qwertyMod, "AncientEmblem"),
      Common.GetModItem(ModConditions.qwertyMod, "B4Summon"),
      Common.GetModItem(ModConditions.qwertyMod, "BladeBossSummon"),
      Common.GetModItem(ModConditions.qwertyMod, "DinoEgg"),
      Common.GetModItem(ModConditions.qwertyMod, "HydraSummon"),
      Common.GetModItem(ModConditions.qwertyMod, "RitualInterupter"),
      Common.GetModItem(ModConditions.qwertyMod, "SummoningRune"),
      Common.GetModItem(ModConditions.redemptionMod, "EaglecrestSpelltome"),
      Common.GetModItem(ModConditions.redemptionMod, "EggCrown"),
      Common.GetModItem(ModConditions.redemptionMod, "FowlWarHorn"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticLure"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SuspiciousLookingCandle"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "JarOfPeanuts"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "CatalystBomb"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "PumpkinLantern"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "PrimordiaSummon"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "AbaddonSummon"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "SerpentSummon"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "SoranEmblem"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "HeirsAuthority"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "PigmanBanner"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "SandstormMedallion"),
      Common.GetModItem(ModConditions.spiritMod, "DistressJellyItem"),
      Common.GetModItem(ModConditions.spiritMod, "GladeWreath"),
      Common.GetModItem(ModConditions.spiritMod, "ReachBossSummon"),
      Common.GetModItem(ModConditions.spiritMod, "JewelCrown"),
      Common.GetModItem(ModConditions.spiritMod, "BlackPearl"),
      Common.GetModItem(ModConditions.spiritMod, "BlueMoonSpawn"),
      Common.GetModItem(ModConditions.spiritMod, "DuskCrown"),
      Common.GetModItem(ModConditions.spiritMod, "CursedCloth"),
      Common.GetModItem(ModConditions.spiritMod, "StoneSkin"),
      Common.GetModItem(ModConditions.spiritMod, "MartianTransmitter"),
      Common.GetModItem(ModConditions.spookyMod, "Fertilizer"),
      Common.GetModItem(ModConditions.spookyMod, "RottenSeed"),
      Common.GetModItem(ModConditions.stormsAdditionsMod, "AridBossSummon"),
      Common.GetModItem(ModConditions.stormsAdditionsMod, "MoonlingSummoner"),
      Common.GetModItem(ModConditions.stormsAdditionsMod, "StormBossSummoner"),
      Common.GetModItem(ModConditions.stormsAdditionsMod, "UltimateBossSummoner"),
      Common.GetModItem(ModConditions.supernovaMod, "BugOnAStick"),
      Common.GetModItem(ModConditions.supernovaMod, "EerieCrystal"),
      Common.GetModItem(ModConditions.thoriumMod, "StormFlare"),
      Common.GetModItem(ModConditions.thoriumMod, "JellyfishResonator"),
      Common.GetModItem(ModConditions.thoriumMod, "UnstableCore"),
      Common.GetModItem(ModConditions.thoriumMod, "AncientBlade"),
      Common.GetModItem(ModConditions.thoriumMod, "StarCaller"),
      Common.GetModItem(ModConditions.thoriumMod, "StriderTear"),
      Common.GetModItem(ModConditions.thoriumMod, "VoidLens"),
      Common.GetModItem(ModConditions.thoriumMod, "AromaticBulb"),
      Common.GetModItem(ModConditions.thoriumMod, "AbyssalShadow2"),
      Common.GetModItem(ModConditions.thoriumMod, "DoomSayersCoin"),
      Common.GetModItem(ModConditions.thoriumMod, "FreshBrain"),
      Common.GetModItem(ModConditions.thoriumMod, "RottingSpore"),
      Common.GetModItem(ModConditions.thoriumMod, "IllusionaryGlass"),
      Common.GetModItem(ModConditions.uhtricMod, "RareGeode"),
      Common.GetModItem(ModConditions.uhtricMod, "SnowyCharcoal"),
      Common.GetModItem(ModConditions.uhtricMod, "CosmicLure"),
      Common.GetModItem(ModConditions.universeOfSwordsMod, "SwordBossSummon"),
      Common.GetModItem(ModConditions.valhallaMod, "HeavensSeal"),
      Common.GetModItem(ModConditions.valhallaMod, "HellishRadish"),
      Common.GetModItem(ModConditions.vitalityMod, "CloudCore"),
      Common.GetModItem(ModConditions.vitalityMod, "AncientCrown"),
      Common.GetModItem(ModConditions.vitalityMod, "MultigemCluster"),
      Common.GetModItem(ModConditions.vitalityMod, "MoonlightLotusFlower"),
      Common.GetModItem(ModConditions.vitalityMod, "Dreadcandle"),
      Common.GetModItem(ModConditions.vitalityMod, "MeatyMushroom"),
      Common.GetModItem(ModConditions.vitalityMod, "AnarchyCrystal"),
      Common.GetModItem(ModConditions.vitalityMod, "TotemofChaos"),
      Common.GetModItem(ModConditions.vitalityMod, "MartianRadio"),
      Common.GetModItem(ModConditions.vitalityMod, "SpiritBox"),
      Common.GetModItem(ModConditions.wayfairContentMod, "MagicFertilizer"),
      Common.GetModItem(ModConditions.zylonMod, "ForgottenFlame"),
      Common.GetModItem(ModConditions.zylonMod, "SlimyScepter")
    };
    Common.ModdedBossAndEventSummons.UnionWith((IEnumerable<int>) other3);
    HashSet<int> other4 = new HashSet<int>()
    {
      Common.GetModItem(ModConditions.fargosMutantMod, "Anemometer"),
      Common.GetModItem(ModConditions.fargosMutantMod, "BatteredClub"),
      Common.GetModItem(ModConditions.fargosMutantMod, "BetsyEgg"),
      Common.GetModItem(ModConditions.fargosMutantMod, "FestiveOrnament"),
      Common.GetModItem(ModConditions.fargosMutantMod, "ForbiddenScarab"),
      Common.GetModItem(ModConditions.fargosMutantMod, "ForbiddenTome"),
      Common.GetModItem(ModConditions.fargosMutantMod, "HeadofMan"),
      Common.GetModItem(ModConditions.fargosMutantMod, "IceKingsRemains"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MartianMemoryStick"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MatsuriLantern"),
      Common.GetModItem(ModConditions.fargosMutantMod, "NaughtyList"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PartyInvite"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PillarSummon"),
      Common.GetModItem(ModConditions.fargosMutantMod, "RunawayProbe"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SlimyBarometer"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SpentLantern"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SpookyBranch"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousLookingScythe"),
      Common.GetModItem(ModConditions.fargosMutantMod, "WeatherBalloon"),
      Common.GetModItem(ModConditions.fargosMutantMod, "AmalgamatedSkull"),
      Common.GetModItem(ModConditions.fargosMutantMod, "AmalgamatedSpirit"),
      Common.GetModItem(ModConditions.fargosMutantMod, "AthenianIdol"),
      Common.GetModItem(ModConditions.fargosMutantMod, "AttractiveOre"),
      Common.GetModItem(ModConditions.fargosMutantMod, "BloodSushiPlatter"),
      Common.GetModItem(ModConditions.fargosMutantMod, "BloodUrchin"),
      Common.GetModItem(ModConditions.fargosMutantMod, "CloudSnack"),
      Common.GetModItem(ModConditions.fargosMutantMod, "ClownLicense"),
      Common.GetModItem(ModConditions.fargosMutantMod, "CoreoftheFrostCore"),
      Common.GetModItem(ModConditions.fargosMutantMod, "CorruptChest"),
      Common.GetModItem(ModConditions.fargosMutantMod, "CrimsonChest"),
      Common.GetModItem(ModConditions.fargosMutantMod, "DemonicPlushie"),
      Common.GetModItem(ModConditions.fargosMutantMod, "DilutedRainbowMatter"),
      Common.GetModItem(ModConditions.fargosMutantMod, "Eggplant"),
      Common.GetModItem(ModConditions.fargosMutantMod, "ForbiddenForbiddenFragment"),
      Common.GetModItem(ModConditions.fargosMutantMod, "GnomeHat"),
      Common.GetModItem(ModConditions.fargosMutantMod, "GoblinScrap"),
      Common.GetModItem(ModConditions.fargosMutantMod, "GoldenSlimeCrown"),
      Common.GetModItem(ModConditions.fargosMutantMod, "GrandCross"),
      Common.GetModItem(ModConditions.fargosMutantMod, "HallowChest"),
      Common.GetModItem(ModConditions.fargosMutantMod, "HeartChocolate"),
      Common.GetModItem(ModConditions.fargosMutantMod, "HemoclawCrab"),
      Common.GetModItem(ModConditions.fargosMutantMod, "HolyGrail"),
      Common.GetModItem(ModConditions.fargosMutantMod, "JungleChest"),
      Common.GetModItem(ModConditions.fargosMutantMod, "LeesHeadband"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MothLamp"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MothronEgg"),
      Common.GetModItem(ModConditions.fargosMutantMod, "Pincushion"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PinkSlimeCrown"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PirateFlag"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PlunderedBooty"),
      Common.GetModItem(ModConditions.fargosMutantMod, "RuneOrb"),
      Common.GetModItem(ModConditions.fargosMutantMod, "ShadowflameIcon"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SlimyLockBox"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousLookingChest"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousLookingLure"),
      Common.GetModItem(ModConditions.fargosMutantMod, "WormSnack"),
      Common.GetModItem(ModConditions.fargosMutantMod, "Abeemination2"),
      Common.GetModItem(ModConditions.fargosMutantMod, "AncientSeal"),
      Common.GetModItem(ModConditions.fargosMutantMod, "CelestialSigil2"),
      Common.GetModItem(ModConditions.fargosMutantMod, "CultistSummon"),
      Common.GetModItem(ModConditions.fargosMutantMod, "DeathBringerFairy"),
      Common.GetModItem(ModConditions.fargosMutantMod, "DeerThing2"),
      Common.GetModItem(ModConditions.fargosMutantMod, "FleshyDoll"),
      Common.GetModItem(ModConditions.fargosMutantMod, "GoreySpine"),
      Common.GetModItem(ModConditions.fargosMutantMod, "JellyCrystal"),
      Common.GetModItem(ModConditions.fargosMutantMod, "LihzahrdPowerCell2"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MechanicalAmalgam"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MechEye"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MechSkull"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MechWorm"),
      Common.GetModItem(ModConditions.fargosMutantMod, "MutantVoodoo"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PlanterasFruit"),
      Common.GetModItem(ModConditions.fargosMutantMod, "PrismaticPrimrose"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SlimyCrown"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousEye"),
      Common.GetModItem(ModConditions.fargosMutantMod, "SuspiciousSkull"),
      Common.GetModItem(ModConditions.fargosMutantMod, "TruffleWorm2"),
      Common.GetModItem(ModConditions.fargosMutantMod, "WormyFood"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "AbomsCurse"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "ChampionySigil"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "CoffinSummon"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "DevisCurse"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "FragilePixieLamp"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "MechLure"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsCurse"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "SquirrelCoatofArms"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "AbandonedRemote"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ABombInMyNation"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "AstrumCor"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "BirbPheromones"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "BlightedEye"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "BloodyWorm"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ChunkyStardust"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ClamPearl"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "ColossalTentacle"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "CryingKey"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DeepseaProteinShake"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DefiledCore"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DefiledShard"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "DragonEgg"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "EyeofExtinction"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "FriedDoll"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "HiveTumor"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "LetterofKos"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "MaulerSkull"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "MedallionoftheDesert"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "MurkySludge"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "NoisyWhistle"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "NuclearChunk"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "OphiocordycipitaceaeSprout"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "PlaguedWalkieTalkie"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "PolterplasmicBeacon"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "PortableCodebreaker"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "QuakeIdol"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "RedStainedWormFood"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "RiftofKos"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SeeFood"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SirensPearl"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SomeKindofSpaceWorm"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "StormIdol"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "SulphurBearTrap"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "WormFoodofKos"),
      Common.GetModItem(ModConditions.fargosSoulsDLCMod, "WyrmTablet"),
      Common.GetModItem(ModConditions.fargosSoulsExtrasMod, "PandorasBox")
    };
    Common.FargosBossAndEventSummons.UnionWith((IEnumerable<int>) other4);
    HashSet<int> other5 = new HashSet<int>()
    {
      Common.GetModPrefix(ModConditions.calamityMod, "Flawless"),
      Common.GetModPrefix(ModConditions.calamityMod, "Silent"),
      Common.GetModPrefix(ModConditions.clickerClassMod, "Elite"),
      Common.GetModPrefix(ModConditions.clickerClassMod, "ClickerRadius"),
      Common.GetModPrefix(ModConditions.martainsOrderMod, "StrikerPrefix"),
      Common.GetModPrefix(ModConditions.orchidMod, "EmpyreanPrefix"),
      Common.GetModPrefix(ModConditions.orchidMod, "EtherealPrefix"),
      Common.GetModPrefix(ModConditions.orchidMod, "BlockingPrefix"),
      Common.GetModPrefix(ModConditions.orchidMod, "BrewingPrefix"),
      Common.GetModPrefix(ModConditions.orchidMod, "LoadedPrefix"),
      Common.GetModPrefix(ModConditions.orchidMod, "SpiritualPrefix"),
      Common.GetModPrefix(ModConditions.secretsOfTheShadowsMod, "Omnipotent"),
      Common.GetModPrefix(ModConditions.secretsOfTheShadowsMod, "Omniscient"),
      Common.GetModPrefix(ModConditions.secretsOfTheShadowsMod, "Soulbound"),
      Common.GetModPrefix(ModConditions.thoriumMod, "Fabled"),
      Common.GetModPrefix(ModConditions.thoriumMod, "Engrossing"),
      Common.GetModPrefix(ModConditions.thoriumMod, "Lucrative"),
      Common.GetModPrefix(ModConditions.vitalityMod, "MalevolentPrefix"),
      Common.GetModPrefix(ModConditions.vitalityMod, "RelentlessPrefix")
    };
    Common.Prefixes.UnionWith((IEnumerable<int>) other5);
    HashSet<int> other6 = new HashSet<int>()
    {
      Common.GetModItem(ModConditions.aequusMod, "CosmicChest"),
      Common.GetModItem(ModConditions.aequusMod, "TinkerersGuidebook"),
      Common.GetModItem(ModConditions.aequusMod, "MoneyTrashcan"),
      Common.GetModItem(ModConditions.aequusMod, "VictorsReward"),
      Common.GetModItem(ModConditions.calamityMod, "MushroomPlasmaRoot"),
      Common.GetModItem(ModConditions.calamityMod, "InfernalBlood"),
      Common.GetModItem(ModConditions.calamityMod, "RedLightningContainer"),
      Common.GetModItem(ModConditions.calamityMod, "ElectrolyteGelPack"),
      Common.GetModItem(ModConditions.calamityMod, "StarlightFuelCell"),
      Common.GetModItem(ModConditions.calamityMod, "Ectoheart"),
      Common.GetModItem(ModConditions.calamityMod, "CelestialOnion"),
      Common.GetModItem(ModConditions.clickerClassMod, "HeavenlyChip"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "EnergyCrystal"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "DeerSinew"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsCreditCard"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsDiscountCard"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "MutantsPact"),
      Common.GetModItem(ModConditions.fargosSoulsMod, "RabiesVaccine"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "Americano"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "Latte"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "Mocha"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "Cappuccino"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "AirHandcanon"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "HotCase"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "GreatCrystal"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "WhimInABottle"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "SunsHeart"),
      Common.GetModItem(ModConditions.homewardJourneyMod, "TheSwitch"),
      Common.GetModItem(ModConditions.martainsOrderMod, "FishOfPurity"),
      Common.GetModItem(ModConditions.martainsOrderMod, "FishOfSpirit"),
      Common.GetModItem(ModConditions.martainsOrderMod, "FishOfWrath"),
      Common.GetModItem(ModConditions.martainsOrderMod, "ShimmerFish"),
      Common.GetModItem(ModConditions.martainsOrderMod, "MerchantBag"),
      Common.GetModItem(ModConditions.martainsOrderMod, "FirstAidTreatments"),
      Common.GetModItem(ModConditions.martainsOrderMod, "MartiniteBless"),
      Common.GetModItem(ModConditions.redemptionMod, "GalaxyHeart"),
      Common.GetModItem(ModConditions.redemptionMod, "MedicKit"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ScarletStar"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VioletStar"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SoulHeart"),
      Common.GetModItem(ModConditions.thoriumMod, "AstralWave"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationGem")
    };
    Common.PermanentUpgrades.UnionWith((IEnumerable<int>) other6);
    HashSet<int> other7 = new HashSet<int>()
    {
      Common.GetModItem(ModConditions.calamityMod, "EnchantedStarfish"),
      Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "StaminaCrystal"),
      Common.GetModItem(ModConditions.ragnarokMod, "InspirationEssence"),
      Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VoidenAnkh"),
      Common.GetModItem(ModConditions.thoriumMod, "CrystalWave"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationFragment"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationShard"),
      Common.GetModItem(ModConditions.thoriumMod, "InspirationCrystalNew")
    };
    Common.PermanentMultiUseUpgrades.UnionWith((IEnumerable<int>) other7);
    HashSet<int> other8 = new HashSet<int>()
    {
      Common.GetModItem(ModConditions.calamityMod, "RogueEmblem"),
      Common.GetModItem(ModConditions.clickerClassMod, "ClickerEmblem"),
      Common.GetModItem(ModConditions.martainsOrderMod, "ThrowerEmblem"),
      Common.GetModItem(ModConditions.martainsOrderMod, "NeutralEmblem"),
      Common.GetModItem(ModConditions.orchidMod, "GuardianEmblem"),
      Common.GetModItem(ModConditions.orchidMod, "ShamanEmblem"),
      Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "NinjaEmblem"),
      Common.GetModItem(ModConditions.thoriumMod, "BardEmblem"),
      Common.GetModItem(ModConditions.thoriumMod, "ClericEmblem"),
      Common.GetModItem(ModConditions.thoriumMod, "NinjaEmblem")
    };
    Common.Emblems.UnionWith((IEnumerable<int>) other8);
    HashSet<int> other9 = new HashSet<int>()
    {
      Common.GetModTile(ModConditions.aequusMod, "Manacle"),
      Common.GetModTile(ModConditions.aequusMod, "Mistral"),
      Common.GetModTile(ModConditions.aequusMod, "Moonflower"),
      Common.GetModTile(ModConditions.aequusMod, "Moray"),
      Common.GetModTile(ModConditions.thoriumMod, "IllumiteChunk"),
      Common.GetModTile(ModConditions.thoriumMod, "LifeQuartz"),
      Common.GetModTile(ModConditions.thoriumMod, "LodeStone"),
      Common.GetModTile(ModConditions.thoriumMod, "SmoothCoal"),
      Common.GetModTile(ModConditions.thoriumMod, "ThoriumOre"),
      Common.GetModTile(ModConditions.thoriumMod, "ValadiumChunk")
    };
    Common.IgnoredTilesForExplosives.UnionWith((IEnumerable<int>) other9);
    HashSet<Mod> other10 = new HashSet<Mod>();
    if (ModConditions.confectionRebakedLoaded)
      other10.Add(ModConditions.confectionRebakedMod);
    if (ModConditions.depthsLoaded)
      other10.Add(ModConditions.depthsMod);
    if (ModConditions.infectedQualitiesLoaded)
      other10.Add(ModConditions.infectedQualitiesMod);
    if (ModConditions.martainsOrderLoaded)
      other10.Add(ModConditions.martainsOrderMod);
    if (ModConditions.orchidLoaded)
      other10.Add(ModConditions.orchidMod);
    if (ModConditions.remnantsLoaded)
      other10.Add(ModConditions.remnantsMod);
    Common.IgnoredModsForExplosives.UnionWith((IEnumerable<Mod>) other10);
    for (int count = BuffID.Count; count < BuffLoader.BuffCount; ++count)
    {
      if (BuffID.Sets.IsAFlaskBuff[BuffLoader.GetBuff(count).Type] && !Common.FlaskBuffs.Contains(BuffLoader.GetBuff(count).Type))
        Common.FlaskBuffs.Add(BuffLoader.GetBuff(count).Type);
    }
    if (!ModConditions.thoriumLoaded)
      return;
    HashSet<int> other11 = new HashSet<int>()
    {
      Common.GetModBuff(ModConditions.thoriumMod, "DeepFreezeCoatingBuff"),
      Common.GetModBuff(ModConditions.thoriumMod, "ExplosiveCoatingBuff"),
      Common.GetModBuff(ModConditions.thoriumMod, "GorgonCoatingBuff"),
      Common.GetModBuff(ModConditions.thoriumMod, "SporeCoatingBuff"),
      Common.GetModBuff(ModConditions.thoriumMod, "ToxicCoatingBuff")
    };
    Common.ThoriumCoatings.UnionWith((IEnumerable<int>) other11);
  }

  public static void Reset()
  {
  }

  public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
  {
    float num = (float) ((Math.Sin(2.0 * Math.PI / (double) seconds * (double) Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
    return Color.Lerp(firstColor, secondColor, num);
  }

  public static bool IsCoin(int type) => type >= 71 && type <= 74;

  public static ulong CalculateCoinValue(int type, uint stack)
  {
    ulong coinValue;
    switch (type)
    {
      case 71:
        coinValue = (ulong) stack;
        break;
      case 72:
        coinValue = (ulong) stack * 100UL;
        break;
      case 73:
        coinValue = (ulong) stack * 10000UL;
        break;
      case 74:
        coinValue = (ulong) stack * 1000000UL;
        break;
      default:
        coinValue = 0UL;
        break;
    }
    return coinValue;
  }

  public static List<Item> ConvertCopperValueToCoins(ulong value)
  {
    (ulong, ulong) tuple1 = Math.DivRem(value, 1000000UL);
    ulong num1 = tuple1.Item1;
    (ulong, ulong) tuple2 = Math.DivRem(tuple1.Item2, 10000UL);
    ulong num2 = tuple2.Item1;
    (ulong num3, ulong num4) = Math.DivRem(tuple2.Item2, 100UL);
    List<Item> coins = new List<Item>();
    for (; num1 > 0UL; num1 -= Math.Min(num1, (ulong) Common.PlatinumMaxStack))
      coins.Add(new Item(74, Math.Min((int) num1, Common.PlatinumMaxStack), 0));
    coins.Add(new Item(73, (int) num2, 0));
    coins.Add(new Item(72, (int) num3, 0));
    coins.Add(new Item(71, (int) num4, 0));
    return coins;
  }

  public static int GetModItem(Mod mod, string itemName)
  {
    ModItem modItem;
    return mod != null && mod.TryFind<ModItem>(itemName, ref modItem) && modItem != null ? modItem.Type : 0;
  }

  public static int GetModProjectile(Mod mod, string projName)
  {
    ModProjectile modProjectile;
    return mod != null && mod.TryFind<ModProjectile>(projName, ref modProjectile) && modProjectile != null ? modProjectile.Type : 0;
  }

  public static int GetModNPC(Mod mod, string npcName)
  {
    ModNPC modNpc;
    return mod != null && mod.TryFind<ModNPC>(npcName, ref modNpc) && modNpc != null ? modNpc.Type : 0;
  }

  public static int GetModTile(Mod mod, string tileName)
  {
    ModTile modTile;
    return mod != null && mod.TryFind<ModTile>(tileName, ref modTile) && modTile != null ? (int) ((ModBlockType) modTile).Type : -1;
  }

  public static int GetModWall(Mod mod, string wallName)
  {
    ModWall modWall;
    return mod != null && mod.TryFind<ModWall>(wallName, ref modWall) && modWall != null ? (int) ((ModBlockType) modWall).Type : 0;
  }

  public static int GetModBuff(Mod mod, string buffName)
  {
    ModBuff modBuff;
    return mod != null && mod.TryFind<ModBuff>(buffName, ref modBuff) && modBuff != null ? modBuff.Type : -1;
  }

  public static int GetModPrefix(Mod mod, string prefixName)
  {
    ModPrefix modPrefix;
    return mod != null && mod.TryFind<ModPrefix>(prefixName, ref modPrefix) && modPrefix != null ? modPrefix.Type : -1;
  }

  public static DamageClass GetModDamageClass(Mod mod, string className)
  {
    DamageClass damageClass;
    return mod != null && mod.TryFind<DamageClass>(className, ref damageClass) && damageClass != null ? damageClass : DamageClass.Default;
  }

  public static string ModBuffAsset(Mod mod, int buffType)
  {
    return mod != null && BuffLoader.GetBuff(buffType) != null ? ((ModTexturedType) BuffLoader.GetBuff(buffType)).Texture : "QoLCompendium/Assets/Items/PermanentBuff";
  }

  public static void CreateBagRecipe(int[] items, int bagID)
  {
    for (int index = 0; index < items.Length; ++index)
      Common.CreateSimpleRecipe(bagID, items[index], 18, 1, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.BossBags", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BossBagRecipes)));
  }

  public static void CreateCrateRecipe(
    int result,
    int crateID,
    int crateHardmodeID,
    int crateCount,
    params Condition[] conditions)
  {
    Recipe itemRecipe1 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes), result, displayText: "Mods.QoLCompendium.ItemToggledConditions.Crates");
    itemRecipe1.AddIngredient(crateID, crateCount);
    foreach (Condition condition in conditions)
      itemRecipe1.AddCondition(condition);
    itemRecipe1.AddTile(18);
    itemRecipe1.DisableDecraft();
    itemRecipe1.Register();
    Recipe itemRecipe2 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes), result, displayText: "Mods.QoLCompendium.ItemToggledConditions.Crates");
    itemRecipe2.AddIngredient(crateHardmodeID, crateCount);
    foreach (Condition condition in conditions)
      itemRecipe2.AddCondition(condition);
    itemRecipe2.AddTile(18);
    itemRecipe2.DisableDecraft();
    itemRecipe2.Register();
  }

  public static void CreateCrateHardmodeRecipe(
    int result,
    int crateHardmodeID,
    int crateCount,
    params Condition[] conditions)
  {
    Recipe itemRecipe = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes), result, displayText: "Mods.QoLCompendium.ItemToggledConditions.Crates");
    itemRecipe.AddIngredient(crateHardmodeID, crateCount);
    foreach (Condition condition in conditions)
      itemRecipe.AddCondition(condition);
    itemRecipe.AddTile(18);
    itemRecipe.DisableDecraft();
    itemRecipe.Register();
  }

  public static void CreateCrateWithKeyRecipe(
    int item,
    int crateID,
    int crateHardmodeID,
    int crateCount,
    int keyID,
    params Condition[] conditions)
  {
    Recipe itemRecipe1 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes), item, displayText: "Mods.QoLCompendium.ItemToggledConditions.Crates");
    itemRecipe1.AddIngredient(crateID, crateCount);
    itemRecipe1.AddIngredient(keyID, 1);
    foreach (Condition condition in conditions)
      itemRecipe1.AddCondition(condition);
    itemRecipe1.AddTile(18);
    itemRecipe1.DisableDecraft();
    itemRecipe1.Register();
    Recipe itemRecipe2 = ModConditions.GetItemRecipe((Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.CrateRecipes), item, displayText: "Mods.QoLCompendium.ItemToggledConditions.Crates");
    itemRecipe2.AddIngredient(crateHardmodeID, crateCount);
    itemRecipe2.AddIngredient(keyID, 1);
    foreach (Condition condition in conditions)
      itemRecipe2.AddCondition(condition);
    itemRecipe2.AddTile(18);
    itemRecipe2.DisableDecraft();
    itemRecipe2.Register();
  }

  public static void ConversionRecipe(int item1, int item2, int station)
  {
    Common.CreateSimpleRecipe(item1, item2, station, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.ItemConversions)));
    Common.CreateSimpleRecipe(item2, item1, station, 1, 1, false, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.ItemConversions", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.ItemConversions)));
  }

  public static void AddBannerGroupToItemRecipe(
    int recipeGroupID,
    int resultID,
    int resultAmount = 1,
    int groupAmount = 1,
    params Condition[] conditions)
  {
    Common.CreateSimpleRecipe(recipeGroupID, resultID, 18, groupAmount, resultAmount, true, true, conditions);
  }

  public static void AddBannerToItemRecipe(
    int bannerItemID,
    int resultID,
    int bannerAmount = 1,
    int resultAmount = 1,
    params Condition[] conditions)
  {
    Common.CreateSimpleRecipe(bannerItemID, resultID, 18, bannerAmount, resultAmount, true, false, conditions);
  }

  public static void AddBannerSetToItemRecipe(bool[] set, int resultID)
  {
    for (int index = 0; index < (int) NPCID.Count; ++index)
    {
      if (set[index])
      {
        int num = Item.NPCtoBanner(index);
        if (num > 0)
          Common.CreateSimpleRecipe(Item.BannerToItem(num), resultID, 18, 1, 1, true, false, ModConditions.ItemToggled("Mods.QoLCompendium.ItemToggledConditions.Banners", (Func<bool>) (() => QoLCompendium.QoLCompendium.mainConfig.BannerRecipes)));
      }
    }
  }

  public static void CreateSimpleRecipe(
    int ingredientID,
    int resultID,
    int tileID,
    int ingredientAmount = 1,
    int resultAmount = 1,
    bool disableDecraft = false,
    bool usesRecipeGroup = false,
    params Condition[] conditions)
  {
    Recipe recipe = Recipe.Create(resultID, resultAmount);
    if (usesRecipeGroup)
      recipe.AddRecipeGroup(ingredientID, ingredientAmount);
    else
      recipe.AddIngredient(ingredientID, ingredientAmount);
    recipe.AddTile(tileID);
    foreach (Condition condition in conditions)
      recipe.AddCondition(condition);
    if (disableDecraft)
      recipe.DisableDecraft();
    recipe.Register();
  }

  public static void SpawnBoss(Player player, int bossType)
  {
    if (((Entity) player).whoAmI != Main.myPlayer)
      return;
    SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
    Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(Vector2.UnitY, 800f));
    int num = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) vector2.X, (int) vector2.Y, bossType, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
    if (Main.netMode != 0)
      NetMessage.SendData(61, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, (float) bossType, 0.0f, 0.0f, 0, 0, 0);
    if (num == Main.maxNPCs || Main.netMode != 2)
      return;
    NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
  }

  public static Asset<Texture2D> GetAsset(string location, string filename, int fileNumber = -1)
  {
    if (fileNumber <= -1)
      return ModContent.Request<Texture2D>($"QoLCompendium/Assets/{location}/{filename}", (AssetRequestMode) 2);
    return ModContent.Request<Texture2D>($"QoLCompendium/Assets/{location}/{filename}{fileNumber.ToString()}", (AssetRequestMode) 2);
  }

  public static void TransmuteItems(int[] items)
  {
    for (int index = 0; index < items.Length; ++index)
    {
      if (index >= items.Length - 1)
        ItemID.Sets.ShimmerTransformToItem[((IEnumerable<int>) items).Last<int>()] = ((IEnumerable<int>) items).First<int>();
      else
        ItemID.Sets.ShimmerTransformToItem[items[index]] = items[index + 1];
    }
  }

  public static bool CheckToActivateGlintEffect(Item item)
  {
    return !Main.gameMenu && ((Entity) Main.LocalPlayer).active && (QoLCompendium.QoLCompendium.mainConfig.ActiveBuffsHaveEnchantedEffects && !item.IsAir && Main.LocalPlayer.GetModPlayer<QoLCPlayer>().activeBuffItems.Contains(item.type) || QoLCompendium.QoLCompendium.mainConfig.GoodPrefixesHaveEnchantedEffects && !item.IsAir && Common.Prefixes.Contains(item.prefix));
  }

  public static int GetSlotItemIsIn(Item lookForThis, Item[] inventory)
  {
    for (int slotItemIsIn = 0; slotItemIsIn < inventory.Length; ++slotItemIsIn)
    {
      if (inventory[slotItemIsIn].type == lookForThis.type)
        return slotItemIsIn;
    }
    return -1;
  }

  public static void SetDefaultsToPermanentBuff(Item item)
  {
    ((Entity) item).width = ((Entity) item).height = 16 /*0x10*/;
    item.consumable = false;
    item.maxStack = 1;
    item.SetShopValues((ItemRarityColor) 1, 0);
  }

  public static void HandleFlaskBuffs(Player player)
  {
    foreach (int flaskBuff in Common.FlaskBuffs)
      player.buffImmune[flaskBuff] = true;
  }

  public static void HandleCoatingBuffs(Player player)
  {
    foreach (int thoriumCoating in Common.ThoriumCoatings)
      player.buffImmune[thoriumCoating] = true;
  }

  public static StatModifier GetBestClassDamage(this Player player)
  {
    StatModifier statModifier1 = StatModifier.Default;
    StatModifier totalDamage1 = player.GetTotalDamage<GenericDamageClass>();
    statModifier1.Base = totalDamage1.Base;
    StatModifier statModifier2 = StatModifier.op_Multiply(statModifier1, ((StatModifier) ref totalDamage1).Multiplicative);
    statModifier2.Flat = totalDamage1.Flat;
    float num = 1f;
    StatModifier totalDamage2 = player.GetTotalDamage<MeleeDamageClass>();
    float additive1 = ((StatModifier) ref totalDamage2).Additive;
    if ((double) additive1 > (double) num)
      num = additive1;
    StatModifier totalDamage3 = player.GetTotalDamage<RangedDamageClass>();
    float additive2 = ((StatModifier) ref totalDamage3).Additive;
    if ((double) additive2 > (double) num)
      num = additive2;
    StatModifier totalDamage4 = player.GetTotalDamage<MagicDamageClass>();
    float additive3 = ((StatModifier) ref totalDamage4).Additive;
    if ((double) additive3 > (double) num)
      num = additive3;
    StatModifier totalDamage5 = player.GetTotalDamage<SummonDamageClass>();
    float additive4 = ((StatModifier) ref totalDamage5).Additive;
    if ((double) additive4 > (double) num)
      num = additive4;
    for (int index = 0; index < DamageClassLoader.DamageClassCount; ++index)
    {
      StatModifier totalDamage6 = player.GetTotalDamage(DamageClassLoader.GetDamageClass(index));
      float additive5 = ((StatModifier) ref totalDamage6).Additive;
      if ((double) additive5 > (double) num)
        num = additive5;
    }
    return StatModifier.op_Addition(statModifier2, num - 1f);
  }

  public static int ToFrames(float seconds, int extraUpdates = 0)
  {
    return (int) ((double) seconds * 60.0 * (double) (extraUpdates + 1));
  }

  public static int ToPixels(float blocks) => (int) ((double) blocks * 16.0);

  public static float ToSeconds(float frames, int extraUpdates = 0)
  {
    return frames / (float) (60 * (extraUpdates + 1));
  }

  public static float ToBlocks(float pixels) => pixels / 16f;

  public static Point16 PlayerCenterTile(Player player)
  {
    return new Point16((int) ((double) ((Entity) player).Center.X / 16.0), (int) ((double) ((Entity) player).Center.Y / 16.0));
  }

  public static int PlayerCenterTileX(Player player)
  {
    return (int) ((double) ((Entity) player).Center.X / 16.0);
  }

  public static int PlayerCenterTileY(Player player)
  {
    return (int) ((double) ((Entity) player).Center.Y / 16.0);
  }

  public static bool InGameWorldLeft(int x) => x > 39;

  public static bool InGameWorldRight(int x) => x < Main.maxTilesX - 39;

  public static bool InGameWorldTop(int y) => y > 39;

  public static bool InGameWorldBottom(int y) => y < Main.maxTilesY - 39;

  public static bool InGameWorld(int x, int y)
  {
    return x > 39 && x < Main.maxTilesX - 39 && y > 39 && y < Main.maxTilesY - 39;
  }

  public static bool InWorldLeft(int x) => x >= 0;

  public static bool InWorldRight(int x) => x < Main.maxTilesX;

  public static bool InWorldTop(int y) => y >= 0;

  public static bool InWorldBottom(int y) => y < Main.maxTilesY;

  public static bool InWorld(int x, int y)
  {
    return x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY;
  }

  public static int CoordsX(int x) => x * 2 - Main.maxTilesX;

  public static int CoordsY(int y) => y * 2 - (int) Main.worldSurface * 2;

  public static string CoordsString(int x, int y)
  {
    x = x * 2 - Main.maxTilesX;
    y = y * 2 - (int) Main.worldSurface * 2;
    string str1 = x < 0 ? " west, " : " east, ";
    string str2 = y < 0 ? " surface." : " underground.";
    x = x < 0 ? x * -1 : x;
    y = y < 0 ? y * -1 : y;
    return x.ToString() + str1 + y.ToString() + str2;
  }

  public static void TileSafe(int x, int y)
  {
    if (x < 0 || y < 0 || x > Main.ActiveWorldFileData.WorldSizeX || y > Main.ActiveWorldFileData.WorldSizeY || !Tile.op_Equality(((Tilemap) ref Main.tile)[x, y], (ArgumentException) null))
      return;
    Tile tile = ((Tilemap) ref Main.tile)[x, y];
    ((Tile) ref tile).ResetToType((ushort) 0);
  }

  public static bool TileNull(int x, int y)
  {
    return Tile.op_Equality(((Tilemap) ref Main.tile)[x, y], (ArgumentException) null);
  }

  public static bool SolidTile(int x, int y)
  {
    Tile tile = ((Tilemap) ref Main.tile)[x, y];
    return !Common.TileNull(x, y) && ((Tile) ref tile).HasTile && Main.tileSolid[(int) ((Tile) ref tile).TileType] && !Main.tileSolidTop[(int) ((Tile) ref tile).TileType] && !((Tile) ref tile).IsHalfBlock && ((Tile) ref tile).Slope == null && !((Tile) ref tile).IsActuated;
  }

  public static bool SearchBelow(Player player, Func<int, int, bool> toSearch, int gap)
  {
    int num1 = Common.PlayerCenterTileX(player);
    int num2 = Common.PlayerCenterTileY(player);
    for (int index = 0; Common.InGameWorldLeft(num1 - index) || Common.InGameWorldRight(num1 + index); index += gap)
    {
      for (int y = num2; Common.InGameWorldBottom(y); y += gap)
      {
        int x1 = num1 - index;
        int x2 = num1 + index;
        if (Common.InGameWorldLeft(x1))
        {
          Common.TileSafe(x1, y);
          if (toSearch(x1, y))
            return true;
        }
        if (Common.InGameWorldRight(x2))
        {
          Common.TileSafe(x2, y);
          if (toSearch(x2, y))
            return true;
        }
      }
    }
    return false;
  }

  public static void AddAfter<T>(this List<T> list, T element, T item)
  {
    int num = list.IndexOf(element);
    list.Insert(num + 1, item);
  }

  public static string GetTooltipValue(string suffix, params object[] args)
  {
    return Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips." + suffix, args);
  }

  public static void AddLastTooltip(List<TooltipLine> tooltips, TooltipLine tooltip)
  {
    TooltipLine last = tooltips.FindLast((Predicate<TooltipLine>) (t => t.Mod == "Terraria"));
    tooltips.AddAfter<TooltipLine>(last, tooltip);
  }

  public static string GetFullNameById(int id, int subtype = -1)
  {
    ModTile tile = TileLoader.GetTile(id);
    if (tile != null)
    {
      string name1 = ((ModType) tile).Mod.Name;
      string name2 = ((ModType) tile).Name;
      string str;
      if (subtype < 0)
        str = "";
      else
        str = $":{subtype}";
      return $"{name1}:{name2}{str}";
    }
    if (id >= (int) TileID.Count)
      return (string) null;
    string name = TileID.Search.GetName(id);
    string str1;
    if (subtype < 0)
      str1 = "";
    else
      str1 = $":{subtype}";
    return $"Terraria:{name}{str1}";
  }

  public static void UpdateWhitelist(int typeId, string name, int style = -1, bool remove = false)
  {
    if (!remove)
    {
      if (!QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist.Contains(name))
      {
        QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist.Add(name);
        Common.SaveConfig((ModConfig) QoLCompendium.QoLCompendium.mainConfig);
      }
      if (Main.netMode != 0)
        return;
      if (KeybindPlayer.timeout <= (byte) 0)
      {
        KeybindPlayer.timeout = byte.MaxValue;
      }
      else
      {
        KeybindPlayer.timeout = (byte) 0;
        QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist.Add(name);
        Common.SaveConfig((ModConfig) QoLCompendium.QoLCompendium.mainConfig);
      }
    }
    else
    {
      if (!QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist.Contains(name))
        return;
      QoLCompendium.QoLCompendium.mainConfig.VeinMinerTileWhitelist.Remove(name);
      Common.SaveConfig((ModConfig) QoLCompendium.QoLCompendium.mainConfig);
    }
  }

  public static bool TryAcceptChanges(int whoAmI, ref NetworkText message)
  {
    if (NetMessage.DoesPlayerSlotCountAsAHost(whoAmI))
      return true;
    message = NetworkText.FromKey(Language.GetTextValue("Mods.QoLCompendium.Messages.HostOnly"), Array.Empty<object>());
    return false;
  }

  public static void SaveConfig(ModConfig config)
  {
    ((MethodBase) (typeof (ConfigManager).GetMethod("Save", (BindingFlags) 40) ?? throw new Exception("Config file could not be created or updated at:\n'{path}'"))).Invoke((object) null, new object[1]
    {
      (object) config
    });
  }

  public enum PlacedPlatformStyles
  {
    Wood,
    Ebonwood,
    RichMahogany,
    Pearlwood,
    Bone,
    Shadewood,
    BlueBrick,
    PinkBrick,
    GreenBrick,
    MetalShelf,
    BrassShelf,
    WoodShelf,
    DungeonShelf,
    Obsidian,
    Glass,
    Pumpkin,
    SpookyWood,
    PalmWood,
    Mushroom,
    BorealWood,
    Slime,
    Steampunk,
    Skyware,
    LivingWood,
    Honey,
    Cactus,
    Martian,
    Meteorite,
    Granite,
    Marble,
    Crystal,
    Golden,
    DynastyWood,
    Lihzahrd,
    Flesh,
    Frozen,
    Spider,
    Lesion,
    Solar,
    Vortex,
    Nebula,
    Stardust,
    Sandstone,
    Stone,
    Bamboo,
    Reef,
    Balloon,
    AshWood,
    Echo,
  }

  public enum PlacedTableStyles1
  {
    Wooden,
    Ebonwood,
    RichMahogany,
    Pearlwood,
    Bone,
    Flesh,
    LivingWood,
    Skyware,
    Shadewood,
    Lihzahrd,
    BlueDungeon,
    GreenDungeon,
    PinkDungeon,
    Obsidian,
    Gothic,
    Glass,
    Banquet,
    Bar,
    Golden,
    Honey,
    Steampunk,
    Pumpkin,
    Spooky,
    Pine,
    Frozen,
    Dynasty,
    PalmWood,
    Mushroom,
    BorealWood,
    Slime,
    Cactus,
    Martian,
    Meteorite,
    Granite,
    Marble,
  }

  public enum PlacedTableStyles2
  {
    Crystal,
    Spider,
    Lesion,
    Solar,
    Vortex,
    Nebula,
    Stardust,
    Sandstone,
    Bamboo,
    Reef,
    Balloon,
    AshWood,
  }

  public enum PlacedChairStyles
  {
    Wooden,
    Tiolet,
    Ebonwood,
    RichMahogany,
    Pearlwood,
    LivingWood,
    Cactus,
    Bone,
    Flesh,
    Mushroom,
    Skyware,
    Shadewood,
    Lihzahrd,
    BlueDungeon,
    GreenDungeon,
    PinkDungeon,
    Obsidian,
    Gothic,
    Glass,
    Golden,
    GoldenToilet,
    BarStool,
    Honey,
    Steampunk,
    Pumpkin,
    Spooky,
    Pine,
    Dynasty,
    Frozen,
    PalmWood,
    BorealWood,
    Slime,
    Martian,
    Meteorite,
    Granite,
    Marble,
    Crystal,
    Spider,
    Lesion,
    Solar,
    Vortex,
    Nebula,
    Stardust,
    Sandstone,
    Bamboo,
  }

  public enum PlacedDoorStyles
  {
    Wooden,
    Ebonwood,
    RichMahogany,
    Pearlwood,
    Cactus,
    Flesh,
    Mushroom,
    LivingWood,
    Bone,
    Skyware,
    Shadewood,
    LockedLihzahrd,
    Lihzahrd,
    Dungeon,
    Lead,
    Iron,
    BlueDungeon,
    GreenDungeon,
    PinkDungeon,
    Obsidian,
    Glass,
    Golden,
    Honey,
    Steampunk,
    Pumpkin,
    Spooky,
    Pine,
    Frozen,
    Dynasty,
    PalmWood,
    BorealWood,
    Slime,
    Martian,
    Meteorite,
    Granite,
    Marble,
    Crystal,
    Spider,
    Lesion,
    Solar,
    Vortex,
    Nebula,
    Stardust,
    Sandstone,
    Stone,
    Bamboo,
  }

  public enum PlacedTorchStyles
  {
    Torch,
    BlueTorch,
    RedTorch,
    GreenTorch,
    PurpleTorch,
    WhiteTorch,
    YellowTorch,
    DemonTorch,
    CursedTorch,
    IceTorch,
    OrangeTorch,
    IchorTorch,
    UltrabrightTorch,
    BoneTorch,
    RainbowTorch,
    PinkTorch,
    DesertTorch,
    CoralTorch,
    CorruptTorch,
    CrimsonTorch,
    HallowedTorch,
    JungleTorch,
    MushroomTorch,
    AetherTorch,
  }

  public enum AlchemyHerbStyles
  {
    Daybloom,
    Moonglow,
    Blinkroot,
    Deathweed,
    Waterleaf,
    Fireblossom,
    Shiverthorn,
  }

  public interface IRightClickOverrideWhenHeld
  {
    bool RightClickOverrideWhileHeld(
      ref Item heldItem,
      Item[] inv,
      int context,
      int slot,
      Player player,
      QoLCPlayer qPlayer);
  }
}
