using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000619 RID: 1561
	public class SlimeBodyItemDropRule : IItemDropRule
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x0060B005 File Offset: 0x00609205
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x0060B00D File Offset: 0x0060920D
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x060044AC RID: 17580 RVA: 0x0060B016 File Offset: 0x00609216
		public SlimeBodyItemDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x0060B029 File Offset: 0x00609229
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.type == 1 && info.npc.ai[1] > 0f && info.npc.ai[1] < (float)ItemLoader.ItemCount;
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x0060B064 File Offset: 0x00609264
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			int itemId = (int)info.npc.ai[1];
			int amountDroppedMinimum;
			int amountDroppedMaximum;
			this.GetDropInfo(itemId, out amountDroppedMinimum, out amountDroppedMaximum);
			CommonCode.DropItem(info, itemId, info.rng.Next(amountDroppedMinimum, amountDroppedMaximum + 1), false);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x0060B0B4 File Offset: 0x006092B4
		public void GetDropInfo(int itemId, out int amountDroppedMinimum, out int amountDroppedMaximum)
		{
			amountDroppedMinimum = 1;
			amountDroppedMaximum = 1;
			if (itemId <= 73)
			{
				if (itemId != 8)
				{
					switch (itemId)
					{
					case 71:
						amountDroppedMinimum = 50;
						amountDroppedMaximum = 99;
						break;
					case 72:
						amountDroppedMinimum = 20;
						amountDroppedMaximum = 99;
						break;
					case 73:
						amountDroppedMinimum = 1;
						amountDroppedMaximum = 2;
						break;
					}
				}
				else
				{
					amountDroppedMinimum = 5;
					amountDroppedMaximum = 10;
				}
			}
			else if (itemId != 166)
			{
				if (itemId != 965)
				{
					if (itemId - 4343 <= 1)
					{
						amountDroppedMinimum = 2;
						amountDroppedMaximum = 5;
					}
				}
				else
				{
					amountDroppedMinimum = 20;
					amountDroppedMaximum = 45;
				}
			}
			else
			{
				amountDroppedMinimum = 2;
				amountDroppedMaximum = 6;
			}
			ValueTuple<int, int> minMaxStack;
			if (ItemID.Sets.OreDropsFromSlime.TryGetValue(itemId, out minMaxStack))
			{
				amountDroppedMinimum = minMaxStack.Item1;
				amountDroppedMaximum = minMaxStack.Item2;
			}
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x0060B15F File Offset: 0x0060935F
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}
	}
}
