using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.MobileStorages
{
	// Token: 0x020001A2 RID: 418
	public class KillMobileStorages : GlobalItem
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0001A74C File Offset: 0x0001894C
		public override bool AppliesToEntity(Item entity, bool lateInstantiation)
		{
			return entity.type == 3213 || entity.type == 4131 || entity.type == 5325 || entity.type == ModContent.ItemType<FlyingSafe>() || entity.type == ModContent.ItemType<EtherianConstruct>();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00002430 File Offset: 0x00000630
		public override bool CanRightClick(Item item)
		{
			return true;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001A79C File Offset: 0x0001899C
		public override void RightClick(Item item, Player player)
		{
			foreach (Projectile proj in Main.projectile)
			{
				if (Common.MobileStorages.Contains(proj.type) && player.ownedProjectileCounts[proj.type] > 0 && proj.owner == player.whoAmI)
				{
					proj.active = false;
				}
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001A7F8 File Offset: 0x000189F8
		public override void OnConsumeItem(Item item, Player player)
		{
			item.stack++;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001A808 File Offset: 0x00018A08
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			Common.AddLastTooltip(tooltips, new TooltipLine(base.Mod, "KillMobileStorage", Language.GetTextValue("Mods.QoLCompendium.CommonItemTooltips.KillMobileStorage"))
			{
				OverrideColor = new Color?(Color.Gray)
			});
		}
	}
}
