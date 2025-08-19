// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.RagnarokPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"ThoriumMod", "RagnarokMod"})]
[JITWhenModsEnabled(new string[] {"ThoriumMod", "RagnarokMod"})]
public class RagnarokPermanentUpgradeTooltip : GlobalItem
{
  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    if (!QoLCompendium.QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
      return;
    this.UsedPermanentUpgrade(item, tooltips);
  }

  public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
  {
    TooltipLine tooltip = new TooltipLine(((ModType) this).Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"))
    {
      OverrideColor = new Color?(Color.LightGreen)
    };
    int num1 = (int) ModConditions.thoriumMod.Call(new object[2]
    {
      (object) "GetBardInspirationMax",
      (object) Main.LocalPlayer
    });
    int num2 = 40;
    if (item.type != Common.GetModItem(ModConditions.ragnarokMod, "InspirationEssence"))
      return;
    tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Math.Clamp(Math.Max(num1 - num2, 0), 0, 10), (object) 10);
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
