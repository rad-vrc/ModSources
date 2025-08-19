// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ItemChanges.NotConsumableBossSummons
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ItemChanges;

public class NotConsumableBossSummons : GlobalItem
{
  public virtual bool AppliesToEntity(Item entity, bool lateInstantiation)
  {
    return Common.VanillaBossAndEventSummons.Contains(entity.type) && QoLCompendium.QoLCompendium.mainConfig.EndlessBossSummons && !ModConditions.calamityLoaded || Common.VanillaRightClickBossAndEventSummons.Contains(entity.type) && QoLCompendium.QoLCompendium.mainConfig.EndlessBossSummons || Common.ModdedBossAndEventSummons.Contains(entity.type) && QoLCompendium.QoLCompendium.mainConfig.EndlessBossSummons || Common.FargosBossAndEventSummons.Contains(entity.type) && QoLCompendium.QoLCompendium.mainConfig.EndlessBossSummons;
  }

  public virtual void SetDefaults(Item item)
  {
    item.consumable = false;
    if (Common.FargosBossAndEventSummons.Contains(item.type))
      return;
    item.maxStack = 1;
  }

  public virtual bool ConsumeItem(Item item, Player player) => false;

  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    TooltipLine tooltipLine1 = tooltips.Find((Predicate<TooltipLine>) (l => l.Name == "Tooltip0"));
    TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "NotConsumable", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.NotConsumable"));
    tooltips.Insert(tooltips.IndexOf(tooltipLine1), tooltipLine2);
  }
}
