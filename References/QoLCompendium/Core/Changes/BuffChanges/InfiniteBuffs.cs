// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.BuffPlayer
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using QoLCompendium.Content.Items.Tools.Usables;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

public class BuffPlayer : ModPlayer
{
  private bool hasLuckyLesser;
  private bool hasLucky;
  private bool hasLuckyGreater;
  private bool hasGardenGnome;
  public byte oldLuckPotion;
  private readonly Dictionary<int, List<ItemInfo>> infoByItemType = new Dictionary<int, List<ItemInfo>>();
  private readonly HashSet<int> infiniteStackedItems = new HashSet<int>();

  public virtual void PreUpdateBuffs()
  {
    this.hasLuckyLesser = this.hasLucky = this.hasLuckyGreater = false;
    this.oldLuckPotion = this.Player.oldLuckPotion;
    this.infoByItemType.Clear();
    this.infiniteStackedItems.Clear();
    if (!QoLCompendium.QoLCompendium.mainConfig.EndlessBuffs)
      return;
    this.CheckInventory(this.Player.inventory);
    this.CheckInventory(this.Player.bank.item);
    this.CheckInventory(this.Player.bank2.item);
    this.CheckInventory(this.Player.bank3.item);
    this.CheckInventory(this.Player.bank4.item);
  }

  public virtual void PostUpdateBuffs()
  {
    int num = this.hasLuckyGreater ? 3 : (this.hasLucky ? 2 : (this.hasLuckyLesser ? 1 : 0));
    if (((Entity) this.Player).whoAmI == Main.myPlayer && (int) this.Player.luckPotion != num)
    {
      if ((int) this.Player.luckPotion < num)
      {
        this.Player.luckNeedsSync = true;
        this.Player.luckPotion = (byte) num;
      }
    }
    else if (this.Player.luckNeedsSync)
      this.Player.luckNeedsSync = false;
    this.Player.oldLuckPotion = this.oldLuckPotion = this.Player.luckPotion;
  }

  public virtual void ModifyLuck(ref float luck)
  {
    if (this.hasGardenGnome)
      luck += 0.2f;
    this.hasGardenGnome = false;
  }

  public void CheckInventory(Item[] inventory)
  {
    this.infoByItemType.Clear();
    foreach (Item obj in inventory)
    {
      if (!obj.IsAir)
        this.CheckItemForInfiniteBuffs(obj);
    }
  }

  public void CheckItemForInfiniteBuffs(Item item)
  {
    if (item.IsAir)
      return;
    this.CheckPotion(item);
    this.CheckEnvironment(item);
    this.CheckStation(item);
    this.CheckHoney(item);
    this.CheckExtras(item);
  }

  private void CheckExtras(Item item)
  {
    if (item.type == ModContent.ItemType<PotionCrate>())
    {
      this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
      for (int index = 0; index < PotionCrate.BuffIDList.Count; ++index)
      {
        this.Player.AddBuff(PotionCrate.BuffIDList[index], 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(PotionCrate.BuffIDList[index]);
        if (PotionCrate.ItemIDList.Contains(4477))
          this.hasLuckyLesser = true;
        if (PotionCrate.ItemIDList.Contains(4478))
          this.hasLucky = true;
        if (PotionCrate.ItemIDList.Contains(4479))
          this.hasLuckyGreater = true;
      }
    }
    if (item.type == ModContent.ItemType<BannerBox>())
    {
      this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
      for (int key = 0; key < NPCLoader.NPCCount; ++key)
      {
        int index = ContentSamples.NpcsByNetId[key].BannerID();
        if (NPC.killCount[key] >= ItemID.Sets.KillsToBanner[Item.BannerToItem(index)])
        {
          this.Player.HasNPCBannerBuff(index);
          this.Player.AddBuff(147, 2, true, false);
          this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(147);
          Main.buffNoTimeDisplay[147] = true;
          Main.SceneMetrics.NPCBannerBuff[index] = true;
          Main.SceneMetrics.hasBanner = true;
        }
      }
    }
    if (item.type == 678 && Main.getGoodWorld)
    {
      this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
      for (int index = 0; index < Common.RedPotionBuffs.Count; ++index)
      {
        this.Player.AddBuff(Common.RedPotionBuffs.ElementAt<int>(index), 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(Common.RedPotionBuffs.ElementAt<int>(index));
      }
    }
    if (item.type == 1128 || item.type == 5302)
    {
      this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
      this.Player.AddBuff(48 /*0x30*/, 2, true, false);
      this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(48 /*0x30*/);
    }
    if (item.type != 4609)
      return;
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
    this.hasGardenGnome = true;
  }

  private void CheckPotion(Item item)
  {
    if (!BuffPlayer.CheckPotion_IsBuffPotion(item))
      return;
    if (!this.infoByItemType.ContainsKey(item.type))
      this.infoByItemType.Add(item.type, new List<ItemInfo>());
    ItemInfo info = new ItemInfo(item);
    this.infoByItemType[item.type].Add(info);
    if (QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount <= 0 || item.stack < QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount)
      return;
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
    this.infiniteStackedItems.Add(item.type);
    this.CheckPotion_AddBuff(info);
  }

  private static bool CheckPotion_IsBuffPotion(Item item)
  {
    return item.healLife <= 0 && item.healMana <= 0 && item.buffType > 0 && item.buffTime > 0;
  }

  private void CheckPotion_AddBuff(ItemInfo info)
  {
    if (!this.Player.buffImmune[info.buffType])
    {
      if (ModConditions.calamityLoaded && info.buffType == Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff"))
        this.Player.AddBuff(info.buffType, 10, true, false);
      else
        this.Player.AddBuff(info.buffType, 2, true, false);
      this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(info.buffType);
    }
    if (info.type == 4477)
      this.hasLuckyLesser = true;
    else if (info.type == 4478)
    {
      this.hasLucky = true;
    }
    else
    {
      if (info.type != 4479)
        return;
      this.hasLuckyGreater = true;
    }
  }

  private void CheckEnvironment(Item item)
  {
    if (!BuffPlayer.CheckEnvironment_ItemIsValidPlaceableTile(item))
      return;
    if (!this.infoByItemType.ContainsKey(item.type))
      this.infoByItemType.Add(item.type, new List<ItemInfo>());
    ItemInfo info = new ItemInfo(item);
    this.infoByItemType[item.type].Add(info);
    int stackTarget = BuffPlayer.GetStackTarget(item, QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount);
    if (stackTarget <= 0 || item.stack < stackTarget)
      return;
    this.infiniteStackedItems.Add(item.type);
    this.CheckEnvironment_AddBuffs(info);
  }

  private void CheckEnvironment_AddBuffs(ItemInfo info)
  {
    switch (info.createTile)
    {
      case 27:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.CheckEnvironment_Sunflower();
        break;
      case 42:
        if (info.placeStyle == 9)
        {
          this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
          this.CheckEnvironment_HeartLantern();
          break;
        }
        if (info.placeStyle == 7)
        {
          this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
          this.CheckEnvironment_StarInABottle();
          break;
        }
        break;
      case 49:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.CheckEnvironment_WaterCandle(info.stack);
        break;
      case 215:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.CheckEnvironment_Campfire();
        break;
      case 372:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.CheckEnvironment_PeaceCandle(info.stack);
        break;
      case 506:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.CheckEnvironment_CatBast();
        break;
      case 646:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.CheckEnvironment_ShadowCandle(info.stack);
        break;
    }
    HashSet<int> intSet = new HashSet<int>()
    {
      215,
      27,
      49,
      372,
      646,
      506
    };
    if ((info.createTile != 42 || info.placeStyle != 7 && info.placeStyle != 9) && !intSet.Contains(info.createTile))
      return;
    Point16 tileCoordinates16 = Utils.ToTileCoordinates16(((Entity) this.Player).Center);
    TileLoader.NearbyEffects((int) tileCoordinates16.X, (int) tileCoordinates16.Y, info.createTile, false);
  }

  private static int GetStackTarget(Item item, int defaultStackConfig)
  {
    if (item.IsAir)
      return defaultStackConfig;
    if (item.type == 1134)
      return QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount;
    switch (item.createTile)
    {
      case 27:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 42:
        if (item.placeStyle == 9 || item.placeStyle == 7)
          return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
        break;
      case 49:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 125:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 215:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 287:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 354:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 372:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 377:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 464:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 506:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 621:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
      case 646:
        return QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount;
    }
    return defaultStackConfig;
  }

  private static bool CheckEnvironment_ItemIsValidPlaceableTile(Item item)
  {
    return item.createTile >= 0 && item.type != 2 && Main.tileFrameImportant[item.createTile];
  }

  private void CheckEnvironment_Campfire()
  {
    Main.SceneMetrics.HasCampfire = true;
    this.Player.AddBuff(87, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(87);
  }

  private void CheckEnvironment_Sunflower()
  {
    if (Main.SceneMetrics.GraveyardTileCount > SceneMetrics.GraveyardTileMin)
      return;
    Main.SceneMetrics.HasSunflower = true;
    this.Player.AddBuff(146, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(146);
  }

  private void CheckEnvironment_HeartLantern()
  {
    Main.SceneMetrics.HasHeartLantern = true;
    this.Player.AddBuff(89, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(89);
  }

  private void CheckEnvironment_StarInABottle()
  {
    Main.SceneMetrics.HasStarInBottle = true;
    this.Player.AddBuff(158, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(158);
  }

  private void CheckEnvironment_WaterCandle(int count)
  {
    Main.SceneMetrics.WaterCandleCount += count;
    this.Player.AddBuff(86, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(86);
    this.Player.ZoneWaterCandle = true;
  }

  private void CheckEnvironment_PeaceCandle(int count)
  {
    Main.SceneMetrics.PeaceCandleCount += count;
    this.Player.AddBuff(157, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(157);
    this.Player.ZonePeaceCandle = true;
  }

  private void CheckEnvironment_ShadowCandle(int count)
  {
    Main.SceneMetrics.ShadowCandleCount += count;
    this.Player.AddBuff(350, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(350);
    this.Player.ZoneShadowCandle = true;
  }

  private void CheckEnvironment_CatBast()
  {
    Main.SceneMetrics.HasCatBast = true;
    this.Player.AddBuff(215, 2, false, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(215);
  }

  private void CheckStation(Item item)
  {
    if (!BuffPlayer.CheckEnvironment_ItemIsValidPlaceableTile(item))
      return;
    List<ItemInfo> itemInfoList;
    if (!this.infoByItemType.TryGetValue(item.type, out itemInfoList))
    {
      itemInfoList = new List<ItemInfo>();
      this.infoByItemType.Add(item.type, itemInfoList);
    }
    ItemInfo info = new ItemInfo(item);
    itemInfoList.Add(info);
    int stackTarget = BuffPlayer.GetStackTarget(item, QoLCompendium.QoLCompendium.mainConfig.EndlessStationAmount);
    if (stackTarget <= 0 || item.stack < stackTarget)
      return;
    this.infiniteStackedItems.Add(item.type);
    this.CheckStation_AddBuffs(info);
  }

  private void CheckStation_AddBuffs(ItemInfo info)
  {
    switch (info.createTile)
    {
      case 125:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.Player.AddBuff(29, 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(29);
        break;
      case 287:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.Player.AddBuff(93, 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(93);
        break;
      case 354:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.Player.AddBuff(150, 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(150);
        break;
      case 377:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.Player.AddBuff(159, 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(159);
        break;
      case 464:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.Player.AddBuff(348, 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(348);
        break;
      case 621:
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
        this.Player.AddBuff(192 /*0xC0*/, 2, true, false);
        this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(192 /*0xC0*/);
        break;
      default:
        using (Dictionary<int, int>.Enumerator enumerator = BuffSystem.ModdedPlaceableItemBuffs.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<int, int> current = enumerator.Current;
            Item lookForThis = new Item();
            if (ModConditions.secretsOfTheShadowsLoaded)
              lookForThis.type = Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent");
            if (info.type == current.Key)
            {
              if (ModConditions.secretsOfTheShadowsLoaded && info.type == lookForThis.type && this.Player.HasItem(lookForThis.type) && !this.Player.inventory[Common.GetSlotItemIsIn(lookForThis, this.Player.inventory)].favorited)
                break;
              this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
              this.Player.AddBuff(current.Value, 2, true, false);
              this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(current.Value);
            }
          }
          break;
        }
    }
  }

  private void CheckHoney(Item item)
  {
    if (item.type != 1134)
      return;
    List<ItemInfo> itemInfoList;
    if (!this.infoByItemType.TryGetValue(item.type, out itemInfoList))
    {
      itemInfoList = new List<ItemInfo>();
      this.infoByItemType.Add(item.type, itemInfoList);
    }
    ItemInfo itemInfo = new ItemInfo(item);
    itemInfoList.Add(itemInfo);
    if (QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount <= 0 || item.stack < QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount)
      return;
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
    this.infiniteStackedItems.Add(item.type);
    this.CheckHoney_AddBuff();
  }

  private void CheckHoney_AddBuff()
  {
    this.Player.AddBuff(48 /*0x30*/, 2, true, false);
    this.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(48 /*0x30*/);
  }
}
