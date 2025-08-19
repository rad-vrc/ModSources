using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006BF RID: 1727
	public class TileDestroyedCondition : AchievementCondition
	{
		// Token: 0x060048F0 RID: 18672 RVA: 0x0064C09C File Offset: 0x0064A29C
		private TileDestroyedCondition(ushort[] tileIds) : base("TILE_DESTROYED_" + tileIds[0].ToString())
		{
			this._tileIds = tileIds;
			TileDestroyedCondition.ListenForDestruction(this);
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0064C0C8 File Offset: 0x0064A2C8
		private static void ListenForDestruction(TileDestroyedCondition condition)
		{
			if (!TileDestroyedCondition._isListenerHooked)
			{
				AchievementsHelper.TileDestroyedEvent value;
				if ((value = TileDestroyedCondition.<>O.<0>__TileDestroyedListener) == null)
				{
					value = (TileDestroyedCondition.<>O.<0>__TileDestroyedListener = new AchievementsHelper.TileDestroyedEvent(TileDestroyedCondition.TileDestroyedListener));
				}
				AchievementsHelper.OnTileDestroyed += value;
				TileDestroyedCondition._isListenerHooked = true;
			}
			for (int i = 0; i < condition._tileIds.Length; i++)
			{
				if (!TileDestroyedCondition._listeners.ContainsKey(condition._tileIds[i]))
				{
					TileDestroyedCondition._listeners[condition._tileIds[i]] = new List<TileDestroyedCondition>();
				}
				TileDestroyedCondition._listeners[condition._tileIds[i]].Add(condition);
			}
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x0064C158 File Offset: 0x0064A358
		private static void TileDestroyedListener(Player player, ushort tileId)
		{
			if (player.whoAmI != Main.myPlayer || !TileDestroyedCondition._listeners.ContainsKey(tileId))
			{
				return;
			}
			foreach (TileDestroyedCondition tileDestroyedCondition in TileDestroyedCondition._listeners[tileId])
			{
				tileDestroyedCondition.Complete();
			}
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x0064C1C8 File Offset: 0x0064A3C8
		public static AchievementCondition Create(params ushort[] tileIds)
		{
			return new TileDestroyedCondition(tileIds);
		}

		// Token: 0x04005C84 RID: 23684
		private const string Identifier = "TILE_DESTROYED";

		// Token: 0x04005C85 RID: 23685
		private static Dictionary<ushort, List<TileDestroyedCondition>> _listeners = new Dictionary<ushort, List<TileDestroyedCondition>>();

		// Token: 0x04005C86 RID: 23686
		private static bool _isListenerHooked;

		// Token: 0x04005C87 RID: 23687
		private ushort[] _tileIds;

		// Token: 0x02000D58 RID: 3416
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B81 RID: 31617
			public static AchievementsHelper.TileDestroyedEvent <0>__TileDestroyedListener;
		}
	}
}
