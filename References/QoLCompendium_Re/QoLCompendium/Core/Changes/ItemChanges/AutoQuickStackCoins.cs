using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ItemChanges
{
	// Token: 0x0200025A RID: 602
	public class AutoQuickStackCoins : GlobalItem
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x0007097C File Offset: 0x0006EB7C
		public override bool OnPickup(Item item, Player player)
		{
			if (!QoLCompendium.mainConfig.AutoMoneyQuickStack)
			{
				return base.OnPickup(item, player);
			}
			return !AutoQuickStackCoins.TryDepositACoin(item, player);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000709A0 File Offset: 0x0006EBA0
		public static bool TryDepositACoin(Item item, Player player)
		{
			if (!player.bank.item.Any(delegate(Item i)
			{
				if (item.type == 74)
				{
					if (item.type == i.type && i.stack < i.maxStack)
					{
						return true;
					}
				}
				else if (item.IsACoin && item.type == i.type)
				{
					return true;
				}
				return i.IsAir;
			}))
			{
				return false;
			}
			int type = item.type;
			if (!item.IsACoin)
			{
				return false;
			}
			ulong totalMoney = Common.CalculateCoinValue(type, (uint)item.stack);
			totalMoney = player.bank.item.Aggregate(totalMoney, (ulong current, Item bItem) => current + Common.CalculateCoinValue(bItem.type, (uint)bItem.stack));
			List<Item> toPlace = Common.ConvertCopperValueToCoins(totalMoney);
			AutoQuickStackCoins.ReplaceOrPlaceIntoChest(player.bank, toPlace);
			toPlace.ForEach(delegate(Item coinLeft)
			{
				if (coinLeft.IsAir)
				{
					return;
				}
				player.QuickSpawnItem(player.GetSource_DropAsItem(null), coinLeft, coinLeft.stack);
			});
			PopupText.NewText(PopupTextContext.RegularItemPickup, item, item.stack, false, false);
			SoundEngine.PlaySound(SoundID.CoinPickup, new Vector2?(player.position), null);
			return true;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00070AAC File Offset: 0x0006ECAC
		public static void ReplaceOrPlaceIntoChest(Chest chest, List<Item> items)
		{
			List<int> toIgnore = new List<int>();
			using (List<Item>.Enumerator enumerator = items.GetEnumerator())
			{
				IL_BA:
				while (enumerator.MoveNext())
				{
					Item item = enumerator.Current;
					for (int i = 0; i < chest.item.Length; i++)
					{
						if (!toIgnore.Contains(i) && chest.item[i].type == item.type)
						{
							chest.item[i] = item.Clone();
							item.TurnToAir(false);
							toIgnore.Add(i);
							goto IL_BA;
						}
					}
					for (int j = 0; j < chest.item.Length; j++)
					{
						if (!toIgnore.Contains(j) && chest.item[j].stack == 0)
						{
							chest.item[j] = item.Clone();
							item.TurnToAir(false);
							toIgnore.Add(j);
							break;
						}
					}
				}
			}
		}
	}
}
