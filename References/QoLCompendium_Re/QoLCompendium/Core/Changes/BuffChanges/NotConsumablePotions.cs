using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.BuffChanges
{
	// Token: 0x02000267 RID: 615
	public class NotConsumablePotions : GlobalItem
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x00073624 File Offset: 0x00071824
		public override bool ConsumeItem(Item item, Player player)
		{
			if (QoLCompendium.mainConfig.EndlessBuffs)
			{
				bool isBuff = item.buffTime > 0;
				int type = item.type;
				if (type <= 2351)
				{
					if (type != 678 && type - 2350 > 1)
					{
						goto IL_55;
					}
				}
				else if (type != 2756 && type != 2997 && type != 4870)
				{
					goto IL_55;
				}
				bool flag = true;
				goto IL_57;
				IL_55:
				flag = false;
				IL_57:
				bool isSpecialBuffItem = flag;
				if ((isBuff || isSpecialBuffItem) && item.stack >= QoLCompendium.mainConfig.EndlessBuffAmount)
				{
					return false;
				}
			}
			return !QoLCompendium.mainConfig.EndlessHealing || (item.healLife <= 0 && item.healMana <= 0) || item.stack < QoLCompendium.mainConfig.EndlessHealingAmount;
		}
	}
}
