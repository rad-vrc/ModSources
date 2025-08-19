// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.PermanentBuffSystems.PermanentBuffPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace QoLCompendium.Core.PermanentBuffSystems;

public class PermanentBuffPlayer : ModPlayer
{
  public bool buffActive;
  public SortedSet<IPermanentBuff> potionEffects = new SortedSet<IPermanentBuff>();
  public SortedSet<IPermanentModdedBuff> modPotionEffects = new SortedSet<IPermanentModdedBuff>();
  public static bool[] permanentBuffsBools = new bool[Enum.GetValues(typeof (PermanentBuffPlayer.PermanentBuffs)).Length];
  public static bool[] permanentCalamityBuffsBools = new bool[Enum.GetValues(typeof (PermanentBuffPlayer.PermanentCalamityBuffs)).Length];
  public static bool[] permanentMartinsOrderBuffsBools = new bool[Enum.GetValues(typeof (PermanentBuffPlayer.PermanentMartinsOrderBuffs)).Length];
  public static bool[] permanentSOTSBuffsBools = new bool[Enum.GetValues(typeof (PermanentBuffPlayer.PermanentSOTSBuffs)).Length];
  public static bool[] permanentSpiritClassicBuffsBools = new bool[Enum.GetValues(typeof (PermanentBuffPlayer.PermanentSpiritClassicBuffs)).Length];
  public static bool[] permanentThoriumBuffsBools = new bool[Enum.GetValues(typeof (PermanentBuffPlayer.PermanentThoriumBuffs)).Length];

  public virtual void ResetEffects() => this.ResetValues();

  public virtual void UpdateDead() => this.ResetValues();

  public virtual void Unload() => PermanentBuffPlayer.UnloadBools();

  public void ResetValues()
  {
    this.buffActive = false;
    this.potionEffects.Clear();
    this.modPotionEffects.Clear();
  }

  public virtual void PostUpdateEquips()
  {
    this.CheckForPotions(this.Player.bank.item);
    this.CheckForPotions(this.Player.bank2.item);
    this.CheckForPotions(this.Player.bank3.item);
    this.CheckForPotions(this.Player.bank4.item);
    this.UpdatePotions();
  }

  public void UpdatePotions()
  {
    foreach (IPermanentBuff potionEffect in this.potionEffects)
      potionEffect.ApplyEffect(this);
    foreach (IPermanentModdedBuff modPotionEffect in this.modPotionEffects)
      modPotionEffect.ApplyEffect(this);
  }

  public void CheckForPotions(Item[] inventory)
  {
    foreach (Item obj in inventory)
    {
      if (!obj.IsAir && obj.ModItem is IPermanentBuffItem modItem1)
        modItem1.ApplyBuff(this);
      if (!obj.IsAir && obj.ModItem is IPermanentModdedBuffItem modItem2)
        modItem2.ApplyBuff(this);
    }
  }

  public virtual void SaveData(TagCompound tag)
  {
    List<string> stringList1 = new List<string>();
    for (int index = 0; index < PermanentBuffPlayer.PermanentBuffsBools.Length; ++index)
    {
      if (PermanentBuffPlayer.PermanentBuffsBools[index])
        stringList1.Add("QoLCPBuff" + index.ToString());
    }
    tag.Add("QoLCPBuff", (object) stringList1);
    List<string> stringList2 = new List<string>();
    for (int index = 0; index < PermanentBuffPlayer.PermanentCalamityBuffsBools.Length; ++index)
    {
      if (PermanentBuffPlayer.PermanentCalamityBuffsBools[index])
        stringList2.Add("QoLCPCalamityBuff" + index.ToString());
    }
    tag.Add("QoLCPCalamityBuff", (object) stringList2);
    List<string> stringList3 = new List<string>();
    for (int index = 0; index < PermanentBuffPlayer.PermanentMartinsOrderBuffsBools.Length; ++index)
    {
      if (PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[index])
        stringList3.Add("QoLCPMartinsOrderBuff" + index.ToString());
    }
    tag.Add("QoLCPMartinsOrderBuff", (object) stringList3);
    List<string> stringList4 = new List<string>();
    for (int index = 0; index < PermanentBuffPlayer.PermanentSOTSBuffsBools.Length; ++index)
    {
      if (PermanentBuffPlayer.PermanentSOTSBuffsBools[index])
        stringList4.Add("QoLCPSOTSBuff" + index.ToString());
    }
    tag.Add("QoLCPSOTSBuff", (object) stringList4);
    List<string> stringList5 = new List<string>();
    for (int index = 0; index < PermanentBuffPlayer.PermanentSpiritClassicBuffsBools.Length; ++index)
    {
      if (PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[index])
        stringList5.Add("QoLCPSpiritClassicBuff" + index.ToString());
    }
    tag.Add("QoLCPSpiritClassicBuff", (object) stringList5);
    List<string> stringList6 = new List<string>();
    for (int index = 0; index < PermanentBuffPlayer.PermanentThoriumBuffsBools.Length; ++index)
    {
      if (PermanentBuffPlayer.PermanentThoriumBuffsBools[index])
        stringList6.Add("QoLCPThoriumBuff" + index.ToString());
    }
    tag.Add("QoLCPThoriumBuff", (object) stringList6);
  }

  public virtual void LoadData(TagCompound tag)
  {
    IList<string> list1 = tag.GetList<string>("QoLCPBuff");
    for (int index = 0; index < PermanentBuffPlayer.PermanentBuffsBools.Length; ++index)
      PermanentBuffPlayer.PermanentBuffsBools[index] = (list1.Contains($"QoLCPBuff{index}") ? 1 : 0) != 0;
    IList<string> list2 = tag.GetList<string>("QoLCPCalamityBuff");
    for (int index = 0; index < PermanentBuffPlayer.PermanentCalamityBuffsBools.Length; ++index)
      PermanentBuffPlayer.PermanentCalamityBuffsBools[index] = (list2.Contains($"QoLCPCalamityBuff{index}") ? 1 : 0) != 0;
    IList<string> list3 = tag.GetList<string>("QoLCPMartinsOrderBuff");
    for (int index = 0; index < PermanentBuffPlayer.PermanentMartinsOrderBuffsBools.Length; ++index)
      PermanentBuffPlayer.PermanentMartinsOrderBuffsBools[index] = (list3.Contains($"QoLCPMartinsOrderBuff{index}") ? 1 : 0) != 0;
    IList<string> list4 = tag.GetList<string>("QoLCPSOTSBuff");
    for (int index = 0; index < PermanentBuffPlayer.PermanentSOTSBuffsBools.Length; ++index)
      PermanentBuffPlayer.PermanentSOTSBuffsBools[index] = (list4.Contains($"QoLCPSOTSBuff{index}") ? 1 : 0) != 0;
    IList<string> list5 = tag.GetList<string>("QoLCPSpiritClassicBuff");
    for (int index = 0; index < PermanentBuffPlayer.PermanentSpiritClassicBuffsBools.Length; ++index)
      PermanentBuffPlayer.PermanentSpiritClassicBuffsBools[index] = (list5.Contains($"QoLCPSpiritClassicBuff{index}") ? 1 : 0) != 0;
    IList<string> list6 = tag.GetList<string>("QoLCPThoriumBuff");
    for (int index = 0; index < PermanentBuffPlayer.PermanentThoriumBuffsBools.Length; ++index)
      PermanentBuffPlayer.PermanentThoriumBuffsBools[index] = (list6.Contains($"QoLCPThoriumBuff{index}") ? 1 : 0) != 0;
  }

  public static void UnloadBools()
  {
    PermanentBuffPlayer.PermanentBuffsBools = (bool[]) null;
    PermanentBuffPlayer.PermanentCalamityBuffsBools = (bool[]) null;
    PermanentBuffPlayer.PermanentMartinsOrderBuffsBools = (bool[]) null;
    PermanentBuffPlayer.PermanentSOTSBuffsBools = (bool[]) null;
    PermanentBuffPlayer.PermanentSpiritClassicBuffsBools = (bool[]) null;
    PermanentBuffPlayer.PermanentThoriumBuffsBools = (bool[]) null;
  }

  public static bool[] PermanentBuffsBools
  {
    get => PermanentBuffPlayer.permanentBuffsBools;
    set => PermanentBuffPlayer.permanentBuffsBools = value;
  }

  public static bool[] PermanentCalamityBuffsBools
  {
    get => PermanentBuffPlayer.permanentCalamityBuffsBools;
    set => PermanentBuffPlayer.permanentCalamityBuffsBools = value;
  }

  public static bool[] PermanentMartinsOrderBuffsBools
  {
    get => PermanentBuffPlayer.permanentMartinsOrderBuffsBools;
    set => PermanentBuffPlayer.permanentMartinsOrderBuffsBools = value;
  }

  public static bool[] PermanentSOTSBuffsBools
  {
    get => PermanentBuffPlayer.permanentSOTSBuffsBools;
    set => PermanentBuffPlayer.permanentSOTSBuffsBools = value;
  }

  public static bool[] PermanentSpiritClassicBuffsBools
  {
    get => PermanentBuffPlayer.permanentSpiritClassicBuffsBools;
    set => PermanentBuffPlayer.permanentSpiritClassicBuffsBools = value;
  }

  public static bool[] PermanentThoriumBuffsBools
  {
    get => PermanentBuffPlayer.permanentThoriumBuffsBools;
    set => PermanentBuffPlayer.permanentThoriumBuffsBools = value;
  }

  public enum PermanentBuffs
  {
    BastStatue,
    Campfire,
    GardenGnome,
    HeartLantern,
    Honey,
    PeaceCandle,
    ShadowCandle,
    StarInABottle,
    Sunflower,
    WaterCandle,
    AmmoReservation,
    Archery,
    Battle,
    BiomeSight,
    Builder,
    Calm,
    Crate,
    Dangersense,
    Endurance,
    ExquisitelyStuffed,
    Featherfall,
    Fishing,
    Flipper,
    Gills,
    Gravitation,
    Heartreach,
    Hunter,
    Inferno,
    Invisibility,
    Ironskin,
    Lifeforce,
    Lucky,
    MagicPower,
    ManaRegeneration,
    Mining,
    NightOwl,
    ObsidianSkin,
    PlentySatisfied,
    Rage,
    Regeneration,
    Shine,
    Sonar,
    Spelunker,
    Summoning,
    Swiftness,
    Thorns,
    Tipsy,
    Titan,
    Warmth,
    WaterWalking,
    WellFed,
    Wrath,
    AmmoBox,
    BewitchingTable,
    CrystalBall,
    SharpeningStation,
    SliceOfCake,
    WarTable,
  }

  public enum PermanentCalamityBuffs
  {
    ChaosCandle,
    CorruptionEffigy,
    CrimsonEffigy,
    EffigyOfDecay,
    ResilientCandle,
    SpitefulCandle,
    TranquilityCandle,
    VigorousCandle,
    WeightlessCandle,
    AnechoicCoating,
    AstralInjection,
    Baguette,
    Bloodfin,
    Bounding,
    Calcium,
    CeaselessHunger,
    GravityNormalizer,
    Omniscience,
    Photosynthesis,
    Shadow,
    Soaring,
    Sulphurskin,
    Tesla,
    Zen,
    Zerg,
    AstraJelly,
    Astracola,
    ExoBaguette,
    SupremeLuck,
    TitanScale,
    VoidCandle,
    SoyMilk,
    YharimsStimulants,
  }

  public enum PermanentMartinsOrderBuffs
  {
    BlackHole,
    Charging,
    Defender,
    Empowerment,
    Evocation,
    GourmetFlavor,
    Haste,
    Healing,
    Rockskin,
    Shooter,
    Soul,
    SpellCaster,
    Starreach,
    Sweeper,
    Thrower,
    Whipper,
    ZincPill,
    Archeology,
    SporeFarm,
  }

  public enum PermanentSOTSBuffs
  {
    Assassination,
    Bluefire,
    Brittle,
    DoubleVision,
    Harmony,
    Nightmare,
    Ripple,
    Roughskin,
    SoulAccess,
    Vibe,
    DigitalDisplay,
  }

  public enum PermanentSpiritClassicBuffs
  {
    CoiledEnergizer,
    KoiTotem,
    SunPot,
    TheCouch,
    Jump,
    MirrorCoat,
    MoonJelly,
    Runescribe,
    Soulguard,
    Spirit,
    Soaring,
    Sporecoid,
    Starburn,
    Steadfast,
    Toxin,
    Zephyr,
  }

  public enum PermanentThoriumBuffs
  {
    Mistletoe,
    AquaAffinity,
    Arcane,
    Artillery,
    Assassin,
    BloodRush,
    BouncingFlame,
    CactusFruit,
    Conflagration,
    Creativity,
    Earworm,
    Frenzy,
    Glowing,
    Holy,
    Hydration,
    InspirationalReach,
    Kinetic,
    Warmonger,
    BatRepellent,
    FishRepellent,
    InsectRepellent,
    SkeletonRepellent,
    ZombieRepellent,
    Altar,
    ConductorsStand,
    NinjaRack,
    Deathsinger,
    InspirationRegeneration,
  }
}
