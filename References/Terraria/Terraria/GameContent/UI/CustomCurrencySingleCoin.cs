﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000336 RID: 822
	public class CustomCurrencySingleCoin : CustomCurrencySystem
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x00568194 File Offset: 0x00566394
		public CustomCurrencySingleCoin(int coinItemID, long currencyCap)
		{
			base.Include(coinItemID, 1);
			base.SetCurrencyCap(currencyCap);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x005681E0 File Offset: 0x005663E0
		public override bool TryPurchasing(long price, List<Item[]> inv, List<Point> slotCoins, List<Point> slotsEmpty, List<Point> slotEmptyBank, List<Point> slotEmptyBank2, List<Point> slotEmptyBank3, List<Point> slotEmptyBank4)
		{
			List<Tuple<Point, Item>> cache = base.ItemCacheCreate(inv);
			long num = price;
			for (int i = 0; i < slotCoins.Count; i++)
			{
				Point point = slotCoins[i];
				long num2 = num;
				if ((long)inv[point.X][point.Y].stack < num2)
				{
					num2 = (long)inv[point.X][point.Y].stack;
				}
				num -= num2;
				inv[point.X][point.Y].stack -= (int)num2;
				if (inv[point.X][point.Y].stack == 0)
				{
					switch (point.X)
					{
					case 0:
						slotsEmpty.Add(point);
						break;
					case 1:
						slotEmptyBank.Add(point);
						break;
					case 2:
						slotEmptyBank2.Add(point);
						break;
					case 3:
						slotEmptyBank3.Add(point);
						break;
					case 4:
						slotEmptyBank4.Add(point);
						break;
					}
					slotCoins.Remove(point);
					i--;
				}
				if (num == 0L)
				{
					break;
				}
			}
			if (num != 0L)
			{
				base.ItemCacheRestore(cache, inv);
				return false;
			}
			return true;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00568304 File Offset: 0x00566504
		public override void DrawSavingsMoney(SpriteBatch sb, string text, float shopx, float shopy, long totalCoins, bool horizontal = false)
		{
			int num = this._valuePerUnit.Keys.ElementAt(0);
			Main.instance.LoadItem(num);
			Texture2D value = TextureAssets.Item[num].Value;
			if (horizontal)
			{
				Vector2 vector = new Vector2(shopx + ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One, -1f).X + 45f, shopy + 50f);
				sb.Draw(value, vector, null, Color.White, 0f, value.Size() / 2f, this.CurrencyDrawScale, SpriteEffects.None, 0f);
				Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, totalCoins.ToString(), vector.X - 11f, vector.Y, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
				return;
			}
			int num2 = (totalCoins > 99L) ? -6 : 0;
			sb.Draw(value, new Vector2(shopx + 11f, shopy + 75f), null, Color.White, 0f, value.Size() / 2f, this.CurrencyDrawScale, SpriteEffects.None, 0f);
			Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, totalCoins.ToString(), shopx + (float)num2, shopy + 75f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x00568490 File Offset: 0x00566690
		public override void GetPriceText(string[] lines, ref int currentLine, long price)
		{
			Color color = this.CurrencyTextColor * ((float)Main.mouseTextColor / 255f);
			int num = currentLine;
			currentLine = num + 1;
			lines[num] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
			{
				color.R,
				color.G,
				color.B,
				Lang.tip[50].Value,
				price,
				Language.GetTextValue(this.CurrencyTextKey).ToLower()
			});
		}

		// Token: 0x04004905 RID: 18693
		public float CurrencyDrawScale = 0.8f;

		// Token: 0x04004906 RID: 18694
		public string CurrencyTextKey = "Currency.DefenderMedals";

		// Token: 0x04004907 RID: 18695
		public Color CurrencyTextColor = new Color(240, 100, 120);
	}
}
