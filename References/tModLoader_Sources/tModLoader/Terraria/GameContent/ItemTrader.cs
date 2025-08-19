using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x020004A2 RID: 1186
	public class ItemTrader
	{
		// Token: 0x06003949 RID: 14665 RVA: 0x005977A5 File Offset: 0x005959A5
		public void AddOption_Interchangable(int itemType1, int itemType2)
		{
			this.AddOption_OneWay(itemType1, 1, itemType2, 1);
			this.AddOption_OneWay(itemType2, 1, itemType1, 1);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x005977BC File Offset: 0x005959BC
		public void AddOption_CyclicLoop(params int[] typesInOrder)
		{
			for (int i = 0; i < typesInOrder.Length - 1; i++)
			{
				this.AddOption_OneWay(typesInOrder[i], 1, typesInOrder[i + 1], 1);
			}
			this.AddOption_OneWay(typesInOrder[typesInOrder.Length - 1], 1, typesInOrder[0], 1);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x005977FC File Offset: 0x005959FC
		public void AddOption_FromAny(int givingItemType, params int[] takingItemTypes)
		{
			for (int i = 0; i < takingItemTypes.Length; i++)
			{
				this.AddOption_OneWay(takingItemTypes[i], 1, givingItemType, 1);
			}
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x00597823 File Offset: 0x00595A23
		public void AddOption_OneWay(int takingItemType, int takingItemStack, int givingItemType, int givingItemStack)
		{
			this._options.Add(new ItemTrader.TradeOption
			{
				TakingItemType = takingItemType,
				TakingItemStack = takingItemStack,
				GivingITemType = givingItemType,
				GivingItemStack = givingItemStack
			});
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x00597854 File Offset: 0x00595A54
		public bool TryGetTradeOption(Item item, out ItemTrader.TradeOption option)
		{
			option = null;
			int type = item.type;
			int stack = item.stack;
			for (int i = 0; i < this._options.Count; i++)
			{
				ItemTrader.TradeOption tradeOption = this._options[i];
				if (tradeOption.WillTradeFor(type, stack))
				{
					option = tradeOption;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x005978A8 File Offset: 0x00595AA8
		public static ItemTrader CreateChlorophyteExtractinator()
		{
			ItemTrader itemTrader = new ItemTrader();
			itemTrader.AddOption_Interchangable(12, 699);
			itemTrader.AddOption_Interchangable(11, 700);
			itemTrader.AddOption_Interchangable(14, 701);
			itemTrader.AddOption_Interchangable(13, 702);
			itemTrader.AddOption_Interchangable(56, 880);
			itemTrader.AddOption_Interchangable(364, 1104);
			itemTrader.AddOption_Interchangable(365, 1105);
			itemTrader.AddOption_Interchangable(366, 1106);
			itemTrader.AddOption_CyclicLoop(new int[]
			{
				134,
				137,
				139
			});
			itemTrader.AddOption_Interchangable(20, 703);
			itemTrader.AddOption_Interchangable(22, 704);
			itemTrader.AddOption_Interchangable(21, 705);
			itemTrader.AddOption_Interchangable(19, 706);
			itemTrader.AddOption_Interchangable(57, 1257);
			itemTrader.AddOption_Interchangable(381, 1184);
			itemTrader.AddOption_Interchangable(382, 1191);
			itemTrader.AddOption_Interchangable(391, 1198);
			itemTrader.AddOption_Interchangable(86, 1329);
			itemTrader.AddOption_FromAny(3, new int[]
			{
				61,
				836,
				409
			});
			itemTrader.AddOption_FromAny(169, new int[]
			{
				370,
				1246,
				408
			});
			itemTrader.AddOption_FromAny(664, new int[]
			{
				833,
				835,
				834
			});
			itemTrader.AddOption_FromAny(3271, new int[]
			{
				3276,
				3277,
				3339
			});
			itemTrader.AddOption_FromAny(3272, new int[]
			{
				3274,
				3275,
				3338
			});
			return itemTrader;
		}

		// Token: 0x04005257 RID: 21079
		public static ItemTrader ChlorophyteExtractinator = ItemTrader.CreateChlorophyteExtractinator();

		// Token: 0x04005258 RID: 21080
		private List<ItemTrader.TradeOption> _options = new List<ItemTrader.TradeOption>();

		// Token: 0x02000BAD RID: 2989
		public class TradeOption
		{
			// Token: 0x06005D87 RID: 23943 RVA: 0x006C8CBA File Offset: 0x006C6EBA
			public bool WillTradeFor(int offeredItemType, int offeredItemStack)
			{
				return offeredItemType == this.TakingItemType && offeredItemStack >= this.TakingItemStack;
			}

			// Token: 0x040076BF RID: 30399
			public int TakingItemType;

			// Token: 0x040076C0 RID: 30400
			public int TakingItemStack;

			// Token: 0x040076C1 RID: 30401
			public int GivingITemType;

			// Token: 0x040076C2 RID: 30402
			public int GivingItemStack;
		}
	}
}
