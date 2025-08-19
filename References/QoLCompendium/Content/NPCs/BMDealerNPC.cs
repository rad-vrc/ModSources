// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Content.NPCs.BMDealerNPC
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Core;
using QoLCompendium.Core.UI.Panels;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Content.NPCs;

[AutoloadHead]
public class BMDealerNPC : ModNPC
{
  public static int shopNum;
  public static string ShopName;

  public virtual string Texture => "QoLCompendium/Content/NPCs/BMDealerNPC";

  public virtual bool IsLoadingEnabled(Mod mod)
  {
    return QoLCompendium.QoLCompendium.mainConfig.BlackMarketDealerCanSpawn;
  }

  public virtual void SetStaticDefaults()
  {
    Main.npcFrameCount[this.NPC.type] = 26;
    NPCID.Sets.ExtraFramesCount[this.NPC.type] = 9;
    NPCID.Sets.AttackFrameCount[this.NPC.type] = 5;
    NPCID.Sets.DangerDetectRange[this.NPC.type] = 800;
    NPCID.Sets.AttackType[this.NPC.type] = 0;
    NPCID.Sets.AttackTime[this.NPC.type] = 90;
    NPCID.Sets.AttackAverageChance[this.NPC.type] = 30;
    NPCID.Sets.HatOffsetY[this.NPC.type] = 4;
    NPCHappiness npcHappiness = this.NPC.Happiness;
    npcHappiness = ((NPCHappiness) ref npcHappiness).SetBiomeAffection<SnowBiome>((AffectionLevel) 100);
    npcHappiness = ((NPCHappiness) ref npcHappiness).SetBiomeAffection<OceanBiome>((AffectionLevel) 50);
    npcHappiness = ((NPCHappiness) ref npcHappiness).SetBiomeAffection<DesertBiome>((AffectionLevel) -50);
    npcHappiness = ((NPCHappiness) ref npcHappiness).SetNPCAffection(550, (AffectionLevel) 100);
    npcHappiness = ((NPCHappiness) ref npcHappiness).SetNPCAffection(19, (AffectionLevel) 50);
    npcHappiness = ((NPCHappiness) ref npcHappiness).SetNPCAffection(208 /*0xD0*/, (AffectionLevel) -50);
    ((NPCHappiness) ref npcHappiness).SetNPCAffection(441, (AffectionLevel) -100);
  }

  public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
  {
    bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new IBestiaryInfoElement[2]
    {
      (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
      (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("He hails from a far away land to sell items that are difficult to get, but how did he even obtain them...")
    });
  }

  public virtual void SetDefaults()
  {
    this.NPC.townNPC = true;
    this.NPC.friendly = true;
    ((Entity) this.NPC).width = 18;
    ((Entity) this.NPC).height = 48 /*0x30*/;
    this.NPC.aiStyle = 7;
    this.NPC.damage = 15;
    this.NPC.defense = 25;
    this.NPC.lifeMax = 500;
    this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
    this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
    this.NPC.knockBackResist = 0.5f;
    this.AnimationType = 22;
  }

  public virtual bool CanTownNPCSpawn(int numTownNPCs)
  {
    return QoLCompendium.QoLCompendium.mainConfig.BlackMarketDealerCanSpawn;
  }

  public virtual List<string> SetNPCNameList()
  {
    return new List<string>()
    {
      "Bon",
      "Ned",
      "Jay",
      "Jack",
      "Jabon"
    };
  }

  public virtual void TownNPCAttackStrength(ref int damage, ref float knockback)
  {
    if (!Main.hardMode)
      damage = 10;
    if (!NPC.downedMoonlord && Main.hardMode)
      damage = 25;
    if (NPC.downedMoonlord)
      damage = 100;
    knockback = 8f;
  }

  public virtual void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
  {
    cooldown = 15;
    randExtraCooldown = 5;
  }

  public virtual void TownNPCAttackProj(ref int projType, ref int attackDelay)
  {
    projType = 30;
    attackDelay = 1;
  }

  public virtual void TownNPCAttackProjSpeed(
    ref float multiplier,
    ref float gravityCorrection,
    ref float randomOffset)
  {
    multiplier = 12f;
    randomOffset = 1f;
  }

  public virtual string GetChat()
  {
    string chat;
    switch (Main.rand.Next(4))
    {
      case 0:
        chat = "Illegal items are my specialty";
        break;
      case 1:
        chat = "Hand over some money, and I'll hook you up";
        break;
      case 2:
        chat = "Don't ask where I got this stuff";
        break;
      default:
        chat = "Hey kid, want some items?";
        break;
    }
    return chat;
  }

  public virtual void SetChatButtons(ref string button, ref string button2)
  {
    switch (BMDealerNPC.shopNum)
    {
      case 0:
        button = "Potions";
        BMDealerNPC.ShopName = "Potions";
        break;
      case 1:
        button = "Flasks, Stations & Foods";
        BMDealerNPC.ShopName = "Flasks, Stations & Foods";
        break;
      case 2:
        button = "Materials";
        BMDealerNPC.ShopName = "Materials";
        break;
      case 3:
        button = "Movement Accessories";
        BMDealerNPC.ShopName = "Movement Accessories";
        break;
      case 4:
        button = "Combat Accessories";
        BMDealerNPC.ShopName = "Combat Accessories";
        break;
      case 5:
        button = "Informative/Building Gear";
        BMDealerNPC.ShopName = "Informative/Building Gear";
        break;
      case 6:
        button = "Treasure Bags";
        BMDealerNPC.ShopName = "Treasure Bags";
        break;
      case 7:
        button = "Crates & Grab Bags";
        BMDealerNPC.ShopName = "Crates & Grab Bags";
        break;
      case 8:
        button = "Ores & Bars";
        BMDealerNPC.ShopName = "Ores & Bars";
        break;
      case 9:
        button = "Natural Blocks";
        BMDealerNPC.ShopName = "Natural Blocks";
        break;
      case 10:
        button = "Building Blocks";
        BMDealerNPC.ShopName = "Building Blocks";
        break;
      case 11:
        button = "Herbs & Plants";
        BMDealerNPC.ShopName = "Herbs & Plants";
        break;
      case 12:
        button = "Fish & Fishing Gear";
        BMDealerNPC.ShopName = "Fish & Fishing Gear";
        break;
      case 13:
        button = "Mounts & Hooks";
        BMDealerNPC.ShopName = "Mounts & Hooks";
        break;
      case 14:
        button = "Ammo";
        BMDealerNPC.ShopName = "Ammo";
        break;
    }
    button2 = "Shop Changer";
  }

  public virtual void OnChatButtonClicked(bool firstButton, ref string shop)
  {
    if (firstButton)
    {
      shop = BMDealerNPC.ShopName;
      BlackMarketDealerNPCUI.visible = false;
    }
    else
    {
      if (!BlackMarketDealerNPCUI.visible)
        BlackMarketDealerNPCUI.timeStart = Main.GameUpdateCount;
      BlackMarketDealerNPCUI.visible = true;
    }
  }

  public virtual void AddShops()
  {
    NPCShop npcShop1 = new NPCShop(this.Type, "Potions");
    Item obj1 = new Item(2344, 1, 0);
    obj1.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray1 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop2 = npcShop1.Add(obj1, conditionArray1);
    Item obj2 = new Item(353, 1, 0);
    obj2.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray2 = Array.Empty<Condition>();
    NPCShop npcShop3 = npcShop2.Add(obj2, conditionArray2);
    Item obj3 = new Item(303, 1, 0);
    obj3.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray3 = new Condition[1]
    {
      ModConditions.HasBeenToPurity
    };
    NPCShop npcShop4 = npcShop3.Add(obj3, conditionArray3);
    Item obj4 = new Item(300, 1, 0);
    obj4.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray4 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop5 = npcShop4.Add(obj4, conditionArray4);
    Item obj5 = new Item(5211, 1, 0);
    obj5.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray5 = new Condition[2]
    {
      Condition.DownedEyeOfCthulhu,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop6 = npcShop5.Add(obj5, conditionArray5);
    Item obj6 = new Item(2325, 1, 0);
    obj6.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray6 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop7 = npcShop6.Add(obj6, conditionArray6);
    Item obj7 = new Item(2324, 1, 0);
    obj7.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray7 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop8 = npcShop7.Add(obj7, conditionArray7);
    Item obj8 = new Item(2356, 1, 0);
    obj8.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray8 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop9 = npcShop8.Add(obj8, conditionArray8);
    Item obj9 = new Item(2329, 1, 0);
    obj9.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray9 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop10 = npcShop9.Add(obj9, conditionArray9);
    Item obj10 = new Item(2346, 1, 0);
    obj10.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray10 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop11 = npcShop10.Add(obj10, conditionArray10);
    Item obj11 = new Item(295, 1, 0);
    obj11.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray11 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop12 = npcShop11.Add(obj11, conditionArray11);
    Item obj12 = new Item(2354, 1, 0);
    obj12.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray12 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop13 = npcShop12.Add(obj12, conditionArray12);
    Item obj13 = new Item(2327, 1, 0);
    obj13.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray13 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop14 = npcShop13.Add(obj13, conditionArray13);
    Item obj14 = new Item(291, 1, 0);
    obj14.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray14 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop15 = npcShop14.Add(obj14, conditionArray14);
    Item obj15 = new Item(305, 1, 0);
    obj15.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray15 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop16 = npcShop15.Add(obj15, conditionArray15);
    Item obj16 = new Item(4479, 1, 0);
    obj16.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray16 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop17 = npcShop16.Add(obj16, conditionArray16);
    Item obj17 = new Item(2323, 1, 0);
    obj17.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray17 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop18 = npcShop17.Add(obj17, conditionArray17);
    Item obj18 = new Item(304, 1, 0);
    obj18.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray18 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop19 = npcShop18.Add(obj18, conditionArray18);
    Item obj19 = new Item(2348, 1, 0);
    obj19.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray19 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop20 = npcShop19.Add(obj19, conditionArray19);
    Item obj20 = new Item(297, 1, 0);
    obj20.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray20 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop21 = npcShop20.Add(obj20, conditionArray20);
    Item obj21 = new Item(292, 1, 0);
    obj21.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray21 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop22 = npcShop21.Add(obj21, conditionArray21);
    Item obj22 = new Item(2345, 1, 0);
    obj22.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray22 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop23 = npcShop22.Add(obj22, conditionArray22);
    Item obj23 = new Item(294, 1, 0);
    obj23.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray23 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop24 = npcShop23.Add(obj23, conditionArray23);
    Item obj24 = new Item(293, 1, 0);
    obj24.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray24 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop25 = npcShop24.Add(obj24, conditionArray24);
    Item obj25 = new Item(2322, 1, 0);
    obj25.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray25 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop26 = npcShop25.Add(obj25, conditionArray25);
    Item obj26 = new Item(299, 1, 0);
    obj26.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray26 = new Condition[1]
    {
      ModConditions.HasBeenToPurity
    };
    NPCShop npcShop27 = npcShop26.Add(obj26, conditionArray26);
    Item obj27 = new Item(288, 1, 0);
    obj27.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray27 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop28 = npcShop27.Add(obj27, conditionArray27);
    Item obj28 = new Item(2347, 1, 0);
    obj28.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray28 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop29 = npcShop28.Add(obj28, conditionArray28);
    Item obj29 = new Item(678, 1, 0);
    obj29.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray29 = new Condition[1]
    {
      Condition.ForTheWorthyWorld
    };
    NPCShop npcShop30 = npcShop29.Add(obj29, conditionArray29);
    Item obj30 = new Item(289, 1, 0);
    obj30.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray30 = new Condition[1]
    {
      ModConditions.HasBeenToPurity
    };
    NPCShop npcShop31 = npcShop30.Add(obj30, conditionArray30);
    Item obj31 = new Item(2266, 1, 0);
    obj31.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray31 = Array.Empty<Condition>();
    NPCShop npcShop32 = npcShop31.Add(obj31, conditionArray31);
    Item obj32 = new Item(298, 1, 0);
    obj32.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray32 = new Condition[1]
    {
      ModConditions.HasBeenToMushroom
    };
    NPCShop npcShop33 = npcShop32.Add(obj32, conditionArray32);
    Item obj33 = new Item(2355, 1, 0);
    obj33.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray33 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop34 = npcShop33.Add(obj33, conditionArray33);
    Item obj34 = new Item(296, 1, 0);
    obj34.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray34 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop35 = npcShop34.Add(obj34, conditionArray34);
    Item obj35 = new Item(2328, 1, 0);
    obj35.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray35 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop36 = npcShop35.Add(obj35, conditionArray35);
    Item obj36 = new Item(290, 1, 0);
    obj36.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray36 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop37 = npcShop36.Add(obj36, conditionArray36);
    Item obj37 = new Item(301, 1, 0);
    obj37.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray37 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop38 = npcShop37.Add(obj37, conditionArray37);
    Item obj38 = new Item(2326, 1, 0);
    obj38.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray38 = new Condition[1]
    {
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop39 = npcShop38.Add(obj38, conditionArray38);
    Item obj39 = new Item(2359, 1, 0);
    obj39.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray39 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop40 = npcShop39.Add(obj39, conditionArray39);
    Item obj40 = new Item(302, 1, 0);
    obj40.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray40 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop41 = npcShop40.Add(obj40, conditionArray40);
    Item obj41 = new Item(2349, 1, 0);
    obj41.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray41 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    ((AbstractNPCShop) npcShop41.Add(obj41, conditionArray41)).Register();
    NPCShop npcShop42 = new NPCShop(this.Type, "Flasks, Stations & Foods");
    Item obj42 = new Item(4624, 1, 0);
    obj42.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray42 = Array.Empty<Condition>();
    NPCShop npcShop43 = npcShop42.Add(obj42, conditionArray42);
    Item obj43 = new Item(4403, 1, 0);
    obj43.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray43 = Array.Empty<Condition>();
    NPCShop npcShop44 = npcShop43.Add(obj43, conditionArray43);
    Item obj44 = new Item(4022, 1, 0);
    obj44.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray44 = Array.Empty<Condition>();
    NPCShop npcShop45 = npcShop44.Add(obj44, conditionArray44);
    Item obj45 = new Item(1353, 1, 0);
    obj45.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray45 = new Condition[3]
    {
      Condition.Hardmode,
      Condition.DownedQueenBee,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop46 = npcShop45.Add(obj45, conditionArray45);
    Item obj46 = new Item(1354, 1, 0);
    obj46.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray46 = new Condition[2]
    {
      Condition.DownedQueenBee,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop47 = npcShop46.Add(obj46, conditionArray46);
    Item obj47 = new Item(1355, 1, 0);
    obj47.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray47 = new Condition[2]
    {
      Condition.Hardmode,
      Condition.DownedQueenBee
    };
    NPCShop npcShop48 = npcShop47.Add(obj47, conditionArray47);
    Item obj48 = new Item(1356, 1, 0);
    obj48.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray48 = new Condition[3]
    {
      Condition.Hardmode,
      Condition.DownedQueenBee,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop49 = npcShop48.Add(obj48, conditionArray48);
    Item obj49 = new Item(1357, 1, 0);
    obj49.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray49 = new Condition[2]
    {
      Condition.DownedPlantera,
      Condition.DownedQueenBee
    };
    NPCShop npcShop50 = npcShop49.Add(obj49, conditionArray49);
    Item obj50 = new Item(1358, 1, 0);
    obj50.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray50 = new Condition[1]
    {
      Condition.DownedQueenBee
    };
    NPCShop npcShop51 = npcShop50.Add(obj50, conditionArray50);
    Item obj51 = new Item(1359, 1, 0);
    obj51.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray51 = new Condition[2]
    {
      Condition.DownedQueenBee,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop52 = npcShop51.Add(obj51, conditionArray51);
    Item obj52 = new Item(1340, 1, 0);
    obj52.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray52 = new Condition[3]
    {
      Condition.DownedPlantera,
      Condition.DownedQueenBee,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop53 = npcShop52.Add(obj52, conditionArray52);
    Item obj53 = new Item(28, 1, 0);
    obj53.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray53 = Array.Empty<Condition>();
    NPCShop npcShop54 = npcShop53.Add(obj53, conditionArray53);
    Item obj54 = new Item(188, 1, 0);
    obj54.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray54 = new Condition[1]
    {
      ModConditions.HasBeenToMushroom
    };
    NPCShop npcShop55 = npcShop54.Add(obj54, conditionArray54);
    Item obj55 = new Item(499, 1, 0);
    obj55.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray55 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop56 = npcShop55.Add(obj55, conditionArray55);
    Item obj56 = new Item(3544, 1, 0);
    obj56.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray56 = new Condition[1]
    {
      ModConditions.DownedLunarEvent
    };
    NPCShop npcShop57 = npcShop56.Add(obj56, conditionArray56);
    Item obj57 = new Item(110, 1, 0);
    obj57.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray57 = Array.Empty<Condition>();
    NPCShop npcShop58 = npcShop57.Add(obj57, conditionArray57);
    Item obj58 = new Item(189, 1, 0);
    obj58.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray58 = new Condition[1]
    {
      ModConditions.HasBeenToMushroom
    };
    NPCShop npcShop59 = npcShop58.Add(obj58, conditionArray58);
    Item obj59 = new Item(500, 1, 0);
    obj59.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray59 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop60 = npcShop59.Add(obj59, conditionArray59);
    Item obj60 = new Item(2209, 1, 0);
    obj60.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray60 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop61 = npcShop60.Add(obj60, conditionArray60);
    Item obj61 = new Item(227, 1, 0);
    obj61.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray61 = new Condition[1]
    {
      ModConditions.HasBeenToMushroom
    };
    NPCShop npcShop62 = npcShop61.Add(obj61, conditionArray61);
    Item obj62 = new Item(126, 1, 0);
    obj62.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray62 = Array.Empty<Condition>();
    NPCShop npcShop63 = npcShop62.Add(obj62, conditionArray62);
    Item obj63 = new Item(1134, 1, 0);
    obj63.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray63 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop64 = npcShop63.Add(obj63, conditionArray63);
    Item obj64 = new Item(3001, 1, 0);
    obj64.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray64 = new Condition[1]
    {
      ModConditions.HasTalkedToSkeletonMerchant
    };
    NPCShop npcShop65 = npcShop64.Add(obj64, conditionArray64);
    Item obj65 = new Item(2756, 1, 0);
    obj65.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray65 = Array.Empty<Condition>();
    NPCShop npcShop66 = npcShop65.Add(obj65, conditionArray65);
    Item obj66 = new Item(4870, 1, 0);
    obj66.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray66 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop67 = npcShop66.Add(obj66, conditionArray66);
    Item obj67 = new Item(2350, 1, 0);
    obj67.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray67 = Array.Empty<Condition>();
    NPCShop npcShop68 = npcShop67.Add(obj67, conditionArray67);
    Item obj68 = new Item(2351, 1, 0);
    obj68.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray68 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop69 = npcShop68.Add(obj68, conditionArray68);
    Item obj69 = new Item(2997, 1, 0);
    obj69.shopCustomPrice = new int?(Item.buyPrice(0, 0, 75, 0));
    Condition[] conditionArray69 = Array.Empty<Condition>();
    NPCShop npcShop70 = npcShop69.Add(obj69, conditionArray69);
    Item obj70 = new Item(63 /*0x3F*/, 1, 0);
    obj70.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray70 = Array.Empty<Condition>();
    NPCShop npcShop71 = npcShop70.Add(obj70, conditionArray70);
    Item obj71 = new Item(966, 1, 0);
    obj71.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray71 = Array.Empty<Condition>();
    NPCShop npcShop72 = npcShop71.Add(obj71, conditionArray71);
    Item obj72 = new Item(487, 1, 0);
    obj72.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray72 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop73 = npcShop72.Add(obj72, conditionArray72);
    Item obj73 = new Item(2177, 1, 0);
    obj73.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray73 = Array.Empty<Condition>();
    NPCShop npcShop74 = npcShop73.Add(obj73, conditionArray73);
    Item obj74 = new Item(3198, 1, 0);
    obj74.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray74 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop75 = npcShop74.Add(obj74, conditionArray74);
    Item obj75 = new Item(2999, 1, 0);
    obj75.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray75 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop76 = npcShop75.Add(obj75, conditionArray75);
    Item obj76 = new Item(3814, 1, 0);
    obj76.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray76 = new Condition[1]
    {
      Condition.DownedOldOnesArmyAny
    };
    NPCShop npcShop77 = npcShop76.Add(obj76, conditionArray76);
    Item obj77 = new Item(4276, 1, 0);
    obj77.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray77 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop78 = npcShop77.Add(obj77, conditionArray77);
    Item obj78 = new Item(3750, 1, 0);
    obj78.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray78 = Array.Empty<Condition>();
    NPCShop npcShop79 = npcShop78.Add(obj78, conditionArray78);
    Item obj79 = new Item(1431, 1, 0);
    obj79.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray79 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop80 = npcShop79.Add(obj79, conditionArray79);
    Item obj80 = new Item(1859, 1, 0);
    obj80.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray80 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop81 = npcShop80.Add(obj80, conditionArray80);
    Item obj81 = new Item(4609, 1, 0);
    obj81.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray81 = Array.Empty<Condition>();
    NPCShop npcShop82 = npcShop81.Add(obj81, conditionArray81);
    Item obj82 = new Item(3117, 1, 0);
    obj82.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray82 = Array.Empty<Condition>();
    NPCShop npcShop83 = npcShop82.Add(obj82, conditionArray82);
    Item obj83 = new Item(148, 1, 0);
    obj83.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray83 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop84 = npcShop83.Add(obj83, conditionArray83);
    Item obj84 = new Item(5322, 1, 0);
    obj84.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray84 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop85 = npcShop84.Add(obj84, conditionArray84);
    Item obj85 = new Item(29, 1, 0);
    obj85.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray85 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop86 = npcShop85.Add(obj85, conditionArray85);
    Item obj86 = new Item(1291, 1, 0);
    obj86.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray86 = new Condition[2]
    {
      Condition.DownedMechBossAny,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop87 = npcShop86.Add(obj86, conditionArray86);
    Item obj87 = new Item(109, 1, 0);
    obj87.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray87 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop88 = npcShop87.Add(obj87, conditionArray87);
    Item obj88 = new Item(5337, 1, 0);
    obj88.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray88 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop89 = npcShop88.Add(obj88, conditionArray88);
    Item obj89 = new Item(5339, 1, 0);
    obj89.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray89 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop90 = npcShop89.Add(obj89, conditionArray89);
    Item obj90 = new Item(5338, 1, 0);
    obj90.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray90 = new Condition[2]
    {
      Condition.DownedMechBossAny,
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop91 = npcShop90.Add(obj90, conditionArray90);
    Item obj91 = new Item(5342, 1, 0);
    obj91.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray91 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop92 = npcShop91.Add(obj91, conditionArray91);
    Item obj92 = new Item(5341, 1, 0);
    obj92.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray92 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop93 = npcShop92.Add(obj92, conditionArray92);
    Item obj93 = new Item(5340, 1, 0);
    obj93.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray93 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop94 = npcShop93.Add(obj93, conditionArray93);
    Item obj94 = new Item(5343, 1, 0);
    obj94.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray94 = new Condition[1]
    {
      ModConditions.HasTalkedToTravelingMerchant
    };
    NPCShop npcShop95 = npcShop94.Add(obj94, conditionArray94);
    Item obj95 = new Item(5326, 1, 0);
    obj95.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray95 = new Condition[1]
    {
      ModConditions.HasTalkedToSkeletonMerchant
    };
    NPCShop npcShop96 = npcShop95.Add(obj95, conditionArray95);
    Item obj96 = new Item(4382, 1, 0);
    obj96.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray96 = new Condition[1]
    {
      ModConditions.DownedBloodMoon
    };
    NPCShop npcShop97 = npcShop96.Add(obj96, conditionArray96);
    Item obj97 = new Item(5336, 1, 0);
    obj97.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray97 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToAether
    };
    ((AbstractNPCShop) npcShop97.Add(obj97, conditionArray97)).Register();
    NPCShop npcShop98 = new NPCShop(this.Type, "Materials");
    Item obj98 = new Item(3794, 1, 0);
    obj98.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray98 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop99 = npcShop98.Add(obj98, conditionArray98);
    Item obj99 = new Item(323, 1, 0);
    obj99.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray99 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop100 = npcShop99.Add(obj99, conditionArray99);
    Item obj100 = new Item(2218, 1, 0);
    obj100.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray100 = new Condition[2]
    {
      Condition.DownedGolem,
      ModConditions.HasBeenToTemple
    };
    NPCShop npcShop101 = npcShop100.Add(obj100, conditionArray100);
    Item obj101 = new Item(2431, 1, 0);
    obj101.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray101 = new Condition[2]
    {
      Condition.DownedQueenBee,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop102 = npcShop101.Add(obj101, conditionArray101);
    Item obj102 = new Item(1811, 1, 0);
    obj102.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray102 = new Condition[1]
    {
      Condition.DownedPumpking
    };
    NPCShop npcShop103 = npcShop102.Add(obj102, conditionArray102);
    Item obj103 = new Item(1119, 1, 0);
    obj103.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray103 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop104 = npcShop103.Add(obj103, conditionArray103);
    Item obj104 = new Item(236, 1, 0);
    obj104.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray104 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop105 = npcShop104.Add(obj104, conditionArray104);
    Item obj105 = new Item(4413, 1, 0);
    obj105.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray105 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop106 = npcShop105.Add(obj105, conditionArray105);
    Item obj106 = new Item(254, 1, 0);
    obj106.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray106 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop107 = npcShop106.Add(obj106, conditionArray106);
    Item obj107 = new Item(154, 1, 0);
    obj107.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray107 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop108 = npcShop107.Add(obj107, conditionArray107);
    Item obj108 = new Item(1517, 1, 0);
    obj108.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray108 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop109 = npcShop108.Add(obj108, conditionArray108);
    Item obj109 = new Item(1520, 1, 0);
    obj109.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray109 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.DownedEclipse
    };
    NPCShop npcShop110 = npcShop109.Add(obj109, conditionArray109);
    Item obj110 = new Item(1570, 1, 0);
    obj110.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray110 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.DownedEclipse
    };
    NPCShop npcShop111 = npcShop110.Add(obj110, conditionArray110);
    Item obj111 = new Item(1611, 1, 0);
    obj111.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray111 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop112 = npcShop111.Add(obj111, conditionArray111);
    Item obj112 = new Item(522, 1, 0);
    obj112.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray112 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop113 = npcShop112.Add(obj112, conditionArray112);
    Item obj113 = new Item(1116, 1, 0);
    obj113.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray113 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop114 = npcShop113.Add(obj113, conditionArray113);
    Item obj114 = new Item(527, 1, 0);
    obj114.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray114 = new Condition[3]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop115 = npcShop114.Add(obj114, conditionArray114);
    Item obj115 = new Item(1508, 1, 0);
    obj115.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray115 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop116 = npcShop115.Add(obj115, conditionArray115);
    Item obj116 = new Item(1347, 1, 0);
    obj116.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray116 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop117 = npcShop116.Add(obj116, conditionArray116);
    Item obj117 = new Item(75, 1, 0);
    obj117.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray117 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop118 = npcShop117.Add(obj117, conditionArray117);
    Item obj118 = new Item(320, 1, 0);
    obj118.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray118 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop119 = npcShop118.Add(obj118, conditionArray118);
    Item obj119 = new Item(1518, 1, 0);
    obj119.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray119 = new Condition[2]
    {
      Condition.DownedMechBossAny,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop120 = npcShop119.Add(obj119, conditionArray119);
    Item obj120 = new Item(5070, 1, 0);
    obj120.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray120 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop121 = npcShop120.Add(obj120, conditionArray120);
    Item obj121 = new Item(3783, 1, 0);
    obj121.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray121 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop122 = npcShop121.Add(obj121, conditionArray121);
    Item obj122 = new Item(2161, 1, 0);
    obj122.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray122 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop123 = npcShop122.Add(obj122, conditionArray122);
    Item obj123 = new Item(23, 1, 0);
    obj123.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray123 = Array.Empty<Condition>();
    NPCShop npcShop124 = npcShop123.Add(obj123, conditionArray123);
    Item obj124 = new Item(1516, 1, 0);
    obj124.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray124 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop125 = npcShop124.Add(obj124, conditionArray124);
    Item obj125 = new Item(1348, 1, 0);
    obj125.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray125 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop126 = npcShop125.Add(obj125, conditionArray125);
    Item obj126 = new Item((int) byte.MaxValue, 1, 0);
    obj126.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray126 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop127 = npcShop126.Add(obj126, conditionArray126);
    Item obj127 = new Item(118, 1, 0);
    obj127.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray127 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop128 = npcShop127.Add(obj127, conditionArray127);
    Item obj128 = new Item(1519, 1, 0);
    obj128.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray128 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop129 = npcShop128.Add(obj128, conditionArray128);
    Item obj129 = new Item(1332, 1, 0);
    obj129.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray129 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop130 = npcShop129.Add(obj129, conditionArray129);
    Item obj130 = new Item(324, 1, 0);
    obj130.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray130 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop131 = npcShop130.Add(obj130, conditionArray130);
    Item obj131 = new Item(331, 1, 0);
    obj131.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray131 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop132 = npcShop131.Add(obj131, conditionArray131);
    Item obj132 = new Item(259, 1, 0);
    obj132.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray132 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop133 = npcShop132.Add(obj132, conditionArray132);
    Item obj133 = new Item(38, 1, 0);
    obj133.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray133 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop134 = npcShop133.Add(obj133, conditionArray133);
    Item obj134 = new Item(528, 1, 0);
    obj134.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray134 = new Condition[3]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop135 = npcShop134.Add(obj134, conditionArray134);
    Item obj135 = new Item(3356, 1, 0);
    obj135.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray135 = new Condition[2]
    {
      Condition.DownedSkeletronPrime,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop136 = npcShop135.Add(obj135, conditionArray135);
    Item obj136 = new Item(3355, 1, 0);
    obj136.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray136 = new Condition[2]
    {
      Condition.DownedDestroyer,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop137 = npcShop136.Add(obj136, conditionArray136);
    Item obj137 = new Item(3354, 1, 0);
    obj137.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray137 = new Condition[2]
    {
      Condition.DownedTwins,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop138 = npcShop137.Add(obj137, conditionArray137);
    Item obj138 = new Item(1346, 1, 0);
    obj138.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray138 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop139 = npcShop138.Add(obj138, conditionArray138);
    Item obj139 = new Item(3457, 1, 0);
    obj139.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray139 = new Condition[1]
    {
      Condition.DownedNebulaPillar
    };
    NPCShop npcShop140 = npcShop139.Add(obj139, conditionArray139);
    Item obj140 = new Item(3111, 1, 0);
    obj140.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray140 = Array.Empty<Condition>();
    NPCShop npcShop141 = npcShop140.Add(obj140, conditionArray140);
    Item obj141 = new Item(4414, 1, 0);
    obj141.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray141 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop142 = npcShop141.Add(obj141, conditionArray141);
    Item obj142 = new Item(981, 1, 0);
    obj142.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray142 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop143 = npcShop142.Add(obj142, conditionArray142);
    Item obj143 = new Item(501, 1, 0);
    obj143.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray143 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop144 = npcShop143.Add(obj143, conditionArray143);
    Item obj144 = new Item(1118, 1, 0);
    obj144.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray144 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop145 = npcShop144.Add(obj144, conditionArray144);
    Item obj145 = new Item(1115, 1, 0);
    obj145.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray145 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop146 = npcShop145.Add(obj145, conditionArray145);
    Item obj146 = new Item(68, 1, 0);
    obj146.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray146 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop147 = npcShop146.Add(obj146, conditionArray146);
    Item obj147 = new Item(86, 1, 0);
    obj147.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray147 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop148 = npcShop147.Add(obj147, conditionArray147);
    Item obj148 = new Item(319, 1, 0);
    obj148.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray148 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop149 = npcShop148.Add(obj148, conditionArray148);
    Item obj149 = new Item(225, 1, 0);
    obj149.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray149 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop150 = npcShop149.Add(obj149, conditionArray149);
    Item obj150 = new Item(2766, 1, 0);
    obj150.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray150 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToTemple
    };
    NPCShop npcShop151 = npcShop150.Add(obj150, conditionArray150);
    Item obj151 = new Item(3458, 1, 0);
    obj151.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray151 = new Condition[1]
    {
      Condition.DownedSolarPillar
    };
    NPCShop npcShop152 = npcShop151.Add(obj151, conditionArray151);
    Item obj152 = new Item(575, 1, 0);
    obj152.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray152 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop153 = npcShop152.Add(obj152, conditionArray152);
    Item obj153 = new Item(520, 1, 0);
    obj153.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray153 = new Condition[3]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop154 = npcShop153.Add(obj153, conditionArray153);
    Item obj154 = new Item(521, 1, 0);
    obj154.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray154 = new Condition[3]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop155 = npcShop154.Add(obj154, conditionArray154);
    Item obj155 = new Item(548, 1, 0);
    obj155.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray155 = new Condition[1]
    {
      Condition.DownedDestroyer
    };
    NPCShop npcShop156 = npcShop155.Add(obj155, conditionArray155);
    Item obj156 = new Item(549, 1, 0);
    obj156.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray156 = new Condition[1]
    {
      Condition.DownedTwins
    };
    NPCShop npcShop157 = npcShop156.Add(obj156, conditionArray156);
    Item obj157 = new Item(547, 1, 0);
    obj157.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray157 = new Condition[1]
    {
      Condition.DownedSkeletronPrime
    };
    NPCShop npcShop158 = npcShop157.Add(obj157, conditionArray157);
    Item obj158 = new Item(2607, 1, 0);
    obj158.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray158 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop159 = npcShop158.Add(obj158, conditionArray158);
    Item obj159 = new Item(1831, 1, 0);
    obj159.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray159 = new Condition[1]
    {
      Condition.DownedMourningWood
    };
    NPCShop npcShop160 = npcShop159.Add(obj159, conditionArray159);
    Item obj160 = new Item(3459, 1, 0);
    obj160.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray160 = new Condition[1]
    {
      Condition.DownedStardustPillar
    };
    NPCShop npcShop161 = npcShop160.Add(obj160, conditionArray160);
    Item obj161 = new Item(209, 1, 0);
    obj161.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray161 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop162 = npcShop161.Add(obj161, conditionArray161);
    Item obj162 = new Item(1521, 1, 0);
    obj162.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray162 = new Condition[2]
    {
      Condition.DownedMechBossAny,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop163 = npcShop162.Add(obj162, conditionArray162);
    Item obj163 = new Item(362, 1, 0);
    obj163.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray163 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop164 = npcShop163.Add(obj163, conditionArray163);
    Item obj164 = new Item(1329, 1, 0);
    obj164.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray164 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop165 = npcShop164.Add(obj164, conditionArray164);
    Item obj165 = new Item(1328, 1, 0);
    obj165.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray165 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop166 = npcShop165.Add(obj165, conditionArray165);
    Item obj166 = new Item(526, 1, 0);
    obj166.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray166 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop167 = npcShop166.Add(obj166, conditionArray166);
    Item obj167 = new Item(1330, 1, 0);
    obj167.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray167 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop168 = npcShop167.Add(obj167, conditionArray167);
    Item obj168 = new Item(1339, 1, 0);
    obj168.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray168 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop169 = npcShop168.Add(obj168, conditionArray168);
    Item obj169 = new Item(210, 1, 0);
    obj169.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray169 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop170 = npcShop169.Add(obj169, conditionArray169);
    Item obj170 = new Item(1117, 1, 0);
    obj170.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray170 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop171 = npcShop170.Add(obj170, conditionArray170);
    Item obj171 = new Item(3456, 1, 0);
    obj171.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray171 = new Condition[1]
    {
      Condition.DownedVortexPillar
    };
    NPCShop npcShop172 = npcShop171.Add(obj171, conditionArray171);
    Item obj172 = new Item(4412, 1, 0);
    obj172.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray172 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop173 = npcShop172.Add(obj172, conditionArray172);
    Item obj173 = new Item(215, 1, 0);
    obj173.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray173 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop174 = npcShop173.Add(obj173, conditionArray173);
    Item obj174 = new Item(69, 1, 0);
    obj174.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray174 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    ((AbstractNPCShop) npcShop174.Add(obj174, conditionArray174)).Register();
    NPCShop npcShop175 = new NPCShop(this.Type, "Movement Accessories");
    Item obj175 = new Item(285, 1, 0);
    obj175.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray175 = Array.Empty<Condition>();
    NPCShop npcShop176 = npcShop175.Add(obj175, conditionArray175);
    Item obj176 = new Item(212, 1, 0);
    obj176.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray176 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop177 = npcShop176.Add(obj176, conditionArray176);
    Item obj177 = new Item(3225, 1, 0);
    obj177.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray177 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop178 = npcShop177.Add(obj177, conditionArray177);
    Item obj178 = new Item(987, 1, 0);
    obj178.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray178 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop179 = npcShop178.Add(obj178, conditionArray178);
    Item obj179 = new Item(953, 1, 0);
    obj179.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray179 = Array.Empty<Condition>();
    NPCShop npcShop180 = npcShop179.Add(obj179, conditionArray179);
    Item obj180 = new Item(53, 1, 0);
    obj180.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray180 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop181 = npcShop180.Add(obj180, conditionArray180);
    Item obj181 = new Item(268, 1, 0);
    obj181.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray181 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop182 = npcShop181.Add(obj181, conditionArray181);
    Item obj182 = new Item(4055, 1, 0);
    obj182.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray182 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop183 = npcShop182.Add(obj182, conditionArray182);
    Item obj183 = new Item(187, 1, 0);
    obj183.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray183 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop184 = npcShop183.Add(obj183, conditionArray183);
    Item obj184 = new Item(4978, 1, 0);
    obj184.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray184 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop185 = npcShop184.Add(obj184, conditionArray184);
    Item obj185 = new Item(3017, 1, 0);
    obj185.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray185 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop186 = npcShop185.Add(obj185, conditionArray185);
    Item obj186 = new Item(934, 1, 0);
    obj186.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray186 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop187 = npcShop186.Add(obj186, conditionArray186);
    Item obj187 = new Item(2423, 1, 0);
    obj187.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray187 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop188 = npcShop187.Add(obj187, conditionArray187);
    Item obj188 = new Item(54, 1, 0);
    obj188.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray188 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop189 = npcShop188.Add(obj188, conditionArray188);
    Item obj189 = new Item(950, 1, 0);
    obj189.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray189 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop190 = npcShop189.Add(obj189, conditionArray189);
    Item obj190 = new Item(4404, 1, 0);
    obj190.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray190 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop191 = npcShop190.Add(obj190, conditionArray190);
    Item obj191 = new Item(1303, 1, 0);
    obj191.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray191 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop192 = npcShop191.Add(obj191, conditionArray191);
    Item obj192 = new Item(906, 1, 0);
    obj192.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray192 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop193 = npcShop192.Add(obj192, conditionArray192);
    Item obj193 = new Item(158, 1, 0);
    obj193.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray193 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop194 = npcShop193.Add(obj193, conditionArray193);
    Item obj194 = new Item(5107, 1, 0);
    obj194.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray194 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop195 = npcShop194.Add(obj194, conditionArray194);
    Item obj195 = new Item(485, 1, 0);
    obj195.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray195 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.DownedBloodMoon
    };
    NPCShop npcShop196 = npcShop195.Add(obj195, conditionArray195);
    Item obj196 = new Item(497, 1, 0);
    obj196.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray196 = new Condition[2]
    {
      Condition.DownedMechBossAny,
      ModConditions.DownedEclipse
    };
    NPCShop npcShop197 = npcShop196.Add(obj196, conditionArray196);
    Item obj197 = new Item(128 /*0x80*/, 1, 0);
    obj197.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray197 = new Condition[1]
    {
      Condition.DownedGoblinArmy
    };
    NPCShop npcShop198 = npcShop197.Add(obj197, conditionArray197);
    Item obj198 = new Item(3200, 1, 0);
    obj198.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray198 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop199 = npcShop198.Add(obj198, conditionArray198);
    Item obj199 = new Item(857, 1, 0);
    obj199.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray199 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop200 = npcShop199.Add(obj199, conditionArray199);
    Item obj200 = new Item(159, 1, 0);
    obj200.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray200 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop201 = npcShop200.Add(obj200, conditionArray200);
    Item obj201 = new Item(975, 1, 0);
    obj201.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray201 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop202 = npcShop201.Add(obj201, conditionArray201);
    Item obj202 = new Item(977, 1, 0);
    obj202.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray202 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop203 = npcShop202.Add(obj202, conditionArray202);
    Item obj203 = new Item(3201, 1, 0);
    obj203.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray203 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop204 = npcShop203.Add(obj203, conditionArray203);
    Item obj204 = new Item(863, 1, 0);
    obj204.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray204 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    ((AbstractNPCShop) npcShop204.Add(obj204, conditionArray204)).Register();
    NPCShop npcShop205 = new NPCShop(this.Type, "Combat Accessories");
    Item obj205 = new Item(885, 1, 0);
    obj205.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray205 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop206 = npcShop205.Add(obj205, conditionArray205);
    Item obj206 = new Item(3809, 1, 0);
    obj206.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray206 = new Condition[1]
    {
      Condition.DownedOldOnesArmyAny
    };
    NPCShop npcShop207 = npcShop206.Add(obj206, conditionArray206);
    Item obj207 = new Item(886, 1, 0);
    obj207.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray207 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop208 = npcShop207.Add(obj207, conditionArray207);
    Item obj208 = new Item(935, 1, 0);
    obj208.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray208 = new Condition[1]
    {
      Condition.DownedMechBossAll
    };
    NPCShop npcShop209 = npcShop208.Add(obj208, conditionArray208);
    Item obj209 = new Item(49, 1, 0);
    obj209.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray209 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop210 = npcShop209.Add(obj209, conditionArray209);
    Item obj210 = new Item(111, 1, 0);
    obj210.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray210 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop211 = npcShop210.Add(obj210, conditionArray210);
    Item obj211 = new Item(887, 1, 0);
    obj211.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray211 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop212 = npcShop211.Add(obj211, conditionArray211);
    Item obj212 = new Item(963, 1, 0);
    obj212.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray212 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop213 = npcShop212.Add(obj212, conditionArray212);
    Item obj213 = new Item(3309, 1, 0);
    obj213.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray213 = Array.Empty<Condition>();
    NPCShop npcShop214 = npcShop213.Add(obj213, conditionArray213);
    Item obj214 = new Item(888, 1, 0);
    obj214.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray214 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop215 = npcShop214.Add(obj214, conditionArray214);
    Item obj215 = new Item(2219, 1, 0);
    obj215.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray215 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop216 = npcShop215.Add(obj215, conditionArray215);
    Item obj216 = new Item(156, 1, 0);
    obj216.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray216 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop217 = npcShop216.Add(obj216, conditionArray216);
    Item obj217 = new Item(554, 1, 0);
    obj217.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray217 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop218 = npcShop217.Add(obj217, conditionArray217);
    Item obj218 = new Item(1301, 1, 0);
    obj218.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray218 = new Condition[1]
    {
      Condition.DownedGolem
    };
    NPCShop npcShop219 = npcShop218.Add(obj218, conditionArray218);
    Item obj219 = new Item(1248, 1, 0);
    obj219.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray219 = new Condition[2]
    {
      Condition.DownedGolem,
      ModConditions.HasBeenToTemple
    };
    NPCShop npcShop220 = npcShop219.Add(obj219, conditionArray219);
    Item obj220 = new Item(889, 1, 0);
    obj220.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray220 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop221 = npcShop220.Add(obj220, conditionArray220);
    Item obj221 = new Item(211, 1, 0);
    obj221.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray221 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop222 = npcShop221.Add(obj221, conditionArray221);
    Item obj222 = new Item(3016, 1, 0);
    obj222.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray222 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop223 = npcShop222.Add(obj222, conditionArray222);
    Item obj223 = new Item(1253, 1, 0);
    obj223.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray223 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop224 = npcShop223.Add(obj223, conditionArray223);
    Item obj224 = new Item(1921, 1, 0);
    obj224.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray224 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop225 = npcShop224.Add(obj224, conditionArray224);
    Item obj225 = new Item(1167, 1, 0);
    obj225.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray225 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop226 = npcShop225.Add(obj225, conditionArray225);
    Item obj226 = new Item(1132, 1, 0);
    obj226.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray226 = new Condition[2]
    {
      Condition.DownedQueenBee,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop227 = npcShop226.Add(obj226, conditionArray226);
    Item obj227 = new Item(3811, 1, 0);
    obj227.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray227 = new Condition[1]
    {
      Condition.DownedOldOnesArmyT2
    };
    NPCShop npcShop228 = npcShop227.Add(obj227, conditionArray227);
    Item obj228 = new Item(1321, 1, 0);
    obj228.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray228 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop229 = npcShop228.Add(obj228, conditionArray228);
    Item obj229 = new Item(1322, 1, 0);
    obj229.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray229 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop230 = npcShop229.Add(obj229, conditionArray229);
    Item obj230 = new Item(890, 1, 0);
    obj230.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray230 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop231 = npcShop230.Add(obj230, conditionArray230);
    Item obj231 = new Item(3812, 1, 0);
    obj231.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray231 = new Condition[1]
    {
      Condition.DownedOldOnesArmyT2
    };
    NPCShop npcShop232 = npcShop231.Add(obj231, conditionArray231);
    Item obj232 = new Item(900, 1, 0);
    obj232.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray232 = new Condition[2]
    {
      Condition.DownedMechBossAny,
      ModConditions.DownedEclipse
    };
    NPCShop npcShop233 = npcShop232.Add(obj232, conditionArray232);
    Item obj233 = new Item(223, 1, 0);
    obj233.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray233 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop234 = npcShop233.Add(obj233, conditionArray233);
    Item obj234 = new Item(891, 1, 0);
    obj234.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray234 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop235 = npcShop234.Add(obj234, conditionArray234);
    Item obj235 = new Item(1845, 1, 0);
    obj235.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray235 = new Condition[1]
    {
      Condition.DownedMourningWood
    };
    NPCShop npcShop236 = npcShop235.Add(obj235, conditionArray235);
    Item obj236 = new Item(1323, 1, 0);
    obj236.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray236 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop237 = npcShop236.Add(obj236, conditionArray236);
    Item obj237 = new Item(193, 1, 0);
    obj237.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray237 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop238 = npcShop237.Add(obj237, conditionArray237);
    Item obj238 = new Item(938, 1, 0);
    obj238.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray238 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop239 = npcShop238.Add(obj238, conditionArray238);
    Item obj239 = new Item(1290, 1, 0);
    obj239.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray239 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop240 = npcShop239.Add(obj239, conditionArray239);
    Item obj240 = new Item(535, 1, 0);
    obj240.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray240 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop241 = npcShop240.Add(obj240, conditionArray240);
    Item obj241 = new Item(3781, 1, 0);
    obj241.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray241 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop242 = npcShop241.Add(obj241, conditionArray241);
    Item obj242 = new Item(3015, 1, 0);
    obj242.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray242 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop243 = npcShop242.Add(obj242, conditionArray242);
    Item obj243 = new Item(1158, 1, 0);
    obj243.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray243 = new Condition[1]
    {
      Condition.DownedQueenBee
    };
    NPCShop npcShop244 = npcShop243.Add(obj243, conditionArray243);
    Item obj244 = new Item(491, 1, 0);
    obj244.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray244 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop245 = npcShop244.Add(obj244, conditionArray244);
    Item obj245 = new Item(1300, 1, 0);
    obj245.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray245 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop246 = npcShop245.Add(obj245, conditionArray245);
    Item obj246 = new Item(216, 1, 0);
    obj246.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray246 = new Condition[1]
    {
      ModConditions.HasBeenThroughNight
    };
    NPCShop npcShop247 = npcShop246.Add(obj246, conditionArray246);
    Item obj247 = new Item(3212, 1, 0);
    obj247.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray247 = new Condition[2]
    {
      ModConditions.DownedBloodMoon,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop248 = npcShop247.Add(obj247, conditionArray247);
    Item obj248 = new Item(489, 1, 0);
    obj248.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray248 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop249 = npcShop248.Add(obj248, conditionArray248);
    Item obj249 = new Item(3810, 1, 0);
    obj249.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray249 = new Condition[1]
    {
      Condition.DownedOldOnesArmyAny
    };
    NPCShop npcShop250 = npcShop249.Add(obj249, conditionArray249);
    Item obj250 = new Item(532, 1, 0);
    obj250.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray250 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop251 = npcShop250.Add(obj250, conditionArray250);
    Item obj251 = new Item(2998, 1, 0);
    obj251.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray251 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop252 = npcShop251.Add(obj251, conditionArray251);
    Item obj252 = new Item(899, 1, 0);
    obj252.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray252 = new Condition[2]
    {
      Condition.DownedGolem,
      ModConditions.HasBeenToTemple
    };
    NPCShop npcShop253 = npcShop252.Add(obj252, conditionArray252);
    Item obj253 = new Item(536, 1, 0);
    obj253.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray253 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop254 = npcShop253.Add(obj253, conditionArray253);
    Item obj254 = new Item(893, 1, 0);
    obj254.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray254 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop255 = npcShop254.Add(obj254, conditionArray254);
    Item obj255 = new Item(892, 1, 0);
    obj255.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray255 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop256 = npcShop255.Add(obj255, conditionArray255);
    Item obj256 = new Item(490, 1, 0);
    obj256.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray256 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop257 = npcShop256.Add(obj256, conditionArray256);
    Item obj257 = new Item(3306, 1, 0);
    obj257.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray257 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop258 = npcShop257.Add(obj257, conditionArray257);
    Item obj258 = new Item(3334, 1, 0);
    obj258.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray258 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    ((AbstractNPCShop) npcShop258.Add(obj258, conditionArray258)).Register();
    NPCShop npcShop259 = new NPCShop(this.Type, "Informative/Building Gear");
    Item obj259 = new Item(407, 1, 0);
    obj259.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray259 = Array.Empty<Condition>();
    NPCShop npcShop260 = npcShop259.Add(obj259, conditionArray259);
    Item obj260 = new Item(1923, 1, 0);
    obj260.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray260 = Array.Empty<Condition>();
    NPCShop npcShop261 = npcShop260.Add(obj260, conditionArray260);
    Item obj261 = new Item(3061, 1, 0);
    obj261.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray261 = new Condition[1]
    {
      Condition.NotDownedSkeletron
    };
    NPCShop npcShop262 = npcShop261.Add(obj261, conditionArray261);
    Item obj262 = new Item(4056, 1, 0);
    obj262.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray262 = new Condition[2]
    {
      Condition.NotDownedSkeletron,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop263 = npcShop262.Add(obj262, conditionArray262);
    Item obj263 = new Item(5126, 1, 0);
    obj263.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray263 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop264 = npcShop263.Add(obj263, conditionArray263);
    Item obj264 = new Item(3624, 1, 0);
    obj264.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray264 = new Condition[1]
    {
      ModConditions.HasTalkedToTravelingMerchant
    };
    NPCShop npcShop265 = npcShop264.Add(obj264, conditionArray264);
    Item obj265 = new Item(4409, 1, 0);
    obj265.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray265 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop266 = npcShop265.Add(obj265, conditionArray265);
    Item obj266 = new Item(1071, 1, 0);
    obj266.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray266 = new Condition[1]
    {
      Condition.NotDownedPlantera
    };
    NPCShop npcShop267 = npcShop266.Add(obj266, conditionArray266);
    Item obj267 = new Item(1072, 1, 0);
    obj267.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray267 = new Condition[1]
    {
      Condition.NotDownedPlantera
    };
    NPCShop npcShop268 = npcShop267.Add(obj267, conditionArray267);
    Item obj268 = new Item(1100, 1, 0);
    obj268.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray268 = new Condition[1]
    {
      Condition.NotDownedPlantera
    };
    NPCShop npcShop269 = npcShop268.Add(obj268, conditionArray268);
    Item obj269 = new Item(1543, 1, 0);
    obj269.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray269 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop270 = npcShop269.Add(obj269, conditionArray269);
    Item obj270 = new Item(1544, 1, 0);
    obj270.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray270 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop271 = npcShop270.Add(obj270, conditionArray270);
    Item obj271 = new Item(1545, 1, 0);
    obj271.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray271 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop272 = npcShop271.Add(obj271, conditionArray271);
    Item obj272 = new Item(4008, 1, 0);
    obj272.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray272 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop273 = npcShop272.Add(obj272, conditionArray272);
    Item obj273 = new Item(410, 1, 0);
    obj273.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray273 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop274 = npcShop273.Add(obj273, conditionArray273);
    Item obj274 = new Item(411, 1, 0);
    obj274.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray274 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop275 = npcShop274.Add(obj274, conditionArray274);
    Item obj275 = new Item(213, 1, 0);
    obj275.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray275 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop276 = npcShop275.Add(obj275, conditionArray275);
    Item obj276 = new Item(709, 1, 0);
    obj276.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray276 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop277 = npcShop276.Add(obj276, conditionArray276);
    Item obj277 = new Item(18, 1, 0);
    obj277.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray277 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop278 = npcShop277.Add(obj277, conditionArray277);
    Item obj278 = new Item(393, 1, 0);
    obj278.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray278 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop279 = npcShop278.Add(obj278, conditionArray278);
    Item obj279 = new Item(3084, 1, 0);
    obj279.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray279 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop280 = npcShop279.Add(obj279, conditionArray279);
    Item obj280 = new Item(3095, 1, 0);
    obj280.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray280 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop281 = npcShop280.Add(obj280, conditionArray280);
    Item obj281 = new Item(3118, 1, 0);
    obj281.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray281 = Array.Empty<Condition>();
    NPCShop npcShop282 = npcShop281.Add(obj281, conditionArray281);
    Item obj282 = new Item(3119, 1, 0);
    obj282.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray282 = Array.Empty<Condition>();
    NPCShop npcShop283 = npcShop282.Add(obj282, conditionArray282);
    Item obj283 = new Item(3099, 1, 0);
    obj283.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray283 = Array.Empty<Condition>();
    NPCShop npcShop284 = npcShop283.Add(obj283, conditionArray283);
    Item obj284 = new Item(3102, 1, 0);
    obj284.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray284 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop285 = npcShop284.Add(obj284, conditionArray284);
    Item obj285 = new Item(3120, 1, 0);
    obj285.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray285 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop286 = npcShop285.Add(obj285, conditionArray285);
    Item obj286 = new Item(3037, 1, 0);
    obj286.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray286 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop287 = npcShop286.Add(obj286, conditionArray286);
    Item obj287 = new Item(3096, 1, 0);
    obj287.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray287 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop288 = npcShop287.Add(obj287, conditionArray287);
    Item obj288 = new Item(50, 1, 0);
    obj288.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray288 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop289 = npcShop288.Add(obj288, conditionArray288);
    Item obj289 = new Item(4263, 1, 0);
    obj289.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray289 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop290 = npcShop289.Add(obj289, conditionArray289);
    Item obj290 = new Item(4819, 1, 0);
    obj290.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray290 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop291 = npcShop290.Add(obj290, conditionArray290);
    Item obj291 = new Item(5358, 1, 0);
    obj291.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray291 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop292 = npcShop291.Add(obj291, conditionArray291);
    Item obj292 = new Item(1861, 1, 0);
    obj292.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray292 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop293 = npcShop292.Add(obj292, conditionArray292);
    Item obj293 = new Item(854, 1, 0);
    obj293.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray293 = new Condition[1]
    {
      Condition.DownedPirates
    };
    NPCShop npcShop294 = npcShop293.Add(obj293, conditionArray293);
    Item obj294 = new Item(855, 1, 0);
    obj294.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray294 = new Condition[1]
    {
      Condition.DownedPirates
    };
    NPCShop npcShop295 = npcShop294.Add(obj294, conditionArray294);
    Item obj295 = new Item(3033, 1, 0);
    obj295.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray295 = new Condition[1]
    {
      Condition.DownedPirates
    };
    NPCShop npcShop296 = npcShop295.Add(obj295, conditionArray295);
    Item obj296 = new Item(3611, 1, 0);
    obj296.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray296 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop297 = npcShop296.Add(obj296, conditionArray296);
    Item obj297 = new Item(3213, 1, 0);
    obj297.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray297 = new Condition[1]
    {
      ModConditions.DownedBloodMoon
    };
    NPCShop npcShop298 = npcShop297.Add(obj297, conditionArray297);
    Item obj298 = new Item(3068, 1, 0);
    obj298.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray298 = Array.Empty<Condition>();
    NPCShop npcShop299 = npcShop298.Add(obj298, conditionArray298);
    Item obj299 = new Item(5323, 1, 0);
    obj299.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray299 = Array.Empty<Condition>();
    NPCShop npcShop300 = npcShop299.Add(obj299, conditionArray299);
    Item obj300 = new Item(3090, 1, 0);
    obj300.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray300 = new Condition[2]
    {
      Condition.DownedKingSlime,
      Condition.InExpertMode
    };
    NPCShop npcShop301 = npcShop300.Add(obj300, conditionArray300);
    Item obj301 = new Item(5043, 1, 0);
    obj301.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray301 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop302 = npcShop301.Add(obj301, conditionArray301);
    Item obj302 = new Item(327, 1, 0);
    obj302.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray302 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop303 = npcShop302.Add(obj302, conditionArray302);
    Item obj303 = new Item(329, 1, 0);
    obj303.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray303 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop304 = npcShop303.Add(obj303, conditionArray303);
    Item obj304 = new Item(1534, 1, 0);
    obj304.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray304 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop305 = npcShop304.Add(obj304, conditionArray304);
    Item obj305 = new Item(1535 /*0x05FF*/, 1, 0);
    obj305.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray305 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop306 = npcShop305.Add(obj305, conditionArray305);
    Item obj306 = new Item(4714, 1, 0);
    obj306.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray306 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop307 = npcShop306.Add(obj306, conditionArray306);
    Item obj307 = new Item(1537, 1, 0);
    obj307.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray307 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop308 = npcShop307.Add(obj307, conditionArray307);
    Item obj308 = new Item(1536 /*0x0600*/, 1, 0);
    obj308.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray308 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop309 = npcShop308.Add(obj308, conditionArray308);
    Item obj309 = new Item(1533, 1, 0);
    obj309.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray309 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop310 = npcShop309.Add(obj309, conditionArray309);
    Item obj310 = new Item(1141, 1, 0);
    obj310.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray310 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop311 = npcShop310.Add(obj310, conditionArray310);
    Item obj311 = new Item(206, 1, 0);
    obj311.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray311 = Array.Empty<Condition>();
    NPCShop npcShop312 = npcShop311.Add(obj311, conditionArray311);
    Item obj312 = new Item(207, 1, 0);
    obj312.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray312 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop313 = npcShop312.Add(obj312, conditionArray312);
    Item obj313 = new Item(1128, 1, 0);
    obj313.shopCustomPrice = new int?(Item.buyPrice(0, 15, 0, 0));
    Condition[] conditionArray313 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop314 = npcShop313.Add(obj313, conditionArray313);
    Item obj314 = new Item(3031, 1, 0);
    obj314.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray314 = Array.Empty<Condition>();
    NPCShop npcShop315 = npcShop314.Add(obj314, conditionArray314);
    Item obj315 = new Item(4820, 1, 0);
    obj315.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray315 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop316 = npcShop315.Add(obj315, conditionArray315);
    Item obj316 = new Item(5302, 1, 0);
    obj316.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray316 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop317 = npcShop316.Add(obj316, conditionArray316);
    Item obj317 = new Item(5364, 1, 0);
    obj317.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray317 = new Condition[2]
    {
      Condition.DownedMoonLord,
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop318 = npcShop317.Add(obj317, conditionArray317);
    Item obj318 = new Item(3032, 1, 0);
    obj318.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray318 = Array.Empty<Condition>();
    NPCShop npcShop319 = npcShop318.Add(obj318, conditionArray318);
    Item obj319 = new Item(5303, 1, 0);
    obj319.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray319 = Array.Empty<Condition>();
    NPCShop npcShop320 = npcShop319.Add(obj319, conditionArray319);
    Item obj320 = new Item(4872, 1, 0);
    obj320.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray320 = Array.Empty<Condition>();
    NPCShop npcShop321 = npcShop320.Add(obj320, conditionArray320);
    Item obj321 = new Item(1299, 1, 0);
    obj321.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray321 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop322 = npcShop321.Add(obj321, conditionArray321);
    Item obj322 = new Item(1786, 1, 0);
    obj322.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray322 = Array.Empty<Condition>();
    NPCShop npcShop323 = npcShop322.Add(obj322, conditionArray322);
    Item obj323 = new Item(4049, 1, 0);
    obj323.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray323 = Array.Empty<Condition>();
    NPCShop npcShop324 = npcShop323.Add(obj323, conditionArray323);
    Item obj324 = new Item(4346, 1, 0);
    obj324.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray324 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop325 = npcShop324.Add(obj324, conditionArray324);
    Item obj325 = new Item(3384, 1, 0);
    obj325.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray325 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop326 = npcShop325.Add(obj325, conditionArray325);
    Item obj326 = new Item(1326, 1, 0);
    obj326.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray326 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop327 = npcShop326.Add(obj326, conditionArray326);
    Item obj327 = new Item(5335, 1, 0);
    obj327.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray327 = new Condition[2]
    {
      ModConditions.ShimmerableAfterMoonLordOrSupremeCalamitas,
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop328 = npcShop327.Add(obj327, conditionArray327);
    Item obj328 = new Item(779, 1, 0);
    obj328.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray328 = new Condition[3]
    {
      Condition.Hardmode,
      Condition.DownedMechBossAny,
      Condition.NotDownedMoonLord
    };
    NPCShop npcShop329 = npcShop328.Add(obj328, conditionArray328);
    Item obj329 = new Item(5134, 1, 0);
    obj329.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray329 = new Condition[2]
    {
      Condition.DownedMoonLord,
      ModConditions.HasBeenToAether
    };
    ((AbstractNPCShop) npcShop329.Add(obj329, conditionArray329)).Register();
    NPCShop npcShop330 = new NPCShop(this.Type, "Treasure Bags");
    Item obj330 = new Item(3318, 1, 0);
    obj330.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray330 = new Condition[2]
    {
      Condition.DownedKingSlime,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop331 = npcShop330.Add(obj330, conditionArray330);
    Item obj331 = new Item(3319, 1, 0);
    obj331.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray331 = new Condition[2]
    {
      Condition.DownedEyeOfCthulhu,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop332 = npcShop331.Add(obj331, conditionArray331);
    Item obj332 = new Item(3320, 1, 0);
    obj332.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray332 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop333 = npcShop332.Add(obj332, conditionArray332);
    Item obj333 = new Item(3321, 1, 0);
    obj333.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray333 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop334 = npcShop333.Add(obj333, conditionArray333);
    Item obj334 = new Item(3322, 1, 0);
    obj334.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray334 = new Condition[2]
    {
      Condition.DownedQueenBee,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop335 = npcShop334.Add(obj334, conditionArray334);
    Item obj335 = new Item(3323, 1, 0);
    obj335.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray335 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop336 = npcShop335.Add(obj335, conditionArray335);
    Item obj336 = new Item(5111, 1, 0);
    obj336.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray336 = new Condition[2]
    {
      Condition.DownedDeerclops,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop337 = npcShop336.Add(obj336, conditionArray336);
    Item obj337 = new Item(3324, 1, 0);
    obj337.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray337 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop338 = npcShop337.Add(obj337, conditionArray337);
    Item obj338 = new Item(4957, 1, 0);
    obj338.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray338 = new Condition[2]
    {
      Condition.DownedQueenSlime,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop339 = npcShop338.Add(obj338, conditionArray338);
    Item obj339 = new Item(3325, 1, 0);
    obj339.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray339 = new Condition[2]
    {
      Condition.DownedDestroyer,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop340 = npcShop339.Add(obj339, conditionArray339);
    Item obj340 = new Item(3326, 1, 0);
    obj340.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray340 = new Condition[2]
    {
      Condition.DownedTwins,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop341 = npcShop340.Add(obj340, conditionArray340);
    Item obj341 = new Item(3327 /*0x0CFF*/, 1, 0);
    obj341.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray341 = new Condition[2]
    {
      Condition.DownedSkeletronPrime,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop342 = npcShop341.Add(obj341, conditionArray341);
    Item obj342 = new Item(3328 /*0x0D00*/, 1, 0);
    obj342.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray342 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop343 = npcShop342.Add(obj342, conditionArray342);
    Item obj343 = new Item(3329, 1, 0);
    obj343.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray343 = new Condition[2]
    {
      Condition.DownedGolem,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop344 = npcShop343.Add(obj343, conditionArray343);
    Item obj344 = new Item(3860, 1, 0);
    obj344.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray344 = new Condition[2]
    {
      Condition.DownedOldOnesArmyT3,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop345 = npcShop344.Add(obj344, conditionArray344);
    Item obj345 = new Item(3330, 1, 0);
    obj345.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray345 = new Condition[2]
    {
      Condition.DownedDukeFishron,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop346 = npcShop345.Add(obj345, conditionArray345);
    Item obj346 = new Item(4782, 1, 0);
    obj346.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray346 = new Condition[2]
    {
      Condition.DownedEmpressOfLight,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop347 = npcShop346.Add(obj346, conditionArray346);
    Item obj347 = new Item(3332, 1, 0);
    obj347.shopCustomPrice = new int?(Item.buyPrice(0, 25, 0, 0));
    Condition[] conditionArray347 = new Condition[2]
    {
      Condition.DownedMoonLord,
      ModConditions.expertOrMaster
    };
    NPCShop npcShop348 = npcShop347.Add(obj347, conditionArray347);
    Item obj348 = new Item(3817, 1, 0);
    obj348.shopCustomPrice = new int?(Item.buyPrice(0, 1, 0, 0));
    Condition[] conditionArray348 = new Condition[1]
    {
      Condition.DownedOldOnesArmyAny
    };
    ((AbstractNPCShop) npcShop348.Add(obj348, conditionArray348)).Register();
    NPCShop npcShop349 = new NPCShop(this.Type, "Crates & Grab Bags");
    Item obj349 = new Item(2334, 1, 0);
    obj349.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray349 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop350 = npcShop349.Add(obj349, conditionArray349);
    Item obj350 = new Item(2335, 1, 0);
    obj350.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray350 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop351 = npcShop350.Add(obj350, conditionArray350);
    Item obj351 = new Item(2336, 1, 0);
    obj351.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray351 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop352 = npcShop351.Add(obj351, conditionArray351);
    Item obj352 = new Item(3208, 1, 0);
    obj352.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray352 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop353 = npcShop352.Add(obj352, conditionArray352);
    Item obj353 = new Item(3206, 1, 0);
    obj353.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray353 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop354 = npcShop353.Add(obj353, conditionArray353);
    Item obj354 = new Item(3203, 1, 0);
    obj354.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray354 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop355 = npcShop354.Add(obj354, conditionArray354);
    Item obj355 = new Item(3204, 1, 0);
    obj355.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray355 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop356 = npcShop355.Add(obj355, conditionArray355);
    Item obj356 = new Item(3207, 1, 0);
    obj356.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray356 = new Condition[1]
    {
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop357 = npcShop356.Add(obj356, conditionArray356);
    Item obj357 = new Item(3205, 1, 0);
    obj357.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray357 = new Condition[2]
    {
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop358 = npcShop357.Add(obj357, conditionArray357);
    Item obj358 = new Item(4405, 1, 0);
    obj358.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray358 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop359 = npcShop358.Add(obj358, conditionArray358);
    Item obj359 = new Item(4407, 1, 0);
    obj359.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray359 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop360 = npcShop359.Add(obj359, conditionArray359);
    Item obj360 = new Item(4877, 1, 0);
    obj360.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray360 = new Condition[1]
    {
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop361 = npcShop360.Add(obj360, conditionArray360);
    Item obj361 = new Item(5002, 1, 0);
    obj361.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray361 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop362 = npcShop361.Add(obj361, conditionArray361);
    Item obj362 = new Item(3979, 1, 0);
    obj362.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray362 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop363 = npcShop362.Add(obj362, conditionArray362);
    Item obj363 = new Item(3980, 1, 0);
    obj363.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray363 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop364 = npcShop363.Add(obj363, conditionArray363);
    Item obj364 = new Item(3981, 1, 0);
    obj364.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray364 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop365 = npcShop364.Add(obj364, conditionArray364);
    Item obj365 = new Item(3987, 1, 0);
    obj365.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray365 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop366 = npcShop365.Add(obj365, conditionArray365);
    Item obj366 = new Item(3985, 1, 0);
    obj366.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray366 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop367 = npcShop366.Add(obj366, conditionArray366);
    Item obj367 = new Item(3982, 1, 0);
    obj367.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray367 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop368 = npcShop367.Add(obj367, conditionArray367);
    Item obj368 = new Item(3983, 1, 0);
    obj368.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray368 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop369 = npcShop368.Add(obj368, conditionArray368);
    Item obj369 = new Item(3986, 1, 0);
    obj369.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray369 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop370 = npcShop369.Add(obj369, conditionArray369);
    Item obj370 = new Item(3984, 1, 0);
    obj370.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray370 = new Condition[3]
    {
      Condition.Hardmode,
      Condition.DownedSkeletron,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop371 = npcShop370.Add(obj370, conditionArray370);
    Item obj371 = new Item(4406, 1, 0);
    obj371.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray371 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop372 = npcShop371.Add(obj371, conditionArray371);
    Item obj372 = new Item(4408, 1, 0);
    obj372.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray372 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop373 = npcShop372.Add(obj372, conditionArray372);
    Item obj373 = new Item(4878, 1, 0);
    obj373.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray373 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop374 = npcShop373.Add(obj373, conditionArray373);
    Item obj374 = new Item(5003, 1, 0);
    obj374.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray374 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop375 = npcShop374.Add(obj374, conditionArray374);
    Item obj375 = new Item(1774, 1, 0);
    obj375.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray375 = Array.Empty<Condition>();
    NPCShop npcShop376 = npcShop375.Add(obj375, conditionArray375);
    Item obj376 = new Item(1869, 1, 0);
    obj376.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray376 = Array.Empty<Condition>();
    ((AbstractNPCShop) npcShop376.Add(obj376, conditionArray376)).Register();
    NPCShop npcShop377 = new NPCShop(this.Type, "Ores & Bars");
    Item obj377 = new Item(12, 1, 0);
    obj377.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray377 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop378 = npcShop377.Add(obj377, conditionArray377);
    Item obj378 = new Item(699, 1, 0);
    obj378.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray378 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop379 = npcShop378.Add(obj378, conditionArray378);
    Item obj379 = new Item(11, 1, 0);
    obj379.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray379 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop380 = npcShop379.Add(obj379, conditionArray379);
    Item obj380 = new Item(700, 1, 0);
    obj380.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray380 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop381 = npcShop380.Add(obj380, conditionArray380);
    Item obj381 = new Item(14, 1, 0);
    obj381.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray381 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop382 = npcShop381.Add(obj381, conditionArray381);
    Item obj382 = new Item(701, 1, 0);
    obj382.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray382 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop383 = npcShop382.Add(obj382, conditionArray382);
    Item obj383 = new Item(13, 1, 0);
    obj383.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray383 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop384 = npcShop383.Add(obj383, conditionArray383);
    Item obj384 = new Item(702, 1, 0);
    obj384.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray384 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop385 = npcShop384.Add(obj384, conditionArray384);
    Item obj385 = new Item(116, 1, 0);
    obj385.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray385 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop386 = npcShop385.Add(obj385, conditionArray385);
    Item obj386 = new Item(56, 1, 0);
    obj386.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray386 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop387 = npcShop386.Add(obj386, conditionArray386);
    Item obj387 = new Item(880, 1, 0);
    obj387.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray387 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop388 = npcShop387.Add(obj387, conditionArray387);
    Item obj388 = new Item(174, 1, 0);
    obj388.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray388 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop389 = npcShop388.Add(obj388, conditionArray388);
    Item obj389 = new Item(364, 1, 0);
    obj389.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray389 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop390 = npcShop389.Add(obj389, conditionArray389);
    Item obj390 = new Item(1104, 1, 0);
    obj390.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray390 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop391 = npcShop390.Add(obj390, conditionArray390);
    Item obj391 = new Item(365, 1, 0);
    obj391.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray391 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop392 = npcShop391.Add(obj391, conditionArray391);
    Item obj392 = new Item(1105, 1, 0);
    obj392.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray392 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop393 = npcShop392.Add(obj392, conditionArray392);
    Item obj393 = new Item(366, 1, 0);
    obj393.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray393 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop394 = npcShop393.Add(obj393, conditionArray393);
    Item obj394 = new Item(1106, 1, 0);
    obj394.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray394 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop395 = npcShop394.Add(obj394, conditionArray394);
    Item obj395 = new Item(947, 1, 0);
    obj395.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray395 = new Condition[2]
    {
      Condition.DownedMechBossAll,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop396 = npcShop395.Add(obj395, conditionArray395);
    Item obj396 = new Item(3460, 1, 0);
    obj396.shopCustomPrice = new int?(Item.buyPrice(0, 0, 25, 0));
    Condition[] conditionArray396 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop397 = npcShop396.Add(obj396, conditionArray396);
    Item obj397 = new Item(20, 1, 0);
    obj397.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray397 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop398 = npcShop397.Add(obj397, conditionArray397);
    Item obj398 = new Item(703, 1, 0);
    obj398.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray398 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop399 = npcShop398.Add(obj398, conditionArray398);
    Item obj399 = new Item(22, 1, 0);
    obj399.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray399 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop400 = npcShop399.Add(obj399, conditionArray399);
    Item obj400 = new Item(704, 1, 0);
    obj400.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray400 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop401 = npcShop400.Add(obj400, conditionArray400);
    Item obj401 = new Item(21, 1, 0);
    obj401.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray401 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop402 = npcShop401.Add(obj401, conditionArray401);
    Item obj402 = new Item(705, 1, 0);
    obj402.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray402 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop403 = npcShop402.Add(obj402, conditionArray402);
    Item obj403 = new Item(19, 1, 0);
    obj403.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray403 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop404 = npcShop403.Add(obj403, conditionArray403);
    Item obj404 = new Item(706, 1, 0);
    obj404.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray404 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop405 = npcShop404.Add(obj404, conditionArray404);
    Item obj405 = new Item(117, 1, 0);
    obj405.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray405 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop406 = npcShop405.Add(obj405, conditionArray405);
    Item obj406 = new Item(57, 1, 0);
    obj406.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray406 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop407 = npcShop406.Add(obj406, conditionArray406);
    Item obj407 = new Item(1257, 1, 0);
    obj407.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray407 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop408 = npcShop407.Add(obj407, conditionArray407);
    Item obj408 = new Item(175, 1, 0);
    obj408.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray408 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop409 = npcShop408.Add(obj408, conditionArray408);
    Item obj409 = new Item(381, 1, 0);
    obj409.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray409 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop410 = npcShop409.Add(obj409, conditionArray409);
    Item obj410 = new Item(1184, 1, 0);
    obj410.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray410 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop411 = npcShop410.Add(obj410, conditionArray410);
    Item obj411 = new Item(382, 1, 0);
    obj411.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray411 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop412 = npcShop411.Add(obj411, conditionArray411);
    Item obj412 = new Item(1191, 1, 0);
    obj412.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray412 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop413 = npcShop412.Add(obj412, conditionArray412);
    Item obj413 = new Item(391, 1, 0);
    obj413.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray413 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop414 = npcShop413.Add(obj413, conditionArray413);
    Item obj414 = new Item(1198, 1, 0);
    obj414.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray414 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop415 = npcShop414.Add(obj414, conditionArray414);
    Item obj415 = new Item(1225, 1, 0);
    obj415.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray415 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop416 = npcShop415.Add(obj415, conditionArray415);
    Item obj416 = new Item(1006, 1, 0);
    obj416.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray416 = new Condition[2]
    {
      Condition.DownedMechBossAll,
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop417 = npcShop416.Add(obj416, conditionArray416);
    Item obj417 = new Item(3261, 1, 0);
    obj417.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray417 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToDungeon
    };
    NPCShop npcShop418 = npcShop417.Add(obj417, conditionArray417);
    Item obj418 = new Item(1552, 1, 0);
    obj418.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray418 = new Condition[2]
    {
      Condition.DownedPlantera,
      ModConditions.HasBeenToMushroom
    };
    NPCShop npcShop419 = npcShop418.Add(obj418, conditionArray418);
    Item obj419 = new Item(3467, 1, 0);
    obj419.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray419 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop420 = npcShop419.Add(obj419, conditionArray419);
    Item obj420 = new Item(502, 1, 0);
    obj420.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray420 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop421 = npcShop420.Add(obj420, conditionArray420);
    Item obj421 = new Item(3380, 1, 0);
    obj421.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray421 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop422 = npcShop421.Add(obj421, conditionArray421);
    Item obj422 = new Item(999, 1, 0);
    obj422.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray422 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop423 = npcShop422.Add(obj422, conditionArray422);
    Item obj423 = new Item(181, 1, 0);
    obj423.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray423 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop424 = npcShop423.Add(obj423, conditionArray423);
    Item obj424 = new Item(182, 1, 0);
    obj424.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray424 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop425 = npcShop424.Add(obj424, conditionArray424);
    Item obj425 = new Item(179, 1, 0);
    obj425.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray425 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop426 = npcShop425.Add(obj425, conditionArray425);
    Item obj426 = new Item(178, 1, 0);
    obj426.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray426 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop427 = npcShop426.Add(obj426, conditionArray426);
    Item obj427 = new Item(177, 1, 0);
    obj427.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray427 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop428 = npcShop427.Add(obj427, conditionArray427);
    Item obj428 = new Item(180, 1, 0);
    obj428.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray428 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop429 = npcShop428.Add(obj428, conditionArray428);
    Item obj429 = new Item(4646, 1, 0);
    obj429.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray429 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop430 = npcShop429.Add(obj429, conditionArray429);
    Item obj430 = new Item(4640, 1, 0);
    obj430.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray430 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop431 = npcShop430.Add(obj430, conditionArray430);
    Item obj431 = new Item(4645, 1, 0);
    obj431.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray431 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop432 = npcShop431.Add(obj431, conditionArray431);
    Item obj432 = new Item(4643, 1, 0);
    obj432.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray432 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop433 = npcShop432.Add(obj432, conditionArray432);
    Item obj433 = new Item(4644, 1, 0);
    obj433.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray433 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop434 = npcShop433.Add(obj433, conditionArray433);
    Item obj434 = new Item(4642, 1, 0);
    obj434.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray434 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop435 = npcShop434.Add(obj434, conditionArray434);
    Item obj435 = new Item(4641, 1, 0);
    obj435.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray435 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop436 = npcShop435.Add(obj435, conditionArray435);
    Item obj436 = new Item(4400, 1, 0);
    obj436.shopCustomPrice = new int?(Item.buyPrice(0, 0, 50, 0));
    Condition[] conditionArray436 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    ((AbstractNPCShop) npcShop436.Add(obj436, conditionArray436)).Register();
    NPCShop npcShop437 = new NPCShop(this.Type, "Natural Blocks");
    Item obj437 = new Item(9, 1, 0);
    obj437.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray437 = Array.Empty<Condition>();
    NPCShop npcShop438 = npcShop437.Add(obj437, conditionArray437);
    Item obj438 = new Item(2503, 1, 0);
    obj438.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray438 = Array.Empty<Condition>();
    NPCShop npcShop439 = npcShop438.Add(obj438, conditionArray438);
    Item obj439 = new Item(2504, 1, 0);
    obj439.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray439 = Array.Empty<Condition>();
    NPCShop npcShop440 = npcShop439.Add(obj439, conditionArray439);
    Item obj440 = new Item(620, 1, 0);
    obj440.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray440 = Array.Empty<Condition>();
    NPCShop npcShop441 = npcShop440.Add(obj440, conditionArray440);
    Item obj441 = new Item(619, 1, 0);
    obj441.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray441 = Array.Empty<Condition>();
    NPCShop npcShop442 = npcShop441.Add(obj441, conditionArray441);
    Item obj442 = new Item(911, 1, 0);
    obj442.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray442 = Array.Empty<Condition>();
    NPCShop npcShop443 = npcShop442.Add(obj442, conditionArray442);
    Item obj443 = new Item(5215, 1, 0);
    obj443.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray443 = Array.Empty<Condition>();
    NPCShop npcShop444 = npcShop443.Add(obj443, conditionArray443);
    Item obj444 = new Item(621, 1, 0);
    obj444.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray444 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop445 = npcShop444.Add(obj444, conditionArray444);
    Item obj445 = new Item(1729, 1, 0);
    obj445.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray445 = new Condition[1]
    {
      Condition.DownedMourningWood
    };
    NPCShop npcShop446 = npcShop445.Add(obj445, conditionArray445);
    Item obj446 = new Item(2260, 1, 0);
    obj446.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray446 = Array.Empty<Condition>();
    NPCShop npcShop447 = npcShop446.Add(obj446, conditionArray446);
    Item obj447 = new Item(4564, 1, 0);
    obj447.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray447 = Array.Empty<Condition>();
    NPCShop npcShop448 = npcShop447.Add(obj447, conditionArray447);
    Item obj448 = new Item(4547, 1, 0);
    obj448.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray448 = Array.Empty<Condition>();
    NPCShop npcShop449 = npcShop448.Add(obj448, conditionArray448);
    Item obj449 = new Item(276, 1, 0);
    obj449.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray449 = Array.Empty<Condition>();
    NPCShop npcShop450 = npcShop449.Add(obj449, conditionArray449);
    Item obj450 = new Item(1725, 1, 0);
    obj450.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray450 = Array.Empty<Condition>();
    NPCShop npcShop451 = npcShop450.Add(obj450, conditionArray450);
    Item obj451 = new Item(1872, 1, 0);
    obj451.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray451 = Array.Empty<Condition>();
    NPCShop npcShop452 = npcShop451.Add(obj451, conditionArray451);
    Item obj452 = new Item(2, 1, 0);
    obj452.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray452 = Array.Empty<Condition>();
    NPCShop npcShop453 = npcShop452.Add(obj452, conditionArray452);
    Item obj453 = new Item(133, 1, 0);
    obj453.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray453 = Array.Empty<Condition>();
    NPCShop npcShop454 = npcShop453.Add(obj453, conditionArray453);
    Item obj454 = new Item(176 /*0xB0*/, 1, 0);
    obj454.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray454 = Array.Empty<Condition>();
    NPCShop npcShop455 = npcShop454.Add(obj454, conditionArray454);
    Item obj455 = new Item(172, 1, 0);
    obj455.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray455 = Array.Empty<Condition>();
    NPCShop npcShop456 = npcShop455.Add(obj455, conditionArray455);
    Item obj456 = new Item(424, 1, 0);
    obj456.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray456 = Array.Empty<Condition>();
    NPCShop npcShop457 = npcShop456.Add(obj456, conditionArray456);
    Item obj457 = new Item(1103, 1, 0);
    obj457.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray457 = Array.Empty<Condition>();
    NPCShop npcShop458 = npcShop457.Add(obj457, conditionArray457);
    Item obj458 = new Item(593, 1, 0);
    obj458.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray458 = Array.Empty<Condition>();
    NPCShop npcShop459 = npcShop458.Add(obj458, conditionArray458);
    Item obj459 = new Item(169, 1, 0);
    obj459.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray459 = Array.Empty<Condition>();
    NPCShop npcShop460 = npcShop459.Add(obj459, conditionArray459);
    Item obj460 = new Item(370, 1, 0);
    obj460.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray460 = Array.Empty<Condition>();
    NPCShop npcShop461 = npcShop460.Add(obj460, conditionArray460);
    Item obj461 = new Item(1246, 1, 0);
    obj461.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray461 = Array.Empty<Condition>();
    NPCShop npcShop462 = npcShop461.Add(obj461, conditionArray461);
    Item obj462 = new Item(408, 1, 0);
    obj462.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray462 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop463 = npcShop462.Add(obj462, conditionArray462);
    Item obj463 = new Item(3, 1, 0);
    obj463.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray463 = Array.Empty<Condition>();
    NPCShop npcShop464 = npcShop463.Add(obj463, conditionArray463);
    Item obj464 = new Item(61, 1, 0);
    obj464.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray464 = Array.Empty<Condition>();
    NPCShop npcShop465 = npcShop464.Add(obj464, conditionArray464);
    Item obj465 = new Item(836, 1, 0);
    obj465.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray465 = Array.Empty<Condition>();
    NPCShop npcShop466 = npcShop465.Add(obj465, conditionArray465);
    Item obj466 = new Item(409, 1, 0);
    obj466.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray466 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop467 = npcShop466.Add(obj466, conditionArray466);
    Item obj467 = new Item(664, 1, 0);
    obj467.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray467 = Array.Empty<Condition>();
    NPCShop npcShop468 = npcShop467.Add(obj467, conditionArray467);
    Item obj468 = new Item(833, 1, 0);
    obj468.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray468 = Array.Empty<Condition>();
    NPCShop npcShop469 = npcShop468.Add(obj468, conditionArray468);
    Item obj469 = new Item(835, 1, 0);
    obj469.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray469 = Array.Empty<Condition>();
    NPCShop npcShop470 = npcShop469.Add(obj469, conditionArray469);
    Item obj470 = new Item(834, 1, 0);
    obj470.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray470 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop471 = npcShop470.Add(obj470, conditionArray470);
    Item obj471 = new Item(3086, 1, 0);
    obj471.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray471 = Array.Empty<Condition>();
    NPCShop npcShop472 = npcShop471.Add(obj471, conditionArray471);
    Item obj472 = new Item(3081, 1, 0);
    obj472.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray472 = Array.Empty<Condition>();
    NPCShop npcShop473 = npcShop472.Add(obj472, conditionArray472);
    Item obj473 = new Item(173, 1, 0);
    obj473.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray473 = Array.Empty<Condition>();
    NPCShop npcShop474 = npcShop473.Add(obj473, conditionArray473);
    Item obj474 = new Item(5349, 1, 0);
    obj474.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray474 = Array.Empty<Condition>();
    NPCShop npcShop475 = npcShop474.Add(obj474, conditionArray474);
    Item obj475 = new Item(3272, 1, 0);
    obj475.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray475 = Array.Empty<Condition>();
    NPCShop npcShop476 = npcShop475.Add(obj475, conditionArray475);
    Item obj476 = new Item(3274, 1, 0);
    obj476.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray476 = Array.Empty<Condition>();
    NPCShop npcShop477 = npcShop476.Add(obj476, conditionArray476);
    Item obj477 = new Item(3275, 1, 0);
    obj477.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray477 = Array.Empty<Condition>();
    NPCShop npcShop478 = npcShop477.Add(obj477, conditionArray477);
    Item obj478 = new Item(3338, 1, 0);
    obj478.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray478 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop479 = npcShop478.Add(obj478, conditionArray478);
    Item obj479 = new Item(3271, 1, 0);
    obj479.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray479 = Array.Empty<Condition>();
    NPCShop npcShop480 = npcShop479.Add(obj479, conditionArray479);
    Item obj480 = new Item(3276, 1, 0);
    obj480.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray480 = Array.Empty<Condition>();
    NPCShop npcShop481 = npcShop480.Add(obj480, conditionArray480);
    Item obj481 = new Item(3277, 1, 0);
    obj481.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray481 = Array.Empty<Condition>();
    NPCShop npcShop482 = npcShop481.Add(obj481, conditionArray481);
    Item obj482 = new Item(3339, 1, 0);
    obj482.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray482 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop483 = npcShop482.Add(obj482, conditionArray482);
    Item obj483 = new Item(3347, 1, 0);
    obj483.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray483 = Array.Empty<Condition>();
    NPCShop npcShop484 = npcShop483.Add(obj483, conditionArray483);
    Item obj484 = new Item(4090, 1, 0);
    obj484.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray484 = Array.Empty<Condition>();
    NPCShop npcShop485 = npcShop484.Add(obj484, conditionArray484);
    Item obj485 = new Item(751, 1, 0);
    obj485.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray485 = Array.Empty<Condition>();
    NPCShop npcShop486 = npcShop485.Add(obj485, conditionArray485);
    Item obj486 = new Item(765, 1, 0);
    obj486.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray486 = Array.Empty<Condition>();
    NPCShop npcShop487 = npcShop486.Add(obj486, conditionArray486);
    Item obj487 = new Item(1124, 1, 0);
    obj487.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray487 = Array.Empty<Condition>();
    NPCShop npcShop488 = npcShop487.Add(obj487, conditionArray487);
    Item obj488 = new Item(1125, 1, 0);
    obj488.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray488 = Array.Empty<Condition>();
    NPCShop npcShop489 = npcShop488.Add(obj488, conditionArray488);
    Item obj489 = new Item(1127, 1, 0);
    obj489.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray489 = Array.Empty<Condition>();
    NPCShop npcShop490 = npcShop489.Add(obj489, conditionArray489);
    Item obj490 = new Item(1727, 1, 0);
    obj490.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray490 = Array.Empty<Condition>();
    NPCShop npcShop491 = npcShop490.Add(obj490, conditionArray490);
    Item obj491 = new Item(150, 1, 0);
    obj491.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray491 = Array.Empty<Condition>();
    NPCShop npcShop492 = npcShop491.Add(obj491, conditionArray491);
    Item obj492 = new Item(933, 1, 0);
    obj492.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray492 = Array.Empty<Condition>();
    NPCShop npcShop493 = npcShop492.Add(obj492, conditionArray492);
    Item obj493 = new Item(832, 1, 0);
    obj493.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray493 = Array.Empty<Condition>();
    NPCShop npcShop494 = npcShop493.Add(obj493, conditionArray493);
    Item obj494 = new Item(3361, 1, 0);
    obj494.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray494 = Array.Empty<Condition>();
    NPCShop npcShop495 = npcShop494.Add(obj494, conditionArray494);
    Item obj495 = new Item(3360, 1, 0);
    obj495.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray495 = Array.Empty<Condition>();
    NPCShop npcShop496 = npcShop495.Add(obj495, conditionArray495);
    Item obj496 = new Item(1129, 1, 0);
    obj496.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray496 = new Condition[1]
    {
      Condition.DownedQueenBee
    };
    NPCShop npcShop497 = npcShop496.Add(obj496, conditionArray496);
    Item obj497 = new Item(932, 1, 0);
    obj497.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray497 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop498 = npcShop497.Add(obj497, conditionArray497);
    Item obj498 = new Item(496, 1, 0);
    obj498.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray498 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop499 = npcShop498.Add(obj498, conditionArray498);
    Item obj499 = new Item(114, 1, 0);
    obj499.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray499 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop500 = npcShop499.Add(obj499, conditionArray499);
    Item obj500 = new Item(5324, 1, 0);
    obj500.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray500 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop501 = npcShop500.Add(obj500, conditionArray500);
    Item obj501 = new Item(4460, 1, 0);
    obj501.shopCustomPrice = new int?(Item.buyPrice(0, 5, 0, 0));
    Condition[] conditionArray501 = Array.Empty<Condition>();
    ((AbstractNPCShop) npcShop501.Add(obj501, conditionArray501)).Register();
    NPCShop npcShop502 = new NPCShop(this.Type, "Building Blocks");
    Item obj502 = new Item(129, 1, 0);
    obj502.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray502 = Array.Empty<Condition>();
    NPCShop npcShop503 = npcShop502.Add(obj502, conditionArray502);
    Item obj503 = new Item(2119, 1, 0);
    obj503.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray503 = Array.Empty<Condition>();
    NPCShop npcShop504 = npcShop503.Add(obj503, conditionArray503);
    Item obj504 = new Item(4962, 1, 0);
    obj504.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray504 = Array.Empty<Condition>();
    NPCShop npcShop505 = npcShop504.Add(obj504, conditionArray504);
    Item obj505 = new Item(131, 1, 0);
    obj505.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray505 = Array.Empty<Condition>();
    NPCShop npcShop506 = npcShop505.Add(obj505, conditionArray505);
    Item obj506 = new Item(607, 1, 0);
    obj506.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray506 = Array.Empty<Condition>();
    NPCShop npcShop507 = npcShop506.Add(obj506, conditionArray506);
    Item obj507 = new Item(594, 1, 0);
    obj507.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray507 = Array.Empty<Condition>();
    NPCShop npcShop508 = npcShop507.Add(obj507, conditionArray507);
    Item obj508 = new Item(883, 1, 0);
    obj508.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray508 = Array.Empty<Condition>();
    NPCShop npcShop509 = npcShop508.Add(obj508, conditionArray508);
    Item obj509 = new Item(414, 1, 0);
    obj509.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray509 = Array.Empty<Condition>();
    NPCShop npcShop510 = npcShop509.Add(obj509, conditionArray509);
    Item obj510 = new Item(413, 1, 0);
    obj510.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray510 = Array.Empty<Condition>();
    NPCShop npcShop511 = npcShop510.Add(obj510, conditionArray510);
    Item obj511 = new Item(192 /*0xC0*/, 1, 0);
    obj511.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray511 = Array.Empty<Condition>();
    NPCShop npcShop512 = npcShop511.Add(obj511, conditionArray511);
    Item obj512 = new Item(609, 1, 0);
    obj512.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray512 = Array.Empty<Condition>();
    NPCShop npcShop513 = npcShop512.Add(obj512, conditionArray512);
    Item obj513 = new Item(4050, 1, 0);
    obj513.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray513 = Array.Empty<Condition>();
    NPCShop npcShop514 = npcShop513.Add(obj513, conditionArray513);
    Item obj514 = new Item(412, 1, 0);
    obj514.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray514 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop515 = npcShop514.Add(obj514, conditionArray514);
    Item obj515 = new Item(662, 1, 0);
    obj515.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray515 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop516 = npcShop515.Add(obj515, conditionArray515);
    Item obj516 = new Item(145, 1, 0);
    obj516.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray516 = Array.Empty<Condition>();
    NPCShop npcShop517 = npcShop516.Add(obj516, conditionArray516);
    Item obj517 = new Item(717, 1, 0);
    obj517.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray517 = Array.Empty<Condition>();
    NPCShop npcShop518 = npcShop517.Add(obj517, conditionArray517);
    Item obj518 = new Item(3951, 1, 0);
    obj518.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray518 = Array.Empty<Condition>();
    NPCShop npcShop519 = npcShop518.Add(obj518, conditionArray518);
    Item obj519 = new Item(3953, 1, 0);
    obj519.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray519 = Array.Empty<Condition>();
    NPCShop npcShop520 = npcShop519.Add(obj519, conditionArray519);
    Item obj520 = new Item(143, 1, 0);
    obj520.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray520 = Array.Empty<Condition>();
    NPCShop npcShop521 = npcShop520.Add(obj520, conditionArray520);
    Item obj521 = new Item(718, 1, 0);
    obj521.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray521 = Array.Empty<Condition>();
    NPCShop npcShop522 = npcShop521.Add(obj521, conditionArray521);
    Item obj522 = new Item(141, 1, 0);
    obj522.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray522 = Array.Empty<Condition>();
    NPCShop npcShop523 = npcShop522.Add(obj522, conditionArray522);
    Item obj523 = new Item(719, 1, 0);
    obj523.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray523 = Array.Empty<Condition>();
    NPCShop npcShop524 = npcShop523.Add(obj523, conditionArray523);
    Item obj524 = new Item(577, 1, 0);
    obj524.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray524 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop525 = npcShop524.Add(obj524, conditionArray524);
    Item obj525 = new Item(2793, 1, 0);
    obj525.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray525 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop526 = npcShop525.Add(obj525, conditionArray525);
    Item obj526 = new Item(3100, 1, 0);
    obj526.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray526 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop527 = npcShop526.Add(obj526, conditionArray526);
    Item obj527 = new Item(214, 1, 0);
    obj527.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray527 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop528 = npcShop527.Add(obj527, conditionArray527);
    Item obj528 = new Item(415, 1, 0);
    obj528.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray528 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop529 = npcShop528.Add(obj528, conditionArray528);
    Item obj529 = new Item(1589, 1, 0);
    obj529.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray529 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop530 = npcShop529.Add(obj529, conditionArray529);
    Item obj530 = new Item(416, 1, 0);
    obj530.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray530 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop531 = npcShop530.Add(obj530, conditionArray530);
    Item obj531 = new Item(1591, 1, 0);
    obj531.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray531 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop532 = npcShop531.Add(obj531, conditionArray531);
    Item obj532 = new Item(604, 1, 0);
    obj532.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray532 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop533 = npcShop532.Add(obj532, conditionArray532);
    Item obj533 = new Item(1593, 1, 0);
    obj533.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray533 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop534 = npcShop533.Add(obj533, conditionArray533);
    Item obj534 = new Item(2792, 1, 0);
    obj534.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray534 = new Condition[1]
    {
      Condition.DownedMechBossAll
    };
    NPCShop npcShop535 = npcShop534.Add(obj534, conditionArray534);
    Item obj535 = new Item(2794, 1, 0);
    obj535.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray535 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop536 = npcShop535.Add(obj535, conditionArray535);
    Item obj536 = new Item(3461, 1, 0);
    obj536.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray536 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop537 = npcShop536.Add(obj536, conditionArray536);
    Item obj537 = new Item(5398, 1, 0);
    obj537.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray537 = Array.Empty<Condition>();
    NPCShop npcShop538 = npcShop537.Add(obj537, conditionArray537);
    Item obj538 = new Item(5439, 1, 0);
    obj538.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray538 = Array.Empty<Condition>();
    NPCShop npcShop539 = npcShop538.Add(obj538, conditionArray538);
    Item obj539 = new Item(5441, 1, 0);
    obj539.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray539 = Array.Empty<Condition>();
    NPCShop npcShop540 = npcShop539.Add(obj539, conditionArray539);
    Item obj540 = new Item(5442, 1, 0);
    obj540.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray540 = Array.Empty<Condition>();
    NPCShop npcShop541 = npcShop540.Add(obj540, conditionArray540);
    Item obj541 = new Item(5440, 1, 0);
    obj541.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray541 = Array.Empty<Condition>();
    NPCShop npcShop542 = npcShop541.Add(obj541, conditionArray541);
    Item obj542 = new Item(5443, 1, 0);
    obj542.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray542 = Array.Empty<Condition>();
    NPCShop npcShop543 = npcShop542.Add(obj542, conditionArray542);
    Item obj543 = new Item(5444, 1, 0);
    obj543.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray543 = Array.Empty<Condition>();
    NPCShop npcShop544 = npcShop543.Add(obj543, conditionArray543);
    Item obj544 = new Item(134, 1, 0);
    obj544.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray544 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop545 = npcShop544.Add(obj544, conditionArray544);
    Item obj545 = new Item(137, 1, 0);
    obj545.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray545 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop546 = npcShop545.Add(obj545, conditionArray545);
    Item obj546 = new Item(139, 1, 0);
    obj546.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray546 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop547 = npcShop546.Add(obj546, conditionArray546);
    Item obj547 = new Item(1101, 1, 0);
    obj547.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray547 = new Condition[1]
    {
      Condition.DownedGolem
    };
    NPCShop npcShop548 = npcShop547.Add(obj547, conditionArray547);
    Item obj548 = new Item(4231, 1, 0);
    obj548.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray548 = new Condition[1]
    {
      Condition.DownedNebulaPillar
    };
    NPCShop npcShop549 = npcShop548.Add(obj548, conditionArray548);
    Item obj549 = new Item(4229, 1, 0);
    obj549.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray549 = new Condition[1]
    {
      Condition.DownedSolarPillar
    };
    NPCShop npcShop550 = npcShop549.Add(obj549, conditionArray549);
    Item obj550 = new Item(4232, 1, 0);
    obj550.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray550 = new Condition[1]
    {
      Condition.DownedStardustPillar
    };
    NPCShop npcShop551 = npcShop550.Add(obj550, conditionArray550);
    Item obj551 = new Item(4230, 1, 0);
    obj551.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray551 = new Condition[1]
    {
      Condition.DownedVortexPillar
    };
    NPCShop npcShop552 = npcShop551.Add(obj551, conditionArray551);
    Item obj552 = new Item(614, 1, 0);
    obj552.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray552 = Array.Empty<Condition>();
    NPCShop npcShop553 = npcShop552.Add(obj552, conditionArray552);
    Item obj553 = new Item(613, 1, 0);
    obj553.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray553 = Array.Empty<Condition>();
    NPCShop npcShop554 = npcShop553.Add(obj553, conditionArray553);
    Item obj554 = new Item(611, 1, 0);
    obj554.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray554 = Array.Empty<Condition>();
    NPCShop npcShop555 = npcShop554.Add(obj554, conditionArray554);
    Item obj555 = new Item(612, 1, 0);
    obj555.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray555 = Array.Empty<Condition>();
    NPCShop npcShop556 = npcShop555.Add(obj555, conditionArray555);
    Item obj556 = new Item(3087, 1, 0);
    obj556.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray556 = Array.Empty<Condition>();
    NPCShop npcShop557 = npcShop556.Add(obj556, conditionArray556);
    Item obj557 = new Item(3066, 1, 0);
    obj557.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray557 = Array.Empty<Condition>();
    NPCShop npcShop558 = npcShop557.Add(obj557, conditionArray557);
    Item obj558 = new Item(824, 1, 0);
    obj558.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray558 = Array.Empty<Condition>();
    NPCShop npcShop559 = npcShop558.Add(obj558, conditionArray558);
    Item obj559 = new Item(4392, 1, 0);
    obj559.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray559 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop560 = npcShop559.Add(obj559, conditionArray559);
    Item obj560 = new Item(2860, 1, 0);
    obj560.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray560 = new Condition[1]
    {
      Condition.DownedMartians
    };
    NPCShop npcShop561 = npcShop560.Add(obj560, conditionArray560);
    Item obj561 = new Item(170, 1, 0);
    obj561.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray561 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop562 = npcShop561.Add(obj561, conditionArray561);
    Item obj562 = new Item(1976, 1, 0);
    obj562.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray562 = Array.Empty<Condition>();
    NPCShop npcShop563 = npcShop562.Add(obj562, conditionArray562);
    Item obj563 = new Item(1970, 1, 0);
    obj563.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray563 = Array.Empty<Condition>();
    NPCShop npcShop564 = npcShop563.Add(obj563, conditionArray563);
    Item obj564 = new Item(1975, 1, 0);
    obj564.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray564 = Array.Empty<Condition>();
    NPCShop npcShop565 = npcShop564.Add(obj564, conditionArray564);
    Item obj565 = new Item(1973, 1, 0);
    obj565.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray565 = Array.Empty<Condition>();
    NPCShop npcShop566 = npcShop565.Add(obj565, conditionArray565);
    Item obj566 = new Item(1974, 1, 0);
    obj566.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray566 = Array.Empty<Condition>();
    NPCShop npcShop567 = npcShop566.Add(obj566, conditionArray566);
    Item obj567 = new Item(1972, 1, 0);
    obj567.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray567 = Array.Empty<Condition>();
    NPCShop npcShop568 = npcShop567.Add(obj567, conditionArray567);
    Item obj568 = new Item(1971, 1, 0);
    obj568.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray568 = Array.Empty<Condition>();
    NPCShop npcShop569 = npcShop568.Add(obj568, conditionArray568);
    Item obj569 = new Item(3621, 1, 0);
    obj569.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray569 = Array.Empty<Condition>();
    NPCShop npcShop570 = npcShop569.Add(obj569, conditionArray569);
    Item obj570 = new Item(3633, 1, 0);
    obj570.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray570 = Array.Empty<Condition>();
    NPCShop npcShop571 = npcShop570.Add(obj570, conditionArray570);
    Item obj571 = new Item(3634, 1, 0);
    obj571.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray571 = Array.Empty<Condition>();
    NPCShop npcShop572 = npcShop571.Add(obj571, conditionArray571);
    Item obj572 = new Item(3635, 1, 0);
    obj572.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray572 = Array.Empty<Condition>();
    NPCShop npcShop573 = npcShop572.Add(obj572, conditionArray572);
    Item obj573 = new Item(3636, 1, 0);
    obj573.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray573 = Array.Empty<Condition>();
    NPCShop npcShop574 = npcShop573.Add(obj573, conditionArray573);
    Item obj574 = new Item(3637, 1, 0);
    obj574.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray574 = Array.Empty<Condition>();
    NPCShop npcShop575 = npcShop574.Add(obj574, conditionArray574);
    Item obj575 = new Item(3575, 1, 0);
    obj575.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray575 = new Condition[1]
    {
      Condition.DownedNebulaPillar
    };
    NPCShop npcShop576 = npcShop575.Add(obj575, conditionArray575);
    Item obj576 = new Item(3573, 1, 0);
    obj576.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray576 = new Condition[1]
    {
      Condition.DownedSolarPillar
    };
    NPCShop npcShop577 = npcShop576.Add(obj576, conditionArray576);
    Item obj577 = new Item(3576, 1, 0);
    obj577.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray577 = new Condition[1]
    {
      Condition.DownedStardustPillar
    };
    NPCShop npcShop578 = npcShop577.Add(obj577, conditionArray577);
    Item obj578 = new Item(3574, 1, 0);
    obj578.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray578 = new Condition[1]
    {
      Condition.DownedVortexPillar
    };
    NPCShop npcShop579 = npcShop578.Add(obj578, conditionArray578);
    Item obj579 = new Item(2261, 1, 0);
    obj579.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray579 = Array.Empty<Condition>();
    NPCShop npcShop580 = npcShop579.Add(obj579, conditionArray579);
    Item obj580 = new Item(2262, 1, 0);
    obj580.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray580 = Array.Empty<Condition>();
    NPCShop npcShop581 = npcShop580.Add(obj580, conditionArray580);
    Item obj581 = new Item(586, 1, 0);
    obj581.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray581 = Array.Empty<Condition>();
    NPCShop npcShop582 = npcShop581.Add(obj581, conditionArray581);
    Item obj582 = new Item(591, 1, 0);
    obj582.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray582 = Array.Empty<Condition>();
    NPCShop npcShop583 = npcShop582.Add(obj582, conditionArray582);
    Item obj583 = new Item(775, 1, 0);
    obj583.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray583 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop584 = npcShop583.Add(obj583, conditionArray583);
    Item obj584 = new Item(2435, 1, 0);
    obj584.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray584 = Array.Empty<Condition>();
    NPCShop npcShop585 = npcShop584.Add(obj584, conditionArray584);
    Item obj585 = new Item(763, 1, 0);
    obj585.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray585 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop586 = npcShop585.Add(obj585, conditionArray585);
    Item obj586 = new Item(3955, 1, 0);
    obj586.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray586 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop587 = npcShop586.Add(obj586, conditionArray586);
    Item obj587 = new Item(4139, 1, 0);
    obj587.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray587 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop588 = npcShop587.Add(obj587, conditionArray587);
    Item obj588 = new Item(762, 1, 0);
    obj588.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray588 = new Condition[1]
    {
      Condition.DownedKingSlime
    };
    NPCShop npcShop589 = npcShop588.Add(obj588, conditionArray588);
    Item obj589 = new Item(3113, 1, 0);
    obj589.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray589 = new Condition[1]
    {
      Condition.DownedKingSlime
    };
    NPCShop npcShop590 = npcShop589.Add(obj589, conditionArray589);
    Item obj590 = new Item(2701, 1, 0);
    obj590.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray590 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop591 = npcShop590.Add(obj590, conditionArray590);
    Item obj591 = new Item(2751, 1, 0);
    obj591.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray591 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop592 = npcShop591.Add(obj591, conditionArray591);
    Item obj592 = new Item(2752, 1, 0);
    obj592.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray592 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop593 = npcShop592.Add(obj592, conditionArray592);
    Item obj593 = new Item(2753, 1, 0);
    obj593.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray593 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop594 = npcShop593.Add(obj593, conditionArray593);
    Item obj594 = new Item(2754, 1, 0);
    obj594.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray594 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop595 = npcShop594.Add(obj594, conditionArray594);
    Item obj595 = new Item(2755, 1, 0);
    obj595.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray595 = new Condition[1]
    {
      Condition.Hardmode
    };
    ((AbstractNPCShop) npcShop595.Add(obj595, conditionArray595)).Register();
    NPCShop npcShop596 = new NPCShop(this.Type, "Herbs & Plants");
    Item obj596 = new Item(3093, 1, 0);
    obj596.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray596 = Array.Empty<Condition>();
    NPCShop npcShop597 = npcShop596.Add(obj596, conditionArray596);
    Item obj597 = new Item(315, 1, 0);
    obj597.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray597 = Array.Empty<Condition>();
    NPCShop npcShop598 = npcShop597.Add(obj597, conditionArray597);
    Item obj598 = new Item(313, 1, 0);
    obj598.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray598 = Array.Empty<Condition>();
    NPCShop npcShop599 = npcShop598.Add(obj598, conditionArray598);
    Item obj599 = new Item(307, 1, 0);
    obj599.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray599 = Array.Empty<Condition>();
    NPCShop npcShop600 = npcShop599.Add(obj599, conditionArray599);
    Item obj600 = new Item(316, 1, 0);
    obj600.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray600 = Array.Empty<Condition>();
    NPCShop npcShop601 = npcShop600.Add(obj600, conditionArray600);
    Item obj601 = new Item(310, 1, 0);
    obj601.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray601 = Array.Empty<Condition>();
    NPCShop npcShop602 = npcShop601.Add(obj601, conditionArray601);
    Item obj602 = new Item(318, 1, 0);
    obj602.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray602 = Array.Empty<Condition>();
    NPCShop npcShop603 = npcShop602.Add(obj602, conditionArray602);
    Item obj603 = new Item(312, 1, 0);
    obj603.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray603 = Array.Empty<Condition>();
    NPCShop npcShop604 = npcShop603.Add(obj603, conditionArray603);
    Item obj604 = new Item(314, 1, 0);
    obj604.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray604 = Array.Empty<Condition>();
    NPCShop npcShop605 = npcShop604.Add(obj604, conditionArray604);
    Item obj605 = new Item(308, 1, 0);
    obj605.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray605 = Array.Empty<Condition>();
    NPCShop npcShop606 = npcShop605.Add(obj605, conditionArray605);
    Item obj606 = new Item(2358, 1, 0);
    obj606.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray606 = Array.Empty<Condition>();
    NPCShop npcShop607 = npcShop606.Add(obj606, conditionArray606);
    Item obj607 = new Item(2357, 1, 0);
    obj607.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray607 = Array.Empty<Condition>();
    NPCShop npcShop608 = npcShop607.Add(obj607, conditionArray607);
    Item obj608 = new Item(317, 1, 0);
    obj608.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray608 = Array.Empty<Condition>();
    NPCShop npcShop609 = npcShop608.Add(obj608, conditionArray608);
    Item obj609 = new Item(311, 1, 0);
    obj609.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray609 = Array.Empty<Condition>();
    NPCShop npcShop610 = npcShop609.Add(obj609, conditionArray609);
    Item obj610 = new Item(1828, 1, 0);
    obj610.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray610 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop611 = npcShop610.Add(obj610, conditionArray610);
    Item obj611 = new Item(1107, 1, 0);
    obj611.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray611 = Array.Empty<Condition>();
    NPCShop npcShop612 = npcShop611.Add(obj611, conditionArray611);
    Item obj612 = new Item(1108, 1, 0);
    obj612.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray612 = Array.Empty<Condition>();
    NPCShop npcShop613 = npcShop612.Add(obj612, conditionArray612);
    Item obj613 = new Item(1109, 1, 0);
    obj613.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray613 = Array.Empty<Condition>();
    NPCShop npcShop614 = npcShop613.Add(obj613, conditionArray613);
    Item obj614 = new Item(1110, 1, 0);
    obj614.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray614 = Array.Empty<Condition>();
    NPCShop npcShop615 = npcShop614.Add(obj614, conditionArray614);
    Item obj615 = new Item(1111, 1, 0);
    obj615.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray615 = Array.Empty<Condition>();
    NPCShop npcShop616 = npcShop615.Add(obj615, conditionArray615);
    Item obj616 = new Item(1112, 1, 0);
    obj616.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray616 = Array.Empty<Condition>();
    NPCShop npcShop617 = npcShop616.Add(obj616, conditionArray616);
    Item obj617 = new Item(1113, 1, 0);
    obj617.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray617 = Array.Empty<Condition>();
    NPCShop npcShop618 = npcShop617.Add(obj617, conditionArray617);
    Item obj618 = new Item(1114, 1, 0);
    obj618.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray618 = Array.Empty<Condition>();
    NPCShop npcShop619 = npcShop618.Add(obj618, conditionArray618);
    Item obj619 = new Item(60, 1, 0);
    obj619.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray619 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop620 = npcShop619.Add(obj619, conditionArray619);
    Item obj620 = new Item(2887, 1, 0);
    obj620.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray620 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop621 = npcShop620.Add(obj620, conditionArray620);
    Item obj621 = new Item(5, 1, 0);
    obj621.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray621 = Array.Empty<Condition>();
    NPCShop npcShop622 = npcShop621.Add(obj621, conditionArray621);
    Item obj622 = new Item(183, 1, 0);
    obj622.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray622 = new Condition[1]
    {
      ModConditions.HasBeenToMushroom
    };
    NPCShop npcShop623 = npcShop622.Add(obj622, conditionArray622);
    Item obj623 = new Item(62, 1, 0);
    obj623.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray623 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop624 = npcShop623.Add(obj623, conditionArray623);
    Item obj624 = new Item(195, 1, 0);
    obj624.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray624 = Array.Empty<Condition>();
    NPCShop npcShop625 = npcShop624.Add(obj624, conditionArray624);
    Item obj625 = new Item(194, 1, 0);
    obj625.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray625 = Array.Empty<Condition>();
    NPCShop npcShop626 = npcShop625.Add(obj625, conditionArray625);
    Item obj626 = new Item(59, 1, 0);
    obj626.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray626 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop627 = npcShop626.Add(obj626, conditionArray626);
    Item obj627 = new Item(2171, 1, 0);
    obj627.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray627 = new Condition[1]
    {
      Condition.DownedEyeOfCthulhu
    };
    NPCShop npcShop628 = npcShop627.Add(obj627, conditionArray627);
    Item obj628 = new Item(5214, 1, 0);
    obj628.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray628 = Array.Empty<Condition>();
    NPCShop npcShop629 = npcShop628.Add(obj628, conditionArray628);
    Item obj629 = new Item(369, 1, 0);
    obj629.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray629 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop630 = npcShop629.Add(obj629, conditionArray629);
    Item obj630 = new Item(4352, 1, 0);
    obj630.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray630 = Array.Empty<Condition>();
    NPCShop npcShop631 = npcShop630.Add(obj630, conditionArray630);
    Item obj631 = new Item(4350, 1, 0);
    obj631.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray631 = Array.Empty<Condition>();
    NPCShop npcShop632 = npcShop631.Add(obj631, conditionArray631);
    Item obj632 = new Item(4349, 1, 0);
    obj632.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray632 = Array.Empty<Condition>();
    NPCShop npcShop633 = npcShop632.Add(obj632, conditionArray632);
    Item obj633 = new Item(4354, 1, 0);
    obj633.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray633 = Array.Empty<Condition>();
    NPCShop npcShop634 = npcShop633.Add(obj633, conditionArray633);
    Item obj634 = new Item(4353, 1, 0);
    obj634.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray634 = Array.Empty<Condition>();
    NPCShop npcShop635 = npcShop634.Add(obj634, conditionArray634);
    Item obj635 = new Item(4351, 1, 0);
    obj635.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray635 = Array.Empty<Condition>();
    NPCShop npcShop636 = npcShop635.Add(obj635, conditionArray635);
    Item obj636 = new Item(4389, 1, 0);
    obj636.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray636 = Array.Empty<Condition>();
    NPCShop npcShop637 = npcShop636.Add(obj636, conditionArray636);
    Item obj637 = new Item(4377, 1, 0);
    obj637.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray637 = Array.Empty<Condition>();
    NPCShop npcShop638 = npcShop637.Add(obj637, conditionArray637);
    Item obj638 = new Item(5127, 1, 0);
    obj638.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray638 = Array.Empty<Condition>();
    NPCShop npcShop639 = npcShop638.Add(obj638, conditionArray638);
    Item obj639 = new Item(4378, 1, 0);
    obj639.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray639 = Array.Empty<Condition>();
    NPCShop npcShop640 = npcShop639.Add(obj639, conditionArray639);
    Item obj640 = new Item(5128, 1, 0);
    obj640.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray640 = Array.Empty<Condition>();
    NPCShop npcShop641 = npcShop640.Add(obj640, conditionArray640);
    Item obj641 = new Item(275, 1, 0);
    obj641.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray641 = Array.Empty<Condition>();
    NPCShop npcShop642 = npcShop641.Add(obj641, conditionArray641);
    Item obj642 = new Item(2626, 1, 0);
    obj642.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray642 = Array.Empty<Condition>();
    NPCShop npcShop643 = npcShop642.Add(obj642, conditionArray642);
    Item obj643 = new Item(2625, 1, 0);
    obj643.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray643 = Array.Empty<Condition>();
    NPCShop npcShop644 = npcShop643.Add(obj643, conditionArray643);
    Item obj644 = new Item(4071, 1, 0);
    obj644.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray644 = Array.Empty<Condition>();
    NPCShop npcShop645 = npcShop644.Add(obj644, conditionArray644);
    Item obj645 = new Item(4072, 1, 0);
    obj645.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray645 = Array.Empty<Condition>();
    NPCShop npcShop646 = npcShop645.Add(obj645, conditionArray645);
    Item obj646 = new Item(4073, 1, 0);
    obj646.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray646 = Array.Empty<Condition>();
    NPCShop npcShop647 = npcShop646.Add(obj646, conditionArray646);
    Item obj647 = new Item(27, 1, 0);
    obj647.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray647 = Array.Empty<Condition>();
    NPCShop npcShop648 = npcShop647.Add(obj647, conditionArray647);
    Item obj648 = new Item(5438, 1, 0);
    obj648.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
    Condition[] conditionArray648 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    ((AbstractNPCShop) npcShop648.Add(obj648, conditionArray648)).Register();
    NPCShop npcShop649 = new NPCShop(this.Type, "Fish & Fishing Gear");
    Item obj649 = new Item(3721, 1, 0);
    obj649.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray649 = new Condition[2]
    {
      Condition.NotDownedEowOrBoc,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop650 = npcShop649.Add(obj649, conditionArray649);
    Item obj650 = new Item(5064, 1, 0);
    obj650.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray650 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop651 = npcShop650.Add(obj650, conditionArray650);
    Item obj651 = new Item(5139, 1, 0);
    obj651.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray651 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop652 = npcShop651.Add(obj651, conditionArray651);
    Item obj652 = new Item(2367, 1, 0);
    obj652.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray652 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop653 = npcShop652.Add(obj652, conditionArray652);
    Item obj653 = new Item(2368, 1, 0);
    obj653.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray653 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop654 = npcShop653.Add(obj653, conditionArray653);
    Item obj654 = new Item(2369, 1, 0);
    obj654.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray654 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop655 = npcShop654.Add(obj654, conditionArray654);
    Item obj655 = new Item(4325, 1, 0);
    obj655.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray655 = new Condition[2]
    {
      ModConditions.DownedBloodMoon,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop656 = npcShop655.Add(obj655, conditionArray655);
    Item obj656 = new Item(2422, 1, 0);
    obj656.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray656 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop657 = npcShop656.Add(obj656, conditionArray656);
    Item obj657 = new Item(2294, 1, 0);
    obj657.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray657 = new Condition[2]
    {
      Condition.AnglerQuestsFinishedOver(1),
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop658 = npcShop657.Add(obj657, conditionArray657);
    Item obj658 = new Item(3183, 1, 0);
    obj658.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray658 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop659 = npcShop658.Add(obj658, conditionArray658);
    Item obj659 = new Item(4608, 1, 0);
    obj659.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray659 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop660 = npcShop659.Add(obj659, conditionArray659);
    Item obj660 = new Item(2303 /*0x08FF*/, 1, 0);
    obj660.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray660 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop661 = npcShop660.Add(obj660, conditionArray660);
    Item obj661 = new Item(2299, 1, 0);
    obj661.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray661 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop662 = npcShop661.Add(obj661, conditionArray661);
    Item obj662 = new Item(2290, 1, 0);
    obj662.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray662 = Array.Empty<Condition>();
    NPCShop npcShop663 = npcShop662.Add(obj662, conditionArray662);
    Item obj663 = new Item(2317, 1, 0);
    obj663.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray663 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop664 = npcShop663.Add(obj663, conditionArray663);
    Item obj664 = new Item(2305, 1, 0);
    obj664.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray664 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop665 = npcShop664.Add(obj664, conditionArray664);
    Item obj665 = new Item(2304 /*0x0900*/, 1, 0);
    obj665.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray665 = new Condition[1]
    {
      ModConditions.HasBeenToSky
    };
    NPCShop npcShop666 = npcShop665.Add(obj665, conditionArray665);
    Item obj666 = new Item(2313, 1, 0);
    obj666.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray666 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop667 = npcShop666.Add(obj666, conditionArray666);
    Item obj667 = new Item(2318, 1, 0);
    obj667.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray667 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop668 = npcShop667.Add(obj667, conditionArray667);
    Item obj668 = new Item(2312, 1, 0);
    obj668.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray668 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop669 = npcShop668.Add(obj668, conditionArray668);
    Item obj669 = new Item(4401, 1, 0);
    obj669.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray669 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop670 = npcShop669.Add(obj669, conditionArray669);
    Item obj670 = new Item(2306, 1, 0);
    obj670.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray670 = new Condition[1]
    {
      ModConditions.HasBeenToSnow
    };
    NPCShop npcShop671 = npcShop670.Add(obj670, conditionArray670);
    Item obj671 = new Item(2308, 1, 0);
    obj671.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray671 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop672 = npcShop671.Add(obj671, conditionArray671);
    Item obj672 = new Item(2319, 1, 0);
    obj672.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray672 = new Condition[1]
    {
      ModConditions.HasBeenToEvil
    };
    NPCShop npcShop673 = npcShop672.Add(obj672, conditionArray672);
    Item obj673 = new Item(2314, 1, 0);
    obj673.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray673 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop674 = npcShop673.Add(obj673, conditionArray673);
    Item obj674 = new Item(2302, 1, 0);
    obj674.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray674 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop675 = npcShop674.Add(obj674, conditionArray674);
    Item obj675 = new Item(2315, 1, 0);
    obj675.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray675 = new Condition[2]
    {
      Condition.DownedEowOrBoc,
      ModConditions.HasBeenToUnderworld
    };
    NPCShop npcShop676 = npcShop675.Add(obj675, conditionArray675);
    Item obj676 = new Item(2307, 1, 0);
    obj676.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray676 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop677 = npcShop676.Add(obj676, conditionArray676);
    Item obj677 = new Item(2310, 1, 0);
    obj677.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray677 = new Condition[2]
    {
      Condition.Hardmode,
      ModConditions.HasBeenToHallow
    };
    NPCShop npcShop678 = npcShop677.Add(obj677, conditionArray677);
    Item obj678 = new Item(2301, 1, 0);
    obj678.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray678 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop679 = npcShop678.Add(obj678, conditionArray678);
    Item obj679 = new Item(4402, 1, 0);
    obj679.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray679 = new Condition[1]
    {
      ModConditions.HasBeenToDesert
    };
    NPCShop npcShop680 = npcShop679.Add(obj679, conditionArray679);
    Item obj680 = new Item(2298, 1, 0);
    obj680.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray680 = Array.Empty<Condition>();
    NPCShop npcShop681 = npcShop680.Add(obj680, conditionArray680);
    Item obj681 = new Item(2316, 1, 0);
    obj681.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray681 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop682 = npcShop681.Add(obj681, conditionArray681);
    Item obj682 = new Item(2309, 1, 0);
    obj682.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray682 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop683 = npcShop682.Add(obj682, conditionArray682);
    Item obj683 = new Item(2321, 1, 0);
    obj683.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray683 = new Condition[1]
    {
      ModConditions.HasBeenToCavernsOrUnderground
    };
    NPCShop npcShop684 = npcShop683.Add(obj683, conditionArray683);
    Item obj684 = new Item(2297, 1, 0);
    obj684.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray684 = Array.Empty<Condition>();
    NPCShop npcShop685 = npcShop684.Add(obj684, conditionArray684);
    Item obj685 = new Item(2300, 1, 0);
    obj685.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray685 = Array.Empty<Condition>();
    NPCShop npcShop686 = npcShop685.Add(obj685, conditionArray685);
    Item obj686 = new Item(2311, 1, 0);
    obj686.shopCustomPrice = new int?(Item.buyPrice(0, 0, 10, 0));
    Condition[] conditionArray686 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop687 = npcShop686.Add(obj686, conditionArray686);
    Item obj687 = new Item(2674, 1, 0);
    obj687.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray687 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop688 = npcShop687.Add(obj687, conditionArray687);
    Item obj688 = new Item(2675, 1, 0);
    obj688.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray688 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop689 = npcShop688.Add(obj688, conditionArray688);
    Item obj689 = new Item(2676, 1, 0);
    obj689.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray689 = new Condition[1]
    {
      ModConditions.HasBeenToOcean
    };
    NPCShop npcShop690 = npcShop689.Add(obj689, conditionArray689);
    Item obj690 = new Item(4334, 1, 0);
    obj690.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray690 = Array.Empty<Condition>();
    NPCShop npcShop691 = npcShop690.Add(obj690, conditionArray690);
    Item obj691 = new Item(2156, 1, 0);
    obj691.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray691 = Array.Empty<Condition>();
    NPCShop npcShop692 = npcShop691.Add(obj691, conditionArray691);
    Item obj692 = new Item(4335, 1, 0);
    obj692.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray692 = Array.Empty<Condition>();
    NPCShop npcShop693 = npcShop692.Add(obj692, conditionArray692);
    Item obj693 = new Item(2436, 1, 0);
    obj693.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray693 = Array.Empty<Condition>();
    NPCShop npcShop694 = npcShop693.Add(obj693, conditionArray693);
    Item obj694 = new Item(3194, 1, 0);
    obj694.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray694 = Array.Empty<Condition>();
    NPCShop npcShop695 = npcShop694.Add(obj694, conditionArray694);
    Item obj695 = new Item(3191, 1, 0);
    obj695.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray695 = Array.Empty<Condition>();
    NPCShop npcShop696 = npcShop695.Add(obj695, conditionArray695);
    Item obj696 = new Item(1992, 1, 0);
    obj696.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray696 = Array.Empty<Condition>();
    NPCShop npcShop697 = npcShop696.Add(obj696, conditionArray696);
    Item obj697 = new Item(2007, 1, 0);
    obj697.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray697 = Array.Empty<Condition>();
    NPCShop npcShop698 = npcShop697.Add(obj697, conditionArray697);
    Item obj698 = new Item(2891, 1, 0);
    obj698.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray698 = Array.Empty<Condition>();
    NPCShop npcShop699 = npcShop698.Add(obj698, conditionArray698);
    Item obj699 = new Item(4340, 1, 0);
    obj699.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray699 = Array.Empty<Condition>();
    NPCShop npcShop700 = npcShop699.Add(obj699, conditionArray699);
    Item obj700 = new Item(2893, 1, 0);
    obj700.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray700 = Array.Empty<Condition>();
    NPCShop npcShop701 = npcShop700.Add(obj700, conditionArray700);
    Item obj701 = new Item(4362, 1, 0);
    obj701.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray701 = Array.Empty<Condition>();
    NPCShop npcShop702 = npcShop701.Add(obj701, conditionArray701);
    Item obj702 = new Item(4419, 1, 0);
    obj702.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray702 = Array.Empty<Condition>();
    NPCShop npcShop703 = npcShop702.Add(obj702, conditionArray702);
    Item obj703 = new Item(2895, 1, 0);
    obj703.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray703 = Array.Empty<Condition>();
    NPCShop npcShop704 = npcShop703.Add(obj703, conditionArray703);
    Item obj704 = new Item(2740, 1, 0);
    obj704.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray704 = Array.Empty<Condition>();
    NPCShop npcShop705 = npcShop704.Add(obj704, conditionArray704);
    Item obj705 = new Item(4336, 1, 0);
    obj705.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray705 = Array.Empty<Condition>();
    NPCShop npcShop706 = npcShop705.Add(obj705, conditionArray705);
    Item obj706 = new Item(2437, 1, 0);
    obj706.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray706 = Array.Empty<Condition>();
    NPCShop npcShop707 = npcShop706.Add(obj706, conditionArray706);
    Item obj707 = new Item(3192, 1, 0);
    obj707.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray707 = Array.Empty<Condition>();
    NPCShop npcShop708 = npcShop707.Add(obj707, conditionArray707);
    Item obj708 = new Item(4845, 1, 0);
    obj708.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray708 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop709 = npcShop708.Add(obj708, conditionArray708);
    Item obj709 = new Item(2001, 1, 0);
    obj709.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray709 = Array.Empty<Condition>();
    NPCShop npcShop710 = npcShop709.Add(obj709, conditionArray709);
    Item obj710 = new Item(4361, 1, 0);
    obj710.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray710 = Array.Empty<Condition>();
    NPCShop npcShop711 = npcShop710.Add(obj710, conditionArray710);
    Item obj711 = new Item(4847, 1, 0);
    obj711.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray711 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop712 = npcShop711.Add(obj711, conditionArray711);
    Item obj712 = new Item(2004, 1, 0);
    obj712.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray712 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop713 = npcShop712.Add(obj712, conditionArray712);
    Item obj713 = new Item(4363, 1, 0);
    obj713.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray713 = Array.Empty<Condition>();
    NPCShop npcShop714 = npcShop713.Add(obj713, conditionArray713);
    Item obj714 = new Item(4849, 1, 0);
    obj714.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray714 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop715 = npcShop714.Add(obj714, conditionArray714);
    Item obj715 = new Item(1994, 1, 0);
    obj715.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray715 = Array.Empty<Condition>();
    NPCShop npcShop716 = npcShop715.Add(obj715, conditionArray715);
    Item obj716 = new Item(4337, 1, 0);
    obj716.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray716 = Array.Empty<Condition>();
    NPCShop npcShop717 = npcShop716.Add(obj716, conditionArray716);
    Item obj717 = new Item(2438, 1, 0);
    obj717.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray717 = Array.Empty<Condition>();
    NPCShop npcShop718 = npcShop717.Add(obj717, conditionArray717);
    Item obj718 = new Item(1995, 1, 0);
    obj718.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray718 = Array.Empty<Condition>();
    NPCShop npcShop719 = npcShop718.Add(obj718, conditionArray718);
    Item obj719 = new Item(1996, 1, 0);
    obj719.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray719 = Array.Empty<Condition>();
    NPCShop npcShop720 = npcShop719.Add(obj719, conditionArray719);
    Item obj720 = new Item(4338, 1, 0);
    obj720.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray720 = Array.Empty<Condition>();
    NPCShop npcShop721 = npcShop720.Add(obj720, conditionArray720);
    Item obj721 = new Item(2157, 1, 0);
    obj721.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray721 = Array.Empty<Condition>();
    NPCShop npcShop722 = npcShop721.Add(obj721, conditionArray721);
    Item obj722 = new Item(3193, 1, 0);
    obj722.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray722 = Array.Empty<Condition>();
    NPCShop npcShop723 = npcShop722.Add(obj722, conditionArray722);
    Item obj723 = new Item(2006, 1, 0);
    obj723.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray723 = Array.Empty<Condition>();
    NPCShop npcShop724 = npcShop723.Add(obj723, conditionArray723);
    Item obj724 = new Item(5132, 1, 0);
    obj724.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray724 = Array.Empty<Condition>();
    NPCShop npcShop725 = npcShop724.Add(obj724, conditionArray724);
    Item obj725 = new Item(1998, 1, 0);
    obj725.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray725 = Array.Empty<Condition>();
    NPCShop npcShop726 = npcShop725.Add(obj725, conditionArray725);
    Item obj726 = new Item(1999, 1, 0);
    obj726.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray726 = Array.Empty<Condition>();
    NPCShop npcShop727 = npcShop726.Add(obj726, conditionArray726);
    Item obj727 = new Item(2673, 1, 0);
    obj727.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray727 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop728 = npcShop727.Add(obj727, conditionArray727);
    Item obj728 = new Item(1997, 1, 0);
    obj728.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray728 = Array.Empty<Condition>();
    NPCShop npcShop729 = npcShop728.Add(obj728, conditionArray728);
    Item obj729 = new Item(4418, 1, 0);
    obj729.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray729 = Array.Empty<Condition>();
    NPCShop npcShop730 = npcShop729.Add(obj729, conditionArray729);
    Item obj730 = new Item(2002, 1, 0);
    obj730.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray730 = Array.Empty<Condition>();
    NPCShop npcShop731 = npcShop730.Add(obj730, conditionArray730);
    Item obj731 = new Item(4339, 1, 0);
    obj731.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray731 = Array.Empty<Condition>();
    NPCShop npcShop732 = npcShop731.Add(obj731, conditionArray731);
    Item obj732 = new Item(2000, 1, 0);
    obj732.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray732 = Array.Empty<Condition>();
    ((AbstractNPCShop) npcShop732.Add(obj732, conditionArray732)).Register();
    NPCShop npcShop733 = new NPCShop(this.Type, "Mounts & Hooks");
    Item obj733 = new Item(2430, 1, 0);
    obj733.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray733 = new Condition[1]
    {
      Condition.DownedKingSlime
    };
    NPCShop npcShop734 = npcShop733.Add(obj733, conditionArray733);
    Item obj734 = new Item(2502, 1, 0);
    obj734.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray734 = new Condition[1]
    {
      Condition.DownedQueenBee
    };
    NPCShop npcShop735 = npcShop734.Add(obj734, conditionArray734);
    Item obj735 = new Item(2491, 1, 0);
    obj735.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray735 = Array.Empty<Condition>();
    NPCShop npcShop736 = npcShop735.Add(obj735, conditionArray735);
    Item obj736 = new Item(2428, 1, 0);
    obj736.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray736 = Array.Empty<Condition>();
    NPCShop npcShop737 = npcShop736.Add(obj736, conditionArray736);
    Item obj737 = new Item(4791, 1, 0);
    obj737.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray737 = Array.Empty<Condition>();
    NPCShop npcShop738 = npcShop737.Add(obj737, conditionArray737);
    Item obj738 = new Item(4264, 1, 0);
    obj738.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray738 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop739 = npcShop738.Add(obj738, conditionArray738);
    Item obj739 = new Item(4716, 1, 0);
    obj739.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray739 = Array.Empty<Condition>();
    NPCShop npcShop740 = npcShop739.Add(obj739, conditionArray739);
    Item obj740 = new Item(4785, 1, 0);
    obj740.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray740 = Array.Empty<Condition>();
    NPCShop npcShop741 = npcShop740.Add(obj740, conditionArray740);
    Item obj741 = new Item(4786, 1, 0);
    obj741.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray741 = Array.Empty<Condition>();
    NPCShop npcShop742 = npcShop741.Add(obj741, conditionArray741);
    Item obj742 = new Item(4787, 1, 0);
    obj742.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray742 = Array.Empty<Condition>();
    NPCShop npcShop743 = npcShop742.Add(obj742, conditionArray742);
    Item obj743 = new Item(4828, 1, 0);
    obj743.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray743 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop744 = npcShop743.Add(obj743, conditionArray743);
    Item obj744 = new Item(3771, 1, 0);
    obj744.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray744 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop745 = npcShop744.Add(obj744, conditionArray744);
    Item obj745 = new Item(5130, 1, 0);
    obj745.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray745 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop746 = npcShop745.Add(obj745, conditionArray745);
    Item obj746 = new Item(3260, 1, 0);
    obj746.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray746 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop747 = npcShop746.Add(obj746, conditionArray746);
    Item obj747 = new Item(2429, 1, 0);
    obj747.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray747 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop748 = npcShop747.Add(obj747, conditionArray747);
    Item obj748 = new Item(4981, 1, 0);
    obj748.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray748 = new Condition[1]
    {
      Condition.DownedQueenSlime
    };
    NPCShop npcShop749 = npcShop748.Add(obj748, conditionArray748);
    Item obj749 = new Item(1914, 1, 0);
    obj749.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray749 = new Condition[1]
    {
      Condition.DownedIceQueen
    };
    NPCShop npcShop750 = npcShop749.Add(obj749, conditionArray749);
    Item obj750 = new Item(2771, 1, 0);
    obj750.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray750 = new Condition[1]
    {
      Condition.DownedMartians
    };
    NPCShop npcShop751 = npcShop750.Add(obj750, conditionArray750);
    Item obj751 = new Item(2769, 1, 0);
    obj751.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray751 = new Condition[1]
    {
      Condition.DownedMartians
    };
    NPCShop npcShop752 = npcShop751.Add(obj751, conditionArray751);
    Item obj752 = new Item(4444, 1, 0);
    obj752.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray752 = new Condition[2]
    {
      Condition.DownedMourningWood,
      Condition.InExpertMode
    };
    NPCShop npcShop753 = npcShop752.Add(obj752, conditionArray752);
    Item obj753 = new Item(3367, 1, 0);
    obj753.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray753 = new Condition[2]
    {
      Condition.DownedDukeFishron,
      Condition.InExpertMode
    };
    NPCShop npcShop754 = npcShop753.Add(obj753, conditionArray753);
    Item obj754 = new Item(2768, 1, 0);
    obj754.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray754 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop755 = npcShop754.Add(obj754, conditionArray754);
    Item obj755 = new Item(4796, 1, 0);
    obj755.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray755 = new Condition[2]
    {
      Condition.DownedOldOnesArmyT1,
      Condition.InMasterMode
    };
    NPCShop npcShop756 = npcShop755.Add(obj755, conditionArray755);
    Item obj756 = new Item(4795, 1, 0);
    obj756.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray756 = new Condition[2]
    {
      Condition.Hardmode,
      Condition.InMasterMode
    };
    NPCShop npcShop757 = npcShop756.Add(obj756, conditionArray756);
    Item obj757 = new Item(4792, 1, 0);
    obj757.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray757 = new Condition[2]
    {
      Condition.DownedPirates,
      Condition.InMasterMode
    };
    NPCShop npcShop758 = npcShop757.Add(obj757, conditionArray757);
    Item obj758 = new Item(4793, 1, 0);
    obj758.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray758 = new Condition[2]
    {
      Condition.DownedMourningWood,
      Condition.InMasterMode
    };
    NPCShop npcShop759 = npcShop758.Add(obj758, conditionArray758);
    Item obj759 = new Item(4794, 1, 0);
    obj759.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray759 = new Condition[2]
    {
      Condition.DownedSantaNK1,
      Condition.InMasterMode
    };
    NPCShop npcShop760 = npcShop759.Add(obj759, conditionArray759);
    Item obj760 = new Item(84, 1, 0);
    obj760.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray760 = Array.Empty<Condition>();
    NPCShop npcShop761 = npcShop760.Add(obj760, conditionArray760);
    Item obj761 = new Item(1236, 1, 0);
    obj761.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray761 = Array.Empty<Condition>();
    NPCShop npcShop762 = npcShop761.Add(obj761, conditionArray761);
    Item obj762 = new Item(1237, 1, 0);
    obj762.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray762 = Array.Empty<Condition>();
    NPCShop npcShop763 = npcShop762.Add(obj762, conditionArray762);
    Item obj763 = new Item(1238, 1, 0);
    obj763.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray763 = Array.Empty<Condition>();
    NPCShop npcShop764 = npcShop763.Add(obj763, conditionArray763);
    Item obj764 = new Item(1239, 1, 0);
    obj764.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray764 = Array.Empty<Condition>();
    NPCShop npcShop765 = npcShop764.Add(obj764, conditionArray764);
    Item obj765 = new Item(4257, 1, 0);
    obj765.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray765 = Array.Empty<Condition>();
    NPCShop npcShop766 = npcShop765.Add(obj765, conditionArray765);
    Item obj766 = new Item(1240, 1, 0);
    obj766.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray766 = Array.Empty<Condition>();
    NPCShop npcShop767 = npcShop766.Add(obj766, conditionArray766);
    Item obj767 = new Item(1241, 1, 0);
    obj767.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray767 = Array.Empty<Condition>();
    NPCShop npcShop768 = npcShop767.Add(obj767, conditionArray767);
    Item obj768 = new Item(185, 1, 0);
    obj768.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray768 = new Condition[1]
    {
      ModConditions.HasBeenToJungle
    };
    NPCShop npcShop769 = npcShop768.Add(obj768, conditionArray768);
    Item obj769 = new Item(2585, 1, 0);
    obj769.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray769 = new Condition[1]
    {
      Condition.DownedKingSlime
    };
    NPCShop npcShop770 = npcShop769.Add(obj769, conditionArray769);
    Item obj770 = new Item(2360, 1, 0);
    obj770.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray770 = Array.Empty<Condition>();
    NPCShop npcShop771 = npcShop770.Add(obj770, conditionArray770);
    Item obj771 = new Item(4759, 1, 0);
    obj771.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray771 = Array.Empty<Condition>();
    NPCShop npcShop772 = npcShop771.Add(obj771, conditionArray771);
    Item obj772 = new Item(1800, 1, 0);
    obj772.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray772 = Array.Empty<Condition>();
    NPCShop npcShop773 = npcShop772.Add(obj772, conditionArray772);
    Item obj773 = new Item(1915, 1, 0);
    obj773.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray773 = Array.Empty<Condition>();
    NPCShop npcShop774 = npcShop773.Add(obj773, conditionArray773);
    Item obj774 = new Item(939, 1, 0);
    obj774.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray774 = Array.Empty<Condition>();
    NPCShop npcShop775 = npcShop774.Add(obj774, conditionArray774);
    Item obj775 = new Item(1273, 1, 0);
    obj775.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray775 = new Condition[1]
    {
      Condition.DownedSkeletron
    };
    NPCShop npcShop776 = npcShop775.Add(obj775, conditionArray775);
    Item obj776 = new Item(437, 1, 0);
    obj776.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray776 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop777 = npcShop776.Add(obj776, conditionArray776);
    Item obj777 = new Item(3020, 1, 0);
    obj777.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray777 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop778 = npcShop777.Add(obj777, conditionArray777);
    Item obj778 = new Item(3023, 1, 0);
    obj778.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray778 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop779 = npcShop778.Add(obj778, conditionArray778);
    Item obj779 = new Item(3022, 1, 0);
    obj779.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray779 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop780 = npcShop779.Add(obj779, conditionArray779);
    Item obj780 = new Item(4980, 1, 0);
    obj780.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray780 = new Condition[1]
    {
      Condition.DownedQueenSlime
    };
    NPCShop npcShop781 = npcShop780.Add(obj780, conditionArray780);
    Item obj781 = new Item(3623, 1, 0);
    obj781.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray781 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop782 = npcShop781.Add(obj781, conditionArray781);
    Item obj782 = new Item(3021, 1, 0);
    obj782.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray782 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop783 = npcShop782.Add(obj782, conditionArray782);
    Item obj783 = new Item(1829, 1, 0);
    obj783.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray783 = new Condition[1]
    {
      Condition.DownedMourningWood
    };
    NPCShop npcShop784 = npcShop783.Add(obj783, conditionArray783);
    Item obj784 = new Item(1916, 1, 0);
    obj784.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray784 = new Condition[1]
    {
      Condition.DownedEverscream
    };
    NPCShop npcShop785 = npcShop784.Add(obj784, conditionArray784);
    Item obj785 = new Item(2800, 1, 0);
    obj785.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray785 = new Condition[1]
    {
      Condition.DownedMartians
    };
    NPCShop npcShop786 = npcShop785.Add(obj785, conditionArray785);
    Item obj786 = new Item(3572, 1, 0);
    obj786.shopCustomPrice = new int?(Item.buyPrice(0, 2, 0, 0));
    Condition[] conditionArray786 = new Condition[1]
    {
      Condition.DownedCultist
    };
    ((AbstractNPCShop) npcShop786.Add(obj786, conditionArray786)).Register();
    NPCShop npcShop787 = new NPCShop(this.Type, "Ammo");
    Item obj787 = new Item(97, 1, 0);
    obj787.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray787 = Array.Empty<Condition>();
    NPCShop npcShop788 = npcShop787.Add(obj787, conditionArray787);
    Item obj788 = new Item(278, 1, 0);
    obj788.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray788 = Array.Empty<Condition>();
    NPCShop npcShop789 = npcShop788.Add(obj788, conditionArray788);
    Item obj789 = new Item(4915, 1, 0);
    obj789.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray789 = Array.Empty<Condition>();
    NPCShop npcShop790 = npcShop789.Add(obj789, conditionArray789);
    Item obj790 = new Item(1349, 1, 0);
    obj790.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray790 = Array.Empty<Condition>();
    NPCShop npcShop791 = npcShop790.Add(obj790, conditionArray790);
    Item obj791 = new Item(234, 1, 0);
    obj791.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray791 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop792 = npcShop791.Add(obj791, conditionArray791);
    Item obj792 = new Item(515, 1, 0);
    obj792.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray792 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop793 = npcShop792.Add(obj792, conditionArray792);
    Item obj793 = new Item(546, 1, 0);
    obj793.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray793 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop794 = npcShop793.Add(obj793, conditionArray793);
    Item obj794 = new Item(1335, 1, 0);
    obj794.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray794 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop795 = npcShop794.Add(obj794, conditionArray794);
    Item obj795 = new Item(1302, 1, 0);
    obj795.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray795 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop796 = npcShop795.Add(obj795, conditionArray795);
    Item obj796 = new Item(1352, 1, 0);
    obj796.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray796 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop797 = npcShop796.Add(obj796, conditionArray796);
    Item obj797 = new Item(1351, 1, 0);
    obj797.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray797 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop798 = npcShop797.Add(obj797, conditionArray797);
    Item obj798 = new Item(1179, 1, 0);
    obj798.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray798 = new Condition[1]
    {
      Condition.DownedMechBossAll
    };
    NPCShop npcShop799 = npcShop798.Add(obj798, conditionArray798);
    Item obj799 = new Item(1342, 1, 0);
    obj799.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray799 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop800 = npcShop799.Add(obj799, conditionArray799);
    Item obj800 = new Item(1350, 1, 0);
    obj800.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray800 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop801 = npcShop800.Add(obj800, conditionArray800);
    Item obj801 = new Item(3567, 1, 0);
    obj801.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray801 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop802 = npcShop801.Add(obj801, conditionArray801);
    Item obj802 = new Item(3104, 1, 0);
    obj802.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray802 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop803 = npcShop802.Add(obj802, conditionArray802);
    Item obj803 = new Item(40, 1, 0);
    obj803.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray803 = Array.Empty<Condition>();
    NPCShop npcShop804 = npcShop803.Add(obj803, conditionArray803);
    Item obj804 = new Item(41, 1, 0);
    obj804.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray804 = Array.Empty<Condition>();
    NPCShop npcShop805 = npcShop804.Add(obj804, conditionArray804);
    Item obj805 = new Item(988, 1, 0);
    obj805.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray805 = Array.Empty<Condition>();
    NPCShop npcShop806 = npcShop805.Add(obj805, conditionArray805);
    Item obj806 = new Item(51, 1, 0);
    obj806.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray806 = Array.Empty<Condition>();
    NPCShop npcShop807 = npcShop806.Add(obj806, conditionArray806);
    Item obj807 = new Item(47, 1, 0);
    obj807.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray807 = Array.Empty<Condition>();
    NPCShop npcShop808 = npcShop807.Add(obj807, conditionArray807);
    Item obj808 = new Item(3003, 1, 0);
    obj808.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray808 = new Condition[1]
    {
      ModConditions.HasTalkedToSkeletonMerchant
    };
    NPCShop npcShop809 = npcShop808.Add(obj808, conditionArray808);
    Item obj809 = new Item(5348, 1, 0);
    obj809.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray809 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop810 = npcShop809.Add(obj809, conditionArray809);
    Item obj810 = new Item(265, 1, 0);
    obj810.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray810 = new Condition[1]
    {
      Condition.DownedEowOrBoc
    };
    NPCShop npcShop811 = npcShop810.Add(obj810, conditionArray810);
    Item obj811 = new Item(545, 1, 0);
    obj811.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray811 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop812 = npcShop811.Add(obj811, conditionArray811);
    Item obj812 = new Item(1334, 1, 0);
    obj812.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray812 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop813 = npcShop812.Add(obj812, conditionArray812);
    Item obj813 = new Item(516, 1, 0);
    obj813.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray813 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop814 = npcShop813.Add(obj813, conditionArray813);
    Item obj814 = new Item(1235, 1, 0);
    obj814.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray814 = new Condition[1]
    {
      Condition.DownedMechBossAll
    };
    NPCShop npcShop815 = npcShop814.Add(obj814, conditionArray814);
    Item obj815 = new Item(1341, 1, 0);
    obj815.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray815 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop816 = npcShop815.Add(obj815, conditionArray815);
    Item obj816 = new Item(3568, 1, 0);
    obj816.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray816 = new Condition[1]
    {
      Condition.DownedMoonLord
    };
    NPCShop npcShop817 = npcShop816.Add(obj816, conditionArray816);
    Item obj817 = new Item(3103, 1, 0);
    obj817.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray817 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop818 = npcShop817.Add(obj817, conditionArray817);
    Item obj818 = new Item(771, 1, 0);
    obj818.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray818 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop819 = npcShop818.Add(obj818, conditionArray818);
    Item obj819 = new Item(772, 1, 0);
    obj819.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray819 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop820 = npcShop819.Add(obj819, conditionArray819);
    Item obj820 = new Item(773, 1, 0);
    obj820.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray820 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop821 = npcShop820.Add(obj820, conditionArray820);
    Item obj821 = new Item(774, 1, 0);
    obj821.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray821 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop822 = npcShop821.Add(obj821, conditionArray821);
    Item obj822 = new Item(4459, 1, 0);
    obj822.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray822 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop823 = npcShop822.Add(obj822, conditionArray822);
    Item obj823 = new Item(4447, 1, 0);
    obj823.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray823 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop824 = npcShop823.Add(obj823, conditionArray823);
    Item obj824 = new Item(4448, 1, 0);
    obj824.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray824 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop825 = npcShop824.Add(obj824, conditionArray824);
    Item obj825 = new Item(4449, 1, 0);
    obj825.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray825 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop826 = npcShop825.Add(obj825, conditionArray825);
    Item obj826 = new Item(4457, 1, 0);
    obj826.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray826 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop827 = npcShop826.Add(obj826, conditionArray826);
    Item obj827 = new Item(4458, 1, 0);
    obj827.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray827 = new Condition[1]
    {
      Condition.DownedPlantera
    };
    NPCShop npcShop828 = npcShop827.Add(obj827, conditionArray827);
    Item obj828 = new Item(4445, 1, 0);
    obj828.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray828 = new Condition[1]
    {
      Condition.DownedMartians
    };
    NPCShop npcShop829 = npcShop828.Add(obj828, conditionArray828);
    Item obj829 = new Item(4446, 1, 0);
    obj829.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray829 = new Condition[1]
    {
      Condition.DownedMartians
    };
    NPCShop npcShop830 = npcShop829.Add(obj829, conditionArray829);
    Item obj830 = new Item(283, 1, 0);
    obj830.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray830 = Array.Empty<Condition>();
    NPCShop npcShop831 = npcShop830.Add(obj830, conditionArray830);
    Item obj831 = new Item(1310, 1, 0);
    obj831.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray831 = Array.Empty<Condition>();
    NPCShop npcShop832 = npcShop831.Add(obj831, conditionArray831);
    Item obj832 = new Item(3010, 1, 0);
    obj832.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray832 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop833 = npcShop832.Add(obj832, conditionArray832);
    Item obj833 = new Item(3011, 1, 0);
    obj833.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray833 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop834 = npcShop833.Add(obj833, conditionArray833);
    Item obj834 = new Item(3009, 1, 0);
    obj834.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray834 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop835 = npcShop834.Add(obj834, conditionArray834);
    Item obj835 = new Item(931, 1, 0);
    obj835.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray835 = Array.Empty<Condition>();
    NPCShop npcShop836 = npcShop835.Add(obj835, conditionArray835);
    Item obj836 = new Item(1614, 1, 0);
    obj836.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray836 = Array.Empty<Condition>();
    NPCShop npcShop837 = npcShop836.Add(obj836, conditionArray836);
    Item obj837 = new Item(5377, 1, 0);
    obj837.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray837 = Array.Empty<Condition>();
    NPCShop npcShop838 = npcShop837.Add(obj837, conditionArray837);
    Item obj838 = new Item(5380, 1, 0);
    obj838.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray838 = new Condition[1]
    {
      ModConditions.HasBeenToAether
    };
    NPCShop npcShop839 = npcShop838.Add(obj838, conditionArray838);
    Item obj839 = new Item(5378, 1, 0);
    obj839.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray839 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop840 = npcShop839.Add(obj839, conditionArray839);
    Item obj840 = new Item(5379, 1, 0);
    obj840.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray840 = new Condition[1]
    {
      Condition.Hardmode
    };
    NPCShop npcShop841 = npcShop840.Add(obj840, conditionArray840);
    Item obj841 = new Item(949, 1, 0);
    obj841.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray841 = Array.Empty<Condition>();
    NPCShop npcShop842 = npcShop841.Add(obj841, conditionArray841);
    Item obj842 = new Item(1261, 1, 0);
    obj842.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray842 = new Condition[1]
    {
      Condition.DownedGolem
    };
    NPCShop npcShop843 = npcShop842.Add(obj842, conditionArray842);
    Item obj843 = new Item(1783, 1, 0);
    obj843.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray843 = new Condition[1]
    {
      Condition.DownedPumpking
    };
    NPCShop npcShop844 = npcShop843.Add(obj843, conditionArray843);
    Item obj844 = new Item(1785, 1, 0);
    obj844.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray844 = new Condition[1]
    {
      Condition.DownedPumpking
    };
    NPCShop npcShop845 = npcShop844.Add(obj844, conditionArray844);
    Item obj845 = new Item(1836, 1, 0);
    obj845.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray845 = new Condition[1]
    {
      Condition.DownedMourningWood
    };
    NPCShop npcShop846 = npcShop845.Add(obj845, conditionArray845);
    Item obj846 = new Item(3108, 1, 0);
    obj846.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray846 = new Condition[1]
    {
      ModConditions.DownedEclipse
    };
    NPCShop npcShop847 = npcShop846.Add(obj846, conditionArray846);
    Item obj847 = new Item(780, 1, 0);
    obj847.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray847 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop848 = npcShop847.Add(obj847, conditionArray847);
    Item obj848 = new Item(5392, 1, 0);
    obj848.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray848 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop849 = npcShop848.Add(obj848, conditionArray848);
    Item obj849 = new Item(5393, 1, 0);
    obj849.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray849 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop850 = npcShop849.Add(obj849, conditionArray849);
    Item obj850 = new Item(5394, 1, 0);
    obj850.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray850 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop851 = npcShop850.Add(obj850, conditionArray850);
    Item obj851 = new Item(782, 1, 0);
    obj851.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray851 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop852 = npcShop851.Add(obj851, conditionArray851);
    Item obj852 = new Item(784, 1, 0);
    obj852.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray852 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop853 = npcShop852.Add(obj852, conditionArray852);
    Item obj853 = new Item(783, 1, 0);
    obj853.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray853 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    NPCShop npcShop854 = npcShop853.Add(obj853, conditionArray853);
    Item obj854 = new Item(781, 1, 0);
    obj854.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 1));
    Condition[] conditionArray854 = new Condition[1]
    {
      Condition.DownedMechBossAny
    };
    ((AbstractNPCShop) npcShop854.Add(obj854, conditionArray854)).Register();
  }
}
