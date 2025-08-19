using System;
using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x0200020C RID: 524
	public class NPCKilledCondition : AchievementCondition
	{
		// Token: 0x06001DD0 RID: 7632 RVA: 0x00506FD4 File Offset: 0x005051D4
		private NPCKilledCondition(short npcId) : base("NPC_KILLED_" + npcId)
		{
			this._npcIds = new short[]
			{
				npcId
			};
			NPCKilledCondition.ListenForPickup(this);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00507002 File Offset: 0x00505202
		private NPCKilledCondition(short[] npcIds) : base("NPC_KILLED_" + npcIds[0])
		{
			this._npcIds = npcIds;
			NPCKilledCondition.ListenForPickup(this);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0050702C File Offset: 0x0050522C
		private static void ListenForPickup(NPCKilledCondition condition)
		{
			if (!NPCKilledCondition._isListenerHooked)
			{
				AchievementsHelper.OnNPCKilled += NPCKilledCondition.NPCKilledListener;
				NPCKilledCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._npcIds.Length; i++)
			{
				if (!NPCKilledCondition._listeners.ContainsKey(condition._npcIds[i]))
				{
					NPCKilledCondition._listeners[condition._npcIds[i]] = new List<NPCKilledCondition>();
				}
				NPCKilledCondition._listeners[condition._npcIds[i]].Add(condition);
			}
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x005070B0 File Offset: 0x005052B0
		private static void NPCKilledListener(Player player, short npcId)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (NPCKilledCondition._listeners.ContainsKey(npcId))
			{
				foreach (NPCKilledCondition npckilledCondition in NPCKilledCondition._listeners[npcId])
				{
					npckilledCondition.Complete();
				}
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x00507120 File Offset: 0x00505320
		public static AchievementCondition Create(params short[] npcIds)
		{
			return new NPCKilledCondition(npcIds);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00507128 File Offset: 0x00505328
		public static AchievementCondition Create(short npcId)
		{
			return new NPCKilledCondition(npcId);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00507130 File Offset: 0x00505330
		public static AchievementCondition[] CreateMany(params short[] npcs)
		{
			AchievementCondition[] array = new AchievementCondition[npcs.Length];
			for (int i = 0; i < npcs.Length; i++)
			{
				array[i] = new NPCKilledCondition(npcs[i]);
			}
			return array;
		}

		// Token: 0x04004576 RID: 17782
		private const string Identifier = "NPC_KILLED";

		// Token: 0x04004577 RID: 17783
		private static Dictionary<short, List<NPCKilledCondition>> _listeners = new Dictionary<short, List<NPCKilledCondition>>();

		// Token: 0x04004578 RID: 17784
		private static bool _isListenerHooked;

		// Token: 0x04004579 RID: 17785
		private short[] _npcIds;
	}
}
