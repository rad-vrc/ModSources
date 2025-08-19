using System;
using System.Collections.Generic;

namespace Terraria.GameContent
{
	// Token: 0x020001FF RID: 511
	public class ItemShopSellbackHelper
	{
		// Token: 0x06001D3A RID: 7482 RVA: 0x005013CC File Offset: 0x004FF5CC
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

		// Token: 0x06001D3B RID: 7483 RVA: 0x00501430 File Offset: 0x004FF630
		public void Clear()
		{
			this._memos.Clear();
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00501440 File Offset: 0x004FF640
		public int GetAmount(Item item)
		{
			ItemShopSellbackHelper.ItemMemo itemMemo = this._memos.Find((ItemShopSellbackHelper.ItemMemo x) => x.Matches(item));
			if (itemMemo != null)
			{
				return itemMemo.stack;
			}
			return 0;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00501480 File Offset: 0x004FF680
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

		// Token: 0x0400441D RID: 17437
		private List<ItemShopSellbackHelper.ItemMemo> _memos = new List<ItemShopSellbackHelper.ItemMemo>();

		// Token: 0x0200061C RID: 1564
		private class ItemMemo
		{
			// Token: 0x0600338E RID: 13198 RVA: 0x00606955 File Offset: 0x00604B55
			public ItemMemo(Item item)
			{
				this.itemNetID = item.netID;
				this.itemPrefix = (int)item.prefix;
				this.stack = item.stack;
			}

			// Token: 0x0600338F RID: 13199 RVA: 0x00606981 File Offset: 0x00604B81
			public bool Matches(Item item)
			{
				return item.netID == this.itemNetID && (int)item.prefix == this.itemPrefix;
			}

			// Token: 0x040060A8 RID: 24744
			public readonly int itemNetID;

			// Token: 0x040060A9 RID: 24745
			public readonly int itemPrefix;

			// Token: 0x040060AA RID: 24746
			public int stack;
		}
	}
}
