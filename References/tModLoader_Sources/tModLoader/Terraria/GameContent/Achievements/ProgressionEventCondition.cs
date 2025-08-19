using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006BE RID: 1726
	public class ProgressionEventCondition : AchievementCondition
	{
		// Token: 0x060048E8 RID: 18664 RVA: 0x0064BF00 File Offset: 0x0064A100
		private ProgressionEventCondition(int eventID) : base("PROGRESSION_EVENT_" + eventID.ToString())
		{
			this._eventIDs = new int[]
			{
				eventID
			};
			ProgressionEventCondition.ListenForPickup(this);
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x0064BF2F File Offset: 0x0064A12F
		private ProgressionEventCondition(int[] eventIDs) : base("PROGRESSION_EVENT_" + eventIDs[0].ToString())
		{
			this._eventIDs = eventIDs;
			ProgressionEventCondition.ListenForPickup(this);
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x0064BF5C File Offset: 0x0064A15C
		private static void ListenForPickup(ProgressionEventCondition condition)
		{
			if (!ProgressionEventCondition._isListenerHooked)
			{
				AchievementsHelper.ProgressionEventEvent value;
				if ((value = ProgressionEventCondition.<>O.<0>__ProgressionEventListener) == null)
				{
					value = (ProgressionEventCondition.<>O.<0>__ProgressionEventListener = new AchievementsHelper.ProgressionEventEvent(ProgressionEventCondition.ProgressionEventListener));
				}
				AchievementsHelper.OnProgressionEvent += value;
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

		// Token: 0x060048EB RID: 18667 RVA: 0x0064BFEC File Offset: 0x0064A1EC
		private static void ProgressionEventListener(int eventID)
		{
			if (!ProgressionEventCondition._listeners.ContainsKey(eventID))
			{
				return;
			}
			foreach (ProgressionEventCondition progressionEventCondition in ProgressionEventCondition._listeners[eventID])
			{
				progressionEventCondition.Complete();
			}
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x0064C050 File Offset: 0x0064A250
		public static ProgressionEventCondition Create(params int[] eventIDs)
		{
			return new ProgressionEventCondition(eventIDs);
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x0064C058 File Offset: 0x0064A258
		public static ProgressionEventCondition Create(int eventID)
		{
			return new ProgressionEventCondition(eventID);
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x0064C060 File Offset: 0x0064A260
		public static ProgressionEventCondition[] CreateMany(params int[] eventIDs)
		{
			ProgressionEventCondition[] array = new ProgressionEventCondition[eventIDs.Length];
			for (int i = 0; i < eventIDs.Length; i++)
			{
				array[i] = new ProgressionEventCondition(eventIDs[i]);
			}
			return array;
		}

		// Token: 0x04005C80 RID: 23680
		private const string Identifier = "PROGRESSION_EVENT";

		// Token: 0x04005C81 RID: 23681
		private static Dictionary<int, List<ProgressionEventCondition>> _listeners = new Dictionary<int, List<ProgressionEventCondition>>();

		// Token: 0x04005C82 RID: 23682
		private static bool _isListenerHooked;

		// Token: 0x04005C83 RID: 23683
		private int[] _eventIDs;

		// Token: 0x02000D57 RID: 3415
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B80 RID: 31616
			public static AchievementsHelper.ProgressionEventEvent <0>__ProgressionEventListener;
		}
	}
}
