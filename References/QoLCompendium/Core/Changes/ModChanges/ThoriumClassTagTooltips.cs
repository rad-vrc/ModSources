// Decompiled with JetBrains decompiler
// Type: QoLCompendium.Core.Changes.ModChanges.ThoriumClassTagTooltips
// Assembly: QoLCompendium, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03199677-CF03-4A96-8142-252B5FBEAF41
// Assembly location: D:\dorad\Documents\My Games\Terraria\tModLoader\ModSources\References\QoLCompendium\QoLCompendium.dll

using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items;
using ThoriumMod.Utilities;

#nullable disable
namespace QoLCompendium.Core.Changes.ModChanges;

[ExtendsFromMod(new string[] {"ThoriumMod"})]
[JITWhenModsEnabled(new string[] {"ThoriumMod"})]
public class ThoriumClassTagTooltips : GlobalItem
{
  public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
  {
    if (QoLCompendium.QoLCompendium.tooltipConfig.ClassTagTooltip && !ThoriumConfigClient.Instance.ShowClassTags)
      ThoriumClassTagTooltips.ItemClassTooltip(item, tooltips);
    if (!QoLCompendium.QoLCompendium.tooltipConfig.ClassTagTooltip)
      return;
    ThoriumClassTagTooltips.ThrowerClassTooltip(item, tooltips);
  }

  public static void ItemClassTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (ModConditions.thoriumLoaded && item.ModItem is ThoriumItem modItem1 && modItem1.isThrower && !item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")))
      tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ThrowerClass")));
    if (ModConditions.thoriumLoaded && item.ModItem is ThoriumItem modItem2)
    {
      if (modItem2.isHealer && !modItem2.isDarkHealer && !PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).darkAura)
        tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HealerClass")));
      if (modItem2.isDarkHealer || PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).darkAura)
        tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.DarkHealerClass")));
    }
    if (!ModConditions.thoriumLoaded || !(item.ModItem is BardItem))
      return;
    tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.BardClass")));
  }

  public static void ThrowerClassTooltip(Item item, List<TooltipLine> tooltips)
  {
    if (!item.CountsAsClass(DamageClass.Throwing) || item.accessory || item.ModItem is ThoriumItem || item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")))
      return;
    tooltips.Insert(1, new TooltipLine((Mod) QoLCompendium.QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ThrowerClass")));
  }
}
