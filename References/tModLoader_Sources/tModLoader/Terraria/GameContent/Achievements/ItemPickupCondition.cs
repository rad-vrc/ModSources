using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006BC RID: 1724
	public class ItemPickupCondition : AchievementCondition
	{
		// Token: 0x060048D8 RID: 18648 RVA: 0x0064BBB0 File Offset: 0x00649DB0
		private ItemPickupCondition(short itemId) : base("ITEM_PICKUP_" + itemId.ToString())
		{
			this._itemIds = new short[]
			{
				itemId
			};
			ItemPickupCondition.ListenForPickup(this);
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0064BBDF File Offset: 0x00649DDF
		private ItemPickupCondition(short[] itemIds) : base("ITEM_PICKUP_" + itemIds[0].ToString())
		{
			this._itemIds = itemIds;
			ItemPickupCondition.ListenForPickup(this);
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x0064BC0C File Offset: 0x00649E0C
		private static void ListenForPickup(ItemPickupCondition condition)
		{
			if (!ItemPickupCondition._isListenerHooked)
			{
				AchievementsHelper.ItemPickupEvent value;
				if ((value = ItemPickupCondition.<>O.<0>__ItemPickupListener) == null)
				{
					value = (ItemPickupCondition.<>O.<0>__ItemPickupListener = new AchievementsHelper.ItemPickupEvent(ItemPickupCondition.ItemPickupListener));
				}
				AchievementsHelper.OnItemPickup += value;
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

		// Token: 0x060048DB RID: 18651 RVA: 0x0064BC9C File Offset: 0x00649E9C
		private static void ItemPickupListener(Player player, short itemId, int count)
		{
			if (player.whoAmI != Main.myPlayer || !ItemPickupCondition._listeners.ContainsKey(itemId))
			{
				return;
			}
			foreach (ItemPickupCondition itemPickupCondition in ItemPickupCondition._listeners[itemId])
			{
				itemPickupCondition.Complete();
			}
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x0064BD0C File Offset: 0x00649F0C
		public static AchievementCondition Create(params short[] items)
		{
			return new ItemPickupCondition(items);
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x0064BD14 File Offset: 0x00649F14
		public static AchievementCondition Create(short item)
		{
			return new ItemPickupCondition(item);
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x0064BD1C File Offset: 0x00649F1C
		public static AchievementCondition[] CreateMany(params short[] items)
		{
			AchievementCondition[] array = new AchievementCondition[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = new ItemPickupCondition(items[i]);
			}
			return array;
		}

		// Token: 0x04005C78 RID: 23672
		private const string Identifier = "ITEM_PICKUP";

		// Token: 0x04005C79 RID: 23673
		private static Dictionary<short, List<ItemPickupCondition>> _listeners = new Dictionary<short, List<ItemPickupCondition>>();

		// Token: 0x04005C7A RID: 23674
		private static bool _isListenerHooked;

		// Token: 0x04005C7B RID: 23675
		private short[] _itemIds;

		// Token: 0x02000D55 RID: 3413
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B7E RID: 31614
			public static AchievementsHelper.ItemPickupEvent <0>__ItemPickupListener;
		}
	}
}
