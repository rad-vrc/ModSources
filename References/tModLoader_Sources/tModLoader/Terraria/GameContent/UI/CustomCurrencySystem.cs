using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004C8 RID: 1224
	public class CustomCurrencySystem
	{
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x005A9060 File Offset: 0x005A7260
		public long CurrencyCap
		{
			get
			{
				return this._currencyCap;
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x005A9068 File Offset: 0x005A7268
		public void Include(int coin, int howMuchIsItWorth)
		{
			this._valuePerUnit[coin] = howMuchIsItWorth;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x005A9077 File Offset: 0x005A7277
		public void SetCurrencyCap(long cap)
		{
			this._currencyCap = cap;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x005A9080 File Offset: 0x005A7280
		public virtual long CountCurrency(out bool overFlowing, Item[] inv, params int[] ignoreSlots)
		{
			List<int> list = new List<int>(ignoreSlots);
			long num = 0L;
			for (int i = 0; i < inv.Length; i++)
			{
				if (!list.Contains(i))
				{
					int value;
					if (this._valuePerUnit.TryGetValue(inv[i].type, out value))
					{
						num += (long)(value * inv[i].stack);
					}
					if (num >= this.CurrencyCap)
					{
						overFlowing = true;
						return this.CurrencyCap;
					}
				}
			}
			overFlowing = false;
			return num;
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x005A90EC File Offset: 0x005A72EC
		public virtual long CombineStacks(out bool overFlowing, params long[] coinCounts)
		{
			long num = 0L;
			foreach (long num2 in coinCounts)
			{
				num += num2;
				if (num >= this.CurrencyCap)
				{
					overFlowing = true;
					return this.CurrencyCap;
				}
			}
			overFlowing = false;
			return num;
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x005A912C File Offset: 0x005A732C
		public virtual bool TryPurchasing(long price, List<Item[]> inv, List<Point> slotCoins, List<Point> slotsEmpty, List<Point> slotEmptyBank, List<Point> slotEmptyBank2, List<Point> slotEmptyBank3, List<Point> slotEmptyBank4)
		{
			long num = price;
			Dictionary<Point, Item> dictionary = new Dictionary<Point, Item>();
			bool result = true;
			while (num > 0L)
			{
				long num2 = 1000000L;
				for (int i = 0; i < 4; i++)
				{
					if (num >= num2)
					{
						foreach (Point slotCoin in slotCoins)
						{
							if (inv[slotCoin.X][slotCoin.Y].type == 74 - i)
							{
								long num3 = num / num2;
								dictionary[slotCoin] = inv[slotCoin.X][slotCoin.Y].Clone();
								if (num3 < (long)inv[slotCoin.X][slotCoin.Y].stack)
								{
									inv[slotCoin.X][slotCoin.Y].stack -= (int)num3;
								}
								else
								{
									inv[slotCoin.X][slotCoin.Y].SetDefaults(0);
									slotsEmpty.Add(slotCoin);
								}
								num -= num2 * (long)(dictionary[slotCoin].stack - inv[slotCoin.X][slotCoin.Y].stack);
							}
						}
					}
					num2 /= 100L;
				}
				if (num > 0L)
				{
					if (slotsEmpty.Count <= 0)
					{
						foreach (KeyValuePair<Point, Item> item2 in dictionary)
						{
							inv[item2.Key.X][item2.Key.Y] = item2.Value.Clone();
						}
						result = false;
						break;
					}
					Comparison<Point> comparison;
					if ((comparison = CustomCurrencySystem.<>O.<0>__CompareYReverse) == null)
					{
						comparison = (CustomCurrencySystem.<>O.<0>__CompareYReverse = new Comparison<Point>(DelegateMethods.CompareYReverse));
					}
					slotsEmpty.Sort(comparison);
					Point item3;
					item3..ctor(-1, -1);
					for (int j = 0; j < inv.Count; j++)
					{
						num2 = 10000L;
						for (int k = 0; k < 3; k++)
						{
							if (num >= num2)
							{
								foreach (Point slotCoin2 in slotCoins)
								{
									if (slotCoin2.X == j && inv[slotCoin2.X][slotCoin2.Y].type == 74 - k && inv[slotCoin2.X][slotCoin2.Y].stack >= 1)
									{
										List<Point> list = slotsEmpty;
										if (j == 1 && slotEmptyBank.Count > 0)
										{
											list = slotEmptyBank;
										}
										if (j == 2 && slotEmptyBank2.Count > 0)
										{
											list = slotEmptyBank2;
										}
										if (j == 3 && slotEmptyBank3.Count > 0)
										{
											list = slotEmptyBank3;
										}
										if (j == 4 && slotEmptyBank4.Count > 0)
										{
											list = slotEmptyBank4;
										}
										Item item4 = inv[slotCoin2.X][slotCoin2.Y];
										int num4 = item4.stack - 1;
										item4.stack = num4;
										if (num4 <= 0)
										{
											inv[slotCoin2.X][slotCoin2.Y].SetDefaults(0);
											list.Add(slotCoin2);
										}
										dictionary[list[0]] = inv[list[0].X][list[0].Y].Clone();
										inv[list[0].X][list[0].Y].SetDefaults(73 - k);
										inv[list[0].X][list[0].Y].stack = 100;
										item3 = list[0];
										list.RemoveAt(0);
										break;
									}
								}
							}
							if (item3.X != -1 || item3.Y != -1)
							{
								break;
							}
							num2 /= 100L;
						}
						for (int l = 0; l < 2; l++)
						{
							if (item3.X == -1 && item3.Y == -1)
							{
								foreach (Point slotCoin3 in slotCoins)
								{
									if (slotCoin3.X == j && inv[slotCoin3.X][slotCoin3.Y].type == 73 + l && inv[slotCoin3.X][slotCoin3.Y].stack >= 1)
									{
										List<Point> list2 = slotsEmpty;
										if (j == 1 && slotEmptyBank.Count > 0)
										{
											list2 = slotEmptyBank;
										}
										if (j == 2 && slotEmptyBank2.Count > 0)
										{
											list2 = slotEmptyBank2;
										}
										if (j == 3 && slotEmptyBank3.Count > 0)
										{
											list2 = slotEmptyBank3;
										}
										if (j == 4 && slotEmptyBank4.Count > 0)
										{
											list2 = slotEmptyBank4;
										}
										Item item5 = inv[slotCoin3.X][slotCoin3.Y];
										int num4 = item5.stack - 1;
										item5.stack = num4;
										if (num4 <= 0)
										{
											inv[slotCoin3.X][slotCoin3.Y].SetDefaults(0);
											list2.Add(slotCoin3);
										}
										dictionary[list2[0]] = inv[list2[0].X][list2[0].Y].Clone();
										inv[list2[0].X][list2[0].Y].SetDefaults(72 + l);
										inv[list2[0].X][list2[0].Y].stack = 100;
										item3 = list2[0];
										list2.RemoveAt(0);
										break;
									}
								}
							}
						}
						if (item3.X != -1 && item3.Y != -1)
						{
							slotCoins.Add(item3);
							break;
						}
					}
					Comparison<Point> comparison2;
					if ((comparison2 = CustomCurrencySystem.<>O.<0>__CompareYReverse) == null)
					{
						comparison2 = (CustomCurrencySystem.<>O.<0>__CompareYReverse = new Comparison<Point>(DelegateMethods.CompareYReverse));
					}
					slotsEmpty.Sort(comparison2);
					Comparison<Point> comparison3;
					if ((comparison3 = CustomCurrencySystem.<>O.<0>__CompareYReverse) == null)
					{
						comparison3 = (CustomCurrencySystem.<>O.<0>__CompareYReverse = new Comparison<Point>(DelegateMethods.CompareYReverse));
					}
					slotEmptyBank.Sort(comparison3);
					Comparison<Point> comparison4;
					if ((comparison4 = CustomCurrencySystem.<>O.<0>__CompareYReverse) == null)
					{
						comparison4 = (CustomCurrencySystem.<>O.<0>__CompareYReverse = new Comparison<Point>(DelegateMethods.CompareYReverse));
					}
					slotEmptyBank2.Sort(comparison4);
					Comparison<Point> comparison5;
					if ((comparison5 = CustomCurrencySystem.<>O.<0>__CompareYReverse) == null)
					{
						comparison5 = (CustomCurrencySystem.<>O.<0>__CompareYReverse = new Comparison<Point>(DelegateMethods.CompareYReverse));
					}
					slotEmptyBank3.Sort(comparison5);
					Comparison<Point> comparison6;
					if ((comparison6 = CustomCurrencySystem.<>O.<0>__CompareYReverse) == null)
					{
						comparison6 = (CustomCurrencySystem.<>O.<0>__CompareYReverse = new Comparison<Point>(DelegateMethods.CompareYReverse));
					}
					slotEmptyBank4.Sort(comparison6);
				}
			}
			return result;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x005A9860 File Offset: 0x005A7A60
		public virtual bool Accepts(Item item)
		{
			return this._valuePerUnit.ContainsKey(item.type);
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x005A9873 File Offset: 0x005A7A73
		public virtual void DrawSavingsMoney(SpriteBatch sb, string text, float shopx, float shopy, long totalCoins, bool horizontal = false)
		{
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x005A9875 File Offset: 0x005A7A75
		public virtual void GetPriceText(string[] lines, ref int currentLine, long price)
		{
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x005A9877 File Offset: 0x005A7A77
		protected int SortByHighest(Tuple<int, int> valueA, Tuple<int, int> valueB)
		{
			if (valueA.Item2 > valueB.Item2)
			{
				return -1;
			}
			if (valueA.Item2 == valueB.Item2)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x005A989C File Offset: 0x005A7A9C
		protected List<Tuple<Point, Item>> ItemCacheCreate(List<Item[]> inventories)
		{
			List<Tuple<Point, Item>> list = new List<Tuple<Point, Item>>();
			for (int i = 0; i < inventories.Count; i++)
			{
				for (int j = 0; j < inventories[i].Length; j++)
				{
					Item item = inventories[i][j];
					list.Add(new Tuple<Point, Item>(new Point(i, j), item.DeepClone()));
				}
			}
			return list;
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x005A98F8 File Offset: 0x005A7AF8
		protected void ItemCacheRestore(List<Tuple<Point, Item>> cache, List<Item[]> inventories)
		{
			foreach (Tuple<Point, Item> item in cache)
			{
				inventories[item.Item1.X][item.Item1.Y] = item.Item2;
			}
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x005A9964 File Offset: 0x005A7B64
		public virtual void GetItemExpectedPrice(Item item, out long calcForSelling, out long calcForBuying)
		{
			int storeValue = item.GetStoreValue();
			calcForSelling = (long)storeValue;
			calcForBuying = (long)storeValue;
		}

		// Token: 0x04005408 RID: 21512
		protected Dictionary<int, int> _valuePerUnit = new Dictionary<int, int>();

		// Token: 0x04005409 RID: 21513
		private long _currencyCap = 999999999L;

		// Token: 0x02000BCE RID: 3022
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400772D RID: 30509
			public static Comparison<Point> <0>__CompareYReverse;
		}
	}
}
