using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x02000277 RID: 631
	public class LootSimulationItemCounter
	{
		// Token: 0x06001FDE RID: 8158 RVA: 0x0051747C File Offset: 0x0051567C
		public void AddItem(int itemId, int amount, bool expert)
		{
			if (expert)
			{
				this._itemCountsObtainedExpert[itemId] += (long)amount;
				return;
			}
			this._itemCountsObtained[itemId] += (long)amount;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x005174A8 File Offset: 0x005156A8
		public void Exclude(params int[] itemIds)
		{
			foreach (int num in itemIds)
			{
				this._itemCountsObtained[num] = 0L;
				this._itemCountsObtainedExpert[num] = 0L;
			}
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x005174DD File Offset: 0x005156DD
		public void IncreaseTimesAttempted(int amount, bool expert)
		{
			if (expert)
			{
				this._totalTimesAttemptedExpert += (long)amount;
				return;
			}
			this._totalTimesAttempted += (long)amount;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x00517504 File Offset: 0x00515704
		public string PrintCollectedItems(bool expert)
		{
			long[] collectionToUse = this._itemCountsObtained;
			long totalDropsAttempted = this._totalTimesAttempted;
			if (expert)
			{
				collectionToUse = this._itemCountsObtainedExpert;
				this._totalTimesAttempted = this._totalTimesAttemptedExpert;
			}
			IEnumerable<string> values = from entry in collectionToUse.Select((long count, int itemId) => new
			{
				itemId,
				count
			})
			where entry.count > 0L
			select entry.itemId into itemId
			select string.Format("new ItemDropInfo(ItemID.{0}, {1}, {2})", ItemID.Search.GetName(itemId), collectionToUse[itemId], totalDropsAttempted);
			return string.Join(",\n", values);
		}

		// Token: 0x0400469F RID: 18079
		private long[] _itemCountsObtained = new long[(int)ItemID.Count];

		// Token: 0x040046A0 RID: 18080
		private long[] _itemCountsObtainedExpert = new long[(int)ItemID.Count];

		// Token: 0x040046A1 RID: 18081
		private long _totalTimesAttempted;

		// Token: 0x040046A2 RID: 18082
		private long _totalTimesAttemptedExpert;
	}
}
