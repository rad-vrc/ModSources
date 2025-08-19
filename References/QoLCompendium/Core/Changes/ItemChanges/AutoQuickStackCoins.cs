// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.AutoQuickStackCoins
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class AutoQuickStackCoins : GlobalItem
{
  public virtual bool OnPickup(Item item, Player player)
  {
    return !QoLCompendium.QoLCompendium.mainConfig.AutoMoneyQuickStack ? base.OnPickup(item, player) : !AutoQuickStackCoins.TryDepositACoin(item, player);
  }

  public static bool TryDepositACoin(Item item, Player player)
  {
    if (!((IEnumerable<Item>) player.bank.item).Any<Item>((Func<Item, bool>) (i =>
    {
      if (item.type == 74)
      {
        if (item.type == i.type && i.stack < i.maxStack)
          return true;
      }
      else if (item.IsACoin && item.type == i.type)
        return true;
      return i.IsAir;
    })))
      return false;
    int type = item.type;
    if (!item.IsACoin)
      return false;
    ulong coinValue = Common.CalculateCoinValue(type, (uint) item.stack);
    List<Item> coins = Common.ConvertCopperValueToCoins(((IEnumerable<Item>) player.bank.item).Aggregate<Item, ulong>(coinValue, (Func<ulong, Item, ulong>) ((current, bItem) => current + Common.CalculateCoinValue(bItem.type, (uint) bItem.stack))));
    AutoQuickStackCoins.ReplaceOrPlaceIntoChest(player.bank, coins);
    coins.ForEach((Action<Item>) (coinLeft =>
    {
      if (coinLeft.IsAir)
        return;
      player.QuickSpawnItem(((Entity) player).GetSource_DropAsItem((string) null), coinLeft, coinLeft.stack);
    }));
    PopupText.NewText((PopupTextContext) 0, item, item.stack, false, false);
    SoundEngine.PlaySound(ref SoundID.CoinPickup, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
    return true;
  }

  public static void ReplaceOrPlaceIntoChest(Chest chest, List<Item> items)
  {
    List<int> intList = new List<int>();
    using (List<Item>.Enumerator enumerator = items.GetEnumerator())
    {
label_12:
      while (enumerator.MoveNext())
      {
        Item current = enumerator.Current;
        for (int index = 0; index < chest.item.Length; ++index)
        {
          if (!intList.Contains(index) && chest.item[index].type == current.type)
          {
            chest.item[index] = current.Clone();
            current.TurnToAir(false);
            intList.Add(index);
            goto label_12;
          }
        }
        for (int index = 0; index < chest.item.Length; ++index)
        {
          if (!intList.Contains(index) && chest.item[index].stack == 0)
          {
            chest.item[index] = current.Clone();
            current.TurnToAir(false);
            intList.Add(index);
            break;
          }
        }
      }
    }
  }
}
