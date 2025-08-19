using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.UI
{
	// Token: 0x020000AB RID: 171
	public class ItemSorting
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x004AE950 File Offset: 0x004ACB50
		public static void SetupWhiteLists()
		{
			ItemSorting._layerWhiteLists.Clear();
			List<ItemSorting.ItemSortingLayer> list = new List<ItemSorting.ItemSortingLayer>();
			List<Item> list2 = new List<Item>();
			List<int> list3 = new List<int>();
			list.Add(ItemSorting.ItemSortingLayers.WeaponsMelee);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsRanged);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsMagic);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsMinions);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsAssorted);
			list.Add(ItemSorting.ItemSortingLayers.WeaponsAmmo);
			list.Add(ItemSorting.ItemSortingLayers.ToolsPicksaws);
			list.Add(ItemSorting.ItemSortingLayers.ToolsHamaxes);
			list.Add(ItemSorting.ItemSortingLayers.ToolsPickaxes);
			list.Add(ItemSorting.ItemSortingLayers.ToolsAxes);
			list.Add(ItemSorting.ItemSortingLayers.ToolsHammers);
			list.Add(ItemSorting.ItemSortingLayers.ToolsTerraforming);
			list.Add(ItemSorting.ItemSortingLayers.ToolsAmmoLeftovers);
			list.Add(ItemSorting.ItemSortingLayers.ArmorCombat);
			list.Add(ItemSorting.ItemSortingLayers.ArmorVanity);
			list.Add(ItemSorting.ItemSortingLayers.ArmorAccessories);
			list.Add(ItemSorting.ItemSortingLayers.EquipGrapple);
			list.Add(ItemSorting.ItemSortingLayers.EquipMount);
			list.Add(ItemSorting.ItemSortingLayers.EquipCart);
			list.Add(ItemSorting.ItemSortingLayers.EquipLightPet);
			list.Add(ItemSorting.ItemSortingLayers.EquipVanityPet);
			list.Add(ItemSorting.ItemSortingLayers.PotionsDyes);
			list.Add(ItemSorting.ItemSortingLayers.PotionsHairDyes);
			list.Add(ItemSorting.ItemSortingLayers.PotionsLife);
			list.Add(ItemSorting.ItemSortingLayers.PotionsMana);
			list.Add(ItemSorting.ItemSortingLayers.PotionsElixirs);
			list.Add(ItemSorting.ItemSortingLayers.PotionsBuffs);
			list.Add(ItemSorting.ItemSortingLayers.MiscValuables);
			list.Add(ItemSorting.ItemSortingLayers.MiscPainting);
			list.Add(ItemSorting.ItemSortingLayers.MiscWiring);
			list.Add(ItemSorting.ItemSortingLayers.MiscMaterials);
			list.Add(ItemSorting.ItemSortingLayers.MiscRopes);
			list.Add(ItemSorting.ItemSortingLayers.MiscExtractinator);
			list.Add(ItemSorting.ItemSortingLayers.LastMaterials);
			list.Add(ItemSorting.ItemSortingLayers.LastTilesImportant);
			list.Add(ItemSorting.ItemSortingLayers.LastTilesCommon);
			list.Add(ItemSorting.ItemSortingLayers.LastNotTrash);
			list.Add(ItemSorting.ItemSortingLayers.LastTrash);
			for (int i = -48; i < ItemLoader.ItemCount; i++)
			{
				Item item = new Item();
				item.netDefaults(i);
				list2.Add(item);
				list3.Add(i + 48);
			}
			Item[] array = list2.ToArray();
			foreach (ItemSorting.ItemSortingLayer item2 in list)
			{
				List<int> list4 = item2.SortingMethod(item2, array, list3);
				List<int> list5 = new List<int>();
				for (int j = 0; j < list4.Count; j++)
				{
					list5.Add(array[list4[j]].netID);
				}
				ItemSorting._layerWhiteLists.Add(item2.Name, list5);
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x004AEBF0 File Offset: 0x004ACDF0
		private unsafe static void SetupSortingPriorities()
		{
			Player player = Main.player[Main.myPlayer];
			ItemSorting._layerList.Clear();
			List<StatModifier> list = new List<StatModifier>
			{
				*player.meleeDamage,
				*player.rangedDamage,
				*player.magicDamage,
				*player.minionDamage
			};
			list.Sort((StatModifier x, StatModifier y) => (y.Additive * y.Multiplicative).CompareTo(x.Additive * x.Multiplicative));
			for (int i = 0; i < 5; i++)
			{
				if (!ItemSorting._layerList.Contains(ItemSorting.ItemSortingLayers.WeaponsMelee) && *player.meleeDamage == list[0])
				{
					list.RemoveAt(0);
					ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsMelee);
				}
				if (!ItemSorting._layerList.Contains(ItemSorting.ItemSortingLayers.WeaponsRanged) && *player.rangedDamage == list[0])
				{
					list.RemoveAt(0);
					ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsRanged);
				}
				if (!ItemSorting._layerList.Contains(ItemSorting.ItemSortingLayers.WeaponsMagic) && *player.magicDamage == list[0])
				{
					list.RemoveAt(0);
					ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsMagic);
				}
				if (!ItemSorting._layerList.Contains(ItemSorting.ItemSortingLayers.WeaponsMinions) && *player.minionDamage == list[0])
				{
					list.RemoveAt(0);
					ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsMinions);
				}
			}
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsAssorted);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.WeaponsAmmo);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsPicksaws);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsHamaxes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsPickaxes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsAxes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsHammers);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsTerraforming);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ToolsAmmoLeftovers);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ArmorCombat);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ArmorVanity);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.ArmorAccessories);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipGrapple);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipMount);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipCart);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipLightPet);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.EquipVanityPet);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsDyes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsHairDyes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsLife);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsMana);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsElixirs);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.PotionsBuffs);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscValuables);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscPainting);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscWiring);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscMaterials);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscRopes);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.MiscExtractinator);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastMaterials);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastTilesImportant);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastTilesCommon);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastNotTrash);
			ItemSorting._layerList.Add(ItemSorting.ItemSortingLayers.LastTrash);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x004AEF94 File Offset: 0x004AD194
		private static void Sort(Item[] inv, params int[] ignoreSlots)
		{
			ItemSorting.SetupSortingPriorities();
			List<int> list = new List<int>();
			for (int i = 0; i < inv.Length; i++)
			{
				if (!ignoreSlots.Contains(i))
				{
					Item item = inv[i];
					if (item != null && item.stack != 0 && item.type != 0 && !item.favorited)
					{
						list.Add(i);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				Item item2 = inv[list[j]];
				if (item2.stack < item2.maxStack)
				{
					int num = item2.maxStack - item2.stack;
					for (int k = j; k < list.Count; k++)
					{
						if (j != k)
						{
							Item item3 = inv[list[k]];
							int num5;
							if (item2.type == item3.type && item3.stack != item3.maxStack && ItemLoader.TryStackItems(item2, item3, out num5, false))
							{
								if (item3.stack == 0)
								{
									inv[list[k]] = new Item();
									list.Remove(list[k]);
									j--;
									k--;
									break;
								}
								if (num == 0)
								{
									break;
								}
							}
						}
					}
				}
			}
			List<int> list2 = new List<int>(list);
			for (int l = 0; l < inv.Length; l++)
			{
				if (!ignoreSlots.Contains(l) && !list2.Contains(l))
				{
					Item item4 = inv[l];
					if (item4 == null || item4.stack == 0 || item4.type == 0)
					{
						list2.Add(l);
					}
				}
			}
			list2.Sort();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			foreach (ItemSorting.ItemSortingLayer layer in ItemSorting._layerList)
			{
				List<int> list5 = layer.SortingMethod(layer, inv, list);
				if (list5.Count > 0)
				{
					list4.Add(list5.Count);
				}
				list3.AddRange(list5);
			}
			list3.AddRange(list);
			List<Item> list6 = new List<Item>();
			foreach (int item5 in list3)
			{
				list6.Add(inv[item5]);
				inv[item5] = new Item();
			}
			float num2 = 1f / (float)list4.Count;
			float num3 = num2 / 2f;
			for (int m = 0; m < list6.Count; m++)
			{
				int num4 = list2[0];
				ItemSlot.SetGlow(num4, num3, Main.player[Main.myPlayer].chest != -1);
				List<int> list7 = list4;
				int num5 = list7[0];
				list7[0] = num5 - 1;
				if (list4[0] == 0)
				{
					list4.RemoveAt(0);
					num3 += num2;
				}
				inv[num4] = list6[m];
				list2.Remove(num4);
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x004AF298 File Offset: 0x004AD498
		public static void SortInventory()
		{
			if (!Main.LocalPlayer.HasItem(905))
			{
				ItemSorting.SortCoins();
			}
			ItemSorting.SortAmmo();
			ItemSorting.Sort(Main.player[Main.myPlayer].inventory, new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				50,
				51,
				52,
				53,
				54,
				55,
				56,
				57,
				58
			});
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x004AF2E8 File Offset: 0x004AD4E8
		public static void SortChest()
		{
			int chest = Main.player[Main.myPlayer].chest;
			if (chest == -1)
			{
				return;
			}
			Item[] item = Main.player[Main.myPlayer].bank.item;
			if (chest == -3)
			{
				item = Main.player[Main.myPlayer].bank2.item;
			}
			if (chest == -4)
			{
				item = Main.player[Main.myPlayer].bank3.item;
			}
			if (chest == -5)
			{
				item = Main.player[Main.myPlayer].bank4.item;
			}
			if (chest > -1)
			{
				item = Main.chest[chest].item;
			}
			Tuple<int, int, int>[] array = new Tuple<int, int, int>[40];
			for (int i = 0; i < 40; i++)
			{
				array[i] = Tuple.Create<int, int, int>(item[i].netID, item[i].stack, item[i].prefix);
			}
			ItemSorting.Sort(item, Array.Empty<int>());
			Tuple<int, int, int>[] array2 = new Tuple<int, int, int>[40];
			for (int j = 0; j < 40; j++)
			{
				array2[j] = Tuple.Create<int, int, int>(item[j].netID, item[j].stack, item[j].prefix);
			}
			if (Main.netMode != 1 || Main.player[Main.myPlayer].chest <= -1)
			{
				return;
			}
			for (int k = 0; k < 40; k++)
			{
				if (array2[k] != array[k])
				{
					NetMessage.SendData(32, -1, -1, null, Main.player[Main.myPlayer].chest, (float)k, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x004AF469 File Offset: 0x004AD669
		public static void SortAmmo()
		{
			ItemSorting.ClearAmmoSlotSpaces();
			ItemSorting.FillAmmoFromInventory();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x004AF478 File Offset: 0x004AD678
		public static void FillAmmoFromInventory()
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			Item[] inventory = Main.player[Main.myPlayer].inventory;
			for (int i = 54; i < 58; i++)
			{
				ItemSlot.SetGlow(i, 0.31f, false);
				Item item = inventory[i];
				if (item.IsAir)
				{
					list2.Add(i);
				}
				else if (item.ammo != AmmoID.None)
				{
					if (!list.Contains(item.type))
					{
						list.Add(item.type);
					}
					ItemSorting.RefillItemStack(inventory, inventory[i], 0, 50);
				}
			}
			if (list2.Count < 1)
			{
				return;
			}
			for (int j = 0; j < 50; j++)
			{
				Item item2 = inventory[j];
				if (item2.stack >= 1 && item2.CanFillEmptyAmmoSlot() && list.Contains(item2.type))
				{
					int num = list2[0];
					list2.Remove(num);
					Utils.Swap<Item>(ref inventory[j], ref inventory[num]);
					ItemSorting.RefillItemStack(inventory, inventory[num], 0, 50);
					if (list2.Count == 0)
					{
						break;
					}
				}
			}
			if (list2.Count < 1)
			{
				return;
			}
			for (int k = 0; k < 50; k++)
			{
				Item item3 = inventory[k];
				if (item3.stack >= 1 && item3.CanFillEmptyAmmoSlot() && item3.FitsAmmoSlot())
				{
					int num2 = list2[0];
					list2.Remove(num2);
					Utils.Swap<Item>(ref inventory[k], ref inventory[num2]);
					ItemSorting.RefillItemStack(inventory, inventory[num2], 0, 50);
					if (list2.Count == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x004AF604 File Offset: 0x004AD804
		public static void ClearAmmoSlotSpaces()
		{
			Item[] inventory = Main.player[Main.myPlayer].inventory;
			for (int i = 54; i < 58; i++)
			{
				Item item = inventory[i];
				if (!item.IsAir && item.ammo != AmmoID.None && item.stack < item.maxStack)
				{
					ItemSorting.RefillItemStack(inventory, item, i + 1, 58);
				}
			}
			for (int j = 54; j < 58; j++)
			{
				if (inventory[j].type > 0)
				{
					ItemSorting.TrySlidingUp(inventory, j, 54);
				}
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x004AF688 File Offset: 0x004AD888
		private static void SortCoins()
		{
			Item[] inventory = Main.LocalPlayer.inventory;
			bool overFlowing;
			long count = Utils.CoinsCount(out overFlowing, inventory, new int[]
			{
				58
			});
			int commonMaxStack = Item.CommonMaxStack;
			if (overFlowing)
			{
				return;
			}
			int[] array = Utils.CoinsSplit(count);
			int num = 0;
			for (int i = 0; i < 3; i++)
			{
				int num2 = array[i];
				while (num2 > 0)
				{
					num2 -= 99;
					num++;
				}
			}
			int num3 = array[3];
			while (num3 > commonMaxStack)
			{
				num3 -= commonMaxStack;
				num++;
			}
			int num4 = 0;
			for (int j = 0; j < 58; j++)
			{
				if (inventory[j].type >= 71 && inventory[j].type <= 74 && inventory[j].stack > 0)
				{
					num4++;
				}
			}
			if (num4 < num)
			{
				return;
			}
			for (int k = 0; k < 58; k++)
			{
				if (inventory[k].type >= 71 && inventory[k].type <= 74 && inventory[k].stack > 0)
				{
					inventory[k].TurnToAir(false);
				}
			}
			int num5 = 100;
			do
			{
				int num6 = -1;
				for (int num7 = 3; num7 >= 0; num7--)
				{
					if (array[num7] > 0)
					{
						num6 = num7;
						break;
					}
				}
				if (num6 == -1)
				{
					return;
				}
				int num8 = array[num6];
				if (num6 == 3 && num8 > commonMaxStack)
				{
					num8 = commonMaxStack;
				}
				bool flag = false;
				if (!flag)
				{
					for (int l = 50; l < 54; l++)
					{
						if (inventory[l].IsAir)
						{
							inventory[l].SetDefaults(71 + num6);
							inventory[l].stack = num8;
							array[num6] -= num8;
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					for (int m = 0; m < 50; m++)
					{
						if (inventory[m].IsAir)
						{
							inventory[m].SetDefaults(71 + num6);
							inventory[m].stack = num8;
							array[num6] -= num8;
							break;
						}
					}
				}
				num5--;
			}
			while (num5 > 0);
			for (int num9 = 3; num9 >= 0; num9--)
			{
				if (array[num9] > 0)
				{
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetItemSource_Misc(7), 71 + num9, array[num9]);
				}
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x004AF8BC File Offset: 0x004ADABC
		private static void RefillItemStack(Item[] inv, Item itemToRefill, int loopStartIndex, int loopEndIndex)
		{
			int num = itemToRefill.maxStack - itemToRefill.stack;
			if (num <= 0)
			{
				return;
			}
			for (int i = loopStartIndex; i < loopEndIndex; i++)
			{
				Item item = inv[i];
				int numTransfered;
				if (item.stack >= 1 && item.type == itemToRefill.type && ItemLoader.TryStackItems(itemToRefill, inv[i], out numTransfered, false))
				{
					num -= numTransfered;
					if (item.stack <= 0)
					{
						item.TurnToAir(false);
					}
					if (num <= 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x004AF92C File Offset: 0x004ADB2C
		private static void TrySlidingUp(Item[] inv, int slot, int minimumIndex)
		{
			for (int num = slot; num > minimumIndex; num--)
			{
				if (inv[num - 1].IsAir)
				{
					Utils.Swap<Item>(ref inv[num], ref inv[num - 1]);
				}
			}
		}

		// Token: 0x040010FA RID: 4346
		private static List<ItemSorting.ItemSortingLayer> _layerList = new List<ItemSorting.ItemSortingLayer>();

		// Token: 0x040010FB RID: 4347
		private static Dictionary<string, List<int>> _layerWhiteLists = new Dictionary<string, List<int>>();

		// Token: 0x0200086D RID: 2157
		private class ItemSortingLayer
		{
			// Token: 0x06005165 RID: 20837 RVA: 0x00696ED0 File Offset: 0x006950D0
			public ItemSortingLayer(string name, Func<ItemSorting.ItemSortingLayer, Item[], List<int>, List<int>> method)
			{
				this.Name = name;
				this.SortingMethod = method;
			}

			// Token: 0x06005166 RID: 20838 RVA: 0x00696EE8 File Offset: 0x006950E8
			public void Validate(ref List<int> indexesSortable, Item[] inv)
			{
				List<int> list;
				if (ItemSorting._layerWhiteLists.TryGetValue(this.Name, out list))
				{
					indexesSortable = (from i in indexesSortable
					where list.Contains(inv[i].netID)
					select i).ToList<int>();
				}
			}

			// Token: 0x06005167 RID: 20839 RVA: 0x00696F34 File Offset: 0x00695134
			public override string ToString()
			{
				return this.Name;
			}

			// Token: 0x0400695D RID: 26973
			public readonly string Name;

			// Token: 0x0400695E RID: 26974
			public readonly Func<ItemSorting.ItemSortingLayer, Item[], List<int>, List<int>> SortingMethod;
		}

		// Token: 0x0200086E RID: 2158
		private class ItemSortingLayers
		{
			// Token: 0x0400695F RID: 26975
			public static ItemSorting.ItemSortingLayer WeaponsMelee = new ItemSorting.ItemSortingLayer("Weapons - Melee", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable38 = (from i in itemsToSort
				where inv[i].maxStack == 1 && inv[i].damage > 0 && inv[i].ammo == 0 && inv[i].melee && inv[i].pick < 1 && inv[i].hammer < 1 && inv[i].axe < 1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable38, inv);
				foreach (int item in indexesSortable38)
				{
					itemsToSort.Remove(item);
				}
				indexesSortable38.Sort(delegate(int x, int y)
				{
					int num33 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num33 == 0)
					{
						num33 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num33 == 0)
					{
						num33 = x.CompareTo(y);
					}
					return num33;
				});
				return indexesSortable38;
			});

			// Token: 0x04006960 RID: 26976
			public static ItemSorting.ItemSortingLayer WeaponsRanged = new ItemSorting.ItemSortingLayer("Weapons - Ranged", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable37 = (from i in itemsToSort
				where inv[i].maxStack == 1 && inv[i].damage > 0 && inv[i].ammo == 0 && inv[i].ranged
				select i).ToList<int>();
				layer.Validate(ref indexesSortable37, inv);
				foreach (int item2 in indexesSortable37)
				{
					itemsToSort.Remove(item2);
				}
				indexesSortable37.Sort(delegate(int x, int y)
				{
					int num32 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num32 == 0)
					{
						num32 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num32 == 0)
					{
						num32 = x.CompareTo(y);
					}
					return num32;
				});
				return indexesSortable37;
			});

			// Token: 0x04006961 RID: 26977
			public static ItemSorting.ItemSortingLayer WeaponsMagic = new ItemSorting.ItemSortingLayer("Weapons - Magic", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable36 = (from i in itemsToSort
				where inv[i].maxStack == 1 && inv[i].damage > 0 && inv[i].ammo == 0 && inv[i].magic
				select i).ToList<int>();
				layer.Validate(ref indexesSortable36, inv);
				foreach (int item3 in indexesSortable36)
				{
					itemsToSort.Remove(item3);
				}
				indexesSortable36.Sort(delegate(int x, int y)
				{
					int num31 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num31 == 0)
					{
						num31 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num31 == 0)
					{
						num31 = x.CompareTo(y);
					}
					return num31;
				});
				return indexesSortable36;
			});

			// Token: 0x04006962 RID: 26978
			public static ItemSorting.ItemSortingLayer WeaponsMinions = new ItemSorting.ItemSortingLayer("Weapons - Minions", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable35 = (from i in itemsToSort
				where inv[i].maxStack == 1 && inv[i].damage > 0 && inv[i].summon
				select i).ToList<int>();
				layer.Validate(ref indexesSortable35, inv);
				foreach (int item4 in indexesSortable35)
				{
					itemsToSort.Remove(item4);
				}
				indexesSortable35.Sort(delegate(int x, int y)
				{
					int num30 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num30 == 0)
					{
						num30 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num30 == 0)
					{
						num30 = x.CompareTo(y);
					}
					return num30;
				});
				return indexesSortable35;
			});

			// Token: 0x04006963 RID: 26979
			public static ItemSorting.ItemSortingLayer WeaponsAssorted = new ItemSorting.ItemSortingLayer("Weapons - Assorted", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable34 = (from i in itemsToSort
				where inv[i].damage > 0 && inv[i].ammo == 0 && inv[i].pick == 0 && inv[i].axe == 0 && inv[i].hammer == 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable34, inv);
				foreach (int item5 in indexesSortable34)
				{
					itemsToSort.Remove(item5);
				}
				indexesSortable34.Sort(delegate(int x, int y)
				{
					int num29 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num29 == 0)
					{
						num29 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num29 == 0)
					{
						num29 = x.CompareTo(y);
					}
					return num29;
				});
				return indexesSortable34;
			});

			// Token: 0x04006964 RID: 26980
			public static ItemSorting.ItemSortingLayer WeaponsAmmo = new ItemSorting.ItemSortingLayer("Weapons - Ammo", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable33 = (from i in itemsToSort
				where inv[i].ammo > 0 && inv[i].damage > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable33, inv);
				foreach (int item6 in indexesSortable33)
				{
					itemsToSort.Remove(item6);
				}
				indexesSortable33.Sort(delegate(int x, int y)
				{
					int num28 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num28 == 0)
					{
						num28 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num28 == 0)
					{
						num28 = x.CompareTo(y);
					}
					return num28;
				});
				return indexesSortable33;
			});

			// Token: 0x04006965 RID: 26981
			public static ItemSorting.ItemSortingLayer ToolsPicksaws = new ItemSorting.ItemSortingLayer("Tools - Picksaws", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable32 = (from i in itemsToSort
				where inv[i].pick > 0 && inv[i].axe > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable32, inv);
				foreach (int item7 in indexesSortable32)
				{
					itemsToSort.Remove(item7);
				}
				indexesSortable32.Sort((int x, int y) => inv[x].pick.CompareTo(inv[y].pick));
				return indexesSortable32;
			});

			// Token: 0x04006966 RID: 26982
			public static ItemSorting.ItemSortingLayer ToolsHamaxes = new ItemSorting.ItemSortingLayer("Tools - Hamaxes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable31 = (from i in itemsToSort
				where inv[i].hammer > 0 && inv[i].axe > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable31, inv);
				foreach (int item8 in indexesSortable31)
				{
					itemsToSort.Remove(item8);
				}
				indexesSortable31.Sort((int x, int y) => inv[x].axe.CompareTo(inv[y].axe));
				return indexesSortable31;
			});

			// Token: 0x04006967 RID: 26983
			public static ItemSorting.ItemSortingLayer ToolsPickaxes = new ItemSorting.ItemSortingLayer("Tools - Pickaxes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable30 = (from i in itemsToSort
				where inv[i].pick > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable30, inv);
				foreach (int item9 in indexesSortable30)
				{
					itemsToSort.Remove(item9);
				}
				indexesSortable30.Sort((int x, int y) => inv[x].pick.CompareTo(inv[y].pick));
				return indexesSortable30;
			});

			// Token: 0x04006968 RID: 26984
			public static ItemSorting.ItemSortingLayer ToolsAxes = new ItemSorting.ItemSortingLayer("Tools - Axes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable29 = (from i in itemsToSort
				where inv[i].pick > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable29, inv);
				foreach (int item10 in indexesSortable29)
				{
					itemsToSort.Remove(item10);
				}
				indexesSortable29.Sort((int x, int y) => inv[x].axe.CompareTo(inv[y].axe));
				return indexesSortable29;
			});

			// Token: 0x04006969 RID: 26985
			public static ItemSorting.ItemSortingLayer ToolsHammers = new ItemSorting.ItemSortingLayer("Tools - Hammers", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable28 = (from i in itemsToSort
				where inv[i].hammer > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable28, inv);
				foreach (int item11 in indexesSortable28)
				{
					itemsToSort.Remove(item11);
				}
				indexesSortable28.Sort((int x, int y) => inv[x].hammer.CompareTo(inv[y].hammer));
				return indexesSortable28;
			});

			// Token: 0x0400696A RID: 26986
			public static ItemSorting.ItemSortingLayer ToolsTerraforming = new ItemSorting.ItemSortingLayer("Tools - Terraforming", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable27 = (from i in itemsToSort
				where inv[i].netID > 0 && ItemID.Sets.SortingPriorityTerraforming[inv[i].netID] > -1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable27, inv);
				foreach (int item12 in indexesSortable27)
				{
					itemsToSort.Remove(item12);
				}
				indexesSortable27.Sort(delegate(int x, int y)
				{
					int num27 = ItemID.Sets.SortingPriorityTerraforming[inv[x].netID].CompareTo(ItemID.Sets.SortingPriorityTerraforming[inv[y].netID]);
					if (num27 == 0)
					{
						num27 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num27 == 0)
					{
						num27 = x.CompareTo(y);
					}
					return num27;
				});
				return indexesSortable27;
			});

			// Token: 0x0400696B RID: 26987
			public static ItemSorting.ItemSortingLayer ToolsAmmoLeftovers = new ItemSorting.ItemSortingLayer("Weapons - Ammo Leftovers", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable26 = (from i in itemsToSort
				where inv[i].ammo > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable26, inv);
				foreach (int item13 in indexesSortable26)
				{
					itemsToSort.Remove(item13);
				}
				indexesSortable26.Sort(delegate(int x, int y)
				{
					int num26 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num26 == 0)
					{
						num26 = inv[y].OriginalDamage.CompareTo(inv[x].OriginalDamage);
					}
					if (num26 == 0)
					{
						num26 = x.CompareTo(y);
					}
					return num26;
				});
				return indexesSortable26;
			});

			// Token: 0x0400696C RID: 26988
			public static ItemSorting.ItemSortingLayer ArmorCombat = new ItemSorting.ItemSortingLayer("Armor - Combat", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable25 = (from i in itemsToSort
				where (inv[i].bodySlot >= 0 || inv[i].headSlot >= 0 || inv[i].legSlot >= 0) && !inv[i].vanity
				select i).ToList<int>();
				layer.Validate(ref indexesSortable25, inv);
				foreach (int item14 in indexesSortable25)
				{
					itemsToSort.Remove(item14);
				}
				indexesSortable25.Sort(delegate(int x, int y)
				{
					int num25 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num25 == 0)
					{
						num25 = inv[y].OriginalDefense.CompareTo(inv[x].OriginalDefense);
					}
					if (num25 == 0)
					{
						num25 = inv[x].netID.CompareTo(inv[y].netID);
					}
					return num25;
				});
				return indexesSortable25;
			});

			// Token: 0x0400696D RID: 26989
			public static ItemSorting.ItemSortingLayer ArmorVanity = new ItemSorting.ItemSortingLayer("Armor - Vanity", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable24 = (from i in itemsToSort
				where (inv[i].bodySlot >= 0 || inv[i].headSlot >= 0 || inv[i].legSlot >= 0) && inv[i].vanity
				select i).ToList<int>();
				layer.Validate(ref indexesSortable24, inv);
				foreach (int item15 in indexesSortable24)
				{
					itemsToSort.Remove(item15);
				}
				indexesSortable24.Sort(delegate(int x, int y)
				{
					int num24 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num24 == 0)
					{
						num24 = inv[x].netID.CompareTo(inv[y].netID);
					}
					return num24;
				});
				return indexesSortable24;
			});

			// Token: 0x0400696E RID: 26990
			public static ItemSorting.ItemSortingLayer ArmorAccessories = new ItemSorting.ItemSortingLayer("Armor - Accessories", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable23 = (from i in itemsToSort
				where inv[i].accessory
				select i).ToList<int>();
				layer.Validate(ref indexesSortable23, inv);
				foreach (int item16 in indexesSortable23)
				{
					itemsToSort.Remove(item16);
				}
				indexesSortable23.Sort(delegate(int x, int y)
				{
					int num23 = inv[x].vanity.CompareTo(inv[y].vanity);
					if (num23 == 0)
					{
						num23 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num23 == 0)
					{
						num23 = inv[y].OriginalDefense.CompareTo(inv[x].OriginalDefense);
					}
					if (num23 == 0)
					{
						num23 = inv[x].netID.CompareTo(inv[y].netID);
					}
					return num23;
				});
				return indexesSortable23;
			});

			// Token: 0x0400696F RID: 26991
			public static ItemSorting.ItemSortingLayer EquipGrapple = new ItemSorting.ItemSortingLayer("Equip - Grapple", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable22 = (from i in itemsToSort
				where Main.projHook[inv[i].shoot]
				select i).ToList<int>();
				layer.Validate(ref indexesSortable22, inv);
				foreach (int item17 in indexesSortable22)
				{
					itemsToSort.Remove(item17);
				}
				indexesSortable22.Sort(delegate(int x, int y)
				{
					int num22 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num22 == 0)
					{
						num22 = inv[x].netID.CompareTo(inv[y].netID);
					}
					if (num22 == 0)
					{
						num22 = x.CompareTo(y);
					}
					return num22;
				});
				return indexesSortable22;
			});

			// Token: 0x04006970 RID: 26992
			public static ItemSorting.ItemSortingLayer EquipMount = new ItemSorting.ItemSortingLayer("Equip - Mount", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable21 = (from i in itemsToSort
				where inv[i].mountType != -1 && !MountID.Sets.Cart[inv[i].mountType]
				select i).ToList<int>();
				layer.Validate(ref indexesSortable21, inv);
				foreach (int item18 in indexesSortable21)
				{
					itemsToSort.Remove(item18);
				}
				indexesSortable21.Sort(delegate(int x, int y)
				{
					int num21 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num21 == 0)
					{
						num21 = inv[x].netID.CompareTo(inv[y].netID);
					}
					if (num21 == 0)
					{
						num21 = x.CompareTo(y);
					}
					return num21;
				});
				return indexesSortable21;
			});

			// Token: 0x04006971 RID: 26993
			public static ItemSorting.ItemSortingLayer EquipCart = new ItemSorting.ItemSortingLayer("Equip - Cart", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable20 = (from i in itemsToSort
				where inv[i].mountType != -1 && MountID.Sets.Cart[inv[i].mountType]
				select i).ToList<int>();
				layer.Validate(ref indexesSortable20, inv);
				foreach (int item19 in indexesSortable20)
				{
					itemsToSort.Remove(item19);
				}
				indexesSortable20.Sort(delegate(int x, int y)
				{
					int num20 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num20 == 0)
					{
						num20 = inv[x].netID.CompareTo(inv[y].netID);
					}
					if (num20 == 0)
					{
						num20 = x.CompareTo(y);
					}
					return num20;
				});
				return indexesSortable20;
			});

			// Token: 0x04006972 RID: 26994
			public static ItemSorting.ItemSortingLayer EquipLightPet = new ItemSorting.ItemSortingLayer("Equip - Light Pet", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable19 = (from i in itemsToSort
				where inv[i].buffType > 0 && Main.lightPet[inv[i].buffType]
				select i).ToList<int>();
				layer.Validate(ref indexesSortable19, inv);
				foreach (int item20 in indexesSortable19)
				{
					itemsToSort.Remove(item20);
				}
				indexesSortable19.Sort(delegate(int x, int y)
				{
					int num19 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num19 == 0)
					{
						num19 = inv[x].netID.CompareTo(inv[y].netID);
					}
					if (num19 == 0)
					{
						num19 = x.CompareTo(y);
					}
					return num19;
				});
				return indexesSortable19;
			});

			// Token: 0x04006973 RID: 26995
			public static ItemSorting.ItemSortingLayer EquipVanityPet = new ItemSorting.ItemSortingLayer("Equip - Vanity Pet", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable18 = (from i in itemsToSort
				where inv[i].buffType > 0 && Main.vanityPet[inv[i].buffType]
				select i).ToList<int>();
				layer.Validate(ref indexesSortable18, inv);
				foreach (int item21 in indexesSortable18)
				{
					itemsToSort.Remove(item21);
				}
				indexesSortable18.Sort(delegate(int x, int y)
				{
					int num18 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num18 == 0)
					{
						num18 = inv[x].netID.CompareTo(inv[y].netID);
					}
					if (num18 == 0)
					{
						num18 = x.CompareTo(y);
					}
					return num18;
				});
				return indexesSortable18;
			});

			// Token: 0x04006974 RID: 26996
			public static ItemSorting.ItemSortingLayer PotionsLife = new ItemSorting.ItemSortingLayer("Potions - Life", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable17 = (from i in itemsToSort
				where inv[i].consumable && inv[i].healLife > 0 && inv[i].healMana < 1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable17, inv);
				foreach (int item22 in indexesSortable17)
				{
					itemsToSort.Remove(item22);
				}
				indexesSortable17.Sort(delegate(int x, int y)
				{
					int num17 = inv[y].healLife.CompareTo(inv[x].healLife);
					if (num17 == 0)
					{
						num17 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num17 == 0)
					{
						num17 = x.CompareTo(y);
					}
					return num17;
				});
				return indexesSortable17;
			});

			// Token: 0x04006975 RID: 26997
			public static ItemSorting.ItemSortingLayer PotionsMana = new ItemSorting.ItemSortingLayer("Potions - Mana", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable16 = (from i in itemsToSort
				where inv[i].consumable && inv[i].healLife < 1 && inv[i].healMana > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable16, inv);
				foreach (int item23 in indexesSortable16)
				{
					itemsToSort.Remove(item23);
				}
				indexesSortable16.Sort(delegate(int x, int y)
				{
					int num16 = inv[y].healMana.CompareTo(inv[x].healMana);
					if (num16 == 0)
					{
						num16 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num16 == 0)
					{
						num16 = x.CompareTo(y);
					}
					return num16;
				});
				return indexesSortable16;
			});

			// Token: 0x04006976 RID: 26998
			public static ItemSorting.ItemSortingLayer PotionsElixirs = new ItemSorting.ItemSortingLayer("Potions - Elixirs", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable15 = (from i in itemsToSort
				where inv[i].consumable && inv[i].healLife > 0 && inv[i].healMana > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable15, inv);
				foreach (int item24 in indexesSortable15)
				{
					itemsToSort.Remove(item24);
				}
				indexesSortable15.Sort(delegate(int x, int y)
				{
					int num15 = inv[y].healLife.CompareTo(inv[x].healLife);
					if (num15 == 0)
					{
						num15 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num15 == 0)
					{
						num15 = x.CompareTo(y);
					}
					return num15;
				});
				return indexesSortable15;
			});

			// Token: 0x04006977 RID: 26999
			public static ItemSorting.ItemSortingLayer PotionsBuffs = new ItemSorting.ItemSortingLayer("Potions - Buffs", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable14 = (from i in itemsToSort
				where inv[i].consumable && inv[i].buffType > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable14, inv);
				foreach (int item25 in indexesSortable14)
				{
					itemsToSort.Remove(item25);
				}
				indexesSortable14.Sort(delegate(int x, int y)
				{
					int num14 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num14 == 0)
					{
						num14 = inv[x].netID.CompareTo(inv[y].netID);
					}
					if (num14 == 0)
					{
						num14 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num14 == 0)
					{
						num14 = x.CompareTo(y);
					}
					return num14;
				});
				return indexesSortable14;
			});

			// Token: 0x04006978 RID: 27000
			public static ItemSorting.ItemSortingLayer PotionsDyes = new ItemSorting.ItemSortingLayer("Potions - Dyes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable13 = (from i in itemsToSort
				where inv[i].dye > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable13, inv);
				foreach (int item26 in indexesSortable13)
				{
					itemsToSort.Remove(item26);
				}
				indexesSortable13.Sort(delegate(int x, int y)
				{
					int num13 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num13 == 0)
					{
						num13 = inv[y].dye.CompareTo(inv[x].dye);
					}
					if (num13 == 0)
					{
						num13 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num13 == 0)
					{
						num13 = x.CompareTo(y);
					}
					return num13;
				});
				return indexesSortable13;
			});

			// Token: 0x04006979 RID: 27001
			public static ItemSorting.ItemSortingLayer PotionsHairDyes = new ItemSorting.ItemSortingLayer("Potions - Hair Dyes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable12 = (from i in itemsToSort
				where inv[i].hairDye >= 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable12, inv);
				foreach (int item27 in indexesSortable12)
				{
					itemsToSort.Remove(item27);
				}
				indexesSortable12.Sort(delegate(int x, int y)
				{
					int num12 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num12 == 0)
					{
						num12 = inv[y].hairDye.CompareTo(inv[x].hairDye);
					}
					if (num12 == 0)
					{
						num12 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num12 == 0)
					{
						num12 = x.CompareTo(y);
					}
					return num12;
				});
				return indexesSortable12;
			});

			// Token: 0x0400697A RID: 27002
			public static ItemSorting.ItemSortingLayer MiscValuables = new ItemSorting.ItemSortingLayer("Misc - Importants", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable11 = (from i in itemsToSort
				where inv[i].netID > 0 && ItemID.Sets.SortingPriorityBossSpawns[inv[i].netID] > -1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable11, inv);
				foreach (int item28 in indexesSortable11)
				{
					itemsToSort.Remove(item28);
				}
				indexesSortable11.Sort(delegate(int x, int y)
				{
					int num11 = ItemID.Sets.SortingPriorityBossSpawns[inv[x].netID].CompareTo(ItemID.Sets.SortingPriorityBossSpawns[inv[y].netID]);
					if (num11 == 0)
					{
						num11 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num11 == 0)
					{
						num11 = x.CompareTo(y);
					}
					return num11;
				});
				return indexesSortable11;
			});

			// Token: 0x0400697B RID: 27003
			public static ItemSorting.ItemSortingLayer MiscWiring = new ItemSorting.ItemSortingLayer("Misc - Wiring", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable10 = (from i in itemsToSort
				where (inv[i].netID > 0 && ItemID.Sets.SortingPriorityWiring[inv[i].netID] > -1) || inv[i].mech
				select i).ToList<int>();
				layer.Validate(ref indexesSortable10, inv);
				foreach (int item29 in indexesSortable10)
				{
					itemsToSort.Remove(item29);
				}
				indexesSortable10.Sort(delegate(int x, int y)
				{
					int num10 = ItemID.Sets.SortingPriorityWiring[inv[y].netID].CompareTo(ItemID.Sets.SortingPriorityWiring[inv[x].netID]);
					if (num10 == 0)
					{
						num10 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					}
					if (num10 == 0)
					{
						num10 = inv[y].netID.CompareTo(inv[x].netID);
					}
					if (num10 == 0)
					{
						num10 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num10 == 0)
					{
						num10 = x.CompareTo(y);
					}
					return num10;
				});
				return indexesSortable10;
			});

			// Token: 0x0400697C RID: 27004
			public static ItemSorting.ItemSortingLayer MiscMaterials = new ItemSorting.ItemSortingLayer("Misc - Materials", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable9 = (from i in itemsToSort
				where inv[i].netID > 0 && ItemID.Sets.SortingPriorityMaterials[inv[i].netID] > -1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable9, inv);
				foreach (int item30 in indexesSortable9)
				{
					itemsToSort.Remove(item30);
				}
				indexesSortable9.Sort(delegate(int x, int y)
				{
					int num9 = ItemID.Sets.SortingPriorityMaterials[inv[y].netID].CompareTo(ItemID.Sets.SortingPriorityMaterials[inv[x].netID]);
					if (num9 == 0)
					{
						num9 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num9 == 0)
					{
						num9 = x.CompareTo(y);
					}
					return num9;
				});
				return indexesSortable9;
			});

			// Token: 0x0400697D RID: 27005
			public static ItemSorting.ItemSortingLayer MiscExtractinator = new ItemSorting.ItemSortingLayer("Misc - Extractinator", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable8 = (from i in itemsToSort
				where inv[i].netID > 0 && ItemID.Sets.SortingPriorityExtractibles[inv[i].netID] > -1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable8, inv);
				foreach (int item31 in indexesSortable8)
				{
					itemsToSort.Remove(item31);
				}
				indexesSortable8.Sort(delegate(int x, int y)
				{
					int num8 = ItemID.Sets.SortingPriorityExtractibles[inv[y].netID].CompareTo(ItemID.Sets.SortingPriorityExtractibles[inv[x].netID]);
					if (num8 == 0)
					{
						num8 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num8 == 0)
					{
						num8 = x.CompareTo(y);
					}
					return num8;
				});
				return indexesSortable8;
			});

			// Token: 0x0400697E RID: 27006
			public static ItemSorting.ItemSortingLayer MiscPainting = new ItemSorting.ItemSortingLayer("Misc - Painting", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable7 = (from i in itemsToSort
				where (inv[i].netID > 0 && ItemID.Sets.SortingPriorityPainting[inv[i].netID] > -1) || inv[i].paint > 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable7, inv);
				foreach (int item32 in indexesSortable7)
				{
					itemsToSort.Remove(item32);
				}
				indexesSortable7.Sort(delegate(int x, int y)
				{
					int num7 = ItemID.Sets.SortingPriorityPainting[inv[y].netID].CompareTo(ItemID.Sets.SortingPriorityPainting[inv[x].netID]);
					if (num7 == 0)
					{
						num7 = inv[x].paint.CompareTo(inv[y].paint);
					}
					if (num7 == 0)
					{
						num7 = inv[x].paintCoating.CompareTo(inv[y].paintCoating);
					}
					if (num7 == 0)
					{
						num7 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num7 == 0)
					{
						num7 = x.CompareTo(y);
					}
					return num7;
				});
				return indexesSortable7;
			});

			// Token: 0x0400697F RID: 27007
			public static ItemSorting.ItemSortingLayer MiscRopes = new ItemSorting.ItemSortingLayer("Misc - Ropes", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable6 = (from i in itemsToSort
				where inv[i].netID > 0 && ItemID.Sets.SortingPriorityRopes[inv[i].netID] > -1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable6, inv);
				foreach (int item33 in indexesSortable6)
				{
					itemsToSort.Remove(item33);
				}
				indexesSortable6.Sort(delegate(int x, int y)
				{
					int num6 = ItemID.Sets.SortingPriorityRopes[inv[y].netID].CompareTo(ItemID.Sets.SortingPriorityRopes[inv[x].netID]);
					if (num6 == 0)
					{
						num6 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num6 == 0)
					{
						num6 = x.CompareTo(y);
					}
					return num6;
				});
				return indexesSortable6;
			});

			// Token: 0x04006980 RID: 27008
			public static ItemSorting.ItemSortingLayer LastMaterials = new ItemSorting.ItemSortingLayer("Last - Materials", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable5 = (from i in itemsToSort
				where inv[i].createTile < 0 && inv[i].createWall < 1
				select i).ToList<int>();
				layer.Validate(ref indexesSortable5, inv);
				foreach (int item34 in indexesSortable5)
				{
					itemsToSort.Remove(item34);
				}
				indexesSortable5.Sort(delegate(int x, int y)
				{
					int num5 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num5 == 0)
					{
						num5 = inv[y].value.CompareTo(inv[x].value);
					}
					if (num5 == 0)
					{
						num5 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num5 == 0)
					{
						num5 = x.CompareTo(y);
					}
					return num5;
				});
				return indexesSortable5;
			});

			// Token: 0x04006981 RID: 27009
			public static ItemSorting.ItemSortingLayer LastTilesImportant = new ItemSorting.ItemSortingLayer("Last - Tiles (Frame Important)", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable4 = (from i in itemsToSort
				where inv[i].createTile >= 0 && Main.tileFrameImportant[inv[i].createTile]
				select i).ToList<int>();
				layer.Validate(ref indexesSortable4, inv);
				foreach (int item35 in indexesSortable4)
				{
					itemsToSort.Remove(item35);
				}
				indexesSortable4.Sort(delegate(int x, int y)
				{
					int num4 = string.Compare(inv[x].Name, inv[y].Name, StringComparison.OrdinalIgnoreCase);
					if (num4 == 0)
					{
						num4 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num4 == 0)
					{
						num4 = x.CompareTo(y);
					}
					return num4;
				});
				return indexesSortable4;
			});

			// Token: 0x04006982 RID: 27010
			public static ItemSorting.ItemSortingLayer LastTilesCommon = new ItemSorting.ItemSortingLayer("Last - Tiles (Common), Walls", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable3 = (from i in itemsToSort
				where inv[i].createWall > 0 || inv[i].createTile >= 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable3, inv);
				foreach (int item36 in indexesSortable3)
				{
					itemsToSort.Remove(item36);
				}
				indexesSortable3.Sort(delegate(int x, int y)
				{
					int num3 = string.Compare(inv[x].Name, inv[y].Name, StringComparison.OrdinalIgnoreCase);
					if (num3 == 0)
					{
						num3 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num3 == 0)
					{
						num3 = x.CompareTo(y);
					}
					return num3;
				});
				return indexesSortable3;
			});

			// Token: 0x04006983 RID: 27011
			public static ItemSorting.ItemSortingLayer LastNotTrash = new ItemSorting.ItemSortingLayer("Last - Not Trash", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable2 = (from i in itemsToSort
				where inv[i].OriginalRarity >= 0
				select i).ToList<int>();
				layer.Validate(ref indexesSortable2, inv);
				foreach (int item37 in indexesSortable2)
				{
					itemsToSort.Remove(item37);
				}
				indexesSortable2.Sort(delegate(int x, int y)
				{
					int num2 = inv[y].OriginalRarity.CompareTo(inv[x].OriginalRarity);
					if (num2 == 0)
					{
						num2 = string.Compare(inv[x].Name, inv[y].Name, StringComparison.OrdinalIgnoreCase);
					}
					if (num2 == 0)
					{
						num2 = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num2 == 0)
					{
						num2 = x.CompareTo(y);
					}
					return num2;
				});
				return indexesSortable2;
			});

			// Token: 0x04006984 RID: 27012
			public static ItemSorting.ItemSortingLayer LastTrash = new ItemSorting.ItemSortingLayer("Last - Trash", delegate(ItemSorting.ItemSortingLayer layer, Item[] inv, List<int> itemsToSort)
			{
				List<int> indexesSortable = new List<int>(itemsToSort);
				layer.Validate(ref indexesSortable, inv);
				foreach (int item38 in indexesSortable)
				{
					itemsToSort.Remove(item38);
				}
				indexesSortable.Sort(delegate(int x, int y)
				{
					int num = inv[y].value.CompareTo(inv[x].value);
					if (num == 0)
					{
						num = inv[y].stack.CompareTo(inv[x].stack);
					}
					if (num == 0)
					{
						num = x.CompareTo(y);
					}
					return num;
				});
				return indexesSortable;
			});
		}
	}
}
