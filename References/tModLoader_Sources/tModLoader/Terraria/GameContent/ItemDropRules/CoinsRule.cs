using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005EE RID: 1518
	public class CoinsRule : IItemDropRule
	{
		// Token: 0x0600437C RID: 17276 RVA: 0x005FFC0F File Offset: 0x005FDE0F
		public CoinsRule(long value, bool withRandomBonus = false)
		{
			this.value = value;
			this.withRandomBonus = withRandomBonus;
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x005FFC30 File Offset: 0x005FDE30
		// (set) Token: 0x0600437E RID: 17278 RVA: 0x005FFC38 File Offset: 0x005FDE38
		public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; } = new List<IItemDropRuleChainAttempt>();

		// Token: 0x0600437F RID: 17279 RVA: 0x005FFC41 File Offset: 0x005FDE41
		public bool CanDrop(DropAttemptInfo info)
		{
			return true;
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x005FFC44 File Offset: 0x005FDE44
		public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
		{
			double scale = 1.0;
			if (this.withRandomBonus)
			{
				scale += (double)((float)info.rng.Next(-20, 21) * 0.01f);
				if (info.rng.Next(5) == 0)
				{
					scale += (double)((float)info.rng.Next(5, 11) * 0.01f);
				}
				if (info.rng.Next(10) == 0)
				{
					scale += (double)((float)info.rng.Next(10, 21) * 0.01f);
				}
				if (info.rng.Next(15) == 0)
				{
					scale += (double)((float)info.rng.Next(15, 31) * 0.01f);
				}
				if (info.rng.Next(20) == 0)
				{
					scale += (double)((float)info.rng.Next(20, 41) * 0.01f);
				}
			}
			foreach (ValueTuple<int, int> valueTuple in CoinsRule.ToCoins((long)((double)this.value * scale)))
			{
				int itemId = valueTuple.Item1;
				int count = valueTuple.Item2;
				CommonCode.DropItem(info, itemId, count, false);
			}
			return new ItemDropAttemptResult
			{
				State = ItemDropAttemptResultState.Success
			};
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x005FFD8C File Offset: 0x005FDF8C
		[return: TupleElementNames(new string[]
		{
			"itemId",
			"count"
		})]
		public static IEnumerable<ValueTuple<int, int>> ToCoins(long money)
		{
			int copper = (int)(money % 100L);
			money /= 100L;
			int silver = (int)(money % 100L);
			money /= 100L;
			int gold = (int)(money % 100L);
			money /= 100L;
			int plat = (int)money;
			if (copper > 0)
			{
				yield return new ValueTuple<int, int>(71, copper);
			}
			if (silver > 0)
			{
				yield return new ValueTuple<int, int>(72, silver);
			}
			if (gold > 0)
			{
				yield return new ValueTuple<int, int>(73, gold);
			}
			if (plat > 0)
			{
				yield return new ValueTuple<int, int>(74, plat);
			}
			yield break;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x005FFD9C File Offset: 0x005FDF9C
		public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
		{
			foreach (ValueTuple<int, int> valueTuple in CoinsRule.ToCoins(this.value))
			{
				int itemId = valueTuple.Item1;
				int count = valueTuple.Item2;
				drops.Add(new DropRateInfo(itemId, count, count, ratesInfo.parentDroprateChance, ratesInfo.conditions));
			}
			Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
		}

		// Token: 0x04005A2D RID: 23085
		public long value;

		// Token: 0x04005A2E RID: 23086
		public bool withRandomBonus;
	}
}
