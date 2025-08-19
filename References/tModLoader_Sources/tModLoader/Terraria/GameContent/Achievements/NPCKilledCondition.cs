using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006BD RID: 1725
	public class NPCKilledCondition : AchievementCondition
	{
		// Token: 0x060048E0 RID: 18656 RVA: 0x0064BD58 File Offset: 0x00649F58
		private NPCKilledCondition(short npcId) : base("NPC_KILLED_" + npcId.ToString())
		{
			this._npcIds = new short[]
			{
				npcId
			};
			NPCKilledCondition.ListenForPickup(this);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0064BD87 File Offset: 0x00649F87
		private NPCKilledCondition(short[] npcIds) : base("NPC_KILLED_" + npcIds[0].ToString())
		{
			this._npcIds = npcIds;
			NPCKilledCondition.ListenForPickup(this);
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x0064BDB4 File Offset: 0x00649FB4
		private static void ListenForPickup(NPCKilledCondition condition)
		{
			if (!NPCKilledCondition._isListenerHooked)
			{
				AchievementsHelper.NPCKilledEvent value;
				if ((value = NPCKilledCondition.<>O.<0>__NPCKilledListener) == null)
				{
					value = (NPCKilledCondition.<>O.<0>__NPCKilledListener = new AchievementsHelper.NPCKilledEvent(NPCKilledCondition.NPCKilledListener));
				}
				AchievementsHelper.OnNPCKilled += value;
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

		// Token: 0x060048E3 RID: 18659 RVA: 0x0064BE44 File Offset: 0x0064A044
		private static void NPCKilledListener(Player player, short npcId)
		{
			if (player.whoAmI != Main.myPlayer || !NPCKilledCondition._listeners.ContainsKey(npcId))
			{
				return;
			}
			foreach (NPCKilledCondition npckilledCondition in NPCKilledCondition._listeners[npcId])
			{
				npckilledCondition.Complete();
			}
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x0064BEB4 File Offset: 0x0064A0B4
		public static AchievementCondition Create(params short[] npcIds)
		{
			return new NPCKilledCondition(npcIds);
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x0064BEBC File Offset: 0x0064A0BC
		public static AchievementCondition Create(short npcId)
		{
			return new NPCKilledCondition(npcId);
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x0064BEC4 File Offset: 0x0064A0C4
		public static AchievementCondition[] CreateMany(params short[] npcs)
		{
			AchievementCondition[] array = new AchievementCondition[npcs.Length];
			for (int i = 0; i < npcs.Length; i++)
			{
				array[i] = new NPCKilledCondition(npcs[i]);
			}
			return array;
		}

		// Token: 0x04005C7C RID: 23676
		private const string Identifier = "NPC_KILLED";

		// Token: 0x04005C7D RID: 23677
		private static Dictionary<short, List<NPCKilledCondition>> _listeners = new Dictionary<short, List<NPCKilledCondition>>();

		// Token: 0x04005C7E RID: 23678
		private static bool _isListenerHooked;

		// Token: 0x04005C7F RID: 23679
		private short[] _npcIds;

		// Token: 0x02000D56 RID: 3414
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B7F RID: 31615
			public static AchievementsHelper.NPCKilledEvent <0>__NPCKilledListener;
		}
	}
}
