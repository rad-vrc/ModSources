// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.MartainsOrderPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using MartainsOrder;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"MartainsOrder"})]
[JITWhenModsEnabled(new string[] {"MartainsOrder"})]
public class MartainsOrderPermanentUpgradeTooltip : GlobalItem
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
    if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FishOfPurity") && Main.LocalPlayer.GetModPlayer<MyPlayer>().fishOfPurity)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FishOfSpirit") && Main.LocalPlayer.GetModPlayer<MyPlayer>().fishOfSpirit)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FishOfWrath") && Main.LocalPlayer.GetModPlayer<MyPlayer>().fishOfWrath)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "ShimmerFish") && Main.LocalPlayer.GetModPlayer<MyPlayer>().shimmerFish)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "MerchantBag") && Main.LocalPlayer.GetModPlayer<MyPlayer>().shimmerMerchBag)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.martainsOrderMod, "FirstAidTreatments") && MartainWorld.firstAidTreatments)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type != Common.GetModItem(ModConditions.martainsOrderMod, "MartiniteBless") || !MartainWorld.martiniteBless)
      return;
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
