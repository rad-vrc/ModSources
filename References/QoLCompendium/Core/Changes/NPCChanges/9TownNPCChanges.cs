// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.NPCChanges.ModifyShopPrices
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.NPCs;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.NPCChanges;

public class ModifyShopPrices : GlobalNPC
{
  public virtual bool InstancePerEntity => true;

  public virtual void ModifyActiveShop(NPC npc, string shopName, Item[] items)
  {
    Player localPlayer = Main.LocalPlayer;
    if (npc.type == ModContent.NPCType<BMDealerNPC>() && ((Entity) localPlayer).active && localPlayer == Main.player[Main.myPlayer])
    {
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Potions")
      {
        foreach (Item obj1 in items)
        {
          if (obj1 != null && obj1.type != 0)
          {
            Item obj2 = obj1;
            int? shopCustomPrice = obj2.shopCustomPrice;
            int potionPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.PotionPriceMultiplier;
            obj2.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * potionPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Flasks, Stations & Foods")
      {
        foreach (Item obj3 in items)
        {
          if (obj3 != null && obj3.type != 0)
          {
            if (obj3.buffType > 0)
            {
              Item obj4 = obj3;
              int? shopCustomPrice = obj4.shopCustomPrice;
              int potionPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.PotionPriceMultiplier;
              obj4.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * potionPriceMultiplier) : new int?();
            }
            else
            {
              Item obj5 = obj3;
              int? shopCustomPrice = obj5.shopCustomPrice;
              int stationPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.StationPriceMultiplier;
              obj5.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * stationPriceMultiplier) : new int?();
            }
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Materials")
      {
        foreach (Item obj6 in items)
        {
          if (obj6 != null && obj6.type != 0)
          {
            Item obj7 = obj6;
            int? shopCustomPrice = obj7.shopCustomPrice;
            int materialPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.MaterialPriceMultiplier;
            obj7.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * materialPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Movement Accessories")
      {
        foreach (Item obj8 in items)
        {
          if (obj8 != null && obj8.type != 0)
          {
            Item obj9 = obj8;
            int? shopCustomPrice = obj9.shopCustomPrice;
            int accessoryPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.AccessoryPriceMultiplier;
            obj9.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * accessoryPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Combat Accessories")
      {
        foreach (Item obj10 in items)
        {
          if (obj10 != null && obj10.type != 0)
          {
            Item obj11 = obj10;
            int? shopCustomPrice = obj11.shopCustomPrice;
            int accessoryPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.AccessoryPriceMultiplier;
            obj11.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * accessoryPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Informative/Building Gear")
      {
        foreach (Item obj12 in items)
        {
          if (obj12 != null && obj12.type != 0)
          {
            Item obj13 = obj12;
            int? shopCustomPrice = obj13.shopCustomPrice;
            int accessoryPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.AccessoryPriceMultiplier;
            obj13.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * accessoryPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Treasure Bags")
      {
        foreach (Item obj14 in items)
        {
          if (obj14 != null && obj14.type != 0)
          {
            if (NPC.downedBoss1 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj15 = obj14;
              int? shopCustomPrice = obj15.shopCustomPrice;
              obj15.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedBoss2 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj16 = obj14;
              int? shopCustomPrice = obj16.shopCustomPrice;
              obj16.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedBoss3 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj17 = obj14;
              int? shopCustomPrice = obj17.shopCustomPrice;
              obj17.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (Main.hardMode && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj18 = obj14;
              int? shopCustomPrice = obj18.shopCustomPrice;
              obj18.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 50000) : new int?();
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj19 = obj14;
              int? shopCustomPrice = obj19.shopCustomPrice;
              obj19.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedPlantBoss && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj20 = obj14;
              int? shopCustomPrice = obj20.shopCustomPrice;
              obj20.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedGolemBoss && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj21 = obj14;
              int? shopCustomPrice = obj21.shopCustomPrice;
              obj21.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedMoonlord && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj22 = obj14;
              int? shopCustomPrice = obj22.shopCustomPrice;
              obj22.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            Item obj23 = obj14;
            int? shopCustomPrice1 = obj23.shopCustomPrice;
            int bagPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.BagPriceMultiplier;
            obj23.shopCustomPrice = shopCustomPrice1.HasValue ? new int?(shopCustomPrice1.GetValueOrDefault() * bagPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Crates & Grab Bags")
      {
        foreach (Item obj24 in items)
        {
          if (obj24 != null && obj24.type != 0)
          {
            Item obj25 = obj24;
            int? shopCustomPrice = obj25.shopCustomPrice;
            int cratePriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.CratePriceMultiplier;
            obj25.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * cratePriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Ores & Bars")
      {
        foreach (Item obj26 in items)
        {
          if (obj26 != null && obj26.type != 0)
          {
            Item obj27 = obj26;
            int? shopCustomPrice = obj27.shopCustomPrice;
            int orePriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.OrePriceMultiplier;
            obj27.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * orePriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Natural Blocks")
      {
        foreach (Item obj28 in items)
        {
          if (obj28 != null && obj28.type != 0)
          {
            Item obj29 = obj28;
            int? shopCustomPrice = obj29.shopCustomPrice;
            int blockPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.NaturalBlockPriceMultiplier;
            obj29.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * blockPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Building Blocks")
      {
        foreach (Item obj30 in items)
        {
          if (obj30 != null && obj30.type != 0)
          {
            Item obj31 = obj30;
            int? shopCustomPrice = obj31.shopCustomPrice;
            int blockPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.BuildingBlockPriceMultiplier;
            obj31.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * blockPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Herbs & Plants")
      {
        foreach (Item obj32 in items)
        {
          if (obj32 != null && obj32.type != 0)
          {
            Item obj33 = obj32;
            int? shopCustomPrice = obj33.shopCustomPrice;
            int herbPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.HerbPriceMultiplier;
            obj33.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * herbPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Fish & Fishing Gear")
      {
        foreach (Item obj34 in items)
        {
          if (obj34 != null && obj34.type != 0)
          {
            Item obj35 = obj34;
            int? shopCustomPrice = obj35.shopCustomPrice;
            int fishPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.FishPriceMultiplier;
            obj35.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * fishPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Mounts & Hooks")
      {
        foreach (Item obj36 in items)
        {
          if (obj36 != null && obj36.type != 0)
          {
            Item obj37 = obj36;
            int? shopCustomPrice = obj37.shopCustomPrice;
            int mountPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.MountPriceMultiplier;
            obj37.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * mountPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Ammo")
      {
        foreach (Item obj38 in items)
        {
          if (obj38 != null && obj38.type != 0)
          {
            Item obj39 = obj38;
            int? shopCustomPrice = obj39.shopCustomPrice;
            int ammoPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.AmmoPriceMultiplier;
            obj39.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * ammoPriceMultiplier) : new int?();
          }
        }
      }
    }
    if (npc.type == ModContent.NPCType<EtherealCollectorNPC>() && ((Entity) localPlayer).active && localPlayer == Main.player[Main.myPlayer])
    {
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Potions")
      {
        foreach (Item obj40 in items)
        {
          if (obj40 != null && obj40.type != 0)
          {
            Item obj41 = obj40;
            int? shopCustomPrice = obj41.shopCustomPrice;
            int potionPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.PotionPriceMultiplier;
            obj41.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * potionPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Flasks, Stations & Foods")
      {
        foreach (Item obj42 in items)
        {
          if (obj42 != null && obj42.type != 0)
          {
            if (obj42.buffType > 0)
            {
              Item obj43 = obj42;
              int? shopCustomPrice = obj43.shopCustomPrice;
              int potionPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.PotionPriceMultiplier;
              obj43.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * potionPriceMultiplier) : new int?();
            }
            else
            {
              Item obj44 = obj42;
              int? shopCustomPrice = obj44.shopCustomPrice;
              int stationPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.StationPriceMultiplier;
              obj44.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * stationPriceMultiplier) : new int?();
            }
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Materials")
      {
        foreach (Item obj45 in items)
        {
          if (obj45 != null && obj45.type != 0)
          {
            Item obj46 = obj45;
            int? shopCustomPrice = obj46.shopCustomPrice;
            int materialPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.MaterialPriceMultiplier;
            obj46.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * materialPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Treasure Bags")
      {
        foreach (Item obj47 in items)
        {
          if (obj47 != null && obj47.type != 0)
          {
            if (NPC.downedBoss1 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj48 = obj47;
              int? shopCustomPrice = obj48.shopCustomPrice;
              obj48.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedBoss2 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj49 = obj47;
              int? shopCustomPrice = obj49.shopCustomPrice;
              obj49.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedBoss3 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj50 = obj47;
              int? shopCustomPrice = obj50.shopCustomPrice;
              obj50.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (Main.hardMode && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj51 = obj47;
              int? shopCustomPrice = obj51.shopCustomPrice;
              obj51.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 50000) : new int?();
            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj52 = obj47;
              int? shopCustomPrice = obj52.shopCustomPrice;
              obj52.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedPlantBoss && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj53 = obj47;
              int? shopCustomPrice = obj53.shopCustomPrice;
              obj53.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedGolemBoss && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj54 = obj47;
              int? shopCustomPrice = obj54.shopCustomPrice;
              obj54.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            if (NPC.downedMoonlord && QoLCompendium.QoLCompendium.shopConfig.BossScaling)
            {
              Item obj55 = obj47;
              int? shopCustomPrice = obj55.shopCustomPrice;
              obj55.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() + 25000) : new int?();
            }
            Item obj56 = obj47;
            int? shopCustomPrice2 = obj56.shopCustomPrice;
            int bagPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.BagPriceMultiplier;
            obj56.shopCustomPrice = shopCustomPrice2.HasValue ? new int?(shopCustomPrice2.GetValueOrDefault() * bagPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Crates & Grab Bags")
      {
        foreach (Item obj57 in items)
        {
          if (obj57 != null && obj57.type != 0)
          {
            Item obj58 = obj57;
            int? shopCustomPrice = obj58.shopCustomPrice;
            int cratePriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.CratePriceMultiplier;
            obj58.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * cratePriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Ores & Bars")
      {
        foreach (Item obj59 in items)
        {
          if (obj59 != null && obj59.type != 0)
          {
            Item obj60 = obj59;
            int? shopCustomPrice = obj60.shopCustomPrice;
            int orePriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.OrePriceMultiplier;
            obj60.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * orePriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Natural Blocks")
      {
        foreach (Item obj61 in items)
        {
          if (obj61 != null && obj61.type != 0)
          {
            Item obj62 = obj61;
            int? shopCustomPrice = obj62.shopCustomPrice;
            int blockPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.NaturalBlockPriceMultiplier;
            obj62.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * blockPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Building Blocks")
      {
        foreach (Item obj63 in items)
        {
          if (obj63 != null && obj63.type != 0)
          {
            Item obj64 = obj63;
            int? shopCustomPrice = obj64.shopCustomPrice;
            int blockPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.BuildingBlockPriceMultiplier;
            obj64.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * blockPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Herbs & Plants")
      {
        foreach (Item obj65 in items)
        {
          if (obj65 != null && obj65.type != 0)
          {
            Item obj66 = obj65;
            int? shopCustomPrice = obj66.shopCustomPrice;
            int herbPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.HerbPriceMultiplier;
            obj66.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * herbPriceMultiplier) : new int?();
          }
        }
      }
      if (shopName == ((ModType) NPCLoader.GetNPC(npc.type)).FullName + "/Modded Fish & Fishing Gear")
      {
        foreach (Item obj67 in items)
        {
          if (obj67 != null && obj67.type != 0)
          {
            Item obj68 = obj67;
            int? shopCustomPrice = obj68.shopCustomPrice;
            int fishPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.FishPriceMultiplier;
            obj68.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * fishPriceMultiplier) : new int?();
          }
        }
      }
    }
    if (npc.type != ModContent.NPCType<BMDealerNPC>() && npc.type != ModContent.NPCType<EtherealCollectorNPC>() || !((Entity) localPlayer).active || localPlayer != Main.player[Main.myPlayer])
      return;
    foreach (Item obj69 in items)
    {
      if (obj69 != null && obj69.type != 0)
      {
        Item obj70 = obj69;
        int? shopCustomPrice = obj70.shopCustomPrice;
        int globalPriceMultiplier = QoLCompendium.QoLCompendium.shopConfig.GlobalPriceMultiplier;
        obj70.shopCustomPrice = shopCustomPrice.HasValue ? new int?(shopCustomPrice.GetValueOrDefault() * globalPriceMultiplier) : new int?();
      }
    }
  }
}
