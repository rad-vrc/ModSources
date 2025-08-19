// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.CalamityPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using CalamityMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"CalamityMod"})]
[JITWhenModsEnabled(new string[] {"CalamityMod"})]
public class CalamityPermanentUpgradeTooltip : GlobalItem
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
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "EnchantedStarfish"))
    {
      tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) Main.LocalPlayer.ConsumedManaCrystals, (object) 9);
      Common.AddLastTooltip(tooltips, tooltip);
    }
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "MushroomPlasmaRoot") && CalamityUtils.Calamity(Main.LocalPlayer).rageBoostOne)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "InfernalBlood") && CalamityUtils.Calamity(Main.LocalPlayer).rageBoostTwo)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "RedLightningContainer") && CalamityUtils.Calamity(Main.LocalPlayer).rageBoostThree)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "ElectrolyteGelPack") && CalamityUtils.Calamity(Main.LocalPlayer).adrenalineBoostOne)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "StarlightFuelCell") && CalamityUtils.Calamity(Main.LocalPlayer).adrenalineBoostTwo)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.calamityMod, "Ectoheart") && CalamityUtils.Calamity(Main.LocalPlayer).adrenalineBoostThree)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type != Common.GetModItem(ModConditions.calamityMod, "CelestialOnion") || !CalamityUtils.Calamity(Main.LocalPlayer).extraAccessoryML)
      return;
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
