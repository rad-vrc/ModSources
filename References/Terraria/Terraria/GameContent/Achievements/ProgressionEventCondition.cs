using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200020D RID: 525
	public class ProgressionEventCondition : AchievementCondition
	{
		// Token: 0x06001DD8 RID: 7640 RVA: 0x0050716C File Offset: 0x0050536C
		private ProgressionEventCondition(int eventID) : base("PROGRESSION_EVENT_" + eventID)
		{
			this._eventIDs = new int[]
			{
				eventID
			};
			ProgressionEventCondition.ListenForPickup(this);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x0050719A File Offset: 0x0050539A
		private ProgressionEventCondition(int[] eventIDs) : base("PROGRESSION_EVENT_" + eventIDs[0])
		{
			this._eventIDs = eventIDs;
			ProgressionEventCondition.ListenForPickup(this);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x005071C4 File Offset: 0x005053C4
		private static void ListenForPickup(ProgressionEventCondition condition)
		{
			if (!ProgressionEventCondition._isListenerHooked)
			{
				AchievementsHelper.OnProgressionEvent += ProgressionEventCondition.ProgressionEventListener;
				ProgressionEventCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._eventIDs.Length; i++)
			{
				if (!ProgressionEventCondition._listeners.ContainsKey(condition._eventIDs[i]))
				{
					ProgressionEventCondition._listeners[condition._eventIDs[i]] = new List<ProgressionEventCondition>();
				}
				ProgressionEventCondition._listeners[condition._eventIDs[i]].Add(condition);
			}
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00507248 File Offset: 0x00505448
		private static void ProgressionEventListener(int eventID)
		{
			if (ProgressionEventCondition._listeners.ContainsKey(eventID))
			{
				foreach (ProgressionEventCondition progressionEventCondition in ProgressionEventCondition._listeners[eventID])
				{
					progressionEventCondition.Complete();
				}
			}
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x005072AC File Offset: 0x005054AC
		public static ProgressionEventCondition Create(params int[] eventIDs)
		{
			return new ProgressionEventCondition(eventIDs);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x005072B4 File Offset: 0x005054B4
		public static ProgressionEventCondition Create(int eventID)
		{
			return new ProgressionEventCondition(eventID);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x005072BC File Offset: 0x005054BC
		public static ProgressionEventCondition[] CreateMany(params int[] eventIDs)
		{
			ProgressionEventCondition[] array = new ProgressionEventCondition[eventIDs.Length];
			for (int i = 0; i < eventIDs.Length; i++)
			{
				array[i] = new ProgressionEventCondition(eventIDs[i]);
			}
			return array;
		}

		// Token: 0x0400457A RID: 17786
		private const string Identifier = "PROGRESSION_EVENT";

		// Token: 0x0400457B RID: 17787
		private static Dictionary<int, List<ProgressionEventCondition>> _listeners = new Dictionary<int, List<ProgressionEventCondition>>();

		// Token: 0x0400457C RID: 17788
		private static bool _isListenerHooked;

		// Token: 0x0400457D RID: 17789
		private int[] _eventIDs;
	}
}
