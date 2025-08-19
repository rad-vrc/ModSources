using System;
using QoLCompendium.Content.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x02000247 RID: 583
	public class ModifyShopPrices : GlobalNPC
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00002430 File Offset: 0x00000630
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0006DA2C File Offset: 0x0006BC2C
		public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
		{
			Player player = Main.LocalPlayer;
			if (npc.type == ModContent.NPCType<BMDealerNPC>() && player.active && player == Main.player[Main.myPlayer])
			{
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Potions")
				{
					foreach (Item item in items)
					{
						if (item != null && item.type != 0)
						{
							item.shopCustomPrice *= QoLCompendium.shopConfig.PotionPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Flasks, Stations & Foods")
				{
					foreach (Item item2 in items)
					{
						if (item2 != null && item2.type != 0)
						{
							if (item2.buffType > 0)
							{
								item2.shopCustomPrice *= QoLCompendium.shopConfig.PotionPriceMultiplier;
							}
							else
							{
								item2.shopCustomPrice *= QoLCompendium.shopConfig.StationPriceMultiplier;
							}
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Materials")
				{
					foreach (Item item3 in items)
					{
						if (item3 != null && item3.type != 0)
						{
							item3.shopCustomPrice *= QoLCompendium.shopConfig.MaterialPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Movement Accessories")
				{
					foreach (Item item4 in items)
					{
						if (item4 != null && item4.type != 0)
						{
							item4.shopCustomPrice *= QoLCompendium.shopConfig.AccessoryPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Combat Accessories")
				{
					foreach (Item item5 in items)
					{
						if (item5 != null && item5.type != 0)
						{
							item5.shopCustomPrice *= QoLCompendium.shopConfig.AccessoryPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Informative/Building Gear")
				{
					foreach (Item item6 in items)
					{
						if (item6 != null && item6.type != 0)
						{
							item6.shopCustomPrice *= QoLCompendium.shopConfig.AccessoryPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Treasure Bags")
				{
					foreach (Item item7 in items)
					{
						if (item7 != null && item7.type != 0)
						{
							if (NPC.downedBoss1 && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							if (NPC.downedBoss2 && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							if (NPC.downedBoss3 && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							if (Main.hardMode && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 50000;
							}
							if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							if (NPC.downedPlantBoss && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							if (NPC.downedGolemBoss && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							if (NPC.downedMoonlord && QoLCompendium.shopConfig.BossScaling)
							{
								item7.shopCustomPrice += 25000;
							}
							item7.shopCustomPrice *= QoLCompendium.shopConfig.BagPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Crates & Grab Bags")
				{
					foreach (Item item8 in items)
					{
						if (item8 != null && item8.type != 0)
						{
							item8.shopCustomPrice *= QoLCompendium.shopConfig.CratePriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Ores & Bars")
				{
					foreach (Item item9 in items)
					{
						if (item9 != null && item9.type != 0)
						{
							item9.shopCustomPrice *= QoLCompendium.shopConfig.OrePriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Natural Blocks")
				{
					foreach (Item item10 in items)
					{
						if (item10 != null && item10.type != 0)
						{
							item10.shopCustomPrice *= QoLCompendium.shopConfig.NaturalBlockPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Building Blocks")
				{
					foreach (Item item11 in items)
					{
						if (item11 != null && item11.type != 0)
						{
							item11.shopCustomPrice *= QoLCompendium.shopConfig.BuildingBlockPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Herbs & Plants")
				{
					foreach (Item item12 in items)
					{
						if (item12 != null && item12.type != 0)
						{
							item12.shopCustomPrice *= QoLCompendium.shopConfig.HerbPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Fish & Fishing Gear")
				{
					foreach (Item item13 in items)
					{
						if (item13 != null && item13.type != 0)
						{
							item13.shopCustomPrice *= QoLCompendium.shopConfig.FishPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Mounts & Hooks")
				{
					foreach (Item item14 in items)
					{
						if (item14 != null && item14.type != 0)
						{
							item14.shopCustomPrice *= QoLCompendium.shopConfig.MountPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Ammo")
				{
					foreach (Item item15 in items)
					{
						if (item15 != null && item15.type != 0)
						{
							item15.shopCustomPrice *= QoLCompendium.shopConfig.AmmoPriceMultiplier;
						}
					}
				}
			}
			if (npc.type == ModContent.NPCType<EtherealCollectorNPC>() && player.active && player == Main.player[Main.myPlayer])
			{
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Potions")
				{
					foreach (Item item16 in items)
					{
						if (item16 != null && item16.type != 0)
						{
							item16.shopCustomPrice *= QoLCompendium.shopConfig.PotionPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Flasks, Stations & Foods")
				{
					foreach (Item item17 in items)
					{
						if (item17 != null && item17.type != 0)
						{
							if (item17.buffType > 0)
							{
								item17.shopCustomPrice *= QoLCompendium.shopConfig.PotionPriceMultiplier;
							}
							else
							{
								item17.shopCustomPrice *= QoLCompendium.shopConfig.StationPriceMultiplier;
							}
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Materials")
				{
					foreach (Item item18 in items)
					{
						if (item18 != null && item18.type != 0)
						{
							item18.shopCustomPrice *= QoLCompendium.shopConfig.MaterialPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Treasure Bags")
				{
					foreach (Item item19 in items)
					{
						if (item19 != null && item19.type != 0)
						{
							if (NPC.downedBoss1 && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							if (NPC.downedBoss2 && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							if (NPC.downedBoss3 && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							if (Main.hardMode && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 50000;
							}
							if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							if (NPC.downedPlantBoss && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							if (NPC.downedGolemBoss && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							if (NPC.downedMoonlord && QoLCompendium.shopConfig.BossScaling)
							{
								item19.shopCustomPrice += 25000;
							}
							item19.shopCustomPrice *= QoLCompendium.shopConfig.BagPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Crates & Grab Bags")
				{
					foreach (Item item20 in items)
					{
						if (item20 != null && item20.type != 0)
						{
							item20.shopCustomPrice *= QoLCompendium.shopConfig.CratePriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Ores & Bars")
				{
					foreach (Item item21 in items)
					{
						if (item21 != null && item21.type != 0)
						{
							item21.shopCustomPrice *= QoLCompendium.shopConfig.OrePriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Natural Blocks")
				{
					foreach (Item item22 in items)
					{
						if (item22 != null && item22.type != 0)
						{
							item22.shopCustomPrice *= QoLCompendium.shopConfig.NaturalBlockPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Building Blocks")
				{
					foreach (Item item23 in items)
					{
						if (item23 != null && item23.type != 0)
						{
							item23.shopCustomPrice *= QoLCompendium.shopConfig.BuildingBlockPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Herbs & Plants")
				{
					foreach (Item item24 in items)
					{
						if (item24 != null && item24.type != 0)
						{
							item24.shopCustomPrice *= QoLCompendium.shopConfig.HerbPriceMultiplier;
						}
					}
				}
				if (shopName == NPCLoader.GetNPC(npc.type).FullName + "/Modded Fish & Fishing Gear")
				{
					foreach (Item item25 in items)
					{
						if (item25 != null && item25.type != 0)
						{
							item25.shopCustomPrice *= QoLCompendium.shopConfig.FishPriceMultiplier;
						}
					}
				}
			}
			if ((npc.type == ModContent.NPCType<BMDealerNPC>() || npc.type == ModContent.NPCType<EtherealCollectorNPC>()) && player.active && player == Main.player[Main.myPlayer])
			{
				foreach (Item item26 in items)
				{
					if (item26 != null && item26.type != 0)
					{
						item26.shopCustomPrice *= QoLCompendium.shopConfig.GlobalPriceMultiplier;
					}
				}
			}
		}
	}
}
