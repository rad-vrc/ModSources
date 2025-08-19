using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004C6 RID: 1222
	public class CustomCurrencyManager
	{
		// Token: 0x06003A6A RID: 14954 RVA: 0x005A862C File Offset: 0x005A682C
		public static void Initialize()
		{
			CustomCurrencyManager._nextCurrencyIndex = 0;
			CustomCurrencyManager._currencies.Clear();
			CustomCurrencyID.DefenderMedals = CustomCurrencyManager.RegisterCurrency(new CustomCurrencySingleCoin(3817, 999L));
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x005A8658 File Offset: 0x005A6858
		public static int RegisterCurrency(CustomCurrencySystem collection)
		{
			int nextCurrencyIndex = CustomCurrencyManager._nextCurrencyIndex;
			CustomCurrencyManager._nextCurrencyIndex++;
			CustomCurrencyManager._currencies[nextCurrencyIndex] = collection;
			return nextCurrencyIndex;
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x005A8684 File Offset: 0x005A6884
		public static void DrawSavings(SpriteBatch sb, int currencyIndex, float shopx, float shopy, bool horizontal = false)
		{
			CustomCurrencySystem customCurrencySystem = CustomCurrencyManager._currencies[currencyIndex];
			Player player = Main.player[Main.myPlayer];
			bool overFlowing;
			long num = customCurrencySystem.CountCurrency(out overFlowing, player.bank.item, Array.Empty<int>());
			long num2 = customCurrencySystem.CountCurrency(out overFlowing, player.bank2.item, Array.Empty<int>());
			long num3 = customCurrencySystem.CountCurrency(out overFlowing, player.bank3.item, Array.Empty<int>());
			long num4 = customCurrencySystem.CountCurrency(out overFlowing, player.bank4.item, Array.Empty<int>());
			long num5 = customCurrencySystem.CombineStacks(out overFlowing, new long[]
			{
				num,
				num2,
				num3,
				num4
			});
			if (num5 > 0L)
			{
				Texture2D itemTexture;
				Rectangle itemFrame;
				Main.GetItemDrawFrame(4076, out itemTexture, out itemFrame);
				Texture2D itemTexture2;
				Rectangle itemFrame2;
				Main.GetItemDrawFrame(3813, out itemTexture2, out itemFrame2);
				Texture2D itemTexture3;
				Rectangle itemFrame3;
				Main.GetItemDrawFrame(346, out itemTexture3, out itemFrame3);
				Texture2D itemTexture4;
				Rectangle itemFrame4;
				Main.GetItemDrawFrame(87, out itemTexture4, out itemFrame4);
				if (num4 > 0L)
				{
					sb.Draw(itemTexture, Utils.CenteredRectangle(new Vector2(shopx + 96f, shopy + 50f), itemFrame.Size() * 0.65f), null, Color.White);
				}
				if (num3 > 0L)
				{
					sb.Draw(itemTexture2, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), itemFrame2.Size() * 0.65f), null, Color.White);
				}
				if (num2 > 0L)
				{
					sb.Draw(itemTexture3, Utils.CenteredRectangle(new Vector2(shopx + 80f, shopy + 50f), itemFrame3.Size() * 0.65f), null, Color.White);
				}
				if (num > 0L)
				{
					sb.Draw(itemTexture4, Utils.CenteredRectangle(new Vector2(shopx + 70f, shopy + 60f), itemFrame4.Size() * 0.65f), null, Color.White);
				}
				Utils.DrawBorderStringFourWay(sb, FontAssets.MouseText.Value, Lang.inter[66].Value, shopx, shopy + 40f, Color.White * ((float)Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, 1f);
				customCurrencySystem.DrawSavingsMoney(sb, Lang.inter[66].Value, shopx, shopy, num5, horizontal);
			}
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x005A88EC File Offset: 0x005A6AEC
		public static void GetPriceText(int currencyIndex, string[] lines, ref int currentLine, long price)
		{
			CustomCurrencyManager._currencies[currencyIndex].GetPriceText(lines, ref currentLine, price);
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x005A8901 File Offset: 0x005A6B01
		public static bool BuyItem(Player player, long price, int currencyIndex)
		{
			return CustomCurrencyManager.CanAfford(player, price, currencyIndex) && CustomCurrencyManager.PayCurrency(player, price, currencyIndex);
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x005A8918 File Offset: 0x005A6B18
		public static bool CanAfford(Player player, long price, int currencyIndex = -1)
		{
			CustomCurrencySystem customCurrencySystem = CustomCurrencyManager._currencies[currencyIndex];
			bool overFlowing;
			long num = customCurrencySystem.CountCurrency(out overFlowing, player.inventory, new int[]
			{
				58,
				57,
				56,
				55,
				54
			});
			long num2 = customCurrencySystem.CountCurrency(out overFlowing, player.bank.item, Array.Empty<int>());
			long num3 = customCurrencySystem.CountCurrency(out overFlowing, player.bank2.item, Array.Empty<int>());
			long num4 = customCurrencySystem.CountCurrency(out overFlowing, player.bank3.item, Array.Empty<int>());
			long num5 = customCurrencySystem.CountCurrency(out overFlowing, player.bank4.item, Array.Empty<int>());
			return customCurrencySystem.CombineStacks(out overFlowing, new long[]
			{
				num,
				num2,
				num3,
				num4,
				num5
			}) >= price;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x005A89E4 File Offset: 0x005A6BE4
		public static bool PayCurrency(Player player, long price, int currencyIndex = -1)
		{
			CustomCurrencySystem customCurrencySystem = CustomCurrencyManager._currencies[currencyIndex];
			List<Item[]> list = new List<Item[]>();
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			List<Point> list2 = new List<Point>();
			List<Point> list3 = new List<Point>();
			List<Point> list4 = new List<Point>();
			List<Point> list5 = new List<Point>();
			List<Point> list6 = new List<Point>();
			List<Point> list7 = new List<Point>();
			list.Add(player.inventory);
			list.Add(player.bank.item);
			list.Add(player.bank2.item);
			list.Add(player.bank3.item);
			list.Add(player.bank4.item);
			for (int i = 0; i < list.Count; i++)
			{
				dictionary[i] = new List<int>();
			}
			dictionary[0] = new List<int>
			{
				58,
				57,
				56,
				55,
				54
			};
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < list[j].Length; k++)
				{
					if (!dictionary[j].Contains(k) && customCurrencySystem.Accepts(list[j][k]))
					{
						list3.Add(new Point(j, k));
					}
				}
			}
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list2, 0);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list4, 1);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list5, 2);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list6, 3);
			CustomCurrencyManager.FindEmptySlots(list, dictionary, list7, 4);
			return customCurrencySystem.TryPurchasing(price, list, list3, list2, list4, list5, list6, list7);
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x005A8B84 File Offset: 0x005A6D84
		private static void FindEmptySlots(List<Item[]> inventories, Dictionary<int, List<int>> slotsToIgnore, List<Point> emptySlots, int currentInventoryIndex)
		{
			for (int num = inventories[currentInventoryIndex].Length - 1; num >= 0; num--)
			{
				if (!slotsToIgnore[currentInventoryIndex].Contains(num) && (inventories[currentInventoryIndex][num].type == 0 || inventories[currentInventoryIndex][num].stack == 0))
				{
					emptySlots.Add(new Point(currentInventoryIndex, num));
				}
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x005A8BE4 File Offset: 0x005A6DE4
		public static bool IsCustomCurrency(Item item)
		{
			foreach (KeyValuePair<int, CustomCurrencySystem> currency in CustomCurrencyManager._currencies)
			{
				if (currency.Value.Accepts(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x005A8C48 File Offset: 0x005A6E48
		public static void GetPrices(Item item, out long calcForSelling, out long calcForBuying)
		{
			CustomCurrencyManager._currencies[item.shopSpecialCurrency].GetItemExpectedPrice(item, out calcForSelling, out calcForBuying);
		}

		/// <summary>
		/// Attempts to retrieve a CustomCurrencySystem object with the specified id from the _currencies dictionary.
		/// </summary>
		/// <param name="id">The id of the currency system to retrieve.</param>
		/// <param name="system">When this method returns, contains the retrieved CustomCurrencySystem object, or null if the retrieval failed.</param>
		/// <returns>true if the retrieval was successful; otherwise, false.</returns>
		// Token: 0x06003A74 RID: 14964 RVA: 0x005A8C62 File Offset: 0x005A6E62
		public static bool TryGetCurrencySystem(int id, out CustomCurrencySystem system)
		{
			return CustomCurrencyManager._currencies.TryGetValue(id, out system);
		}

		// Token: 0x04005403 RID: 21507
		private static int _nextCurrencyIndex;

		// Token: 0x04005404 RID: 21508
		private static Dictionary<int, CustomCurrencySystem> _currencies = new Dictionary<int, CustomCurrencySystem>();
	}
}
