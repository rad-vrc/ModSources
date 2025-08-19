using System;
using System.Collections.Generic;
using System.Linq;
using QoLCompendium.Content.Items.Tools.Usables;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.BuffChanges
{
	// Token: 0x02000264 RID: 612
	public class BuffPlayer : ModPlayer
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x000720BC File Offset: 0x000702BC
		public override void PreUpdateBuffs()
		{
			this.hasLuckyLesser = (this.hasLucky = (this.hasLuckyGreater = false));
			this.oldLuckPotion = base.Player.oldLuckPotion;
			this.infoByItemType.Clear();
			this.infiniteStackedItems.Clear();
			if (QoLCompendium.mainConfig.EndlessBuffs)
			{
				this.CheckInventory(base.Player.inventory);
				this.CheckInventory(base.Player.bank.item);
				this.CheckInventory(base.Player.bank2.item);
				this.CheckInventory(base.Player.bank3.item);
				this.CheckInventory(base.Player.bank4.item);
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00072180 File Offset: 0x00070380
		public override void PostUpdateBuffs()
		{
			int luckLevel = this.hasLuckyGreater ? 3 : (this.hasLucky ? 2 : ((this.hasLuckyLesser > false) ? 1 : 0));
			if (base.Player.whoAmI == Main.myPlayer && (int)base.Player.luckPotion != luckLevel)
			{
				if ((int)base.Player.luckPotion < luckLevel)
				{
					base.Player.luckNeedsSync = true;
					base.Player.luckPotion = (byte)luckLevel;
				}
			}
			else if (base.Player.luckNeedsSync)
			{
				base.Player.luckNeedsSync = false;
			}
			base.Player.oldLuckPotion = (this.oldLuckPotion = base.Player.luckPotion);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0007222E File Offset: 0x0007042E
		public override void ModifyLuck(ref float luck)
		{
			if (this.hasGardenGnome)
			{
				luck += 0.2f;
			}
			this.hasGardenGnome = false;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0007224C File Offset: 0x0007044C
		public void CheckInventory(Item[] inventory)
		{
			this.infoByItemType.Clear();
			foreach (Item val in inventory)
			{
				if (!val.IsAir)
				{
					this.CheckItemForInfiniteBuffs(val);
				}
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00072287 File Offset: 0x00070487
		public void CheckItemForInfiniteBuffs(Item item)
		{
			if (!item.IsAir)
			{
				this.CheckPotion(item);
				this.CheckEnvironment(item);
				this.CheckStation(item);
				this.CheckHoney(item);
				this.CheckExtras(item);
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000722B4 File Offset: 0x000704B4
		private void CheckExtras(Item item)
		{
			if (item.type == ModContent.ItemType<PotionCrate>())
			{
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
				for (int i = 0; i < PotionCrate.BuffIDList.Count; i++)
				{
					base.Player.AddBuff(PotionCrate.BuffIDList[i], 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(PotionCrate.BuffIDList[i]);
					if (PotionCrate.ItemIDList.Contains(4477))
					{
						this.hasLuckyLesser = true;
					}
					if (PotionCrate.ItemIDList.Contains(4478))
					{
						this.hasLucky = true;
					}
					if (PotionCrate.ItemIDList.Contains(4479))
					{
						this.hasLuckyGreater = true;
					}
				}
			}
			if (item.type == ModContent.ItemType<BannerBox>())
			{
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
				for (int j = 0; j < NPCLoader.NPCCount; j++)
				{
					int bItem = ContentSamples.NpcsByNetId[j].BannerID();
					if (NPC.killCount[j] >= ItemID.Sets.KillsToBanner[Item.BannerToItem(bItem)])
					{
						base.Player.HasNPCBannerBuff(bItem);
						base.Player.AddBuff(147, 2, true, false);
						base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(147);
						Main.buffNoTimeDisplay[147] = true;
						Main.SceneMetrics.NPCBannerBuff[bItem] = true;
						Main.SceneMetrics.hasBanner = true;
					}
				}
			}
			if (item.type == 678 && Main.getGoodWorld)
			{
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
				for (int k = 0; k < Common.RedPotionBuffs.Count; k++)
				{
					base.Player.AddBuff(Common.RedPotionBuffs.ElementAt(k), 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(Common.RedPotionBuffs.ElementAt(k));
				}
			}
			if (item.type == 1128 || item.type == 5302)
			{
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
				base.Player.AddBuff(48, 2, true, false);
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(48);
			}
			if (item.type == 4609)
			{
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
				this.hasGardenGnome = true;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00072558 File Offset: 0x00070758
		private void CheckPotion(Item item)
		{
			if (BuffPlayer.CheckPotion_IsBuffPotion(item))
			{
				if (!this.infoByItemType.ContainsKey(item.type))
				{
					this.infoByItemType.Add(item.type, new List<ItemInfo>());
				}
				ItemInfo itemInfo = new ItemInfo(item);
				this.infoByItemType[item.type].Add(itemInfo);
				if (QoLCompendium.mainConfig.EndlessBuffAmount > 0 && item.stack >= QoLCompendium.mainConfig.EndlessBuffAmount)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
					this.infiniteStackedItems.Add(item.type);
					this.CheckPotion_AddBuff(itemInfo);
				}
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0007260B File Offset: 0x0007080B
		private static bool CheckPotion_IsBuffPotion(Item item)
		{
			return item.healLife <= 0 && item.healMana <= 0 && item.buffType > 0 && item.buffTime > 0;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00072634 File Offset: 0x00070834
		private void CheckPotion_AddBuff(ItemInfo info)
		{
			if (!base.Player.buffImmune[info.buffType])
			{
				if (ModConditions.calamityLoaded && info.buffType == Common.GetModBuff(ModConditions.calamityMod, "TeslaBuff"))
				{
					base.Player.AddBuff(info.buffType, 10, true, false);
				}
				else
				{
					base.Player.AddBuff(info.buffType, 2, true, false);
				}
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(info.buffType);
			}
			if (info.type == 4477)
			{
				this.hasLuckyLesser = true;
				return;
			}
			if (info.type == 4478)
			{
				this.hasLucky = true;
				return;
			}
			if (info.type == 4479)
			{
				this.hasLuckyGreater = true;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000726F8 File Offset: 0x000708F8
		private void CheckEnvironment(Item item)
		{
			if (BuffPlayer.CheckEnvironment_ItemIsValidPlaceableTile(item))
			{
				if (!this.infoByItemType.ContainsKey(item.type))
				{
					this.infoByItemType.Add(item.type, new List<ItemInfo>());
				}
				ItemInfo itemInfo = new ItemInfo(item);
				this.infoByItemType[item.type].Add(itemInfo);
				int stackTarget = BuffPlayer.GetStackTarget(item, QoLCompendium.mainConfig.EndlessStationAmount);
				if (stackTarget > 0 && item.stack >= stackTarget)
				{
					this.infiniteStackedItems.Add(item.type);
					this.CheckEnvironment_AddBuffs(itemInfo);
				}
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0007278C File Offset: 0x0007098C
		private void CheckEnvironment_AddBuffs(ItemInfo info)
		{
			int createTile = info.createTile;
			if (createTile <= 49)
			{
				if (createTile != 27)
				{
					if (createTile != 42)
					{
						if (createTile == 49)
						{
							base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
							this.CheckEnvironment_WaterCandle(info.stack);
						}
					}
					else if (info.placeStyle == 9)
					{
						base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
						this.CheckEnvironment_HeartLantern();
					}
					else if (info.placeStyle == 7)
					{
						base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
						this.CheckEnvironment_StarInABottle();
					}
				}
				else
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					this.CheckEnvironment_Sunflower();
				}
			}
			else if (createTile <= 372)
			{
				if (createTile != 215)
				{
					if (createTile == 372)
					{
						base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
						this.CheckEnvironment_PeaceCandle(info.stack);
					}
				}
				else
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					this.CheckEnvironment_Campfire();
				}
			}
			else if (createTile != 506)
			{
				if (createTile == 646)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					this.CheckEnvironment_ShadowCandle(info.stack);
				}
			}
			else
			{
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
				this.CheckEnvironment_CatBast();
			}
			HashSet<int> hashSet = new HashSet<int>();
			hashSet.Add(215);
			hashSet.Add(27);
			hashSet.Add(49);
			hashSet.Add(372);
			hashSet.Add(646);
			hashSet.Add(506);
			HashSet<int> nearbyEffects = hashSet;
			if ((info.createTile == 42 && (info.placeStyle == 7 || info.placeStyle == 9)) || nearbyEffects.Contains(info.createTile))
			{
				Point16 val = base.Player.Center.ToTileCoordinates16();
				TileLoader.NearbyEffects((int)val.X, (int)val.Y, info.createTile, false);
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000729E0 File Offset: 0x00070BE0
		private static int GetStackTarget(Item item, int defaultStackConfig)
		{
			if (item.IsAir)
			{
				return defaultStackConfig;
			}
			if (item.type == 1134)
			{
				return QoLCompendium.mainConfig.EndlessBuffAmount;
			}
			int createTile = item.createTile;
			if (createTile <= 287)
			{
				if (createTile <= 49)
				{
					if (createTile == 27)
					{
						return QoLCompendium.mainConfig.EndlessStationAmount;
					}
					if (createTile != 42)
					{
						if (createTile == 49)
						{
							return QoLCompendium.mainConfig.EndlessStationAmount;
						}
					}
					else
					{
						if (item.placeStyle == 9)
						{
							return QoLCompendium.mainConfig.EndlessStationAmount;
						}
						if (item.placeStyle == 7)
						{
							return QoLCompendium.mainConfig.EndlessStationAmount;
						}
					}
				}
				else
				{
					if (createTile == 125)
					{
						return QoLCompendium.mainConfig.EndlessStationAmount;
					}
					if (createTile == 215)
					{
						return QoLCompendium.mainConfig.EndlessStationAmount;
					}
					if (createTile == 287)
					{
						return QoLCompendium.mainConfig.EndlessStationAmount;
					}
				}
			}
			else if (createTile <= 377)
			{
				if (createTile == 354)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
				if (createTile == 372)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
				if (createTile == 377)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
			}
			else if (createTile <= 506)
			{
				if (createTile == 464)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
				if (createTile == 506)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
			}
			else
			{
				if (createTile == 621)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
				if (createTile == 646)
				{
					return QoLCompendium.mainConfig.EndlessStationAmount;
				}
			}
			return defaultStackConfig;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00072B74 File Offset: 0x00070D74
		private static bool CheckEnvironment_ItemIsValidPlaceableTile(Item item)
		{
			return item.createTile >= 0 && item.type != 2 && Main.tileFrameImportant[item.createTile];
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00072B96 File Offset: 0x00070D96
		private void CheckEnvironment_Campfire()
		{
			Main.SceneMetrics.HasCampfire = true;
			base.Player.AddBuff(87, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(87);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00072BCC File Offset: 0x00070DCC
		private void CheckEnvironment_Sunflower()
		{
			if (Main.SceneMetrics.GraveyardTileCount <= SceneMetrics.GraveyardTileMin)
			{
				Main.SceneMetrics.HasSunflower = true;
				base.Player.AddBuff(146, 2, false, false);
				base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(146);
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00072C22 File Offset: 0x00070E22
		private void CheckEnvironment_HeartLantern()
		{
			Main.SceneMetrics.HasHeartLantern = true;
			base.Player.AddBuff(89, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(89);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00072C56 File Offset: 0x00070E56
		private void CheckEnvironment_StarInABottle()
		{
			Main.SceneMetrics.HasStarInBottle = true;
			base.Player.AddBuff(158, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(158);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00072C90 File Offset: 0x00070E90
		private void CheckEnvironment_WaterCandle(int count)
		{
			Main.SceneMetrics.WaterCandleCount += count;
			base.Player.AddBuff(86, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(86);
			base.Player.ZoneWaterCandle = true;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00072CE4 File Offset: 0x00070EE4
		private void CheckEnvironment_PeaceCandle(int count)
		{
			Main.SceneMetrics.PeaceCandleCount += count;
			base.Player.AddBuff(157, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(157);
			base.Player.ZonePeaceCandle = true;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00072D3C File Offset: 0x00070F3C
		private void CheckEnvironment_ShadowCandle(int count)
		{
			Main.SceneMetrics.ShadowCandleCount += count;
			base.Player.AddBuff(350, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(350);
			base.Player.ZoneShadowCandle = true;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00072D94 File Offset: 0x00070F94
		private void CheckEnvironment_CatBast()
		{
			Main.SceneMetrics.HasCatBast = true;
			base.Player.AddBuff(215, 2, false, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(215);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00072DD0 File Offset: 0x00070FD0
		private void CheckStation(Item item)
		{
			if (BuffPlayer.CheckEnvironment_ItemIsValidPlaceableTile(item))
			{
				List<ItemInfo> value;
				if (!this.infoByItemType.TryGetValue(item.type, ref value))
				{
					value = new List<ItemInfo>();
					this.infoByItemType.Add(item.type, value);
				}
				ItemInfo itemInfo = new ItemInfo(item);
				value.Add(itemInfo);
				int stackTarget = BuffPlayer.GetStackTarget(item, QoLCompendium.mainConfig.EndlessStationAmount);
				if (stackTarget > 0 && item.stack >= stackTarget)
				{
					this.infiniteStackedItems.Add(item.type);
					this.CheckStation_AddBuffs(itemInfo);
				}
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00072E58 File Offset: 0x00071058
		private void CheckStation_AddBuffs(ItemInfo info)
		{
			int createTile = info.createTile;
			if (createTile <= 354)
			{
				if (createTile == 125)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(29, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(29);
					return;
				}
				if (createTile == 287)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(93, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(93);
					return;
				}
				if (createTile == 354)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(150, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(150);
					return;
				}
			}
			else
			{
				if (createTile == 377)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(159, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(159);
					return;
				}
				if (createTile == 464)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(348, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(348);
					return;
				}
				if (createTile == 621)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(192, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(192);
					return;
				}
			}
			foreach (KeyValuePair<int, int> moddedBuff in BuffSystem.ModdedPlaceableItemBuffs)
			{
				Item electroDeterrent = new Item();
				if (ModConditions.secretsOfTheShadowsLoaded)
				{
					electroDeterrent.type = Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ElectromagneticDeterrent");
				}
				if (info.type == moddedBuff.Key)
				{
					if (ModConditions.secretsOfTheShadowsLoaded && info.type == electroDeterrent.type && base.Player.HasItem(electroDeterrent.type) && !base.Player.inventory[Common.GetSlotItemIsIn(electroDeterrent, base.Player.inventory)].favorited)
					{
						break;
					}
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(info.type);
					base.Player.AddBuff(moddedBuff.Value, 2, true, false);
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(moddedBuff.Value);
				}
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00073174 File Offset: 0x00071374
		private void CheckHoney(Item item)
		{
			if (item.type == 1134)
			{
				List<ItemInfo> value;
				if (!this.infoByItemType.TryGetValue(item.type, ref value))
				{
					value = new List<ItemInfo>();
					this.infoByItemType.Add(item.type, value);
				}
				ItemInfo item2 = new ItemInfo(item);
				value.Add(item2);
				if (QoLCompendium.mainConfig.EndlessBuffAmount > 0 && item.stack >= QoLCompendium.mainConfig.EndlessBuffAmount)
				{
					base.Player.GetModPlayer<QoLCPlayer>().activeBuffItems.Add(item.type);
					this.infiniteStackedItems.Add(item.type);
					this.CheckHoney_AddBuff();
				}
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0007321F File Offset: 0x0007141F
		private void CheckHoney_AddBuff()
		{
			base.Player.AddBuff(48, 2, true, false);
			base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Add(48);
		}

		// Token: 0x040005B5 RID: 1461
		private bool hasLuckyLesser;

		// Token: 0x040005B6 RID: 1462
		private bool hasLucky;

		// Token: 0x040005B7 RID: 1463
		private bool hasLuckyGreater;

		// Token: 0x040005B8 RID: 1464
		private bool hasGardenGnome;

		// Token: 0x040005B9 RID: 1465
		public byte oldLuckPotion;

		// Token: 0x040005BA RID: 1466
		private readonly Dictionary<int, List<ItemInfo>> infoByItemType = new Dictionary<int, List<ItemInfo>>();

		// Token: 0x040005BB RID: 1467
		private readonly HashSet<int> infiniteStackedItems = new HashSet<int>();
	}
}
