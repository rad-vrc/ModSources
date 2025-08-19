// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.HomewardJourneyPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Accessories.PermanentUpgradesSystem;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"ContinentOfJourney"})]
[JITWhenModsEnabled(new string[] {"ContinentOfJourney"})]
public class HomewardJourneyPermanentUpgradeTooltip : GlobalItem
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
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Americano") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Americano > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Latte") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Latte > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Mocha") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Mocha > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "Cappuccino") && Main.LocalPlayer.GetModPlayer<CoffeePlayer>().Cappuccino > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "AirHandcanon") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().AirHandcanon > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "HotCase") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().HotCase > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "GreatCrystal") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().GreatCrystal > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "WhimInABottle") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().WhimInABottle > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.homewardJourneyMod, "SunsHeart") && Main.LocalPlayer.GetModPlayer<OtherUpgradesPlayer>().SunsHeart > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type != Common.GetModItem(ModConditions.homewardJourneyMod, "TheSwitch") || !Main.LocalPlayer.GetModPlayer<PermanentUpgradesPlayer>().PermanentUpgradesActivated[0] || !Main.LocalPlayer.GetModPlayer<PermanentUpgradesPlayer>().PermanentUpgradesActivated[1])
      return;
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
