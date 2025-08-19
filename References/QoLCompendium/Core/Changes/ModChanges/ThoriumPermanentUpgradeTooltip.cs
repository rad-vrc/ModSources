// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.ThoriumPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Utilities;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"ThoriumMod"})]
[JITWhenModsEnabled(new string[] {"ThoriumMod"})]
public class ThoriumPermanentUpgradeTooltip : GlobalItem
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
    if (item.type == Common.GetModItem(ModConditions.thoriumMod, "CrystalWave"))
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).consumedCrystalWaveCount, (object) 5);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type == Common.GetModItem(ModConditions.thoriumMod, "AstralWave") && PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).consumedAstralWave)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationGem") && PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).consumedInspirationGem)
      Common.AddLastTooltip(tooltips, tooltip);
    int num1 = (int) ModConditions.thoriumMod.Call(new object[2]
    {
      (object) "GetBardInspirationMax",
      (object) Main.LocalPlayer
    });
    int num2 = 10;
    int num3 = 20;
    int num4 = 30;
    if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationFragment"))
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Math.Clamp(Math.Max(num1 - num2, 0), 0, 10), (object) 10);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type == Common.GetModItem(ModConditions.thoriumMod, "InspirationShard"))
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Math.Clamp(Math.Max(num1 - num3, 0), 0, 10), (object) 10);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type != Common.GetModItem(ModConditions.thoriumMod, "InspirationCrystalNew"))
      return;
    tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Math.Clamp(Math.Max(num1 - num4, 0), 0, 10), (object) 10);
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
