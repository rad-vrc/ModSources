using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x020004C7 RID: 1223
	public class CustomCurrencySingleCoin : CustomCurrencySystem
	{
		// Token: 0x06003A77 RID: 14967 RVA: 0x005A8C84 File Offset: 0x005A6E84
		public CustomCurrencySingleCoin(int coinItemID, long currencyCap)
		{
			base.Include(coinItemID, 1);
			base.SetCurrencyCap(currencyCap);
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x005A8CD0 File Offset: 0x005A6ED0
		public override bool TryPurchasing(long price, List<Item[]> inv, List<Point> slotCoins, List<Point> slotsEmpty, List<Point> slotEmptyBank, List<Point> slotEmptyBank2, List<Point> slotEmptyBank3, List<Point> slotEmptyBank4)
		{
			List<Tuple<Point, Item>> cache = base.ItemCacheCreate(inv);
			long num = price;
			for (int i = 0; i < slotCoins.Count; i++)
			{
				Point item = slotCoins[i];
				long num2 = num;
				if ((long)inv[item.X][item.Y].stack < num2)
				{
					num2 = (long)inv[item.X][item.Y].stack;
				}
				num -= num2;
				inv[item.X][item.Y].stack -= (int)num2;
				if (inv[item.X][item.Y].stack == 0)
				{
					switch (item.X)
					{
					case 0:
						slotsEmpty.Add(item);
						break;
					case 1:
						slotEmptyBank.Add(item);
						break;
					case 2:
						slotEmptyBank2.Add(item);
						break;
					case 3:
						slotEmptyBank3.Add(item);
						break;
					case 4:
						slotEmptyBank4.Add(item);
						break;
					}
					slotCoins.Remove(item);
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

		// Token: 0x06003A79 RID: 14969 RVA: 0x005A8DF4 File Offset: 0x005A6FF4
		public override void DrawSavingsMoney(SpriteBatch sb, string text, float shopx, float shopy, long totalCoins, bool horizontal = false)
		{
			int num = this._valuePerUnit.Keys.ElementAt(0);
			Main.instance.LoadItem(num);
			Texture2D value = TextureAssets.Item[num].Value;
			if (horizontal)
			{
				Vector2 position;
				position..ctor(shopx + ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One, -1f).X + 45f, shopy + 50f);
				sb.Draw(value, position, null, Color.White, 0f, value.Size() / 2f, this.CurrencyDrawScale, 0, 0f);
				Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, totalCoins.ToString(), position.X - 11f, position.Y, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
				return;
			}
			int num2 = (totalCoins > 99L) ? -6 : 0;
			sb.Draw(value, new Vector2(shopx + 11f, shopy + 75f), null, Color.White, 0f, value.Size() / 2f, this.CurrencyDrawScale, 0, 0f);
			Utils.DrawBorderStringFourWay(sb, FontAssets.ItemStack.Value, totalCoins.ToString(), shopx + (float)num2, shopy + 75f, Color.White, Color.Black, new Vector2(0.3f), 0.75f);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x005A8F78 File Offset: 0x005A7178
		public override void GetPriceText(string[] lines, ref int currentLine, long price)
		{
			Color color = this.CurrencyTextColor * ((float)Main.mouseTextColor / 255f);
			int num = currentLine;
			currentLine = num + 1;
			int num2 = num;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 6);
			defaultInterpolatedStringHandler.AppendLiteral("[c/");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(color.R, "X2");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(color.G, "X2");
			defaultInterpolatedStringHandler.AppendFormatted<byte>(color.B, "X2");
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted(Lang.tip[50].Value);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted<long>(price);
			defaultInterpolatedStringHandler.AppendLiteral(" ");
			defaultInterpolatedStringHandler.AppendFormatted(Language.GetTextValue(this.CurrencyTextKey).ToLower());
			defaultInterpolatedStringHandler.AppendLiteral("]");
			lines[num2] = defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x04005405 RID: 21509
		public float CurrencyDrawScale = 0.8f;

		// Token: 0x04005406 RID: 21510
		public string CurrencyTextKey = "Currency.DefenderMedals";

		// Token: 0x04005407 RID: 21511
		public Color CurrencyTextColor = new Color(240, 100, 120);
	}
}
