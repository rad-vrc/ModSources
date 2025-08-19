// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.ClickerPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using ClickerClass;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"ClickerClass"})]
[JITWhenModsEnabled(new string[] {"ClickerClass"})]
public class ClickerPermanentUpgradeTooltip : GlobalItem
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
    if (item.type != Common.GetModItem(ModConditions.clickerClassMod, "HeavenlyChip") || !Main.LocalPlayer.GetModPlayer<ClickerPlayer>().consumedHeavenlyChip)
      return;
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
