// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.BuffChanges.NotConsumablePotions
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.BuffChanges;

public class NotConsumablePotions : GlobalItem
{
  public virtual bool ConsumeItem(Item item, Player player)
  {
    if (QoLCompendium.QoLCompendium.mainConfig.EndlessBuffs)
    {
      bool flag1 = item.buffTime > 0;
      bool flag2;
      switch (item.type)
      {
        case 678:
        case 2350:
        case 2351:
        case 2756:
        case 2997:
        case 4870:
          flag2 = true;
          break;
        default:
          flag2 = false;
          break;
      }
      bool flag3 = flag2;
      if (flag1 | flag3 && item.stack >= QoLCompendium.QoLCompendium.mainConfig.EndlessBuffAmount)
        return false;
    }
    return !QoLCompendium.QoLCompendium.mainConfig.EndlessHealing || (item.healLife > 0 ? 1 : (item.healMana > 0 ? 1 : 0)) == 0 || item.stack < QoLCompendium.QoLCompendium.mainConfig.EndlessHealingAmount;
  }
}
