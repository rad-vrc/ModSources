using System;
using System.Collections.Generic;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000184 RID: 388
	public class StarterBag : ModItem
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x00015FAC File Offset: 0x000141AC
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.StarterBag;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00015FC6 File Offset: 0x000141C6
		public override void SetDefaults()
		{
			base.Item.width = 15;
			base.Item.height = 12;
			base.Item.SetShopValues(ItemRarityColor.White0, Item.buyPrice(0, 0, 0, 0));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00015FF7 File Offset: 0x000141F7
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.StarterBag);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001600F File Offset: 0x0001420F
		public override bool CanRightClick()
		{
			return QoLCompendium.itemConfig.CustomItems != null;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00016020 File Offset: 0x00014220
		public override void RightClick(Player player)
		{
			if (QoLCompendium.itemConfig.CustomItems != null)
			{
				if (QoLCompendium.itemConfig.CustomItemQuantities != null)
				{
					this.loadCount = true;
				}
				for (int i = 0; i < QoLCompendium.itemConfig.CustomItems.Count; i++)
				{
					this.type = QoLCompendium.itemConfig.CustomItems[i].Type;
					if (this.loadCount)
					{
						if (i <= QoLCompendium.itemConfig.CustomItemQuantities.Count - 1)
						{
							player.QuickSpawnItem(null, this.type, QoLCompendium.itemConfig.CustomItemQuantities[i]);
						}
						else
						{
							player.QuickSpawnItem(null, this.type, 1);
						}
					}
					else
					{
						player.QuickSpawnItem(null, this.type, 1);
					}
				}
			}
		}

		// Token: 0x04000039 RID: 57
		public int type;

		// Token: 0x0400003A RID: 58
		public int curItem;

		// Token: 0x0400003B RID: 59
		public bool loadCount;
	}
}
