using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x020004A1 RID: 1185
	public class ItemShopSellbackHelper
	{
		// Token: 0x06003944 RID: 14660 RVA: 0x0059766C File Offset: 0x0059586C
		public void Add(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo != null)
			{
				itemMemo.stack += item.stack;
				return;
			}
			this._memos.Add(new ItemShopSellbackHelper.ItemMemo(item));
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x005976D0 File Offset: 0x005958D0
		public void Clear()
		{
			this._memos.Clear();
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x005976E0 File Offset: 0x005958E0
		public int GetAmount(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo == null)
			{
				return 0;
			}
			return itemMemo.stack;
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x0059771C File Offset: 0x0059591C
		public int Remove(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo == null)
			{
				return 0;
			}
			int stack = itemMemo.stack;
			itemMemo.stack -= item.stack;
			if (itemMemo.stack <= 0)
			{
				this._memos.Remove(itemMemo);
				return stack;
			}
			return stack - itemMemo.stack;
		}

		// Token: 0x04005256 RID: 21078
		private List<ItemShopSellbackHelper.ItemMemo> _memos = new List<ItemShopSellbackHelper.ItemMemo>();

		// Token: 0x02000BA9 RID: 2985
		private class ItemMemo
		{
			// Token: 0x06005D7F RID: 23935 RVA: 0x006C8BF4 File Offset: 0x006C6DF4
			public ItemMemo(Item item)
			{
				this.itemNetID = item.netID;
				this.itemPrefix = item.prefix;
				this.stack = item.stack;
				this.value = ((item.shopSpecialCurrency == -1) ? item.GetStoreValue() : item.value);
			}

			// Token: 0x06005D80 RID: 23936 RVA: 0x006C8C48 File Offset: 0x006C6E48
			public bool Matches(Item item)
			{
				return item.GetStoreValue() == this.value && item.netID == this.itemNetID && item.prefix == this.itemPrefix;
			}

			// Token: 0x040076B8 RID: 30392
			public readonly int itemNetID;

			// Token: 0x040076B9 RID: 30393
			public readonly int itemPrefix;

			// Token: 0x040076BA RID: 30394
			public int stack;

			// Token: 0x040076BB RID: 30395
			public int value;
		}
	}
}
