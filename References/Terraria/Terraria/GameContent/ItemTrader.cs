using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x020001CA RID: 458
	public class ItemTrader
	{
		// Token: 0x06001BD4 RID: 7124 RVA: 0x004F02F8 File Offset: 0x004EE4F8
		public void AddOption_Interchangable(int itemType1, int itemType2)
		{
			this.AddOption_OneWay(itemType1, 1, itemType2, 1);
			this.AddOption_OneWay(itemType2, 1, itemType1, 1);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x004F0310 File Offset: 0x004EE510
		public void AddOption_CyclicLoop(params int[] typesInOrder)
		{
			for (int i = 0; i < typesInOrder.Length - 1; i++)
			{
				this.AddOption_OneWay(typesInOrder[i], 1, typesInOrder[i + 1], 1);
			}
			this.AddOption_OneWay(typesInOrder[typesInOrder.Length - 1], 1, typesInOrder[0], 1);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x004F0350 File Offset: 0x004EE550
		public void AddOption_FromAny(int givingItemType, params int[] takingItemTypes)
		{
			for (int i = 0; i < takingItemTypes.Length; i++)
			{
				this.AddOption_OneWay(takingItemTypes[i], 1, givingItemType, 1);
			}
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x004F0377 File Offset: 0x004EE577
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

		// Token: 0x06001BD8 RID: 7128 RVA: 0x004F03A8 File Offset: 0x004EE5A8
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

		// Token: 0x06001BD9 RID: 7129 RVA: 0x004F03FC File Offset: 0x004EE5FC
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

		// Token: 0x0400434F RID: 17231
		public static ItemTrader ChlorophyteExtractinator = ItemTrader.CreateChlorophyteExtractinator();

		// Token: 0x04004350 RID: 17232
		private List<ItemTrader.TradeOption> _options = new List<ItemTrader.TradeOption>();

		// Token: 0x020005F1 RID: 1521
		public class TradeOption
		{
			// Token: 0x06003308 RID: 13064 RVA: 0x00604D2D File Offset: 0x00602F2D
			public bool WillTradeFor(int offeredItemType, int offeredItemStack)
			{
				return offeredItemType == this.TakingItemType && offeredItemStack >= this.TakingItemStack;
			}

			// Token: 0x04006000 RID: 24576
			public int TakingItemType;

			// Token: 0x04006001 RID: 24577
			public int TakingItemStack;

			// Token: 0x04006002 RID: 24578
			public int GivingITemType;

			// Token: 0x04006003 RID: 24579
			public int GivingItemStack;
		}
	}
}
