using System;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x02000294 RID: 660
	public class SlimeBodyItemDropRule : IItemDropRule
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x005189E2 File Offset: 0x00516BE2
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x005189EA File Offset: 0x00516BEA
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

		// Token: 0x06002054 RID: 8276 RVA: 0x005189F3 File Offset: 0x00516BF3
		public SlimeBodyItemDropRule()
		{
			this.ChainedRules = new List<IItemDropRuleChainAttempt>();
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x00518A06 File Offset: 0x00516C06
		public bool CanDrop(DropAttemptInfo info)
		{
			return info.npc.type == 1 && info.npc.ai[1] > 0f && info.npc.ai[1] < (float)ItemID.Count;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00518A44 File Offset: 0x00516C44
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			int itemId = (int)info.npc.ai[1];
			int minValue;
			int num;
			this.GetDropInfo(itemId, out minValue, out num);
			CommonCode.DropItemFromNPC(info.npc, itemId, info.rng.Next(minValue, num + 1), false);
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x00518A98 File Offset: 0x00516C98
		public void GetDropInfo(int itemId, out int amountDroppedMinimum, out int amountDroppedMaximum)
		{
			amountDroppedMinimum = 1;
			amountDroppedMaximum = 1;
			if (itemId <= 73)
			{
				if (itemId == 8)
				{
					amountDroppedMinimum = 5;
					amountDroppedMaximum = 10;
					return;
				}
				if (itemId - 11 > 3)
				{
					switch (itemId)
					{
					case 71:
						amountDroppedMinimum = 50;
						amountDroppedMaximum = 99;
						return;
					case 72:
						amountDroppedMinimum = 20;
						amountDroppedMaximum = 99;
						return;
					case 73:
						amountDroppedMinimum = 1;
						amountDroppedMaximum = 2;
						return;
					default:
						return;
					}
				}
			}
			else if (itemId <= 702)
			{
				if (itemId == 166)
				{
					amountDroppedMinimum = 2;
					amountDroppedMaximum = 6;
					return;
				}
				if (itemId - 699 > 3)
				{
					return;
				}
			}
			else
			{
				if (itemId == 965)
				{
					amountDroppedMinimum = 20;
					amountDroppedMaximum = 45;
					return;
				}
				if (itemId - 4343 > 1)
				{
					return;
				}
				amountDroppedMinimum = 2;
				amountDroppedMaximum = 5;
				return;
			}
			amountDroppedMinimum = 3;
			amountDroppedMaximum = 13;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x00518B3E File Offset: 0x00516D3E
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}
	}
}
