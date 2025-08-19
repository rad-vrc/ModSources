// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.ExxoAvalonPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"Avalon"})]
[JITWhenModsEnabled(new string[] {"Avalon"})]
public class ExxoAvalonPermanentUpgradeTooltip : GlobalItem
{
  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    if (!QoLCompendium.QoLCompendium.tooltipConfig.UsedPermanentUpgradeTooltip)
      return;
    this.UsedPermanentUpgrade(item, tooltips);
  }

  public void UsedPermanentUpgrade(Item item, List<TooltipLine> tooltips)
  {
    TooltipLine tooltip = new TooltipLine(((ModType) this).Mod, "UsedItem", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.UsedItem"));
    tooltip.OverrideColor = new Color?(Color.LightGreen);
    if (item.type == QoLCompendium.Core.Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "StaminaCrystal"))
    {
      tooltip.Text = QoLCompendium.Core.Common.GetTooltipValue("UsedItemCountable", (object) (Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStam / 30 - 1), (object) 9);
      QoLCompendium.Core.Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type != QoLCompendium.Core.Common.GetModItem(ModConditions.exxoAvalonOriginsMod, "EnergyCrystal") || !Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().EnergyCrystal)
      return;
    QoLCompendium.Core.Common.AddLastTooltip(tooltips, tooltip);
  }
}
