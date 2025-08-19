using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006BB RID: 1723
	public class ItemCraftCondition : AchievementCondition
	{
		// Token: 0x060048D0 RID: 18640 RVA: 0x0064BA15 File Offset: 0x00649C15
		private ItemCraftCondition(short itemId) : base("ITEM_PICKUP_" + itemId.ToString())
		{
			this._itemIds = new short[]
			{
				itemId
			};
			ItemCraftCondition.ListenForCraft(this);
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0064BA44 File Offset: 0x00649C44
		private ItemCraftCondition(short[] itemIds) : base("ITEM_PICKUP_" + itemIds[0].ToString())
		{
			this._itemIds = itemIds;
			ItemCraftCondition.ListenForCraft(this);
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x0064BA70 File Offset: 0x00649C70
		private static void ListenForCraft(ItemCraftCondition condition)
		{
			if (!ItemCraftCondition._isListenerHooked)
			{
				AchievementsHelper.ItemCraftEvent value;
				if ((value = ItemCraftCondition.<>O.<0>__ItemCraftListener) == null)
				{
					value = (ItemCraftCondition.<>O.<0>__ItemCraftListener = new AchievementsHelper.ItemCraftEvent(ItemCraftCondition.ItemCraftListener));
				}
				AchievementsHelper.OnItemCraft += value;
				ItemCraftCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._itemIds.Length; i++)
			{
				if (!ItemCraftCondition._listeners.ContainsKey(condition._itemIds[i]))
				{
					ItemCraftCondition._listeners[condition._itemIds[i]] = new List<ItemCraftCondition>();
				}
				ItemCraftCondition._listeners[condition._itemIds[i]].Add(condition);
			}
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x0064BB00 File Offset: 0x00649D00
		private static void ItemCraftListener(short itemId, int count)
		{
			if (!ItemCraftCondition._listeners.ContainsKey(itemId))
			{
				return;
			}
			foreach (ItemCraftCondition itemCraftCondition in ItemCraftCondition._listeners[itemId])
			{
				itemCraftCondition.Complete();
			}
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x0064BB64 File Offset: 0x00649D64
		public static AchievementCondition Create(params short[] items)
		{
			return new ItemCraftCondition(items);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x0064BB6C File Offset: 0x00649D6C
		public static AchievementCondition Create(short item)
		{
			return new ItemCraftCondition(item);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x0064BB74 File Offset: 0x00649D74
		public static AchievementCondition[] CreateMany(params short[] items)
		{
			AchievementCondition[] array = new AchievementCondition[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = new ItemCraftCondition(items[i]);
			}
			return array;
		}

		// Token: 0x04005C74 RID: 23668
		private const string Identifier = "ITEM_PICKUP";

		// Token: 0x04005C75 RID: 23669
		private static Dictionary<short, List<ItemCraftCondition>> _listeners = new Dictionary<short, List<ItemCraftCondition>>();

		// Token: 0x04005C76 RID: 23670
		private static bool _isListenerHooked;

		// Token: 0x04005C77 RID: 23671
		private short[] _itemIds;

		// Token: 0x02000D54 RID: 3412
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B7D RID: 31613
			public static AchievementsHelper.ItemCraftEvent <0>__ItemCraftListener;
		}
	}
}
