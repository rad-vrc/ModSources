using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200020B RID: 523
	public class ItemPickupCondition : AchievementCondition
	{
		// Token: 0x06001DC8 RID: 7624 RVA: 0x00506E3C File Offset: 0x0050503C
		private ItemPickupCondition(short itemId) : base("ITEM_PICKUP_" + itemId)
		{
			this._itemIds = new short[]
			{
				itemId
			};
			ItemPickupCondition.ListenForPickup(this);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00506E6A File Offset: 0x0050506A
		private ItemPickupCondition(short[] itemIds) : base("ITEM_PICKUP_" + itemIds[0])
		{
			this._itemIds = itemIds;
			ItemPickupCondition.ListenForPickup(this);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00506E94 File Offset: 0x00505094
		private static void ListenForPickup(ItemPickupCondition condition)
		{
			if (!ItemPickupCondition._isListenerHooked)
			{
				AchievementsHelper.OnItemPickup += ItemPickupCondition.ItemPickupListener;
				ItemPickupCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._itemIds.Length; i++)
			{
				if (!ItemPickupCondition._listeners.ContainsKey(condition._itemIds[i]))
				{
					ItemPickupCondition._listeners[condition._itemIds[i]] = new List<ItemPickupCondition>();
				}
				ItemPickupCondition._listeners[condition._itemIds[i]].Add(condition);
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00506F18 File Offset: 0x00505118
		private static void ItemPickupListener(Player player, short itemId, int count)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (ItemPickupCondition._listeners.ContainsKey(itemId))
			{
				foreach (ItemPickupCondition itemPickupCondition in ItemPickupCondition._listeners[itemId])
				{
					itemPickupCondition.Complete();
				}
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00506F88 File Offset: 0x00505188
		public static AchievementCondition Create(params short[] items)
		{
			return new ItemPickupCondition(items);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00506F90 File Offset: 0x00505190
		public static AchievementCondition Create(short item)
		{
			return new ItemPickupCondition(item);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00506F98 File Offset: 0x00505198
		public static AchievementCondition[] CreateMany(params short[] items)
		{
			AchievementCondition[] array = new AchievementCondition[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = new ItemPickupCondition(items[i]);
			}
			return array;
		}

		// Token: 0x04004572 RID: 17778
		private const string Identifier = "ITEM_PICKUP";

		// Token: 0x04004573 RID: 17779
		private static Dictionary<short, List<ItemPickupCondition>> _listeners = new Dictionary<short, List<ItemPickupCondition>>();

		// Token: 0x04004574 RID: 17780
		private static bool _isListenerHooked;

		// Token: 0x04004575 RID: 17781
		private short[] _itemIds;
	}
}
