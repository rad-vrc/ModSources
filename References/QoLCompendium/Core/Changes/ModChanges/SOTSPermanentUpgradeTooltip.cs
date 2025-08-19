// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.SOTSPermanentUpgradeTooltip
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using Microsoft.Xna.Framework;
using SOTS.Void;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"SOTS"})]
[JITWhenModsEnabled(new string[] {"SOTS"})]
public class SOTSPermanentUpgradeTooltip : GlobalItem
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
    VoidPlayer voidPlayer = VoidPlayer.ModPlayer(Main.LocalPlayer);
    if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "ScarletStar") && voidPlayer.voidStar > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VioletStar") && voidPlayer.voidStar > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type == Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "SoulHeart") && voidPlayer.voidSoul > 0)
      Common.AddLastTooltip(tooltips, tooltip);
    if (item.type != Common.GetModItem(ModConditions.secretsOfTheShadowsMod, "VoidenAnkh"))
      return;
    tooltip.Text = Common.GetTooltipValue("UsedItemCountable", (object) voidPlayer.voidAnkh, (object) 5);
    Common.AddLastTooltip(tooltips, tooltip);
  }
}
