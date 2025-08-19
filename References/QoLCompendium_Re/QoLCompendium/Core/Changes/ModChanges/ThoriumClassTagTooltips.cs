using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items;
using ThoriumMod.Utilities;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000258 RID: 600
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	public class ThoriumClassTagTooltips : GlobalItem
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x0007057F File Offset: 0x0006E77F
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (QoLCompendium.tooltipConfig.ClassTagTooltip && !ThoriumConfigClient.Instance.ShowClassTags)
			{
				ThoriumClassTagTooltips.ItemClassTooltip(item, tooltips);
			}
			if (QoLCompendium.tooltipConfig.ClassTagTooltip)
			{
				ThoriumClassTagTooltips.ThrowerClassTooltip(item, tooltips);
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000705B4 File Offset: 0x0006E7B4
		public static void ItemClassTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (ModConditions.thoriumLoaded)
			{
				ThoriumItem throwerItem = item.ModItem as ThoriumItem;
				if (throwerItem != null && throwerItem.isThrower && !item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")))
				{
					tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ThrowerClass")));
				}
			}
			if (ModConditions.thoriumLoaded)
			{
				ThoriumItem healerItem = item.ModItem as ThoriumItem;
				if (healerItem != null)
				{
					if (healerItem.isHealer && !healerItem.isDarkHealer && !PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).darkAura)
					{
						tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.HealerClass")));
					}
					if (healerItem.isDarkHealer || PlayerHelper.GetThoriumPlayer(Main.LocalPlayer).darkAura)
					{
						tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.DarkHealerClass")));
					}
				}
			}
			if (ModConditions.thoriumLoaded && item.ModItem is BardItem)
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.BardClass")));
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000706E0 File Offset: 0x0006E8E0
		public static void ThrowerClassTooltip(Item item, List<TooltipLine> tooltips)
		{
			if (item.CountsAsClass(DamageClass.Throwing) && !item.accessory && !(item.ModItem is ThoriumItem) && !item.CountsAsClass(Common.GetModDamageClass(ModConditions.calamityMod, "RogueDamageClass")))
			{
				tooltips.Insert(1, new TooltipLine(QoLCompendium.instance, "DamageClassType", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.ThrowerClass")));
			}
		}
	}
}
