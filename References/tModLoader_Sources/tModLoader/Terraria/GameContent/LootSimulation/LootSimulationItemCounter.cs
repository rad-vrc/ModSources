using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.LootSimulation
{
	// Token: 0x020005E1 RID: 1505
	public class LootSimulationItemCounter
	{
		// Token: 0x06004330 RID: 17200 RVA: 0x005FC81F File Offset: 0x005FAA1F
		public void AddItem(int itemId, int amount, bool expert)
		{
			if (expert)
			{
				this._itemCountsObtainedExpert[itemId] += (long)amount;
				return;
			}
			this._itemCountsObtained[itemId] += (long)amount;
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x005FC84C File Offset: 0x005FAA4C
		public void Exclude(params int[] itemIds)
		{
			foreach (int num in itemIds)
			{
				this._itemCountsObtained[num] = 0L;
				this._itemCountsObtainedExpert[num] = 0L;
			}
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x005FC881 File Offset: 0x005FAA81
		public void IncreaseTimesAttempted(int amount, bool expert)
		{
			if (expert)
			{
				this._totalTimesAttemptedExpert += (long)amount;
				return;
			}
			this._totalTimesAttempted += (long)amount;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x005FC8A8 File Offset: 0x005FAAA8
		public string PrintCollectedItems(bool expert)
		{
			long[] collectionToUse = this._itemCountsObtained;
			long totalDropsAttempted = this._totalTimesAttempted;
			if (expert)
			{
				collectionToUse = this._itemCountsObtainedExpert;
				this._totalTimesAttempted = this._totalTimesAttemptedExpert;
			}
			IEnumerable<string> values = (from entry in collectionToUse.Select((long count, int itemId) => new
			{
				itemId,
				count
			})
			where entry.count > 0L
			select entry.itemId).Select(delegate(int itemId)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 3);
				defaultInterpolatedStringHandler.AppendLiteral("new ItemDropInfo(ItemID.");
				defaultInterpolatedStringHandler.AppendFormatted(ItemID.Search.GetName(itemId));
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<long>(collectionToUse[itemId]);
				defaultInterpolatedStringHandler.AppendLiteral(", ");
				defaultInterpolatedStringHandler.AppendFormatted<long>(totalDropsAttempted);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			});
			return string.Join(",\n", values);
		}

		// Token: 0x040059ED RID: 23021
		private long[] _itemCountsObtained = new long[ItemLoader.ItemCount];

		// Token: 0x040059EE RID: 23022
		private long[] _itemCountsObtainedExpert = new long[ItemLoader.ItemCount];

		// Token: 0x040059EF RID: 23023
		private long _totalTimesAttempted;

		// Token: 0x040059F0 RID: 23024
		private long _totalTimesAttemptedExpert;
	}
}
