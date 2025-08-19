using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200020A RID: 522
	public class ItemCraftCondition : AchievementCondition
	{
		// Token: 0x06001DC0 RID: 7616 RVA: 0x00506CB1 File Offset: 0x00504EB1
		private ItemCraftCondition(short itemId) : base("ITEM_PICKUP_" + itemId)
		{
			this._itemIds = new short[]
			{
				itemId
			};
			ItemCraftCondition.ListenForCraft(this);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00506CDF File Offset: 0x00504EDF
		private ItemCraftCondition(short[] itemIds) : base("ITEM_PICKUP_" + itemIds[0])
		{
			this._itemIds = itemIds;
			ItemCraftCondition.ListenForCraft(this);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00506D08 File Offset: 0x00504F08
		private static void ListenForCraft(ItemCraftCondition condition)
		{
			if (!ItemCraftCondition._isListenerHooked)
			{
				AchievementsHelper.OnItemCraft += ItemCraftCondition.ItemCraftListener;
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

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00506D8C File Offset: 0x00504F8C
		private static void ItemCraftListener(short itemId, int count)
		{
			if (ItemCraftCondition._listeners.ContainsKey(itemId))
			{
				foreach (ItemCraftCondition itemCraftCondition in ItemCraftCondition._listeners[itemId])
				{
					itemCraftCondition.Complete();
				}
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00506DF0 File Offset: 0x00504FF0
		public static AchievementCondition Create(params short[] items)
		{
			return new ItemCraftCondition(items);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00506DF8 File Offset: 0x00504FF8
		public static AchievementCondition Create(short item)
		{
			return new ItemCraftCondition(item);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00506E00 File Offset: 0x00505000
		public static AchievementCondition[] CreateMany(params short[] items)
		{
			AchievementCondition[] array = new AchievementCondition[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = new ItemCraftCondition(items[i]);
			}
			return array;
		}

		// Token: 0x0400456E RID: 17774
		private const string Identifier = "ITEM_PICKUP";

		// Token: 0x0400456F RID: 17775
		private static Dictionary<short, List<ItemCraftCondition>> _listeners = new Dictionary<short, List<ItemCraftCondition>>();

		// Token: 0x04004570 RID: 17776
		private static bool _isListenerHooked;

		// Token: 0x04004571 RID: 17777
		private short[] _itemIds;
	}
}
